using System;
using System.Collections.Generic;
using System.Text;
using Eleooo.DAL;

namespace Eleooo.Client
{
    public class UserEntity
    {
        private SysMember _userData;
        internal SysMember UserData
        {
            get
            {
                if (_userData == null)
                {
                    InitUserData( );
                    _userData = new SysMember
                    {
                        AdminRoleId = 0,
                        CompanyId = 0,
                        MemberAddress1 = string.Empty,
                        MemberAddress2 = string.Empty,
                        MemberBalance = 0,
                        MemberBalanceCash = 0,
                        MemberBirthday = null,
                        MemberCity = 0,
                        MemberCompanyID = AppContext.Company.Id,
                        MemberCountry = "中国",
                        MemberDate = DateTime.Now,
                        MemberEmail = string.Empty,
                        MemberFinger = string.Empty,
                        MemberFullname = string.Empty,
                        MemberGrade = GradeBLL.DefGrade,
                        MemberMemo = string.Empty,
                        MemberPhoneNumber = string.Empty,
                        MemberPid = 0,
                        MemberPwd = string.Empty,
                        MemberRoleId = 2,
                        MemberSex = true,
                        MemberState = string.Empty,
                        MemberStatus = 1,
                        MemberSum = 0,
                        MemberZip = string.Empty,
                        LastLoginDate = null,
                        LastLoginSubSys = 0,
                        IsNew = true,
                        CompanyRoleId = 0,
                        AreaDepth1 = AppContext.Company.AreaDepth,
                        AreaDepth2 = null,
                        AreaDepth3 = null,
                        AreaModifyDate = null,
                        MemberArea = null,
                        MemberLocation = null
                    };
                    MemberGrade = GradeBLL.GetNameByID(Convert.ToString(_userData.MemberGrade));
                }
                return _userData;
            }
        }
        public void InitUserData( )
        {
            _userData = null;
            UserPwd = string.Empty;
            UserPwdConfirm = string.Empty;
            UserFinger = string.Empty;
            _IsOtherUser = false;
            MemberPhoneNumber = string.Empty;
        }
        public void SetOldUser(SysMember oldUser, bool isOtherUser = false)
        {
            if (_userData == null)
                _userData = new SysMember( );
            oldUser.CopyTo(_userData);
            MemberGrade = GradeBLL.GetNameByID(Convert.ToString(oldUser.MemberGrade));
            _userData.MarkOld( );
            _userData.IsLoaded = true;
            MemberPhoneNumber = _userData.MemberPhoneNumber;
            _IsOtherUser = isOtherUser;
        }
        private bool _IsOtherUser;
        public bool IsOtherUser { get { return _IsOtherUser; } }
        public string MemberPhoneNumber { get; set; }
        public string MemberFullname
        {
            get { return UserData.MemberFullname; }
            set { UserData.MemberFullname = value; }
        }

        public string MemberGrade { get; set; }
        //{
        //    get
        //    {
        //        return GradeHelper.GetNameByID(Convert.ToString(UserData.MemberGrade));
        //    }
        //    set
        //    {
        //        UserData.MemberGrade = Convert.ToInt32(GradeHelper.GetIDByName(value));
        //    }
        //}
        public string MemberEmail
        {
            get { return UserData.MemberEmail; }
            set { UserData.MemberEmail = value; }
        }
        public string MemberSex
        {
            get
            {
                if (Convert.ToBoolean(UserData.MemberSex))
                    return "男";
                else
                    return "女";
            }
            set
            {
                UserData.MemberSex = value == "男";
            }
        }
        public DateTime? MemberBirthday
        {
            get
            {
                return UserData.MemberBirthday;
            }
            set
            {
                UserData.MemberBirthday = value;
                //DateTime dt;
                //if (DateTime.TryParse(value, out dt))
                //    UserData.MemberBirthday = dt;
            }
        }
        public string MemberAddress1
        {
            get { return UserData.MemberAddress1; }
            set { UserData.MemberAddress1 = value; }
        }
        public string UserPwd { get; set; }
        public string UserPwdConfirm { get; set; }
        public string UserFinger { get; set; }
    }
}
