using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eleooo.DAL;
using Eleooo.Web;
using Eleooo.Common;

namespace Eleooo.BLL.Services
{
    class SendPasswordHandler : IHandlerServices
    {
        #region IHandlerServices 成员

        public ServicesResult Query(System.Web.HttpContext context)
        {
            string phone = context.Request["phone"];
            ServicesResult result = new ServicesResult { code = -1 };
            try
            {
                SysMember user = UserBLL.GetUserByPhoneNum(phone);
                if (user == null)
                    throw new Exception("你输入的账号不存在,你可以使用此账号注册会员.");
                if (!user.MemberStatus.HasValue || user.MemberStatus.Value != 1)
                    throw new Exception("你输入的处于停用状态,不能发送密码,请联系乐多分客服解决.");
                var config = UserBLL.GetUserConfig(user.Id);
                var count = Convert.ToInt32(config.MsnPwdCount);
                if (count >= 3)
                    throw new Exception("你最多只允许使用3次短信找回密码功能.");
                string pwd = user.MemberPwd;
                if (pwd.Length > 6)
                    pwd = Utilities.DESDecrypt(pwd);
                int logId;
                string message;
                if (MsnBLL.SendMessage(user.MemberPhoneNumber, "亲爱的用户：您的登录密码为" + pwd + "，请妥善保管。【乐多分】", 0, out message, out logId))
                {
                    config.MsnPwdCount = count + 1;
                    config.Save( );
                    result.message = "你的登录密码已经发送到:" + phone;
                    result.code = 0;
                }
                else
                    throw new Exception(message);
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;
        }

        public ServicesResult Add(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public ServicesResult Edit(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public ServicesResult Delete(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }


        public ServicesResult Login(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public ServicesResult Logout(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
