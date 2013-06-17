using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Web.Controls;
using Eleooo.Common;

namespace Eleooo.Web.Public
{
    public partial class Register : ActionPage
    {
        private SysMember _rUser;
        public SysMember RUser
        {
            get
            {
                int uid = Utilities.ToInt(Request.Params["uid"]);
                if (_rUser == null && uid > 0)
                    _rUser = SysMember.FetchByID(uid);
                return _rUser;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //using (AreaBLL.Selector)
            //{
            //    AreaSelector.Selector1.DefaultValue = AreaSelector.SelectedArea1;
            //    AreaSelector.Selector1.RenderTo = string.Empty;
            //    areaContainer.InnerHtml = AreaSelector.Selector1.RenderResult( ).ToString( );
            //}
            if (!IsPostBack && RUser != null)
            {
                txtMyFriend.Text = RUser.MemberPhoneNumber;
                txtMyFriend.Enabled = false;
            }
            lblRMember.InnerHtml = this.RUser == null ? "选填,请输入推荐人的手机号码" : this.RUser.MemberFullname;
        }

        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (this.CheckData())
            {
                bool isSucc = false;
                try
                {
                    isSucc = SaveData();
                }
                catch (Exception exception)
                {
                    Logging.Log("Register->btnReg_Click", exception);
                    this.lblPhoneInfo.InnerHtml = "保存失败，请与管理员联系！" + exception.Message;
                }
                if (isSucc)
                    Utilities.ShowMessageRedirect("注册成功^_^", "/Public/OrderMealPage.aspx");
            }
        }
        private SysMember GetRUser()
        {
            if (RUser != null)
                return RUser;
            else
                return UserBLL.GetUserByPhoneNum(txtMyFriend.Text);
        }
        protected bool SaveData()
        {
            SysMember rUser = GetRUser();
            int pid = rUser != null ? rUser.Id : 0;
            SysMember user = new SysMember
            {
                MemberAddress1 = string.Empty,
                MemberAddress2 = string.Empty,
                MemberBalance = 0,
                MemberBalanceCash = 0,
                MemberBirthday = null,
                MemberCompanyID = UserBLL.MainCompanyAccount.Id,
                MemberCountry = null,
                MemberDate = DateTime.Now,
                MemberEmail = null,
                MemberFinger = null,
                MemberFullname = null,
                MemberGrade = 0,
                MemberMemo = null,
                MemberPhoneNumber = txtMemberPhone.Text.Trim(),
                MemberPid = pid,
                MemberPwd = Utilities.DESEncrypt(txtMemberPwd1.Text.Trim()),
                MemberRoleId = UserBLL.GetDefaultUseRole((int)SubSystem.Member),
                MemberSex = null,
                MemberState = null,
                MemberStatus = 1,
                MemberSum = 0,
                MemberZip = null,
                LastLoginDate = null,
                LastLoginSubSys = 0,
                AdminRoleId = 0,
                CompanyId = 0,
                CompanyRoleId = 0,
                AreaDepth1 = null,
                AreaDepth2 = null,
                AreaDepth3 = null,
                AreaModifyDate = null,
                MemberCity = Utilities.ToInt(AreaSelector.Selector1.GetSelectedValue(0)),
                MemberArea = AreaSelector.GetSelectedLocation2(),
                MemberLocation = AreaSelector.GetSelectedLocation3(),
                ValidateWhenSaving = false,
                MemberMsnPhone = txtMemberPhone.Text.Trim()
            };
            user.Save(0);
            //auto login website
            SysMember loginUser;
            UserBLL.UserLogin(user.Id.ToString(), txtMemberPwd1.Text.Trim(), SubSystem.Member, LoginSystem.Web, out loginUser);
            return true;
        }

        private static void AddRedColorStyle(System.Web.UI.HtmlControls.HtmlControl control)
        {
            if (string.IsNullOrEmpty(control.Style[HtmlTextWriterStyle.Color]))
                control.Style.Add(HtmlTextWriterStyle.Color, "Red");
        }
        protected bool CheckData()
        {
            string message;
            bool isSuccess = true;
            if (!string.IsNullOrEmpty(txtMyFriend.Text))
            {
                _rUser = UserBLL.GetUserByPhoneNum(txtMyFriend.Text.Trim());
                if (_rUser == null)
                {

                    lblRMember.InnerHtml = "你输入的推荐人不存在！";
                    AddRedColorStyle(lblRMember);
                    _rUser = null;
                    isSuccess = false;
                }
                else if (_rUser.CompanyId > 0)
                {
                    lblRMember.InnerHtml = ("你输入的推荐人账号是商家账号");
                    AddRedColorStyle(lblRMember);
                    _rUser = null;
                    isSuccess = false;
                }
            }
            if (!UserBLL.CheckUserName(this.txtMemberPhone.Text, out message))
            {
                this.lblPhoneInfo.InnerHtml = (message);
                AddRedColorStyle(lblPhoneInfo);
                isSuccess = false;
            }

            if (!UserBLL.CheckUserPwd(this.txtMemberPwd1.Text, out message))
            {
                this.lblPwd1Info.InnerHtml = (message);
                AddRedColorStyle(lblPwd1Info);
                isSuccess = false;
            }

            if (string.Compare(this.txtMemberPwd1.Text, this.txtMemberPwd2.Text) != 0)
            {
                this.lblPwd2Info.InnerHtml = ("两次输入的密码不同!");
                AddRedColorStyle(lblPwd2Info);
                isSuccess = false;
            }

            //if (!SubSonic.Sugar.Validation.IsEmail(txtMemberEmail.Text))
            //    LogMessage("电子邮箱的格式不正确!");

            //if (string.IsNullOrEmpty(txtMemberName.Text))
            //    LogMessage("会员名不能为空!");

            if (UserBLL.CheckUserExist(txtMemberPhone.Text))
            {
                this.lblPhoneInfo.InnerHtml = ("该手机已经注册");
                AddRedColorStyle(lblPhoneInfo);
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}