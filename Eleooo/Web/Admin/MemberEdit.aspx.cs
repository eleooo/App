using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Transactions;
using SubSonic;
using Eleooo.Web.Controls;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class MemberEdit : ActionPage
    {
        #region const html template
        const string PASSWORD_INPUT_TEXT = "<input id='{0}' name='{0}' value='{1}' type='password' />";
        const string DISABLE_INPUT_TEXT = "<input name='{0}' type='text' value='{1}' id='{0}' disabled='disabled' />";
        const string MEMBER_STATUS =
        @"<input class='radio' type='radio' name='{0}' value='1' {1} />正常
          <input class='radio' type='radio' name='{0}' value='2' {2} />审核";
        #endregion
        bool isChangePhoneNum = false;
        int UserID
        {
            get
            {
                return Utilities.ToInt(Request.Params["id"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            formView.DataSource = DB.Select( ).From<SysMember>( )
                                    .Where(SysMember.CompanyIdColumn).IsEqualTo(0)
                                    .And(SysMember.IdColumn).IsEqualTo(UserID);
            formView.AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysMember.MemberEmailColumn)
                    .AddShowColumn(SysMember.MemberPwdColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(SysMember.MemberMemoColumn)
                    .AddShowColumn(SysMember.MemberSumColumn, "0")
                    .AddShowColumn(SysMember.MemberBalanceColumn, "0")
                    .AddShowColumn(SysMember.MemberBalanceCashColumn, "0")
                    .AddShowColumn(SysMember.MemberStatusColumn, "1");
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnValidate += new Web.Controls.OnFormViewValidate(formView_OnValidate);
            lblMessage.InnerHtml = string.Empty;
        }
        
        void formView_OnValidate(string columnName, Controls.UcFormView.FormViewRow viewRow)
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

        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "MemberPwd":
                    viewRow.RenderHtml = string.Format(PASSWORD_INPUT_TEXT, viewRow.ParamName, viewRow.Value);
                    break;
                case "MemberSum":
                    viewRow.IsDisabled = true;
                    viewRow.RenderHtml = string.Format(DISABLE_INPUT_TEXT, viewRow.ParamName, viewRow.Value);
                    break;
                case "MemberBalance":
                    viewRow.IsDisabled = true;
                    viewRow.RenderHtml = string.Format(DISABLE_INPUT_TEXT, viewRow.ParamName, viewRow.Value);
                    break;
                case "MemberBalanceCash":
                    viewRow.IsDisabled = true;
                    viewRow.RenderHtml = string.Format(DISABLE_INPUT_TEXT, viewRow.ParamName, viewRow.Value);
                    break;
                case "MemberStatus":
                    viewRow.IsClientValidate = false;
                    viewRow.RenderHtml = string.Format(MEMBER_STATUS, viewRow.ParamName,
                        viewRow.Value == "1" ? "checked='checked'" : string.Empty,
                        viewRow.Value == "2" ? "checked='checked'" : string.Empty);
                    break;
            }
        }

        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope( ))
            {
                using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                {
                    if (formView.Save<SysMember>(UserID) == 0)
                    {
                        if (isChangePhoneNum)
                        {
                            UcFormView.FormViewRow phone = formView.GetViewRow(SysMember.MemberPhoneNumberColumn);
                            CompanyBLL.UpdateUserPhone(phone.ParamValue, phone.DbValue);
                        }
                        ts.Complete( );
                        lblMessage.InnerHtml = "保存成功!";
                    }
                    else
                        lblMessage.InnerHtml = "保存失败";
                }
            }
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }
    }
}