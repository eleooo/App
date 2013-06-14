using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eleooo.Common;

namespace Eleooo.BLL.Services
{
    class UserHandler : IHandlerServices
    {
        #region IHandlerServices 成员

        public Common.ServicesResult Query(System.Web.HttpContext context)
        {
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
            throw new NotImplementedException( );
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
