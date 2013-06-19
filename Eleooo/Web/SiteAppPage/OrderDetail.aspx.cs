using System;
using System.Collections.Generic;
using Eleooo.DAL;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.SiteAppPage
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        private string _addrFloor;
        private string _addrRoom;
        private string _addrSeat;
        private Order _meal;
        protected Order Meal
        {
            get
            {
                if (_meal == null)
                    _meal = Order.FetchByID(Utilities.ToInt(Request.Params["OrderId"]));
                return _meal;
            }
        }
        protected string AddrFloor { get { return _addrFloor; } }
        protected string AddrRoom { get { return _addrRoom; } }
        protected string AddrSeat { get { return _addrSeat; } }
        protected string GetMansionName( )
        {
            return MansionBLL.GetMansionNameByID(Meal.MansionId.Value);
        }
        protected string GetMemberPhone( )
        {
            string vSql = "select top 1 memberphoneNumber from sys_member where id=" + Meal.OrderMemberID.ToString( );
            QueryCommand cmd = new QueryCommand(vSql);
            object v= DataService.ExecuteScalar(cmd);
            if (Utilities.IsNull(v))
                return null;
            else
                return v.ToString( );
        }
        protected string GetCompanyPhone( )
        {
            string vSql = "select top 1 CompanyPhone from sys_company where id=" + Meal.OrderSellerID.ToString( );
            QueryCommand cmd = new QueryCommand(vSql);
            object v = DataService.ExecuteScalar(cmd);
            if (Utilities.IsNull(v))
                return null;
            else
                return v.ToString( );
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Meal != null)
            {
                lblCompanyItem.Visible = Meal.OrderType == (int)OrderType.CompanyItem;
                lblMealItem.Visible = !lblCompanyItem.Visible;
                rpDetail.DataSource = OrderMealBLL.GetOrderDetailByOrder(Meal).Values;
                rpDetail.DataBind( );
                Utilities.GetAddrFloorRoom(Meal.OrderProduct, out _addrSeat, out _addrFloor, out  _addrRoom);
            }
            else
            {
                Response.Write("参数错误");
                this.Visible = false;
            }
        }

        //protected void rpDir_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemIndex > -1)
        //    {
        //        Repeater rp = (Repeater)e.Item.FindControl("rpDetail");
        //        rp.DataSource = e.Item.DataItem;
        //        rp.DataBind( );
        //    }
        //}


        protected string GetTimespan( )
        {
            return OrderMealBLL.GetOrderTimespan(Meal);
        }
    }
}