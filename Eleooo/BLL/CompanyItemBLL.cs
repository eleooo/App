using System;
using System.Collections.Generic;
using System.Web;
using System.Transactions;
using Eleooo.DAL;
using SubSonic;
using System.Data;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class CompanyItemBLL
    {
        public static readonly Dictionary<int, string> ItemCheckResult;
        static CompanyItemBLL( )
        {
            //-- 1 success,-1 已过期,-2 仅限抢一次, 
            //-- -3上次抢购还没消费,-4 本期内限一次,-5 限外来会员,-6 限本店会员
            //-- -7上月消费额度不足, -8 非法账号,-9不在所属商圈会员
            ItemCheckResult = new Dictionary<int, string>( );
            ItemCheckResult.Add(1, "成功");
            ItemCheckResult.Add(-1, "此优惠项目已经过期.");
            ItemCheckResult.Add(-2, "您已抢购了一次，无法再次抢购^_^");
            ItemCheckResult.Add(-3, "您已抢购过该商家的优惠，无法再次抢购^_^");
            ItemCheckResult.Add(-4, "此优惠项目仅限抢购一次!");
            ItemCheckResult.Add(-5, "此优惠项目仅限外来会员");
            ItemCheckResult.Add(-6, "此优惠项目仅限本店会员");
            ItemCheckResult.Add(-7, "根据您上月消费额，暂无权限抢购此优惠^_^");
            ItemCheckResult.Add(-8, "非法账号");
            ItemCheckResult.Add(-9, "该优惠仅限部分商圈会员享有，您暂无权限抢购");
            ItemCheckResult.Add(-10, "您已抢购此优惠，无法再次抢购^_^");

            ItemCheckResult.Add(-15, "此促销项目已经停止^_^");
            ItemCheckResult.Add(-16, "您要在促销时段内才能抢购哦^_^");
            ItemCheckResult.Add(-17, "您的平均订餐金额未达到商家要求哦^_^");
            ItemCheckResult.Add(-18, "您最近两周订餐次数未达到商家要求哦^_^");
            ItemCheckResult.Add(-19, "您今天已抢购了一次，无法再次抢购^_^");
            ItemCheckResult.Add(-20, "抢光了，明天早点儿下手哦^_^");
            ItemCheckResult.Add(-21, "此促销项目目前缺货");
        }
        public const string IMGE_TPL = "<img alt='' src='{0}' />";
        public const string TEMPLATE = @"
<div class='picContentInfo'>
{0}
<div class='content'>
{1}
</div>
</div>";
        const string CHECK_COMPANY_ITEM_FUNC = "dbo.CheckCompanyItemCanOrder({0})";
        public const decimal DEF_CHANGE_RATIO = 100M;
        private static Dictionary<string, string> _memberLimit;
        public static Dictionary<string, string> MemberLimit
        {
            get
            {
                if (_memberLimit == null)
                {
                    _memberLimit = new Dictionary<string, string>( );
                    _memberLimit.Add("0", "所有会员");
                    _memberLimit.Add("1", "本店会员");
                    _memberLimit.Add("2", "外来会员");
                }
                return _memberLimit;
            }
        }
        private static Dictionary<string, string> _itemStatus;
        public static Dictionary<string, string> Itemstatus
        {
            get
            {
                if (_itemStatus == null)
                {
                    //1 已抢购 2已消费 3 已过期
                    _itemStatus = new Dictionary<string, string>( );
                    _itemStatus.Add("1", "已抢购");
                    _itemStatus.Add("2", "已消费");
                    _itemStatus.Add("3", "已过期");
                    _itemStatus.Add("4", "已取消");
                }
                return _itemStatus;
            }
        }
        private static Dictionary<string, string> _itemLimit;
        public static Dictionary<string, string> ItemLimit
        {
            get
            {
                if (_itemLimit == null)
                {
                    _itemLimit = new Dictionary<string, string>( );
                    _itemLimit.Add("1", "限一次");
                    _itemLimit.Add("2", "本期限一次");
                    _itemLimit.Add("3", "每天限一次");
                }
                return _itemLimit;
            }
        }

        private static Dictionary<string, string> _companyItemStatus;
        public static Dictionary<string, string> CompanyItemStatus
        {
            get
            {
                if (_companyItemStatus == null)
                {
                    _companyItemStatus = new Dictionary<string, string>( );
                    _companyItemStatus.Add("0", "停止");
                    _companyItemStatus.Add("1", "正常");
                }
                return _companyItemStatus;
            }
        }

        private static Dictionary<string, string> _isItemCanDel;
        public static Dictionary<string, string> IsItemCanDel
        {
            get
            {
                if (_isItemCanDel == null)
                {
                    _isItemCanDel = new Dictionary<string, string>( );
                    _isItemCanDel.Add("0", "否");
                    _isItemCanDel.Add("1", "是");
                }
                return _isItemCanDel;
            }
        }
        public static MemberCompanyItemStatus GetItemStatus(int? status)
        {
            if (status.HasValue)
                return (MemberCompanyItemStatus)status.Value;
            else
                return MemberCompanyItemStatus.None;
        }
        //[ThreadStatic]
        private static List<string> _itemLimitCol;
        private static List<string> ItemLimitCol
        {
            get
            {
                if (_itemLimitCol == null)
                {
                    //@CompanyID INT, --优惠项目商家
                    //@ItemID INT, --优惠项目ID,
                    //@ItemPoint DECIMAL(18,2), --抢购所需积分
                    //@AreaDepth NVARCHAR(500), --覆盖区域
                    //@OrderSumLimit DECIMAL(18,2), --消费层次
                    //@MemberLimit INT, --消费对象 0 所有会员 1 本店会员 2外来会员
                    //@ItemLimit INT, --抢购频率 0表示不限
                    //@EndDate DATETIME  --有效期限
                    //@ItemStatus INT, --0 停止 1正常
                    //@OrderFreqLimit INT, --消费频率限制 上二个星期消费次数
                    //@WorkingHours nvarchar(max), --时间段
                    //@CompanyType INT --商家类型
                    _itemLimitCol = new List<string>
                    {
                        Utilities.GetTableColumn(SysCompanyItem.CompanyIDColumn),
                        Utilities.GetTableColumn(SysCompanyItem.ItemIDColumn),
                        Utilities.GetTableColumn(SysCompanyItem.ItemPointColumn),
                        Utilities.GetTableColumn(SysCompanyItem.AreaDepthColumn),
                        Utilities.GetTableColumn(SysCompanyItem.OrderSumLimitColumn),
                        Utilities.GetTableColumn(SysCompanyItem.MemberLimitColumn),
                        Utilities.GetTableColumn(SysCompanyItem.ItemLimitColumn),
                        Utilities.GetTableColumn(SysCompanyItem.ItemEndDateColumn),
                        Utilities.GetTableColumn(SysCompanyItem.ItemStatusColumn),
                        Utilities.GetTableColumn(SysCompanyItem.OrderFreqLimitColumn),
                        Utilities.GetTableColumn(SysCompanyItem.ItemAmountColumn),
                        Utilities.GetTableColumn(SysCompanyItem.WorkingHoursColumn),
                        Utilities.GetTableColumn(SysCompany.CompanyTypeColumn),
                        Utilities.GetTableColumn(SysCompanyItem.ItemInfoColumn)
                    };
                }
                return _itemLimitCol;
            }
        }
        private static List<string> GetParamList(int userID, decimal lastOrderSum, int orderId = 0)
        {
            List<string> lstParam = new List<string>
            {
                userID.ToString(),
                lastOrderSum.ToString(),
                orderId.ToString()
            };
            lstParam.AddRange(ItemLimitCol);
            return lstParam;
        }
        public static string RenderCheckItemFunc(int userID, decimal lastOrderSum, int equalVal)
        {
            List<string> lstParam = GetParamList(userID, lastOrderSum);
            return string.Format("And ({0} = {1})", string.Format(CHECK_COMPANY_ITEM_FUNC, string.Join(",", lstParam.ToArray( ))), equalVal);
        }
        public static string RenderCheckItemFuncInVals(int userID, decimal lastOrderSum, params string[] inVals)
        {
            List<string> lstParam = GetParamList(userID, lastOrderSum);
            return string.Format("And ({0} IN ({1}))", string.Format(CHECK_COMPANY_ITEM_FUNC, string.Join(",", lstParam.ToArray( ))), string.Join(",", inVals));
        }
        public static int ExecuteCheckFunc(int userID, int itemID, decimal lastOrderSum, int orderId = 0)
        {
            List<string> lstParam = GetParamList(userID, lastOrderSum, orderId);
            var sql = "SELECT {0} FROM dbo.Sys_Company_Item,dbo.Sys_Company WHERE CompanyID = ID AND [ItemID] = {1}";
            QueryCommand cmd = new QueryCommand(string.Format(sql, string.Format(CHECK_COMPANY_ITEM_FUNC, string.Join(",", lstParam.ToArray( ))), itemID), DB.Provider.Name);
            return Utilities.ToInt(DataService.ExecuteScalar(cmd));
        }
        public static void UpdateExpireItem( )
        {
            var updater = SP_.UpdateExpireItem( );
            updater.Execute( );
        }
        public static string GetItemStatusText(DataRow row)
        {
            MemberCompanyItemStatus status = Formatter.ToEnum<MemberCompanyItemStatus>(row[SysMemberItem.Columns.ItemStatus]);
            CompanyType type = CompanyType.SpecialCompany;
            if (row.Table.Columns.Contains(SysCompany.Columns.CompanyType))
                type = Formatter.ToEnum<CompanyType>(row[SysCompany.Columns.CompanyType]);
            if (status == MemberCompanyItemStatus.InProgress)
                return "已抢购";
            //return type == CompanyType.UnionCompany ? "已预订" : "已抢购";
            else if (status == MemberCompanyItemStatus.Completed)
                return "已消费";
            else if (status == MemberCompanyItemStatus.OutDate)
                return "已过期";
            else
                return "None";
        }
        public static int? GetCompanyLastItemID(int userID, int companyID)
        {
            string vSql = @"Select top 1 ItemID from Sys_Company_Item
                            Where CompanyID = {0} And IsDeleted=0 And IsPass=1 And ItemClicked < ItemAmount And ItemEndDate >= dbo.GetToDay() Order By ItemId DESC";
            //QueryCommand cmd = new QueryCommand(string.Format(vSql, companyID, RenderCheckItemFunc(userID, 0, 1)));
            QueryCommand cmd = new QueryCommand(string.Format(vSql, companyID));
            int v = Utilities.ToInt(DataService.ExecuteScalar(cmd));
            if (v > 0)
                return v;
            else
                return null;
        }
        public static bool CheckCompanyItemOnceLimit(int companyID, int userID)
        {
            string vSql = @"select count(*) from dbo.Sys_Member_Item as t1
                            Inner Join dbo.Sys_Company_Item as t2 On t1.CompanyItemID = t2.ItemID
                            where t1.[CompanyID] = {0} and t1.[MemberID] = {1} and t2.[ItemLimit]={2}  and t1.[ItemStatus]<3";
            QueryCommand cmd = new QueryCommand(string.Format(vSql, companyID, userID, 1));
            return Utilities.ToInt(DataService.ExecuteScalar(cmd)) > 0;
        }
        public static bool OrderCompanyItem(SysCompany company, int itemID, string MemberPwd, string MemberFinger, out string message)
        {
            SysMemberItem mItem = SysMemberItem.FetchByID(itemID);
            if (mItem == null || company == null)
            {
                message = "参数错误";
                goto lbl_false;
            }
            if (mItem.CompanyID != company.Id)
            {
                message = "你不能结算不是本商家的优惠项目";
                goto lbl_false;
            }
            SysCompanyItem cItem = SysCompanyItem.FetchByID(mItem.CompanyItemID);
            if (cItem == null)
            {
                message = "参数错误";
                goto lbl_false;
            }
            SysMember user = SysMember.FetchByID(mItem.MemberID);
            if (user == null)
            {
                message = "会员不存在";
                goto lbl_false;
            }

            if (string.IsNullOrEmpty(MemberFinger) && string.IsNullOrEmpty(MemberPwd))
            {
                message = "请输入会员密码";
                goto lbl_false;
            }
            if (!UserBLL.CheckUserPwd(user, MemberPwd) &&
                 !UserBLL.CheckUserFinger(user, MemberFinger))
            {
                message = "会员密码或者指纹不正确!";
                goto lbl_false;
            }
            return SaveOrder(company, mItem, cItem, user, out message);
        lbl_false:
            return false;
        }
        private static bool SaveOrder(SysCompany company, SysMemberItem mItem, SysCompanyItem cItem, SysMember user, out string message)
        {
            message = string.Empty;
            //Order order = new Order
            //{
            //    OrderCode = OrderBLL.GetOrderCode(company),
            //    OrderCard = string.Empty,
            //    OrderDate = DateTime.Now,
            //    OrderDateDeliver = DateTime.Now,
            //    OrderDateUpload = DateTime.Now,
            //    OrderMemberID = user.Id,
            //    OrderMemo = string.Empty,
            //    OrderProduct = "优惠消费",
            //    OrderQty = 0,
            //    OrderRateSale = 0,
            //    OrderRate = 0,
            //    OrderPoint = 0,
            //    OrderSellerID = company.Id,
            //    OrderStatus = 1,
            //    OrderSum = cItem.ItemSum,
            //    OrderSumOk = cItem.ItemSum,
            //    OrderPay = 0,
            //    OrderPayCash = 0,
            //    OrderPayPoint = mItem.ItemPoint,
            //    OrderType = (int)OrderType.CompanyItem
            //};
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                //order.Save( );
                //if (mItem.ItemPoint.HasValue && mItem.ItemPoint.Value > 0)
                //{
                //    new Payment
                //    {
                //        PaymentCode = order.OrderCode,
                //        PaymentCompanyID = company.Id,
                //        PaymentDate = order.OrderDate,
                //        PaymentEmail = string.Empty,
                //        PaymentMemberID = user.Id,
                //        PaymentMemo = string.Format("{0}抢购{1}的优惠项目,并且使用{2}个积分抵扣", user.MemberPhoneNumber, company.CompanyName, -mItem.ItemPoint.Value),
                //        PaymentOrderID = order.Id,
                //        PaymentStatus = 1,
                //        PaymentSum = -mItem.ItemPoint.Value,
                //        PaymentType = (int)PaymentType.CompanyItem
                //    }.Save( );
                //}
                //如果是联盟商家则更新积分状态
                CompanyType companyType = Formatter.ToEnum<CompanyType>(company.CompanyType);
                Payment p = Payment.FetchByID(mItem.PaymentID.Value);
                if (p != null)
                {
                    p.PaymentStatus = 1;
                    p.Save( );
                }
                mItem.ItemStatus = (int)MemberCompanyItemStatus.Completed;
                mItem.SetDate = DateTime.Now;
                mItem.Save( );
                cItem.ItemUsed = Utilities.ToInt(cItem.ItemUsed) + 1;
                cItem.Save( );
                OrderBLL.UpdateBalance( );
                ts.Complete( );
                message = string.Format("结算成功,{0}使用{1}个积分购得{2}", user.MemberFullname, mItem.ItemPoint, cItem.ItemTitle);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
        }
        public static bool CompleteMemberMealItem(Order order, out string message)
        {
            message = string.Empty;
            if (!OrderMealBLL.HasCompanyItemInfo(order.Id))
                return true;
            if (order.OrderType != (int)OrderType.OrderMeal)
            {
                message = "订单类型错误.";
                return false;
            }
            SysMemberItem mItem = GetMemberMealItemByOrderId(order.Id, order.OrderMemberID);
            if (mItem == null)
            {
                message = "没有相应的抢购记录.";
                return false;
            }
            mItem.ItemStatus = (int)MemberCompanyItemStatus.Completed;
            mItem.SetDate = DateTime.Now;
            mItem.Save( );
            return true;
        }
        public static bool CancelMemberMealItem(Order order, out string message)
        {
            message = string.Empty;
            if (!OrderMealBLL.HasCompanyItemInfo(order.Id))
                return true;
            if (order.OrderType != (int)OrderType.OrderMeal)
            {
                message = "订单类型错误.";
                return false;
            }
            SysMemberItem mItem = GetMemberMealItemByOrderId(order.Id, order.OrderMemberID);
            if (mItem == null)
            {
                message = "没有相应的抢购记录.";
                return false;
            }
            mItem.ItemStatus = (int)MemberCompanyItemStatus.Cancel;
            mItem.Save( );
            QueryCommand cmd = new QueryCommand("update Sys_company_item Set ItemClicked = ItemClicked-1 Where ItemID=@ItemID AND ItemClicked > 0");
            cmd.AddParameter("@ItemID", mItem.CompanyItemID, DbType.Int32);
            DataService.ExecuteQuery(cmd);
            if (mItem.PaymentID.HasValue)
                Payment.Delete(mItem.PaymentID);

            return true;
        }
        public static bool CancelCompanyItem(SysMember user, SysCompanyItem companyItem, SysMemberItem memberItem, out string message)
        {
            message = string.Empty;
            if (companyItem == null || user == null || memberItem == null)
            {
                message = "参数错误!";
                goto lbl_end;
            }
            if (companyItem.IsCanDel.HasValue && companyItem.IsCanDel.Value != 1)
            {
                message = "本单不支持退订";
                goto lbl_end; ;
            }
            if (memberItem.ItemStatus == (int)MemberCompanyItemStatus.Completed)
            {
                message = "此单你已经消费 ,不能再退订";
                goto lbl_end;
            }
            TransactionScope ts = null;
            SharedDbConnectionScope ss = null;
            try
            {
                ts = new TransactionScope( );
                ss = new SharedDbConnectionScope( );

                if (memberItem.PaymentID.HasValue)
                    Payment.Delete(memberItem.PaymentID.Value);
                SysMemberItem.Delete(memberItem.ItemID);
                companyItem.ItemClicked = companyItem.ItemClicked.Value - 1;
                companyItem.Save( );
                ts.Complete( );

                message = "退订成功";
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                Logging.Log("CompanyItemBLL->CancelCompanyItem", ex, true);
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
        lbl_end:
            return false;
        }
        public static bool ClickCompanyItem(SysCompany company, SysMember user, DateTime dtDate, SysCompanyItem item, out int memberItemID, out string message)
        {
            memberItemID = 0;
            if (item == null || user == null)
            {
                message = "参数错误!";
                goto lbl_end;
            }

            if (item.ItemClicked >= item.ItemAmount)
            {
                message = "已经达到了最大可{ItemType}的数量";
                goto lbl_end;
            }
            //check ItemLimitOnce
            if (CheckCompanyItemOnceLimit(company.Id, user.Id))
            {
                message = ItemCheckResult[-3];
                goto lbl_end;
            }
            decimal userLastOrderSum = UserBLL.GetUserLastMonthOrderSum(user.Id);
            int result = CompanyItemBLL.ExecuteCheckFunc(user.Id, item.ItemID, userLastOrderSum);
            if (result < 0)
            {
                message = ItemCheckResult.ContainsKey(result) ? ItemCheckResult[result] : "你无权{ItemType}此优惠项目!";
                goto lbl_end;
            }
            //判断会员积分是否足够
            decimal point = DB.Select(SysMember.Columns.MemberBalance).From<SysMember>( )
                              .Where(SysMember.IdColumn).IsEqualTo(user.Id)
                              .ExecuteScalar<decimal>( );
            if (item.ItemPoint.HasValue && point < item.ItemPoint.Value)
            {
                message = "您的账户积分余额不足，无法{ItemType}^_^";
                goto lbl_end;
            }
            SysMemberItem data = new SysMemberItem
            {
                CompanyID = item.CompanyID,
                CompanyItemID = item.ItemID,
                ItemDate = dtDate,
                OrderDate = DateTime.Now,
                MemberID = user.Id,
                OrderSum = userLastOrderSum,
                ItemPoint = item.ItemPoint,
                ItemStatus = (int)MemberCompanyItemStatus.InProgress,
                SetDate = null,
                IsCanModifiedDate = true,
                PaymentID = 0,
                OrderID = 0
            };
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                if (item.ItemPoint.HasValue && item.ItemPoint.Value > 0)
                {
                    Payment p = new Payment
                    {
                        PaymentCode = string.Empty,
                        PaymentCompanyID = item.CompanyID,
                        PaymentDate = data.OrderDate.Value,
                        PaymentEmail = string.Empty,
                        PaymentMemberID = user.Id,
                        PaymentMemo = string.Format("抢购【{0}】的优惠项目,并使用{1:0.00}个积分消费", company.CompanyName, item.ItemPoint.Value),
                        PaymentOrderID = 0,
                        PaymentStatus = 2,
                        PaymentSum = -item.ItemPoint.Value,
                        PaymentType = (int)PaymentType.CompanyItem
                    };
                    p.Save( );
                    data.PaymentID = p.Id;
                }
                data.Save( );
                item.ItemClicked = Utilities.ToInt(item.ItemClicked) + 1;
                item.Save( );
                OrderBLL.UpdateBalance( );
                ts.Complete( );
                message = "{ItemType}成功";
                memberItemID = data.ItemID;
                return true;
            }
            catch (Exception ex)
            {
                message = "{ItemType}失败:" + ex.Message;
                Logging.Log("CompanyItemBLL->ClickCompanyItem", ex, true);
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
        lbl_end:
            return false;
        }
        public static bool CanClickCompanyMealItem(SysCompany company, SysMember user, SysCompanyItem item, int orderId, out decimal userLastOrderSum, out string message)
        {
            userLastOrderSum = 0;
            if (item == null || user == null || company.Id != item.CompanyID)
            {
                message = "参数错误!";
                goto lbl_end;
            }
            userLastOrderSum = UserBLL.GetUserAvgOrderSum(user.Id);
            int result = CompanyItemBLL.ExecuteCheckFunc(user.Id, item.ItemID, userLastOrderSum, orderId);
            if (result < 0)
            {
                message = ItemCheckResult.ContainsKey(result) ? ItemCheckResult[result] : "你无权抢购此优惠项目!";
                goto lbl_end;
            }
            //判断会员积分是否足够
            decimal point = DB.Select(SysMember.Columns.MemberBalance).From<SysMember>( )
                              .Where(SysMember.IdColumn).IsEqualTo(user.Id)
                              .ExecuteScalar<decimal>( );
            if (item.ItemPoint.HasValue && point < item.ItemPoint.Value)
            {
                message = "您的账户积分不足，无法抢购此优惠";
                goto lbl_end;
            }
            message = string.Empty;
            return true;
        lbl_end:
            return false;
        }
        public static void ClickCompanyMealItem(SysCompany company, SysMember user, SysCompanyItem item, Order order, decimal userLastOrderSum)
        {
            Payment p = null;
            Func<SysMemberItem> fnNew = ( ) =>
            {
                item.ItemClicked = Utilities.ToInt(item.ItemClicked) + 1;
                item.Save( );
                if (item.ItemPoint.HasValue && item.ItemPoint.Value > 0)
                {
                    p = new Payment
                    {
                        PaymentCode = string.Empty,
                        PaymentCompanyID = item.CompanyID,
                        PaymentDate = order.OrderDate,
                        PaymentEmail = string.Empty,
                        PaymentMemberID = user.Id,
                        PaymentOrderID = order.Id,
                        PaymentStatus = 1,
                        PaymentType = (int)PaymentType.CompanyItem
                    };
                }
                return new SysMemberItem( ) { PaymentID = 0, CompanyID = company.Id, CompanyItemID = item.ItemID, MemberID = user.Id, OrderID = order.Id };
            };
            Func<SysMemberItem> fnOld = ( ) =>
                {
                    var memberItem = GetMemberMealItemByOrderId(order.Id, user.Id);
                    if (memberItem != null)
                        p = Payment.FetchByID(memberItem.PaymentID);
                    return memberItem;
                };
            SysMemberItem data = fnOld( ) ?? fnNew( );
            data.ItemDate = order.OrderDate;
            data.OrderDate = order.OrderDate;
            data.OrderSum = userLastOrderSum;
            data.ItemPoint = item.ItemPoint;
            data.ItemStatus = (int)MemberCompanyItemStatus.InProgress;
            data.SetDate = null;
            data.IsCanModifiedDate = true;
            if (p != null)
            {
                if (item.ItemPoint.HasValue && item.ItemPoint.Value > 0)
                {
                    p.PaymentMemo = string.Format("抢购【{0}】的优惠项目,并使用{1:0.00}个积分消费", company.CompanyName, item.ItemPoint);
                    p.PaymentSum = -item.ItemPoint.Value;
                    p.Save( );
                    data.PaymentID = p.Id;
                }
                else
                {
                    Payment.Delete(p.Id);
                    data.PaymentID = 0;
                }
            }
            data.Save( );
        }
        public static SysMemberItem GetMemberMealItemByOrderId(int orderId, int userId)
        {
            var query = DB.Select( ).From<SysMemberItem>( )
                         .Where(SysMemberItem.MemberIDColumn).IsEqualTo(userId)
                         .And(SysMemberItem.OrderIDColumn).IsEqualTo(orderId);
            return query.ExecuteSingle<SysMemberItem>( );
        }
        public static SqlQuery GetOrderCompanyItemQuery(string MemberPhone, string MemberPwd, string MemberFinger, int companyID, out int flag, out string message)
        {
            int id = 0;
            flag = 0;
            message = string.Empty;
            CompanyType cmpType = Formatter.ToEnum<CompanyType>(AppContextBase.Context.Company.CompanyType);
            if (cmpType == CompanyType.AdCompany || cmpType == CompanyType.MealCompany)
            {
                message = "您暂无权限使用该功能";
                flag = 1;
                goto lbl_query;
            }
            if (!string.IsNullOrEmpty(MemberPhone) &&
                (!string.IsNullOrEmpty(MemberPwd) || !string.IsNullOrEmpty(MemberFinger)))
            {
                SysMember user = UserBLL.GetUserByPhoneNum(MemberPhone);
                if (user == null)
                {
                    message = "会员不存在!";
                    flag = 1;
                    goto lbl_query;
                }
                if (!UserBLL.CheckUserPwd(user, MemberPwd) && !UserBLL.CheckUserFinger(user, MemberFinger))
                {
                    message = "会员密码或者指纹不正确!";
                    flag = 2;
                    goto lbl_query;
                }
                id = user.Id;
            }
        lbl_query:
            var query = DB.Select(Utilities.GetTableColumns(SysMemberItem.Schema),
                                  "1 as Amount",
                                  SysCompanyItem.Columns.ItemTitle,
                                  SysCompanyItem.ItemSumColumn.ColumnName,
                                  SysMember.Columns.MemberPhoneNumber)
                          .From<SysMemberItem>( )
                          .InnerJoin(SysMember.IdColumn, SysMemberItem.MemberIDColumn)
                          .InnerJoin(SysCompanyItem.ItemIDColumn, SysMemberItem.CompanyItemIDColumn)
                          .Where(SysMember.IdColumn).IsEqualTo(id)
                          .And(SysMemberItem.ItemStatusColumn).IsEqualTo((int)MemberCompanyItemStatus.InProgress)
                          .And(SysMemberItem.CompanyIDColumn).IsEqualTo(companyID)
                          .OrderDesc(Utilities.GetTableColumn(SysMemberItem.ItemIDColumn));
            return query;
        }
        public static List<int> GetCookieRecViewItems( )
        {
            string cookieData = HttpUtility.UrlDecode(Utilities.GetCookie("RecViewItems").Value);
            if (string.IsNullOrEmpty(cookieData))
                return new List<int>( );
            return Utilities.JSONToObj<List<int>>(cookieData);
        }
        public static void UpdateCompanyItemSum(int? itemID, decimal itemSum, decimal? oldSum = null)
        {
            QueryCommand cmd;
            if (oldSum.HasValue)
            {
                cmd = new QueryCommand("UPDATE Sys_Company_Item SET [ItemSum]= ([ItemSum] - @OldSum + @ItemSum) WHERE charindex('['+@ItemID+']',ItemInfo) > 0 OR charindex('['+@ItemID+',',ItemInfo) > 0 OR charindex(','+@ItemID+',',ItemInfo) > 0 OR charindex(','+@ItemID+']',ItemInfo) >0;");
                cmd.AddParameter("@OldSum", oldSum.Value, DbType.Decimal);
                cmd.AddParameter("@ItemID", itemID.ToString( ), DbType.String);
            }
            else
            {
                cmd = new QueryCommand("UPDATE Sys_Company_Item SET [ItemSum]=@ItemSum WHERE ItemID = @ItemID AND  [ItemSum]<>@ItemSum;");
                cmd.AddParameter("@ItemID", Math.Abs(itemID.Value), DbType.Int32);
            }
            cmd.AddParameter("@ItemSum", itemSum, DbType.Decimal);
            DataService.ExecuteQuery(cmd);
        }
        public static void SetCookieRecViewItem(int itemID)
        {
            var items = GetCookieRecViewItems( );
            if (!items.Contains(itemID))
            {
                if (items.Count >= 4)
                    items.RemoveAt(3);
                items.Insert(0, itemID);
                Utilities.AddCookie("RecViewItems", HttpUtility.UrlEncode(Utilities.ObjToJSON(items)));
            }
        }
        public static void RemoveRecViewItems( )
        {
            Utilities.RemoveCookie("RecViewItems");
        }
    }
}