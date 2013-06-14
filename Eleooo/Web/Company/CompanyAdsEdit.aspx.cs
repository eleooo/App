using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Web.Controls;
using SubSonic;
using System.Transactions;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class CompanyAdsEdit : ActionPage
    {
        const string ADSIMAGE_BTN = "<input id='{0}' name='{0}' value='{1}' type='hidden' /><input type='button' value='上传广告图片' id='btnAdsImage' class='button' />";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CompanyBLL.GetCompanyType(AppContext.Context.Company.CompanyType) == CompanyType.MealCompany)
            {
                txtMessage.InnerHtml = "你暂不允许使用此功能";
                return;
            }
            var pointQuery = DB.Select( ).From<SysCompanyAdsPointSetting>( )
                                 .Where(SysCompanyAdsPointSetting.AdsIDColumn).IsEqualTo(AdsID);
            EditableGrid<SysCompanyAdsPointSetting> pointGrid = new EditableGrid<SysCompanyAdsPointSetting>(SysCompanyAdsPointSetting.table.Name, pointQuery);
            pointGrid.IsNeedLoadScript = false;
            pointGrid.AddShowColumn(SysCompanyAdsPointSetting.OrderSumLimitColumn);
            pointGrid.AddShowColumn(SysCompanyAdsPointSetting.AdsPointColumn);
            pointGrid.MaxAllowRow = 5;
            pointGrid.OnBeforSave += new EditableGrid<SysCompanyAdsPointSetting>.OnEditableGridSaveHandler(pointGrid_OnBeforSave);

            formView.DataSource = DB.Select( ).From<SysCompanyAd>( )
                                    .Where(SysCompanyAd.AdsIDColumn).IsEqualTo(AdsID)
                                    .And(SysCompanyAd.AdsCompanyIDColumn).IsEqualTo(AppContext.Context.Company.Id)
                                    .And(SysCompanyAd.IsDeletedColumn).IsEqualTo(false);
            formView.AddShowColumn(SysCompanyAd.AdsTitleColumn)
                                .AddGridColumn(pointGrid, "奖励设置")
                                .AddCustomColumn("AdsImages", "广告图片", AdsID == 0 ? Guid.NewGuid( ).ToString("D") : Request.Params["id"])
                                .AddShowColumn(SysCompanyAd.AreaDepthColumn)
                                .AddShowColumn(SysCompanyAd.SexLimitColumn, "0")
                                .AddShowColumn(SysCompanyAd.AdsMemberLimitColumn, "0", false, "浏览对象")
                                .AddShowColumn(SysCompanyAd.AdsClickLimitColumn, "1", false, "浏览频率")
                                .AddShowColumn(SysCompanyAd.AdsQuestionColumn, "", false, "互动问答", "", 400)
                                .AddShowColumn(SysCompanyAd.AdsAnswer1Column, "", false, "答案A", "", 400)
                                .AddShowColumn(SysCompanyAd.AdsAnswer2Column, "", false, "答案B", "", 400)
                                .AddShowColumn(SysCompanyAd.AdsAnswer3Column, "", false, "答案C", "", 400)
                                .AddShowColumn(SysCompanyAd.AdsAnswer4Column, "", false, "答案D", "", 400)
                                .AddShowColumn(SysCompanyAd.AdsRightAnswerColumn, "", false, "正确答案")
                                .AddShowColumn(SysCompanyAd.AdsDayLimitAmountColumn, "", false, "日最高浏览量")
                                .AddShowColumn(SysCompanyAd.AdsDayLimitSumColumn, "", false, "日最高投放额")
                                .AddShowColumn(SysCompanyAd.AdsDateColumn, DateTime.Today.ToString("yyyy-MM-dd"), false, "开始日期")
                                .AddShowColumn(SysCompanyAd.AdsEndDateColumn, DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"), false, "截止日期"); //有效期限
            formView.OnBeforeSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnBeforeSaved);
            formView.OnAfterSaved += new OnFormViewSaveHandle(formView_OnAfterSaved);
            formView.OnValidate += new Web.Controls.OnFormViewValidate(formView_OnValidate);
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            txtMessage.InnerHtml = string.Empty;
        }

        void formView_OnAfterSaved(object item)
        {
            SysCompanyAd ad = (SysCompanyAd)item;
            Eleooo.Web.Controls.UcFormView.FormViewRow viewRow = formView.GetViewRow("AdsImages");
            if (AdsID == 0)
            {
                Eleooo.Common.FileUpload.RenameFolder(SaveType.CompanyAds, viewRow.ParamValue, ad.AdsID.ToString( ));
                ad.AdsPic = Eleooo.Common.FileUpload.GetFileRelPath(SaveType.CompanyAds, FileType.Image, ad.AdsID.ToString( ), false);
                ad.Save( );
            }
        }

        void pointGrid_OnBeforSave(SysCompanyAdsPointSetting item, out string message)
        {
            message = string.Empty;
            if (!item.AdsPoint.HasValue || item.AdsPoint <= 0)
                message = "奖励积分必须大于零";
            //if (!item.OrderSumLimit.HasValue || item.OrderSumLimit < 0)
            //    message = "上月消费限额必须大于零";
            if (item.IsNew)
                item.AdsID = (formView.SavedItem as SysCompanyAd).AdsID;
        }
        void formView_OnValidate(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "AdsTitle":
                    if (string.IsNullOrEmpty(viewRow.Value))
                        viewRow.ValidateMessage = "请输入广告内容!";
                    break;
                case "AdsPoint":
                    decimal point = Utilities.ToDecimal(viewRow.Value);
                    if (point <= 0)
                        viewRow.ValidateMessage = "广告奖励积分必须要大于零!";
                    break;
                case "AdsDayLimitAmount":
                    decimal amount = Utilities.ToDecimal(viewRow.Value);
                    if (amount <= 0)
                        viewRow.ValidateMessage = "日最高浏览量必须要大于零!";
                    break;
                case "AdsDayLimitSum":
                    decimal sum = Utilities.ToDecimal(viewRow.Value);
                    if (sum <= 0)
                        viewRow.ValidateMessage = "日最高投放额必须要大于零!";
                    break;
                case "AreaDepth":
                    string depth = string.Empty;
                    depth = AreaSelector.SelectedArea3;
                    if (string.IsNullOrEmpty(depth))
                        viewRow.ValidateMessage = "请选择广告投放区域";
                    viewRow.Value = depth;
                    break;
                case "AdsDate":
                    DateTime dtBegin, dtEnd;
                    if (!DateTime.TryParse(viewRow.Value, out dtBegin))
                    {
                        viewRow.ValidateMessage = "请输入有效的开始日期";
                        break;
                    }
                    Controls.UcFormView.FormViewRow rowEndDate = formView.GetViewRow(SysCompanyAd.AdsEndDateColumn);
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
                case "AdsTitle":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetTextAreaHtml(viewRow.ParamName, viewRow.Value);
                    break;
                case "AreaDepth":
                    AreaSelector.Selector3.DefaultValue = string.IsNullOrEmpty(viewRow.Value) ? AppContext.Context.Company.CompanyArea : viewRow.Value;
                    AreaSelector.Selector3.DefaultMultiValue = viewRow.Value;
                    viewRow.RenderHtml = AreaSelector.Selector3.RenderResult( ).ToString( );
                    break;
                case "SexLimit":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetRadioHtml(CompanyAdsBLL.SexLimit, viewRow.ParamName, viewRow.Value).ToString( );
                    break;
                case "AdsMemberLimit":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetRadioHtml(CompanyAdsBLL.MemberLimit, viewRow.ParamName, viewRow.Value).ToString( );
                    break;
                case "AdsClickLimit":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetRadioHtml(CompanyAdsBLL.AdsLimit, viewRow.ParamName, viewRow.Value).ToString( );
                    break;
                case "AdsRightAnswer":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetRadioHtml(CompanyAdsBLL.QuestionAnswers, viewRow.ParamName, viewRow.Value).ToString( );
                    break;
                case "AdsImages":
                    viewRow.RenderHtml = string.Format(ADSIMAGE_BTN, viewRow.ParamName, viewRow.Value);
                    break;
            }
        }
        void formView_OnBeforeSaved(object item)
        {
            SysCompanyAd ad = item as SysCompanyAd;
            if (ad.IsNew)
            {
                ad.AdsCompanyID = CurrentUser.CompanyId;
                ad.AdsClicked = 0;
                ad.AdsDate = DateTime.Now;
                ad.IsDeleted = false;
                ad.IsPass = false;
                ad.AdsPic = null;
                ad.AdsPointSum = 0;
            }
            else
            {
                ad.AdsPic = Eleooo.Common.FileUpload.GetFileRelPath(SaveType.CompanyAds, FileType.Image, ad.AdsID.ToString( ), false);
            }
            ad.AreaDepth = AreaSelector.SelectedArea3;
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope( ))
            {
                using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                {
                    try
                    {
                        if (CompanyBLL.IsMaxPointLevel(CurrentUser.CompanyId.Value, 0))
                            txtMessage.InnerHtml = "累计赠送的积分已经超过500，须进行积分结算后才能继续操作系统";
                        else if (formView.Save<SysCompanyAd>(0) == 0)
                        {
                            ts.Complete( );
                            txtMessage.InnerHtml = "保存成功";
                            formView.ClearValue( );
                        }
                        else
                            txtMessage.InnerHtml = "保存失败";
                    }
                    catch (Exception ex)
                    {
                        Logging.Log("CompanyAdsEdit->On_ActionAdd", ex,true);
                        txtMessage.InnerHtml = ex.Message;
                    }
                }
            }
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope( ))
            {
                using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                {
                    try
                    {
                        if (CompanyBLL.IsMaxPointLevel(CurrentUser.CompanyId.Value, 0))
                            txtMessage.InnerHtml = "累计赠送的积分已经超过500，须进行积分结算后才能继续操作系统";
                        else if (formView.Save<SysCompanyAd>(AdsID) == 0)
                        {
                            ts.Complete( );
                            txtMessage.InnerHtml = "保存成功";
                            //formView.ClearValue( );
                        }
                        else
                            txtMessage.InnerHtml = "保存失败";
                    }
                    catch (Exception ex)
                    {
                        Logging.Log("CompanyAdsEdit->On_ActionEdit", ex, true);
                        txtMessage.InnerHtml = ex.Message;
                    }
                }
            }
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }
        int AdsID
        {
            get
            {
                return Utilities.ToInt(Request.Params["id"]);
            }
        }
    }
}