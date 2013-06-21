using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eleooo.DAL;
using System.Data;
using Newtonsoft.Json;
using Eleooo.Web;
using Eleooo.Common;

namespace Eleooo.BLL.Services
{
    class MealMenuHandler : IHandlerServices
    {
        private static readonly int _PageSize = 10;

        private static readonly Dictionary<string, Func<System.Web.HttpContext, Common.ServicesResult>> _MealMenuCommands;
        private static bool TryGetMenuItem(System.Web.HttpContext context, out SysTakeawayMenu menu, out string message)
        {

            menu = SysTakeawayMenu.FetchByID(Utilities.ToInt(context.Request["id"]));
            if (menu == null)
            {
                message = "菜单不存在,可能参数有误.";
                return false;
            }
            if (menu.CompanyID != AppContextBase.Context.Company.Id)
            {
                message = "非法操作.";
                return false;
            }
            message = string.Empty;
            return true;
        }

        static MealMenuHandler()
        {
            _MealMenuCommands = new Dictionary<string, Func<System.Web.HttpContext, ServicesResult>>();
            _MealMenuCommands.Add("chg", (context) =>
                {
                    int code = -1;
                    string message;
                    object result = null;
                    SysTakeawayMenu menu;
                    if (!TryGetMenuItem(context, out menu, out message))
                        goto lbl_return;
                    var price = Utilities.ToDecimal(context.Request["v"]);
                    menu.Price = price;
                    AppContextBase.Context.Company.MenuDate = DateTime.Now;
                    menu.Save();
                    AppContextBase.Context.Company.Save();
                    code = 0;
                    message = "修改成功";
                    result = menu.ToDictionary<SysTakeawayMenu>();
                lbl_return:
                    return Common.ServicesResult.GetInstance(code, message, result);
                });
            _MealMenuCommands.Add("out", (context) =>
                {
                    int code = -1;
                    string message;
                    object result = null;
                    SysTakeawayMenu menu;
                    if (!TryGetMenuItem(context, out menu, out message))
                        goto lbl_return;
                    var isout = Utilities.ToBool(context.Request["v"]);
                    menu.IsOutOfStock = isout;
                    if (isout)
                        menu.OutOfStockDate = DateTime.Now;
                    else
                        menu.OutOfStockDate = null;
                    AppContextBase.Context.Company.MenuDate = DateTime.Now;
                    menu.Save();
                    menu.Save();
                    AppContextBase.Context.Company.Save();
                    code = 0;
                    message = "修改成功";
                    result = menu.ToDictionary<SysTakeawayMenu>();
                lbl_return:
                    return Common.ServicesResult.GetInstance(code, message, result);
                });
            _MealMenuCommands.Add("del", (context) =>
                {
                    int code = -1;
                    string message;
                    SysTakeawayMenu menu;
                    if (!TryGetMenuItem(context, out menu, out message))
                        goto lbl_return;
                    menu.IsDeleted = true;
                    AppContextBase.Context.Company.MenuDate = DateTime.Now;
                    menu.Save();
                    menu.Save();
                    AppContextBase.Context.Company.Save();
                    code = 0;
                    message = "删除成功.";
                lbl_return:
                    return Common.ServicesResult.GetInstance(code, message, null);
                });
        }

        #region IHandlerServices 成员

        public Common.ServicesResult Get(System.Web.HttpContext context)
        {

            if (AppContextBase.Context.Company != null)
            {
                var p = Utilities.ToInt(context.Request["p"]);
                var q = context.Request["q"];
                var query = DB.Select(Utilities.GetTableColumns(SysTakeawayMenu.Schema),
                                      SysTakeawayDirectory.Columns.DirName)
                              .From<SysTakeawayMenu>()
                              .InnerJoin(SysTakeawayDirectory.IdColumn, SysTakeawayMenu.DirIDColumn)
                              .Where(SysTakeawayDirectory.CompanyIDColumn).IsEqualTo(AppContextBase.Context.Company.Id)
                              .And(SysTakeawayMenu.IsDeletedColumn).IsEqualTo(false)
                              .OrderAsc(SysTakeawayMenu.DirIDColumn.QualifiedName, SysTakeawayMenu.IdColumn.QualifiedName);
                if (!string.IsNullOrEmpty(q))
                {
                    q = Utilities.GetAllLikeQuery(q);
                    query.And(SysTakeawayMenu.NameColumn).Like(q);
                }
                var pageCount = Utilities.CalcPageCount(_PageSize, query.GetRecordCount());
                var menus = query.Paged(p, _PageSize).ExecuteDataTable();
                Utilities.LowerCaseDataTable(menus);
                return new Common.ServicesResult { data = new { pageCount = pageCount, menus = menus } };
            }
            else
                return new Common.ServicesResult { data = new { pageCount = 0 } };
        }

        public Common.ServicesResult Query(System.Web.HttpContext context)
        {
            object dt = null;
            SysCompany company = CompanyBLL.GetCompanyByTel(context.Request.Params["CompanyTel"]);
            if (company == null)
            {
                goto label_end;
            }
            var query = DB.Select("id as id", "DirName as name").From<SysTakeawayDirectory>()
                          .Where(SysTakeawayDirectory.CompanyIDColumn).IsEqualTo(company.Id);
            dt = query.ExecuteDataTable();
        label_end:
            return new Common.ServicesResult { data = dt };
        }

        public Common.ServicesResult Add(System.Web.HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Common.ServicesResult Edit(System.Web.HttpContext context)
        {
            var cmd = context.Request["cmd"];
            if (_MealMenuCommands.ContainsKey(cmd))
                return _MealMenuCommands[cmd](context);
            else
                return Common.ServicesResult.GetInstance(-1, "unkow command.", null);
        }

        public Common.ServicesResult Delete(System.Web.HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Common.ServicesResult Login(System.Web.HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Common.ServicesResult Logout(System.Web.HttpContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
