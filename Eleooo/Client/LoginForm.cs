using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Eleooo.DAL;
using System.Deployment.Application;

namespace Eleooo.Client
{
    public partial class LoginForm : DevComponents.DotNetBar.Metro.MetroForm
    {
        private int nCount { get; set; }
        private string VerFormat
        {
            get
            {
                return "<font color=\"#1F497D\">Version</font><font color=\"#ED1C24\"> <b>{0}</b></font>";
            }
        }
        public LoginForm( )
        {
            Icon = Eleooo.Client.Properties.Resources.Eleooo;
            InitializeComponent( );
            nCount = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Close( );
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppContext.User == null &&
                MessageBoxEx.Show("确认要退出吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                e.Cancel = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            nCount++;
            if (nCount == 5)
            {
                MessageBoxEx.Show("你已经试了5次,请退出重试!");
                Close( );
                return;
            }
            LogMessage(string.Empty);
            string userName = txtName.Text.Trim( );
            string userPwd = txtPassword.Text.Trim( );
            if (string.IsNullOrEmpty(userName))
            {
                LogMessage("请输入登录账号!");
                goto label_name;
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                LogMessage("请输入登录密码");
                goto label_pass;
            }
            SysMember user;
            int nRet = 0;
            try
            {
                nRet = ServiceProvider.Service.Login(userName, userPwd, 2, out user);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "登录发生异常");
                LogMessage(ex.Message);
                goto label_name;
            }
            switch (nRet)
            {
                case 1:
                    LogMessage("用户不存在!");
                    goto label_name;
                case 2:
                    LogMessage("登录密码错误!");
                    goto label_pass;
                case 3:
                    LogMessage("你的账号不允许登录到系统!");
                    goto label_name;
                case 4:
                    LogMessage("你的账号处于审核状态!");
                    goto label_name;
            }
            if (user == null)
            {
                LogMessage("获取数据异常,请稍候再试!");
                goto label_name;
            }
            AppContext.User = user;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            return;
        label_pass:
            txtPassword.Focus( );
            return;
        label_name:
            txtName.Focus( );
            return;
        }
        void LogMessage(string message, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                lblMessage.Text = message;
            }
            else
            {
                lblMessage.Text = string.Format(message, args);
            }
        }
        void LogMessage(string message)
        {
            LogMessage(message, null);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            lblVer.Text = string.Format(VerFormat,AppVersion.CurrentVersion);
#if DEBUG
            txtName.Text = "13200000000";
            txtPassword.Text = "123456";
            
#endif
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
