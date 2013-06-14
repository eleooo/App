using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Web.Controls;
using System.Transactions;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class MemberEdit : ActionPage
    {
        int UserID
        {
            get
            {
                return Utilities.ToInt(Request.Params["id"]);
            }
        }
        bool isChangePhoneNum = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppContext.Context.CompanyType == CompanyType.MealCompany)
            {
                txtMessage.InnerHtml = "阁下的商家类型无权使用此功能";
                return;
            }
            bool isMyUser = CompanyBLL.CheckIsOwnerUser(UserID, CurrentUser.CompanyId.Value);
            formView.DataSource = DB.Select( ).From<SysMember>( ).Where(SysMember.IdColumn).IsEqualTo(UserID);
            formView.IsAllowSave = isMyUser;
            formView.AddShowColumn(SysMember.MemberFullnameColumn,"",true)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(SysMember.MemberEmailColumn)
                    .AddShowColumn(SysMember.MemberSexColumn)
                    .AddShowColumn(SysMember.MemberBirthdayColumn)
                    .AddShowColumn(SysMember.MemberAddress1Column);
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnValidate += new OnFormViewValidate(formView_OnValidate);
            txtMessage.InnerHtml = string.Empty;
        }

        void formView_OnValidate(string columnName, UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                //case "MemberFullname":
                //    if (string.IsNullOrEmpty(viewRow.Value))
                //        viewRow.ValidateMessage = "会员名称不能为空!";
                //    break;
                case "MemberPhoneNumber":
                    if (string.IsNullOrEmpty(viewRow.Value))
                    {
                        viewRow.ValidateMessage = "会员账号不能为空!";
                        break;
                    }
                    if (!SubSonic.Sugar.Validation.IsNumeric(viewRow.Value))
                    {
                        viewRow.ValidateMessage = "登录账号不合法,请使用手机号码作为登录账号!";
                        break;
                    }
                    if (viewRow.Value.Length != 11)
                    {
                        viewRow.ValidateMessage = "你的登录账号不是11位数字,请使用手机号码作为登录账号!";
                        break;
                    }
                    if (!Utilities.Compare(viewRow.ParamValue, viewRow.DbValue))
                    {
                        if (UserBLL.CheckUserExist(viewRow.ParamValue))
                        {
                            viewRow.ValidateMessage = "你输入的会员账号已经存在!";
                            break;
                        }
                        isChangePhoneNum = true;
                    }
                    break;
            }
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (!CompanyBLL.CheckIsOwnerUser(UserID, CurrentUser.CompanyId.Value))
            {
                txtMessage.InnerHtml = "你无权编辑非本店会员";
                goto lbl_end;
            }
            if (UserID == CurrentUser.Id)
            {
                txtMessage.InnerHtml = "此账号为系统内置账号,不能修改.";
                goto lbl_end;
            }
            using (TransactionScope ts = new TransactionScope( ))
            {
                using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                {
                    if (formView.Save<SysMember>(UserID) == 0)
                    {
                        if(isChangePhoneNum)
                        {
                            UcFormView.FormViewRow phone = formView.GetViewRow(SysMember.MemberPhoneNumberColumn);
                            CompanyBLL.UpdateUserPhone(phone.ParamValue, phone.DbValue);
                        }
                        ts.Complete( );
                        txtMessage.InnerHtml = "保存成功!";
                    }
                    else
                        txtMessage.InnerHtml = "保存失败";
                }
            }
        lbl_end:
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }
        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                //case "MemberPhoneNumber":
                //    if (!formView.IsAllowSave)
                //        viewRow.RenderHtml = string.Format(UcFormView.FORM_VIEW_TEXT_DISABLED_TEMPATE, viewRow.ParamName, CompanyBLL.EnUserPhoe(viewRow.Value));
                //    break;
                case "MemberSex":
                    //viewRow.RenderHtml = string.Format(INPUT_SEX, viewRow.ParamName,
                    //    Utilities.Compare(viewRow.Value, "true") ? "checked=\"checked\"" : string.Empty,
                    //    Utilities.Compare(viewRow.Value, "false") ? "checked=\"checked\"" : string.Empty);
                    viewRow.RenderHtml = App.GetSexRadio(viewRow.ParamName, viewRow.Value);
                    break;
                case "MemberBirthday":
                    DateTime dtNow;
                    if (!DateTime.TryParse(viewRow.Value, out dtNow))
                        dtNow = DateTime.Now;
                    viewRow.RenderHtml = string.Concat(DateHtmlSelect.GetDateSelectCtrl(dtNow.AddYears(-150), dtNow, dtNow, viewRow.ParamName),
                                          string.Format(UcFormView.FORM_VIEW_HIDDEN_TEMPLATE, viewRow.ParamName, viewRow.Value));
                    break;
            }
        }
    }
}