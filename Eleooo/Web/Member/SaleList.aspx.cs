using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class SaleList : ActionPage
    {
        const string LINK_ORDER_VIEW = "<a href=\"/Member/OrderMealViewPage.aspx?OrderId={0}\" target=\"_blank\">{1}</a>";
        private decimal dOrderSumOk = 0;
        private decimal dOrderPoint = 0;
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

            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(Order.Schema), "SYS_COMPANY.CompanyName").From<Order>( )
                                         .InnerJoin(SysCompany.IdColumn, Order.OrderSellerIDColumn)
                                         .Where(Order.OrderDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                         .And(Order.OrderMemberIDColumn).IsEqualTo(AppContext.Context.User.Id)
                                         //.And(Order.OrderStatusColumn).IsNotEqualTo((int)OrderStatus.Canceled)
                                         .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                         .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                         .CloseEx( )
                                         .OrderDesc("Orders.Id");
            this.BindEvalHandler((item, exp, val) => string.Format("{0}%", (Utilities.ToDecimal(val) * 100M).ToString("####0.####")))
                .BindEvalHandler((item, exp, val) =>
                    {
                        var rowData = (item as System.Data.DataRowView);
                        return string.Format(ACTION_DLG_TEMPLATE, rowData[Order.Columns.Id], "[详细]");
                    })
                .BindEvalHandler((item, exp, val) => (Utilities.ToDecimal(val) * 10M).ToString("####0.0###"))
                .BindEvalHandler((item, exp, val) =>
                    {
                        var rowData = (item as System.Data.DataRowView);
                        OrderType oType = Formatter.ToEnum<OrderType>(rowData[Order.OrderTypeColumn.ColumnName]);
                        OrderStatus oStatus = Formatter.ToEnum<OrderStatus>(rowData[Order.OrderStatusColumn.ColumnName]);
                        if (oType != OrderType.Common)
                            return string.Format(LINK_ORDER_VIEW, rowData[Order.Columns.Id],val);
                        else
                            return val;
                    });
            view.ItemCreated += new RepeaterItemEventHandler(view_ItemCreated);
            view.QuerySource = query;
            view.DataBind( );
        }

        void view_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                var rowData = (e.Item.DataItem as System.Data.DataRowView).Row;
                dOrderSumOk += Utilities.ToDecimal(rowData[Order.Columns.OrderSumOk]);
                dOrderPoint += Utilities.ToDecimal(rowData[Order.Columns.OrderPoint]);
            }
            else if (e.Item.ItemType == ListItemType.Footer)
                e.Item.DataItem = new object[] { dOrderSumOk, dOrderPoint };
        }
        //string gridView_OnDataBind(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        //{
        //    string result = string.Empty;

        //    switch (column)
        //    {
        //        case "OrderRate":
        //            result = string.Format("{0}%", (Utilities.ToDecimal(rowData[column]) * 100M).ToString("####0.####"));
        //            break;
        //        case "OrderDate":
        //            result = Utilities.ToDate(rowData[column]);
        //            dOrderSumOk += Convert.ToDecimal(rowData[Order.Columns.OrderSumOk]);
        //            dOrderPoint += Convert.ToDecimal(rowData[Order.Columns.OrderPoint]);
        //            break;
        //        case "OrderRateSale":
        //            result = (Convert.ToDecimal(rowData[column]) * 10M).ToString("####0.0###");
        //            break;
        //        case "Action":
        //            OrderType oType = Formatter.ToEnum<OrderType>(rowData[Order.OrderTypeColumn.ColumnName]);
        //            OrderStatus oStatus = Formatter.ToEnum<OrderStatus>(rowData[Order.OrderStatusColumn.ColumnName]);
        //            if (oType != OrderType.Common)
        //            {
        //                if (oStatus == OrderStatus.Canceled || oStatus == OrderStatus.Completed)
        //                    result = string.Format(ACTION_DLG_INDEX_TEMPLATE, rowData[Order.Columns.Id], "[详细]", 1);
        //                else
        //                    result = string.Format(LINK_ORDER_VIEW, rowData[Order.Columns.Id]);
        //            }
        //            else
        //                result = string.Format(ACTION_DLG_TEMPLATE, rowData[Order.Columns.Id], "[详细]");
        //            break;
        //        default:
        //            result = Utilities.ToHTML(rowData[column]);
        //            break;
        //    }
        //    return result;
        //}
    }
}