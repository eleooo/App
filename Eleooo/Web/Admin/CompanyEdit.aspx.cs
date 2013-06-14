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
    public partial class CompanyEdit : ActionPage
    {
        #region const html template
        const string PASSWORD_INPUT_TEXT = "<input id='{0}' name='{0}' value='{1}' type='password' />";
        const string TEXTAERA_INPUT_TEXT = "<textarea name='{0}' rows='2' cols='20' id='{0}' class=\"xheditor {{skin:'o2007blue',clickCancelDialog:false,forcePtag:false}}\" style='height:360px;width:100%;'>{1}</textarea>";
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
          <input class='radio' type='radio' name='{0}' value='2' {2} />待审核";
        const string COMPANYPIC_BTN = "<input id='{0}' name='{0}' value='{1}' type='hidden' /><input type='button' value='上传实景图片' id='btnCompanyPic' class='button' />";
        #endregion

        #region fields
        Uc.UcFormView.FormViewRow msnPhoneRow;
        Uc.UcFormView.FormViewRow workTimeRow;
        Uc.UcFormView.FormViewRow servicesRow;
        Uc.UcFormView.FormViewRow servicesSumRow;
        Uc.UcFormView.FormViewRow orderMaxAmountRow;
        #endregion

        string sChangedPwd = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            formView.DataSource = DB.Select( ).From<SysCompany>( )
                                    .Where(SysCompany.IdColumn).IsEqualTo(Utilities.ToInt(Request.Params["id"]));
            formView.AddShowColumn(SysCompany.CompanyCodeColumn, "", true)
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
                    .AddCustomColumn("CompanyPic", "实景图片", Request.Params["id"])
                    .AddShowColumn(SysCompany.CompanyRateMasterColumn)
                    .AddShowColumn(SysCompany.CompanyRateColumn)
                    .AddShowColumn(SysCompany.IsUseMsgColumn) //是否使用短信接口
                    .AddShowColumn(SysCompany.CompanyTypeColumn)
                    .AddCustomColumn("SetTopDate", "是否推荐", "0")
                    .AddShowColumn(SysCompany.IsPointColumn)
                    .AddShowColumn(SysCompany.IsOnSaleColumn)
                    .AddShowColumn(SysCompany.CompanyStatusColumn);

            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnValidate += new Web.Controls.OnFormViewValidate(formView_OnValidate);
            formView.OnAfterSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnAfterSaved);
            formView.OnBeforeSaved += new Uc.OnFormViewSaveHandle(formView_OnBeforeSaved);

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
            sChangedPwd = string.Empty;
        }

        void formView_OnBeforeSaved(object item)
        {
            var company = item as SysCompany;
            if (Request.Form["SetTopDate"] == "1")
            {
                var dt = DateTime.Now;
                company.SetTopDate = dt;
                formView.SetValue("SetTopDate", dt);
            }
            else
            {
                company.SetTopDate = null;
                formView.SetValue("SetTopDate", DBNull.Value);
            }
        }
        void formView_OnAfterSaved(object item)
        {
            SysCompany company = item as SysCompany;
            SysMember user = UserBLL.CompanyToMember(company);
            if (!user.IsNew)
            {
                if (!string.IsNullOrEmpty(sChangedPwd))
                    user.MemberPwd = sChangedPwd;
                user.MemberStatus = company.CompanyStatus;
            }
            user.Save( );

        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }

        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope( ))
            {
                using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                {
                    if (formView.Save<SysCompany>(Utilities.ToInt(Request.Params["id"])) == 0)
                    {
                        ts.Complete( );
                        lblMessage.InnerHtml = "保存成功!";
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
            if (columnName == "CompanyPwd" &&
                !Utilities.Compare(viewRow.DbValue, viewRow.ParamValue))
            {
                string message;
                viewRow.ValidateMessage = string.Empty;
                if (!UserBLL.CheckUserPwd(viewRow.ParamValue, out message))
                {
                    viewRow.ValidateMessage = message;
                    return;
                }
                viewRow.Value = Utilities.DESEncrypt(viewRow.Value);
                sChangedPwd = viewRow.Value;
            }
        }

        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                //case "CompanyCode":
                //    viewRow.RenderHtml = string.Format(DISABLE_INPUT_TEXT, viewRow.ParamName, viewRow.Value);
                //    break;
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
                    viewRow.RenderHtml = string.Format(TEXTAERA_INPUT_TEXT, viewRow.ParamName, viewRow.Value);
                    formView.AddBeforSubmitScript("$('#{0}').val($('#{0}').val());", viewRow.ParamName);
                    break;
                case "CompanyPhoto":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetPhotoHtml(viewRow.ParamName, viewRow.Value);
                    break;
                case "CompanyRateMaster":
                    viewRow.RenderHtml = string.Concat(viewRow.RenderHtml, "%");
                    break;
                case "CompanyRate":
                    viewRow.RenderHtml = string.Concat(viewRow.RenderHtml, "请输入整数,不同的积分比例请使用,号分隔");
                    break;
                case "CompanyType":
                    viewRow.IsClientValidate = false;
                    viewRow.RenderHtml = string.Format(COMPANY_TYPE, viewRow.ParamName,
                        viewRow.Value == "1" ? "checked='checked'" : string.Empty,
                        viewRow.Value == "2" ? "checked='checked'" : string.Empty,
                        viewRow.Value == "3" ? "checked='checked'" : string.Empty,
                        viewRow.Value == "4" ? "checked='checked'" : string.Empty);
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
                case "IsPoint":
                case "IsOnSale":
                case "IsUseMsg":
                    var v =  viewRow.Value;
                    if (v == bool.TrueString)
                        v = "1";
                    else if (v == bool.FalseString)
                        v = "0";
                    viewRow.RenderHtml = Uc.HtmlControl.GetBoolRadioHtml(viewRow.ParamName, v).ToString( );
                    break;
                case "SetTopDate":
                    var topDate = formView.GetValue<DateTime?>("SetTopDate");
                    viewRow.RenderHtml = Uc.HtmlControl.GetBoolRadioHtml(viewRow.ParamName, topDate.HasValue ? "1" : "0").ToString( );
                    break;
            }
        }
    }
}