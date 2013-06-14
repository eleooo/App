using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.SiteAppPage
{
    public partial class pupSale : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Utilities.ToInt(Request.Params["id"]);
            Order order = Order.FetchByID(id);
            if (order != null)
            {
                this.txtOrderSumOk.Text = Utilities.ToDecimal(order.OrderSumOk).ToString("#########0.00"); ;
                this.txtOrderRate.Text = (Utilities.ToDecimal(order.OrderRateSale) * 10M).ToString("##0.00");
                this.txtOrderPay.Text = order.OrderPay.ToString( );
                this.txtOrderPayCash.Text = order.OrderPayCash.ToString( );
                this.txtOrderPayPoint.Text = order.OrderPayPoint.ToString( );
            }
        }
    }
}