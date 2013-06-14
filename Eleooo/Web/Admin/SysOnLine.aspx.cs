using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysOnLine : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            gridView.DataSource = DB.Select(SysMember.Columns.Id, SysMember.Columns.MemberFullname,
                                            SysMember.Columns.MemberPhoneNumber, SysMember.Columns.LastLoginDate, SysMember.Columns.CompanyId)
                                            .From<SysMember>( )
                                            .Where(SysMember.IdColumn).IsGreaterThan(0)
                                            .ConstraintExpression("AND (GETDATE() - LastLoginDate < 1.0/24.0)")
                                            .OrderDesc(SysMember.Columns.LastLoginDate);
            gridView.AddShowColumn(SysMember.IdColumn)
                    .AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(SysMember.LastLoginDateColumn)
                    .AddCustomColumn("MemberType", "会员类型");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData,ref bool isRenderedCell)
        {
            string result = string.Empty;
            
            switch (column)
            {
                case "MemberType":
                    int CompanyID = Utilities.ToInt(rowData[SysMember.Columns.CompanyId]);
                    result = CompanyID > 0 ? "商家" : "会员";
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}