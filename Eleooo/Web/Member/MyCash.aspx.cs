using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using SubSonic;

namespace Eleooo.Web.Member
{
    public partial class MyCash : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!base.IsPostBack)
            //{
            //    this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd" );
            //    this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            //}
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            //DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            //DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            var query = DB.Select(new Aggregate("( Case When [CashSum]>0 Then [CashSum] Else 0 End)", "CashSum", AggregateFunction.Sum),
                                            new Aggregate(SysMemberCash.CashSumColumn, "Balance", AggregateFunction.Sum),
                                            new Aggregate(SysCompany.CompanyNameColumn, SysCompany.Columns.CompanyName, AggregateFunction.GroupBy),
                                            new Aggregate(SysMemberCash.CashCompanyIDColumn, AggregateFunction.GroupBy))
                                    .From<SysMemberCash>( )
                                    .InnerJoin(SysCompany.IdColumn, SysMemberCash.CashCompanyIDColumn)
                                    .Where(SysMemberCash.CashMemberIDColumn).IsEqualTo(AppContext.Context.User.Id)
                                    //.And(SysMemberCash.CashDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                    .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                    .CloseEx( );
            query.DefaultPagingSort = SysMemberCash.Columns.CashCompanyID +" DESC";
            view.QuerySource = query;
            view.DataBind( );
        }
    }
}