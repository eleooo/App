using System;
using System.Collections.Generic;
using System.Text;
using Eleooo.DAL;

namespace Eleooo.Client
{
    public class OrderEntity
    {
        internal SysMember OrderUser { get; set; }
        private Order _orderData;
        internal Order OrderData
        {
            get
            {
                if (_orderData == null)
                {
                    //will set default value on web services
                    _orderData = new Order
                    {
                        OrderSellerID = AppContext.Company.Id,
                        OrderSum = 0,
                        OrderPay = 0,
                        OrderPayCash = 0,
                        OrderPayPoint = 0,
                        OrderSumOk = 0,
                        OrderRate = 0,
                        OrderRateSale = 0,
                        OrderMemo = string.Empty,
                        OrderProduct = OrderBLL.OrderProducts[0],
                        OrderCard = string.Empty,
                        OrderQty = 0,
                        OrderType = 1,
                        OrderDate = DateTime.Now,
                        OrderCode = string.Empty,
                        OrderDateDeliver = DateTime.Now,
                        OrderDateUpload = DateTime.Now,
                        OrderMemberID = 0,
                        OrderStatus = 1,
                        OrderPoint = 0,
                        ServiceSum = 0,
                        MansionId = 0
                    };
                }
                return _orderData;
            }
        }
        public void InitOrder( )
        {
            UserPwd = string.Empty;
            UserFinger = string.Empty;
            PhoneNum = string.Empty;
            _orderData = null;
        }
        //flag 1 OrderSum,2 OrderPay,3 OrderPoint,4 OrderPayCash
        public void CalcSumOk(int flag)
        {
            switch (flag)
            {
                #region calc sumok
                case 1:
                    decimal dRateSale = Formatter.ToDecimal(OrderData.OrderRateSale);
                    if (dRateSale > 0 && dRateSale < 100)
                    {
                        OrderData.OrderSumOk = Math.Round(Convert.ToDecimal(OrderData.OrderSum * dRateSale / 100M));
                    }
                    else
                    {
                        OrderData.OrderSumOk = Math.Round(Convert.ToDecimal(OrderData.OrderSum));
                    }
                    if (OrderUser != null && OrderData.OrderPayPoint == 0 && OrderData.OrderPayCash == 0)
                    {
                        if (OrderUser.MemberBalance > 0)
                            OrderData.OrderPayPoint = OrderUser.MemberBalance > OrderData.OrderSumOk ? OrderData.OrderSumOk : OrderUser.MemberBalance;
                        else
                            OrderData.OrderPayPoint = 0;
                        if (OrderUser.MemberBalanceCash > 0)
                            OrderData.OrderPayCash = OrderUser.MemberBalanceCash > (OrderData.OrderSumOk - OrderData.OrderPayPoint) ? OrderData.OrderSumOk - OrderData.OrderPayPoint : OrderUser.MemberBalanceCash;
                        else
                            OrderData.OrderPayCash = 0;
                    }
                    OrderData.OrderPay = OrderData.OrderSumOk - OrderData.OrderPayPoint - OrderData.OrderPayCash;
                    OrderData.OrderPoint = OrderData.OrderPay * OrderData.OrderRate;
                    break;
                #endregion
                #region calc orderpay
                case 2:
                    if (OrderData.OrderPay >= OrderData.OrderSumOk)
                    {
                        OrderData.OrderPay = OrderData.OrderSumOk;
                        OrderData.OrderPayCash = 0;
                        OrderData.OrderPayPoint = 0;
                    }
                    else
                    {
                        int dVer = OrderSumOk - OrderPay;
                        if (OrderUser != null && OrderUser.MemberBalance > 0)
                        {
                            if (OrderUser.MemberBalance > dVer)
                            {
                                OrderData.OrderPayPoint = dVer;
                                dVer = 0;
                            }
                            else
                            {
                                OrderData.OrderPayPoint = OrderUser.MemberBalance;
                                dVer = dVer - Formatter.ToInt(OrderUser.MemberBalance);
                            }
                        }
                        if (OrderUser != null && OrderUser.MemberBalanceCash > 0)
                        {
                            if (OrderUser.MemberBalanceCash > dVer)
                            {
                                OrderData.OrderPayCash = dVer;
                                dVer = 0;
                            }
                            else
                            {
                                OrderData.OrderPayCash = OrderUser.MemberBalanceCash;
                                dVer = dVer - Formatter.ToInt(OrderUser.MemberBalanceCash);
                            }
                        }
                        if (dVer != 0)
                        {
                            OrderData.OrderPay = OrderSumOk;
                            OrderData.OrderPayCash = 0;
                            OrderData.OrderPayPoint = 0;
                        }
                    }
                    break;
                #endregion
                case 3:
                    if (OrderData.OrderPayPoint >= 0 && OrderData.OrderPayPoint <= OrderUser.MemberBalance)
                    {
                        int dVer = OrderSumOk - OrderPayPoint;
                        if (OrderUser.MemberBalanceCash > 0)
                        {
                            if (OrderData.OrderPayCash > 0)
                            {
                                if (OrderData.OrderPayCash > dVer)
                                {
                                    OrderData.OrderPayCash = dVer;
                                    dVer = 0;
                                }
                                else
                                {
                                    dVer = dVer - Formatter.ToInt(OrderData.OrderPayCash);
                                }
                            }
                            else if (OrderUser.MemberBalanceCash >= dVer)
                            {
                                OrderData.OrderPayCash = dVer;
                                dVer = 0;
                            }
                            else
                            {
                                OrderData.OrderPayCash = OrderUser.MemberBalanceCash;
                                dVer = dVer - Formatter.ToInt(OrderData.OrderPayCash);
                            }
                        }
                        OrderData.OrderPay = dVer;
                    }
                    break;
                case 4:
                    if (OrderData.OrderPayCash >= 0 && OrderData.OrderPayCash <= OrderUser.MemberBalanceCash)
                    {
                        int dVer = OrderSumOk - OrderPayCash;
                        if (OrderData.OrderPayPoint > 0)
                        {
                            if (OrderData.OrderPayPoint > dVer)
                            {
                                OrderData.OrderPayPoint = dVer;
                                dVer = 0;
                            }
                            else
                            {
                                dVer = dVer - Formatter.ToInt(OrderData.OrderPayPoint);
                            }
                        }
                        else if (OrderUser.MemberBalance > 0)
                        {
                            if (OrderUser.MemberBalance >= dVer)
                            {
                                OrderData.OrderPayPoint = dVer;
                                dVer = 0;
                            }
                            else
                            {
                                OrderData.OrderPayPoint = OrderUser.MemberBalance;
                                dVer = dVer - Formatter.ToInt(OrderData.OrderPayPoint);
                            }
                        }
                        OrderData.OrderPay = dVer;
                    }
                    break;
            }
        }
        public string PhoneNum
        {
            get;
            set;
        }
        public decimal OrderSum
        {
            get
            {
                if (OrderData.OrderSum == null)
                    OrderData.OrderSum = 0;
                return OrderData.OrderSum.Value;
            }
            set
            {
                OrderData.OrderSum = value;
                //CalcSumOk(1);
            }
        }
        //折扣
        public int OrderRateSale
        {
            get
            {
                if (OrderData.OrderRateSale == null)
                    OrderData.OrderRateSale = 0;
                return Formatter.ToInt(OrderData.OrderRateSale.Value);
            }
            set
            {
                OrderData.OrderRateSale = value;
                //CalcSumOk(1);
                //CalcSumOk(2);
            }
        }
        public int OrderSumOk
        {
            get
            {
                if (OrderData.OrderSumOk == null)
                    OrderData.OrderSumOk = 0;
                return  Formatter.ToInt (OrderData.OrderSumOk);
            }
            set
            {
                OrderData.OrderSumOk = value;
            }
        }
        public string OrderProduct
        {
            get
            {
                return OrderData.OrderProduct;
            }
            set
            {
                OrderData.OrderProduct = value;
            }
        }
        public int OrderPay //现金支付
        {
            get
            {
                if (OrderData.OrderPay == null)
                    OrderData.OrderPay = 0;
                return Formatter.ToInt(OrderData.OrderPay);
            }
            set
            {
                OrderData.OrderPay = value;
                //CalcSumOk(2);
            }
        }
        public int OrderPayCash //储值支付
        {
            get
            {
                if (OrderData.OrderPayCash == null)
                    OrderData.OrderPayCash = 0;
                return Formatter.ToInt(OrderData.OrderPayCash);
            }
            set
            {
                OrderData.OrderPayCash = value;
                //CalcSumOk(4);
            }
        }
        public int OrderPayPoint //积分支付
        {
            get
            {
                if (OrderData.OrderPayPoint == null)
                    OrderData.OrderPayPoint = 0;
                return Formatter.ToInt(OrderData.OrderPayPoint);
            }
            set
            {
                OrderData.OrderPayPoint = value;
                //CalcSumOk(3);
            }
        }
        public string OrderMemo
        {
            get
            {
                if (OrderData.OrderMemo == null)
                    OrderData.OrderMemo = string.Empty;
                return OrderData.OrderMemo;
            }
            set
            {
                OrderData.OrderMemo = value;
            }
        }
        public string UserPwd { get; set; }
        public string UserFinger { get; set; }
    }
}
