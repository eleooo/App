using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Web.Controls;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class MemberAdd : ActionPage
    {
        const string INPUT_SEX =
        @"<input class='radio' type='radio' name='{0}' value='true' {1} />男
        <input class='radio' type='radio' name='{0}' value='false' {2} />女";
        int nCheck = -1;
        const string HDNPHONENUM = "hdnMemberPhoneNumber";
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            if (nCheck > 0)
            {
                formView.GetViewRow(SysMember.MemberFullnameColumn).IsDisabled = true;
                formView.GetViewRow(SysMember.MemberPhoneNumberColumn).IsDisabled = true;
                formView.AddHideColumn(HDNPHONENUM, nCheck.ToString( ));
                formView.DataSource = DB.Select( ).From<SysMember>( ).Where(SysMember.IdColumn).IsEqualTo(nCheck);
            }

            formView.DataBind( );
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppContext.Context.CompanyType == CompanyType.MealCompany)
            {
                txtMessage.InnerHtml = "阁下的商家类型无权使用此功能";
                return;
            }
            formView.DataSource = DB.Select( ).From<SysMember>( ).Where(SysMember.IdColumn).IsEqualTo(0);
            formView.AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(SysMember.MemberPwdColumn)
                    .AddCustomColumn("ConfirmPwd", "确认密码", string.Empty)
                    .AddShowColumn(SysMember.MemberGradeColumn, "0")
                    .AddShowColumn(SysMember.MemberEmailColumn)
                    .AddShowColumn(SysMember.MemberSexColumn)
                    .AddShowColumn(SysMember.MemberBirthdayColumn)
                    .AddShowColumn(SysMember.MemberAddress1Column)
                //.AddShowColumn(SysMember.MemberStateColumn)
                //.AddShowColumn(SysMember.MemberCityColumn)
                //.AddShowColumn(SysMember.MemberZipColumn)
                    .AddHideColumn(SysMember.MemberFingerColumn);
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnValidate += new OnFormViewValidate(formView_OnValidate);
            formView.OnBeforeSaved += new OnFormViewSaveHandle(formView_OnBeforeSaved);
            txtMessage.InnerHtml = string.Empty;
        }

        void formView_OnBeforeSaved(object item)
        {
            SysMember user = item as SysMember;
            user.MemberAddress2 = string.Empty;
            user.MemberCountry = "中国";
            user.MemberDate = DateTime.Now;
            user.MemberSum = 0;
            user.MemberBalance = 0;
            user.MemberBalanceCash = 0;
            user.MemberStatus = 1;
            user.MemberMemo = string.Empty;
            user.MemberPid = 0;
            user.MemberCompanyID = CurrentUser.CompanyId;
            user.MemberRoleId = UserBLL.GetDefaultUseRole((int)SubSystem.Member);
            //user.MemberGrade = 0;
            user.AdminRoleId = 0;
            user.CompanyRoleId = 0;
            user.LastLoginDate = null;
            user.LastLoginSubSys = 0;
            user.CompanyId = 0;
            user.MemberState = null;
            user.MemberCity = AppContext.Context.Company.CompanyCity;
            user.MemberArea = AppContext.Context.Company.CompanyArea;
            user.MemberLocation = AppContext.Context.Company.CompanyLocation;
            user.MemberZip = null;
            user.AreaDepth1 = AppContext.Context.Company.AreaDepth;
            user.AreaDepth2 = null;
            user.AreaDepth3 = null;
            user.AreaModifyDate = null;
            user.MemberMsnPhone = user.MemberPhoneNumber;
        }

        void formView_OnValidate(string columnName, UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "MemberFullname":
                    if (string.IsNullOrEmpty(viewRow.Value))
                        viewRow.ValidateMessage = "会员名称不能为空!";
                    break;
                case "MemberPwd":
                    if (nCheck > 0)
                    {
                        viewRow.ValidateMessage = string.Empty;
                        break;
                    }
                    if (string.IsNullOrEmpty(viewRow.Value))
                    {
                        viewRow.ValidateMessage = "会员密码不能为空!";
                        break;
                    }
                    if (viewRow.Value.Length < 6)
                    {
                        viewRow.ValidateMessage = "会员密码不能小于6位!";
                        break;
                    }
                    if (!Utilities.Compare(viewRow.Value, Request.Params["ConfirmPwd"]))
                    {
                        viewRow.ValidateMessage = "两次输入的密码不一样";
                        break;
                    }
                    viewRow.ParamValue = Utilities.DESEncrypt(viewRow.Value);
                    break;
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
                    nCheck = CompanyBLL.CheckIsOwnerUserOrExist(viewRow.Value, CurrentUser.CompanyId.Value);
                    if (nCheck == -2)
                    {
                        viewRow.ValidateMessage = "此账号是商家账号,不允许注册为会员";
                        break;
                    }
                    if (nCheck >= 0)
                    {
                        viewRow.ValidateMessage = "该账号已经注册";
                        break;
                    }
                    break;
            }
        }

        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            CompanyType cmpType = Formatter.ToEnum<CompanyType>(AppContext.Context.Company.CompanyType);
            if (cmpType != CompanyType.UnionCompany)
            {
                txtMessage.InnerHtml = "您暂无权限使用该功能";
                goto lbl_end;
            }
            int userID = Utilities.ToInt(Request.Params[HDNPHONENUM]);
            int c = CompanyBLL.CheckIsOwnerUserOrExist(userID, CurrentUser.CompanyId.Value);
            if (c == -2)
            {
                txtMessage.InnerHtml = "此账号是商家账号,不允许注册为会员";
                goto lbl_end;
            }
            else if (c == 0)
            {
                txtMessage.InnerHtml = "此账号已经在本店注册过,请使用其他账号注册!";
                goto lbl_end;
            }
            else if (c == -1)
            {
                txtMessage.InnerHtml = "会员不存在!";
                goto lbl_end;
            }
            //check pwd
            SysMember user = SysMember.FetchByID(userID);
            UcFormView.FormViewRow pwd = formView.GetViewRow(SysMember.MemberPwdColumn);
            UcFormView.FormViewRow finger = formView.GetViewRow(SysMember.MemberFingerColumn);
            if (string.IsNullOrEmpty(pwd.Value) && string.IsNullOrEmpty(finger.Value))
            {
                txtMessage.InnerHtml = "请输入会员的登录密码或者指纹";
                goto lbl_end;
            }
            if (!UserBLL.CheckUserPwd(user, pwd.Value) && !UserBLL.CheckUserFinger(user, finger.Value))
            {
                txtMessage.InnerHtml = "会员密码或指纹不正确!";
                goto lbl_end;
            }
            new SysMemberCompany
            {
                MemberCompanyCompanyID = CurrentUser.CompanyId.Value,
                MemberCompanyDate = DateTime.Now,
                MemberCompanyMemberID = userID
            }.Save( );
            txtMessage.InnerHtml = "保存成功";
        lbl_end:
            On_ActionQuery(sender, e);
        }

        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            CompanyType cmpType = Formatter.ToEnum<CompanyType>(AppContext.Context.Company.CompanyType);
            if (cmpType != CompanyType.UnionCompany)
            {
                txtMessage.InnerHtml = "您暂无权限使用该功能";
                goto lbl_end;
            }
            if (formView.Save<SysMember>(0) == -1)
            {
                if (nCheck == -1)
                    txtMessage.InnerHtml = "请输入会员登录密码或指纹,然后提交";
                else
                    txtMessage.InnerHtml = "保存失败!";
            }
            else
            {
                txtMessage.InnerHtml = "保存成功!";
                formView.ClearValue( );
            }
        lbl_end:
            On_ActionQuery(sender, e);

        }

        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "MemberGrade":
                    viewRow.RenderHtml = Eleooo.Web.Controls.GradeHtmlSelect.GetHtmlSelect(AppContext.Context.Company.Id, viewRow.ParamName, viewRow.Value);
                    break;
                case "MemberSex":
                    viewRow.RenderHtml = string.Format(INPUT_SEX, viewRow.ParamName,
                        Utilities.Compare(viewRow.Value, "true") || string.IsNullOrEmpty(viewRow.Value) ? "checked=\"checked\"" : string.Empty,
                        Utilities.Compare(viewRow.Value, "false") ? "checked=\"checked\"" : string.Empty);
                    break;
                case "MemberPhoneNumber":
                    viewRow.RenderHtml = string.Format(UcFormView.FORM_VIEW_PHONE_TEMPLATE, viewRow.ParamName, viewRow.Value);
                    break;
                case "MemberPwd":
                    viewRow.RenderHtml = string.Format(UcFormView.FORM_VIEW_PWD_TEMPLATE, viewRow.ParamName, viewRow.Value);
                    break;
                case "ConfirmPwd":
                    viewRow.RenderHtml = string.Format(UcFormView.FORM_VIEW_PWD_TEMPLATE, viewRow.ParamName, viewRow.Value);
                    break;
                case "MemberBirthday":
                    DateTime dtNow = DateTime.Now;
                    viewRow.RenderHtml = string.Concat(DateHtmlSelect.GetDateSelectCtrl(dtNow.AddYears(-150), dtNow, dtNow, viewRow.ParamName),
                                          string.Format(UcFormView.FORM_VIEW_HIDDEN_TEMPLATE, viewRow.ParamName, viewRow.Value));
                    break;
            }
        }
    }
}