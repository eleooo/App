using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.SiteAppPage
{
    public partial class ViewOrderStatus : System.Web.UI.Page
    {
        System.Data.DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            int orderId = Utilities.ToInt(Request.Params["OrderId"]);
            dt = OrderProgressBLL.GenOrderProgress(orderId);
            rpStatus.DataSource = dt;
            rpStatus.DataBind( );
            var status = GetOrderStatus(orderId);
            autoRun.Visible = status < (int)OrderStatus.Canceled;
        }
        int GetOrderStatus(int orderId)
        {
            SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("select top 1 OrderStatus from   orders where  id=@ID;");
            cmd.AddParameter("@ID", orderId, System.Data.DbType.Int32);
            return Utilities.ToInt(SubSonic.DataService.ExecuteScalar(cmd));
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
    }
}