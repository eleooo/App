using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Public
{
    public partial class Sign : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //using (AreaBLL.Selector)
            {
                AreaSelector.Selector1.DefaultValue = AreaSelector.SelectedArea1;
                AreaSelector.Selector1.RenderTo = string.Empty;
                areaContainer.InnerHtml = AreaSelector.Selector1.RenderResult( ).ToString( );
            }
            lblMessage.Text = string.Empty;
        }
        protected void btnReg_Click(object sender, EventArgs e)
        {
            if (this.CheckData( ))
            {
                try
                {
                    this.SaveData( );
                }
                catch (Exception exception)
                {
                    Logging.Log("Sign->btnReg_Click", exception);
                    this.lblMessage.Text = "保存失败，请与管理员联系！" + exception.Message;
                }
            }
        }

        protected void SaveData( )
        {
            new SysMember
            {
                MemberAddress1 = string.Empty,
                MemberAddress2 = string.Empty,
                MemberBalance = 0,
                MemberBalanceCash = 0,
                MemberBirthday = null,
                MemberCompanyID = UserBLL.MainCompanyAccount.Id,
                MemberCountry = null,
                MemberDate = DateTime.Now,
                MemberEmail = txtMemberEmail.Text,
                MemberFinger = null,
                MemberFullname = txtMemberName.Text,
                MemberGrade = 0,
                MemberMemo = null,
                MemberPhoneNumber = txtMemberPhone.Text,
                MemberPid = 0,
                MemberPwd = Utilities.DESEncrypt(txtMemberPwd1.Text),
                MemberRoleId = UserBLL.GetDefaultUseRole((int)SubSystem.Member),
                MemberSex = Convert.ToBoolean(lblSex.SelectedValue),
                MemberState = null,
                MemberStatus = 1,
                MemberSum = 0,
                MemberZip = null,
                LastLoginDate = null,
                LastLoginSubSys = 0,
                AdminRoleId = 0,
                CompanyId = 0,
                CompanyRoleId = 0,
                AreaDepth1 = AreaSelector.SelectedArea1,
                AreaDepth2 = AreaSelector.SelectedArea2,
                AreaDepth3 = null,
                AreaModifyDate = null,
                MemberCity = Utilities.ToInt(AreaSelector.Selector1.GetSelectedValue(0)),
                MemberArea = AreaSelector.GetSelectedLocation2( ),
                MemberLocation = AreaSelector.GetSelectedLocation3( )
            }.Save(0);
            txtMemberName.Text = string.Empty;
            txtMemberPhone.Text = string.Empty;
            txtMemberPwd1.Text = string.Empty;
            txtMemberPwd2.Text = string.Empty;
            lblSex.SelectedValue = Boolean.TrueString;
            txtMemberEmail.Text = string.Empty;
            this.lblMessage.Text = "恭喜您，注册成功！，现在你可以登录系统了!";
        }

        protected bool CheckData( )
        {
            string message;
            if (string.IsNullOrEmpty(AreaSelector.SelectedArea1))
                PrintMessage("请选择你所在的商圈");
            if (!UserBLL.CheckUserName(this.txtMemberPhone.Text, out message))
                PrintMessage(message);

            if (!UserBLL.CheckUserPwd(this.txtMemberPwd1.Text, out message))
                PrintMessage(message);

            if (string.Compare(this.txtMemberPwd1.Text, this.txtMemberPwd2.Text) != 0)
                PrintMessage("两次输入的密码不同,请重新输入!");

            if (!SubSonic.Sugar.Validation.IsEmail(txtMemberEmail.Text))
                PrintMessage("电子邮箱的格式不正确!");

            if (string.IsNullOrEmpty(txtMemberName.Text))
                PrintMessage("会员名不能为空!");

            if (UserBLL.CheckUserExist(txtMemberPhone.Text))
                PrintMessage("该手机已经注册");

            if (string.IsNullOrEmpty(lblMessage.Text))
                goto label_true;
            return false;
        label_true:
            return true;
        }
        void PrintMessage(string message)
        {
            lblMessage.Text = string.Concat(lblMessage.Text, message, "<br />");
        }
    }
}