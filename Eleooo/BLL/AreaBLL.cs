using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.Common;
using Eleooo.DAL;
using SubSonic;
using System.Data;
using System.Linq;

namespace Eleooo.Web
{
    public class AreaBLL
    {
        public const string CITY_COOKIE = "City";
        private const string GETAREA_NAMES_FUNC = " dbo.GetAreaNames({0}) as {1} ";
        private const string GET_FIRST_AREA_NAME_FUNC = "dbo.GetFirstAreaName({0}) as {1}";
        private const string CHECKAREADEPTH_FUNC = " dbo.CheckAreaDepth({0},{1}) ";
        private const string CHECKAREADEPTHS_FUNC = " dbo.CheckAreaDepths({0},{1}) ";
        public static readonly char[] DepthSpliter = new char[] { '/',',' };
        private static readonly object _locker = new object( );

        private static List<SysArea> _areas;
        public static List<SysArea> Areas
        {
            get
            {
                if (_areas == null)
                {
                    lock (_locker)
                    {
                        _areas = DB.Select( ).From<SysArea>( )
                                   .ExecuteTypedList<SysArea>( );
                    }
                }
                return _areas;
            }
        }

        [ThreadStatic]
        private static Dictionary<string, SysArea> _areaNameMapping;
        public static Dictionary<string, SysArea> AreaNameMapping
        {
            get
            {
                if (_areaNameMapping == null)
                    _areaNameMapping = GetAreaNameMapping( );
                return _areaNameMapping;
            }
        }

        public static SysArea GetAreaByDepth(string depth)
        {
            return DB.Select( ).From<SysArea>( ).Where(SysArea.DepthColumn).IsEqualTo(depth).ExecuteSingle<SysArea>( );
        }
        public static SysArea GetAreaById(string id)
        {
            return Areas.Find((SysArea match) =>
                {
                    return match.Id.ToString( ).Equals(id);
                });
        }
        public static List<SysArea> GetAreasByIds(string ids)
        {
            List<SysArea> areas = new List<SysArea>( );
            if (string.IsNullOrEmpty(ids))
                return areas;
            string[] arr = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in arr)
            {
                SysArea area = GetAreaById(s);
                if (area != null)
                    areas.Add(area);
            }
            return areas;
        }
        public static List<SysArea> GetAreasByDepths(string depth)
        {
            List<SysArea> areas = new List<SysArea>( );
            if (string.IsNullOrEmpty(depth))
                return areas;
            string[] depths = depth.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (depths.Length > 0)
            {
                var query = DB.Select( ).From<SysArea>( ).Where(SysArea.DepthColumn).In(depths);
                using (var dr = query.ExecuteReader( ))
                {
                    while (dr.Read( ))
                    {
                        SysArea area = new SysArea( );
                        area.Load(dr);
                        areas.Add(area);
                    }
                }
            }
            return areas;
        }


        public static string GenCompanyCodeByID(int areaID)
        {
            SysArea area = SysArea.FetchByID(areaID);
            if (area != null)
                return GenCompanyCodeByDepth(area.Depth);
            else
                return string.Empty;
        }
        public static string GenCompanyCodeByDepth(string depth)
        {
            string prefix = GetAreaCodeByDepth(depth);
            return string.Format("{0}{1:D2}", prefix, GetAreaCompanyCountByDepth(depth));
        }
        public static int GetAreaCompanyCountByDepth(string depth)
        {
            int count = DB.Select("Count(*) + 1").From<SysCompany>( )
                          .Where(SysCompany.AreaDepthColumn).Like(depth + "%")
                          .ExecuteScalar<int>( );
            return count;
        }
        public static int GetAreaCompanyCountByPrefix(string prefix)
        {
            int count = DB.Select("Count(*) + 1").From<SysCompany>( )
              .Where(SysCompany.CompanyCodeColumn).Like(prefix + "%")
              .ExecuteScalar<int>( );
            return count;
        }
        public static string GetAreaCodeByDepth(string depth)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(depth))
                goto lable_return;
            string[] ids = depth.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (ids == null || ids.Length == 0)
                goto lable_return;
            foreach (string id in ids)
            {
                SysArea area = SysArea.FetchByID(id);
                if (area != null && !string.IsNullOrEmpty(area.AreaCode))
                    result = string.Concat(result, area.AreaCode);
            }

        lable_return:
            return result;
        }
        public static string GetCompanyCodePrefix(int areaID)
        {
            string result = string.Empty;
            SysArea area = SysArea.FetchByID(areaID);
            if (area == null)
                goto lable_return;
            result = GetAreaCodeByDepth(area.Depth);
        lable_return:
            return result;
        }
        public static string GetCompanyCodePrefix(string companyCode)
        {
            if (string.IsNullOrEmpty(companyCode))
                return string.Empty;
            return companyCode.Substring(0, companyCode.Length - 2);
        }
        public static string GetAreaDepthLike(string areaDepth)
        {
            return string.Concat(string.IsNullOrEmpty(areaDepth) ? string.Empty : areaDepth, "%");
        }
        public static string GetAreaNamesByColumn(SubSonic.TableSchema.TableColumn col)
        {
            return string.Format(GETAREA_NAMES_FUNC, Utilities.GetTableColumn(col), col.ColumnName);
        }
        public static string GetAreaNamesByDepths(string depths, string asColName)
        {
            return string.Format(GETAREA_NAMES_FUNC, string.Concat("N'", depths, "'"), asColName);
        }
        public static string GetFirstAreaName(string depths, string asColName)
        {
            return string.Format(GET_FIRST_AREA_NAME_FUNC, string.Concat("N'", depths, "'"), asColName);
        }
        public static string GetFirstAreaName(SubSonic.TableSchema.TableColumn col, string asColName)
        {
            return string.Format(GET_FIRST_AREA_NAME_FUNC, Utilities.GetTableColumn(col), asColName);
        }
        public static string[] ConvertDepthToIds(string depth)
        {
            //1/2/7/ return 7
            if (string.IsNullOrEmpty(depth))
                return null;
            return depth.Split(DepthSpliter, StringSplitOptions.RemoveEmptyEntries);
        }
        public static IEnumerable<SysArea> LoadAreas( )
        {
            var rdr = DB.Select( ).From<SysArea>( ).ExecuteReader( );
            using (rdr)
            {
                while (rdr.Read( ))
                {
                    SysArea area = new SysArea( );
                    area.Load(rdr);
                    yield return area;
                }
            }
        }
        public static Dictionary<string, SysArea> GetAreaNameMapping( )
        {
            var areas = LoadAreas( );
            if (areas == null)
                return new Dictionary<string, SysArea>( );
            else
                return areas.ToDictionary(area => area.AreaName);
        }
        public static SysArea GetCurrentUserCity( )
        {
            return GetAreaById(Utilities.ToString(AppContextBase.Context.User.MemberCity));
        }
        public static SysArea GetCurrentCity( )
        {
            SysArea area = null;
            string cityID = Utilities.GetCookieValue(CITY_COOKIE);
            SubSystem subSys = AppContextBase.Context.CurrentSubSys;
            if (string.IsNullOrEmpty(cityID))
            {
                if (subSys == SubSystem.ALL)
                    area = GetAreaById("1");
                else
                    area = GetCurrentUserCity( );
            }
            else
                area = GetAreaById(cityID);
            if (area == null)
                area = GetAreaById("1");
            Utilities.AddCookie(CITY_COOKIE, area.Id.ToString( ));
            return area;
        }

        public static string RenderCheckAreaDepthFunc(TableSchema.TableColumn col1, TableSchema.TableColumn col2, bool isDepths = false)
        {
            if (isDepths)
                return string.Format(CHECKAREADEPTHS_FUNC, Utilities.GetTableColumn(col1), Utilities.GetTableColumn(col2));
            else
                return string.Format(CHECKAREADEPTH_FUNC, Utilities.GetTableColumn(col1), Utilities.GetTableColumn(col2));
        }
        public static string RenderCheckAreaDepthFunc(string depth1, TableSchema.TableColumn col2, bool isDepths = false)
        {
            if (isDepths)
                return string.Format(CHECKAREADEPTHS_FUNC, "'" + depth1 + "'", Utilities.GetTableColumn(col2));
            else
                return string.Format(CHECKAREADEPTH_FUNC, "'" + depth1 + "'", Utilities.GetTableColumn(col2));
        }
        public static string RenderCheckAreaDepthFunc(TableSchema.TableColumn col1, string depth2, bool isDepths = false)
        {
            if (isDepths)
                return string.Format(CHECKAREADEPTHS_FUNC, Utilities.GetTableColumn(col1), "'" + depth2 + "'");
            else
                return string.Format(CHECKAREADEPTH_FUNC, Utilities.GetTableColumn(col1), "'" + depth2 + "'");
        }
        public static string RenderCheckAreaDepthFunc(string depth1, string depth2, bool isDepths = false)
        {
            if (isDepths)
                return string.Format(CHECKAREADEPTHS_FUNC, "'" + depth1 + "'", "'" + depth2 + "'");
            else
                return string.Format(CHECKAREADEPTH_FUNC, "'" + depth1 + "'", "'" + depth2 + "'");
        }
        public static string GetAreaTag(object areaDepths)
        {
            if (Utilities.IsNull(areaDepths) || areaDepths.ToString( ).IndexOf(",") < 0)
                return "商圈";
            else
                return "等商圈";
        }
    }
}