using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class MyCashDetail : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd" );
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            view.QuerySource = DB.Select( Utilities.GetTableColumns(SysMemberCash.Schema),SysCompany.Columns.CompanyName )
                                    .From<SysMemberCash>( )
                                    .InnerJoin(SysCompany.IdColumn, SysMemberCash.CashCompanyIDColumn)
                                    .Where(SysMemberCash.CashMemberIDColumn).IsEqualTo(AppContext.Context.User.Id)
                                    .And(SysMemberCash.CashDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .And(SysMemberCash.CashSumColumn).IsGreaterThan(0)
                                    .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                    .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                    .CloseEx( )
                                    .OrderDesc(SysMemberCash.Columns.CashDate);

            view.DataBind( );
        }
    }
}