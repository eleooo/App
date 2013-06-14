using System;
using System.Collections.Generic;
using System.Text;
using Eleooo.DAL;

namespace Eleooo.Client
{
    public class RoleBLL
    {
        private static int _defaultUseRoleID = 0;
        public static int GetDefaultUseRole( )
        {
            return GetDefaultUseRole(2);
        }
        public static int GetDefaultUseRole(int SubSysID)
        {
            if (_defaultUseRoleID == 0)
            {
                var query = DB.Select("max(ID)").From<SysRoleDefine>( )
                              .Where(SysRoleDefine.IsDefaultColumn).IsEqualTo(true)
                              .And(SysRoleDefine.SubSysIdColumn).IsEqualTo(SubSysID);
                try
                {
                    _defaultUseRoleID = (int)ServiceProvider.Service.ExecuteScalar(query);
                }
                catch
                {
                    _defaultUseRoleID = 0;
                }
            }
            return _defaultUseRoleID;
        }
    }
}
