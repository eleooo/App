using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eleooo.DAL;
using System.Data;
using Newtonsoft.Json;
using Eleooo.Web;

namespace Eleooo.BLL.Services
{
    class MealMenuHandler : IHandlerServices
    {

        #region IHandlerServices 成员

        public Common.ServicesResult Query(System.Web.HttpContext context)
        {
            object dt = null;
            SysCompany company = CompanyBLL.GetCompanyByTel(context.Request.Params["CompanyTel"]);
            if (company == null)
            {
                goto label_end;
            }
            var query = DB.Select("id as id", "DirName as name").From<SysTakeawayDirectory>( )
                          .Where(SysTakeawayDirectory.CompanyIDColumn).IsEqualTo(company.Id);
            dt = query.ExecuteDataTable( );
        label_end:
            return new Common.ServicesResult { data = dt };
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
