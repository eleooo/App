using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Eleooo.Client
{
    public partial class UcPwdInfo : UserControlBase
    {
        public UcPwdInfo( )
        {
            InitializeComponent( );
        }
        public override void SetFoucs( )
        {
            txtPwd.Focus( );
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(txtPwd.Text))
            {
                message = "请输入原始密码!";
                goto lbl_pwd;
            }
            string pwd = txtPwd1.Text.Trim( );
            if (string.IsNullOrEmpty(pwd))
            {
                message = "请输入新密码!";
                goto lbl_pwd1;
            }
            if (string.IsNullOrEmpty(txtPwd2.Text))
            {
                message = "请输入新密码的确认密码";
                goto lbl_pwd2;
            }
            if (pwd.Length < 6 || pwd.Length > 6 || !Utilities.IsNumeric(pwd))
            {
                message = "请使用6位数字做登录密码!";
                goto lbl_pwd1;
            }
            if (!UserBLL.CheckUserPwd(AppContext.User, txtPwd.Text))
            {
                message = "输入的原始密码不正确!";
                goto lbl_pwd;
            }
            if (!Utilities.Compare(pwd, txtPwd2.Text))
            {
                message = "新密码和确认密码输入不一样!";
                goto lbl_pwd1;
            }
            if (UserBLL.CheckUserPwd(AppContext.User, pwd))
            {
                message = "新密码与旧密码一样";
                goto lbl_pwd;
            }
            if (AppContext.User.DirtyColumns.Count > 0)
                AppContext.User.MarkClean( );
            if (AppContext.Company.DirtyColumns.Count > 0)
                AppContext.Company.MarkClean( );
            AppContext.Company.MarkOld( );
            AppContext.User.MarkOld( );
            AppContext.User.MemberPwd = Utilities.DESEncrypt(pwd);
            AppContext.Company.CompanyPwd = AppContext.User.MemberPwd;
            if (ServiceProvider.Service.SaveEntity<DAL.SysMember>(AppContext.User) > 0)
            {
                ServiceProvider.Service.SaveEntity<DAL.SysCompany>(AppContext.Company);
                message = "保存成功";
            }
            else
                message = "保存失败!";
            btnCancle_Click(sender, e);
            goto lbl_message;
        lbl_pwd:
            txtPwd.Focus( );
            goto lbl_message;
        lbl_pwd1:
            txtPwd1.Focus( );
            goto lbl_message;
        lbl_pwd2:
            txtPwd2.Focus( );
        lbl_message:
            MessageBoxEx.Show(message);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            txtPwd.Text = string.Empty;
            txtPwd1.Text = string.Empty;
            txtPwd2.Text = string.Empty;
            txtPwd.Focus( );
        }
    }
}
