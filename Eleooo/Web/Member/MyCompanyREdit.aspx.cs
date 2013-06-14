using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class MyCompanyREdit : ActionPage
    {
        const string HEADER_TEMPLATE = "<table width='100%' cellspacing='0' cellpadding='0' class='tjTb'><tbody>";
        const string ROW_TEMPLATE = "<tr><th>{0}</th><td>{1}{2}</td></tr>";
        const string FOOTER_TEMPLATE = @"
                <tr>
                    <td style='text-align: right;' colspan='2'>
		            {0}
                    {1}	
                    </td>
                </tr></tbody></table>";
        const string SAVE_TEMPLATE = "<input type='button' class='tj_Btn tj_Btn_Ok' OnClick='__doPostBack({0});' id='btnSubmit' value='确认'/>";
        const string CALCEL_TEMPLATE = "<input type='button' class='tj_Btn tj_Btn_Cancel' onclick='__onBtnCloseClick();' id='btnClose' value='取消'/>";
        const string INPUT_TEMPLATE = "<input type=\"text\" class=\"tj_txt\" name=\"{0}\" value=\"{1}\" />";
        const string TEXTAREA_INPUT = "<textarea name=\"{0}\" id=\"{0}\" class=\"tj_tarea\" rows=\"2\" cols=\"20\">{1}</textarea>";
        const string PHONE_TEMPLATE = "<input id='{0}' name='{0}' value='{1}' type='text' class=\"tj_txt\" />";
        const string ADDR_TEMPLATE = "<input id='{0}' name='{0}' value='{1}' type='text' class=\"tj_txt\" style='width:250px;' />";
        protected void Page_Load(object sender, EventArgs e)
        {
            formView.HeaderTemplate = HEADER_TEMPLATE;
            formView.RowTemplate = ROW_TEMPLATE;
            formView.FooterTemplate = FOOTER_TEMPLATE;
            formView.SaveTemplate = SAVE_TEMPLATE;
            formView.CancelTemplate = CALCEL_TEMPLATE;

            formView.DataSource = DB.Select( ).From<SysMemberCompanyR>( )
                                    .Where(SysMemberCompanyR.IdColumn).IsEqualTo(Utilities.ToInt(Request.Params["ID"]));
            formView.AddShowColumn(SysMemberCompanyR.CompanyNameColumn)
                    .AddShowColumn(SysMemberCompanyR.CompanyTelColumn)
                    .AddShowColumn(SysMemberCompanyR.CompanyAddressColumn)
                    .AddShowColumn(SysMemberCompanyR.CompanyDescColumn);
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnBeforeSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnBeforeSaved);
            formView.OnValidate += new Web.Controls.OnFormViewValidate(formView_OnValidate);
            txtMessage.InnerHtml = string.Empty;
        }

        void formView_OnValidate(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            if (columnName == SysMemberCompanyR.Columns.CompanyName && string.IsNullOrEmpty(viewRow.Value))
            {
                viewRow.ValidateMessage = "请输入商家的名称";
                return;
            }
            else if (columnName == "CompanyTel")
            {
                if (string.IsNullOrEmpty(viewRow.Value))
                {
                    viewRow.ValidateMessage = "请输入商家的联系电话";
                    return;
                }
                //string message;
                //UserBLL.CheckUserName(viewRow.Value, out message);
                //if (!string.IsNullOrEmpty(message))
                //{
                //    viewRow.ValidateMessage = message;
                //    return;
                //}
                if (!string.IsNullOrEmpty(Request.Params["ID"]) && UserBLL.CheckUserExist(viewRow.Value))
                    viewRow.ValidateMessage = "此电话已经存在!";
                else if (viewRow.DbValue != viewRow.ParamValue &&
                    UserBLL.CheckUserExist(viewRow.ParamValue))
                    viewRow.ValidateMessage = "此电话已经存在!";
            }
            else if (columnName == SysMemberCompanyR.Columns.CompanyAddress && string.IsNullOrEmpty(viewRow.Value))
            {
                viewRow.ValidateMessage = "请输入商家的联系地址";
                return;
            }
        }

        void formView_OnBeforeSaved(object item)
        {
            SysMemberCompanyR company = item as SysMemberCompanyR;
            if (company.IsNew)
            {
                company.CompanyMemberID = CurrentUser.Id;
                company.CompanyDate = DateTime.Now;
                company.CompanyStatus = (int)CompanyRStatus.InProgress;
            }
        }

        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            if (Utilities.Compare(columnName, "CompanyDesc"))
                viewRow.RenderHtml = string.Format(TEXTAREA_INPUT, viewRow.ParamName, viewRow.Value);
            else if (columnName == "CompanyTel")
                viewRow.RenderHtml = string.Format(PHONE_TEMPLATE, viewRow.ParamName, viewRow.Value);
            else if (columnName == "CompanyAddress")
                viewRow.RenderHtml = string.Format(ADDR_TEMPLATE, viewRow.ParamName, viewRow.Value);
            else
                viewRow.RenderHtml = string.Format(INPUT_TEMPLATE, viewRow.ParamName, viewRow.Value);
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }

        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (formView.Save<SysMemberCompanyR>(Utilities.ToInt(Request.Params["ID"])) == 0)
            {
                txtMessage.InnerHtml = "保存成功!";
                formView.ClearValue( );
            }
            else
                txtMessage.InnerHtml = "保存失败";
            On_ActionQuery(sender, e);
        }

        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (formView.Save<SysMemberCompanyR>(Utilities.ToInt(Request.Params["ID"])) == 0)
                txtMessage.InnerHtml = "保存成功!";
            else
                txtMessage.InnerHtml = "保存失败";
            On_ActionQuery(sender, e);
        }
    }
}