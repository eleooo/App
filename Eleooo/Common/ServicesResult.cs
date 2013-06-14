using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eleooo.Common
{
    public class ServicesResult
    {
        private static readonly Type _type = typeof(ServicesResult);
        public int code { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public static ServicesResult GetInstance(int c, string msg, object d)
        {
            return new ServicesResult { data = d, code = c, message = msg };
        }
        public static ServicesResult GetInstance(object d)
        {
            return GetInstance(0, string.Empty, d);
        }
        public override string ToString( )
        {
            if (!string.IsNullOrEmpty(message))
                message = message.Trim('\r', '\n');
            return Utilities.ObjToJSON(this);
        }

        public enum ResultType
        {
            ValueType,
            ServicesResultType,
            Other
        }

        public static ResultType GetResultType(Type type)
        {
            if (type.IsValueType)
                return ResultType.ValueType;
            else if (type.Equals(_type))
                return ResultType.ServicesResultType;
            else
                return ResultType.Other;
        }
    }
}
