using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar;
using Eleooo.DAL;

namespace Eleooo.Client
{
    public class UserBLL
    {
        public static int SaveUser(UserEntity userData, out string message)
        {
            if (AppContext.Company.CompanyType.HasValue && AppContext.Company.CompanyType.Value != 1)
            {
                message = "您暂无权限使用该功能";
                return -1;
            }
            if (string.IsNullOrEmpty(userData.MemberPhoneNumber))
            {
                message = "会员账号(手机号码),不能为空!";
                return -1;
            }
            if (!Utilities.CheckEmail(userData.MemberEmail))
            {
                message = "会员Email的格式不正确!";
                return -3;
            }
            if (string.IsNullOrEmpty(userData.UserData.MemberPhoneNumber))
                userData.UserData.MemberPhoneNumber = userData.MemberPhoneNumber;
            userData.UserData.MemberGrade = GradeBLL.GetIDByName(userData.MemberGrade);
            if (!userData.UserData.IsNew && userData.UserData.Id > 0)
                return UpdateUser(userData, out message);
            else
                return AddNewUser(userData, out message);
        }
        private static int AddNewUser(UserEntity userData, out string message)
        {
            if (string.IsNullOrEmpty(userData.UserPwd))
            {
                message = "请输入会员的密码!";
                return 1;
            }

            if(string.IsNullOrEmpty(userData.UserPwdConfirm))
            {
                message = "请再一次输入会员密码!";
                return 2;
            }
            if (!Utilities.Compare(userData.UserPwd, userData.UserPwdConfirm,false))
            {
                message = "二次输入的密码不一致!";
                return 1;
            }
            userData.UserData.MemberPwd = Utilities.DESEncrypt(userData.UserPwd);
            userData.UserData.MemberFinger = userData.UserFinger;
            userData.UserData.MemberRoleId = 2;
            try
            {
                int nRet = ServiceProvider.Service.SaveEntity<DAL.SysMember>(userData.UserData);
                if (nRet > 0)
                {
                    message = "保存成功";
                    return 0;
                }
                else
                {
                    message = "保存失败!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return -1;
            }
        }
        private static int UpdateUser(UserEntity userData, out string message)
        {
            bool isReadFinger = !string.IsNullOrEmpty(userData.UserFinger);
            bool isFingerPass = FingerPrint.Finger.MatchFinger(userData.UserFinger, userData.UserData.MemberFinger);
            bool isPwdPass = CheckUserPwd(userData, userData.UserPwd);
            bool isModifyFinger = isReadFinger && !isFingerPass;
            if (isModifyFinger)
            {
                if (string.IsNullOrEmpty(userData.UserPwd))
                {
                    message = "修改会员信息需要输入会员密码!";
                    return 1;
                }
                if (!CheckUserPwd(userData, userData.UserPwd))
                {

                    message = "你输入的会员密码不正确!";
                    return 1;
                }
                userData.UserData.MemberFinger = userData.UserFinger;
            }
            else if (!isReadFinger && !isPwdPass)
            {
                message = "你输入的会员密码不正确!";
                return 1;
            }
            else if (!isFingerPass && !isPwdPass)
            {
                message = "指纹验证不正确!";
                return 1;
            }
            if (userData.UserData.DirtyColumns.Count == 0)
            {
                message = "保存成功";
                return 0;
            }
            try
            {
                if (userData.IsOtherUser)
                {
                    bool isOwnerUser = ServiceProvider.Service.IsOwnerUser(userData.UserData.Id, AppContext.Company.Id);
                    if (!isOwnerUser)
                    {
                        SysMemberCompany u = new SysMemberCompany
                        {
                            MemberCompanyCompanyID = AppContext.Company.Id,
                            MemberCompanyDate = DateTime.Now,
                            MemberCompanyMemberID = userData.UserData.Id
                        };
                        ServiceProvider.Service.SaveEntity<SysMemberCompany>(u);
                    }
                }
                int nRet = ServiceProvider.Service.SaveEntity<DAL.SysMember>(userData.UserData);
                if (nRet > 0)
                {
                    message = "保存成功";
                    return 0;
                }
                else
                {
                    message = "保存失败!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return -1;
            }
        }
        public static bool CheckUserPwd(DAL.SysMember user, string strPwd)
        {
            string enPass = Utilities.DESEncrypt(strPwd);
            return user != null && !string.IsNullOrEmpty(strPwd) &&
                    (Utilities.Compare(enPass, user.MemberPwd) ||
                    Utilities.Compare(strPwd, user.MemberPwd));
        }
        public static bool CheckUserPwd(UserEntity userData, string strPwd)
        {
            return userData != null && CheckUserPwd(userData.UserData, strPwd);
        }
        public static bool GetUserByPhone(string phoneNum,UserEntity userData, out string message)
        {
            message = string.Empty;
            userData.InitUserData( );
            if (string.IsNullOrEmpty(phoneNum))
            {
                message = "请输入会员账号!";
                return false;
            }
            if (AppContext.Company.CompanyType.HasValue && AppContext.Company.CompanyType.Value != 1)
            {
                message = "您暂无权限使用该功能";
                return false;
            }
            var query = DAL.DB.Select( ).From<DAL.SysMember>( )
                           .Where(DAL.SysMember.MemberPhoneNumberColumn).IsEqualTo(phoneNum);
            try
            {
                DAL.SysMember u = ServiceProvider.Service.ExecuteSingle<DAL.SysMember>(query);
                if (u == null || u.Id == 0)
                {
                    message = "此账号可用";
                    return true;
                }
                if (u.CompanyId > 0)
                {
                    message = "此账号已经注册为商家账号";
                    return false;
                }
                bool IsOwnerUser = ServiceProvider.Service.IsOwnerUser(u.Id, AppContext.Company.Id);
                if (!IsOwnerUser)
                {
                    message = "此账号可用";
                    MessageBoxEx.Show("此账号已经是其他商家会员!");
                    //userData.SetOldUser(u,true);
                    return false;
                }
                if (IsOwnerUser && MessageBoxEx.Show("已经账号已经存在,是否调出会员资料进行修改?\r\n修改会员资料需要输入会员密码或者指纹!", "提示", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    userData.SetOldUser(u);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
