using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Transactions;
using SubSonic;
using Uc = Eleooo.Web.Controls;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class CompanyAdd : ActionPage
    {
        #region const html template
        const string DISABLE_INPUT_TEXT = "<input name='{0}' type='text' value='{1}' id='{0}' disabled='disabled' />";
        const string PASSWORD_INPUT_TEXT = "<input maxLength='6' id='{0}' name='{0}' value='{1}' type='password' />&nbsp;&nbsp;确认密码:<input maxLength='6' id='{0}_1' name='{0}_1' value='{1}' type='password' />";

        const string COMPANY_PHOTO =
        @" <input id='{0}' name='{0}' value='{1}' type='text'  class='txtUpload' />
           <input type='button' value='上传图片' id='btnUpload' class='button' />
           <br />
           <img id='{0}_img' src='{1}' width='300px' class='imgUpload' {2} />";
        const string COMPANY_TYPE =
        @"<input class='radio' type='radio' name='{0}' value='1' {1} />联盟商家
          <input class='radio' type='radio' name='{0}' value='2' {2} />特约商家
          <input class='radio' type='radio' name='{0}' value='3' {3} />广告商家
          <input class='radio' type='radio' name='{0}' value='4' {4} />快餐店";
        const string COMPANY_FLAG =
        @"<input class='radio' type='radio' name='{0}' value='1' {1} />积分
          <input class='radio' type='radio' name='{0}' value='2' {2} />全部";
        const string COMPANY_STATUS =
        @"<input class='radio' type='radio' name='{0}' value='1' {1} />正常
          <input class='radio' type='radio' name='{0}' value='2' {2} />审核";
        const string COMPANY_CODE = "<span id='{0}' style='color:red;'></span>";
        const string COMPANYPIC_BTN = "<input id='{0}' name='{0}' value='{1}' type='hidden' /><input type='button' value='上传实景图片' id='btnCompanyPic' class='button' />";
        #endregion

        #region fields
        Uc.UcFormView.FormViewRow msnPhoneRow;
        Uc.UcFormView.FormViewRow workTimeRow;
        Uc.UcFormView.FormViewRow servicesRow;
        Uc.UcFormView.FormViewRow servicesSumRow;
        Uc.UcFormView.FormViewRow orderMaxAmountRow;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            formView.DataSource = DB.Select( ).From<SysCompany>( )
                        .Where(SysCompany.IdColumn).IsEqualTo(0);
            formView.AddShowColumn(SysCompany.CompanyCodeColumn)
                    .AddShowColumn(SysCompany.CompanyNameColumn)
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(SysCompany.CompanyPwdColumn)
                    .AddShowColumn(SysCompany.CompanyPhoneColumn)
                    .AddShowColumn(SysCompany.CompanyEmailColumn)
                    .AddShowColumn(SysCompany.CompanyAddressColumn)

                    .AddShowColumn(SysCompany.MsnPhoneNumColumn) //短信接收号码
                    .AddShowColumn(SysCompany.OnSetSumColumn, "", false, "", "", 90) //起送金额
                    .AddShowColumn(SysCompany.ServiceSumColumn) //送餐费用
                    .AddShowColumn(SysCompany.OrderElapsedColumn) //送餐速度
                    .AddShowColumn(SysCompany.OrderMaxAmountColumn)//峰值接单量

                    .AddShowColumn(SysCompany.CompanyMemoColumn)
                    .AddShowColumn(SysCompany.CompanyItemColumn)
                    .AddShowColumn(SysCompany.CompanyWorkTimeColumn)
                    .AddShowColumn(SysCompany.CompanyServicesColumn)
                    .AddShowColumn(SysCompany.CompanyIntroColumn)
                    .AddShowColumn(SysCompany.CompanyContentColumn)
                    .AddShowColumn(SysCompany.CompanyPhotoColumn)
                    .AddCustomColumn("CompanyPic", "实景图片", Guid.NewGuid( ).ToString("D"))
                    .AddShowColumn(SysCompany.CompanyRateMasterColumn)
                    .AddShowColumn(SysCompany.IsUseMsgColumn, "0", false, "自动模式") //是否自动模式
                    .AddShowColumn(SysCompany.CompanyRateColumn)
                    .AddShowColumn(SysCompany.CompanyTypeColumn, "1")
                    .AddShowColumn(SysCompany.CompanyStatusColumn, "1");
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnValidate += new Web.Controls.OnFormViewValidate(formView_OnValidate);
            formView.OnBeforeSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnBeforeSaved);
            formView.OnAfterSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnAfterSaved);

            msnPhoneRow = formView.GetViewRow(SysCompany.MsnPhoneNumColumn);
            workTimeRow = formView.GetViewRow(SysCompany.CompanyWorkTimeColumn);
            servicesRow = formView.GetViewRow(SysCompany.CompanyServicesColumn);
            servicesSumRow = formView.GetViewRow(SysCompany.ServiceSumColumn);
            orderMaxAmountRow = formView.GetViewRow(SysCompany.OrderMaxAmountColumn);
            msnPhoneRow.IsSkip = true;
            workTimeRow.IsSkip = true;
            servicesRow.IsSkip = true;
            servicesRow.Width = 150;
            servicesSumRow.IsSkip = true;
            servicesSumRow.Width = 90;
            orderMaxAmountRow.IsSkip = true;

            lblMessage.InnerHtml = string.Empty;
        }

        void formView_OnAfterSaved(object item)
        {
            SysCompany company = item as SysCompany;
            SysMember user = UserBLL.CompanyToMember(company);
            user.Save( );
            Eleooo.Web.Controls.UcFormView.FormViewRow viewRow = formView.GetViewRow("CompanyPic");
            Eleooo.Common.FileUpload.RenameFolder(SaveType.Company, viewRow.ParamValue, company.Id.ToString( ));
            string vSql = string.Format("UPDATE Sys_Member_CompanyR SET CompanyStatus = 1 WHERE (CompanyTel='{0}' OR CompanyTel='{1}') and CompanyStatus = 0;", company.CompanyPhone, company.CompanyTel);
            QueryCommand cmd = new QueryCommand(vSql);
            DataService.ExecuteQuery(cmd);
        }
        void formView_OnBeforeSaved(object item)
        {
            SysCompany company = item as SysCompany;
            company.CompanyPwd = Utilities.DESEncrypt(company.CompanyPwd);
            company.AreaDepth = AreaSelector.SelectedArea1;
            company.CompanyCity = Utilities.ToInt(AreaSelector.Selector1.GetSelectedValue(0));
            company.CompanyArea = AreaSelector.GetSelectedLocation2( );
            company.CompanyLocation = AreaSelector.GetSelectedLocation3( );
            company.CompanyBalance = 0;
            company.CompanyBalanceCash = 0;
            company.CompanyDate = DateTime.Now;
            company.CompanyDateView = DateTime.Now;
            company.CompanyFacebookCount = 0;
            company.CompanyMsn = string.Empty;
            company.CompanyProvince = null;
            company.CompanySaleCount = 0;
            company.CompanySaleSum = 0;
            company.CompanyRateSale = null;
            company.CompanySkype = string.Empty;
            company.CompanyToken = string.Empty;
            company.IsUseFinger = true;
            company.MenuDate = null;
            //company.CompanyItem = null;
            //company.CompanyIntro = null;
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }

        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope( ))
            {
                using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                {
                    if (formView.Save<SysCompany>(0) == 0)
                    {
                        ts.Complete( );
                        lblMessage.InnerHtml = "保存成功!";
                        formView.ClearValue( );
                    }
                    else
                        lblMessage.InnerHtml = "保存失败,请检查错误信息!";
                }
            }
            On_ActionQuery(sender, e);
        }

        void formView_OnValidate(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            App.ValidateCompany(columnName, viewRow);
        }

        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "CompanyCode":
                    using (AreaSelector.Selector1)
                    {
                        AreaSelector.Selector1.DefaultValue = AreaSelector.SelectedArea1;
                        AreaSelector.Selector1.RenderTo = viewRow.ParamName;
                        AreaSelector.Selector1.IsShowLabel = true;
                        viewRow.RenderHtml = string.Concat(AreaSelector.Selector1.RenderResult( ), string.Format(COMPANY_CODE, viewRow.ParamName));
                        AreaSelector.Selector1.IsShowLabel = false;
                    }
                    break;
                case "CompanyMemo":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetRadioHtml(App.TypeFilterDefineList, viewRow.ParamName, viewRow.Value).ToString( );
                    break;
                case "CompanyPwd":
                    viewRow.RenderHtml = string.Format(PASSWORD_INPUT_TEXT, viewRow.ParamName, viewRow.Value);
                    break;
                case "CompanyIntro":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetTextAreaHtml(viewRow.ParamName, viewRow.Value);
                    break;
                case "CompanyContent":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetXheditorHtml(viewRow.ParamName, viewRow.Value);
                    break;
                case "CompanyTel":
                    viewRow.RenderHtml = string.Format(Eleooo.Web.Controls.UcFormView.FORM_VIEW_PHONE_TEMPLATE, viewRow.ParamName, viewRow.Value);
                    break;
                case "CompanyPhoto":
                    viewRow.RenderHtml = string.Format(COMPANY_PHOTO,
                                                    viewRow.ParamName,
                                                    viewRow.Value,
                                                    string.IsNullOrEmpty(viewRow.Value) ? "style='display: none;'" : string.Empty);
                    break;
                case "CompanyRate":
                    viewRow.RenderHtml = string.Concat(viewRow.RenderHtml, "请输入整数,不同的积分比例请使用,号分隔");
                    break;
                case "CompanyRateMaster":
                    viewRow.RenderHtml = string.Concat(viewRow.RenderHtml, "%");
                    break;
                case "CompanyType":
                    viewRow.IsClientValidate = false;
                    viewRow.RenderHtml = string.Format(COMPANY_TYPE, viewRow.ParamName,
                        viewRow.Value == "1" ? "checked='true'" : string.Empty,
                        viewRow.Value == "2" ? "checked='true'" : string.Empty,
                        viewRow.Value == "3" ? "checked='true'" : string.Empty,
                        viewRow.Value == "4" ? "checked='true'" : string.Empty);
                    break;
                case "CompanyFlag":
                    viewRow.IsClientValidate = false;
                    viewRow.RenderHtml = string.Format(COMPANY_FLAG, viewRow.ParamName,
                        viewRow.Value == "1" ? "checked='true'" : string.Empty,
                        viewRow.Value == "2" ? "checked='true'" : string.Empty);
                    break;
                case "CompanyStatus":
                    viewRow.IsClientValidate = false;
                    viewRow.RenderHtml = string.Format(COMPANY_STATUS, viewRow.ParamName,
                        viewRow.Value == "1" ? "checked='true'" : string.Empty,
                        viewRow.Value == "2" ? "checked='true'" : string.Empty);
                    break;
                case "CompanyPic":
                    viewRow.RenderHtml = string.Format(COMPANYPIC_BTN, viewRow.ParamName, viewRow.Value);
                    break;
                case "CompanyAddress":
                    viewRow.RenderHtml = string.Concat(viewRow.RenderHtml, workTimeRow.LblText, workTimeRow.RenderHtml);
                    break;
                case "CompanyPhone":
                    viewRow.RenderHtml = string.Concat(viewRow.RenderHtml, msnPhoneRow.LblText, msnPhoneRow.RenderHtml);
                    break;
                case "OnSetSum":
                    viewRow.RenderHtml = string.Concat(viewRow.RenderHtml,
                                                       servicesSumRow.LblText, servicesSumRow.RenderHtml,
                                                       servicesRow.LblText, servicesRow.RenderHtml);
                    break;
                case "OrderElapsed":
                    viewRow.RenderHtml = string.Concat(viewRow.RenderHtml, orderMaxAmountRow.LblText, orderMaxAmountRow.RenderHtml);
                    break;
                case "IsUseMsg":
                    viewRow.RenderHtml = Uc.HtmlControl.GetBoolRadioHtml(viewRow.ParamName, viewRow.Value).ToString( );
                    break;
            }
        }
    }
}