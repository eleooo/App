using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class CompanyItemUsed : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DateTime dt = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day));
                this.txtDateStart.Value = (AppContext.Context.User.MemberDate ?? (DateTime?)dt).Value.ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = dt.AddMonths(2).AddDays(-1).ToString("yyyy-MM-dd");
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            CompanyItemBLL.UpdateExpireItem( );
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            gridView.DataSource = GetCompanyItemQuery(rblFlag.SelectedValue, dtBegin, dtEnd, filterMemberTel);
            gridView.AddCustomColumn(SysMemberItem.OrderDateColumn.ColumnName, "成交时间")
                    .AddShowColumn(SysCompanyItem.ItemTitleColumn)
                    .AddCustomColumn(SysCompanyItem.Columns.ItemSum, "原价")
                    .AddCustomColumn(SysCompanyItem.ItemPointColumn.ColumnName, "积分兑换")
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddCustomColumn(SysCompany.Columns.CompanyName, "注册地点")
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
                    result = string.Format("<div style=\"text-align:left\">{0}</div>", Formatter.SubStr(rowData[column], 8));
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
        SqlQuery GetCompanyItemQuery(string flag, DateTime dtBegin, DateTime dtEnd, string filter)
        {
            if (flag == "2")
                return GetOuterQuery(dtBegin, dtEnd, filter);
            else if (flag == "1")
                return GetOwnerQuery(dtBegin, dtEnd, filter);
            else
                return GetAllQuery(dtBegin, dtEnd, filter);
        }
        SqlQuery GetAllQuery(DateTime dtBegin, DateTime dtEnd, string filter)
        {
            var query = DB.Select(Utilities.GetTableColumns(SysMemberItem.Schema),
                            SysCompanyItem.Columns.ItemTitle,
                            CompanyBLL.GetCompanyTypeAsCol(AppContext.Context.Company),
                            Utilities.GetTableColumn(SysCompanyItem.ItemSumColumn),
                            Utilities.GetTableColumn(SysCompany.CompanyNameColumn),
                            Utilities.GetTableColumn(SysCompanyItem.ItemPicColumn),
                            Utilities.GetTableColumn(SysMember.MemberPhoneNumberColumn))
                           .From<SysMemberItem>( )
                          .InnerJoin(SysCompanyItem.ItemIDColumn, SysMemberItem.CompanyItemIDColumn)
                          .InnerJoin(SysMember.IdColumn, SysMemberItem.MemberIDColumn)
                          .InnerJoin(SysCompany.IdColumn, SysMember.MemberCompanyIDColumn)
                           //.Where(SysMemberItem.OrderDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .Where(SysCompanyItem.ItemDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                          .And(SysCompanyItem.ItemEndDateColumn).IsLessThan(dtEnd)
                          .And(SysCompanyItem.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .AndEx(SysMember.MemberPhoneNumberColumn).Like(filter)
                          .Or(SysMember.MemberFullnameColumn).Like(filter)
                          .CloseEx( )
                          .OrderDesc("Sys_Member_Item.ItemID");
            return query;
        }
        SqlQuery GetOuterQuery(DateTime dtBegin, DateTime dtEnd, string filter)
        {
            var query = DB.Select(Utilities.GetTableColumns(VSysMemberItemOuter.Schema),
                            SysCompanyItem.Columns.ItemTitle,
                            CompanyBLL.GetCompanyTypeAsCol(AppContext.Context.Company),
                            Utilities.GetTableColumn(SysCompanyItem.ItemSumColumn),
                            Utilities.GetTableColumn(SysCompany.CompanyNameColumn),
                            Utilities.GetTableColumn(SysCompanyItem.ItemPicColumn),
                            Utilities.GetTableColumn(SysMember.MemberPhoneNumberColumn))
                                  .From<VSysMemberItemOuter>( )
                          .InnerJoin(SysCompanyItem.ItemIDColumn, VSysMemberItemOuter.Schema.GetColumn(VSysMemberItemOuter.Columns.CompanyItemID))
                          .InnerJoin(SysMember.IdColumn, VSysMemberItemOuter.Schema.GetColumn(VSysMemberItemOuter.Columns.MemberID))
                          .InnerJoin(SysCompany.IdColumn, SysMember.MemberCompanyIDColumn)
                          .Where(SysCompanyItem.ItemDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                          .And(SysCompanyItem.ItemEndDateColumn).IsLessThan(dtEnd)
                          .And(SysCompanyItem.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .AndEx(SysMember.MemberPhoneNumberColumn).Like(filter)
                          .Or(SysMember.MemberFullnameColumn).Like(filter)
                          .CloseEx( )
                          .OrderDesc("v_Sys_Member_Item_Outer.ItemID");
            return query;
        }
        SqlQuery GetOwnerQuery(DateTime dtBegin, DateTime dtEnd, string filter)
        {
            var query = DB.Select(Utilities.GetTableColumns(SysMemberItem.Schema),
                                    SysCompanyItem.Columns.ItemTitle,
                                    CompanyBLL.GetCompanyTypeAsCol(AppContext.Context.Company),
                                    Utilities.GetTableColumn(SysCompanyItem.ItemSumColumn),
                                    Utilities.GetTableColumn(VSysMember.MemberPhoneNumberColumn),
                                    Utilities.GetTableColumn(SysCompanyItem.ItemPicColumn),
                                    string.Format("'{0}' as {1}", AppContext.Context.Company.CompanyName, SysCompany.Columns.CompanyName)
                                  ).From<SysMemberItem>( )
                         .InnerJoin(SysCompanyItem.ItemIDColumn, SysMemberItem.CompanyItemIDColumn)
                          .InnerJoin(VSysMember.Schema.GetColumn(VSysMember.Columns.Id), SysMemberItem.MemberIDColumn)
                          //.Where(SysMemberItem.OrderDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .Where(SysCompanyItem.ItemDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                          .And(SysCompanyItem.ItemEndDateColumn).IsLessThan(dtEnd)
                          .And(SysCompanyItem.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .And(VSysMember.MemberCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .AndEx(VSysMember.Schema.GetColumn(VSysMember.Columns.MemberPhoneNumber)).Like(filter)
                          .Or(VSysMember.Schema.GetColumn(VSysMember.Columns.MemberFullname)).Like(filter)
                          .CloseEx( )
                          .OrderDesc("Sys_Member_Item.ItemID");
            return query;
        }
    }
}