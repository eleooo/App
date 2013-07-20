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
                SysCompanyItem.Columns.ItemID,
                SysCompanyItem.Columns.ItemTitle,
                SysCompanyItem.Columns.ItemStatus
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
            _ItemQueryColumnsFormatter.Add(SysCompanyItem.Columns.ItemStatus, (col, dr) =>
                {
                    var v = Utilities.ToString(dr[col]);
                    if (CompanyItemBLL.CompanyItemStatus.ContainsKey(v))
                        return CompanyItemBLL.CompanyItemStatus[v];
                    else
                        return v;
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

        #region for mobile companyitem

        public Common.ServicesResult GetItem(HttpContext context)
        {
            var id = Utilities.ToInt(context.Request["id"]);
            int code = -1;
            string message = string.Empty;
            Dictionary<string, object> result = new Dictionary<string, object>( );
            //var cols = _ItemQueryColumns.Where(col => !Utilities.Compare(SysCompanyItem.Columns.ItemTitle,col)).ToArray( );
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
            code = 0;
            return Common.ServicesResult.GetInstance(code, message, result);
        }
        public Common.ServicesResult GetItems(HttpContext context)
        {
            int code = -1;
            string message = string.Empty;
            object result = null;
            var p = Utilities.ToInt(context.Request["p"]);
            var d1 = Utilities.ToDateTime(context.Request["d1"]);
            var d2 = Utilities.ToDateTime(context.Request["d2"]).AddDays(1);
            if (d1 < AppContextBase.Context.Company.CompanyDateView.Value)
                d1 = AppContextBase.Context.Company.CompanyDateView.Value;
            var query = DB.Select(_ItemQueryColumns)
                         .From<SysCompanyItem>( )
                         .Where(SysCompanyItem.CompanyIDColumn).IsEqualTo(AppContextBase.Context.User.CompanyId)
                         .And(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                         .And(SysCompanyItem.ItemDateColumn).IsBetweenAnd(d1, d2)
                         .OrderDesc(SysCompanyItem.ItemIDColumn.QualifiedName);
            var pageCount = Utilities.CalcPageCount(_PageSize, query.GetRecordCount( ));
            result = query.Paged(p, _PageSize).GetDataReaderEnumerator( ).Select(dr =>
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>( );
                    foreach (var col in _ItemQueryColumns)
                    {
                        if (_ItemQueryColumnsFormatter.ContainsKey(col))
                            dict[col] = _ItemQueryColumnsFormatter[col](col, dr);
                        else
                            dict[col] = dr[col];
                    }
                    dict["Row"] = Utilities.ToInt(dr["Row"]).ToString("00");
                    return dict;
                });
            code = 0;
            return Common.ServicesResult.GetInstance(code, message, new { pageCount = pageCount, items = result });
        }
        public Common.ServicesResult SaveItem(HttpContext context)
        {
            int code = -1;
            string message;
            object ret = null;
            try
            {
                var data = Utilities.JSONToObj<Dictionary<string, object>>(context.Request["item"]);
                if (!data.ContainsKey(SysCompanyItem.Columns.ItemID))
                {
                    message = "数据格式不合法.";
                    goto lbl_return;
                }
                var id = Utilities.ToInt(data[SysCompanyItem.Columns.ItemID]);
                data.Remove(SysCompanyItem.Columns.ItemID);
                SysCompanyItem item = SysCompanyItem.FetchByID(id);
                if (item != null || AppContextBase.Context.Company == null)
                {
                    if (AppContextBase.Context.Company == null || item.CompanyID != AppContextBase.Context.Company.Id)
                    {
                        message = "你没权限进行此操作.";
                        goto lbl_return;
                    }
                }
                else
                {
                    item = new SysCompanyItem( );
                    item.MemberLimit = 0;
                    item.IsCanDel = 0;
                    item.IsPass = true;
                    item.AreaDepth = AppContextBase.Context.Company.AreaDepth;
                    item.IsDeleted = false;
                    item.ItemClicked = 0;
                    item.ItemUsed = 0;
                }
                foreach (var pair in data)
                {
                    item.SetColumnValue(pair.Key, pair.Value);
                }
                var img = context.Request["img"];
                if (!string.IsNullOrEmpty(img))
                {
                    var imgData = Convert.FromBase64String(img);
                    var result = FileUpload.SaveUploadFile(imgData, FileType.Image, SaveType.CompanyItem, "a.jpg", out message, true);
                    if (result == null)
                        goto lbl_return;
                    item.ItemPic = result.RelPath;
                }
                if (item.ItemSum == null)
                    item.ItemSum = 0M;
                if (item.ItemAmount == null)
                    item.ItemAmount = 0;
                if (item.OrderSumLimit == null)
                    item.OrderSumLimit = 0;
                item.CompanyID = AppContextBase.Context.User.CompanyId.Value;
                item.Save( );
                data[SysCompanyItem.Columns.ItemID] = item.ItemID;
                message = "保存成功.";
                code = 0;
                ret = data;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        lbl_return:
            return Common.ServicesResult.GetInstance(code, message, ret);
        }
        public Common.ServicesResult DelItem(HttpContext context)
        {
            var id = Utilities.ToInt(context.Request["id"]);
            var t = context.Request["t"];
            int code = -1;
            string message;
            var item = SysCompanyItem.FetchByID(id);
            if (item == null)
            {
                message = "抢购项目不存在.";
                goto lbl_return;
            }
            if (item.CompanyID != AppContextBase.Context.User.CompanyId)
            {
                message = "你没权限进行此操作.";
                goto lbl_return;
            }
            if (string.IsNullOrEmpty(t))
                item.IsDeleted = true;
            else
                item.ItemStatus = !item.ItemStatus.Value;
            item.Save( );
            message = "删除成功.";
            code = 0;
        lbl_return:
            return Common.ServicesResult.GetInstance(code, message, null);
        }
        public Common.ServicesResult GetRushItems(HttpContext context)
        {
            var p = Utilities.ToInt(context.Request["p"]);
            var d1 = Utilities.ToDateTime(context.Request["d1"]);
            var d2 = Utilities.ToDateTime(context.Request["d2"]).AddDays(1);
            int total = 0;
            var sp = SP_.SpGetRushRecord(AppContextBase.Context.User.CompanyId, d1, d2, p, _PageSize, total);
            var result = sp.GetDataReaderEnumerator( ).Select(dr =>
                {
                    return new
                    {
                        MemberPhoneNumber = dr[SysMember.Columns.MemberPhoneNumber],
                        OrderPrice = dr[OrdersDetail.Columns.OrderPrice],
                        OrderDate = Utilities.ToDateTime(dr[SysMemberItem.Columns.OrderDate]).ToString("MM-dd HH:mm:ss"),
                        ItemPoint = dr[SysMemberItem.Columns.ItemPoint],
                        ItemID = dr[SysMemberItem.Columns.ItemID]
                    };
                }).ToArray( );
            total = Utilities.CalcPageCount(_PageSize, Utilities.ToInt(sp.OutputValues[0]));
            return Common.ServicesResult.GetInstance(0, null, new { pageCount = total, items = result });
        }
        #endregion
    }
}
