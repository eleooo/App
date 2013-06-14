using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;
using SubSonic;
using System.Transactions;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class CompanyAdsBLL
    {
        public static readonly Dictionary<int, string> AdsCheckResult;
        static CompanyAdsBLL( )
        {
            // -- -1广告已经过期 -2性别不符合 -3你的圈子不在广告投放的区域 -4你已经浏览过此广告 
            //-- -5此广告仅限外来会员浏览 -6此广告仅限本店会员浏览 
            //--- -7此广告已达日最高浏览量 -8此广告已达日最高投放额 -9 你已经达到最每天可浏览的广告量
            AdsCheckResult = new Dictionary<int, string>( );
            AdsCheckResult.Add(1, "成功");
            AdsCheckResult.Add(-1, "广告已经过期.");
            AdsCheckResult.Add(-2, "此广告仅限男性会员浏览！");
            AdsCheckResult.Add(-3, "此广告仅限女性会员浏览！");
            AdsCheckResult.Add(-4, "您未符合此广告的投放区域");
            AdsCheckResult.Add(-5, "您已经浏览过此广告!");
            AdsCheckResult.Add(-6, "此广告仅限外来会员浏览");
            AdsCheckResult.Add(-7, "此广告仅限本店会员浏览");
            AdsCheckResult.Add(-8, "此广告已达日最高浏览量");
            AdsCheckResult.Add(-9, "此广告已达日最高投放额");
            AdsCheckResult.Add(-10, "根据您上月消费额<br/>暂无权限浏览该广告^_^");
            AdsCheckResult.Add(-11, "您已浏览过该商家的广告哦");
            AdsCheckResult.Add(-12, "您尚未消费过<br/>只能看一条广告^_^");
            AdsCheckResult.Add(-13, "根据您的消费额<br/>每天只能看2条广告^_^");
            AdsCheckResult.Add(-14, "根据您上月消费额<br/>每天只能看{0}条广告^_^");
        }
        #region constanct
        public const string IMGE_TPL = "<img alt='' src='{0}' />";
        public const string TEMPLATE = @"
<div class='picContentInfo'>
{0}
<div class='content'>
{1}
</div>
</div>";
        const string CHECK_COMPANY_ADS_FUNC = "dbo.CheckCompanyAdCanClick({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})";
        const string GET_COMPANY_AD_POINT_FUNC = "dbo.[GetAdsMaxPoint]({0},{1}) as {2}";
        const string CHECK_ADS_CLICK_SETTINGS = "SELECT dbo.CheckUserAdsClickSettings({0},{1})";
        const string CHECK_ADS_DAY_LIMIT = "dbo.CheckCompanyAdsDayLimit({0},{1},{2})";
        #endregion

        private static Dictionary<string, string> _sexLimit;
        public static Dictionary<string, string> SexLimit
        {
            get
            {
                if (_sexLimit == null)
                {
                    _sexLimit = new Dictionary<string, string>( );
                    _sexLimit.Add("0", "全部");
                    _sexLimit.Add("1", "男");
                    _sexLimit.Add("2", "女");
                }
                return _sexLimit;
            }
        }
        private static Dictionary<string, string> _adsLimit;
        public static Dictionary<string, string> AdsLimit
        {
            get
            {
                if (_adsLimit == null)
                {
                    _adsLimit = new Dictionary<string, string>( );
                    _adsLimit.Add("1", "限一次");
                    _adsLimit.Add("2", "本期限一次");
                }
                return _adsLimit;
            }
        }
        private static Dictionary<string, string> _memberLimit;
        public static Dictionary<string, string> MemberLimit
        {
            get
            {
                if (_memberLimit == null)
                {
                    _memberLimit = new Dictionary<string, string>( );
                    _memberLimit.Add("0", "所有会员");
                    _memberLimit.Add("1", "本店会员");
                    _memberLimit.Add("2", "外来会员");
                }
                return _memberLimit;
            }
        }
        private static Dictionary<string, string> _questionAnswers;
        public static Dictionary<string, string> QuestionAnswers
        {
            get
            {
                if (_questionAnswers == null)
                {
                    _questionAnswers = new Dictionary<string, string>( );
                    _questionAnswers.Add("1", "A");
                    _questionAnswers.Add("2", "B");
                    _questionAnswers.Add("3", "C");
                    _questionAnswers.Add("4", "D");
                }
                return _questionAnswers;
            }
        }

        private static List<string> _adsLimitCol;
        private static List<string> AdsLimitCol
        {
            get
            {
                if (_adsLimitCol == null)
                {
                    //@UserID INT, --会员ID
                    //@LastOrderSum DECIMAL(18,2), --上月消费金额
                    //@UserSex BIT, --会员性别
                    //@CompanyID INT, --广告提供商
                    //@AdsID INT, --广告ID
                    //@AreaDepth NVARCHAR(500), --投放区域
                    //@SexLimit INT, --性别限制
                    //@MemberLimit INT, --浏览对象 0 所有会员 1 本店会员 2外来会员
                    //@AdsLimit INT, --浏览频率 1 表示限抢一次,2 本期限一次
                    //@DayLimitAmount INT,--日最高浏览量
                    //@DayLimitSum DECIMAL(18,2), --日最高投放额
                    //@AdsEndDate DATETIME --有效期限
                    _adsLimitCol = new List<string>
                    {
                        Utilities.GetTableColumn(SysCompanyAd.AdsCompanyIDColumn),
                        Utilities.GetTableColumn(SysCompanyAd.AdsIDColumn),
                        Utilities.GetTableColumn(SysCompanyAd.AreaDepthColumn),
                        Utilities.GetTableColumn(SysCompanyAd.SexLimitColumn),
                        Utilities.GetTableColumn(SysCompanyAd.AdsMemberLimitColumn),
                        Utilities.GetTableColumn(SysCompanyAd.AdsClickLimitColumn),
                        Utilities.GetTableColumn(SysCompanyAd.AdsDayLimitAmountColumn),
                        Utilities.GetTableColumn(SysCompanyAd.AdsDayLimitSumColumn),
                        Utilities.GetTableColumn(SysCompanyAd.AdsEndDateColumn)
                    };
                }
                return _adsLimitCol;
            }
        }
        private static List<string> GetParamList(int userID, bool? sex, decimal lastOrderSum)
        {
            List<string> lstParam = new List<string>
            {
                userID.ToString(),
                lastOrderSum.ToString(),
                ConvertMemberSex(sex).ToString()
            };
            lstParam.AddRange(AdsLimitCol);
            return lstParam;
        }
        public static string RenderCheckAdsDayLimit( )
        {
            return RenderCheckAdsDayLimit(SysCompanyAd.AdsIDColumn, SysCompanyAd.AdsDayLimitAmountColumn, SysCompanyAd.AdsDayLimitSumColumn);
        }
        public static string RenderCheckAdsDayLimit(SubSonic.TableSchema.TableColumn adIdCol, SubSonic.TableSchema.TableColumn amountLimitCol, SubSonic.TableSchema.TableColumn sumLimitCol)
        {
            return string.Format(CHECK_ADS_DAY_LIMIT, Utilities.GetTableColumn(adIdCol), Utilities.GetTableColumn(amountLimitCol), Utilities.GetTableColumn(sumLimitCol));
        }
        public static string RenderCheckAdsFunc(int userID, bool? sex, decimal lastOrderSum, int eqVal = 1)
        {
            List<string> lstParam = GetParamList(userID, sex, lastOrderSum);
            return string.Format(" AND ({0} = {1}) ", string.Format(CHECK_COMPANY_ADS_FUNC, lstParam.ToArray( )), eqVal);
        }
        public static string RenderGetAdPointFunc( )
        {
            return RenderGetAdPointFunc(-1, SysCompanyAd.AdsIDColumn);
        }
        public static string RenderGetAdPointFunc(decimal lastOrderSum, SubSonic.TableSchema.TableColumn adIdCol)
        {
            return string.Format(GET_COMPANY_AD_POINT_FUNC, Utilities.GetTableColumn(adIdCol), lastOrderSum, SysCompanyAdsPointSetting.AdsPointColumn.ColumnName);
        }
        public static string GetMaxOrderSumOrPoint(string sumAndPoint, bool isGetPoint)
        {
            if (string.IsNullOrEmpty(sumAndPoint))
                return "0";
            if (isGetPoint)
                return sumAndPoint.Substring(sumAndPoint.IndexOf(",")).Trim(',', '-');
            else
                return sumAndPoint.Substring(0, sumAndPoint.IndexOf(",")).Trim(',', '-');
        }
        public static decimal GetCompanyAdPoint(decimal lastOrderSum, int adsID)
        {
            var sql = string.Concat("SELECT ", string.Format(GET_COMPANY_AD_POINT_FUNC, adsID, lastOrderSum, "result"));
            QueryCommand cmd = new QueryCommand(sql, DB.Provider.Name);
            if (!string.IsNullOrEmpty(AppContextBase.Page.Request.Params["debug"]))
                AppContextBase.Page.Response.Output.WriteLine(cmd.CommandSql);
            return Utilities.ToDecimal(DataService.ExecuteScalar(cmd));
        }
        public static int ExecuteCheckFunc(int userID, bool? sex, int adsID, decimal lastOrderSum)
        {
            List<string> lstParam = GetParamList(userID, sex, lastOrderSum);
            var sql = "SELECT {0} FROM dbo.Sys_Company_Ads WHERE [AdsID] = {1}";
            QueryCommand cmd = new QueryCommand(string.Format(sql, string.Format(CHECK_COMPANY_ADS_FUNC, lstParam.ToArray( )), adsID), DB.Provider.Name);
            if (!string.IsNullOrEmpty(AppContextBase.Page.Request.Params["debug"]))
                AppContextBase.Page.Response.Output.WriteLine(cmd.CommandSql);
            return Utilities.ToInt(DataService.ExecuteScalar(cmd));
        }
        public static int CheckUserAdsClickSettings(int userID, decimal lastOrderSum)
        {
            QueryCommand cmd = new QueryCommand(string.Format(CHECK_ADS_CLICK_SETTINGS, userID, lastOrderSum));
            return Utilities.ToInt(DataService.ExecuteScalar(cmd));
        }
        public static bool CheckUserOnceClickLimitAds(int userID, int companyID)
        {
            //检测是否有浏览过仅限一次的限制
            string vSql = @"SELECT top 1 1 from dbo.Sys_Member_Ads as t1
                            INNER JOIN dbo.Sys_Company_Ads as t2
                            ON t1.CompanyAdsID = t2.AdsID AND t2.AdsClickLimit = 1
                            WHERE t1.AdsMemberID = {0} AND t2.AdsCompanyID = {1}";
            QueryCommand cmd = new QueryCommand(string.Format(vSql, userID, companyID));
            return Utilities.ToInt(DataService.ExecuteScalar(cmd)) == 1;
        }

        public static int ConvertMemberSex(bool? sex)
        {
            if (sex.HasValue && sex.Value)
                return 1;
            else
                return 0;
        }

        public static int? GetCompanyLastAdsID(int userID, bool? sex, int companyID)
        {
            string vSql = @"Select Top 1 AdsID from Sys_Company_Ads
                                Where AdsCompanyID = {0} {1} And IsDeleted=0 And IsPass=1 Order By AdsID DESC";
            QueryCommand cmd = new QueryCommand(string.Format(vSql, companyID, RenderCheckAdsFunc(userID, sex, 0, 1)));
            int v = Utilities.ToInt(DataService.ExecuteScalar(cmd));
            if (v > 0)
                return v;
            else
                return null;
        }
        public static bool CheckCompanyAdsCanClick(SysMember user, SysCompanyAd item, SysCompany company, decimal userLastOrderSum, out string message)
        {
            if (item == null || company == null || user == null)
            {
                message = "参数错误!";
                goto lbl_end;
            }
            if (item.IsDeleted.HasValue && item.IsDeleted.Value)
            {
                message = "此广告已经无效";
                goto lbl_end;
            }
            //common double check
            int nCode = CompanyAdsBLL.ExecuteCheckFunc(user.Id, user.MemberSex, item.AdsID, userLastOrderSum);
            if (nCode < 0)
            {
                message = AdsCheckResult.ContainsKey(nCode) ? AdsCheckResult[nCode] : "你无权浏览此广告!";
                goto lbl_end;
            }
            //检测是否达到每日最大浏览量限制
            nCode = CheckUserAdsClickSettings(user.Id, userLastOrderSum);
            if (nCode != -1)
            {
                //message = nCode == -2 ? AdsCheckResult[-12] : string.Format("根据您上月消费额<br/>每天最多能看{0}条广告^_^", nCode);
                if (nCode == -2)
                    message = AdsCheckResult[-12];
                else if (nCode == -3)
                    message = AdsCheckResult[-13];
                else
                    message = string.Format(AdsCheckResult[-14], nCode);
                goto lbl_end;
            }
            //当会员在第3个月，或者后续某个月份没有产生过消费，
            //if (user.MemberDate.HasValue)
            //{
            //    //注册月份的1号
            //    DateTime dtRegMonth = user.MemberDate.Value.Date.AddDays((double)(1 - user.MemberDate.Value.Day));
            //    DateTime dtNowMonth = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day));
            //    if (dtNowMonth >= dtRegMonth.AddMonths(3) && userLastOrderSum == 0)
            //    {
            //        message = "您上月未消费过<br/>暂无权限浏览广告^_^";
            //        goto lbl_end;
            //    }
            //}
            //检查是否符合设定的消费额
            if (GetCompanyAdPoint(userLastOrderSum, item.AdsID) == 0)
            {
                message = AdsCheckResult[-10];
                goto lbl_end;
            }

            //检测是否浏览仅限一次的广告
            if (CheckUserOnceClickLimitAds(user.Id, company.Id))
            {
                message = "你已浏览过此商家投放的广告!";
                goto lbl_end;
            }
            message = string.Empty;
            return true;
        lbl_end:
            return false;
        }
        public static bool ClickCompanyAds(SysMember user, int adsID, string answer, out string message)
        {
            SysCompanyAd item = SysCompanyAd.FetchByID(adsID);
            SysCompany company = SysCompany.FetchByID(item.AdsCompanyID);
            decimal userLastOrderSum = UserBLL.GetUserLastMonthOrderSum(user.Id);
            if (!CheckCompanyAdsCanClick(user, item, company, userLastOrderSum, out message))
                goto lbl_end;
            if (!string.IsNullOrEmpty(item.AdsQuestion) && item.AdsRightAnswer.HasValue &&
                Utilities.ToInt(answer) != item.AdsRightAnswer.Value)
            {
                message = "你的互动答题答案不正确!";
                goto lbl_end;
            }
            decimal point = CompanyAdsBLL.GetCompanyAdPoint(userLastOrderSum, item.AdsID);
            if (CompanyBLL.IsMaxPointLevel(company.Id, point))
            {
                message = "此广告主累计赠送的积分已经超过500，须进行积分结算后才能继续操作系统";
                goto lbl_end;
            }
            SysMemberAd ad = new SysMemberAd
            {
                AdsDate = DateTime.Now,
                AdsMemberID = user.Id,
                AdsPoint = point,
                CompanyID = item.AdsCompanyID,
                CompanyAdsID = item.AdsID,
                OrderSum = userLastOrderSum,
                PaymentID = 0
            };
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                ad.Save( );
                item.AdsClicked = Utilities.ToInt(item.AdsClicked) + 1;
                item.AdsPointSum = Utilities.ToDecimal(item.AdsPointSum) + point;
                item.Save( );
                if (ad.AdsPoint.HasValue && ad.AdsPoint.Value > 0)
                {
                    var p = new Payment
                    {
                        PaymentDate = DateTime.Now,
                        PaymentCode = string.Empty,
                        PaymentCompanyID = item.AdsCompanyID,
                        PaymentEmail = string.Empty,
                        PaymentMemberID = user.Id,
                        PaymentMemo = string.Format("浏览【{0}】投放的广告,获得{1:0.00}个积分", company.CompanyName, ad.AdsPoint),
                        PaymentOrderID = ad.AdsID,
                        PaymentStatus = 1,
                        PaymentSum = ad.AdsPoint.Value,
                        PaymentType = (int)PaymentType.AdvsGive
                    };
                    p.Save( );
                    ad.PaymentID = p.Id;
                    ad.Save( );
                    OrderBLL.UpdateBalance( );
                }
                ts.Complete( );
                message = string.Format("成功抢得{0}个积分", ad.AdsPoint);
                return true;
            }
            catch (Exception ex)
            {
                message = "抢积分失败:" + ex.Message;
                Logging.Log("CompanyAdsBLL->ClickCompanyAds", ex, true);
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
        lbl_end:
            return false;
        }
        public static List<int> GetCookieRecViewAds( )
        {
            string cookieData = HttpUtility.UrlDecode(Utilities.GetCookie("RecViewAds").Value);
            if (string.IsNullOrEmpty(cookieData))
                return new List<int>( );
            return Utilities.JSONToObj<List<int>>(cookieData);
        }
        public static void SetCookieRecViewAds(int adsID)
        {
            var items = GetCookieRecViewAds( );
            if (!items.Contains(adsID))
            {
                if (items.Count >= 4)
                    items.RemoveAt(3);
                items.Insert(0, adsID);
                Utilities.AddCookie("RecViewAds", HttpUtility.UrlEncode(Utilities.ObjToJSON(items)));
            }
        }
        public static void RemoveRecViewAds( )
        {
            Utilities.RemoveCookie("RecViewAds");
        }
    }
}