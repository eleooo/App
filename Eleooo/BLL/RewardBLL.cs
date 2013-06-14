using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class RewardBLL
    {
        private static Dictionary<string, string> _rewardFlag;
        public static Dictionary<string, string> RewardFlag
        {
            get
            {
                if (_rewardFlag == null)
                {
                    _rewardFlag = new Dictionary<string, string>( );
                    _rewardFlag.Add("0", "否");
                    _rewardFlag.Add("1", "是");
                }
                return _rewardFlag;
            }
        }
        public static decimal OrderMealRewardRate(SysCompany company)
        {
            if (company != null && (company.IsPoint ?? (bool?)true).Value)
                return 3M;
            else
                return 0;
        }
        private static decimal _rewardRate = 0;
        public static decimal RewardRate
        {
            get
            {
                if (_rewardRate == 0)
                {
                    //_rewardRate = Utilities.ToDecimal(ResBLL.GetRes("RewardRate", "0.05", "推荐好友奖励设置"));
                    var query = DB.Select(SysReward.Columns.RewardRate)
                                  .Top("1")
                                  .From<SysReward>( )
                                  .Where(SysReward.RewardIDColumn).IsGreaterThan(0)
                                  .OrderDesc(SysReward.Columns.RewardID);
                    _rewardRate = query.ExecuteScalar<decimal>( );
                }
                return _rewardRate;
            }
        }
        public static string RewardRateStr
        {
            get
            {
                return (RewardRate).ToString("####.##") + "%";
            }
        }
        public static SysReward Reward
        {
            get
            {
                return DB.Select( ).Top("1").From<SysReward>( ).OrderDesc(SysReward.Columns.RewardID)
                                 .ExecuteSingle<SysReward>( );
            }
        }
        public static void CancelRewardMemberPointForOrderMeal(Order order)
        {
            Payment p = OrderMealBLL.GetOrderMealPayment(order, order.OrderMemberID);
            if (p != null)
                Payment.Delete(p.Id);
            order.OrderPoint = 0;
            order.OrderRate = 0;
            QueryCommand cmd = new QueryCommand("SELECT MemberPid FROM SYS_MEMBER WHERE ID = @ID;");
            cmd.AddParameter("@ID", order.OrderMemberID);
            int userPID = Utilities.ToInt(DataService.ExecuteScalar(cmd));
            SysMemberReward r;
            Payment rp;
            TryGetMemberReward(order, userPID, out r, out rp);
            if (r != null)
                SysMemberReward.Delete(r.RewardID);
            if (rp != null)
                Payment.Delete(rp.Id);
        }
        public static decimal CalcRewardMemberPointForOrderMeal(Order order,SysCompany company)
        {
            return order.OrderSum.Value * (OrderMealRewardRate(company) / 100M);
        }
        public static void RewardMemberPointForOrderMeal(SysMember user, SysCompany company, Order order)
        {
            if (Formatter.ToEnum<OrderType>(order.OrderType.Value) != OrderType.OrderMeal)
                return;
            decimal point = CalcRewardMemberPointForOrderMeal(order,company);
            if (point <= 0)
                return;
            string memo = "在【{0}】订餐消费{1:0.00}元，您获奖励{2:0.00}个积分";
            //处理消费赠送积分
            Payment p = OrderMealBLL.GetOrderMealPayment(order, order.OrderMemberID);
            if (p == null)
                p = new Payment
                {
                    PaymentDate = order.OrderDate,
                    PaymentCode = order.OrderCode,
                    PaymentCompanyID = UserBLL.MainCompanyAccount.Id,
                    PaymentEmail = string.Empty,
                    PaymentMemberID = user.Id,
                    PaymentMemo = string.Format(memo, company.CompanyName, order.OrderSumOk, point),
                    PaymentOrderID = order.Id,
                    PaymentStatus = 1,
                    PaymentSum = point,
                    PaymentType = (int)PaymentType.ConsumeGive
                };
            else
            {
                p.PaymentMemo = string.Format(memo, company.CompanyName, order.OrderSumOk, point);
                p.PaymentSum = point;
            }
            p.Save( );
            UpdateOrderPoint(order.Id, point);
            RewardMemberPoint(user, order);
            //r.PaymentID = p.Id;
            //r.ValidateWhenSaving = false;
            //r.Save( );
        }
        private static void UpdateOrderPoint(int orderId, decimal orderPoint)
        {
            SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("UPDATE ORDERS SET OrderPoint = @OrderPoint where ID=@ID");
            cmd.AddParameter("@OrderPoint", orderPoint, System.Data.DbType.Decimal);
            cmd.AddParameter("@ID", orderId, System.Data.DbType.Int32);
            SubSonic.DataService.ExecuteQuery(cmd);
        }
        public static void RewardMemberPoint(SysMember user, Order order)
        {
            //if (AppContextBase.Context.CompanyType.HasValue && AppContextBase.Context.CompanyType.Value != CompanyType.UnionCompany)
            //    return;
            if (!user.MemberPid.HasValue || user.MemberPid.Value <= 0)
                return;
            SysMember rUser = SysMember.FetchByID(user.MemberPid);
            if (rUser == null || rUser.CompanyId > 0)
                return;
            SysReward reward = Reward;
            if (reward == null)
                return;
            //是否在推广期内注册
            bool isCanReward = user.MemberDate.Value >= reward.RewardDate.Value && user.MemberDate.Value < reward.RewardEndDate.Value.AddDays(1);
            if (!isCanReward)
                return;
            //是否一直有效
            if (reward.RewardFlag.HasValue && reward.RewardFlag.Value == 0)
            {
                var query = DB.Select( ).From<SysMemberReward>( )
                              .Where(SysMemberReward.OrderMemberIDColumn).IsEqualTo(user.Id)
                              .And(SysMemberReward.RewardMemberIDColumn).IsEqualTo(rUser.Id);
                if (query.GetRecordCount( ) > 0)
                    return;
            }
            decimal point = order.OrderSumOk.Value * (RewardRate / 100M);
            if (point <= 0)
                return;
            string companyName = CompanyBLL.GetCompanyName(order.OrderSellerID);
            string memo = "好友{0}在【{1}】消费{2:0.00}元，您获奖励{3:0.00}个积分";
            SysMemberReward r;
            Payment p;
            TryGetMemberReward(order, rUser.Id, out r, out p);
            if (r == null)
                r = new SysMemberReward( );
            if (p == null)
                p = new Payment( );
            r.OrderCompanyID = order.OrderSellerID;
            r.OrderID = order.Id;
            r.OrderMemberID = user.Id;
            r.OrderSumOk = order.OrderSumOk.Value;
            r.RewardMemberID = rUser.Id;
            r.RewardPoint = point;
            r.RewardDate = order.OrderDate;


            p.PaymentDate = order.OrderDate;
            p.PaymentCode = string.Empty;
            p.PaymentCompanyID = UserBLL.MainCompanyAccount.Id;
            p.PaymentEmail = string.Empty;
            p.PaymentMemberID = rUser.Id;
            p.PaymentMemo = string.Format(memo, CompanyBLL.EnUserPhoe(user.MemberPhoneNumber), companyName, order.OrderSumOk, point);
            p.PaymentOrderID = order.Id;
            p.PaymentStatus = 1;
            p.PaymentSum = point;
            p.PaymentType = (int)PaymentType.Reward;

            p.Save( );
            r.PaymentID = p.Id;
            r.ValidateWhenSaving = false;
            r.Save( );
        }
        private static void TryGetMemberReward(Order order, int rUserID, out SysMemberReward r, out Payment p)
        {
            p = null;
            r = DB.Select( ).From<SysMemberReward>( )
                      .Where(SysMemberReward.OrderCompanyIDColumn).IsEqualTo(order.OrderSellerID)
                      .And(SysMemberReward.RewardMemberIDColumn).IsEqualTo(rUserID)
                      .And(SysMemberReward.OrderMemberIDColumn).IsEqualTo(order.OrderMemberID)
                      .ExecuteSingle<SysMemberReward>( );
            if (r != null)
                p = Payment.FetchByID(r.PaymentID);
        }
    }
}