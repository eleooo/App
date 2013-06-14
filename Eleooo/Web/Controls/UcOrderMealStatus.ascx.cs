using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcOrderMealStatus : System.Web.UI.UserControl
    {
        public bool IsNeedReload { get; set; }
        System.Data.DataTable dt;
        OrderStatus _status;
        protected void Page_Load(object sender, EventArgs e)
        {
            IsNeedReload = false;
            int orderId = Utilities.ToInt(Request.Params["OrderId"]);
            Order order = Order.FetchByID(orderId);
            if (order != null)
            {
                SysCompany company = SysCompany.FetchByID(order.OrderSellerID);
                dt = OrderProgressBLL.GenOrderProgress(orderId);
                _status = Formatter.ToEnum<OrderStatus>(order.OrderStatus);
                ShowCommandButton(dt.TableName, order, company);
                rpStatus.ItemCreated += new RepeaterItemEventHandler(rpStatus_ItemCreated);
                rpStatus.DataSource = dt;
                rpStatus.DataBind( );
            }
        }

        void rpStatus_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                var row = e.Item.DataItem as System.Data.DataRowView;
                if (Convert.ToInt32(row[OrderProgressBLL.IsCurrentColumn]) == 1)
                {
                    IsNeedReload = true;
                    OrderProgressBLL.UpdateLogCurrent(row[OrderProgressBLL.IdColumn], 0);
                }
            }
        }
        protected string GetItemCssClass(int index, object dataItem)
        {
            System.Data.DataRowView row = dataItem as System.Data.DataRowView;
            if (index == (dt.Rows.Count - 1))
            {
                var span = DateTime.Now - Convert.ToDateTime(row["Date"]);
                return span.TotalSeconds > 30 ? "class='hs'" : null;
            }
            return "class='hs'";
        }
        private void ShowCommandButton(string tblName, Order order, SysCompany company)
        {
            //不外送
            if (order.IsNonOut.HasValue && order.IsNonOut.Value)
                ShowWhenNonOut.Visible = true;
            else if (order.HasOutOfStock.Value && order.HasOutOfStock.Value && _status != OrderStatus.Canceled)
                ShowWhenOutOfStock.Visible = true;
            else
            {
                if (_status == OrderStatus.NotStart || _status == OrderStatus.Modified || 
                    (order.MsnType.HasValue && order.MsnType == 3 && _status == OrderStatus.InProgress))
                    ShowBeforConfirm.Visible = true;
                else if (_status == OrderStatus.InProgress)
                    ShowAfterConfirm.Visible = true;
                else if (_status == OrderStatus.Canceled)
                    ShowWhenOrderCanceled.Visible = true;
            }
        }
    }
}