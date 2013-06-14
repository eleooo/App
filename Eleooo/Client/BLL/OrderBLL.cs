using System;
using System.Collections.Generic;
using System.Text;
using Eleooo.DAL;
using System.Data;
using SubSonic.Utilities;

namespace Eleooo.Client
{
    public class OrderBLL
    {
        public SysMember OrderUser
        {
            get { return OrderData.OrderUser; }
            set { OrderData.OrderUser = value; }
        }
        private DataTable _userInfo;
        public DataTable UserInfo
        {
            get
            {
                if (_userInfo == null)
                {
                    _userInfo = EntityFormat.GetUserInfoTable( );
                }
                return _userInfo;
            }
        }
        private OrderEntity _orderData;
        public OrderEntity OrderData
        {
            get
            {
                if (_orderData == null)
                    _orderData = new OrderEntity( );
                return _orderData;
            }
        }
        public void InitOrder( )
        {
            UserInfo.Rows.Clear( );
            OrderUser = null;
            _orderData = null;
        }
        public bool GetOrderUserByPhone(string phoneNum, out SysMemberCash userCash, out string message)
        {
            userCash = null;
            try
            {
                DataTable dtUserInfo;
                SysMember user;
                bool bRet = ServiceProvider.Service.GetMemberForOrder(0, phoneNum, out user, out dtUserInfo, out userCash, out message);
                if (_userInfo != null)
                    _userInfo.Dispose( );
                _userInfo = dtUserInfo;
                if (user != null)
                {
                    user.MemberBalance = Formatter.ToInt(user.MemberBalance);
                    user.MemberBalanceCash = Formatter.ToInt(user.MemberBalanceCash);
                    user.MarkClean( );
                }
                OrderUser = user;
                return bRet;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        private static List<string> _orderProducts;
        public static List<string> OrderProducts
        {
            get
            {
                if (_orderProducts == null)
                {
                    _orderProducts = new List<string>( );
                    string sCompanyItem = AppContext.Company.CompanyItem;
                    if (!string.IsNullOrEmpty(sCompanyItem))
                    {
                        foreach (string item in sCompanyItem.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (item.Trim( ) != string.Empty)
                            {
                                _orderProducts.Add(item);
                            }
                        }
                    }
                    else
                    {
                        _orderProducts.Add("普通消费");
                    }
                }
                return _orderProducts;
            }
        }
        private static CompanyRateCollection _companyRates;
        public static CompanyRateCollection CompanyRates
        {
            get
            {
                if (_companyRates == null)
                {
                    _companyRates = new CompanyRateCollection( );
                    string sCompanyRate = string.IsNullOrEmpty(AppContext.Company.CompanyRate) ? string.Empty :
                        AppContext.Company.CompanyRate.Replace(" ", string.Empty);
                    decimal d = 0;
                    foreach (string rate in sCompanyRate.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (decimal.TryParse(rate, out d) && d >= 0)
                        {
                            _companyRates.Add(d);
                        }
                    }
                }
                return _companyRates;
            }
        }
        public static int SaveOrder(SysMember user, OrderEntity orderData, out string message)
        {
            try
            {
                return ServiceProvider.Service.SaveOrder(orderData.OrderData, user, string.IsNullOrEmpty(orderData.UserPwd) ? orderData.UserFinger : orderData.UserPwd, out message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return 1;
            }
        }
    }
    public class CompanyRate
    {
        public decimal Rate { get; set; }
        public override string ToString( )
        {
            return Rate.ToString("####0.####") + "%";
        }
    }
    public class CompanyRateCollection : List<CompanyRate>
    {
        public bool Add(decimal rate)
        {
            if (Contains(rate))
                return false;
            else
            {
                base.Add(new CompanyRate { Rate = rate });
                return true;
            }
        }
        
        public bool Contains(decimal rate)
        {
            return Find(rate) != null;
        }
        public CompanyRate Find(decimal rate)
        {
            CompanyRate item = this.Find((CompanyRate match) =>
                {
                    return match.Rate == rate;
                });
            return item;
        }
        public int FindIndex(decimal rate)
        {
            return FindIndex((CompanyRate match) =>
                {
                    return match.Rate == rate;
                });
        }
    }
}
