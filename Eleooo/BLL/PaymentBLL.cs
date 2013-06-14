using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class PaymentBLL
    {
        public static PaymentType GetPaymentType(int? flag)
        {
            if (flag.HasValue)
                return (PaymentType)flag.Value;
            else
                return PaymentType.All;
        }
    }
}