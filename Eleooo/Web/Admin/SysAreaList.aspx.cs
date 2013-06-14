using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysAreaList : ActionPage
    {
        private char[] separator = { '/' };
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            var query = DB.Select( ).From<SysArea>( )
                          .OrderAsc(SysArea.Columns.Depth, SysArea.Columns.Id);
            gridView.DataSource = query;
            gridView.AddShowColumn(SysArea.IdColumn)
                    .AddShowColumn(SysArea.AreaNameColumn)
                    .AddShowColumn(SysArea.AreaCodeColumn)
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "Area_Name":
                    isRenderedCell = true;
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, GetAreaSpace(rowData));
                    break;
                case "Action":
                    var id = rowData["ID"];
                    result = string.Concat("[", string.Format(ACTION_DLG_INDEX_TEMPLATE, id, "新建下级",0), "]&nbsp;&nbsp;[",
                                            string.Format(ACTION_DLG_INDEX_TEMPLATE, id, "编辑",1), "]&nbsp;&nbsp;[",
                                           string.Format(ACTION_DEL_TEMPLATE, id, "删除"),"]");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        private string GetAreaSpace(System.Data.DataRow rowData)
        {
            string sSpace = string.Empty; ;
            string sDepth = Convert.ToString(rowData["Depth"]);
            for (int i = 2; i <= sDepth.Split(separator, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                sSpace = string.Concat(sSpace, "&nbsp;&nbsp;");
            return string.Format("{0}•&nbsp;{1}", sSpace, rowData["Area_Name"]);
        }

        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            SysArea area = SysArea.FetchByID(Utilities.ToInt(EVENTARGUMENT));
            if (area == null)
            {
                txtDesc.InnerHtml = string.Format("删除失败! ID {0}不存在!", EVENTARGUMENT);
            }
            else
            {
                var query = DB.Delete().From<SysArea>()
                              .Where(SysArea.DepthColumn).Like(area.Depth+"%");
                query.Execute();
                txtDesc.InnerHtml = "删除成功";
            }
            On_ActionQuery(sender, e);
        }

        
    }
}