using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcOrderMeal : UserControlBase
    {
        private Order _oldOrder;
        protected Order OldOrder
        {
            get
            {
                if (_oldOrder == null)
                    _oldOrder = Order.FetchByID(Request.Params["OrderId"]);
                return _oldOrder;
            }
        }
        private SysCompany _company;
        protected SysCompany Company
        {
            get
            {
                if (_company == null)
                {
                    if (OldOrder != null)
                        _company = SysCompany.FetchByID(OldOrder.OrderSellerID);
                    else
                        _company = SysCompany.FetchByID(Request.Params["CompanyId"]);
                }
                return _company;
            }
        }
        private int _mansionId;
        protected int MansionId
        {
            get
            {
                if (_mansionId <= 0)
                {
                    if (OldOrder != null)
                        _mansionId = OldOrder.MansionId.Value;
                    else
                        _mansionId = Utilities.ToInt(Request.Params["MansionId"]);
                }
                return _mansionId;
            }
        }
        private SysAreaMansion _mansion;
        protected SysAreaMansion Mansion
        {
            get
            {
                if (_mansion == null)
                    _mansion = SysAreaMansion.FetchByID(MansionId);
                return _mansion;
            }
        }
        protected string GetMenuUpdateDate( )
        {
            if (Company.MenuDate.HasValue)
                return Company.MenuDate.Value.Month.ToString( );
            else
                return string.Empty;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Company == null || CompanyBLL.GetCompanyType(Company.CompanyType) != CompanyType.MealCompany)
                goto lbl_redirect;
            if (_oldOrder != null && _oldOrder.OrderMemberID != AppContext.Context.User.Id)
                goto lbl_redirect;
            if (_oldOrder != null && _oldOrder.OrderType == (int)OrderType.CompanyItem)
            {
                var mItem = CompanyItemBLL.GetMemberMealItemByOrderId(_oldOrder.Id, _oldOrder.OrderMemberID);
                if (mItem == null)
                    goto lbl_redirect;
                else
                {
                    Utilities.Redirect(string.Format("/Member/OrderCompanyMealItem.aspx?MemberItemID={0}", _oldOrder.Id), false);
                    return;
                }
            }
            if (_oldOrder != null && Formatter.ToEnum<OrderType>(_oldOrder.OrderType.Value) != OrderType.OrderMeal)
                goto lbl_redirect;
            if (_oldOrder != null && (_oldOrder.OrderStatus == (int)OrderStatus.Canceled ||
                                     _oldOrder.OrderStatus == (int)OrderStatus.Commonn ||
                                     _oldOrder.OrderStatus == (int)OrderStatus.Completed))
                goto lbl_redirect;
            if (MansionId <= 0)
                goto lbl_redirect;
            faceBook.CompanyID = Company.Id;

            Dictionary<string, OrderMealMenu> menuDict = new Dictionary<string, OrderMealMenu>( );
            var dsMenu = MealMenuBLL.LoadCompanyMenu(Company.Id);
            rpMenuDir.DataSource = dsMenu.GroupBy(dr => new KeyValuePair<int, string>(Convert.ToInt32(dr[SysTakeawayMenu.DirIDColumn.ColumnName]), dr[SysTakeawayDirectory.DirNameColumn.ColumnName].ToString( )),
                                                  dr =>
                                                  {
                                                      var result = new OrderMealMenu
                                                      {
                                                          menuId = dr[SysTakeawayMenu.IdColumn.ColumnName],
                                                          menuName = dr[SysTakeawayMenu.NameColumn.ColumnName],
                                                          menuPrice = dr[SysTakeawayMenu.PriceColumn.ColumnName],
                                                          dirId = dr[SysTakeawayMenu.DirIDColumn.ColumnName]
                                                      };
                                                      menuDict[result.menuId.ToString( )] = result;
                                                      return result;
                                                  });
            rpMenuDir.DataBind( );
            InitData = Utilities.ObjToJSON(GetInitOrderMealData(menuDict));
            companyContentContainer.Visible = !string.IsNullOrEmpty(Company.CompanyContent);
            return;
        lbl_redirect:
            if (Company != null && CompanyBLL.GetCompanyType(Company.CompanyType) != CompanyType.MealCompany && MansionId > 0)
                Utilities.Redirect(string.Format("/Public/OrderMealPage.aspx?CompanyId={0}&MansionId={1}", Company.Id, MansionId), false);
            else
                Utilities.Redirect("/Public/OrderMealPage.aspx", false);
        }
        protected string InitData { get; set; }
        private Dictionary<string, object> GetInitOrderMealData(Dictionary<string, OrderMealMenu> menuDict)
        {
            bool isLogin = AppContext.Context.CurrentSubSys != SubSystem.ALL;
            int itemId = 0;
            string itemInfo = null;
            if (isLogin)
            {
                List<int> favCompanys = UserBLL.GetUserFavCompany(CurContext.User.Id);
                if (favCompanys.Contains(Company.Id))
                    favCompanyPic.Src = "/App_Themes/ThemesV2/images/cwdctsc.png";
                itemId = Utilities.ToInt(Request.QueryString["ItemID"]);
            }
            Dictionary<string, object> data = new Dictionary<string, object>( );
            Dictionary<string, object> userData = new Dictionary<string, object>( );
            data.Add("companyId", Company.Id);
            data.Add("isLogin", isLogin);
            data.Add("isWorkingTime", CompanyBLL.CheckIsWorkingTime(Company.CompanyWorkTime, Company.CompanyType.Value));
            data.Add("menus", menuDict);
            data.Add("userData", userData);
            data.Add("companyRate", RewardBLL.OrderMealRewardRate(Company) / 100M);
            data.Add("serviceSum", Company.ServiceSum.HasValue ? Company.ServiceSum.Value : 0);
            data.Add("onSetSum", Company.OnSetSum.HasValue ? Company.OnSetSum.Value : 0);
            data.Add("mansionId", MansionId);
            if (Mansion != null)
                data.Add("mansionName", this.Mansion.Name);
            if (_oldOrder != null)
            {
                //orderCount
                data.Add("orderId", _oldOrder.Id);
                var orderData = OrderMealBLL.GetOrderNonOutOfStockDetailByOrderId(_oldOrder.Id)
                                            .Select(detail =>
                                                {
                                                    OrderMealMenu m;
                                                    bool isCompanyItem = detail.MenuId.Value < 0 ;
                                                    if (!isCompanyItem && menuDict.TryGetValue(detail.MenuId.ToString( ), out m) &&
                                                        !MealDirIds.Contains(m.dirId))
                                                        MealDirIds.Add(m.dirId);
                                                    if (isCompanyItem)
                                                    {
                                                        itemId = -detail.MenuId.Value;
                                                        itemInfo = detail.ItemInfo;
                                                    }
                                                    return new
                                                    {
                                                        menuId = detail.MenuId,
                                                        amount = detail.OrderQty,
                                                        isCompanyItem = isCompanyItem
                                                    };
                                                }).ToList( );
                data["oldOrder"] = orderData;
            }

            if (isLogin)
            {
                //userData.Add("userFavAddress", UserBLL.GetUserFavAddress(AppContext.Context.User.Id).Select(address => new { name = address }).ToList( ));
                Order order = _oldOrder;
                string phone = AppContext.Context.User.MemberMsnPhone ?? AppContext.Context.User.MemberPhoneNumber;
                string floor, room, seat;
                var addr = order != null ? order.OrderProduct : HttpUtility.UrlDecode(Utilities.GetCookieValue("addr"), System.Text.Encoding.UTF8);
                if (string.IsNullOrEmpty(addr))
                {
                    var address = UserBLL.GetUserDefFavAddress(AppContext.Context.User.Id, MansionId);
                    if (address.HasValue)
                        addr = address.Value.name;
                }
                Utilities.GetAddrFloorRoom(addr, out seat, out floor, out room);
                userData["info"] = new
                {
                    txtUserPhone = phone,
                    txtSeat = seat,
                    txtFloor = floor,
                    txtRoom = room,
                    mansionId = order != null ? order.MansionId : 0,
                    txtUserMemo = _oldOrder != null ? _oldOrder.OrderMemo : string.Empty
                };
                data.Add("isCheckCode", MsnBLL.IsPhoneNumNeedCheck(phone));
                SysCompanyItem item;
                if (itemId > 0 && (item = SysCompanyItem.FetchByID(itemId)) != null)
                {
                    var items = Utilities.JSONToObj<List<int>>(itemInfo ?? item.ItemInfo);
                    bool isOutOfStock = false;
                    if (items != null)
                    {
                        items.ForEach<int>(m =>
                            {
                                isOutOfStock = !menuDict.ContainsKey(m.ToString( ));
                                return isOutOfStock;
                            });
                    }
                    if (!isOutOfStock)
                    {
                        menuDict.Add((-itemId).ToString( ), new OrderMealMenu
                        {
                            menuId = -itemId,
                            menuName = item.ItemTitle,
                            menuPrice = (item.ItemNeedPay ?? (decimal?)0).Value,
                            dirId = 0
                        });
                    }
                }
            }
            else
            {
                data.Add("isCheckCode", true);
                //userData.Add("userFavAddress", new object[0]);
            }
            data.Add("itemId", itemId);
            return data;
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
        protected string OrderMealRewardRate
        {
            get
            {
                return (RewardBLL.OrderMealRewardRate(Company) / 100M).ToString("#%");
            }
        }
        protected string GetDirAttribute(object dirId, int itemIndex)
        {
            if ((MealDirIds.Count == 0 && itemIndex == 0) || (MealDirIds.Contains(dirId)))
                return "show='true'";
            else
                return "show='false' style='display:none'";
        }
        protected int GetFaceBookRateCount( )
        {
            return faceBook.FbBadCount + faceBook.FbGoodCount + faceBook.FbNormalCount;
        }
        protected int GetOrderCount( )
        {
            return OrderMealBLL.GetOrderCount(Company.Id);
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
    }
    class OrderMealMenu
    {
        public object menuId { get; set; }
        public object menuName { get; set; }
        public object menuPrice { get; set; }
        public object dirId { get; set; }
    }
}