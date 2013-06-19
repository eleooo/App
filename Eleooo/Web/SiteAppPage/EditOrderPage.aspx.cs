using System;
using System.Collections.Generic;
using Eleooo.DAL;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.SiteAppPage
{
    public partial class EditOrderPage : System.Web.UI.Page
    {
        private string _addrFloor;
        private string _addrRoom;
        private string _addrSeat;
        private string _initData;
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
        protected string GetMansionName( )
        {
            return MansionBLL.GetMansionNameByID(Meal.MansionId.Value);
        }
        protected string GetMemberPhone( )
        {
            string vSql = "select top 1 memberphoneNumber from sys_member where id=" + Meal.OrderMemberID.ToString( );
            QueryCommand cmd = new QueryCommand(vSql);
            object v = DataService.ExecuteScalar(cmd);
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
        protected string AddrFloor { get { return _addrFloor; } }
        protected string AddrRoom { get { return _addrRoom; } }
        protected string AddrSeat { get { return _addrSeat; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            var data = OrderMealBLL.GetOrderDetailByOrder(Meal);
            if (Meal != null)
            {
                lblCompanyItem.Visible = Meal.OrderType == (int)OrderType.CompanyItem;
                lblMealItem.Visible = !lblCompanyItem.Visible;
                rpOrderDetail.DataSource = data.Values;
                rpOrderDetail.DataBind( );
            }
            if (Meal == null)
            {
                Response.Write("订单不存在");
                this.Visible = false;
                return;
            }
            else if (!AppContext.Context.User.AdminRoleId.HasValue || AppContext.Context.User.AdminRoleId.Value <= 0)
            {
                lblMessage.InnerHtml = "你无权查看此页.";
                return;
            }
            else if (Meal.OrderStatus == (int)OrderStatus.Completed)
            {
                lblMessage.InnerHtml = "此订单已经订餐成功.";
                return;
            }
            else if (Meal.OrderStatus == (int)OrderStatus.Canceled)
            {
                lblMessage.InnerHtml = "此订单已经取消.";
                return;
            }
            var companyName = DB.Select(SysCompany.CompanyNameColumn.QualifiedName).From<SysCompany>( )
                                .Where(SysCompany.IdColumn).IsEqualTo(Meal.OrderSellerID)
                                .ExecuteScalar( );
            rpDetail.DataSource = data.Values;
            rpDetail.DataBind( );
            Dictionary<string, object> initData = new Dictionary<string, object>( );
            initData.Add("orderId", Meal.Id);
            initData.Add("orderSum", Meal.OrderSum);
            initData.Add("serviceSum", !Meal.ServiceSum.HasValue ? 0 : Meal.ServiceSum.Value);
            initData.Add("companyName", companyName);
            initData.Add("orderSessionVal", Meal.OrderUpdateOn.Value.Ticks.ToString());
            initData.Add("orders", data.ToDictionary(pair => pair.Key.ToString( ), pair => pair.Value));
            _initData = Utilities.ObjToJSON(initData);
            tblReply.Visible = true;
            Utilities.GetAddrFloorRoom(Meal.OrderProduct,out _addrSeat, out _addrFloor,out  _addrRoom);
        }
        protected string GetInitData( )
        {
            return _initData;
        }

        //protected DateTime GetMealCurrentDate( )
        //{
        //    var status = Meal.OrderStatus;
        //    if (status == (int)OrderStatus.Completed)
        //        return Meal.OrderDateDeliver;
        //    else if (status == (int)OrderStatus.Canceled)
        //        return (Meal.OrderUpdateOn ?? Meal.ModifiedOn).Value;
        //    else
        //        return DateTime.Now;
        //}

        protected string GetTimespan()
        {
            return OrderMealBLL.GetOrderTimespan(Meal);
        }
    }
}