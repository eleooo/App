using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class CompanyItemUsed : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DateTime dt = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day));
                this.txtDateStart.Value = dt.ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = dt.AddMonths(2).AddDays(-1).ToString("yyyy-MM-dd");
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            CompanyItemBLL.UpdateExpireItem( );
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            gridView.DataSource = this.GetAllQuery(dtBegin, dtEnd, filterMemberTel, filterCompanyTel);
            gridView.AddCustomColumn(SysMemberItem.OrderDateColumn.ColumnName, "成交时间")
                    .AddShowColumn(SysCompanyItem.ItemTitleColumn)
                    .AddCustomColumn(SysCompanyItem.Columns.ItemSum, "原价")
                    .AddCustomColumn(SysCompanyItem.ItemPointColumn.ColumnName, "积分兑换")
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(SysMemberItem.OrderSumColumn)
                    .AddCustomColumn(SysMemberItem.ItemDateColumn.ColumnName, "预计到店时间")
                    .AddShowColumn(SysMemberItem.ItemStatusColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "ItemTitle":
                    result = string.Format("<div style=\"text-align:left\">{0}</div>", Formatter.SubStr(rowData[column], 10));
                    break;
                case "ItemStatus":
                    result = CompanyItemBLL.GetItemStatusText(rowData);
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        SubSonic.SqlQuery GetAllQuery(DateTime dtBegin, DateTime dtEnd, string memberFilter, string companyFilter)
        {
            var query = DB.Select(Utilities.GetTableColumns(SysMemberItem.Schema),
                            SysCompanyItem.Columns.ItemTitle,
                            Utilities.GetTableColumn(SysCompanyItem.ItemSumColumn),
                            Utilities.GetTableColumn(SysCompany.CompanyTelColumn),
                            Utilities.GetTableColumn(SysMember.MemberPhoneNumberColumn))
                           .From<SysMemberItem>( )
                          .InnerJoin(SysCompanyItem.ItemIDColumn, SysMemberItem.CompanyItemIDColumn)
                          .InnerJoin(SysMember.IdColumn, SysMemberItem.MemberIDColumn)
                          .InnerJoin(SysCompany.IdColumn, SysMemberItem.CompanyIDColumn)
                          .Where(SysCompanyItem.ItemDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                          .And(SysCompanyItem.ItemEndDateColumn).IsLessThanOrEqualTo(dtEnd)
                          .AndEx(SysMember.MemberPhoneNumberColumn).Like(memberFilter)
                          .Or(SysMember.MemberFullnameColumn).Like(memberFilter)
                          .CloseEx( )
                          .AndEx(SysCompany.CompanyTelColumn).Like(companyFilter)
                          .Or(SysCompany.CompanyNameColumn).Like(companyFilter)
                          .CloseEx( )
                          .OrderDesc("Sys_Member_Item.ItemID");
            return query;
        }
    }
}