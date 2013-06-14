using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubSonic;
using System.Data;
using Newtonsoft.Json;

namespace Eleooo.BLL.Services
{
    class CompanyAreaMansionHandler : IHandlerServices
    {

        #region IHandlerServices 成员

        public Common.ServicesResult Query(System.Web.HttpContext context)
        {
            string result = string.Empty;
            string tel = context.Request.Params["CompanyTel"];
            if (string.IsNullOrEmpty(tel))
            {
                result = "[]";
                goto label_end;
            }
            QueryCommand cmd = new QueryCommand("select distinct [Sys_Area_Mansion].[id],[Sys_Area_Mansion].[name] from [Sys_Area_Mansion] inner join [Sys_Company] on [Sys_Company].[AreaDepth] like [Sys_Area_Mansion].[AreaDepth]+'%' where [Sys_Company].[CompanyTel] = @companyTel;");
            cmd.AddParameter("@companyTel", tel, DbType.String);
            DataTable dt = DataService.GetDataSet(cmd).Tables[0];
            result = JsonConvert.SerializeObject(dt);
        label_end:
            return new Common.ServicesResult { data = result };
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
