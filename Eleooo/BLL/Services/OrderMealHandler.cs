using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Eleooo.Web;
using Eleooo.Common;
using SubSonic;
using Eleooo.DAL;

namespace Eleooo.BLL.Services
{
    class OrderMealHandler : IHandlerServices
    {
        private static readonly int _PageSize = 10;
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
            int pageCount = 0;
            var query = DB.Select(Utilities.GetTableColumns(Order.Schema),
                                      SysMember.Columns.MemberPhoneNumber,
                                      SysMember.Columns.MemberFullname)
                              .From<Order>( )
                              .InnerJoin(SysMember.IdColumn, Order.OrderMemberIDColumn)
                              .Where(Order.OrderDateColumn).IsBetweenAnd(d1, d2)
                              .OrderDesc(Order.OrderUpdateOnColumn.QualifiedName);
            if (AppContextBase.CurrentSysId != (int)SubSystem.Admin)
                query.And(Order.OrderSellerIDColumn).IsEqualTo(companyId);
            if (!isSyn)
            {
                if (!string.IsNullOrEmpty(phone))
                    query.And(SysMember.MemberPhoneNumberColumn).IsEqualTo(phone);
                var total = query.GetRecordCount( );
                pageCount = Utilities.CalcPageCount(_PageSize, total);
                query.Paged(pageIndex, _PageSize);
            }
            else
                query.And(Order.OrderUpdateOnColumn).IsGreaterThan(Utilities.ToDateTime(t));
            return ServicesResult.GetInstance(new { pageCount = pageCount, orders = query.ExecuteDataTable( ) });
        }
    }
}
