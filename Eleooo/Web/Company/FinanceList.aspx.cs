using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using SubSonic;
using System.Transactions;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class FinanceList : ActionPage
    {
        decimal dCashSum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
            txtMessage.InnerHtml = string.Empty;
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                int cashID = Utilities.ToInt(EVENTARGUMENT);
                string message;
                if (OrderBLL.DeleteMemberCash(cashID, AppContext.Context.Company, out message))
                {
                    OrderBLL.UpdateBalance( );
                    ts.Complete( );
                }
                txtMessage.InnerHtml = message;
            }
            catch (Exception ex)
            {
                Logging.Log("FinanceList->On_ActionDelete", ex, true);
                txtMessage.InnerHtml = ex.Message;
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(SysMemberCash.Schema),
                                 SysMember.Columns.MemberPhoneNumber,
                                 SysMember.Columns.MemberFullname)
                                    .From<SysMemberCash>( )
                                    .InnerJoin(SysMember.IdColumn, SysMemberCash.CashMemberIDColumn)
                                    .Where(SysMemberCash.CashDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .And(SysMemberCash.CashOrderIDColumn).IsEqualTo(0)
                                    .AndEx(SysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                                    .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                                    .CloseEx( )
                                    .And(SysMemberCash.CashCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                                    .OrderDesc(SysMemberCash.Columns.CashID);
            if (rblFlag.SelectedValue == "1")//本店
            {
                query.ConstraintExpression(CompanyBLL.RenderIsOwner(SysMemberCash.CashMemberIDColumn, SysMemberCash.CashCompanyIDColumn, MemberFilter.Owner));
            }
            else if (rblFlag.SelectedValue == "2") //外来
            {
                query.ConstraintExpression(CompanyBLL.RenderIsOwner(SysMemberCash.CashMemberIDColumn, SysMemberCash.CashCompanyIDColumn, MemberFilter.Outer));
            }
            gridView.DataSource = query;
            gridView.AddShowColumn(SysMemberCash.CashDateColumn)
                    .AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(SysMemberCash.CashSumColumn)
                    .AddShowColumn(SysMemberCash.CashRateColumn)
                    .AddShowColumn(SysMemberCash.CashRateSaleColumn)
                    .AddShowColumn(SysMemberCash.CashPointColumn)
                    .AddCustomColumn("Action", "操作");
            //.AddShowColumn(SysMemberCash.CashMemoColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
            PrintCashDesc(rblFlag.Text);
        }
        void PrintCashDesc(string flag)
        {
            if (flag == "1")
                lblSaleDesc.InnerHtml = string.Format("本店会员共计储值金额{0}元", dCashSum);
            else if (flag == "2")
                lblSaleDesc.InnerHtml = string.Format("外来会员共计储值金额{0}元", dCashSum);
            else
                lblSaleDesc.InnerHtml = string.Format("本期间共计储值金额{0}元", dCashSum);
        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "CashRate":
                    decimal rate = Utilities.ToDecimal(rowData[column]);
                    result = (rate * 10M).ToString("#####.####");
                    break;
                case "CashRateSale":
                    decimal rateSale = Utilities.ToDecimal(rowData[column]);
                    result = rateSale > 0 ? (rateSale).ToString("###0.###") + "%" : "";
                    break;
                case "CashDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "CashSum":
                    decimal dSum = Convert.ToDecimal(rowData[column]);
                    dCashSum += dSum;
                    result = dSum.ToString("######0.00");
                    break;
                case "Action":
                    result = string.Format(ACTION_DEL_TEMPLATE, rowData[SysMemberCash.Columns.CashID], "[删除]");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}