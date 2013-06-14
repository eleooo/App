using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class MemberGrade : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDesc.InnerHtml = string.Empty;
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            gridView.DataSource = DB.Select( ).From<SysCompanyMemberGrade>( )
                                    .Where(SysCompanyMemberGrade.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                                    .OrderAsc(SysCompanyMemberGrade.GradeOrderColumn.ColumnName);
            gridView.AddShowColumn(SysCompanyMemberGrade.GradeNameColumn)
                    .AddShowColumn(SysCompanyMemberGrade.GradeOrderColumn)
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "GradeName":
                    isRenderedCell = true;
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, rowData[column]);
                    break;
                case "Action":
                    result = string.Concat("[", string.Format(ACTION_DLG_TEMPLATE, rowData["ID"], "编辑"), "]&nbsp;&nbsp;[",
                                           string.Format(ACTION_DEL_TEMPLATE, rowData["ID"], "删除"), "]");
                    break;
                case "GradeOrder":
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            if (SysCompanyMemberGrade.Delete(EVENTARGUMENT) <= 0)
            {
                txtDesc.InnerHtml = string.Format("删除失败! {0}不存在!", EVENTARGUMENT);
            }
            else
            {
                txtDesc.InnerHtml = "删除成功";
            }
            On_ActionQuery(sender, e);
        }
    }
}