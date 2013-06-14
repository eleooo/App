using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.Common;
using Eleooo.DAL;
using SubSonic;

namespace Eleooo.Web.SiteAppPage
{
    public partial class ItemMenuInfo : System.Web.UI.Page
    {
        private static readonly string QueryCommpanyIdCommand = @"
declare @Sql nvarchar(100)
if Ascii(@ItemToken) = 48
   set @Sql = 'select ID from Sys_Company where CompanyTel ='+Right(@ItemToken,11)
else
   set @Sql = 'select CompanyID from Sys_Company_Item where ItemID ='+@ItemToken
exec sp_executeSql  @Sql;";
        protected int CompanyId
        {
            get
            {
                QueryCommand cmd = new QueryCommand(QueryCommpanyIdCommand);
                cmd.AddParameter("@ItemToken", Request["itemToken"]);
                return Utilities.ToInt(DataService.ExecuteScalar(cmd));
            }
        }
        protected object GetMenu( )
        {
            return MealMenuBLL.LoadCompanyMenu(CompanyId, false)
                              .GroupBy(dr => new KeyValuePair<object, object>(dr[SysTakeawayMenu.Columns.DirID], dr[SysTakeawayDirectory.Columns.DirName]),
                                       dr => new
                                       {
                                           id = dr[SysTakeawayMenu.Columns.Id],
                                           name = dr[SysTakeawayMenu.Columns.Name],
                                           price = dr[SysTakeawayMenu.Columns.Price]
                                       });
        }

        protected string GetItemInfo( )
        {
            var info = Request.QueryString["itemInfo"];
            if (!string.IsNullOrEmpty(info))
                return Server.UrlDecode(info).Replace(",", "','").Replace("[", "['").Replace("]", "']");
            else
                return "[]";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rpMenuDir.DataBind( );
        }
    }
}