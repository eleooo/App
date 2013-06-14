using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;
using System.Data;
using Eleooo.Common;

namespace Eleooo.Web
{
    public static class OrderBLL
    {
        public static void UpdateUserRevenue(SysMember user, decimal dBalance, decimal dBalanceCash, decimal dSum)
        {
            user.MemberBalance = Convert.ToDecimal(user.MemberBalance) + dBalance;
            user.MemberBalanceCash = Convert.ToDecimal(user.MemberBalanceCash) + dBalanceCash;
            user.MemberSum = Convert.ToDecimal(user.MemberSum) + dSum;
            user.Save( );
            if (user.CompanyId > 0)
            {
                SysCompany company = SysCompany.FetchByID(user.CompanyId);
                if (company != null)
                {
                    company.CompanyBalance = Convert.ToDecimal(company.CompanyBalance) + dBalance;
                    company.CompanyBalanceCash = Convert.ToDecimal(company.CompanyBalanceCash) + dBalanceCash;
                    company.CompanySaleSum = Convert.ToDecimal(company.CompanySaleSum) + dSum;
                    company.Save( );
                }
            }
        }

        public static void UpdateUserBalance(SysMember user, decimal dBalance)
        {
            UpdateUserRevenue(user, dBalance, 0, 0);
        }
        public static void UpdateUserBalanceCash(SysMember user, decimal dBalanceCash)
        {
            UpdateUserRevenue(user, 0, dBalanceCash, 0);
        }
        public static void UpdateUserSum(SysMember user, decimal dSum)
        {
            UpdateUserRevenue(user, 0, 0, dSum);
        }

        public static void UpdateComRevenue(SysCompany company, decimal dBalance, decimal dBalanceCash, decimal dSum)
        {
            company.CompanyBalance = Convert.ToDecimal(company.CompanyBalance) + dBalance;
            company.CompanyBalanceCash = Convert.ToDecimal(company.CompanyBalanceCash) + dBalanceCash;
            company.CompanySaleSum = Convert.ToDecimal(company.CompanySaleSum) + dSum;
            company.Save( );
            SysMember user = DB.Select( ).From<SysMember>( )
                                 .Where(SysMember.CompanyIdColumn).IsEqualTo(company.Id)
                                 .ExecuteSingle<SysMember>( );
            if (user != null)
            {
                user.MemberBalance = Convert.ToDecimal(user.MemberBalance) + dBalance;
                user.MemberBalanceCash = Convert.ToDecimal(user.MemberBalanceCash) + dBalanceCash;
                user.MemberSum = Convert.ToDecimal(user.MemberSum) + dSum;
                user.Save( );
            }
        }

        public static void UpdateComBalance(SysCompany company, decimal dBalance)
        {
            UpdateComRevenue(company, dBalance, 0, 0);
        }

        public static void UpdateComBalanceCash(SysCompany company, decimal dBalanceCash)
        {
            UpdateComRevenue(company, 0, dBalanceCash, 0);
        }

        public static void UpdateComSum(SysCompany company, decimal dSum)
        {
            UpdateComRevenue(company, 0, 0, dSum);
        }

        public static void UpdateBalance( )
        {
            return;
            //SP_.UpdateSaleBalance( ).Execute( );
        }

        public static string GetOrderCode(SysCompany company)
        {
            string OrderCode = string.Empty;
            var getCodeSP = SP_.GenCompanyOrderCode(company.Id, OrderCode);
            getCodeSP.Execute( );
            OrderCode = Convert.ToString(getCodeSP.OutputValues[0]);
            return OrderCode;
        }

        public static bool SaveSaleRate(Order data, SysMember member,out string message)
        {
            message = string.Empty;

            //赠送积分＝(现金支付＋储值支付)　＊　赠送比例
            data.OrderPoint = (data.OrderPay + data.OrderPayCash) * data.OrderRate;
            if (data.OrderPoint.HasValue && data.OrderPoint > 0)
            {
                if (CompanyBLL.IsMaxPointLevel(data.OrderSellerID, data.OrderPoint.Value))
                {
                    message = "累计赠送的积分已经超过500，须进行积分结算后才能继续操作系统";
                    return false;
                }
            }
            data.Save( );
            //data.OrderPoint = (AppContextBase.Context.Company.CompanyType == 2) ? (data.OrderPayCash * data.OrderRate) : ((data.OrderPay + data.OrderPayCash) * data.OrderRate);
            //推荐好友奖励
            RewardBLL.RewardMemberPoint(member, data);
            if (data.OrderPoint > 0M)
            {
                new Payment
                {
                    PaymentDate = data.OrderDate,
                    PaymentCompanyID = data.OrderSellerID,
                    PaymentMemberID = data.OrderMemberID,
                    PaymentMemo = string.Format("在【{0}】消费{1:0.00}元,并获赠{2:0.00}个积分",  AppContextBase.Context.Company.CompanyName,data.OrderSumOk, data.OrderPoint.Value.ToString("#####0.00")),
                    PaymentStatus = 1,
                    PaymentSum = Convert.ToDecimal(data.OrderPoint),
                    PaymentType = 1,
                    PaymentOrderID = data.Id,
                    PaymentCode = data.OrderCode,
                    PaymentEmail = string.Empty
                }.Save( );
            }
            if (data.OrderPayPoint > 0M)
            {
                new Payment
                {
                    PaymentDate = data.OrderDate,
                    PaymentCompanyID = data.OrderSellerID,
                    PaymentMemberID = data.OrderMemberID,
                    PaymentMemo = string.Format("在【{0}】消费{1:0.00}元，其中使用{2:0.00}个积分抵扣", AppContextBase.Context.Company.CompanyName, data.OrderSumOk, Math.Abs(data.OrderPayPoint.Value).ToString("#####0.00")),
                    PaymentStatus = 1,
                    PaymentSum = -Convert.ToDecimal(data.OrderPayPoint),
                    PaymentType = 2,
                    PaymentOrderID = data.Id,
                    PaymentCode = data.OrderCode,
                    PaymentEmail = string.Empty
                }.Save( );
            }
            if (data.OrderPayCash > 0M)
            {
                #region hot company
                if (CompanyBLL.GetCompanyType(AppContextBase.Context.Company.CompanyType) == CompanyType.MealCompany)
                {
                    string queryCode = string.Concat(AreaBLL.GetCompanyCodePrefix(AppContextBase.Context.Company.CompanyCode), "%");
                    //queryCode = queryCode.Substring(0, queryCode.Length - 2) + "%";
                    var query = DB.Select("CashCompanyID", "sum(CashSum) as CashSum")
                                  .From<SysMemberCash>( ).InnerJoin(SysCompany.IdColumn, SysMemberCash.CashCompanyIDColumn)
                                  .Where(SysMemberCash.CashMemberIDColumn).IsEqualTo(member.Id)
                                  .And(SysCompany.CompanyCodeColumn).Like(queryCode)
                                  .ConstraintExpression("Group By CashCompanyID");
                    DataTable result = query.ExecuteDataTable( );
                    foreach (DataRow row in result.Rows)
                    {
                        if (!Utilities.IsNull(row["CashSum"]) && Convert.ToDecimal(row["CashSum"]) > 0M)
                        {
                            OrdersCashCompany orders_cashcompany = new OrdersCashCompany
                            {
                                CashCompanyID = Convert.ToInt32(row["CashCompanyID"]),
                                CashOrderID = data.Id,
                                CashSum = Convert.ToInt32(data.OrderPayCash)
                            };
                            if (Convert.ToInt32(row["CashCompanyID"]) != data.OrderSellerID)
                            {
                                orders_cashcompany.Save( );
                            }
                            new PaymentCash
                            {
                                PaymentCashDate = data.OrderDate,
                                PaymentCashCompanyID = orders_cashcompany.CashCompanyID,
                                PaymentCashMemberID = data.OrderMemberID,
                                PaymentMemo = string.Format("在【{0}】消费{1:0.00}元，使用储值支付{2:0.00}元", AppContextBase.Context.Company.CompanyName, data.OrderSumOk, Math.Abs(data.OrderPayCash.Value).ToString("######.##")),
                                PaymentStatus = 1,
                                PaymentCashSum = -Convert.ToDecimal(data.OrderPayCash),
                                PaymentType = 2,
                                PaymentOrderID = data.Id,
                                PaymentCashCode = data.OrderCode
                            }.Save( );
                            new SysMemberCash
                            {
                                CashCompanyID = orders_cashcompany.CashCompanyID,
                                CashMemberID = data.OrderMemberID,
                                CashOrderID = data.Id,
                                CashDate = data.OrderDate,
                                CashSum = -data.OrderPayCash,
                                CashRate = 0M,
                                CashPoint = 0,
                                CashMemo = "储值支付",
                                CashRateSale = 0,
                            }.Save( );
                        }
                    }
                }
                #endregion
                else
                {
                    new PaymentCash
                    {
                        PaymentCashDate = data.OrderDate,
                        PaymentCashCompanyID = data.OrderSellerID,
                        PaymentCashMemberID = data.OrderMemberID,
                        PaymentMemo = string.Format("在【{0}】消费{1:0.00}元，其中储值支付{2:0.00}元", AppContextBase.Context.Company.CompanyName, data.OrderSumOk, Math.Abs(data.OrderPayCash.Value).ToString("#######.##")),
                        PaymentStatus = 1,
                        PaymentCashSum = -Convert.ToDecimal(data.OrderPayCash),
                        PaymentType = 2,
                        PaymentOrderID = data.Id,
                        PaymentCashCode = data.OrderCode
                    }.Save( );
                    new SysMemberCash
                    {
                        CashCompanyID = data.OrderSellerID,
                        CashMemberID = data.OrderMemberID,
                        CashOrderID = data.Id,
                        CashDate = data.OrderDate,
                        CashSum = -(data.OrderPayCash),
                        CashRate = 0M,
                        CashPoint = 0,
                        CashMemo = "储值支付",
                        CashRateSale = 0
                    }.Save( );
                }
            }

            return true;
        }
        public static bool CheckCanOrder(DateTime dtOrder,int companyID)
        {
            var query = DB.Select( ).From<VPaymentRateDate>( )
                       .Where(VPaymentRateDate.PaymentRateDateColumn).IsGreaterThanOrEqualTo(dtOrder)
                       .And(VPaymentRateDate.PaymentRateCompanyIDColumn).IsEqualTo(companyID);
            return query.GetRecordCount( ) == 0;
        }
        public static bool DeleteMemberOrder(int orderId, SysCompany company, out string message)
        {
            message = string.Empty;
            if (company == null)
            {
                message = "参数错误";
                goto lbl_false;
            }
            Order order = DB.Select( ).From<Order>( )
                            .Where(Order.IdColumn).IsEqualTo(orderId)
                            .And(Order.OrderSellerIDColumn).IsEqualTo(company.Id)
                            .And(Order.OrderTypeColumn).IsEqualTo((int)OrderType.Common)
                            .ExecuteSingle<Order>( );
            if (order == null)
            {
                message = "消费记录不存在！";
                goto lbl_false;
            }
            if (order.OrderMemberID != company.Id && !CheckCanOrder(order.OrderDate, company.Id))
            {
                message = "已经结算的消费记录不能删除";
                goto lbl_false;
            }
            //处理消费赠送积分
            Payment p1 = null;
            if (order.OrderPoint.HasValue && order.OrderPoint.Value > 0)
            {
                p1 = DB.Select( ).From<Payment>( )
                       .Where(Payment.PaymentOrderIDColumn).IsEqualTo(order.Id)
                       .And(Payment.PaymentCompanyIDColumn).IsEqualTo(company.Id)
                       .And(Payment.PaymentCodeColumn).IsEqualTo(order.OrderCode)
                       .And(Payment.PaymentTypeColumn).IsEqualTo((int)PaymentType.ConsumeGive)
                       .ExecuteSingle<Payment>( );
            }
            //处理消费积分支付
            Payment p2 = null;
            if (order.OrderPayPoint.HasValue && order.OrderPayPoint.Value > 0)
            {
                p2 = DB.Select( ).From<Payment>( )
                       .Where(Payment.PaymentOrderIDColumn).IsEqualTo(order.Id)
                       .And(Payment.PaymentCompanyIDColumn).IsEqualTo(company.Id)
                       .And(Payment.PaymentCodeColumn).IsEqualTo(order.OrderCode)
                       .And(Payment.PaymentTypeColumn).IsEqualTo((int)PaymentType.Mortgage)
                       .ExecuteSingle<Payment>( );
            }
            //处理消费储值支付
            PaymentCash payCash = null;
            SysMemberCash cash = null;
            if (order.OrderPayCash.HasValue && order.OrderPayCash.Value > 0)
            {
                payCash = DB.Select( ).From<PaymentCash>( )
                            .Where(PaymentCash.PaymentOrderIDColumn).IsEqualTo(order.Id)
                            .And(PaymentCash.PaymentCashCompanyIDColumn).IsEqualTo(company.Id)
                            .And(PaymentCash.PaymentCashCodeColumn).IsEqualTo(order.OrderCode)
                            .And(PaymentCash.PaymentTypeColumn).IsEqualTo((int)PaymentCashType.Mortgage)
                            .ExecuteSingle<PaymentCash>( );
                cash = DB.Select( ).From<SysMemberCash>( )
                         .Where(SysMemberCash.CashCompanyIDColumn).IsEqualTo(company.Id)
                         .And(SysMemberCash.CashOrderIDColumn).IsEqualTo(order.Id)
                         .And(SysMemberCash.CashSumColumn).IsEqualTo(-order.OrderPayCash.Value)
                         .ExecuteSingle<SysMemberCash>( );
            }
            //处理推荐奖励
            SysMemberReward r = DB.Select( ).From<SysMemberReward>( )
                                  .Where(SysMemberReward.OrderIDColumn).IsEqualTo(order.Id)
                                  .ExecuteSingle<SysMemberReward>( );
            Payment p3 = null;
            if (r != null)
                p3 = Payment.FetchByID(r.PaymentID);
            //执行操作
            Order.Delete(order.Id);
            if (p1 != null)
                Payment.Delete(p1.Id);
            if (p2 != null)
                Payment.Delete(p2.Id);
            if (cash != null)
                SysMemberCash.Delete(cash.CashID);
            if (payCash != null)
                PaymentCash.Delete(payCash.Id);
            if (r != null)
                SysMemberReward.Delete(r.RewardID);
            if (p3 != null)
                Payment.Delete(p3.Id);
            message = "删除成功";
            return true;
        lbl_false:
            return false;
        }
        public static bool DeleteMemberCash(int cashId, SysCompany company, out string message)
        {
            message = string.Empty;
            if (company == null)
            {
                message = "参数错误";
                goto lbl_false;
            }
            SysMemberCash cash = DB.Select( ).From<SysMemberCash>( )
                                   .Where(SysMemberCash.CashIDColumn).IsEqualTo(cashId)
                                   .And(SysMemberCash.CashSumColumn).IsGreaterThan(0)
                                   .And(SysMemberCash.CashOrderIDColumn).IsEqualTo(0)
                                   .And(SysMemberCash.CashCompanyIDColumn).IsEqualTo(company.Id)
                                   .ExecuteSingle<SysMemberCash>( );
            if (cash == null)
            {
                message = "充值记录不存在！";
                goto lbl_false;
            }
            if (!CheckCanOrder(cash.CashDate.Value, company.Id))
            {
                message = "已经结算的储值记录不能删除";
                goto lbl_false;
            }
            //如果充值记录已经进行消费,则不允许删除
            decimal cashSum = UserBLL.GetUserBalanceCash(cash.CashMemberID.Value, company.Id);
            if (cash.CashSum.Value > cashSum)
            {
                message = "此充值记录已经进行了消费,请先将消费记录删除!";
                goto lbl_false;
            }

            //处理paymentcash
            PaymentCash payCash = DB.Select( ).From<PaymentCash>( )
                                    .Where(PaymentCash.PaymentCashCompanyIDColumn).IsEqualTo(company.Id)
                                    .And(PaymentCash.PaymentOrderIDColumn).IsEqualTo(cash.CashID)
                                    .And(PaymentCash.PaymentTypeColumn).In((int)PaymentCashType.Import, (int)PaymentCashType.Prepaid)
                                    .ExecuteSingle<PaymentCash>( );
            if (cash == null)
            {
                message = "充值记录不存在!";
                goto lbl_false;
            }
            //处理赠送积分
            Payment p = null;
            if (cash.CashPoint.HasValue && cash.CashPoint.Value != 0)
            {
                p = DB.Select( ).From<Payment>( )
                      .Where(Payment.PaymentCompanyIDColumn).IsEqualTo(company.Id)
                      .And(Payment.PaymentSumColumn).IsEqualTo(cash.CashPoint)
                      .And(Payment.PaymentTypeColumn).IsEqualTo((int)PaymentType.PrepaidGive)
                      .And(Payment.PaymentOrderIDColumn).IsEqualTo(cash.CashID)
                      .ExecuteSingle<Payment>( );
            }
            SysMemberCash.Delete(cash.CashID);
            PaymentCash.Delete(payCash.Id);
            if (p != null)
                Payment.Delete(p.Id);
            message = "删除成功";
            return true;
        lbl_false:
            return false;
        }
        public static bool SaveMemberCash(SysMemberCash cash, SysMember user,out string message)
        {
            //check is owner user
            //if (!CompanyBLL.CheckIsOwnerUser(user.Id, cash.CashCompanyID.Value))
            //{
            //    new SysMemberCompany
            //    {
            //        MemberCompanyCompanyID = cash.CashCompanyID.Value,
            //        MemberCompanyDate = DateTime.Now,
            //        MemberCompanyMemberID = user.Id
            //    }.Save( );
            //}
            message = "";
            if (cash.CashPoint.HasValue && cash.CashPoint.Value > 0)
            {
                if (CompanyBLL.IsMaxPointLevel(cash.CashCompanyID.Value, cash.CashPoint.Value))
                {
                    message = "累计赠送的积分已经超过500，须进行积分结算后才能继续操作系统";
                    return false;
                }
            }
            cash.Save( );
            new PaymentCash
            {
                PaymentCashCode = "",
                PaymentCashDate = DateTime.Now,
                PaymentCashCompanyID = cash.CashCompanyID,
                PaymentCashMemberID = cash.CashMemberID,
                PaymentCashSum = Convert.ToDecimal(cash.CashSum),
                PaymentOrderID = cash.CashID,
                PaymentMemo = string.Format("在【{0}】充值{1:0.00}元，并获赠{2:0.00}个积分", AppContextBase.Context.Company.CompanyName, cash.CashSum, cash.CashPoint),
                PaymentStatus = 1,
                PaymentType = 1
            }.Save( );

            if (cash.CashPoint > 0)
            {
                new Payment
                {
                    PaymentCode = "",
                    PaymentDate = DateTime.Now,
                    PaymentCompanyID = cash.CashCompanyID,
                    PaymentMemberID = cash.CashMemberID,
                    PaymentSum = Convert.ToDecimal(cash.CashPoint),
                    PaymentOrderID = cash.CashID,
                    PaymentMemo = string.Format("在【{0}】充值{1:0.00}元，并获赠{2:0.00}个积分", AppContextBase.Context.Company.CompanyName, cash.CashSum, cash.CashPoint),
                    PaymentStatus = 1,
                    PaymentType = 3,
                    PaymentEmail = ""
                }.Save( );
            }
            return true;
        }
    }
}