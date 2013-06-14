using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Eleooo.DAL;
using System.Xml;
using System.Data;
using System.Transactions;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.WebRestServices
{
    /// <summary>
    /// WebRestService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class WebRestService : System.Web.Services.WebService
    {
        [WebMethod]
        public int ClientLogin(string userName, string userPass, int subSys, out DataTable loginUser)
        {
            loginUser = null;
            SysMember user;
            int state = UserBLL.UserLogin(userName, userPass, (SubSystem)subSys, out user);
            if (state == 0 && user != null)
            {
                loginUser = SubSonic.Utilities.EntityFormat.EntityToTable<SysMember>(user);
            }
            return state;
        }

        [WebMethod]
        public void GetMemberInfo(int id, string phoneNum)
        {
            var query = DB.Select( ).From<SysMember>( );
            if (id > 0)
                query = query.Where(SysMember.IdColumn).IsEqualTo(id);
            else if (!string.IsNullOrEmpty(phoneNum))
                query = query.Where(SysMember.MemberPhoneNumberColumn).IsEqualTo(phoneNum);
            else
            {
                Utilities.PrintRestError("参数错误!");
                return;
            }
            string result = query.ExecuteXML("Sys_Members", "Sys_Member").Replace("<NewDataSet />", string.Empty);
            Utilities.PrintRestResult(result);
        }

        [WebMethod]
        public string GetMemberFinger(int id, string phoneNum)
        {
            var query = DB.Select( ).From<SysMember>( );
            if (id > 0)
                query = query.Where(SysMember.IdColumn).IsEqualTo(id);
            else if (!string.IsNullOrEmpty(phoneNum))
                query = query.Where(SysMember.MemberPhoneNumberColumn).IsEqualTo(phoneNum);
            else
                return "-1";
            SysMember member = query.ExecuteSingle<SysMember>( );
            if (member == null)
                return "-1";
            else
                return member.MemberFinger;
        }

        [WebMethod]
        public bool GetMemberForOrder(int id, string phoneNum, out DataTable dtUser, out DataTable dtUserInfo, out DataTable dtUserCash, out string message)
        {
            dtUser = null;
            dtUserInfo = null;
            dtUserCash = null;
            message = string.Empty;
            SysMember user = UserBLL.GetMember(id, phoneNum);
            if (user == null)
            {
                message = "用户不存在!";
                return false;
            }
            bool bRet = UserBLL.GetMemberForOrder(user, out message);
            if (bRet)
            {
                user.MarkClean( );
                SysMemberCash cash = UserBLL.GetUserLatestCash(user.Id, AppContext.Context.Company.Id);
                decimal dBalance = Utilities.ToDecimal(user.MemberBalance);
                if (AppContext.Context.CompanyType.HasValue &&
                        AppContext.Context.CompanyType.Value != CompanyType.UnionCompany)
                    user.MemberBalance = 0M;
                user.MarkClean( );
                dtUser = SubSonic.Utilities.EntityFormat.EntityToTable<SysMember>(user);
                user.MemberBalance = dBalance;
                dtUserCash = SubSonic.Utilities.EntityFormat.EntityToTable<SysMemberCash>(cash);
                dtUserInfo = UserBLL.GetUserInfoDataTable(user);
            }
            return bRet;
        }

        [WebMethod]
        public bool GetMemberForCash(int id, string phoneNum, out DataTable dtUser, out DataTable dtUserInfo, out string message)
        {
            dtUser = null;
            dtUserInfo = null;
            message = string.Empty;
            SysMember user = UserBLL.GetMember(id, phoneNum);
            if (user == null)
            {
                message = "用户不存在!";
                return false;
            }
            bool bRet = UserBLL.GetMemberForCash(user, out message);
            if (bRet)
            {
                user.MarkClean( );
                dtUser = SubSonic.Utilities.EntityFormat.EntityToTable<SysMember>(user);
                dtUserInfo = UserBLL.GetUserInfoDataTable(user);
            }
            return bRet;
        }

        [WebMethod]
        public byte[] GetDataTable(string cmdSql)
        {
            return GetDataTableByParam(cmdSql, null);
        }
        [WebMethod]
        public byte[] GetDataTableByParam(string cmdSql, DataTable cmdParam)
        {
            if (string.IsNullOrEmpty(cmdSql))
                return null;
            SubSonic.QueryCommand cmd = SubSonic.Utilities.EntityFormat.SqlToCmd(cmdSql, cmdParam);
            DataTable dt = SubSonic.DataService.GetDataTable(cmd);
            return SubSonic.Utilities.DataFormat.GetBinFormatDataZip(dt);
        }
        [WebMethod]
        public int ExecuteQuery(string cmdSql, DataTable cmdParam)
        {
            if (string.IsNullOrEmpty(cmdSql))
                return 0;
            SubSonic.QueryCommand cmd = SubSonic.Utilities.EntityFormat.SqlToCmd(cmdSql, cmdParam);
            return SubSonic.DataService.ExecuteQuery(cmd);
        }
        [WebMethod]
        public object ExecuteScalar(string cmdSql, DataTable cmdParam)
        {
            if (string.IsNullOrEmpty(cmdSql))
                return null;
            SubSonic.QueryCommand cmd = SubSonic.Utilities.EntityFormat.SqlToCmd(cmdSql, cmdParam);
            return SubSonic.DataService.ExecuteScalar(cmd);
        }
        [WebMethod]
        public bool SaveCompanyAdForClient(DataTable dtCompanyAd, DataTable dtPointSetting, byte[] fileData, string fileName, out int adsID, out string message)
        {
            message = string.Empty;
            adsID = 0;
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                SysCompanyAd ad = SubSonic.Utilities.EntityFormat.TableToEntity<SysCompanyAd>(dtCompanyAd);
                //SysCompanyAdsPointSetting point = SubSonic.Utilities.EntityFormat.TableToEntity<SysCompanyAdsPointSetting>(dtPointSetting);
                //SysCompanyAdsClickSetting click = SubSonic.Utilities.EntityFormat.TableToEntity<SysCompanyAdsClickSetting>(dtClickSetting);
                if (ad == null || dtPointSetting == null)
                {
                    message = "参数异常";
                    return false;
                }
                if (dtPointSetting.Rows.Count == 0)
                {
                    message = "奖励设置的行数为零";
                    return false;
                }
                ad.ValidateWhenSaving = false;
                ad.AdsDate = DateTime.Now;
                ad.AdsPic = null;
                ad.MarkNew( );
                ad.Save( );
                adsID = ad.AdsID;
                if (fileData != null && fileData.Length > 0 && !string.IsNullOrEmpty(fileName))
                {
                    string phyPath;
                    ad.AdsPic = FileUpload.SaveUploadFile(fileData, FileType.Image, SaveType.CompanyAds, fileName, out phyPath, out message, true, adsID.ToString( ));
                    ad.Save( );
                }
                foreach (DataRow row in dtPointSetting.Rows)
                {
                    SysCompanyAdsPointSetting p = new SysCompanyAdsPointSetting( );
                    p.AdsID = ad.AdsID;
                    p.OrderSumLimit = Utilities.ToDecimal(row[0]);
                    p.AdsPoint = Utilities.ToDecimal(row[1]);
                    p.ValidateWhenSaving = false;
                    p.Save( );
                }
                ts.Complete( );
                message = "保存成功";
                return true;
            }
            catch (Exception ex)
            {
                Logging.Log("WebRestService->SaveCompanyAdForClient", ex, true);
                message = ex.Message;
                return false;
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
        }
        [WebMethod]
        public int SaveOrderForClient(DataTable dtOrder, int userID, string userPwd, out string message)
        {
            message = string.Empty;
            if (dtOrder == null || userID < 1)
            {
                message = "结算数据包错误,请重试!";
                return -1;
            }
            if (AppContext.Context.CompanyType.Value != CompanyType.UnionCompany &&
                AppContext.Context.CompanyType.Value != CompanyType.SpecialCompany)
            {
                message = "您暂无权限使用该功能";
                return -1;
            }
            SysMember orderUser = SysMember.FetchByID(userID);
            if (orderUser == null)
            {
                message = "结算会员不存在!";
                return -1;
            }
            Order orderData = SubSonic.Utilities.EntityFormat.TableToEntity<Order>(dtOrder);
            if (orderData.OrderPayCash > 0 || orderData.OrderPayPoint > 0)
            {
                if (!UserBLL.CheckUserPwd(orderUser, userPwd) && !UserBLL.CheckUserFinger(orderUser, userPwd))
                {
                    message = "会员密码或者指纹错误!";
                    return 2;
                }
            }
            //orderData.OrderPoint = orderData.OrderSumOk * orderData.OrderRate;
            if (orderData.OrderRateSale.HasValue)
                orderData.OrderRateSale = orderData.OrderRateSale.Value / 100M;
            if (AppContext.Context.CompanyType.Value != CompanyType.UnionCompany)
                orderData.OrderRate = 0;
            orderData.MarkNew( );
            orderData.IsNew = true;
            orderData.IsLoaded = false;
            orderData.OrderCode = OrderBLL.GetOrderCode(AppContext.Context.Company);
            orderData.OrderCard = string.Empty;
            orderData.OrderDate = DateTime.Now;
            orderData.OrderDateDeliver = DateTime.Now;
            orderData.OrderDateUpload = DateTime.Now;
            orderData.OrderMemberID = orderUser.Id;
            orderData.OrderSellerID = AppContext.Context.Company.Id;
            orderData.OrderStatus = 1;
            orderData.OrderType = 1;
            orderData.OrderQty = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope( ))
                {
                    using (SubSonic.SharedDbConnectionScope ss = new SubSonic.SharedDbConnectionScope( ))
                    {
                        orderData.ValidateWhenSaving = false;
                        //orderData.Save( );
                        if (!OrderBLL.SaveSaleRate(orderData, orderUser, out message))
                            return 1;
                        OrderBLL.UpdateBalance( );
                        ts.Complete( );
                        message = "结算成功!";
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log("WebRestService->SaveOrderForClient", ex, true);
                message = ex.Message;
                return 1;
            }
        }

        [WebMethod]
        public int SaveCashForClient(DataTable dtCash, int userID, out string message)
        {
            message = string.Empty;
            if (dtCash == null || userID < 1 || dtCash.Rows.Count == 0)
            {
                message = "储值数据包错误,请重试!";
                return -1;
            }
            SysMember user = SysMember.FetchByID(userID);
            if (user == null)
            {
                message = "储值会员不存在!";
                return -1;
            }
            SysMemberCash cashData = SubSonic.Utilities.EntityFormat.TableToEntity<SysMemberCash>(dtCash);
            if (cashData.CashRate.HasValue && cashData.CashRate.Value >= 0)
                cashData.CashRate = cashData.CashRate.Value / 100M;
            else
                cashData.CashRate = 0;
            if (AppContext.Context.CompanyType.Value != CompanyType.UnionCompany)
            {
                cashData.CashPoint = 0;
                cashData.CashRateSale = null;
            }
            cashData.MarkNew( );
            cashData.CashDate = DateTime.Now;
            cashData.CashCompanyID = AppContext.Context.Company.Id;
            cashData.CashMemberID = userID;
            cashData.CashOrderID = 0;
            cashData.ValidateWhenSaving = false;
            if (cashData.CashPoint == null)
                cashData.CashPoint = 0;
            if (cashData.CashRate == null)
                cashData.CashRate = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope( ))
                {
                    using (SubSonic.SharedDbConnectionScope ss = new SubSonic.SharedDbConnectionScope( ))
                    {
                        //int gradeID = string.IsNullOrEmpty(cashData.CashMemo) ? 0 : Convert.ToInt32(cashData.CashMemo);
                        //SysCompanyMemberGrade grade = SysCompanyMemberGrade.FetchByID(gradeID);
                        //if (grade != null)
                        //{
                        //    user.MemberGrade = grade.Id;
                        //    cashData.CashMemo = grade.GradeName;
                        //    user.Save( );
                        //}
                        //cashData.Save( );
                        if (!OrderBLL.SaveMemberCash(cashData, user, out message))
                            return 1;
                        OrderBLL.UpdateBalance( );
                        ts.Complete( );

                        message = string.Format("{0}储值{1}元成功，现储值余额为{2}元", user.MemberFullname, cashData.CashSum, UserBLL.GetUserBalanceCash(userID, AppContext.Context.Company.Id));
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log("WebRestService->SaveCashForClient", ex, true);
                message = ex.Message;
                return 1;
            }
        }

        [WebMethod]
        public DataTable GetCompanyInfo(int companyID)
        {
            SysCompany company = SysCompany.FetchByID(companyID);
            return UserBLL.GetCompanyInfoDataTable(company);
        }

        [WebMethod]
        public string UploadFile(byte[] fileData, string fileName, int fileType, int saveType, string folderName, out string message)
        {
            string phyPath;
            return FileUpload.SaveUploadFile(fileData, (FileType)fileType, (SaveType)saveType, fileName, out phyPath, out message, true, folderName);
        }

        [WebMethod]
        public byte[] GetSaveFile(string fileName, int saveType)
        {
            return FileUpload.GetSaveFile(fileName, (SaveType)saveType);
        }

        [WebMethod]
        public string GetGenCompanyCode(int areaID)
        {
            return AreaBLL.GenCompanyCodeByID(areaID);
        }
        [WebMethod]
        public bool IsOwnerUser(int userID, int companyID)
        {
            return CompanyBLL.CheckIsOwnerUser(userID, companyID);
        }
        [WebMethod]
        public byte[] GetUserCompanyItems(string phoneNum, string userPwd, string userFinger, int companyID, out int flag, out string message)
        {
            var query = CompanyItemBLL.GetOrderCompanyItemQuery(phoneNum, userPwd, userFinger, companyID, out flag, out message);
            DataTable dt = query.ExecuteDataTable( );
            return SubSonic.Utilities.DataFormat.GetBinFormatDataZip(dt);
        }
        [WebMethod]
        public bool OrderCompanyItem(int companyID, int itemID, string MemberPwd, string MemberFinger, out string message)
        {
            SysCompany company = SysCompany.FetchByID(companyID);
            if (company == null)
            {
                message = "商家不存在";
                return false;
            }
            return CompanyItemBLL.OrderCompanyItem(company, itemID, MemberPwd, MemberFinger, out message);
        }
    }
}
