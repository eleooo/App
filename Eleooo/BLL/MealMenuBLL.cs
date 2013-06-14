using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Linq;
using Eleooo.DAL;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class MealMenuBLL
    {
        public struct CompanyMenuDirId
        {
            public int CompanyId;
            public string DirName;
            public static CompanyMenuDirId GetCompanyMenuDirId(int companyId, string dirName)
            {
                CompanyMenuDirId dir;
                dir.CompanyId = companyId;
                dir.DirName = dirName;
                return dir;
            }
        }
        public struct CompanyMealMenuId
        {
            public string MenuName;
            public string DirName;
            public static CompanyMealMenuId GetCompanyMealMenuId(string dirName, string menuName)
            {
                CompanyMealMenuId menu;
                menu.MenuName = menuName;
                menu.DirName = dirName;
                return menu;
            }
        }
        [ThreadStatic]
        private static Dictionary<CompanyMenuDirId, int> _menuDirMapping;
        public static Dictionary<CompanyMenuDirId, int> MenuDirMapping
        {
            get
            {
                if (_menuDirMapping == null)
                    _menuDirMapping = new Dictionary<CompanyMenuDirId, int>( );
                return _menuDirMapping;
            }
        }

        [ThreadStatic]
        private static Dictionary<int, SysTakeawayMenu> _menuCaching;
        public static Dictionary<int, SysTakeawayMenu> MenuCaching
        {
            get
            {
                if (_menuCaching == null)
                    _menuCaching = new Dictionary<int, SysTakeawayMenu>( );
                return _menuCaching;
            }
        }

        [ThreadStatic]
        private static Dictionary<int, Dictionary<CompanyMealMenuId, SysTakeawayMenu>> _companyMealMenuMapping;

        public static Dictionary<CompanyMealMenuId, SysTakeawayMenu> GetMealDirMappingByCompanyId(int companyId)
        {
            if (_companyMealMenuMapping == null)
                _companyMealMenuMapping = new Dictionary<int, Dictionary<CompanyMealMenuId, SysTakeawayMenu>>( );
            if (!_companyMealMenuMapping.ContainsKey(companyId))
            {
                var dict = LoadCompanyMenu(companyId, true).ToDictionary(dr => CompanyMealMenuId.GetCompanyMealMenuId(dr[SysTakeawayDirectory.DirNameColumn.ColumnName].ToString( ), dr[SysTakeawayMenu.NameColumn.ColumnName].ToString( )),
                                                                         dr =>
                                                                         {
                                                                             SysTakeawayMenu m = new SysTakeawayMenu( );
                                                                             m.Load(dr);
                                                                             return m;
                                                                         });
                _companyMealMenuMapping.Add(companyId, dict);
                return dict;
            }
            return _companyMealMenuMapping[companyId];
        }

        public static IEnumerable<SysTakeawayDirectory> LoadMenuDirectory( )
        {
            using (var dr = SysTakeawayDirectory.FetchAll( ))
            {
                while (dr.Read( ))
                {
                    SysTakeawayDirectory dir = new SysTakeawayDirectory( );
                    dir.Load(dr);
                    yield return dir;
                }
            }
        }
        public static IEnumerable<SysTakeawayDirectory> LoadMenuDirectory(int companyID)
        {
            var query = DB.Select( ).From<SysTakeawayDirectory>( )
                          .Where(SysTakeawayDirectory.CompanyIDColumn).IsEqualTo(companyID);
            using (var dr = query.ExecuteReader( ))
            {
                while (dr.Read( ))
                {
                    SysTakeawayDirectory dir = new SysTakeawayDirectory( );
                    dir.Load(dr);
                    yield return dir;
                }
            }
        }

        public static IEnumerable<IDataReader> LoadCompanyMenu(int companyId, bool isLoadAll = false)
        {
            var query = DB.Select(Utilities.GetTableColumns(SysTakeawayMenu.Schema),
                                  SysTakeawayDirectory.DirNameColumn.ColumnName)
                          .From<SysTakeawayMenu>( )
                          .InnerJoin(SysTakeawayDirectory.IdColumn, SysTakeawayMenu.DirIDColumn)
                          .Where(SysTakeawayMenu.CompanyIDColumn).IsEqualTo(companyId);
            if (!isLoadAll)
            {
                query.And(SysTakeawayMenu.IsDeletedColumn).IsEqualTo(false)
                     .And(SysTakeawayMenu.IsOutOfStockColumn).IsEqualTo(false);
            }
            return query.GetDataReaderEnumerator( );
        }


        public static bool CheckCompanyMenuExist(int companyId, string menuName, int ignoreId)
        {
            var query = DB.Select( ).From<SysTakeawayMenu>( )
                          .Where(SysTakeawayMenu.IdColumn).IsNotEqualTo(ignoreId)
                          .And(SysTakeawayMenu.CompanyIDColumn).IsEqualTo(companyId)
                          .And(SysTakeawayMenu.NameColumn).IsEqualTo(menuName);
            return query.GetRecordCount( ) > 0;
        }
        private static int GetMenuDirByName(int companyId, string dirName)
        {
            var companyMenuDirId = CompanyMenuDirId.GetCompanyMenuDirId(companyId, dirName);
            if (MenuDirMapping.ContainsKey(companyMenuDirId))
                return MenuDirMapping[companyMenuDirId];
            SysTakeawayDirectory dir = DB.Select().From<SysTakeawayDirectory>()
                                         .Where(SysTakeawayDirectory.DirNameColumn).IsEqualTo(dirName)
                                         .And(SysTakeawayDirectory.CompanyIDColumn).IsEqualTo(companyId)
                                         .ExecuteSingle<SysTakeawayDirectory>();
            if (dir == null)
            {
                dir = new SysTakeawayDirectory( );
                dir.DirName = dirName;
                dir.CompanyID = companyId;
                dir.Save( );
            }
            MenuDirMapping.Add(companyMenuDirId, dir.Id);
            return dir.Id;
        }
        public static bool ImportCompanyMealMenu(DataTable dt, out string message)
        {
            bool result = false;
            message = string.Empty;
            DataColumn dcCompanyTel = null;
            DataColumn dcMenuName = null;
            DataColumn dcDirName = null;
            DataColumn dcPrice = null;
            DataColumn dcCode = null;
            if (dt.Columns.Contains("商家账号"))
                dcCompanyTel = dt.Columns["商家账号"];
            if (dt.Columns.Contains("餐点名称"))
                dcMenuName = dt.Columns["餐点名称"];
            if (dt.Columns.Contains("菜品系列"))
                dcDirName = dt.Columns["菜品系列"];
            if (dt.Columns.Contains("价格"))
                dcPrice = dt.Columns["价格"];
            if (dt.Columns.Contains("菜单编号"))
                dcCode = dt.Columns["菜单编号"];
            if (dcCompanyTel == null || dcMenuName == null)
            {
                message = "导入文件必须包含有商家账号和餐点名称二列信息.";
                goto lbl_return;
            }
            Dictionary<string, int> companyDict = new Dictionary<string, int>( );
            decimal dPrice; int dirId, companyId; string companyTel, menuName, dirName, menuCode;
            SysTakeawayMenu menu;
            int nCounter = 0;
            foreach (DataRow row in dt.Rows)
            {
                dirId = 0;
                dPrice = 0;
                menuCode = null;
                dirName = null;
                if (string.IsNullOrEmpty(companyTel = Utilities.ToDecimal(row[dcCompanyTel]).ToString("0")))
                    continue;
                if (companyDict.ContainsKey(companyTel))
                    companyId = companyDict[companyTel];
                else
                {
                    companyId = CompanyBLL.GetCompanyIdByTel(companyTel, CompanyType.MealCompany);
                    if (companyId == 0)
                        continue;
                    companyDict.Add(companyTel, companyId);
                }
                if (string.IsNullOrEmpty(menuName = Utilities.ToString(row[dcMenuName]).Trim( )))
                    continue;
                if (dcDirName != null && !string.IsNullOrEmpty(dirName = Utilities.ToString(row[dcDirName]).Trim( )))
                    dirId = GetMenuDirByName(companyId, dirName);
                if (dcCode != null)
                    menuCode = Utilities.ToString(row[dcCode]).Trim( );
                if (dcPrice != null)
                    dPrice = Utilities.ToDecimal(row[dcPrice]);
                //var dict = GetMealDirMappingByCompanyId(cmp.Id);
                //cmpMenuId = CompanyMealMenuId.GetCompanyMealMenuId(dirName, menuName);
                //if (dict.ContainsKey(cmpMenuId))
                //    menu = dict[cmpMenuId];
                //else
                menu = new SysTakeawayMenu( );
                menu.Name = menuName;
                menu.CompanyID = companyId;
                menu.DirID = dirId;
                menu.Price = dPrice;
                menu.IsDeleted = false;
                menu.Code = menuCode;
                menu.IsOutOfStock = false;
                menu.OutOfStockDate = null;
                menu.Save( );
                if (string.IsNullOrEmpty(menu.Code))
                {
                    menu.Code = menu.Id.ToString( );
                    menu.Save( );
                }
                //dict[cmpMenuId] = menu;
                nCounter++;
            }
            if (companyDict.Count > 0)
                UpdateCompanyMenuDate(companyDict.Values, DateTime.Now);
            message = "成功读取到" + nCounter.ToString( ) + "条菜单信息";
            result = true;
        lbl_return:
            return result;
        }
        private static SysTakeawayMenu GetCacheMenuItem(int menuId)
        {
            SysTakeawayMenu m;
            if (!MenuCaching.ContainsKey(menuId))
            {
                m = SysTakeawayMenu.FetchByID(menuId);
                if (m != null)
                    MenuCaching.Add(menuId, m);
            }
            else
                m = MenuCaching[menuId];
            return m;
        }
        public static bool ChangeMenuOutOfStokcStatus(object menuId, bool isOutOfStock)
        {
            var cmd = new QueryCommand("UPDATE dbo.Sys_Takeaway_Menu SET IsOutOfStock = @IsOutOfStock,OutOfStockDate = GETDATE() WHERE ID=@ID AND IsOutOfStock <> @IsOutOfStock;");
            cmd.AddParameter("@IsOutOfStock", isOutOfStock, DbType.Boolean);
            cmd.AddParameter("@ID", menuId, DbType.Int32);
            DataService.ExecuteQuery(cmd);
            return true;
        }
        public static bool ChangeMenuPrice(object menuId, decimal newPrice)
        {
            var cmd = new QueryCommand("UPDATE dbo.Sys_Takeaway_Menu SET Price = @Price WHERE ID=@ID AND Price <> @Price;");
            cmd.AddParameter("@Price", newPrice, DbType.Decimal);
            cmd.AddParameter("@ID", menuId, DbType.Int32);
            DataService.ExecuteQuery(cmd);
            return true;
        }
        public static int UpdateCompanyMenuDate(IEnumerable<int> ids, DateTime dtDate)
        {
            var sql = "Update Sys_Company Set MenuDate = @MenuDate Where Id in(" + string.Join(",", ids.Select(id => id.ToString( )).ToArray( )) + ");";
            var cmd = new QueryCommand(sql);
            cmd.AddParameter("@MenuDate", dtDate);
            return DataService.ExecuteQuery(cmd);
        }
    }
}