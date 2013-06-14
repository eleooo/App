using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class MyPwd : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMessage.InnerHtml = string.Empty;
        }

        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (Utilities.IsNull(txtCompanyPwd.Text))
            {
                txtMessage.InnerHtml = "请输入原来的密码!";
                return;
            }
            if (!(Utilities.Compare(txtCompanyPwd.Text, CurrentUser.MemberPwd) ||
                Utilities.Compare(Utilities.DESEncrypt(txtCompanyPwd.Text), CurrentUser.MemberPwd)))
            {
                txtMessage.InnerHtml = "你输入的原始密码不正确!";
                return;
            }
            string message;
            if (!UserBLL.CheckUserPwd(txtCompanyPwd1.Text, out message))
            {
                txtMessage.InnerHtml = message;
                return;
            }

            if (!Utilities.Compare(txtCompanyPwd1.Text, txtCompanyPwd2.Text))
            {
                txtMessage.InnerHtml = "你两次输入的密码不一致!";
                return;
            }

            if (AppContext.Context.Company.DirtyColumns.Count > 0)
                AppContext.Context.Company.MarkClean( );
            AppContext.Context.Company.MarkOld( );
            if (CurrentUser.DirtyColumns.Count > 0)
                CurrentUser.MarkClean( );
            CurrentUser.MarkOld( );
            CurrentUser.MemberPwd = Utilities.DESEncrypt(txtCompanyPwd1.Text);
            AppContext.Context.Company.CompanyPwd = CurrentUser.MemberPwd;
            CurrentUser.Save( );
            AppContext.Context.Company.Save( );
            txtMessage.InnerHtml = "保存成功!";
        }
    }
}