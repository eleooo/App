using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class OrderCompanyItem : ActionPage
    {
        const string DATE_INPUT = "<input class=\"input_r\" onclick=\"WdatePicker()\" value=\"{0}\" type=\"text\" name=\"txtDate\" id=\"txtDate\" style=\"text-align:center\" /><br /><p style=\"color: rgb(251, 124, 4); padding-top: 5px; font-size: 12px;\">选好后只能修改一次</p>";
        private int _itemID = 0;
        public int ItemID
        {
            get
            {
                if (_itemID == 0)
                    _itemID = Utilities.ToInt(Request.Params["ItemID"]);
                return _itemID;
            }
        }
        public string ItemType
        {
            get
            {
                //return CmpType == CompanyType.UnionCompany ? "预订" : "抢购";
                return "抢购";
            }
        }
        public CompanyType CmpType
        {
            get
            {
                return Formatter.ToEnum<CompanyType>(Company.CompanyType);
            }
        }
        private SysCompany _company;
        public SysCompany Company
        {
            get
            {
                if (_company == null)
                    _company = SysCompany.FetchByID(CompanyItem.CompanyID);
                return _company;
            }
        }
        private int _memberItemID = 0;
        public int MemberItemID
        {
            get
            {
                if (_memberItemID == 0)
                {
                    _memberItemID = Utilities.ToInt(txtMemberItemID.Value);
                }
                return _memberItemID;
            }
        }
        private SysCompanyItem _companyItem;
        public SysCompanyItem CompanyItem
        {
            get
            {
                if (_companyItem == null)
                    _companyItem = SysCompanyItem.FetchByID(ItemID);
                return _companyItem;
            }
        }
        private SysMemberItem _memberItem;
        public SysMemberItem MemberItem
        {
            get
            {
                if (_memberItem == null)
                {
                    var query = DB.Select( ).From<SysMemberItem>( )
                                  .Where(SysMemberItem.ItemIDColumn).IsEqualTo(MemberItemID)
                                  .And(SysMemberItem.CompanyItemIDColumn).IsEqualTo(ItemID);
                    _memberItem = query.ExecuteSingle<SysMemberItem>( );
                }
                return _memberItem;
            }
        }
        public bool IsCanModifiedDate
        {
            get
            {
                return !(MemberItem != null &&
                         MemberItem.IsCanModifiedDate.HasValue &&
                         !MemberItem.IsCanModifiedDate.Value);
            }
        }
        public string RenderOrderDateInput( )
        {
            string date = MemberItem != null ? Utilities.ToDate(MemberItem.ItemDate) : string.Empty;
            if (string.IsNullOrEmpty(date) && CompanyItem.ItemDate.HasValue && CompanyItem.ItemDate.Value >= DateTime.Today)
                date = Utilities.ToDate(CompanyItem.ItemDate);
            if (string.IsNullOrEmpty(date) && CompanyItem.ItemEndDate.HasValue && CompanyItem.ItemEndDate >= DateTime.Today)
                date = Utilities.ToDate(DateTime.Today);
            if (string.IsNullOrEmpty(date))
                date = Utilities.ToDate(DateTime.Today);
            if (IsCanModifiedDate)
                return string.Format(DATE_INPUT, date);
            else
                return date;
        }
        public string AddOrEdit
        {
            get
            {
                return MemberItem == null ? "Add" : "Edit";
            }
        }
        private string InputDate
        {
            get
            {
                return Request.Params["txtDate"];
            }
        }
        protected string ValidateMessage { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CompanyItem == null)
            {
                Utilities.ShowMessageRedirect("优惠项目不存在!");
                return;
            }
            else if (CmpType == CompanyType.MealCompany)
            {
                string mansionId = Request.Params["MansionId"];
                if (string.IsNullOrEmpty(mansionId))
                    Utilities.Redirect("/Public/OrderMealPage.aspx");
                else
                    Utilities.Redirect(string.Format("/Member/OrderCompanyMealItem.aspx?ItemId={0}&MansionId={1}", ItemID, mansionId), false);
                return;
            }

            if (!IsPostBack &&
                !string.IsNullOrEmpty(Request.Params["MemberItemID"]) &&
                string.IsNullOrEmpty(txtMemberItemID.Value))
            {
                txtMemberItemID.Value = Request.Params["MemberItemID"];
            }
            ValidateMessage = string.Empty;
            btnPost.Visible = MemberItemID == 0 || IsCanModifiedDate;
            if (MemberItemID > 0)
                imgButton.Src = "/App_Themes/ThemesV2/images/xd-aniu-tj.png";
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            if (MemberItem == null)
            {
                //txtMessage.InnerHtml = "你还没有抢购此项目";
                Utilities.Redirect("/Public/ViewItemList.aspx", false);
                this.Visible = false;
                return;
            }
            string message;
            if (CompanyItemBLL.CancelCompanyItem(CurrentUser, CompanyItem, MemberItem, out message))
            {
                txtMemberItemID.Value = "-1";
                Utilities.ShowMessageRedirect("删除成功", "/Member/CompanyMyItems.aspx");
                return;
            }
            ValidateMessage = message;
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (MemberItem == null)
            {
                ValidateMessage = "你还没有抢购此项目";
                return;
            }
            if (!IsCanModifiedDate)
            {
                ValidateMessage = "您已修改一次到店时间，无法再次修改";
                return;
            }
            DateTime dt;
            string message;
            if (!GetInputDate(out dt, out message))
            {
                ValidateMessage = message;
                return;
            }
            MemberItem.ItemDate = dt;
            MemberItem.IsCanModifiedDate = false;
            MemberItem.Save( );
            Utilities.ShowMessageRedirect("修改成功", "/Member/CompanyMyItems.aspx");
            //txtMessage.InnerHtml = "修改成功";
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            string message;
            int memberItemID;
            DateTime dtDate;
            if (GetInputDate(out dtDate, out message))
            {
                if (CompanyItemBLL.ClickCompanyItem(Company, CurrentUser, dtDate, CompanyItem, out memberItemID, out message))
                {
                    Utilities.ShowMessageRedirect("{ItemType}成功!".Replace("{ItemType}", ItemType), "/Member/CompanyMyItems.aspx");
                    return;
                }
            }
            ValidateMessage = message.Replace("{ItemType}", ItemType);
        }
        private bool GetInputDate(out DateTime dt, out string message)
        {
            string date = InputDate;
            message = string.Empty;
            dt = DateTime.MinValue;
            if (string.IsNullOrEmpty(date))
            {
                message = "请输入预计到店时间";
                return false;
            }
            DateTime dtMin = (CompanyItem.ItemDate.HasValue && CompanyItem.ItemDate.Value.Date >= DateTime.Today) ? CompanyItem.ItemDate.Value : DateTime.Today;
            dt = Utilities.ToDateTime(date);
            if (dt < dtMin ||
                (CompanyItem.ItemEndDate.HasValue && dt > CompanyItem.ItemEndDate.Value))
            {
                message = string.Format("请选择{0}至{1}的到店时间", dtMin.ToString("yyyy-MM-dd"),
                                                                       CompanyItem.ItemEndDate.Value.ToString("yyyy-MM-dd"));
                return false;
            }
            return true;
        }
    }
}