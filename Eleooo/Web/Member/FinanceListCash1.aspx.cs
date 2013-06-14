using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class FinanceListCash1 : ActionPage
    {
        const string MOD_ROW_TEMPLATE = @"<tr class='os' align='middle'>
                                            {0}
                                          </tr>";
        const string SIG_ROW_TEMPLATE = @"<tr>
                                            {0}
                                          </tr>";
        private decimal dOrderPaySum = 0;
        private string _orderDesc;
        protected string OrderDesc
        {
            get
            {
                if (string.IsNullOrEmpty(_orderDesc))
                {
                    _orderDesc = ResBLL.GetRes("MemberCashOrderDesc", "在[{0}]消费{1}元,其中现金支付{2}元", "会员现金明细消费信息");
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
            var query = DB.Select(Utilities.GetTableColumns(Order.Schema), SysCompany.Columns.CompanyName)
                          .From<Order>( )
                          .InnerJoin(SysCompany.IdColumn, Order.OrderSellerIDColumn)
                          .Where(Order.OrderDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(Order.OrderMemberIDColumn).IsEqualTo(CurrentUser.Id)
                          .OrderDesc(Utilities.GetTableColumn(Order.IdColumn));
            gridView.DataSource = query;
            gridView.AddShowColumn(Order.OrderDateColumn)
                    .AddCustomColumn("OrderDesc", "消费信息")
                    .AddShowColumn(Order.OrderPayColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.OnDataBindHeader += new Web.Controls.DataBindHeaderHandle(gridView_OnDataBindHeader);
            gridView.OnDataBindRow += new Web.Controls.DataBindRowHandler(gridView_OnDataBindRow);
            gridView.OnDataBindFooter += new Web.Controls.DataBindFooterHandler(gridView_OnDataBindFooter);
            gridView.DataBind( );
        }

        string gridView_OnDataBindFooter( )
        {
            return string.Format(footerTemplate.InnerHtml, gridView.ColumnCount, dOrderPaySum);
        }

        string gridView_OnDataBindRow(int rowIndex, System.Data.DataRow rowData, ref bool isRenderedRow)
        {
            isRenderedRow = false;
            int mod;
            Math.DivRem(rowIndex + 1, 2, out mod);
            if (mod == 0)
                return MOD_ROW_TEMPLATE;
            else
                return SIG_ROW_TEMPLATE;
        }

        void gridView_OnDataBindHeader(string column, ref string caption, ref bool isRenderedCell)
        {
            if (column == "OrderPay")
                caption = "备注";
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "OrderDesc":
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, string.Format(OrderDesc, rowData[SysCompany.Columns.CompanyName], rowData[Order.Columns.OrderSumOk], rowData[Order.Columns.OrderPay]));
                    dOrderPaySum += Convert.ToDecimal(rowData[Order.Columns.OrderPay]);
                    isRenderedCell = true;
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}