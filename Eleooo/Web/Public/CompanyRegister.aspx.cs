using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Public
{
    public partial class CompanyRegister : ActionPage
    {
        #region const html template
        const string DISABLE_INPUT_TEXT = "<input name='{0}' type='text' value='{1}' id='{0}' disabled='disabled' />";
        const string PASSWORD_INPUT_TEXT = "<input id='{0}' name='{0}' value='{1}' type='password' />&nbsp;&nbsp;确认密码:<input id='{0}_1' name='{0}_1' value='{1}' type='password' />";
        const string TEXTAERA_INPUT_TEXT = "<textarea name='{0}' rows='2' cols='20' id='{0}' class=\"xheditor {{skin:'o2007blue',clickCancelDialog:false,forcePtag:false}}\" style='height:360px;width:100%;'>{1}</textarea>";
        const string COMPANY_PHOTO =
        @" <input type='button' value='上传图片' id='btnUpload' class='button' />
           <br />
           <img id='{0}_img' src='{1}' width='300px' class='imgUpload' {2} />";
        const string COMPANY_TYPE =
        @"<input class='radio' type='radio' name='{0}' value='1' {1} />社区商家
          <input class='radio' type='radio' name='{0}' value='2' {2} />热点商家";
        const string COMPANY_FLAG =
        @"<input class='radio' type='radio' name='{0}' value='1' {1} />积分
          <input class='radio' type='radio' name='{0}' value='2' {2} />全部";
        const string COMPANY_STATUS =
        @"<input class='radio' type='radio' name='{0}' value='1' {1} />正常
          <input class='radio' type='radio' name='{0}' value='2' {2} />停用";
        const string COMPANY_CODE = "<span id='{0}' style='color:red;'></span>";
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
                    .AddShowColumn(SysCompany.CompanyMemoColumn);
            //.AddShowColumn(SysCompany.CompanyContentColumn)
            //.AddShowColumn(SysCompany.CompanyIntroColumn);
            //.AddShowColumn(SysCompany.CompanyPhotoColumn)
            //.AddShowColumn(SysCompany.CompanyItemColumn)
            //.AddShowColumn(SysCompany.CompanyRateMasterColumn)
            //.AddShowColumn(SysCompany.CompanyRateColumn)
            //.AddShowColumn(SysCompany.CompanyTypeColumn)
            //.AddShowColumn(SysCompany.CompanyFlagColumn)
            //.AddShowColumn(SysCompany.CompanyStatusColumn);
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnValidate += new Web.Controls.OnFormViewValidate(formView_OnValidate);
            formView.OnBeforeSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnBeforeSaved);
            formView.OnAfterSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnAfterSaved);
            lblMessage.InnerHtml = string.Empty;
        }
        void formView_OnAfterSaved(object item)
        {
            SysCompany company = item as SysCompany;
            SysMember user = UserBLL.CompanyToMember(company);
            user.Save( );
        }
        void formView_OnBeforeSaved(object item)
        {
            SysCompany company = item as SysCompany;
            company.CompanyPwd = Utilities.DESEncrypt(company.CompanyPwd);
            company.AreaDepth = AreaSelector.SelectedArea1;
            company.CompanyCity = Utilities.ToInt(AreaSelector.Selector1.GetSelectedValue(0));
            company.CompanyArea = AreaSelector.GetSelectedLocation2( );
            company.CompanyLocation = AreaSelector.GetSelectedLocation3( );
            company.CompanyItem = null;
            company.CompanyMemo = string.Empty;
            company.CompanyBalance = 0;
            company.CompanyBalanceCash = 0;
            company.CompanyContent = null;
            company.CompanyDate = DateTime.Now;
            company.CompanyDateView = DateTime.Now;
            company.CompanyFacebookCount = 0;
            company.CompanyMsn = string.Empty;
            company.CompanyPhoto = null;
            company.CompanyProvince = null;
            company.CompanyRate = null;
            company.CompanyRateMaster = null;
            company.CompanyRateSale = null;
            company.CompanySaleCount = 0;
            company.CompanySaleSum = 0;
            company.CompanySkype = string.Empty;
            company.CompanyStatus = 1;
            company.CompanyToken = string.Empty;
            company.CompanyType = 1;
            company.IsUseFinger = true;
        }

        void formView_OnValidate(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            App.ValidateCompany(columnName, viewRow);
        }

        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "CompanyMemo":
                    viewRow.RenderHtml = string.Format("{0}&nbsp;如:体检,健身,美容,美发,KTV", viewRow.RenderHtml);
                    break;
                case "CompanyCode":
                    AreaSelector.Selector1.DefaultValue = AreaSelector.SelectedArea1;
                    AreaSelector.Selector1.RenderTo = viewRow.ParamName;
                    viewRow.RenderHtml = string.Concat(AreaSelector.Selector1.RenderResult( ), string.Format(COMPANY_CODE, viewRow.ParamName));
                    break;
                case "CompanyPwd":
                    viewRow.RenderHtml = string.Format(PASSWORD_INPUT_TEXT, viewRow.ParamName, viewRow.Value);
                    break;
                case "CompanyContent":
                    viewRow.RenderHtml = string.Format(TEXTAERA_INPUT_TEXT, viewRow.ParamName, viewRow.Value);
                    break;
                case "CompanyPhoto":
                    viewRow.RenderHtml = string.Concat(viewRow.RenderHtml,
                        string.Format(COMPANY_PHOTO,
                        viewRow.ParamName,
                        viewRow.Value,
                        string.IsNullOrEmpty(viewRow.Value) ? "style='display: none;'" : string.Empty
                        ));
                    break;
                case "CompanyRateMaster":
                    viewRow.RenderHtml = string.Concat(viewRow.RenderHtml, "%");
                    break;
                case "CompanyType":
                    viewRow.IsClientValidate = false;
                    viewRow.RenderHtml = string.Format(COMPANY_TYPE, viewRow.ParamName,
                        viewRow.Value == "1" ? "checked='checked'" : string.Empty,
                        viewRow.Value == "2" ? "checked='checked'" : string.Empty);
                    break;
                case "CompanyFlag":
                    viewRow.IsClientValidate = false;
                    viewRow.RenderHtml = string.Format(COMPANY_FLAG, viewRow.ParamName,
                        viewRow.Value == "1" ? "checked='checked'" : string.Empty,
                        viewRow.Value == "2" ? "checked='checked'" : string.Empty);
                    break;
                case "CompanyStatus":
                    viewRow.IsClientValidate = false;
                    viewRow.RenderHtml = string.Format(COMPANY_STATUS, viewRow.ParamName,
                        viewRow.Value == "1" ? "checked='checked'" : string.Empty,
                        viewRow.Value == "2" ? "checked='checked'" : string.Empty);
                    break;
            }
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (formView.Save<SysCompany>(0) == 0)
            {
                Utilities.ShowMessageRedirect("注册成功,你现在可以登录系统了!", "/default.aspx");
            }
            else
            {
                PrintMessage("注册失败,请检查错误信息!");
                On_ActionQuery(sender, e);
            }
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }
        void PrintMessage(string message)
        {
            lblMessage.InnerHtml = string.Concat(lblMessage.InnerHtml, message, "<br />");
        }
    }
}