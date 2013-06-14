using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class MemberFinger : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMessage.InnerHtml = string.Empty;
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            int id = Utilities.ToInt(Params["id"]);
            SysMember member = SysMember.FetchByID(id);
            if (member == null)
                goto lbl_End2;

            txtMemberTel.Text = member.MemberPhoneNumber;
            txtMemberTel.Enabled = false;
            txtMemberPwd.Focus( );
        lbl_End2:
            ResetField( );
        }

        private void ResetField( )
        {
            txtMemberPwd.Text = string.Empty;
            txtMemberTel.Text = string.Empty;
            lblMemberPwdInfo.InnerHtml = string.Empty;
            lblMemberTelInfo.InnerHtml = string.Empty;
            hdnMemberFinger.Value = string.Empty;
            txtMemberTel.Focus( );
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            txtMessage.InnerHtml = "保存失败!";
            if (string.IsNullOrEmpty(hdnMemberFinger.Value))
            {
                lblMemberTelInfo.InnerHtml = "请先读取会员指纹,再提交!";
                return;
            }
            string id = Params["ID"];
            if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(txtMemberTel.Text))
            {
                lblMemberTelInfo.InnerHtml = "会员账号不能为空!";
                txtMemberTel.Enabled = true;
                txtMemberTel.Focus( );
                return;
            }
            SysMember member = null;
            if (!string.IsNullOrEmpty(id))
                member = SysMember.FetchByID(id);
            else
                member = UserBLL.GetUserByPhoneNum(txtMemberTel.Text);
            if (member == null)
            {
                lblMemberTelInfo.InnerHtml = "会员不存在,会员账号是否输入有误?";
                txtMemberTel.Enabled = true;
                txtMemberTel.Focus( );
                return;
            }
            if (string.IsNullOrEmpty(txtMemberPwd.Text))
            {
                lblMemberPwdInfo.InnerHtml = "请输入会员的密码!";
                txtMemberPwd.Focus( );
                return;
            }
            if (!UserBLL.CheckUserPwd(member, txtMemberPwd.Text))
            {
                lblMemberPwdInfo.InnerHtml = "密码输入不正确!";
                return;
            }
            member.MemberFinger = hdnMemberFinger.Value;
            member.Save( );
            txtMessage.InnerHtml = "保存成功!";
            ResetField( );
        }
    }
}