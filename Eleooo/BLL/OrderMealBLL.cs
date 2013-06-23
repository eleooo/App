using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using Eleooo.DAL;
using SubSonic;
using System.Transactions;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class OrderMealBLL
    {
        private static readonly string ItemDetailMenusCmd = @"
declare @OrderQty INT,@ItemInfo nvarchar(max),@OrderSum float
Select top 1 @ItemInfo= replace(replace(ItemInfo,N'[',N''),N']',N''),@OrderQty = OrderQty,@OrderSum = [OrderPrice] from Orders_Detail where OrderId = @OrderId and MenuId < 0 and ItemInfo is not null
set @ItemInfo = ISNULL(@ItemInfo,N'0')
set @ItemInfo = N'select @OrderQty as OrderQty,@OrderSum as OrderSum,t2.DirName,t1.* from dbo.Sys_Takeaway_Menu as t1 inner join dbo.Sys_Takeaway_Directory as t2 on t1.DirID = t2.ID where t1.ID IN('+@ItemInfo+')'
exec sp_executesql @ItemInfo,N'@OrderQty INT,@OrderSum float',@OrderQty = @OrderQty,@OrderSum=@OrderSum";

        private const string COLOR_FORMAT = "<span style=\"color:{0}\">{1}</span>";
        private static readonly object __Locker = new object( );

        private static readonly Dictionary<int, object> __orderLocker = new Dictionary<int, object>( );
        private static readonly Dictionary<string, string> _orderStatusSoruce;
        private static readonly Dictionary<OrderStatus, string> _orderStatusFontColor;

        static OrderMealBLL( )
        {
            if (_orderStatusSoruce == null)
            {
                _orderStatusSoruce = new Dictionary<string, string>( );
                _orderStatusSoruce.Add("2", "待处理");
                _orderStatusSoruce.Add("3", "已修改");
                _orderStatusSoruce.Add("4", "处理中");
                _orderStatusSoruce.Add("5", "已取消");
                _orderStatusSoruce.Add("6", "订餐成功");
            }
            _orderStatusFontColor = new Dictionary<OrderStatus, string>( );
            _orderStatusFontColor.Add(OrderStatus.NotStart, "#FF0000");
            _orderStatusFontColor.Add(OrderStatus.Modified, "#FFC000");
            _orderStatusFontColor.Add(OrderStatus.InProgress, "green");
            _orderStatusFontColor.Add(OrderStatus.Canceled, "#000000");
            _orderStatusFontColor.Add(OrderStatus.Completed, "#000000");
        }

        public static object LockScopeAction(int orderId)
        {
            lock (__Locker)
            {
                object objLock;
                if (__orderLocker.TryGetValue(orderId, out objLock))
                    return objLock;
                else
                {
                    objLock = new object( );
                    __orderLocker.Add(orderId, objLock);
                    return objLock;
                }
            }
        }
        public static void RemoveLockScopeAction(int orderId)
        {
            lock (__Locker)
            {
                if (__orderLocker.ContainsKey(orderId))
                    __orderLocker.Remove(orderId);
            }
        }

        public static Dictionary<string, string> OrderStatusSoruce
        {
            get
            {
                return _orderStatusSoruce;
            }
        }

        public static void ClearOrderMenuDetail(int orderId)
        {
            string vSql = string.Format("Delete Orders_Detail Where OrderId =  {0};", orderId);
            QueryCommand cmd = new QueryCommand(vSql);
            DataService.ExecuteQuery(cmd);
        }
        public static IEnumerable<OrdersDetail> GetOutOfStockDeitalByOrderId(int orderId)
        {
            var query = DB.Select( ).From<OrdersDetail>( )
                                    .Where(OrdersDetail.OrderIdColumn).IsEqualTo(orderId)
                                    .And(OrdersDetail.IsOutOfStockColumn).IsEqualTo(true);
            using (var dr = query.ExecuteReader( ))
            {
                while (dr.Read( ))
                {
                    OrdersDetail d = new OrdersDetail( );
                    d.Load(dr);
                    yield return d;
                }
            }
        }
        public static Dictionary<object, OrderDetailItem> GetOrderItemMenus(int orderId)
        {
            Dictionary<object, OrderDetailItem> result = new Dictionary<object, OrderDetailItem>( );
            var cmd = new QueryCommand(ItemDetailMenusCmd);
            cmd.AddParameter("@OrderId", orderId, DbType.Int32);
            using (var dr = DataService.GetReader(cmd))
            {
                //decimal orderSum = 0, allSum = 0;
                bool isFirst = true;
                OrderDetailItem item;
                while (dr.Read( ))
                {
                    var id = Utilities.ToInt(dr[SysTakeawayMenu.Columns.Id]);
                    if (!result.TryGetValue(id, out item))
                    {
                        item = new OrderDetailItem
                        {
                            OrderSum = isFirst ? Utilities.ToDecimal(dr["OrderSum"]) : 0,
                            DirName = dr[SysTakeawayDirectory.Columns.DirName],
                            MenuName = dr[SysTakeawayMenu.Columns.Name],
                            MenuId = id,
                            OrderId = orderId,
                            OrderQty = Utilities.ToInt(dr[OrdersDetail.Columns.OrderQty]),
                            OrderPrice = Utilities.ToDecimal(dr[SysTakeawayMenu.Columns.Price]),
                            IsCompanyItem = true,
                            IsOutOfStock = Utilities.ToBool(dr[SysTakeawayMenu.Columns.IsOutOfStock])
                        };
                        result.Add(id, item);
                        isFirst = false;
                    }
                    else
                        item.OrderQty = item.OrderQty + Utilities.ToInt(dr[OrdersDetail.Columns.OrderQty]);
                }
            }
            return result;
        }
        public static Dictionary<object, OrderDetailItem> GetOrderDetailByOrder(Order order)
        {
            if (order == null)
                return new Dictionary<object, OrderDetailItem>( );
            var data = GetOrderItemMenus(order.Id);
            var query = DB.Select(Utilities.GetTableColumns(OrdersDetail.Schema),
                                  SysTakeawayDirectory.DirNameColumn.QualifiedName)
                           .From<OrdersDetail>( )
                           .InnerJoin(SysTakeawayMenu.IdColumn, OrdersDetail.MenuIdColumn)
                           .InnerJoin(SysTakeawayDirectory.IdColumn, SysTakeawayMenu.DirIDColumn)
                           .Where(OrdersDetail.OrderIdColumn).IsEqualTo(order.Id);
            query.GetDataReaderEnumerator( ).ForEach<IDataReader>(dr =>
                {
                    var menuId = Utilities.ToInt(dr[OrdersDetail.Columns.MenuId]);
                    OrderDetailItem item;
                    if (!data.TryGetValue(menuId, out item))
                    {
                        item = new OrderDetailItem
                        {
                            DirName = dr[SysTakeawayDirectory.Columns.DirName],
                            MenuName = dr[OrdersDetail.Columns.MenuName],
                            MenuId = menuId,
                            OrderId = order.Id,
                            OrderQty = Utilities.ToInt(dr[OrdersDetail.Columns.OrderQty]),
                            OrderPrice = Utilities.ToDecimal(dr[OrdersDetail.Columns.OrderPrice]),
                            IsCompanyItem = false,
                            IsOutOfStock = Utilities.ToBool(dr[OrdersDetail.Columns.IsOutOfStock])
                        };
                        item.OrderSum = item.OrderQty * item.OrderPrice;
                        data.Add(menuId, item);
                    }
                    else
                    {
                        item.OrderQty = item.OrderQty + Utilities.ToInt(dr[OrdersDetail.Columns.OrderQty]);
                        item.IsOutOfStock = Utilities.ToBool(dr[OrdersDetail.Columns.IsOutOfStock]);
                        item.OrderSum += Utilities.ToInt(dr[OrdersDetail.Columns.OrderQty]) * item.OrderPrice;
                    }
                    return true;
                });
            return data;
        }
        public static IEnumerable<OrdersDetail> GetOrderAllDetailByOrderId(int orderId)
        {
            var query = DB.Select( ).From<OrdersDetail>( )
                        .Where(OrdersDetail.OrderIdColumn).IsEqualTo(orderId);
            using (var dr = query.ExecuteReader( ))
            {
                while (dr.Read( ))
                {
                    OrdersDetail d = new OrdersDetail( );
                    d.Load(dr);
                    yield return d;
                }
            }
        }

        public static IEnumerable<OrdersDetail> GetOrderNonOutOfStockDetailByOrderId(int orderId)
        {
            var query = DB.Select(Utilities.GetTableColumns(OrdersDetail.Schema)).From<OrdersDetail>( )
                                    .Where(OrdersDetail.OrderIdColumn).IsEqualTo(orderId)
                                    .And(OrdersDetail.IsOutOfStockColumn).IsEqualTo(false)
                                    .OrderAsc(OrdersDetail.IdColumn.QualifiedName);
            using (var dr = query.ExecuteReader( ))
            {
                while (dr.Read( ))
                {
                    OrdersDetail d = new OrdersDetail( );
                    d.Load(dr);
                    yield return d;
                }
            }
        }
        public static Payment GetOrderMealPayment(Order order, int userId)
        {
            return DB.Select( ).Top("1").From<Payment>( )
                        .Where(Payment.PaymentOrderIDColumn).IsEqualTo(order.Id)
                        .And(Payment.PaymentCompanyIDColumn).IsEqualTo(UserBLL.MainCompanyAccount.Id)
                        .And(Payment.PaymentTypeColumn).IsEqualTo((int)PaymentType.ConsumeGive)
                        .And(Payment.PaymentMemberIDColumn).IsEqualTo(userId)
                        .ExecuteSingle<Payment>( );
        }
        public static bool UserIsFirstOrder(int userId)
        {
            var query = DB.Select( ).Top("1").From<Order>( )
              .Where(Order.OrderMemberIDColumn).IsEqualTo(userId)
              .And(Order.OrderTypeColumn).IsEqualTo((int)OrderType.OrderMeal)
              .And(Order.OrderStatusColumn).IsEqualTo((int)OrderStatus.Completed);
            return query.GetRecordCount( ) < 1;
        }
        public static Order GetUserLatestMealOrder(int userId)
        {
            var query = DB.Select( ).Top("1").From<Order>( )
                         .Where(Order.OrderMemberIDColumn).IsEqualTo(userId)
                         .And(Order.OrderTypeColumn).IsEqualTo((int)OrderType.OrderMeal)
                         .OrderDesc(Order.IdColumn.ColumnName);
            using (var dr = query.ExecuteReader( ))
            {
                Order order = null;
                if (dr.Read( ))
                {
                    order = new Order( );
                    order.Load(dr);
                }
                return order;
            }
        }

        public static SqlQuery GetOrderMealQuery(string txtCompany, string txtMember, int status, int model, string beginDate, string endDate, bool isViewAll)
        {
            DateTime dtBegin = Utilities.ToDateTime(beginDate);
            DateTime dtEnd = Utilities.ToDateTime(endDate).AddDays(1).Date;
            SqlQuery query = null;

            if (AppContextBase.Context.CurrentRole != (int)EleoooRoleDefine.Admin && !isViewAll)
            {
                query = DB.Select(Utilities.GetTableColumns(VMealOrder.Schema),
                             SysCompany.CompanyNameColumn.QualifiedName,
                             SysCompany.CompanyPhoneColumn.QualifiedName,
                             SysMember.MemberPhoneNumberColumn.QualifiedName)
                          .From<VMealOrder>( )
                          .InnerJoin(SysCompany.IdColumn, VMealOrder.OrderSellerIDColumn)
                          .InnerJoin(SysMember.IdColumn, VMealOrder.OrderMemberIDColumn)
                          .Where(VMealOrder.OrderTypeColumn).IsNotEqualTo((int)OrderType.Common)
                          .And(VMealOrder.OrderDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                          .And(VMealOrder.OrderDateColumn).IsLessThanOrEqualTo(dtEnd)
                          .And(VMealOrder.OrderModelColumn).IsEqualTo(model)
                          .And(SysCompany.CompanyTypeColumn).IsEqualTo((int)CompanyType.MealCompany)
                          .OrderDesc(VMealOrder.OrderDateColumn.QualifiedName)
                          .AndEx(VMealOrder.OrderOperColumn).IsEqualTo(AppContextBase.Context.User.Id)
                          .Or(VMealOrder.OrderOperColumn).IsNull( )
                          .CloseEx( );
                if (status > 0)
                    query.And(VMealOrder.OrderStatusColumn).IsEqualTo(status);
                else
                    query.And(VMealOrder.OrderStatusColumn).IsNotEqualTo((int)OrderStatus.Canceled);
            }
            else
            {
                query = DB.Select(Utilities.GetTableColumns(Order.Schema),
                             SysCompany.CompanyNameColumn.QualifiedName,
                             SysCompany.CompanyPhoneColumn.QualifiedName,
                             SysMember.MemberPhoneNumberColumn.QualifiedName)
                         .From<Order>( )
                         .InnerJoin(SysCompany.IdColumn, Order.OrderSellerIDColumn)
                         .InnerJoin(SysMember.IdColumn, Order.OrderMemberIDColumn)
                         .Where(Order.OrderTypeColumn).IsNotEqualTo((int)OrderType.Common)
                         .And(Order.OrderDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                         .And(Order.OrderDateColumn).IsLessThanOrEqualTo(dtEnd)
                         .And(Order.OrderModelColumn).IsEqualTo(model)
                         .And(SysCompany.CompanyTypeColumn).IsEqualTo((int)CompanyType.MealCompany)
                         .OrderDesc(Order.OrderDateColumn.QualifiedName);
                if (status > 0)
                    query.And(Order.OrderStatusColumn).IsEqualTo(status);
                else
                    query.And(Order.OrderStatusColumn).IsNotEqualTo((int)OrderStatus.Canceled);
            }
            if (!string.IsNullOrEmpty(txtCompany))
            {
                if (Formatter.IsChinese(txtCompany))
                    query.And(SysCompany.CompanyNameColumn).Like(Utilities.GetAllLikeQuery(txtCompany));
                else
                    query.And(SysCompany.CompanyTelColumn).Like(Utilities.GetAllLikeQuery(txtCompany));
            }
            if (!string.IsNullOrEmpty(txtMember))
                query.And(SysMember.MemberPhoneNumberColumn).Like("%" + txtMember);
            return query;
        }

        public static string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "OrderDetail":
                    result = string.Format("<a href=\"javascript:void(0)\" onclick=\"orderMeal.popupOrderDetail('{0}');\">【查看】</a>", rowData["ID"]);
                    break;
                case "OrderCode":
                    result = string.Format("<a href=\"javascript:void(0)\" onclick=\"orderMeal.popupStatus('{0}');\">{1}</a>", rowData["ID"], rowData[column]);
                    break;
                case "Action":
                    string nStatus = rowData["OrderStatus"].ToString( );
                    if (nStatus == "2" || nStatus == "3" || nStatus == "4")
                        result = string.Format("<a href=\"javascript:void(0)\" onclick=\"orderMeal.popupOrderEditor('{0}');\">【回复】</a>", rowData["ID"]);
                    else if (nStatus == "5")
                        result = "已取消";
                    else
                        result = "订餐成功";
                    break;
                case "OrderStatus":
                    CheckOrderStatus(rowData);
                    string sStatus = rowData[column].ToString( );
                    //if (sStatus == "2" || sStatus == "3")
                    //    result = HtmlControl.GetSelectHtml(OrderMealBLL.OrderStatusSoruce, string.Format("OrderStatus_{0}", rowData["ID"]), rowData[column], string.Format("onchange=\"orderMeal.changeStatus(this,'{0}');\"", rowData["ID"])).ToString( );
                    if (OrderMealBLL.OrderStatusSoruce.ContainsKey(sStatus))
                        result = OrderMealBLL.OrderStatusSoruce[sStatus];
                    else
                        result = "普通消费";
                    break;
                case "CompanyName":
                    result = string.Format("<a href='/Admin/CompanyEdit.aspx?id={0}&isdlg=1' target='_blank'>{1}</a>", rowData[Order.OrderSellerIDColumn.ColumnName], rowData[column]);
                    break;
                default:
                    result = Utilities.ToString(rowData[column]);
                    break;
            }
            return GetFormatCellData(rowData, result); ;
        }
        public static void CheckOrderStatus(DataRow row)
        {
            if (AppContextBase.Context.CurrentRole != (int)EleoooRoleDefine.Admin &&
                    Utilities.IsNull(row[Order.OrderOperColumn.ColumnName]))
                UpdateOrderOper(row["ID"], AppContextBase.Context.User.Id);
        }
        public static void UpdateOrderStatus(object orderId, int nStatus)
        {
            QueryCommand cmd = new QueryCommand(string.Format("UPDATE Orders SET OrderStatus = {0},OrderUpdateOn=GetDate() WHERE ID = {1};", nStatus, orderId));
            DataService.ExecuteQuery(cmd);
        }
        public static void UpdateOrderOper(object orderId, int orderOper)
        {
            if (orderOper > 0)
            {
                QueryCommand cmd = new QueryCommand(string.Format("UPDATE Orders SET OrderOper={0} WHERE ID = {1};", orderOper, orderId));
                DataService.ExecuteQuery(cmd);
            }
        }
        public static string GetFormatCellData(DataRow row, object data)
        {
            var nStatus = Formatter.ToEnum<OrderStatus>(row["OrderStatus"]);
            if (nStatus == OrderStatus.Completed && (row.Field<DateTime>(Order.Columns.OrderDateDeliver) - row.Field<DateTime>(Order.Columns.OrderDate)).TotalMinutes >= 75)
                nStatus = OrderStatus.InProgress;
            //var msnType = Utilities.ToInt(row["MsnType"]);
            //if ((nStatus == OrderStatus.NotStart || nStatus == OrderStatus.Modified) && msnType == 3)
            //    return string.Format(COLOR_FORMAT, "green", data);
            //else
            return string.Format(COLOR_FORMAT, _orderStatusFontColor[nStatus], data);
        }

        private static bool CheckUserOrder(Order order, out string message, bool isAdmin = false)
        {
            if (order == null)
            {
                message = "订单不存在.";
                goto lbl_false;
            }
            if (isAdmin)
            {
                if (AppContextBase.Context.CurrentSubSys != SubSystem.Admin ||
                    !AppContextBase.Context.User.AdminRoleId.HasValue ||
                      AppContextBase.Context.User.AdminRoleId.Value <= 0)
                {
                    message = "你无权使用此服务";
                    goto lbl_false;
                }
            }
            if (!isAdmin && order.OrderMemberID != AppContextBase.Context.User.Id && order.OrderSellerID != AppContextBase.Context.User.CompanyId)
            {
                message = "这个不是你的订单.";
                goto lbl_false;
            }
            if (Formatter.ToEnum<OrderType>(order.OrderType.Value) == OrderType.Common)
            {
                message = "这个不是快餐店的订单.";
                goto lbl_false;
            }
            if (order.OrderStatus == (int)OrderStatus.Canceled)
            {
                message = "这个订单已经取消.";
                goto lbl_false;
            }
            if (order.OrderStatus == (int)OrderStatus.Completed)
            {
                message = "这个订单已经被确认订餐成功.";
                goto lbl_false;
            }
            message = string.Empty;
            return true;
        lbl_false:
            return false;
        }
        public static bool CompleteOrder(int orderId, out string message, bool isAdmin = false)
        {
            Order order = Order.FetchByID(orderId);
            if (!CheckUserOrder(order, out message, isAdmin))
                goto lbl_false;
            //if (order.OrderType == (int)OrderType.CompanyItem)
            //{
            //    return CompanyItemBLL.CompleteMemberMealItem(order, out message);
            //}
            if (order.IsNonOut.HasValue && order.IsNonOut.Value)
            {
                message = "经确认,商家不提供外送服务,请选择其他商家重新下单.";
                goto lbl_false;
            }
            var sp = SP_.SpCompleteMealOrder(orderId, false, null, null);
            sp.Execute( );
            message = Utilities.ToString(sp.OutputValues[1]);
            return Utilities.ToInt(sp.OutputValues[0]) == 0;
        //try
        //{
        //    if (!CompanyItemBLL.CompleteMemberMealItem(order, out message))
        //        goto lbl_false;
        //    order.OrderStatus = (int)OrderStatus.Completed;
        //    order.OrderDateDeliver = DateTime.Now;
        //    order.Save( );
        //    OrderProgressBLL.AddOrderConfirmLog(order.Id, "订餐成功，欢迎下次惠顾！");
        //    message = "订餐成功，欢迎下次惠顾！";
        //}
        //catch (Exception ex)
        //{
        //    Logging.Log("OrderMealBLL->CompleteOrder", ex);
        //    message = ex.Message;
        //    goto lbl_false;
        //}
        //RemoveLockScopeAction(orderId);
        //return true;
        lbl_false:
            return false;
        }
        public static bool CancelOrder(int orderId, out string message, bool isAdmin = false, bool isLogMsg = true)
        {
            Order order = Order.FetchByID(orderId);
            if (!CheckUserOrder(order, out message, isAdmin))
                goto lbl_false;

            var sp = SP_.SpCancelMealOrder(orderId, !isLogMsg, null, null);
            sp.Execute( );
            message = Utilities.ToString(sp.OutputValues[1]);
            return Utilities.ToInt(sp.OutputValues[0]) == 0;
        //TransactionScope ts = new TransactionScope( );
        //SharedDbConnectionScope ss = new SharedDbConnectionScope( );
        //try
        //{
        //    if (!CompanyItemBLL.CancelMemberMealItem(order, out message))
        //        goto lbl_false;
        //    order.OrderStatus = (int)OrderStatus.Canceled;
        //    order.OrderUpdateOn = DateTime.Now;
        //    order.OrderPoint = 0;
        //    order.OrderRate = 0;
        //    order.Save( );
        //    RewardBLL.CancelRewardMemberPointForOrderMeal(order);

            //    if (isLogMsg)
        //        OrderProgressBLL.AddOrderConfirmLog(order.Id, "订单已取消。");
        //    ts.Complete( );
        //    message = "订单取消成功.";
        //}
        //catch (Exception ex)
        //{
        //    message = ex.Message;
        //    Logging.Log("OrderMealBLL->CancelOrder", ex, true);
        //    goto lbl_false;
        //}
        //finally
        //{
        //    ss.Dispose( );
        //    ts.Dispose( );
        //}
        //RemoveLockScopeAction(orderId);
        //return true;
        lbl_false:
            return false;
        }
        public static int UgrentOrder(int orderId, out string message)
        {
            lock (LockScopeAction(orderId))
            {
                int result = -1;
                Order order = Order.FetchByID(orderId);
                if (!CheckUserOrder(order, out message))
                    goto lbl_return;
                var status = order.OrderStatus;
                if ((DateTime.Now - order.OrderDate).TotalMinutes < 20)
                {
                    message = "正在努力配送中，请稍等^_^";
                    goto lbl_return;
                }
                if (status == (int)OrderStatus.NotStart || status == (int)OrderStatus.Modified)
                {
                    message = "乐多分客服还没确认此订单，请再稍等一会儿^_^";
                    goto lbl_return;
                }
                if (order.OrderDateUpload > order.OrderDate)
                {
                    message = "已经催过了，请再稍等一会儿^_^";
                    goto lbl_return;
                }
                order.OrderDateUpload = DateTime.Now;
                order.Save( );
                DateTime dt = DateTime.Now;
                OrderProgressBLL.AddOrderConfirmLog(order, dt.AddSeconds(2), "正在帮您跟餐厅联系，请稍候。");
                OrderProgressBLL.AddOrderConfirmLog(order, dt.AddSeconds(35), "餐厅表示：您的餐点正在配送途中，请耐心等一会儿。");
                message = "我们会尽快的了.";
                result = 0;
            lbl_return:
                RemoveLockScopeAction(orderId);
                return result;
            }
        }

        public static IEnumerable<IDataReader> GetOrderChangePriceDetail(Order order)
        {
            var query = DB.Select(OrdersDetail.MenuNameColumn.ColumnName, OrdersDetail.OrderPriceColumn.ColumnName).From<OrdersDetail>( )
                          .Where(OrdersDetail.OrderIdColumn).IsEqualTo(order.Id)
                          .And(OrdersDetail.IsChgPriceColumn).IsEqualTo(true);
            return query.GetDataReaderEnumerator( );
        }

        public static int GetOrderNum( )
        {
            QueryCommand cmd = new QueryCommand("Select max(OrderNum) + 1 from Orders where OrderDate > dbo.GetToDay();");
            var val = DataService.ExecuteScalar(cmd);
            if (val == DBNull.Value)
                return 1;
            else
                return Convert.ToInt32(val);
        }
        public static int SaveOrderMeal(string userData, string orderData, out int orderId, out string message)
        {
            orderId = -1;
            message = string.Empty;
            try
            {
                OrderMealUserData orderMealUserData = Utilities.JSONToObj<OrderMealUserData>(userData);
                List<OrderMealData> orderDatas = Utilities.JSONToObj<List<OrderMealData>>(orderData);
                return SaveOrderMeal(orderMealUserData, orderDatas, out orderId, out message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                Logging.Log("OrderMealBLL->SaveOrderMeal", ex);
                return -1;
            }
        }
        // return -1 error,1 auto login 0 need redirect login
        private static int SaveOrderMeal(OrderMealUserData userData, List<OrderMealData> orderData, out int orderId, out string message)
        {
            orderId = -1;
            if (!CheckUserData(userData, out message))
                return -1;
            SysCompany company = SysCompany.FetchByID(userData.companyId);
            if (company == null)
            {
                message = "你选择的商家不存在.";
                return -1;
            }
            if (Formatter.ToEnum<CompanyType>(company.CompanyType.Value) != CompanyType.MealCompany)
            {
                message = "你选择的商家不是快餐店.";
                return -1;
            }
            SysCompanyItem companyItem = userData.itemId > 0 ? SysCompanyItem.FetchByID(userData.itemId) : null;
            var menuDict = MealMenuBLL.LoadCompanyMenu(company.Id).ToDictionary(dr => Convert.ToInt32(dr[SysTakeawayMenu.IdColumn.ColumnName]),
                                                                                dr =>
                                                                                {
                                                                                    SysTakeawayMenu m = new SysTakeawayMenu( );
                                                                                    m.Load(dr);
                                                                                    return m;
                                                                                });
            int qty = 0;
            decimal orderSum = 0;
            orderData.Sort((t1, t2) =>
                {
                    if (t1.sort == t2.sort)
                        return 0;
                    else if (t1.sort > t2.sort)
                        return 1;
                    else
                        return -1;
                });
            foreach (var pair in orderData)
            {
                if (!pair.isCompanyItem && !menuDict.ContainsKey(pair.menudId))
                {
                    message = "你选择了一个已经缺货或不存在的菜单,请重新下订.";
                    return -1;
                }
                if (pair.menuAmount <= 0)
                {
                    message = "订餐的数量不能小于或等于零.";
                    return -1;
                }
                if (!pair.isCompanyItem)
                {
                    var m = menuDict[pair.menudId];
                    pair.menudId = m.Id;
                    pair.menuPrice = (m.Price ?? (decimal?)0).Value;
                    pair.menuName = m.Name;
                }
                else if (companyItem != null)
                {
                    pair.menudId = -userData.itemId;
                    pair.menuPrice = (companyItem.ItemNeedPay ?? (decimal?)0).Value;
                    pair.menuName = companyItem.ItemTitle;
                }
                orderSum += (pair.menuPrice.Value * pair.menuAmount);
                qty += pair.menuAmount;
            }
            orderSum = Math.Round(orderSum, 1);
            Order order = null;
            if (userData.orderId > 0)
            {
                lock (LockScopeAction(userData.orderId))
                {
                    order = Order.FetchByID(userData.orderId);
                    if (order == null)
                    {
                        message = "你选择了一个不存在的订单.";
                        return -1;
                    }
                    if (Formatter.ToEnum<OrderType>(order.OrderType.Value) != OrderType.OrderMeal)
                    {
                        message = "你选择了一个不存在的订单.";
                        return -1;
                    }
                    if (order.OrderStatus == (int)OrderStatus.Completed || order.OrderStatus == (int)OrderStatus.Canceled)
                    {
                        message = "此订单不允许修改.";
                        return -1;
                    }
                    return SaveOrder(userData, orderData, order, company, companyItem, orderSum, qty, false, out orderId, out message);
                }
            }
            else
            {
                order = new Order( );
                order.OrderCode = OrderBLL.GetOrderCode(company);
                return SaveOrder(userData, orderData, order, company, companyItem, orderSum, qty, true, out orderId, out message);
            }

        }
        private static int SaveOrder(OrderMealUserData userData,
                              List<OrderMealData> orderData,
                              Order order,
                              SysCompany company,
                              SysCompanyItem companyItem,
                              decimal orderSum,
                              int qty,
                              bool isNewOrder,
                              out int orderId,
                              out string message)
        {
            orderId = -1;
            bool isLogin = AppContextBase.Context.CurrentSubSys != SubSystem.ALL;
            decimal servicesSum = company.ServiceSum.HasValue ? company.ServiceSum.Value : 0;
            SysMember user = isLogin ? AppContextBase.Context.User : null;
            bool isNeedCheckPhone;
            if (!isLogin || !Utilities.Compare(user.MemberPhoneNumber, userData.userPhone))
                isNeedCheckPhone = true;
            else
                isNeedCheckPhone = MsnBLL.IsPhoneNumNeedCheck(userData.userPhone);
            decimal userOrderAvgSum = 0;
            if (isLogin && companyItem != null && !CompanyItemBLL.CanClickCompanyMealItem(company, user, companyItem, userData.orderId, out userOrderAvgSum, out message))
                return -1;
            bool isNew = false;
            using (TransactionScope ts = new TransactionScope( ))
            {
                using (SharedDbConnectionScope ss = new SharedDbConnectionScope( ))
                {
                    if (isNeedCheckPhone && !MsnBLL.CheckPhoneNumCode(userData.userPhone, userData.checkCode, userData.msnLogId))
                    {
                        message = "验证码有误或已超时，请重新验证。";
                        return -1;
                    }
                    user = user ?? UserBLL.GetOrNewMemberByPhoneNumber(userData.userPhone, company, out isNew);
                    if (isNew)
                        user.AreaDepth2 = company.AreaDepth; //第一次订快餐的地址，默认该地址所属商圈为其工作圈
                    else if (!Utilities.Compare(user.AreaDepth2, company.AreaDepth))
                    {
                        if (string.IsNullOrEmpty(user.AreaDepth1))
                            user.AreaDepth1 = company.AreaDepth;
                        else if (string.IsNullOrEmpty(user.AreaDepth3))
                            user.AreaDepth3 = company.AreaDepth;
                    }

                    if (!isNewOrder)
                    {
                        if (order.OrderMemberID != user.Id)
                        {
                            message = "你不能修改其他会员的订单.";
                            return -1;
                        }
                        ClearOrderMenuDetail(order.Id);
                        if (Utilities.ToDecimal(order.OrderPoint) > 0)
                            RewardBLL.RewardMemberPointForOrderMeal(user, company, order);
                        order.OrderStatus = (int)OrderStatus.Modified;
                    }
                    else
                    {
                        order.OrderNum = GetOrderNum( );
                        order.OrderStatus = (int)OrderStatus.NotStart;
                        order.OrderPoint = 0;
                    }
                    var dtNow = DateTime.Now;
                    //order.OrderCode = orderCode;
                    order.OrderCard = string.Empty;
                    order.OrderDate = dtNow;
                    order.OrderDateDeliver = dtNow;
                    order.OrderDateUpload = dtNow;
                    order.OrderMemberID = user.Id;
                    order.OrderMemo = userData.memo;
                    order.OrderProduct = userData.address; //送餐地址
                    order.OrderQty = qty;
                    order.OrderRateSale = 0; // 折扣
                    order.OrderRate = RewardBLL.OrderMealRewardRate(company) / 100M;  //赠送比例
                    order.OrderPrePoint = (RewardBLL.OrderMealRewardRate(company) / 100M) * orderSum; //赠送积分
                    order.OrderSellerID = company.Id;
                    order.OrderSum = orderSum; //订单总额
                    order.OrderSumOk = orderSum + servicesSum; //订单总额
                    order.OrderPay = orderSum + servicesSum; //现金支付
                    order.OrderPayCash = 0; //储值支付
                    order.OrderPayPoint = companyItem != null ? companyItem.ItemPoint : 0;//积分支付
                    order.OrderType = (int)OrderType.OrderMeal;
                    order.ServiceSum = servicesSum;
                    order.MansionId = userData.mansionId;
                    order.OrderModel = company.IsUseMsg.HasValue && company.IsUseMsg.Value ? (int)OrderModel.Auto : (int)OrderModel.Manual;
                    order.OrderUpdateOn = DateTime.Now;
                    order.IsNonOut = false;
                    order.HasOutOfStock = false;
                    order.Save( );
                    //RewardBLL.RewardMemberPointForOrderMeal(user, company, order);
                    //save detail
                    List<OrdersDetail> details = new List<OrdersDetail>( );
                    foreach (var pair in orderData)
                    {
                        var d = new OrdersDetail( )
                        {
                            MenuId = pair.menudId,
                            MenuName = pair.menuName,
                            OrderId = order.Id,
                            OrderQty = pair.menuAmount,
                            OrderPrice = pair.menuPrice.Value,
                            IsChgPrice = false,
                            IsOutOfStock = false,
                            ItemInfo = pair.isCompanyItem && companyItem != null ? companyItem.ItemInfo : null
                        };
                        d.Save( );
                        details.Add(d);
                    }
                    if (isLogin && companyItem != null)
                        CompanyItemBLL.ClickCompanyMealItem(company, user, companyItem, order, userOrderAvgSum);

                    UserBLL.AddUserFavAddress(user.Id, userData.mansionId, userData.address);
                    //UserBLL.AddUserFavCompany(user.Id, company.Id);
                    OrderBLL.UpdateBalance( );
                    ts.Complete( );
                    //SendOrderMealMessage(company, user, order, details, out message);
                    if (userData.orderId > 0)
                        OrderProgressBLL.ClearOrderLog(order.Id);
                    OrderProgressBLL.InitOrderLogProgress(order, company);

                    message = "订餐成功.";
                    orderId = order.Id;
                }
            }
            //Auto login
            if (!isLogin)
            {
                if (user != null)
                {
                    if (user.MemberRoleId > 0)
                        user.LastLoginSubSys = (int)SubSystem.Member;
                    else if (user.CompanyRoleId > 0)
                        user.LastLoginSubSys = (int)SubSystem.Company;
                    else if (user.AdminRoleId > 0)
                        user.LastLoginSubSys = (int)SubSystem.Admin;
                    user.LastLoginDate = DateTime.Now;
                    user.Save( );
                    Utilities.LoginSigIn(user.Id, (SubSystem)user.LastLoginSubSys);
                    if (isNew)
                    {
                        message = "下次订餐您可以直接登录，账号是您的手机号码，密码为手机后6位数，记得及时修改密码哦！";
                        return 2;
                    }
                    return 1;
                }
            }
            return 1;
        }
        private static bool CheckUserData(OrderMealUserData userData, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(userData.userPhone))
            {
                message = "请输入你的手机号码.";
                return false;
            }
            if (userData.companyId <= 0)
            {
                message = "商家不存在!";
                return false;
            }
            if (userData.mansionId <= 0)
            {
                message = "请选择送餐大厦.";
                return false;
            }
            if (userData.msnLogId > 0 && string.IsNullOrEmpty(userData.checkCode))
            {
                message = "请输入手机验证码.";
                return false;
            }
            if (string.IsNullOrEmpty(userData.address))
            {
                message = "请输入送餐的地址.";
                return false;
            }
            return true;
        }
        public static bool SendOrderMealMessage(SysCompany company, SysMember user, Order order, IEnumerable<OrdersDetail> detail, out string message)
        {
            message = string.Empty;
            if (!company.IsUseMsg.HasValue || !company.IsUseMsg.Value || string.IsNullOrEmpty(company.MsnPhoneNum))
                return true;
            StringBuilder sb = new StringBuilder( );
            sb.AppendFormat("订单编号:{0}.", order.Id);
            sb.AppendFormat("手机号码:{0}.", user.MemberMsnPhone);
            SysAreaMansion mansion = SysAreaMansion.FetchByID(order.MansionId.Value);
            sb.AppendFormat("送餐地址:{0} {1}.", mansion != null ? mansion.Name : string.Empty, Utilities.ConcatAddres(order.OrderProduct));
            sb.Append("餐点内容:");
            foreach (var d in detail)
            {
                sb.AppendFormat("[{0}:{1}份]", d.MenuName, d.OrderQty);
            }
            if (!string.IsNullOrEmpty(order.OrderMemo))
                sb.AppendFormat("备注:{0}.", order.OrderMemo);
            int logId;
            return MsnBLL.SendMessage(company.MsnPhoneNum, sb.ToString( ), order.Id, out message, out logId);
        }
        private static bool CheckSendMessagePostData(Order order, out string message)
        {
            if (order == null)
            {
                message = "订单不存在.";
                return false;
            }
            if (order.OrderStatus == (int)OrderStatus.Canceled)
            {
                message = "该订单已取消";
                return false;
            }
            if (order.OrderStatus == (int)OrderStatus.Completed)
            {
                message = "该订单已经订餐成功";
                return false;
            }
            if (Formatter.ToEnum<OrderType>(order.OrderType.Value) == OrderType.Common)
            {
                message = "这个不是快餐店的订单.";
                return false;
            }
            if ((!AppContextBase.Context.User.AdminRoleId.HasValue || AppContextBase.Context.User.AdminRoleId <= 0) && AppContextBase.Context.User.CompanyId != order.OrderSellerID)
            {
                message = "你无权限发送订单消息.";
                return false;
            }
            message = string.Empty;
            return true;
        }
        public static int ConfirmOrder(int orderId, ref long orderSessionVal, int msnType, string message, string orders, out string msg)
        {
            msg = string.Empty;
            int nResult = -1;
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                lock (LockScopeAction(orderId))
                {
                    Order order = Order.FetchByID(orderId);
                    if (!CheckSendMessagePostData(order, out msg))
                        goto lbl_return;
                    if ((order.OrderUpdateOn.Value.Ticks) != (orderSessionVal))
                    {
                        msg = "该订单已修改，请重新查看.";
                        nResult = -2;
                        goto lbl_return;
                    }
                    var oldStatus = order.OrderStatus;
                    var chgData = Utilities.JSONToObj<Dictionary<int, OrderDetailItem>>(orders);
                    bool isChanged, hasChangePrice = false;
                    decimal dSum = 0;
                    SysCompany company = SysCompany.FetchByID(order.OrderSellerID);
                    if (msnType == 1) //暂缺或价格调整
                    {
                        OrdersDetail itemDetail = null;
                        var details = GetOrderAllDetailByOrderId(orderId).ToDictionary(d => d.MenuId, d =>
                            {
                                if (d.MenuId < 0)
                                    itemDetail = d;
                                return d;
                            });
                        //check sum
                        if (itemDetail != null)
                        {
                            decimal itemChgSum = chgData.Values.Sum(item =>
                            {
                                if (item.IsCompanyItem)
                                {
                                    if (item.NewPrice > 0)
                                    {
                                        itemDetail.IsChgPrice = true;
                                        return item.NewPrice;
                                    }
                                    else
                                        return item.OrderPrice;
                                }
                                else
                                    return 0;
                            });
                            if (itemChgSum <= itemDetail.OrderPrice.Value)
                            {
                                msg = "餐点调的新价不能小于促销项目的现金支付金额.";
                                goto lbl_return;
                            }
                            else if (itemDetail.IsChgPrice.Value)
                                CompanyItemBLL.UpdateCompanyItemSum(itemDetail.MenuId, itemChgSum);
                            dSum = itemDetail.OrderPrice.Value;
                        }
                        //bool isOutOfStock;
                        foreach (var item in chgData.Values)
                        {
                            isChanged = false;
                            var detail = details.ContainsKey(item.MenuId) ? details[item.MenuId] : (OrdersDetail)null;
                            if (detail != null)
                            {
                                if (detail.IsOutOfStock != item.IsOutOfStock)
                                {
                                    detail.IsOutOfStock = item.IsOutOfStock;
                                    MealMenuBLL.ChangeMenuOutOfStokcStatus(item.MenuId, item.IsOutOfStock);
                                    isChanged = true;
                                    if (item.IsOutOfStock)
                                        order.HasOutOfStock = item.IsOutOfStock;
                                }

                                if (item.NewPrice > 0 && detail.OrderPrice.Value != item.NewPrice)
                                {
                                    detail.OrderPrice = item.NewPrice;
                                    detail.IsChgPrice = true;
                                    hasChangePrice = true;
                                    isChanged = true;
                                    MealMenuBLL.ChangeMenuPrice(item.MenuId, item.NewPrice);
                                }

                                if (!detail.IsOutOfStock.Value)
                                    dSum += detail.OrderQty.Value * detail.OrderPrice.Value;
                                if (isChanged)
                                    detail.Save( );
                            }
                        }
                        dSum = Math.Round(dSum, 1);
                    }
                    isChanged = false;

                    SysMember user = SysMember.FetchByID(order.OrderMemberID);
                    if (msnType == 1 || hasChangePrice) //某菜单缺货或价格更改
                    {
                        isChanged = true;
                        order.OrderSum = dSum;
                        order.OrderSumOk = order.ServiceSum.HasValue ? (order.ServiceSum.Value + dSum) : dSum;
                        order.OrderPay = order.ServiceSum.HasValue ? (order.ServiceSum.Value + dSum) : dSum;
                        order.OrderPrePoint = (RewardBLL.OrderMealRewardRate(company) / 100M) * dSum; //赠送积分
                        order.OrderRate = RewardBLL.OrderMealRewardRate(company) / 100M;  //赠送比例
                    }

                    if (msnType == 2 || msnType == 4) //商家不外送,订单取消
                    {
                        isChanged = true;
                        if (msnType == 2)
                            order.IsNonOut = true;
                        order.OrderStatus = (int)OrderStatus.Canceled;
                        order.OrderUpdateOn = DateTime.Now;
                        if (!CompanyItemBLL.CancelMemberMealItem(order, out msg))
                            goto lbl_return;
                        RewardBLL.CancelRewardMemberPointForOrderMeal(order);
                    }
                    else //if (msnType != 3)  //自定义回复
                    {
                        if (msnType != 3 ||
                            (msnType == 3 && (oldStatus == (int)OrderStatus.NotStart || oldStatus == (int)OrderStatus.Modified)))
                            order.OrderStatus = (int)OrderStatus.InProgress;
                        order.OrderUpdateOn = DateTime.Now;
                        order.MsnType = msnType == 3 && oldStatus == (int)OrderStatus.InProgress ? 0 : msnType;
                        order.ModifiedOn = DateTime.Now;
                        isChanged = true;
                    }
                    //if (msnType == 0 || (msnType == 1 && !order.HasOutOfStock.Value))
                    //{
                    //    order.OrderPoint = order.OrderPrePoint;
                    //}

                    if (!order.OrderOper.HasValue || order.OrderOper.Value == 0)
                        order.OrderOper = AppContextBase.Context.User.Id;
                    order.Save( );

                    if (msnType == 0 || (msnType == 1 && !order.HasOutOfStock.Value))
                    {
                        RewardBLL.RewardMemberPointForOrderMeal(user, company, order);
                        //OrderBLL.UpdateBalance( );
                    }
                    RemoveLockScopeAction(orderId);
                    ts.Complete( );
                    ss.Dispose( );
                    ss = null;
                    ts.Dispose( );
                    ts = null;
                    OrderProgressBLL.AddOrderConfirmLog(order, company, msnType, hasChangePrice, message);
                    string msnContent = string.Empty;
                    if (msnType == 0)
                    {
                        if (UserIsFirstOrder(order.OrderMemberID))
                        {
                            msnContent = string.Format("亲爱的用户：{0}已经收到您的订单，餐厅正在备餐。您本次订单总计{1}元。预计{2}分钟左右为您送达（仅供参考，高峰时段以实际送达时间为准）。",
                                                        company.CompanyName, order.OrderSumOk, company.OrderElapsed);

                        }
                    }
                    else if (msnType == 1 && order.HasOutOfStock.HasValue && order.HasOutOfStock.Value)
                        msnContent = "亲爱的用户：" + message;
                    else if (msnType == 2)
                        msnContent = "亲爱的用户：很抱歉，" + company.CompanyName + "表示，当前暂不提供外送，请选择其他餐厅。";
                    else if (msnType == 3)
                        msnContent = "亲爱的用户：" + message;
                    if (!string.IsNullOrEmpty(msnContent))
                    {
                        int logId;
                        string phone = string.IsNullOrEmpty(user.MemberMsnPhone) ? user.MemberPhoneNumber : user.MemberMsnPhone;
                        if (!MsnBLL.SendMessage(phone, msnContent, order.Id, out msg, out logId))
                            goto lbl_return;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Logging.Log("OrderMealBLL->ConfirmOrder", ex, true);
                goto lbl_return;
            }
            finally
            {
                if (ss != null)
                    ss.Dispose( );
                if (ts != null)
                    ts.Dispose( );
            }
            msg = "发送成功";
            nResult = 0;
            orderSessionVal = GetOrderUpdateOn(orderId).Ticks;
        lbl_return:
            RemoveLockScopeAction(orderId);
            return nResult;
        }
        public static int GetOrderCount(int companyID)
        {
            var cmd = new QueryCommand("Select count(*) from Orders where OrderStatus <> @OrderStatus and OrderSellerID = @OrderSellerID");
            cmd.AddParameter("@OrderStatus", (int)OrderStatus.Canceled, DbType.Int32);
            cmd.AddParameter("@OrderSellerID", companyID, DbType.Int32);
            return Utilities.ToInt(DataService.ExecuteScalar(cmd));
        }
        public static DateTime? GetUserLatestOrderDate(int userID, int companyID)
        {
            var cmd = new QueryCommand("Select top 1 OrderDate from Orders where OrderStatus <> @OrderStatus and OrderSellerID = @OrderSellerID and  OrderMemberID = @OrderMemberID order by id desc");
            cmd.AddParameter("@OrderStatus", (int)OrderStatus.Canceled, DbType.Int32);
            cmd.AddParameter("@OrderSellerID", companyID, DbType.Int32);
            cmd.AddParameter("@OrderMemberID", userID, DbType.Int32);
            var val = DataService.ExecuteScalar(cmd);
            if (Utilities.IsNull(val))
                return null;
            else
                return Convert.ToDateTime(val);
        }
        public static DateTime GetOrderUpdateOn(int orderId)
        {
            QueryCommand cmd = new QueryCommand("select orderupdateon from orders where id=@id");
            cmd.AddParameter("@id", orderId, DbType.Int32);
            return Convert.ToDateTime(DataService.ExecuteScalar(cmd));
        }
        public static bool HasCompanyItemInfo(int orderId)
        {
            return DB.Select("top 1 1").From(OrdersDetail.Schema.TableName)
                     .Where(OrdersDetail.OrderIdColumn).IsEqualTo(orderId)
                     .And(OrdersDetail.MenuIdColumn).IsLessThan(0)
                     .ExecuteScalar<int>( ) > 0;
        }

        public static string GetOrderTimespan(Order order)
        {
            var status = order.OrderStatus;
            DateTime dt;
            if (status == (int)OrderStatus.Completed)
                dt = order.OrderDateDeliver;
            else if (status == (int)OrderStatus.Canceled)
                dt = (order.OrderUpdateOn ?? order.ModifiedOn).Value;
            else
                dt = DateTime.Now;
            var span = (dt - order.OrderDate).ToString( );
            var index = span.IndexOf(".");
            if (index > 0)
                return span.Substring(0, span.IndexOf("."));
            else
                return span;
        }
        public static DataTable GetOrdersFormMobile(int companyId, string phone, DateTime d1, DateTime d2, DateTime? d3, int pageIndex, int pageSize, out int pageCount)
        {
            int total = 0;
            var sp = SP_.SpGetOrders(companyId, phone, d2, d2, d3, pageIndex, pageSize, total);
            var ds = sp.GetDataSet( );
            if (pageIndex > 0 && d3.HasValue)
            {
                total = Utilities.ToInt(sp.OutputValues[0]);
                pageCount = Utilities.CalcPageCount(pageSize, total);
            }
            else
                pageCount = 0;
            return ds.Tables[0];
        }
        #region nested type
        public class OrderDetailItem
        {
            public object DirName { get; set; }
            public object MenuName { get; set; }
            public int MenuId { get; set; }
            public int OrderId { get; set; }
            public int OrderQty { get; set; }
            public decimal OrderPrice { get; set; }
            public decimal OrderSum { get; set; }
            public bool IsCompanyItem { get; set; }
            public bool IsOutOfStock { get; set; }
            public decimal NewPrice { get; set; }
        }
        public class OrderMealData
        {
            public int menudId { get; set; }
            public string menuName { get; set; }
            public decimal? menuPrice { get; set; }
            public int menuAmount { get; set; }
            public int sort { get; set; }
            public bool isCompanyItem { get; set; }
        }
        public class OrderMealUserData
        {
            public int companyId { get; set; }
            public string userPhone { get; set; }
            public int mansionId { get; set; }
            public string address { get; set; }
            public int msnLogId { get; set; }
            public string checkCode { get; set; }
            public int orderId { get; set; }
            public string memo { get; set; }
            public int itemId { get; set; }
        }
        //public class ChangedOrders
        //{
        //    public int ID { get; set; }
        //    public int MenuId { get; set; }
        //    public string MenuName { get; set; }
        //    public int OrderId { get; set; }
        //    public decimal OrderPrice { get; set; }
        //    public decimal? NewPrice { get; set; }
        //    public bool? IsOutOfStock { get; set; }
        //}
        #endregion
    }
}