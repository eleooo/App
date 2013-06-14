using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Eleooo.Web;
using Eleooo.Common;

namespace Eleooo.BLL.Services
{
    class OrderMealHandler : IHandlerServices
    {
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
    }
}
