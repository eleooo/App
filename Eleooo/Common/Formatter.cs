using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace Eleooo.Common
{
    public class Formatter
    {
        private static readonly int _ChineseBeginChar = 19968;
        private const string PATTERN_QUOTE = @"(.*)(\(.*\))(.*)";
        public static string ReplaceQuote(string source, string replacement)
        {
            if (source == null)
                return null;
            source = source.Replace('（', '(').Replace('）', ')');
            return Regex.Replace(source, PATTERN_QUOTE, "$1" + replacement.Replace("{0}", "$2") + "$3");
        }
        public static string ReplaceWord(string source, string word, string replacement)
        {
            if (string.IsNullOrEmpty(word))
                return source;
            else
                return Regex.Replace(source, word, replacement.Replace("{0}", word));
        }
        public static bool IsChinese(string input)
        {
            return !string.IsNullOrEmpty(input) && (int)(input.First( )) >= _ChineseBeginChar;
        }
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
            int v = Utilities.ToInt(val);
            return (decimal)v;
        }
        public static decimal Round(object val, int num)
        {
            decimal d = Utilities.ToDecimal(val);
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
            if (Utilities.IsNull(obj))
                return string.Empty;
            else
                return SubStr(obj.ToString( ), a_Cnt);
        }
        public static string SubStr(string a_SrcStr, int a_Cnt)
        {
            return SubStr(a_SrcStr, 0, a_Cnt);
        }
        public static string SubStr(string a_SrcStr, int a_StartIndex, int a_Cnt, bool withEllipsis = true)
        {
            if (string.IsNullOrEmpty(a_SrcStr))
                return a_SrcStr;
            else if (a_SrcStr.Length <= a_Cnt)
                return a_SrcStr;
            else
                return withEllipsis ? a_SrcStr.Substring(a_StartIndex, a_Cnt) + "..." : a_SrcStr.Substring(a_StartIndex, a_Cnt);
        }

        public static T ToEnum<T>(object val)
        {
            return ToEnum<T>(Utilities.ToString(val));
        }
        public static T ToEnum<T>(int val)
        {
            return (T)Enum.ToObject(typeof(T), val);
        }
        public static T ToEnum<T>(string val)
        {
            return ToEnum<T>(val, default(T));
        }
        public static T ToEnum<T>(string val, T t)
        {
            try
            {
                if (string.IsNullOrEmpty(val))
                    return t;
                int i;
                if (int.TryParse(val, out i) && i >= 0)
                    return (T)Enum.ToObject(typeof(T), i);
                else
                    return (T)Enum.Parse(typeof(T), val, true);
            }
            catch { return t; }
        }
        public static T ValueIf<T>(T t, T comparer, T result)
        {
            return Object.Equals(t, comparer) ? result : t;
        }
        public static string Join(params string[] vals)
        {
            return vals == null || vals.Any(v => v == null) ? null : string.Join(string.Empty, vals);
        }
        public static T HackType<T>(object value)
        {
            Type conversionType = typeof(T);
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition( ).Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return default(T);

                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return (T)Convert.ChangeType(value, conversionType);
        }

        public static bool IsInteger(System.Data.DbType dbType)
        {
            return dbType == System.Data.DbType.Int16 ||
                    dbType == System.Data.DbType.Int32 ||
                    dbType == System.Data.DbType.Int64 ||
                    dbType == System.Data.DbType.Single ||
                    dbType == System.Data.DbType.UInt16 ||
                    dbType == System.Data.DbType.UInt32 ||
                    dbType == System.Data.DbType.UInt64;
        }
        public static bool IsNumberic(System.Data.DbType dbType)
        {
            return dbType == System.Data.DbType.Currency ||
                           dbType == System.Data.DbType.Decimal ||
                           dbType == System.Data.DbType.Double ||
                           dbType == System.Data.DbType.VarNumeric;
        }
        public static bool IsNumberic(string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;
            foreach (var s in source)
            {
                if (!(s >= 48 && s <= 57))
                    return false;
            }
            return true;
        }
        public static byte[] Base64ToImgage(string source)
        {
            return Convert.FromBase64String(source);
        }
    }
}