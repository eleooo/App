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
    public partial class MyInfo : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMessage.InnerHtml = string.Empty;
            formView.DataSource = DB.Select( ).From<SysCompany>( )
                                    .Where(SysCompany.IdColumn).IsEqualTo(CurrentUser.CompanyId);
            formView.AddShowColumn(SysCompany.CompanyCodeColumn, "", true)
                    .AddShowColumn(SysCompany.CompanyNameColumn, "", true)
                    .AddShowColumn(SysCompany.CompanyTelColumn, "", true)
                    .AddShowColumn(SysCompany.CompanyPhoneColumn)
                    .AddShowColumn(SysCompany.CompanyEmailColumn)
                    .AddShowColumn(SysCompany.CompanyAddressColumn)
                    .AddShowColumn(SysCompany.CompanyIntroColumn)
                    .AddShowColumn(SysCompany.CompanyMemoColumn,"",true)
                    .AddShowColumn(SysCompany.CompanyItemColumn)
                    .AddShowColumn(SysCompany.CompanyWorkTimeColumn)
                    .AddShowColumn(SysCompany.CompanyServicesColumn)
                    //.AddShowColumn(SysCompany.CompanyRateSaleColumn, "", true)
                    .AddShowColumn(SysCompany.CompanyRateColumn, "", true)
                    .AddShowColumn(SysCompany.CompanySaleSumColumn, "", true)
                    .AddShowColumn(SysCompany.CompanyBalanceCashColumn, "", true)
                    .AddShowColumn(SysCompany.CompanyBalanceColumn, "", true)
                    .AddShowColumn(SysCompany.CompanyRateMasterColumn, "", true);
                    //.AddShowColumn(SysCompany.CompanyTypeColumn)
                    //.AddShowColumn(SysCompany.CompanyFlagColumn);
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnAfterSaved += new OnFormViewSaveHandle(formView_OnAfterSaved);
            formView.OnValidate += new OnFormViewValidate(formView_OnValidate);
        }

        void formView_OnAfterSaved(object item)
        {
            SysCompany company = item as SysCompany;
            SysMember user = UserBLL.CompanyToMember(company);
            if (user != null)
                user.Save( );
        }

        void formView_OnValidate(string columnName, UcFormView.FormViewRow viewRow)
        {
            
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
                    if (formView.Save<SysCompany>(CurrentUser.CompanyId) == 0)
                    {
                        ts.Complete( );
                        txtMessage.InnerHtml = "保存成功!";
                    }
                    else
                        txtMessage.InnerHtml = "保存失败!";
                }
            }
            On_ActionQuery(sender, e);
            
        }
        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "CompanyEmail":
                case "CompanyPhone":
                    break;
                //case "CompanyMemo":
                //    viewRow.RenderHtml = string.Format("{0}&nbsp;如:体检,健身,美容,美发,KTV", viewRow.RenderHtml);
                //    break;
                case "CompanyRateMaster":
                    viewRow.RenderHtml = string.Format(UcFormView.FORM_VIEW_TEXT_DISABLED_TEMPATE, viewRow.ParamName, Utilities.ToDecimal(viewRow.DbValue).ToString("####.###") + "%");
                    break;
                case "CompanyIntro":
                    viewRow.RenderHtml = Eleooo.Web.Controls.HtmlControl.GetTextAreaHtml(viewRow.ParamName, viewRow.Value);
                    break;
                //default:
                //    viewRow.RenderHtml = string.Format(UcFormView.FORM_VIEW_TEXT_DISABLED_TEMPATE, string.Empty, viewRow.Value);
                //    break;
            }
        }
    }
}