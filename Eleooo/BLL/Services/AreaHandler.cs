using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eleooo.DAL;
using System.Data;
using Newtonsoft.Json;
using SubSonic;

namespace Eleooo.BLL.Services
{
    class AreaHandler : IHandlerServices
    {
        private static readonly string CompanyMansionAreaQuery = @"
select distinct t1.ID as id,t1.Area_Name as name,t1.Depth as depth from dbo.Sys_Area as t1 
inner join dbo.Sys_Area_Mansion as t2 ON t1.ID = t2.AreaID
inner join dbo.Sys_Company_Mansion as t3 ON t2.ID = t3.MansionID
inner join dbo.Sys_Company as t4 ON t4.ID = t3.CompanyID
where t4.CompanyTel=@Phone";

        #region IHandlerServices 成员

        public Common.ServicesResult Query(System.Web.HttpContext context)
        {
            int nPID;
            DataTable dt = null;
            if (!int.TryParse(context.Request.Params["pid"], out nPID) || nPID < 0)
            {
                goto label_end;
            }
            //if (nPID <= -1)
            //{
            //    result = GetAllArea( );
            //    goto label_end;
            //}
            var query = DB.Select("id as id", "Area_Name as name").From<SysArea>( )
                          .Where(SysArea.PIdColumn).IsEqualTo(nPID);
            dt = query.ExecuteDataTable( );
            //result = JsonConvert.SerializeObject(dt);
        label_end:
            return new Common.ServicesResult { data = dt };
        }

        public Common.ServicesResult GetMansionArea(System.Web.HttpContext context)
        {
            var phone = context.Request["phone"];
            QueryCommand cmd = new QueryCommand(CompanyMansionAreaQuery);
            cmd.AddParameter("@Phone", phone);
            return new Common.ServicesResult { data = DataService.GetDataTable(cmd) };
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
