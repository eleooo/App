using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class CompanyItemEdit : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CompanyBLL.GetCompanyType(AppContext.Context.Company.CompanyType) == CompanyType.AdCompany)
            {
                txtMessage.InnerHtml = "阁下为广告商家类型,不允许使用此功能";
                return;
            }
            formView.DataSource = DB.Select( ).From<SysCompanyItem>( )
                                    .Where(SysCompanyItem.ItemIDColumn).IsEqualTo(ItemID)
                                    .And(SysCompanyItem.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                                    .And(SysCompanyItem.IsDeletedColumn).IsEqualTo(false);
            formView.AddShowColumn(SysCompanyItem.ItemTitleColumn)
                    .AddShowColumn(SysCompanyItem.ItemAmountColumn)
                    .AddShowColumn(SysCompanyItem.ItemSumColumn)
                    .AddShowColumn(SysCompanyItem.ItemPointColumn)
                    .AddShowColumn(SysCompanyItem.AreaDepthColumn)
                    .AddShowColumn(SysCompanyItem.ItemIntroColumn)
                    .AddShowColumn(SysCompanyItem.ItemContentColumn)
                    .AddShowColumn(SysCompanyItem.ItemTipsColumn)
                    .AddShowColumn(SysCompanyItem.ItemPicColumn)
                    .AddShowColumn(SysCompanyItem.ItemPic1Column)
                    .AddShowColumn(SysCompanyItem.ItemPic2Column)
                    .AddShowColumn(SysCompanyItem.OrderSumLimitColumn) //消费层次
                    .AddShowColumn(SysCompanyItem.MemberLimitColumn, "0") //抢购对象
                    .AddShowColumn(SysCompanyItem.ItemLimitColumn, "1") //抢购频率 0
                    .AddShowColumn(SysCompanyItem.IsCanDelColumn, "0") //是否支持退订
                    .AddShowColumn(SysCompanyItem.ItemDateColumn, DateTime.Today.ToString("yyyy-MM-dd"))
                    .AddShowColumn(SysCompanyItem.ItemEndDateColumn, DateTime.Today.AddDays(6).ToString("yyyy-MM-dd")); //有效期限
            formView.OnBeforeSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnBeforeSaved);
            formView.OnValidate += new Web.Controls.OnFormViewValidate(formView_OnValidate);
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            txtMessage.InnerHtml = string.Empty;
        }

        void formView_OnValidate(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "ItemTitle":
                    if (string.IsNullOrEmpty(viewRow.Value))
                        viewRow.ValidateMessage = "优惠信息不能为空!";
                    break;
                case "ItemAmount":
                    int amount = Utilities.ToInt(viewRow.Value);
                    if (amount <= 0)
                        viewRow.ValidateMessage = "优惠数量必须是大于零的整数";
                    break;
                case "AreaDepth":
                    string depth = string.Empty;
                    depth = AreaSelector.SelectedArea3;
                    if (string.IsNullOrEmpty(depth))
                        viewRow.ValidateMessage = "请选择优惠覆盖区域";
                    viewRow.Value = depth;
                    break;
                case "ItemSum":
                    decimal d = Utilities.ToDecimal(viewRow.Value);
                    if (d <= 0)
                        viewRow.ValidateMessage = "兑换额度必须要大于零.";
                    break;
                case "ItemLimit":
                    int n;
                    if (!string.IsNullOrEmpty(viewRow.Value) && (!int.TryParse(viewRow.Value, out n) || n <= 0 || n > 2))
                        viewRow.ValidateMessage = "请选择抢购频率";
                    break;
                case "ItemPoint":
                    decimal dPoint = Utilities.ToDecimal(viewRow.Value);
                    if (dPoint <= 0)
                        viewRow.ValidateMessage = "抢购积分不能小于零.";
                    break;
                case "ItemDate":
                    DateTime dtBegin, dtEnd;
                    if (!DateTime.TryParse(viewRow.Value, out dtBegin))
                    {
                        viewRow.ValidateMessage = "请输入有效的开始日期";
                        break;
                    }
                    Controls.UcFormView.FormViewRow rowEndDate = formView.GetViewRow(SysCompanyItem.ItemEndDateColumn);
                    if (!DateTime.TryParse(rowEndDate.Value, out dtEnd) || dtEnd < dtBegin)
                    {
                        rowEndDate.ValidateMessage = "请输入有效的结束日期";
                    }
                    break;
            }
        }

        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "IsCanDel":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetRadioHtml(CompanyItemBLL.IsItemCanDel, viewRow.ParamName, viewRow.Value).ToString( );
                    break;
                case "ItemTitle":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetTextAreaHtml(viewRow.ParamName, viewRow.Value);
                    break;
                case "AreaDepth":
                    AreaSelector.Selector3.DefaultValue = string.IsNullOrEmpty(viewRow.Value) ? AppContext.Context.Company.CompanyArea : viewRow.Value;
                    AreaSelector.Selector3.DefaultMultiValue = viewRow.Value;
                    viewRow.RenderHtml = AreaSelector.Selector3.RenderResult( ).ToString( );
                    break;
                case "ItemPic":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetPhotoHtml(viewRow.ParamName, viewRow.Value);
                    break;
                case "ItemPic1":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetPhotoHtml(viewRow.ParamName, viewRow.Value);
                    break;
                case "ItemPic2":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetPhotoHtml(viewRow.ParamName, viewRow.Value);
                    break;
                case "MemberLimit":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetRadioHtml(CompanyItemBLL.MemberLimit, viewRow.ParamName, viewRow.Value).ToString( );
                    break;
                case "ItemLimit":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetRadioHtml(CompanyItemBLL.ItemLimit, viewRow.ParamName, viewRow.Value).ToString( );
                    break;
                case "OrderSumLimit":
                    viewRow.RenderHtml = string.Format("{0}&nbsp;&nbsp;留空表示不限消费层次", viewRow.RenderHtml);
                    break;
                case "ItemContent":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetTextAreaHtml(viewRow.ParamName, viewRow.Value);
                    break;
                case "ItemIntro":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetTextAreaHtml(viewRow.ParamName, viewRow.Value);
                    break;
                case "ItemTips":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetTextAreaHtml(viewRow.ParamName, viewRow.Value);
                    break;
            }
        }

        void formView_OnBeforeSaved(object item)
        {
            SysCompanyItem companyItem = item as SysCompanyItem;
            if (companyItem.IsNew)
            {
                companyItem.CompanyID = CurrentUser.CompanyId.Value;
                //companyItem.ItemDate = DateTime.Now;
                companyItem.ItemClicked = 0;
                companyItem.ItemUsed = 0;
                companyItem.ItemAddr = AppContext.Context.Company.CompanyAddress;
                companyItem.IsDeleted = false;
                companyItem.IsPass = false;
            }
            companyItem.AreaDepth = AreaSelector.SelectedArea3;
            if (companyItem.ItemSum == null)
                companyItem.ItemSum = 0M;
            if (companyItem.ItemAmount == null)
                companyItem.ItemAmount = 0;
            if (companyItem.OrderSumLimit == null)
                companyItem.OrderSumLimit = 0;
            if (companyItem.ItemLimit == null)
                companyItem.ItemLimit = 1;
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (formView.Save<SysCompanyItem>(0) == 0)
            {
                txtMessage.InnerHtml = "保存成功";
                formView.ClearValue( );
            }
            else
                txtMessage.InnerHtml = "保存失败";
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (formView.Save<SysCompanyItem>(ItemID) == 0)
                txtMessage.InnerHtml = "保存成功";
            else
                txtMessage.InnerHtml = "保存失败";
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }
        int ItemID
        {
            get
            {
                return Utilities.ToInt(Request.Params["id"]);
            }
        }
    }
}