using System;
using System.Collections.Generic;
using System.Text;
using Eleooo.DAL;

namespace Eleooo.Client
{
    public class AppContext
    {
        public static bool IsRuning { get; set; }
        public static SysMember User { get; set; }
        private static SysCompany _company;
        public static SysCompany Company
        {
            get
            {
                if (User == null || !IsRuning)
                    return new SysCompany( );
                if (_company == null && User != null)
                {
                    var query = DB.Select( ).From<SysCompany>( ).Where(SysCompany.IdColumn).IsEqualTo(User.CompanyId);
                    _company = ServiceProvider.Service.ExecuteSingle<SysCompany>(query);
                }
                return _company;
            }
        }
    }
}
