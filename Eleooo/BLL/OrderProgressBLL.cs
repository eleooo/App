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

        private static readonly string __NewProgressItemCmd = @"
INSERT INTO [dbo].[Orders_Log]
           ([Date]
           ,[Desc]
           ,[IsCurrent]
           ,[OrderId]
           ,[IsPlay]
           ,[FromUser]
		   ,[ToUser])
     SELECT TOP 1 @DateX
           ,@Desc
           ,@IsCurrent
           ,@OrderId
           ,0
           ,t2.ID
           ,t1.OrderMemberID
     FROM Orders as t1,Sys_Member as t2 WHERE t1.ID=@OrderID AND t1.OrderSellerID = t2.Company_ID;";

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
            //var isMember = AppContextBase.Context.CurrentSubSys == SubSystem.Member;
            var dt = DB.Select( ).From<OrdersLog>( )
                                 .Where(OrdersLog.OrderIdColumn).IsEqualTo(orderId)
                                 .And(OrdersLog.DateXColumn).IsLessThanOrEqualTo(DateTime.Now)
                                 .And(OrdersLog.IsCurrentColumn).IsGreaterThanOrEqualTo(0)
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
        private static object ConvertLogToTemp(int user, int cUser, IDataReader dr = null, OrdersLog log = null)
        {
            if (dr != null)
                return new
                {
                    type = Utilities.ToInt(dr[OrdersLog.Columns.FromUser]) == user ? 1 : 0,
                    time = Utilities.ToDateTime(dr[OrdersLog.Columns.DateX]).ToString("HH:mm:ss"),
                    voice = dr[OrdersLog.Columns.Voice],
                    isplay = Utilities.ToInt(dr[OrdersLog.Columns.ToUser]) == cUser ? dr[OrdersLog.Columns.IsPlay] : (object)false,
                    id = dr[OrdersLog.Columns.Id],
                    desc = dr[OrdersLog.Columns.Desc]
                };
            else
                return new
                {
                    type = log.FromUser == user ? 1 : 0,
                    time = log.DateX.Value.ToString("HH:mm:ss"),
                    voice = log.Voice,
                    isplay = log.ToUser.HasValue && log.ToUser == cUser ? log.IsPlay : (bool?)false,
                    id = log.Id,
                    desc = log.Desc
                };
        }
        public static object GetOrderLog(Order order)
        {
            int user = order.OrderMemberID;
            int cUser = AppContextBase.CurrentUserID;
            var query = DB.Select(" * ").From<OrdersLog>( )
                          .Where(OrdersLog.OrderIdColumn).IsEqualTo(order.Id)
                          .And(OrdersLog.DateXColumn).IsLessThanOrEqualTo(DateTime.Now)
                          .And(OrdersLog.IsCurrentColumn).IsGreaterThanOrEqualTo(0);
            var result = query.GetDataReaderEnumerator( ).Select(dr => ConvertLogToTemp(user, cUser, dr)).ToList( );
            return result;
        }
        public static bool AddOrderTempLog(HttpContext context, Order order, out object tempLog, out string message)
        {
            int toUser = order.OrderMemberID;
            int fromUser = AppContextBase.CurrentUserID;
            tempLog = null;
            if (fromUser <= 0)
            {
                message = "你不允许使用此服务.";
                return false;
            }
            var desc = context.Request["m"];
            string voice = null;
            if (context.Request.Files.Count > 0)
            {
                var file = context.Request.Files[0];
                var fileRst = FileUpload.SaveUploadFile(file, FileType.Media, SaveType.Voice, out message, true, order.Id.ToString( ));
                if (fileRst == null)
                    return false;
                voice = fileRst.FileName;
            }
            else if (string.IsNullOrEmpty(desc))
            {
                message = "请输入发送内容.";
                return false;
            }
            var log = new OrdersLog
            {
                DateX = DateTime.Now,
                Desc = desc,
                Voice = voice,
                FromUser = fromUser,
                ToUser = toUser,
                OrderId = order.Id,
                IsCurrent = 0,
                IsPlay = false
            };
            log.Save( );
            order.MsnType = 5;
            order.OrderUpdateOn = DateTime.Now;
            order.Save( );
            tempLog = ConvertLogToTemp(toUser, fromUser, null, log);
            message = "发送成功.";
            return true;
        }
        private static void AddProgressRow(Order order, DateTime dtDate, string desc, int isCurrent = 0)
        {
            //QueryCommand cmd = new QueryCommand(__NewProgressItemCmd);
            //cmd.AddParameter("@OrderID", order.Id, DbType.Int32);
            //cmd.AddParameter("@Desc", desc, DbType.String);
            //cmd.AddParameter("@IsCurrent", isCurrent, DbType.Int32);
            //cmd.AddParameter("@DateX", dtDate, DbType.DateTime);
            //DataService.ExecuteQuery(cmd);
            new OrdersLog
            {
                Desc = desc,
                IsCurrent = isCurrent,
                DateX = dtDate,
                OrderId = order.Id,
                FromUser = AppContextBase.CurrentUserID,
                ToUser = order.OrderMemberID,
                IsPlay = false
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
            AddProgressRow(order, dtDate, "订单已生成。");
            AddProgressRow(order, dtDate.AddSeconds(10), "乐多分系统正在帮您下单。");
            AddProgressRow(order, dtDate.AddSeconds(31), "餐厅正在确认您的订单内容。");

        }

        public static void AddOrderConfirmLog(Order order, SysCompany company, int msnType, bool hasChgPrice, string desc)
        {
            DateTime dtNow = DateTime.Now;
            int id = order.Id;
            int isCurrent = msnType == 1 || hasChgPrice ? 1 : 0;
            if (msnType == 0)
            {
                AddProgressRow(order, dtNow, desc);
                desc = string.Format("您的餐点预计{0}分钟左右送达（仅供参考，高峰时段以实际送达时间为准）。", company.OrderElapsed);
                AddProgressRow(order, dtNow.AddSeconds(10), desc);
            }
            else if (msnType == 2)
            {
                desc = desc.Replace(company.CompanyName + "表示，", "");
                AddProgressRow(order, dtNow, desc);
                AddProgressRow(order, dtNow.AddSeconds(4), "订单已取消。");
            }
            else if (hasChgPrice)
            {

                AddProgressRow(order, dtNow, desc);
                string desc2 = string.Format("您的餐点预计{0}分钟左右送达（仅供参考，高峰时段以实际送达时间为准）。", company.OrderElapsed);
                AddProgressRow(order, dtNow.AddSeconds(7), "订单已确认，餐厅开始备餐。");
                AddProgressRow(order, dtNow.AddSeconds(10), desc2, isCurrent);
            }
            else
                AddProgressRow(order, dtNow, desc, isCurrent);
        }
        public static void AddOrderConfirmLog(Order order, string desc)
        {
            DateTime dtNow = DateTime.Now;
            AddProgressRow(order, dtNow, desc);
        }
        public static void AddOrderConfirmLog(Order order, DateTime dtDate, string desc)
        {
            AddProgressRow(order, dtDate, desc);
        }
        public static void ClearOrderLog(int orderId)
        {
            QueryCommand cmd = new QueryCommand("delete Orders_Log where orderId=" + orderId.ToString( ));
            DataService.ExecuteQuery(cmd);
        }

    }
}