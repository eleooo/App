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
    public partial class FinanceCashList1 : ActionPage
    {
        private decimal dPaySum = 0, dPaySum_Owner = 0, dPaySum_Outer = 0;
        private string _orderDesc;
        protected string OrderDesc
        {
            get
            {
                if (string.IsNullOrEmpty(_orderDesc))
                {
                    _orderDesc = ResBLL.GetRes("MemberCashOrderDesc", "<td style=\"text-align:left;\">在[{0}]消费{1}元,其中现金支付{2}元。</td>", "会员现金明细消费信息");
                }
                return _orderDesc;
            }
        }
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
            var query = GetFinanceCashQuery(rblFlag.SelectedValue, dtBegin, dtEnd, filterMemberTel);
            query.And(Order.Columns.OrderPay).IsGreaterThan(0);
            gridView.DataSource = query;
            gridView.AddShowColumn(Order.OrderDateColumn)
                    .AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddCustomColumn("OrderDesc", "消费信息")
                    .AddShowColumn(Order.OrderPayColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
            PrintCashDesc(rblFlag.Text);

        }
        void PrintCashDesc( string flag )
        {
            switch (flag)
            {
                case "1":
                    lblCashDesc.InnerText = string.Format("本店会员消费了{0}元", dPaySum_Owner);
                    break;
                case "2":
                    lblCashDesc.InnerText = string.Format("外来会员消费了{0}元", dPaySum_Outer);
                    break;
                default:
                    lblCashDesc.InnerText = string.Format("本期间共计收了{0}元现金，本店会员消费了{1}元，外来会员消费了{2}元 ", dPaySum, dPaySum_Owner, dPaySum_Outer);
                    break;
            }
        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "OrderDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "OrderDesc":
                    result = string.Format(OrderDesc, AppContext.Context.Company.CompanyName, rowData[Order.Columns.OrderSumOk], rowData[Order.Columns.OrderPay]);
                    decimal d = Utilities.ToDecimal(rowData[Order.Columns.OrderPay]);
                    dPaySum += d;
                    if (Utilities.ToInt(rowData[CompanyBLL.IS_OWNER]) == 1)
                        dPaySum_Owner += d;
                    else
                        dPaySum_Outer += d;
                    isRenderedCell = result.StartsWith("<td");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        SqlQuery GetFinanceCashQuery(string flag, DateTime dtBegin, DateTime dtEnd, string filter)
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
            var query = DB.Select(Utilities.GetTableColumns(Order.Schema),
                                  SysMember.Columns.MemberFullname,
                                  CompanyBLL.RenderUserPhone(Utilities.GetTableColumn(SysMember.IdColumn),
                                                             Utilities.GetTableColumn(Order.OrderSellerIDColumn),
                                                             Utilities.GetTableColumn(SysMember.MemberPhoneNumberColumn),
                                                             Utilities.GetTableColumn(SysMember.MemberCompanyIDColumn)),
                                  CompanyBLL.RenderIsOwner(Utilities.GetTableColumn(SysMember.IdColumn),
                                                              Utilities.GetTableColumn(Order.OrderSellerIDColumn)))
                          .From<Order>( )
                          .InnerJoin(SysMember.IdColumn, Order.OrderMemberIDColumn)
                          .Where(Order.OrderDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(Order.OrderSellerIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .AndEx(SysMember.MemberPhoneNumberColumn).Like(filter)
                          .Or(SysMember.MemberFullnameColumn).Like(filter)
                          .CloseEx( )
                          .OrderDesc(Utilities.GetTableColumn(Order.IdColumn));
            return query;
        }
        SqlQuery GetOuterQuery(DateTime dtBegin, DateTime dtEnd, string filter)
        {
            var query = DB.Select(Utilities.GetTableColumns(VFinancePayMemberOrder.Schema),
                                  SysMember.Columns.MemberFullname,
                                  CompanyBLL.RenderUserPhone(Utilities.GetTableColumn(SysMember.IdColumn),
                                                             Utilities.GetTableColumn(VFinancePayMemberOrder.OrderSellerIDColumn),
                                                             Utilities.GetTableColumn(SysMember.MemberPhoneNumberColumn),
                                                             Utilities.GetTableColumn(SysMember.MemberCompanyIDColumn)),
                                  CompanyBLL.RenderIsOwner("0"))
                          .From<VFinancePayMemberOrder>( )
                          .InnerJoin(SysMember.IdColumn, VFinancePayMemberOrder.OrderMemberIDColumn)
                          .Where(VFinancePayMemberOrder.OrderDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(VFinancePayMemberOrder.OrderSellerIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .AndEx(SysMember.MemberPhoneNumberColumn).Like(filter)
                          .Or(SysMember.MemberFullnameColumn).Like(filter)
                          .CloseEx( )
                          .OrderDesc(Utilities.GetTableColumn(VFinancePayMemberOrder.IdColumn));
            return query;
        }
        SqlQuery GetOwnerQuery(DateTime dtBegin, DateTime dtEnd, string filter)
        {
            var query = DB.Select(Utilities.GetTableColumns(Order.Schema),
                                  VSysMember.Columns.MemberFullname,
                                  VSysMember.Columns.MemberPhoneNumber,
                                  CompanyBLL.RenderIsOwner("1"))
                          .InnerJoin(VSysMember.IdColumn, Order.OrderMemberIDColumn)
                          .From<Order>( )
                          .Where(Order.OrderDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(Order.OrderSellerIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .And(VSysMember.MemberCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .AndEx(VSysMember.MemberPhoneNumberColumn).Like(filter)
                          .Or(VSysMember.MemberFullnameColumn).Like(filter)
                          .CloseEx( )
                          .OrderDesc(Utilities.GetTableColumn(Order.IdColumn));
            return query;
        }
    }
}