using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;
using SubSonic;
using System.Data;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class UserBLL
    {
        private static readonly List<string> _PhoneBeginWith = new List<string> { "13", "15", "18","16" };
        public static bool CheckUserName(string userName, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(userName))
            {
                message = ResBLL.Get("check_UserName_empty");
                return false;
            }
            if (!SubSonic.Sugar.Validation.IsNumeric(userName) ||
                userName.Length < 11 ||
                userName.Length > 11 ||
                !_PhoneBeginWith.Contains(userName.Substring(0, 2)))
            {
                message = "请输入有效的手机号码";
                return false;
            }
            return true;
        }
        public static bool CheckUserPwd(string userPwd, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(userPwd))
            {
                message = ResBLL.Get("check_PassWord_empty");
                return false;
            }
            if (!Formatter.IsNumberic(userPwd) || userPwd.Length < 6 || userPwd.Length > 6)
            {
                message = "密码只能为6位数字组合!";
                return false;
            }
            return true;
        }
        public static void UserWebLogin( )
        {
            var p = AppContextBase.Page;
            string txtUserName = p.Request.Form["edtUserName"].Trim( );
            string txtPassWord = p.Request.Form["edtPassWord"].Trim( );
            string txtLoginCode = p.Request.Params["edtCheckOut"].Trim( );
            string CheckCode = p.Request.Cookies["CheckCode"].Value;
            string message;
            if (!CheckUserName(txtUserName, out message))
            {
                AppContextBase.Context.AddMessage("check_userName", message);
                return;
            }

            if (!CheckUserPwd(txtPassWord, out message))
            {
                AppContextBase.Context.AddMessage("check_PassWord", message);
                return;
            }
            if (string.IsNullOrEmpty(txtLoginCode))
            {
                AppContextBase.Context.AddMessage("check_login_code", ResBLL.Get("check_Code_empty"));
                return;
            }
            if (!Utilities.Compare(CheckCode, txtLoginCode))
            {
                AppContextBase.Context.AddMessage("check_login_code", ResBLL.GetRes("check_Code_error", "验证码错误!", "验证码错误显示信息"));
                return;
            }
            if (AppContextBase.Context.ParamSubSys == SubSystem.ALL)
            {
                AppContextBase.Context.AddMessage("message", ResBLL.Get("check_login_subsys_error"));
                return;
            }
            SysMember user;
            int state = UserLogin(txtUserName, txtPassWord, AppContextBase.Context.ParamSubSys, out user);
            if (state == 1)
            {
                AppContextBase.Context.AddMessage("check_userName", ResBLL.Get("check_UserName_notexist"));
                return;
            }
            if (state == 2)
            {
                AppContextBase.Context.AddMessage("check_PassWord", ResBLL.Get("check_PassWord_error"));
                return;
            }
            if (state == 3)
            {
                AppContextBase.Context.AddMessage("message", ResBLL.Get("check_login_subsys_error"));
                return;
            }
            if (state == 4)
            {
                AppContextBase.Context.AddMessage("message", ResBLL.GetRes("check_login_lock", "账号处于审核状态", "账号处于审核状态"));
                return;
            }
            string strRedirectUrl = HttpUtility.UrlDecode(p.Request.Params["ReturnUrl"]);
            if (string.IsNullOrEmpty(strRedirectUrl))
            {
                switch (AppContextBase.Context.ParamSubSys)
                {
                    case SubSystem.Admin:
                        strRedirectUrl = user.AdminRoleId == 4 ? "/Admin/SysOrderMeal.aspx" : "/Admin/SaleList.aspx";
                        //if (AppContextBase.Context.User.AdminRoleId > 0)
                        if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["debug"]))
                            NavigationBLL.RefreshNavigation( );
                        ResBLL.LoadRes(true);
                        break;
                    case SubSystem.Member:
                        strRedirectUrl = "/Member/MyCompany.aspx";
                        if (AppContextBase.Context.User.MemberCity.HasValue)
                            Utilities.AddCookie(AreaBLL.CITY_COOKIE, AppContextBase.Context.User.MemberCity.ToString( ));
                        if (!UserHasArea(user))
                            strRedirectUrl = "/Member/MyArea.aspx";
                        break;
                    case SubSystem.Company:
                        var t = CompanyBLL.GetCompanyTypeById(user.CompanyId.Value);
                        if (t == CompanyType.AdCompany)
                            strRedirectUrl = "/Company/CompanyAdsClicked.aspx";
                        //else if (t == CompanyType.SpecialCompany)
                        //    strRedirectUrl = "/Company/CompanyItemUsed.aspx";
                        else
                            strRedirectUrl = "/Company/SaleAdd.aspx";
                        break;
                }
            }
            else if (Utilities.Compare(strRedirectUrl, "/Public/OrderMealPage.aspx"))
            {
                var uFavAddr = GetUserFavAddress(user.Id);
                if (uFavAddr.Count > 0)
                {
                    if (uFavAddr.Count == 1)
                        strRedirectUrl = "/Public/OrderMealPage.aspx?MansionId=" + uFavAddr[0].id.ToString( );
                    Utilities.AddCookie("addr", HttpUtility.UrlEncodeUnicode(uFavAddr[0].name));
                }
            }
            Utilities.Redirect(strRedirectUrl);
        }
        public static bool UserHasArea(SysMember user)
        {
            return !(string.IsNullOrEmpty(user.AreaDepth1) && string.IsNullOrEmpty(user.AreaDepth2) && string.IsNullOrEmpty(user.AreaDepth3));
        }
        public static int UserLogin(string txtUserName, string txtUserPass, SubSystem subSys, out SysMember loginUser)
        {
            loginUser = null;
            int id = 0;
            if (txtUserName.Length <= 9)
                int.TryParse(txtUserName, out id);
            SqlQuery query = null;
            if (id > 0)
                query = DB.Select( ).From<SysMember>( )
                                    .Where(SysMember.IdColumn).IsEqualTo(id);
            else
                query = DB.Select( ).From<SysMember>( )
                                .Where(SysMember.MemberPhoneNumberColumn).IsEqualTo(txtUserName);
            if (subSys == SubSystem.Company)
                query.And(SysMember.CompanyIdColumn).IsGreaterThan(0);
            SysMemberCollection users = query.ExecuteAsCollection<SysMemberCollection>( );
            if (users == null || users.Count == 0)
                return 1;
            bool bPass = false;
            SysMember user = null;
            foreach (SysMember item in users)
            {
                string enPass = Utilities.DESEncrypt(txtUserPass);
                bPass = Utilities.Compare(item.MemberPwd, txtUserPass) ||
                        Utilities.Compare(item.MemberPwd, enPass);
                if (bPass)
                {
                    if (Utilities.Compare(item.MemberPwd, txtUserPass))
                        item.MemberPwd = enPass;
                    user = item;
                    break;
                }
            }
            if (!bPass)
                return 2;
            if (user.MemberRoleId == null)
                user.MemberRoleId = 0;
            if (user.CompanyRoleId == null)
                user.CompanyRoleId = 0;
            if (user.AdminRoleId == null)
                user.AdminRoleId = 0;
            int roleId = 0;
            switch (subSys)
            {
                case SubSystem.Admin:
                    roleId = Convert.ToInt32(user.AdminRoleId);
                    break;
                case SubSystem.Member:
                    roleId = Convert.ToInt32(user.MemberRoleId);
                    break;
                case SubSystem.Company:
                    roleId = Convert.ToInt32(user.CompanyRoleId);
                    break;
            }

            if (GetUserRoleAssignmentById(roleId) == null)
            {
                return 3;
            }
            if (user.MemberStatus != 1)
            {
                return 4;
            }
            user.ValidateWhenSaving = false;
            user.LastLoginSubSys = (int)subSys;
            user.LastLoginDate = DateTime.Now;
            user.Save(user.Id);
            Utilities.LoginSigIn(user.Id, subSys);
            loginUser = user;
            return 0;
        }

        public static SysRoleDefine GetUserRoleAssignmentById(int roleId)
        {

            return SysRoleDefine.FetchByID(roleId);
        }

        public static void DeleteUser(SysMember user)
        {
            if (user == null)
                return;
        }

        public static bool CheckUserIsOnline(int userID)
        {
            SysMember user = DB.Select( ).From<SysMember>( )
                               .Where(SysMember.IdColumn).IsEqualTo(userID)
                               .ConstraintExpression("AND (GETDATE() - LastLoginDate < 1.0/24.0)")
                               .ExecuteSingle<SysMember>( );
            return user != null;
        }

        public static bool CheckAdminIsOnline( )
        {
            SysMember user = DB.Select( ).From<SysMember>( )
                               .Where(SysMember.AdminRoleIdColumn).IsGreaterThan(0)
                               .ConstraintExpression("AND (GETDATE() - LastLoginDate < 1.0/24.0)")
                               .ExecuteSingle<SysMember>( );
            return user != null;
        }
        public static bool CheckUserPwd(SysMember user, string strPwd)
        {
            string enPass = Utilities.DESEncrypt(strPwd);
            return user != null && !string.IsNullOrEmpty(strPwd) &&
                    (Utilities.Compare(enPass, user.MemberPwd) ||
                    Utilities.Compare(strPwd, user.MemberPwd));
        }

        public static bool CheckUserExist(string phoneNum)
        {
            int n = DB.Select("Count(*)").From<SysMember>( )
                      .Where(SysMember.MemberPhoneNumberColumn).IsEqualTo(phoneNum)
                      .ExecuteScalar<int>( );
            bool bExist = n > 0;
            if (!bExist)
            {
                n = DB.Select("Count(*)").From<SysCompany>( )
                      .Where(SysCompany.CompanyTelColumn).IsEqualTo(phoneNum)
                      .ExecuteScalar<int>( );
                bExist = n > 0;
            }
            return bExist;
        }
        public static bool CheckUserFinger(SysMember user, string strFinger)
        {
            return FingerMatch.Match2Fp(user.MemberFinger, strFinger);
        }
        public static SysMemberCash GetUserLatestCash(int userID, int companyID)
        {
            return DB.Select( ).Top("1").From<SysMemberCash>( )
                                               .Where(SysMemberCash.CashMemberIDColumn).IsEqualTo(userID)
                                               .And(SysMemberCash.CashCompanyIDColumn).IsEqualTo(companyID)
                                               .And(SysMemberCash.CashSumColumn).IsGreaterThanOrEqualTo(0)
                                               .OrderDesc(SysMemberCash.Columns.CashID)
                                               .ExecuteSingle<SysMemberCash>( );
        }
        public static decimal GetUserBalanceCash(int userID, int companyID)
        {
            return DB.Select("sum(CashSum)").From<SysMemberCash>( )
                                     .Where(SysMemberCash.CashMemberIDColumn).IsEqualTo(userID)
                                     .And(SysMemberCash.CashCompanyIDColumn).IsEqualTo(companyID)
                                     .ExecuteScalar<decimal>( );
        }
        public static SysMember GetUserByPhoneNum(string phoneNum)
        {
            return DB.Select( ).From<SysMember>( )
                     .Where(SysMember.MemberPhoneNumberColumn).IsEqualTo(phoneNum)
                     .ExecuteSingle<SysMember>( );
        }

        public static SysMember GetMember(int id, string phoneNum)
        {
            var query = DB.Select( ).From<SysMember>( );
            if (!string.IsNullOrEmpty(phoneNum))
                query = query.Where(SysMember.MemberPhoneNumberColumn).IsEqualTo(phoneNum);
            else
                query = query.Where(SysMember.IdColumn).IsEqualTo(id);
            return query.ExecuteSingle<SysMember>( );
        }

        //public static string MemberCash_info(decimal cashSum)
        //{
        //    //宋江  本店会员，有0元储值，4.40个积分
        //    //const string MEMBERCASH_INFO_2 = "<b>{0}</b> 是{1},{3}元储值可以消费,有{2}个积分,但无法在本店充值。";
        //    const string MEMBERCASH_INFO_1 = "<b>{0}</b> {1},有{3}元储值,{2}个积分";
        //    //if (AppContextBase.Context.Company.CompanyType == 4 && cashSum > 0)
        //    //    return MEMBERCASH_INFO_2;
        //    //else
        //    return MEMBERCASH_INFO_1;
        //}
        public static bool GetMemberForCash(SysMember user, out string message)
        {
            return GetMemberForOrder(user, out message);
            //message = string.Empty;
            //if (user == null)
            //{
            //    message = "用户账号不存在！";
            //    return false;
            //}
            //if (user.CompanyId > 0)
            //{
            //    message = "此账号属于商家账号,不允许充值!";
            //    return false;
            //}
            //CompanyType cmpType = Formatter.ToEnum<CompanyType>(AppContextBase.Context.Company.CompanyType);
            //if (cmpType != CompanyType.UnionCompany)
            //{
            //    message = "您暂无权限使用该功能";
            //    return false;
            //}
            ////合作方式为积分合作,无法使用储值功能
            ////if (AppContextBase.Context.Company.CompanyFlag == 1)
            ////{
            ////    message = "与乐多分的合作方式为积分合作,无法使用储值功能";
            ////    return false;
            ////}
            //var query = DB.Select("sum(CashSum)").From<SysMemberCash>( )
            //                                     .Where(SysMemberCash.CashMemberIDColumn).IsEqualTo(user.Id)
            //                                     .And(SysMemberCash.CashCompanyIDColumn).IsEqualTo(AppContextBase.Context.Company.Id);
            //decimal dCashSum = query.ExecuteScalar<decimal>( );
            //message = string.Format(MemberCash_info(dCashSum), user.MemberFullname,
            //                                        UserGradeInfo(user),
            //                                        user.MemberBalance, dCashSum);
            //user.MemberBalanceCash = dCashSum;
            //return true;

        }
        public static string UserGradeInfo(SysMember member)
        {
            string result = string.Empty;
            if (member == null)
                goto lbl_return;
            if (member.CompanyId > 0)
            {
                result = "商家账号";
                goto lbl_return;
            }
            result = GradeBLL.GetUserCurrentGrade(AppContextBase.Context.Company.Id, member.Id);
            if (string.IsNullOrEmpty(result) || result == "0")
            {

                result = (CompanyBLL.CheckIsOwnerUser(member.Id, AppContextBase.Context.Company.Id)) ? "本店会员" : "外来会员";
            }
        lbl_return:
            return result;
        }
        public static bool GetMemberForOrder(SysMember user, out string message)
        {
            message = string.Empty;
            if (user == null)
            {
                message = "用户账号不存在！";
                return false;
            }
            if (user.CompanyId > 0)
            {
                message = "此账号属于商家账号,不允许结算和储值!";
                return false;
            }
            if (AppContextBase.Context.CompanyType.Value != CompanyType.UnionCompany &&
                AppContextBase.Context.CompanyType.Value != CompanyType.SpecialCompany)
            {
                message = "您暂无权限使用该功能";
                return false;
            }
            user.MemberBalanceCash = GetUserBalanceCash(user.Id, AppContextBase.Context.Company.Id);
            string result = GradeBLL.GetUserCurrentGrade(AppContextBase.Context.Company.Id, user.Id);
            if (string.IsNullOrEmpty(result) || result == "0")
            {
                result = string.Format("属性：{0}  级别：普通会员", (user.MemberCompanyID == AppContextBase.Context.Company.Id) ? "本店会员" : "外来会员");
            }
            else
                result = string.Format("属性：本店会员  级别：{0}", result);
            message = result;
            return true;
        }
        public static DataTable GetUserInfoDataTable(SysMember user)
        {
            DataTable dtUserInfo = SubSonic.Utilities.EntityFormat.GetUserInfoTable( );
            dtUserInfo.Rows.Add(ResBLL.GetColumnRes(SysMember.MemberPhoneNumberColumn), user.MemberPhoneNumber);
            dtUserInfo.Rows.Add(ResBLL.GetColumnRes(SysMember.MemberFullnameColumn), user.MemberFullname);

            //SysCompany company = SysCompany.FetchByID(user.MemberCompanyID);
            //if (company != null)
            //    dtUserInfo.Rows.Add(AppRes.GetColumnRes(SysCompany.CompanyNameColumn),company.CompanyName);
            //if (user.MemberGrade > 0)
            //{
            //    SysCompanyMemberGrade grade = SysCompanyMemberGrade.FetchByID(user.MemberGrade);
            //    if (grade != null)
            //        dtUserInfo.Rows.Add(AppRes.GetColumnRes(SysCompanyMemberGrade.GradeNameColumn), grade.GradeName);
            //}
            dtUserInfo.Rows.Add(ResBLL.GetColumnRes(SysMember.MemberBalanceCashColumn), user.MemberBalanceCash);
            dtUserInfo.Rows.Add(ResBLL.GetColumnRes(SysMember.MemberBalanceColumn), user.MemberBalance);
            return dtUserInfo;
        }
        public static DataTable GetCompanyInfoDataTable(SysCompany company)
        {
            if (company == null)
                return null;
            DataTable dtUserInfo = SubSonic.Utilities.EntityFormat.GetUserInfoTable( );
            //TODO add userinfo
            dtUserInfo.Rows.Add(ResBLL.GetColumnRes(SysCompany.CompanyTelColumn)
                                , company.CompanyTel);
            dtUserInfo.Rows.Add(ResBLL.GetColumnRes(SysCompany.CompanyNameColumn)
                                , company.CompanyName);
            dtUserInfo.Rows.Add(ResBLL.GetColumnRes(SysCompany.CompanyCodeColumn)
                                , company.CompanyCode);
            //dtUserInfo.Rows.Add(AppRes.GetColumnRes(SysCompany.CompanyPhoneColumn)
            //                    , company.CompanyPhone);
            dtUserInfo.Rows.Add(ResBLL.GetColumnRes(SysCompany.CompanySaleSumColumn)
                                , GetCompanyMonthSaleSum(company.Id).ToString("##########0.00"));
            dtUserInfo.Rows.Add(ResBLL.GetColumnRes(SysCompany.CompanyBalanceCashColumn)
                                , company.CompanyBalanceCash);
            dtUserInfo.Rows.Add(ResBLL.GetColumnRes(SysCompany.CompanyBalanceColumn)
                                , company.CompanyBalance);
            //dtUserInfo.Rows.Add(AppRes.GetColumnRes(SysCompany.CompanyAddressColumn)
            //                    , company.CompanyAddress);
            return dtUserInfo;
        }
        public static decimal GetCompanyMonthSaleSum(int companyID)
        {
            var query = DB.Select("sum(OrderSumOk)").From<Order>( )
                          .Where(Order.OrderDateColumn).IsBetweenAnd(DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)), DateTime.Today.AddDays(1))
                          .And(Order.OrderSellerIDColumn).IsEqualTo(companyID);
            return query.ExecuteScalar<decimal>( );
        }
        public static int GetDefaultUseRole(int SubSysID)
        {
            var query = DB.Select("max(ID)").From<SysRoleDefine>( )
                          .Where(SysRoleDefine.IsDefaultColumn).IsEqualTo(true)
                          .And(SysRoleDefine.SubSysIdColumn).IsEqualTo(SubSysID);
            return query.ExecuteScalar<int>( );
        }
        private static SysMember _mainAccount;
        public static SysMember MainAccount
        {
            get
            {
                if (_mainAccount == null)
                {
                    string accountID = ResBLL.GetRes("MainAccountID", "0", "主账号ID");
                    _mainAccount = SysMember.FetchByID(accountID);
                    if (_mainAccount == null)
                        throw new Exception("主账号ID没有设置!");
                }
                return _mainAccount;
            }
        }
        private static SysCompany _mainCompanyAccount;
        public static SysCompany MainCompanyAccount
        {
            get
            {
                if (_mainCompanyAccount == null)
                {
                    _mainCompanyAccount = SysCompany.FetchByID(MainAccount.CompanyId);
                    if (_mainCompanyAccount == null)
                        throw new Exception("主账号ID的商家账号没有设置!");
                }
                return _mainCompanyAccount;
            }
        }


        internal static SysCompany MemberToCompany(SysMember member)
        {
            if (member != null && member.CompanyId > 0)
            {
                SysCompany company = AppContextBase.Context.Company != null &&
                                     member.CompanyId == AppContextBase.Context.Company.Id ?
                                     AppContextBase.Context.Company :
                                     SysCompany.FetchByID(member.CompanyId);
                if (company == null)
                    goto lbl_null;
                if (company.DirtyColumns.Count > 0)
                    company.MarkClean( );
                company.MarkOld( );
                company.CompanyAddress = member.MemberAddress1;
                company.CompanyCity = member.MemberCity;
                company.CompanyLocation = member.MemberLocation;
                company.CompanyArea = member.MemberArea;
                company.AreaDepth = member.AreaDepth1;
                company.CompanyEmail = member.MemberEmail;
                return company;
            }
        lbl_null:
            return null;
        }
        public static SysMember CompanyToMember(SysCompany company)
        {
            var query = DB.Select( ).From<SysMember>( )
              .Where(SysMember.CompanyIdColumn).IsEqualTo(company.Id);
            SysMember user = query.ExecuteSingle<SysMember>( );
            if (user != null)
            {
                if (user.DirtyColumns.Count > 0)
                    user.MarkClean( );
                user.MemberPhoneNumber = company.CompanyTel;
                user.MarkOld( );
                user.MemberEmail = company.CompanyEmail;
                user.MemberAddress1 = company.CompanyAddress;
                user.MemberCity = company.CompanyCity;
                user.MemberLocation = company.CompanyLocation;
                user.AreaDepth1 = company.AreaDepth;
                user.AreaDepth2 = null;
                user.AreaDepth3 = null;
                user.AreaModifyDate = null;
                user.MemberArea = company.CompanyArea;
                user.MemberStatus = company.CompanyStatus;
            }
            else
            {
                user = new SysMember( );
                user.MarkNew( );
                user.MemberAddress1 = company.CompanyAddress;
                user.MemberAddress2 = string.Empty;
                user.MemberBalance = 0;
                user.MemberBalanceCash = 0;
                user.MemberBirthday = null;
                user.MemberCity = company.CompanyCity;
                user.MemberCompanyID = company.Id;
                user.MemberCountry = null;
                user.MemberDate = company.CompanyDate;
                user.MemberEmail = company.CompanyEmail;
                user.MemberFinger = null;
                user.MemberFullname = company.CompanyName;
                user.MemberGrade = 0;
                user.MemberMemo = null;
                user.MemberPhoneNumber = company.CompanyTel;
                user.MemberPid = 0;
                user.MemberPwd = company.CompanyPwd;
                user.MemberRoleId = 0;
                user.MemberSex = null;
                user.MemberState = null;
                user.MemberStatus = company.CompanyStatus;
                user.MemberSum = 0;
                user.MemberZip = null;
                user.LastLoginDate = null;
                user.LastLoginSubSys = 0;
                user.AdminRoleId = 0;
                user.CompanyId = company.Id;
                user.MemberLocation = company.CompanyLocation;
                user.AreaDepth1 = company.AreaDepth;
                user.AreaDepth2 = null;
                user.AreaDepth3 = null;
                user.AreaModifyDate = null;
                user.MemberArea = company.CompanyArea;
                user.CompanyRoleId = UserBLL.GetDefaultUseRole((int)SubSystem.Company);
            }
            return user;
        }

        public static decimal GetUserLastMonthOrderSum(int userID)
        {
            var query = DB.Select("SUM(OrderSum)").From<Order>( )
                         .Where(Order.OrderMemberIDColumn).IsEqualTo(userID)
                         .And(Order.OrderStatusColumn).In(1, 6)
                         .And(Order.OrderDateColumn).IsBetweenAnd(Formatter.GetMonthFirstDate(-1), Formatter.GetMonthFirstDate(0));
            return query.ExecuteScalar<decimal>( );
        }
        public static decimal GetUserAvgOrderSum(int userId)
        {
            return DB.Select("SUM(OrderSumOK) / Count(*)").From<Order>()
                          .Where(Order.OrderMemberIDColumn).IsEqualTo(userId)
                          .And(Order.OrderStatusColumn).In(1, 6)
                          .ExecuteScalar<decimal>( );
        }
        public static bool CheckUserIsOrderInCompany(int userID, int companyID)
        {
            var query = DB.Select( ).From<VPayment>( )
                          .Where(VPayment.PaymentMemberIDColumn).IsEqualTo(userID)
                          .And(VPayment.PaymentCompanyIDColumn).IsEqualTo(companyID);
            return query.GetRecordCount( ) > 0;
        }
        public static NameIDResult? GetUserDefFavAddress(int userId,int mansionId)
        {
            var favAddr = UserBLL.GetUserFavAddress(userId);
            NameIDResult? result = null;
            favAddr.ForEach<NameIDResult>(address =>
            {
                if (address.id == mansionId)
                {
                    result = address;
                    return false;
                }
                return true;
            });
            return result;
        }
        public static List<NameIDResult> GetUserFavAddress(int userId)
        {
            SysMemberConfig config = GetUserConfig(userId);
            if (string.IsNullOrEmpty(config.MyAddress))
                return new List<NameIDResult>( );
            else
                return Utilities.JSONToObj<List<NameIDResult>>(config.MyAddress);
        }
        public static bool ModifyUserFavAddress(int userId, int mansionId, string oldAddress, string newAddress)
        {
            var oldAddr = NameIDResult.GetNameIDResult(mansionId, oldAddress);
            var newAddr = NameIDResult.GetNameIDResult(mansionId, newAddress);
            if (oldAddr.Equals(newAddr))
                return true;
            var favAddress = GetUserFavAddress(userId);
            if (favAddress.Contains(newAddr))
                favAddress.Remove(newAddr);
            int index = favAddress.IndexOf(oldAddr);
            if (index >= 0)
            {
                favAddress.Insert(index, newAddr);
                favAddress.Remove(oldAddr);
            }
            SysMemberConfig config = GetUserConfig(userId);
            config.MyAddress = Utilities.ObjToJSON(favAddress);
            config.Save( );
            return true;
        }
        public static bool AddUserFavAddress(int userId, int mansionId, string address)
        {
            var favAddress = GetUserFavAddress(userId);
            var addr = NameIDResult.GetNameIDResult(mansionId, address);
            if (favAddress.Count <= 2 && !favAddress.Contains(addr))
            {
                favAddress.Insert(0, addr);
                SysMemberConfig config = GetUserConfig(userId);
                config.MyAddress = Utilities.ObjToJSON(favAddress);
                config.Save( );
                return true;
            }
            else if (!favAddress.Contains(addr))
            {
                favAddress.RemoveAt(favAddress.Count - 1);
                favAddress.Insert(0, addr);
                SysMemberConfig config = GetUserConfig(userId);
                config.MyAddress = Utilities.ObjToJSON(favAddress);
                config.Save( );
                return true;
            }
            return false;
        }
        public static void RemoveUserFavAddress(int userId, int mansionId, string address)
        {
            var favAddress = GetUserFavAddress(userId);
            var addr = NameIDResult.GetNameIDResult(mansionId, address);
            if (favAddress.Contains(addr))
            {
                favAddress.Remove(addr);
                SysMemberConfig config = GetUserConfig(userId);
                config.MyAddress = Utilities.ObjToJSON(favAddress);
                config.Save( );
            }
        }

        public static List<int> GetUserFavCompany(int userId)
        {
            SysMemberConfig config = GetUserConfig(userId);
            if (string.IsNullOrEmpty(config.MyFavCompany))
                return new List<int>( );
            else
                return Utilities.JSONToObj<List<int>>(config.MyFavCompany);
        }
        public static int AddUserFavCompany(int userId, int companyId)
        {
            List<int> favCompanys = GetUserFavCompany(userId);
            //if (favCompanys.Count > 5)
            //    return -1;
            if (!favCompanys.Contains(companyId))
            {
                favCompanys.Add(companyId);
                SysMemberConfig config = GetUserConfig(userId);
                config.MyFavCompany = Utilities.ObjToJSON(favCompanys);
                config.Save( );
                return 0;
            }
            return 1;
        }
        public static void RemoveUserFavCompany(int userId, int companyId)
        {
            List<int> favCompanys = GetUserFavCompany(userId);
            if (favCompanys.Contains(companyId))
            {
                favCompanys.Remove(companyId);
                SysMemberConfig config = GetUserConfig(userId);
                config.MyFavCompany = Utilities.ObjToJSON(favCompanys);
                config.Save( );
            }
        }
        public static SysMemberConfig GetUserConfig(int userID)
        {
            SysMemberConfig config = SysMemberConfig.FetchByID(userID);
            if (config == null)
            {
                config = new SysMemberConfig( );
                config.AreaModifyCount = 0;
                config.AreaModifyDate = null;
                config.MyFavCompany = null;
                config.MemberId = userID;
                config.Save( );
            }
            return config;
        }
        public static SysMember GetOrNewMemberByPhoneNumber(string phoneNumber, SysCompany company, out bool isNew)
        {
            SysMember user = GetUserByPhoneNum(phoneNumber);
            isNew = user == null;
            if (isNew)
            {
                user = new SysMember( )
                {
                    MemberPhoneNumber = phoneNumber,
                    MemberPwd = phoneNumber.Substring(phoneNumber.Length - 6, 6),
                    MemberAddress1 = null,
                    MemberAddress2 = null,
                    MemberBalance = 0,
                    MemberBalanceCash = 0,
                    MemberBirthday = null,
                    MemberCity = 0,
                    MemberCompanyID = MainAccount.CompanyId,
                    MemberCountry = "中国",
                    MemberDate = DateTime.Now,
                    MemberEmail = null,
                    MemberFinger = null,
                    MemberFullname = null,
                    MemberGrade = 0,
                    AreaDepth1 = null,
                    MemberArea = company.CompanyArea,
                    MemberLocation = company.CompanyLocation,
                    MemberMemo = null,
                    MemberPid = -1,
                    MemberRoleId = 2,
                    AdminRoleId = 0,
                    CompanyId = 0,
                    CompanyRoleId = 0,
                    MemberSex = null,
                    MemberState = null,
                    MemberStatus = 1,
                    MemberSum = 0,
                    MemberZip = null,
                    AreaDepth2 = null,
                    AreaDepth3 = null,
                    AreaModifyDate = null,
                    LastLoginDate = null,
                    LastLoginSubSys = 0,

                };
                user.Save( );
            }
            else if (!Utilities.Compare(user.MemberMsnPhone, phoneNumber))
            {
                user.MemberMsnPhone = phoneNumber;
                user.Save( );
            }
            return user;
        }
        public static string GetUserPhoneById(int id)
        {
            var sql = "Select MemberPhoneNumber from Sys_Member where id=" + id.ToString( );
            return Utilities.ToString(DataService.ExecuteScalar(new QueryCommand(sql)));
        }
    }
}