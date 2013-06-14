using System;
using System.Collections.Generic;
using System.Text;
using Eleooo.DAL;

namespace Eleooo.Client
{
    public class CashEntity
    {
        internal SysMember CashUser { get; set; }
        private SysMemberCash _cashData;
        internal SysMemberCash CashData
        {
            get
            {
                if (_cashData == null)
                {
                    _cashData = new SysMemberCash
                    {
                        CashCompanyID = AppContext.Company.Id,
                        CashDate = DateTime.Now,
                        CashMemberID = 0,
                        CashMemo = GradeBLL.DefaultGradeIDStr,
                        CashPoint = null,
                        CashOrderID = 0,
                        CashRate = null,
                        CashSum = 0
                    };
                }
                return _cashData;
            }
        }
        public void InitCashData( )
        {
            _cashData = null;
            CashUser = null;
            PhoneNum = string.Empty;
        }
        public string PhoneNum { get; set; }
        public decimal CashSum
        {
            get
            {
                return Convert.ToDecimal(CashData.CashSum);
            }
            set
            {
                CashData.CashSum = value;
            }
        }
        public decimal? CashRate
        {
            get
            {
                return CashData.CashRate;
            }
            set
            {
                CashData.CashRate = value;
            }
        }
        public decimal? CashPoint
        {
            get
            {
                return CashData.CashPoint;
            }
            set
            {
                CashData.CashPoint = value;
            }
        }
        public decimal? CashPointRate
        {
            get
            {
                return CashData.CashRateSale;
            }
            set
            {
                CashData.CashRateSale = value;
            }
        }
        public string CashMemo
        {
            get
            {
                return CashData.CashMemo;
            }
            set
            {
                CashData.CashMemo = value;
            }
        }

    }
}
