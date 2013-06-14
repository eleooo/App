using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class MealDirectoryList : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDesc.InnerHtml = string.Empty;
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            var query = DB.Select(Utilities.GetTableColumns(SysTakeawayDirectory.Schema), SysCompany.CompanyTelColumn.QualifiedName).From<SysTakeawayDirectory>( )
                                    .InnerJoin(SysCompany.IdColumn, SysTakeawayDirectory.CompanyIDColumn)
                                    .OrderDesc(Utilities.GetTableColumn(SysTakeawayDirectory.IdColumn));
            if (!string.IsNullOrEmpty(txtCompanyName.Value))
                query.Where(SysCompany.CompanyTelColumn).Like(Utilities.GetAllLikeQuery(txtCompanyName.Value));
            gridView.DataSource = query;
            gridView.AddShowColumn(SysTakeawayDirectory.IdColumn)
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(SysTakeawayDirectory.DirNameColumn)
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "Action":
                    var id = rowData["ID"];
                    result = string.Concat("[", string.Format(ACTION_DLG_INDEX_TEMPLATE, id, "编辑", 1), "]&nbsp;&nbsp;[",
                                           string.Format(ACTION_DEL_TEMPLATE, id, "删除"), "]");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            SysTakeawayDirectory dir = SysTakeawayDirectory.FetchByID(Utilities.ToInt(EVENTARGUMENT));
            if (dir == null)
            {
                txtDesc.InnerHtml = string.Format("删除失败! ID {0}不存在!", EVENTARGUMENT);
            }
            else
            {
                SysTakeawayDirectory.Delete(dir.Id);
                txtDesc.InnerHtml = "删除成功";
            }
            On_ActionQuery(sender, e);
        }
    }
}