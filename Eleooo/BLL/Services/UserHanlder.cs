using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eleooo.Common;
using Eleooo.Web;
using SubSonic;
using System.Transactions;

namespace Eleooo.BLL.Services
{
    class UserHandler : IHandlerServices
    {
        #region IHandlerServices 成员

        public Common.ServicesResult Get(System.Web.HttpContext context)
        {
            if (AppContextBase.CurrentSysId == (int)SubSystem.Company && AppContextBase.Context.Company != null)
                return Common.ServicesResult.GetInstance(CompanyBLL.GetCompanyInfo(AppContextBase.Context.Company));
            else
                return new Common.ServicesResult
                {
                    data = new { ID = AppContextBase.Context.User.Id, Name = AppContextBase.Context.User.MemberFullname, Phone = AppContextBase.Context.User.MemberPhoneNumber }
                };
        }

        public Common.ServicesResult Add(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public Common.ServicesResult Edit(System.Web.HttpContext context)
        {
            int code = -1;
            string message = string.Empty;
            if (AppContextBase.CurrentSysId == (int)SubSystem.Company && AppContextBase.Context.Company != null)
            {
                //OrderElapsed = company.OrderElapsed,
                //Ranking2 = ranking2,
                //CompanyWorkTime = company.CompanyWorkTime,
                //OnSetSum = company.OnSetSum,
                //IsSuspend = company.IsSuspend,
                //Ranking1 = ranking1,
                //Amount = amount
                var company = AppContextBase.Context.Company;
                var request = context.Request;
                company.OrderElapsed = request["OrderElapsed"];
                company.CompanyWorkTime = request["CompanyWorkTime"];
                company.OnSetSum = Utilities.ToDecimal(request["OnSetSum"]);
                company.IsSuspend = Utilities.ToBool(request["IsSuspend"]);
                company.OrderMaxAmount = Utilities.ToInt(request["OrderMaxAmount"]);
                company.ServiceSum = Utilities.ToDecimal(request["ServiceSum"]);
                company.CompanyProvince = request["CompanyProvince"];
                string temp = request["p1"];
                bool ispass = !string.IsNullOrEmpty(temp);
                if (ispass) //change password
                {
                    if (!UserBLL.CheckUserPwd(temp, out message))
                        goto lbl_return;
                    if (!Utilities.Compare(temp, request["p2"]))
                    {
                        message = "你两次输入密码不一致^_^";
                        goto lbl_return;
                    }
                    AppContextBase.Context.User.MemberPwd = temp;
                }
                temp = request["p"].Trim( );
                if (!Utilities.Compare(temp, company.CompanyTel))
                {
                    if (!UserBLL.CheckUserName(temp, out message))
                        goto lbl_return;
                    if (UserBLL.CheckUserExist(temp))
                    {
                        message = "此账号已经是乐多分会员。";
                        goto lbl_return;
                    }
                    using (TransactionScope ts = new TransactionScope( ))
                    {
                        using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                        {
                            CompanyBLL.UpdateUserPhone(temp, company.CompanyTel);
                            company.CompanyTel = temp;
                            AppContextBase.Context.User.MemberPhoneNumber = temp;
                            company.Save( );
                            AppContextBase.Context.User.Save( );
                            ts.Complete( );
                        }
                    }
                    goto lbl_success;
                }
                company.Save( );
                if (ispass)
                    AppContextBase.Context.User.Save( );
            }
        lbl_success:
            code = 0;
            message = "修改成功.";
        lbl_return:
            return Common.ServicesResult.GetInstance(code, message, null);
        }

        public Common.ServicesResult Delete(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public Common.ServicesResult Login(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public Common.ServicesResult Logout(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
