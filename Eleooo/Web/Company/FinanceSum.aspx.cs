using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class FinanceSum : ActionPage
    {
        decimal dCashSum = 0;
        decimal dSpareSum = 0;
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
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            var query = DB.Select(CompanyBLL.RenderUserPhone(SysMemberCash.CashMemberIDColumn, SysMemberCash.CashCompanyIDColumn, SysMember.MemberPhoneNumberColumn, SysMember.MemberCompanyIDColumn),
                                  SysMember.Columns.MemberFullname,
                                  "SUM(case when CashSum > 0 then CashSum else 0 end) as MemberCashSum",
                                  "SUM(case when CashSum > 0 then 1 else 0 end) as MemberCashCounter",
                                  "SUM(CashSum) as MemberCashSpareSum")
                                    .From<SysMemberCash>( ).InnerJoin(SysMember.IdColumn, SysMemberCash.CashMemberIDColumn)
                                    .Where(SysMemberCash.CashDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .AndEx(SysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                                    .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                                    .CloseEx( )
                                    .And(SysMemberCash.CashCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId);
            if (rblFlag.SelectedValue == "1")//本店
                query.ConstraintExpression(CompanyBLL.RenderIsOwner(SysMemberCash.CashMemberIDColumn,SysMemberCash.CashCompanyIDColumn, MemberFilter.Owner));
            else if (rblFlag.SelectedValue == "2") //外来
                query.ConstraintExpression(CompanyBLL.RenderIsOwner(SysMemberCash.CashMemberIDColumn, SysMemberCash.CashCompanyIDColumn, MemberFilter.Outer));
            
            query.DefaultPagingSort = "MemberPhoneNumber";
            query.ConstraintExpression(string.Concat("Group By MemberPhoneNumber,MemberFullname, ",CompanyBLL.RenderUserPhone(SysMemberCash.CashMemberIDColumn, SysMemberCash.CashCompanyIDColumn, SysMember.MemberPhoneNumberColumn, SysMember.MemberCompanyIDColumn,false)));
            gridView.DataSource = query;
            gridView.AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddCustomColumn("MemberCashSum", "储值金额")
                    .AddCustomColumn("MemberCashSpareSum", "储值余额")
                    .AddCustomColumn("MemberCashCounter", "储值次数");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
            lblSaleDesc.InnerHtml = string.Format("累计储值金额{0}元，储值余额{1}元 ", dCashSum, dSpareSum);
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "MemberCashSum":
                    decimal dSum = 0;
                    if (!Utilities.IsNull(rowData[column]))
                        dSum = Convert.ToDecimal(rowData[column]);
                    decimal dSpare = 0;
                    if (!Utilities.IsNull(rowData["MemberCashSpareSum"]))
                        dSpare = Convert.ToDecimal(rowData["MemberCashSpareSum"]);
                    dCashSum += dSum;
                    dSpareSum += dSpare;
                    result = dSum.ToString("0.00");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}