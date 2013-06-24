using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Eleooo.Common;
using Eleooo.Web;
using Eleooo.DAL;

namespace Eleooo.BLL.Services
{
    class AppHandler : IHandlerServices
    {
        public void Log(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request["source"]) && !string.IsNullOrEmpty(context.Request["message"]))
                Logging.Log(context.Request["source"], context.Request["message"], context.Request["trace"], false);
        }
        public Common.ServicesResult Login(HttpContext context)
        {
            var phone = context.Request["u"];
            var pwd = context.Request["p"];
            var loginSys = Formatter.ToEnum<LoginSystem>(context.Request["s"]);
            SysMember user;
            var state = UserBLL.UserLogin(phone, pwd, SubSystem.Company, loginSys, out user);
            string message = string.Empty;
            if (state == 1)
                message = ResBLL.Get("check_UserName_notexist");
            else if (state == 2)
                message = ResBLL.Get("check_PassWord_error");
            else if (state == 3)
                message = ResBLL.Get("check_login_subsys_error");
            else if (state == 4)
                message = ResBLL.GetRes("check_login_lock", "账号处于审核状态", "账号处于审核状态");
            else if (state == 0)
            {
                object result = null;
                if (loginSys == LoginSystem.Mobile)
                {
                    result = new
                    {
                        id = user.Id,
                        p = user.MemberPhoneNumber,
                        t = Utilities.GenFormsAuthenticationTicketValue(user.Id, SubSystem.Company, loginSys),
                        c = user.CompanyId
                    };
                }
                return ServicesResult.GetInstance(result);
            }
            return ServicesResult.GetInstance(-state, message, null);
        }
        public Common.ServicesResult SendPassword(HttpContext context)
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
                    config.Save();
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
        public Common.ServicesResult GetFinance(HttpContext context)
        {
            var d1 = Utilities.ToDateTime(context.Request["d1"]);
            var d2 = Utilities.ToDateTime(context.Request["d2"]).AddDays(1);
            var t = Utilities.ToInt(context.Request["t"]);
            var sp = SP_.SpGetFinance(AppContextBase.Context.User.CompanyId, d1, d2, t);
            return ServicesResult.GetInstance(0, null, sp.ExecuteScalar( ));
        }
    }
}
