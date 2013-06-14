using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Transactions;
using SubSonic;

namespace Eleooo.Web.Member
{
    public partial class MyInfo : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMessage.InnerHtml = string.Empty;
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            //formView.DataBind( );
            txtMemberName.Value = CurrentUser.MemberFullname;
            txtEmail.Value = CurrentUser.MemberEmail;
            txtUserPhone.Text = CurrentUser.MemberPhoneNumber;
            if (CurrentUser.MemberSex.HasValue)
            {
                if (CurrentUser.MemberSex.Value)
                    rbSex.Items[0].Selected = true;
                else
                    rbSex.Items[1].Selected = true;
                rbSex.Enabled = false;
            }
            picker.SelectedDate = CurrentUser.MemberBirthday;
            picker.Enabled = !CurrentUser.MemberBirthday.HasValue;
            if (!IsAllowModifyPhone( ))
                txtUserPhone.ReadOnly = true;
        }

        private SysMemberConfig _cfg;
        private SysMemberConfig Cfg
        {
            get
            {
                if (_cfg == null)
                    _cfg = UserBLL.GetUserConfig(CurrentUser.Id);
                return _cfg;
            }
        }
        private bool IsAllowModifyPhone( )
        {
            var d = Cfg.PhoneModifyDate;
            if (!d.HasValue)
                return true;
            var span = DateTime.Today - d.Value;
            return span.TotalDays >= 180;
        }

        private void SaveChangePhone( )
        {
            using (TransactionScope ts = new TransactionScope( ))
            {
                using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                {
                    CompanyBLL.UpdateUserPhone(txtUserPhone.Text, CurrentUser.MemberPhoneNumber);
                    CurrentUser.MemberPhoneNumber = txtUserPhone.Text;
                    CurrentUser.MemberMsnPhone = txtUserPhone.Text;
                    CurrentUser.MemberFullname = txtMemberName.Value;
                    CurrentUser.MemberEmail = txtEmail.Value;
                    if (rbSex.SelectedIndex >= 0)
                        CurrentUser.MemberSex = rbSex.SelectedIndex == 0;
                    CurrentUser.MemberBirthday = picker.GetSelectedDate( );
                    CurrentUser.Save( );
                    Cfg.PhoneModifyDate = DateTime.Now;
                    Cfg.Save( );
                    ts.Complete( );
                    txtMessage.InnerHtml = "保存成功";
                }
            }
            On_ActionQuery(this, EventArgs.Empty);
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (IsAllowModifyPhone( ) && !Common.Utilities.Compare(txtUserPhone.Text, CurrentUser.MemberPhoneNumber))
            {
                if (string.IsNullOrEmpty(txtValidateCode.Value))
                {
                    txtMessage.InnerHtml = "请输入验证码.";
                    return;
                }
                if (string.IsNullOrEmpty(hdfMsnId.Value))
                {
                    txtMessage.InnerHtml = "请获取验证码.";
                    return;
                }
                if (!MsnBLL.CheckPhoneNumCode(txtUserPhone.Text, txtValidateCode.Value, Common.Utilities.ToInt(hdfMsnId.Value)))
                {
                    txtMessage.InnerHtml = "你输入的验证码不正确，或者验证码已经过期，请重新验证。";
                    return;
                }
                string message;
                if (!UserBLL.CheckUserName(txtUserPhone.Text, out message))
                {
                    txtMessage.InnerHtml = message;
                    return;
                }
                if (UserBLL.CheckUserExist(txtUserPhone.Text))
                {
                    txtMessage.InnerHtml = "此账号已经是乐多分会员。";
                    return;
                }
                SaveChangePhone( );
            }
            else
            {
                CurrentUser.MemberFullname = txtMemberName.Value;
                CurrentUser.MemberEmail = txtEmail.Value;
                if (rbSex.SelectedIndex >= 0)
                    CurrentUser.MemberSex = rbSex.SelectedIndex == 0;
                CurrentUser.MemberBirthday = picker.GetSelectedDate( );
                CurrentUser.Save( );
                rbSex.Enabled = !CurrentUser.MemberSex.HasValue;
                picker.Enabled = !CurrentUser.MemberBirthday.HasValue;
                txtMessage.InnerHtml = "保存成功";
            }
        }
    }
}