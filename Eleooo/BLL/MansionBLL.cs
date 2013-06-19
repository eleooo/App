using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web
{
    public static class MansionBLL
    {
        public struct MansionAreaId
        {
            public MansionAreaId(int areaId, string mansionName)
            {
                AreaId = areaId;
                MansionName = mansionName;
            }
            public int AreaId;
            public string MansionName;
            public static MansionAreaId GetMansionAreaId(int areaId, string mansionName)
            {
                return new MansionAreaId(areaId, mansionName);
            }
        }
        public struct CompanyMansionId
        {
            public int MansionId;
            public int CompanyId;
            public static CompanyMansionId GetCompanyMansionId(int companyId, int mansionId)
            {
                CompanyMansionId companyMansionId;
                companyMansionId.CompanyId = companyId;
                companyMansionId.MansionId = mansionId;
                return companyMansionId;
            }
        }

        [ThreadStatic]
        private static Dictionary<MansionAreaId, SysAreaMansion> _areaMansionMapping;
        public static Dictionary<MansionAreaId, SysAreaMansion> AreaMansionMapping
        {
            get
            {
                if (_areaMansionMapping == null)
                {
                    var mansions = LoadAreaMansions( );
                    if (mansions != null)
                        _areaMansionMapping = mansions.ToDictionary(mansion => MansionAreaId.GetMansionAreaId(mansion.AreaID.Value, mansion.Name));
                    else
                        _areaMansionMapping = new Dictionary<MansionAreaId, SysAreaMansion>( );
                }
                return _areaMansionMapping;
            }
        }
        public static IEnumerable<SysAreaMansion> LoadAreaMansions( )
        {
            var rdr = DB.Select( ).From<SysAreaMansion>( ).ExecuteReader( );
            using (rdr)
            {
                while (rdr.Read( ))
                {
                    SysAreaMansion mansion = new SysAreaMansion( );
                    mansion.Load(rdr);
                    yield return mansion;
                }
            }
        }

        [ThreadStatic]
        private static Dictionary<CompanyMansionId, SysCompanyMansion> _companyMansionMapping;
        public static Dictionary<CompanyMansionId, SysCompanyMansion> CompanyMansionMapping
        {
            get
            {
                if (_companyMansionMapping == null)
                {
                    var companyMansions = LoadCompanyMansions( );
                    if (companyMansions != null)
                        _companyMansionMapping = companyMansions.ToDictionary(companyMansion => CompanyMansionId.GetCompanyMansionId(companyMansion.CompanyID.Value, companyMansion.MansionID.Value));
                    else
                        return new Dictionary<CompanyMansionId, SysCompanyMansion>( );
                }
                return _companyMansionMapping;
            }
        }

        public static IEnumerable<SysCompanyMansion> LoadCompanyMansions( )
        {
            var query = DB.Select( ).From<SysCompanyMansion>( );
            using (var rdr = query.ExecuteReader( ))
            {
                while (rdr.Read( ))
                {
                    SysCompanyMansion companyMansion = new SysCompanyMansion( );
                    companyMansion.Load(rdr);
                    yield return companyMansion;
                }
            }
        }
        public static SysAreaMansion GetAreaMansionByName(string name)
        {
            var query = DB.Select( ).Top("1").From<SysAreaMansion>( )
                          .Where(SysAreaMansion.NameColumn).IsEqualTo(name);
            return query.ExecuteSingle<SysAreaMansion>( );
        }
        public static IEnumerable<SysAreaMansion> LoadCompanyMansions(int companyId)
        {
            var query = DB.Select(Utilities.GetTableColumns(SysAreaMansion.Schema)).From<SysAreaMansion>( )
                                    .InnerJoin(SysCompanyMansion.MansionIDColumn, SysAreaMansion.IdColumn)
                                    .Where(SysCompanyMansion.CompanyIDColumn).IsEqualTo(companyId);
            using (var rdr = query.ExecuteReader( ))
            {
                while (rdr.Read( ))
                {
                    SysAreaMansion companyMansion = new SysAreaMansion( );
                    companyMansion.Load(rdr);
                    yield return companyMansion;
                }
            }
        }

        public static IEnumerable<IDataReader> QueryMansionByWord(string word, int page_num, int page_size, out int total)
        {
            //word = Utilities.GetAllLikeQuery(word);
            var query = DB.Select( ).From<SysAreaMansion>( )
                          .ConstraintExpression(BLL.DbCommonFn.FnCompare("WHERE", SysAreaMansion.NameColumn, word, 1));
            total = query.GetRecordCount( );
            query.Paged(page_num, page_size);
            return query.GetDataReaderEnumerator( );
        }

        public static bool CheckAreaMansionExist(int areaId, string mansionName, int ignoreId)
        {
            var query = DB.Select( ).From<SysAreaMansion>( )
                          .Where(SysAreaMansion.IdColumn).IsNotEqualTo(ignoreId)
                          .And(SysAreaMansion.AreaIDColumn).IsEqualTo(areaId)
                          .And(SysAreaMansion.NameColumn).IsEqualTo(mansionName);
            return query.GetRecordCount( ) > 0;
        }
        public static bool CheckCompanyMansionExist(int companyId, int mansionId, int igoreId)
        {
            var query = DB.Select( ).From<SysCompanyMansion>( )
                          .Where(SysCompanyMansion.IdColumn).IsNotEqualTo(igoreId)
                          .And(SysCompanyMansion.CompanyIDColumn).IsEqualTo(companyId)
                          .And(SysCompanyMansion.MansionIDColumn).IsEqualTo(mansionId);
            return query.GetRecordCount( ) > 0;
        }

        public static string GetMansionNameByID(int mansionID)
        {
            return DB.Select(SysAreaMansion.NameColumn.QualifiedName).From<SysAreaMansion>()
                     .Where(SysAreaMansion.IdColumn).IsEqualTo(mansionID)
                     .ExecuteScalar<string>();
        }

        public static SysAreaMansion GetAreaMansion(string areaName, string mansionName)
        {
            var areaDict = AreaBLL.AreaNameMapping;
            if (!areaDict.ContainsKey(areaName))
                return null;
            var area = areaDict[areaName];
            var query = DB.Select( ).Top("1").From<SysAreaMansion>( )
                          .Where(SysAreaMansion.AreaIDColumn).IsEqualTo(area.Id)
                          .And(SysAreaMansion.NameColumn).IsEqualTo(mansionName);
            SysAreaMansion mansion = null;
            using (var dr = query.ExecuteReader( ))
            {
                if (dr.Read( ))
                {
                    mansion = new SysAreaMansion( );
                    mansion.Load(dr);
                }
            }
            if (mansion == null)
            {
                mansion = new SysAreaMansion
                {
                    AreaID = area.Id,
                    Name = mansionName,
                    Address = null,
                    Code = null,
                    AreaDepth = area.Depth,
                };
                mansion.Save( );
            }
            return mansion;
        }
        public static void TryAddCompanyMansion(int mansionId, int companyId)
        {
            var query = DB.Select("Count(*)").Top("1").From<SysCompanyMansion>( )
                          .Where(SysCompanyMansion.CompanyIDColumn).IsEqualTo(companyId)
                          .And(SysCompanyMansion.MansionIDColumn).IsEqualTo(mansionId);
            if (Utilities.ToInt(query.ExecuteScalar( )) == 0)
            {
                new SysCompanyMansion( )
                {
                    CompanyID = companyId,
                    MansionID = mansionId
                }.Save( );
            }
        }
        public static bool ImportAreaMansion(DataTable dt, out string message)
        {
            bool result = false;
            message = string.Empty;
            var dcAreaName = (DataColumn)null;
            var dcName = (DataColumn)null;
            var dcAddr = (DataColumn)null;
            if (dt.Columns.Contains("所属片区"))
                dcAreaName = dt.Columns["所属片区"];
            if (dt.Columns.Contains("派送范围"))
                dcName = dt.Columns["派送范围"];
            if (dt.Columns.Contains("大厦地址"))
                dcAddr = dt.Columns["大厦地址"];
            if (dcAreaName == null || dcName == null)
            {
                message = "上传的excel 文档必须包含有 所属片区 和 派送范围两列信息";
                goto lbl_return;
            }

            var areaDict = AreaBLL.AreaNameMapping;
            string areaName, mansionName;
            SysArea area;
            MansionAreaId mansionAreaId;
            foreach (DataRow dr in dt.Rows)
            {
                if (string.IsNullOrEmpty(areaName = Convert.ToString(dr[dcAreaName]).Trim( )))
                    continue;
                if (string.IsNullOrEmpty(mansionName = Convert.ToString(dr[dcName]).Trim( )))
                    continue;
                if (!areaDict.ContainsKey(areaName))
                    continue;
                area = areaDict[areaName];
                mansionAreaId = MansionAreaId.GetMansionAreaId(area.Id, mansionName);
                if (AreaMansionMapping.ContainsKey(mansionAreaId))
                    continue;
                SysAreaMansion mansion = new SysAreaMansion
                {
                    AreaID = area.Id,
                    Name = mansionName,
                    Address = dcAddr != null ? Convert.ToString(dr[dcAddr]).Trim( ) : null,
                    Code = null,
                    AreaDepth = area.Depth,
                };
                mansion.Save( );
                AreaMansionMapping.Add(mansionAreaId, mansion);
            }
            result = true;
        lbl_return:
            return result;
        }
        public static bool ImportCompanyMansions(DataTable dt, out string message)
        {
            message = string.Empty;
            bool result = false;
            var dcCompanyTel = (DataColumn)null;
            var dcAreaName = (DataColumn)null;
            var dcName = (DataColumn)null;
            var dcAddr = (DataColumn)null;
            if (dt.Columns.Contains("商家账号"))
                dcCompanyTel = dt.Columns["商家账号"];
            if (dt.Columns.Contains("所属片区"))
                dcAreaName = dt.Columns["所属片区"];
            if (dt.Columns.Contains("派送范围"))
                dcName = dt.Columns["派送范围"];
            if (dt.Columns.Contains("大厦地址"))
                dcAddr = dt.Columns["大厦地址"];
            if (dcAreaName == null || dcName == null || dcCompanyTel == null)
            {
                message = "上传的excel 文档必须包含有 商家账号,所属片区,派送范围三列信息";
                goto lbl_return;
            }

            var areaDict = AreaBLL.AreaNameMapping;
            var mansionDict = AreaMansionMapping;
            Dictionary<string, int> mealCompanyDict = new Dictionary<string, int>( );

            string areaName, mansionName, companyTel;
            int companyId;
            SysAreaMansion mansion;
            foreach (DataRow dr in dt.Rows)
            {
                if (string.IsNullOrEmpty(companyTel = Utilities.ToDecimal(dr[dcCompanyTel]).ToString("0")))
                    continue;
                if (string.IsNullOrEmpty(areaName = Utilities.ToString(dr[dcAreaName]).Trim( )))
                    continue;
                if (string.IsNullOrEmpty(mansionName = Utilities.ToString(dr[dcName]).Trim( )))
                    continue;
                if (!areaDict.ContainsKey(areaName))
                    continue;
                mansion = GetAreaMansion(areaName, mansionName);
                if (mansion == null)
                    continue;
                if (mealCompanyDict.ContainsKey(companyTel))
                    companyId = mealCompanyDict[companyTel];
                else
                {
                    companyId = CompanyBLL.GetCompanyIdByTel(companyTel, CompanyType.MealCompany);
                    if (companyId == 0)
                        continue;
                    mealCompanyDict.Add(companyTel, companyId);
                }
                TryAddCompanyMansion(mansion.Id, companyId);
                //companyMansionId = CompanyMansionId.GetCompanyMansionId(company.Id, mansion.Id);
                //if (companyMansionDict.ContainsKey(companyMansionId))
                //    continue;
                //SysCompanyMansion companyMansion = new SysCompanyMansion( );
                //companyMansion.MansionID = companyMansionId.MansionId;
                //companyMansion.CompanyID = companyMansionId.CompanyId;
                //companyMansion.Save( );
                //companyMansionDict.Add(companyMansionId, companyMansion);
            }
            result = true;
        lbl_return:
            return result;
        }
    }
}