using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class CompanyMyItems : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            CompanyItemBLL.UpdateExpireItem( );
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            var query = DB.Select(Utilities.GetTableColumns(SysMemberItem.Schema),
                                  SysCompany.Columns.CompanyName,
                                  SysCompanyItem.Columns.ItemTitle,
                                  SysCompanyItem.Columns.ItemPic,
                                  SysCompanyItem.Columns.ItemSum,
                                  SysCompany.Columns.CompanyType)
                           .From<SysMemberItem>( )
                           .InnerJoin(SysCompanyItem.ItemIDColumn, SysMemberItem.CompanyItemIDColumn)
                           .InnerJoin(SysCompany.IdColumn, SysMemberItem.CompanyIDColumn)
                           .Where(SysMemberItem.OrderDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                           .And(SysMemberItem.MemberIDColumn).IsEqualTo(CurrentUser.Id)
                           .And(SysCompany.CompanyTypeColumn).IsNotEqualTo((int)CompanyType.MealCompany);
            if (rbStatus.Text != "0")
                query.And(SysMemberItem.ItemStatusColumn).IsEqualTo(Convert.ToInt32(rbStatus.Text));
            query.DefaultPagingSort = Utilities.GetTableColumn(SysMemberItem.ItemIDColumn) + " DESC";
            BindEvalHandler((item, exp, val) =>
                {
                    var rowData = (item as System.Data.DataRowView).Row;
                    return GetLink(rowData);
                })
            .BindEvalHandler((item,exp,val)=>
                {
                    var rowData = (item as System.Data.DataRowView).Row;
                    return GetItemStatusText(rowData);
                });
            view.QuerySource = query;
            view.DataBind( );
        }
        string GetLink(System.Data.DataRow rowData)
        {
            string column = "ItemTitle";
            MemberCompanyItemStatus status = Formatter.ToEnum<MemberCompanyItemStatus>(rowData[SysMemberItem.Columns.ItemStatus]);
            if (status == MemberCompanyItemStatus.OutDate)
                return string.Format(orderOutdateLinkTemplate.InnerHtml, rowData[SysMemberItem.Columns.CompanyItemID], Formatter.SubStr(rowData[column], 20));
            else if (status == MemberCompanyItemStatus.Completed)
                return string.Format(orderedLinkTemplate.InnerHtml, rowData[SysMemberItem.Columns.CompanyItemID], Formatter.SubStr(rowData[column], 20));
            else
                return string.Format(orderLinkTemplate.InnerHtml, rowData[SysMemberItem.Columns.CompanyItemID], rowData[SysMemberItem.Columns.ItemID], Formatter.SubStr(rowData[column], 20));


        }
        string GetItemStatusText(System.Data.DataRow rowData)
        {
            return CompanyItemBLL.GetItemStatusText(rowData);
        }
    }
}