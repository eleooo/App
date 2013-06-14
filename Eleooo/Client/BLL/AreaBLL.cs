using System;
using System.Collections.Generic;
using System.Text;
using Eleooo.DAL;

namespace Eleooo.Client
{
    public class AreaBLL
    {
        private static SysArea _defArea;
        public static SysArea DefArea
        {
            get
            {
                if (_defArea == null)
                    _defArea = new SysArea { Id = -1, AreaName = "请选择",PId = -1 };
                return _defArea;
            }
        }
        private static List<SysArea> _areas;
        public static List<SysArea> Areas
        {
            get
            {
                if (_areas == null)
                {
                    var query = DB.Select( ).From<SysArea>( );
                    _areas = ServiceProvider.Service.ExecuteAsCollection<SysArea>(query);
                }
                return _areas;
            }
        }
        public static List<SysArea> GetAreaListByPid(int pid)
        {
            return Areas.FindAll((SysArea match) =>
                {
                    return match.PId == pid;
                });
        }
        public static List<SysArea> GetAreaListForCombox(int pid)
        {
            List<SysArea> areas = GetAreaListByPid(pid);
            areas.Insert(0, DefArea);
            return areas;
        }
        public static SysArea GetAreaByID(int id)
        {
            return Areas.Find((SysArea match) =>
                {
                    return match.Id == id;
                });
        }
        public static SysArea GetAreaByDepth(string areaDepth)
        {
            return Areas.Find((SysArea match) =>
                {
                    return Utilities.Compare(match.Depth, areaDepth);
                });
        }
    }
}
