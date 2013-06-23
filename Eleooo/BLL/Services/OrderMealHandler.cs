using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Eleooo.Web;
using Eleooo.Common;
using SubSonic;
using Eleooo.DAL;
using System.Data;

namespace Eleooo.BLL.Services
{
    class OrderMealHandler : IHandlerServices
    {
        private static readonly string[] _ItemQueryColumns;
        private static readonly Dictionary<string, Func<string, System.Data.IDataReader, string>> _ItemQueryColumnsFormatter;
        private static readonly int _PageSize = 10;

        static OrderMealHandler( )
        {
            _ItemQueryColumns = new string[]
            {
                SysCompanyItem.Columns.ItemPoint,//积分兑换
                SysCompanyItem.Columns.ItemNeedPay,//现金支付
                SysCompanyItem.Columns.ItemAmount,//投放数量
                SysCompanyItem.Columns.OrderFreqLimit,//消费频率
                SysCompanyItem.Columns.OrderSumLimit,//平均金额
                SysCompanyItem.Columns.ItemDate, //投放周期
                SysCompanyItem.Columns.ItemEndDate,//投放周期
                SysCompanyItem.Columns.WorkingHours, //促销时段
                SysCompanyItem.Columns.ItemLimit, //抢购次数
                SysCompanyItem.Columns.ItemPic,
                SysCompanyItem.Columns.ItemSum,//总额
                SysCompanyItem.Columns.CompanyID,
                SysCompanyItem.Columns.ItemInfo,
                SysCompanyItem.Columns.ItemID
            };
            _ItemQueryColumnsFormatter = new Dictionary<string, Func<string, System.Data.IDataReader, string>>( );
            _ItemQueryColumnsFormatter.Add(SysCompanyItem.Columns.ItemDate, (col, dr) =>
                {
                    if (Utilities.IsNull(dr[col]))
                        return null;
                    else
                        return Utilities.ToDateTime(dr[col]).ToString("yyyy-MM-dd");
                });
            _ItemQueryColumnsFormatter.Add(SysCompanyItem.Columns.ItemEndDate, (col, dr) =>
            {
                if (Utilities.IsNull(dr[col]))
                    return null;
                else
                    return Utilities.ToDateTime(dr[col]).ToString("yyyy-MM-dd");
            });
        }

        public Common.ServicesResult GetMsnCode(HttpContext context)
        {
            string message;
            var phone = context.Request["phone"];
            int logId = context.Request["isForChgNo"] == "1" ? MsnBLL.GetMsnCodeForChgNo(phone, out message) : MsnBLL.GetMsnCode(phone, out message);
            if (logId < 0)
                return ServicesResult.GetInstance(logId, message, logId);
            else
                return ServicesResult.GetInstance(logId, string.Format("验证码已经发送到:{0}", phone), logId);
        }

        public Common.ServicesResult SendMessage(HttpContext context)
        {
            int orderId; long orderSessionVal; int msnType; string message; string orders;
            orderId = Utilities.ToInt(context.Request["orderId"]);
            orderSessionVal = Convert.ToInt64(context.Request["orderSessionVal"]);
            msnType = Utilities.ToInt(context.Request["msnType"]);
            message = context.Request["message"];
            orders = context.Request["orders"];
            string result;
            int code = OrderMealBLL.ConfirmOrder(orderId, ref orderSessionVal, msnType, message, orders, out result);
            return Common.ServicesResult.GetInstance(code, result, code < 0 ? null : new { orderSessionVal = orderSessionVal.ToString( ) });
        }

        public Common.ServicesResult GetOrders(HttpContext context)
        {
            var request = context.Request;
            var companyId = Utilities.ToInt(request["c"]);
            var d1 = Utilities.ToDateTime(request["d1"]);
            var d2 = Utilities.ToDateTime(request["d2"]).AddDays(1);
            var pageIndex = Utilities.ToInt(request["p"]);
            var phone = request["q"];
            var t = request["t"];
            bool isSyn = !string.IsNullOrEmpty(t);
            int pageCount;
            var result = OrderMealBLL.GetOrdersFormMobile(companyId, phone, d1, d2, isSyn ? Utilities.ToDateTime(t) : (DateTime?)null, pageIndex, _PageSize, out pageCount);
            return ServicesResult.GetInstance(new { pageCount = pageCount, orders = result });
        }

        public Common.ServicesResult GetDetail(HttpContext context)
        {
            var id = Utilities.ToInt(context.Request["id"]);
            int code = -1;
            Order order = Order.FetchByID(id);
            object result = null;
            if (order != null)
            {
                result = new
                {
                    CompanyName = CompanyBLL.GetCompanyName(order.OrderSellerID),
                    MemberPhoneNumber = UserBLL.GetUserPhoneById(order.OrderMemberID),
                    Timespan = OrderMealBLL.GetOrderTimespan(order),
                    details = OrderMealBLL.GetOrderDetailByOrder(order).Values,
                    OrderMemo = order.OrderMemo,
                    OrderId = id,
                    orderSessionVal = order.OrderUpdateOn.Value.Ticks,
                    OrderSumOk = order.OrderSumOk,
                    ServiceSum = order.ServiceSum,
                    Address = MansionBLL.GetMansionNameByID(order.MansionId.Value) + Utilities.ConcatAddres(order.OrderProduct)
                };
                code = 0;
            }
            return Common.ServicesResult.GetInstance(code, string.Empty, result);
        }
        public Common.ServicesResult GetTemps(HttpContext context)
        {
            var id = Utilities.ToInt(context.Request["id"]);
            int code = -1;
            Order order = Order.FetchByID(id);
            object result = null;
            if (order != null)
            {
                result = new
                {
                    MemberPhoneNumber = UserBLL.GetUserPhoneById(order.OrderMemberID),
                    Timespan = OrderMealBLL.GetOrderTimespan(order),
                    temps = OrderProgressBLL.GetOrderLog(order)
                };
                code = 0;
            }
            return Common.ServicesResult.GetInstance(code, string.Empty, result);
        }
        public Common.ServicesResult SendTemps(HttpContext context)
        {
            var id = Utilities.ToInt(context.Request["id"]);
            int code = -1;
            Order order = Order.FetchByID(id);
            object result = null;
            string message;
            if (order != null)
                code = OrderProgressBLL.AddOrderTempLog(context, order, out result, out message) ? 0 : code;
            else
                message = "订单不存在.";
            return Common.ServicesResult.GetInstance(code, message, result);
        }

        public Common.ServicesResult GetItem(HttpContext context)
        {
            var id = Utilities.ToInt(context.Request["id"]);
            int code = -1;
            string message = string.Empty;
            Dictionary<string, object> result = new Dictionary<string, object>( );
            var query = DB.Select(_ItemQueryColumns)
                         .From<SysCompanyItem>( )
                         .Where(SysCompanyItem.ItemIDColumn).IsEqualTo(id);
            using (var dr = query.ExecuteReader( ))
            {
                if (dr.Read( ))
                {
                    foreach (var col in _ItemQueryColumns)
                    {
                        if (_ItemQueryColumnsFormatter.ContainsKey(col))
                            result[col] = _ItemQueryColumnsFormatter[col](col, dr);
                        else
                            result[col] = dr[col];
                    }
                }
                else
                {
                    foreach (var col in _ItemQueryColumns)
                        result[col] = null;
                }
            }
            return Common.ServicesResult.GetInstance(code, message, result);
        }
    }
}
