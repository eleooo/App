using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace Eleooo.Client
{
    public class Formatter
    {
        public static decimal CalcSumOk(decimal dsumOk)
        {
            return Math.Round(dsumOk);
        }
        public static decimal CalcSumOk(string sumok)
        {
            decimal dsumok;
            if (decimal.TryParse(sumok, out dsumok))
                return CalcSumOk(dsumok);
            else
                return 0;
        }
        public static decimal Low(object val)
        {
            int v = ToInt(val);
            return (decimal)v;
        }
        public static decimal Round(object val, int num)
        {
            decimal d = ToDecimal(val);
            return Math.Round(d, num);
        }
        public static decimal Round(object val)
        {
            return Round(val, 0);
        }
        public static DateTime GetMonthFirstDate(int adder)
        {
            DateTime dt = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day));
            return dt.AddMonths(adder);
        }
        public static string SubStr(object obj, int a_Cnt)
        {
            if (IsNull(obj))
                return string.Empty;
            else
                return SubStr(obj.ToString( ), a_Cnt);
        }
        public static string SubStr(string a_SrcStr, int a_Cnt)
        {
            return SubStr(a_SrcStr, 0, a_Cnt);
        }
        public static string SubStr(string a_SrcStr, int a_StartIndex, int a_Cnt)
        {
            if (string.IsNullOrEmpty(a_SrcStr))
                return a_SrcStr;
            else if (a_SrcStr.Length <= a_Cnt)
                return a_SrcStr;
            else
                return a_SrcStr.Substring(a_StartIndex, a_Cnt) + "...";
        }
        public static T ToEnum<T>(object val)
        {
            return ToEnum<T>(ToString(val));
        }
        public static T ToEnum<T>(int val)
        {
            return (T)Enum.ToObject(typeof(T), val);
        }
        public static T ToEnum<T>(string val)
        {
            try
            {
                if (string.IsNullOrEmpty(val))
                    return default(T);
                int i;
                if (int.TryParse(val, out i) && i >= 0)
                    return (T)Enum.ToObject(typeof(T), i);
                else
                    return (T)Enum.Parse(typeof(T), val);
            }
            catch { return default(T); }
        }

        public static string ToString(object obj)
        {
            if (IsNull(obj))
                return string.Empty;
            else
                return Convert.ToString(obj);
        }
        public static decimal ToDecimal(object obj)
        {
            if (obj == null)
                return 0;
            decimal d;
            decimal.TryParse(obj.ToString( ), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.CurrentCulture, out d);
            return d;
        }
        public static int ToInt(object obj)
        {
            if (IsNull(obj))
                return 0;
            bool b;
            if (bool.TryParse(obj.ToString( ), out b))
                return b ? 1 : 0;
            return (int)ToDecimal(obj);
        }
        public static bool ToBool(object obj)
        {
            if (IsNull(obj))
                return false;
            bool b;
            if (bool.TryParse(obj.ToString( ), out b))
                return b;
            else if (ToInt(obj) > 0)
                return true;
            else
                return false;
        }
        public static bool IsNull(object val)
        {
            if (val is DBNull)
                return true;
            if (val == null)
                return true;
            return false;
        }
        public static DateTime ToDateTime(string datetime)
        {
            DateTime dt;
            if (!string.IsNullOrEmpty(datetime) && DateTime.TryParse(datetime, out dt))
                return dt;
            else
                return DateTime.MinValue.AddYears(1753);
        }
        public static DateTime ToDateTime(object datetime)
        {
            return ToDateTime(Convert.ToString(datetime));
        }
        public static string ToDate(string datetime)
        {
            DateTime dt;
            if (DateTime.TryParse(datetime, out dt) && dt > DateTime.MinValue.AddYears(1753))
                return dt.ToString("yyyy-MM-dd");
            else
                return string.Empty;
        }
    }
}