using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class OrderMealViewPage : ActionPage
    {
        private Order _meal;
        protected Order Meal
        {
            get
            {
                if (_meal == null)
                {
                    _meal = Order.FetchByID(Utilities.ToInt(Request.Params["OrderId"]));
                }
                return _meal;
            }
        }
        private Dictionary<object, OrdersDetail> _mealDetails;
        protected Dictionary<object, OrdersDetail> MealDetails
        {
            get
            {
                if (_mealDetails == null)
                    _mealDetails = OrderMealBLL.GetOrderNonOutOfStockDetailByOrderId(Meal.Id).ToDictionary(item => (object)item.MenuId);
                return _mealDetails;
            }
        }
        private List<object> _mealDirIds;
        private List<object> MealDirIds
        {
            get
            {
                if (_mealDirIds == null)
                    _mealDirIds = new List<object>( );
                return _mealDirIds;
            }
        }

        private SysCompany _company;
        protected SysCompany Company
        {
            get
            {
                if (_company == null && Meal != null)
                {
                    _company = SysCompany.FetchByID(Meal.OrderSellerID);
                }
                return _company;
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            this.IsUserLink = true;
            if (Company == null || CompanyBLL.GetCompanyType(Company.CompanyType) != CompanyType.MealCompany)
                goto lbl_redirect;
            if (_meal != null && Formatter.ToEnum<OrderType>(_meal.OrderType.Value) == OrderType.Common)
                goto lbl_redirect;
            if (_meal != null && _meal.OrderMemberID != AppContext.Context.User.Id)
                goto lbl_redirect;
            BindData( );
            firstOrder.Visible = CurrentUser.MemberPid.HasValue && CurrentUser.MemberPid.Value == -1 && OrderMealBLL.UserIsFirstOrder(Meal.OrderMemberID);
            afterFirstOrder.Visible = !firstOrder.Visible;
            return;
        lbl_redirect:
            Utilities.Redirect("/Public/OrderMealPage.aspx", false);
        }

        protected string GetDirAttribute(object dirId, int itemIndex)
        {
            if ((MealDirIds.Count == 0 && itemIndex == 0) || (MealDirIds.Contains(dirId)))
                return "show='true'";
            else
                return "show='false' style='display:none'";
        }
        private void BindData( )
        {
            var dsMenu = MealMenuBLL.LoadCompanyMenu(Company.Id);
            rpMenuDir.DataSource = dsMenu.GroupBy(dr => new KeyValuePair<int, string>(Convert.ToInt32(dr[SysTakeawayMenu.DirIDColumn.ColumnName]), dr[SysTakeawayDirectory.DirNameColumn.ColumnName].ToString( )),
                                      dr =>
                                      {
                                          if (MealDetails.ContainsKey(dr[SysTakeawayMenu.IdColumn.ColumnName]))
                                              MealDirIds.Add(dr[SysTakeawayMenu.DirIDColumn.ColumnName]);
                                          var result = new
                                          {
                                              menuId = dr[SysTakeawayMenu.IdColumn.ColumnName],
                                              menuName = dr[SysTakeawayMenu.NameColumn.ColumnName],
                                              menuPrice = dr[SysTakeawayMenu.PriceColumn.ColumnName],
                                              menuAmout = MealDetails.ContainsKey(dr[SysTakeawayMenu.IdColumn.ColumnName]) ? string.Concat("<b>", MealDetails[dr[SysTakeawayMenu.IdColumn.ColumnName]].OrderQty, "</b>") : null,
                                              menuCss = MealDetails.ContainsKey(dr[SysTakeawayMenu.IdColumn.ColumnName]) ? "gray_li" : null
                                          };
                                          return result;
                                      });
            rpMenuDir.DataBind( );
            List<int> favCompanys = UserBLL.GetUserFavCompany(AppContext.Context.User.Id);
            if (favCompanys.Contains(Company.Id))
                favCompanyPic.Src = "/App_Themes/ThemesV2/images/cwdctsc.png";
            rpDetail.DataSource = MealDetails.Values;
            rpDetail.DataBind( );
            faceBook.CompanyID = Company.Id;
        }
        protected string GetInitData( )
        {
            Dictionary<string, object> data = new Dictionary<string, object>( );
            data.Add("companyId", Company.Id);
            data.Add("orderId", Meal.Id);
            data.Add("orderSum", Meal.OrderSumOk);
            data.Add("orderPoint", Meal.OrderPoint);
            data.Add("mansionId", Meal.MansionId);
            if (Meal.OrderType == (int)OrderType.OrderMeal)
                data.Add("orderPointMsg", string.Format("您本次订餐获赠{0}个积分", Meal.OrderPrePoint));
            else
                data.Add("orderPointMsg", string.Format("您本次订餐花费{0}个积分", Meal.OrderPayPoint));
            data.Add("orderTimeout", Company.OrderElapsed);
            data.Add("orderStatus", GetStatus( ));
            return Utilities.ObjToJSON(data);
        }
        private int GetStatus( )
        {
            int nStatus = 0;
            DateTime dt = DateTime.Now;
            DateTime dtDate = Meal.OrderDate;
            var ts = dt - dtDate;
            if (Meal.OrderStatus == (int)OrderStatus.InProgress ||
                Meal.OrderStatus == (int)OrderStatus.NotStart || Meal.OrderStatus == (int)OrderStatus.Modified)
                nStatus = 1;
            else if (Meal.OrderStatus == (int)OrderStatus.Completed)
                nStatus = 0;
            else if (Meal.OrderDateDeliver <= dtDate && ts.TotalMinutes <= 75)
                nStatus = 1;
            if (Meal.IsNonOut.HasValue && Meal.IsNonOut.Value)
                nStatus = 0;
            if (Meal.HasOutOfStock.Value && Meal.HasOutOfStock.Value)
                nStatus = 0;
            return nStatus;
        }
        protected string GetMenuUpdateDate( )
        {
            if (Company.MenuDate.HasValue)
                return Company.MenuDate.Value.Month.ToString( );
            else
                return string.Empty;
        }
        protected void rpMenuDir_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                var rp = (Repeater)(e.Item.FindControl("rpMenu"));
                rp.DataSource = e.Item.DataItem;
                rp.DataBind( );
            }
        }
        protected string OrderMealRewardRate
        {
            get
            {
                return (RewardBLL.OrderMealRewardRate(Company) / 100M).ToString("#%");
            }
        }
        protected string FormatText(string val, string expression)
        {
            return Formatter.ReplaceQuote(val, expression);
        }
        protected string FormatText(object val, string expression)
        {
            if (Utilities.IsNull(val))
                return null;
            else
                return FormatText(val.ToString( ), expression);
        }
        protected string FormatMenuDirtext(object text)
        {
            return FormatText(text, "<span class=\"gray\">{0}</span>");
        }
        protected string FormatMenutext(object text)
        {
            return FormatText(text, "<em class=\"gray\">{0}</em>");
        }

        protected int GetFaceBookRateCount( )
        {
            return faceBook.FbBadCount + faceBook.FbGoodCount + faceBook.FbNormalCount;
        }
        protected int GetOrderCount( )
        {
            return OrderMealBLL.GetOrderCount(Company.Id);
        }
    }
}