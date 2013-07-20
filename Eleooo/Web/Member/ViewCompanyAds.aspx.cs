using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Eleooo.DAL;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class ViewCompanyAds : ActionPage
    {
        const string ANSWER_TPL = "<dd><input type=\"radio\" class=\"t_radio\" value=\"{0}\" name=\"answer\" />{1}{2}</dd>";
        int? _adsID = null;
        public int AdsID
        {
            get
            {
                if (!_adsID.HasValue)
                    _adsID = Utilities.ToInt(Request.Params["AdsID"]);
                return _adsID.Value;
            }
        }
        private SysCompanyAd _companyAd;
        public SysCompanyAd CompanyAd
        {
            get
            {
                if (_companyAd == null)
                    _companyAd = SysCompanyAd.FetchByID(AdsID);
                return _companyAd;
            }
        }
        private SysCompany _company;
        public SysCompany Company
        {
            get
            {
                if (_company == null && CompanyAd != null)
                    _company = SysCompany.FetchByID(CompanyAd.AdsCompanyID);
                return _company;
            }
        }
        public string GetCompanyAdsTitle( )
        {
            if (CompanyAd == null)
                return string.Empty;
            else
                return HttpUtility.HtmlDecode(CompanyAd.AdsTitle);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            //if (!UserBLL.UserHasArea(CurrentUser))
            //{
            //    Utilities.Redirect("/Member/MyArea.aspx", false);
            //    return;
            //}
            decimal orderSum, point;
            orderSum = UserBLL.GetUserLastMonthOrderSum(CurrentUser.Id);
            point = CompanyAdsBLL.GetCompanyAdPoint(orderSum, AdsID);
            BuildPointSetting(orderSum, point);
            string json = Utilities.ObjToJSON(GetInitOpts(orderSum, point));
            base.MasterPage.AddLoadedScript(string.Format("(new ViewCompanyAd()).init({0});", json));
            CompanyAdsBLL.SetCookieRecViewAds(AdsID);
            IsUserLink = true;
            CompanyAdsBLL.SetCookieRecViewAds(AdsID);
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            string message;
            bool b = CompanyAdsBLL.ClickCompanyAds(CurrentUser, AdsID, Request.Params["answer"], out message);
            var result = new
            {
                code = b ? 0 : -1,
                message = message
            };
            //Response.ContentType = "text/json";
            this.Visible = false;
            Response.Write(Utilities.ObjToJSON(result));
            //Response.Flush( );
            //Response.End( );
        }
        private int _timerLength = 15;
        public int TimerLength
        {
            get { return _timerLength; }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            if (Action == Actions.Add)
                Response.End( );
            base.OnLoadComplete(e);
        }

        Dictionary<object, object> GetInitOpts(decimal orderSum, decimal point)
        {
            Dictionary<object, object> opts = new Dictionary<object, object>( );
            Dictionary<ViewCompanyAdsViews, string> views = new Dictionary<ViewCompanyAdsViews, string>( );
            bool isCanSee = false;
            int companyItemID = 0;
            StringBuilder sbQuestion;
            if (CompanyAd == null)
                views[ViewCompanyAdsViews.Default] = string.Format(msgTpl.InnerHtml, "此广告不存在!");
            else if (Company == null)
                views[ViewCompanyAdsViews.Default] = string.Format(msgTpl.InnerHtml, "广告主不存在!");
            else
            {
                string message;
                isCanSee = CompanyAdsBLL.CheckCompanyAdsCanClick(CurrentUser, CompanyAd, Company, orderSum, out message);
                if (!isCanSee)
                    views[ViewCompanyAdsViews.Default] = string.Format(msgTpl.InnerHtml, message);
                else
                    views[ViewCompanyAdsViews.Default] = defaultTpl.InnerHtml;
            }
            if (Company != null)
                companyItemID = Utilities.ToInt(CompanyItemBLL.GetCompanyLastItemID(CurrentUser.Id, Company.Id));
            sbQuestion = GetAnswerQuestion( );
            views.Add(ViewCompanyAdsViews.AnswerQuestion, string.Format(questionTpl.InnerHtml, sbQuestion));
            views.Add(ViewCompanyAdsViews.AnswerRight, GetRightAnswerTemplate(companyItemID, point));
            views.Add(ViewCompanyAdsViews.AnswerWrong, answerWrongTpl.InnerHtml);
            views.Add(ViewCompanyAdsViews.Message, msgTpl.InnerHtml);
            opts.Add("ViewTemplate", views);
            opts.Add("CompanyItemID", companyItemID);
            opts.Add("HasQuestion", sbQuestion.Length > 0);
            opts.Add("RightAnswer", CompanyAd != null ? Utilities.ToInt(CompanyAd.AdsRightAnswer) : 0);
            opts.Add("IsCanSee", isCanSee);
            var images = GetAdsImages( );
            _timerLength = images.Count * 3;
            opts.Add("AnswerTimeout", _timerLength);
            opts.Add("AdImages", images);
            return opts;
        }
        string GetPointSettingDesc(decimal orderSum)
        {
            //Math.Abs(Utilities.ToDecimal(item.OrderSumLimit)
            if (orderSum < 0)
                return string.Format("上月消费额{0}元以下", -orderSum);
            else
                return string.Format("上月消费额{0}元以上", orderSum);
        }
        void BuildPointSetting(decimal orderSum, decimal point)
        {
            List<SysCompanyAdsPointSetting> colls = DB.Select( ).From<SysCompanyAdsPointSetting>( )
                                                                   .Where(SysCompanyAdsPointSetting.AdsIDColumn).IsEqualTo(AdsID)
                                                                   .ExecuteTypedList<SysCompanyAdsPointSetting>( );
            HtmlTableRow tr;
            HtmlTableCell tc;
            foreach (var item in colls)
            {
                tr = new HtmlTableRow( );
                tc = new HtmlTableCell( );
                tc.InnerHtml = GetPointSettingDesc(Utilities.ToDecimal(item.OrderSumLimit));
                tr.Cells.Add(tc);
                tc = new HtmlTableCell( );
                tc.InnerHtml = Math.Abs(Utilities.ToDecimal(item.AdsPoint)).ToString("0.###") + "分/次";
                tr.Cells.Add(tc);
                pointSetting.Controls.Add(tr);
            }
            tr = new HtmlTableRow( );
            tc = new HtmlTableCell( );
            tc.InnerHtml = "您本次有效浏览奖励";
            tr.Cells.Add(tc);
            tc = new HtmlTableCell( );
            tc.InnerHtml = string.Format("{0}分", Math.Abs(point).ToString("0.###"));
            tr.Cells.Add(tc);
            pointSetting.Controls.Add(tr);
        }
        StringBuilder GetAnswerQuestion( )
        {
            StringBuilder sb = new StringBuilder( );
            if (CompanyAd != null)
            {
                sb.Append("<dl>");
                if (!string.IsNullOrEmpty(CompanyAd.AdsQuestion))
                    sb.AppendFormat("<dt>{0}{1}</dt>", CompanyAd.AdsQuestion, CompanyAd.AdsQuestion.EndsWith("？") || CompanyAd.AdsQuestion.EndsWith("?") ? string.Empty : "？");
                if (!string.IsNullOrEmpty(CompanyAd.AdsAnswer1))
                    sb.AppendFormat(ANSWER_TPL, 1, "A.", CompanyAd.AdsAnswer1);
                if (!string.IsNullOrEmpty(CompanyAd.AdsAnswer2))
                    sb.AppendFormat(ANSWER_TPL, 2, "B.", CompanyAd.AdsAnswer2);
                if (!string.IsNullOrEmpty(CompanyAd.AdsAnswer3))
                    sb.AppendFormat(ANSWER_TPL, 3, "C.", CompanyAd.AdsAnswer3);
                if (!string.IsNullOrEmpty(CompanyAd.AdsAnswer4))
                    sb.AppendFormat(ANSWER_TPL, 4, "D.", CompanyAd.AdsAnswer4);
                sb.Append("</dl>");
            }
            return sb;
        }
        string GetRightAnswerTemplate(int companyItemID, decimal point)
        {
            if (companyItemID > 0)
                return string.Format(answerRightTpl1.InnerHtml, point, companyItemID);
            else
                return string.Format(answerRightTpl2.InnerHtml, point);
        }
        List<string> GetAdsImages( )
        {
            if (AdsID > 0)
                return FileUpload.GetFileRelPaths(SaveType.CompanyAds, FileType.Image, AdsID.ToString( ));
            else
                return new List<string>( );
        }
    }
}