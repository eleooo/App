using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using SubSonic;
using Eleooo.DAL;
using System.Text;
using System.Linq;
using Eleooo.Common;

namespace Eleooo.Web
{
    public static class OrderProgressBLL
    {
        public static readonly string OrderNormalProgress = "OrderNormalProgress";
        public static readonly string OrderNonOutCompanyProgress = "OrderNonOutCompanyProgress";
        public static readonly string OrderOutOfStockProgress = "OrderOutOfStockProgress";

        public const string IdColumn = "ID";
        public const string DateColumn = "Date";
        public const string DescColumn = "Desc";
        public const string IsCurrentColumn = "IsCurrent";
        private static DataTable GetProgressSchema(string tableName)
        {
            DataTable dt = new DataTable(tableName)
                {
                    CaseSensitive = false
                };
            dt.Columns.Add(DateColumn, typeof(DateTime));
            dt.Columns.Add(DescColumn, typeof(string));
            dt.Columns.Add(IsCurrentColumn, typeof(int));
            return dt;
        }
        private static DateTime AppendProgressRow(DataTable dt, DateTime dtNow, DateTime dtBegin, DateTime dtEnd, string desc, int isCurrent = 0)
        {
            if (dtNow < dtBegin)
                return dtEnd;
            var row = dt.NewRow( );
            row[IsCurrentColumn] = isCurrent;
            row[DescColumn] = desc;
            if (dtNow >= dtBegin && dtNow <= dtEnd)
            {
                row[DateColumn] = dtNow;
            }
            else
                row[DateColumn] = dtEnd;
            dt.Rows.Add(row);
            return dtEnd;
        }
        private static DateTime AppendUploadRow(DataTable dt, DateTime dtNow, DateTime dtBegin, DateTime dtEnd, DateTime dtUpload)
        {
            if (dtUpload > dtBegin && dtUpload <= dtEnd)
            {
                DateTime dtTemp = dtUpload.AddSeconds(3);
                AppendProgressRow(dt, dtNow, dtUpload, dtTemp, "乐多分客服正在帮您跟餐厅联系，请稍后");
                dtTemp = AppendProgressRow(dt, dtNow, dtUpload.AddSeconds(45), dtUpload.AddSeconds(60), "您的餐点正在配送途中，请再等一会儿");
                return dtTemp;
            }
            return dtEnd;
        }
        private static bool CheckOrder(DataTable dt, Order order, SysCompany company)
        {
            dt.CaseSensitive = true;
            DateTime dtNow = DateTime.Now;
            if (order == null)
            {
                AppendProgressRow(dt, dtNow, dtNow, dtNow, "订单不存在.");
                return false;
            }
            if (company == null)
            {
                AppendProgressRow(dt, dtNow, dtNow, dtNow, "商家不存在.");
                return false;
            }
            if (Formatter.ToEnum<OrderType>(order.OrderType.Value) == OrderType.Common)
            {
                AppendProgressRow(dt, dtNow, dtNow, dtNow, "此订单不是快餐店订单.");
                return false;
            }
            if (AppContextBase.Context.User.Id != order.OrderMemberID)
            {
                AppendProgressRow(dt, dtNow, dtNow, dtNow, "你无权查看其他会员的订单.");
                return false;
            }
            dt.CaseSensitive = false;
            return true;
        }

        public static DataTable GenOrderProgress(int orderId)
        {
            var isMember = AppContextBase.Context.CurrentSubSys == SubSystem.Member;
            var dt = DB.Select( ).From<OrdersLog>( )
                                 .Where(OrdersLog.OrderIdColumn).IsEqualTo(orderId)
                                 .And(OrdersLog.DateXColumn).IsLessThanOrEqualTo(DateTime.Now)
                                 .And(OrdersLog.IsCurrentColumn).IsGreaterThanOrEqualTo(isMember ? 0 : -1)
                                 .ExecuteDataTable( );
            return dt;
            //if (order.IsNonOut.HasValue && order.IsNonOut.Value)
            //    return GenOrderNonOutCompanyProgress(order, company);
            //else if (order.HasOutOfStock.HasValue && order.HasOutOfStock.Value)
            //    return GenOrderOutOfStockProgress(order, company, BuildOutOfStockItemInfo(OrderMealBLL.GetOutOfStockDeitalByOrderId(order.Id)));
            //else if (order.OrderStatus == (int)OrderStatus.Canceled)
            //    return GenOrderCanceledProgress(order, company);
            //else
            //    return GenOrderNormalProgress(order, company);
        }

        private static void AddProgressRow(int orderId, DateTime dtDate, string desc, int isCurrent = 0)
        {
            new OrdersLog
            {
                Desc = desc,
                IsCurrent = isCurrent,
                DateX = dtDate,
                OrderId = orderId
            }.Save( );
        }
        public static void UpdateLogCurrent(object logId, int current)
        {
            string vSql = string.Format("Update Orders_Log Set IsCurrent={0} where ID={1}", current, logId);
            DataService.ExecuteQuery(new QueryCommand(vSql));
        }
        public static void InitOrderLogProgress(Order order, SysCompany company)
        {
            DateTime dtDate = order.OrderDate;
            AddProgressRow(order.Id, dtDate, "订单已生成。");
            AddProgressRow(order.Id, dtDate.AddSeconds(10), "乐多分系统正在帮您下单。");
            AddProgressRow(order.Id, dtDate.AddSeconds(31), "餐厅正在确认您的订单内容。");

        }
        public static void AddOrderConfirmLog(Order order, SysCompany company, int msnType, bool hasChgPrice, string desc)
        {
            DateTime dtNow = DateTime.Now;
            int id = order.Id;
            int isCurrent = msnType == 1 || hasChgPrice ? 1 : 0;
            if (msnType == 0)
            {
                AddProgressRow(id, dtNow, desc);
                desc = string.Format("您的餐点预计{0}分钟左右送达（仅供参考，高峰时段以实际送达时间为准）。", company.OrderElapsed);
                AddProgressRow(id, dtNow.AddSeconds(10), desc);
            }
            else if (msnType == 2)
            {
                desc = desc.Replace(company.CompanyName + "表示，", "");
                AddProgressRow(id, dtNow, desc);
                AddProgressRow(id, dtNow.AddSeconds(4), "订单已取消。");
            }
            else if (hasChgPrice)
            {

                AddProgressRow(id, dtNow, desc);
                string desc2 = string.Format("您的餐点预计{0}分钟左右送达（仅供参考，高峰时段以实际送达时间为准）。", company.OrderElapsed);
                AddProgressRow(id, dtNow.AddSeconds(7), "订单已确认，餐厅开始备餐。");
                AddProgressRow(id, dtNow.AddSeconds(10), desc2, isCurrent);
            }
            else
                AddProgressRow(id, dtNow, desc, isCurrent);
        }
        public static void AddOrderConfirmLog(int orderId, string desc)
        {
            DateTime dtNow = DateTime.Now;
            AddProgressRow(orderId, dtNow, desc);
        }
        public static void AddOrderConfirmLog(int orderId, DateTime dtDate, string desc)
        {
            AddProgressRow(orderId, dtDate, desc);
        }
        public static void ClearOrderLog(int orderId)
        {
            QueryCommand cmd = new QueryCommand("delete Orders_Log where orderId=" + orderId.ToString( ));
            DataService.ExecuteQuery(cmd);
        }

        //public static DataTable GenOrderNormalProgress(Order order, SysCompany company)
        //{
        //    var data = GetProgressSchema(OrderNormalProgress);
        //    if (!CheckOrder(data, order, company))
        //        goto lbl_return;
        //    DateTime dtDate = order.OrderDate;
        //    DateTime dtUpload = order.OrderDateUpload; //催单信号
        //    DateTime? dtStatus = order.OrderUpdateOn;
        //    DateTime dtNow = DateTime.Now;
        //    DateTime dtTemp;
        //    dtTemp = AppendProgressRow(data, dtNow, dtDate, dtDate, "订单已生成");
        //    if (!dtStatus.HasValue)
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp.AddSeconds(3), " 乐多分客服正在帮您下单。");
        //    else if (dtStatus.Value >= dtTemp.AddSeconds(30))
        //    {
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp.AddSeconds(3), " 乐多分客服正在帮您下单。");
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp.AddSeconds(27), "餐厅正在确认您的订单内容。");
        //    }
        //    else
        //    {
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp, " 乐多分客服正在帮您下单。");
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtStatus.Value.AddSeconds(-3), "餐厅正在确认您的订单内容。");
        //    }
        //    if (dtStatus.HasValue)
        //    {
        //        BuildChangePriceItemInfo(order, data, dtStatus.Value, dtNow, dtTemp, out dtTemp);
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtStatus.Value, "订单已确认，餐厅开始备餐。");

        //        //10 - 20秒内 预计约XX分钟左右为您送达。
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp.AddSeconds(3), dtTemp.AddSeconds(5), string.Format("预计{0}左右为您送达", company.OrderElapsed));
        //    }
        //    //是否有催单信号
        //    dtTemp = AppendUploadRow(data, dtNow, dtTemp, dtUpload, dtUpload);
        //    if (order.OrderStatus == (int)OrderStatus.Completed)
        //        AppendProgressRow(data, dtNow, dtTemp, order.OrderDateDeliver, "订餐成功，谢谢，欢迎下次惠顾");
        //lbl_return:
        //    return data;
        //}
        //public static DataTable GenOrderNonOutCompanyProgress(Order order, SysCompany company)
        //{
        //    var data = GetProgressSchema(OrderNonOutCompanyProgress);
        //    if (!CheckOrder(data, order, company))
        //        return data;
        //    DateTime dtDate = order.OrderDate;
        //    DateTime dtNow = DateTime.Now;
        //    DateTime dtTemp;
        //    DateTime? dtStatus = order.OrderUpdateOn;
        //    dtTemp = AppendProgressRow(data, dtNow, dtDate, dtDate, "订单已生成");
        //    if (!dtStatus.HasValue)
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp.AddSeconds(3), " 乐多分客服正在帮您下单。");
        //    else if (dtStatus.Value >= dtTemp.AddSeconds(30))
        //    {
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp.AddSeconds(3), " 乐多分客服正在帮您下单。");
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp.AddSeconds(27), "餐厅正在确认您的订单内容。");
        //    }
        //    else
        //    {
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp, " 乐多分客服正在帮您下单。");
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtStatus.Value.AddSeconds(-3), "餐厅正在确认您的订单内容。");
        //    }
        //    dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtStatus.Value, "抱歉，当前暂不外送，请选择其他餐厅 ");
        //    return data;
        //}
        //public static DataTable GenOrderOutOfStockProgress(Order order, SysCompany company, string outOfStockInfo)
        //{
        //    var data = GetProgressSchema(OrderOutOfStockProgress);
        //    if (!CheckOrder(data, order, company))
        //        return data;
        //    DateTime dtDate = order.OrderDate;
        //    DateTime dtNow = DateTime.Now;
        //    DateTime? dtStatus = order.OrderUpdateOn;
        //    DateTime dtTemp;
        //    dtTemp = AppendProgressRow(data, dtNow, dtDate, dtDate, "订单已生成");
        //    if (!dtStatus.HasValue)
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp.AddSeconds(3), " 乐多分客服正在帮您下单。");
        //    else if (dtStatus.Value >= dtTemp.AddSeconds(30))
        //    {
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp.AddSeconds(3), " 乐多分客服正在帮您下单。");
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp.AddSeconds(27), "餐厅正在确认您的订单内容。");
        //    }
        //    else
        //    {
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtTemp, " 乐多分客服正在帮您下单。");
        //        dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtStatus.Value.AddSeconds(-3), "餐厅正在确认您的订单内容。");
        //    }
        //    dtTemp = AppendProgressRow(data, dtNow, dtTemp, dtStatus.Value, outOfStockInfo);
        //    return data;
        //}
        //private static string BuildOutOfStockItemInfo(IEnumerable<OrdersDetail> detail)
        //{
        //    return string.Concat("抱歉，",
        //                         string.Join(",", detail.Select(d => d.MenuName).ToArray( )),
        //                         "今天暂缺，请修改后重新下单");
        //}
        //private static bool BuildChangePriceItemInfo(Order order, DataTable dtData, DateTime dtStatus, DateTime dtNow, DateTime dtBegin, out DateTime dtTemp)
        //{
        //    var arr = OrderMealBLL.GetOrderChangePriceDetail(order).Select(dr => string.Format("{0}价格调整为{1}元，", dr[0], dr[1])).ToArray( );
        //    if (arr.Length > 0)
        //    {
        //        string message = "餐厅表示：" + string.Join("<br/>", arr) + "。";
        //        dtTemp = AppendProgressRow(dtData, dtNow, dtBegin, dtStatus, message);
        //        return true;
        //    }
        //    dtTemp = dtBegin;
        //    return false;
        //}
    }
}