using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections.Specialized;
using System.Web;
using System.Collections;
using SubSonic;

namespace Eleooo
{
    public static class ExtendUtility
    {
        private static BindingFlags flag = BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

        public static void SetTypePrivateField(this Type type, object instance, string fieldName, object value)
        {
            if (type != null)
            {
                FieldInfo field = type.GetField(fieldName, flag);
                field.SetValue(instance, value);
            }
        }
        public static void SetTypePrivateProperty(this Type type, object instance, string propertyName, object value, object[] index)
        {
            if (type != null)
            {
                PropertyInfo field = type.GetProperty(propertyName, flag);
                field.SetValue(instance, value, index);
            }
        }
        public static T GetTypePrivateField<T>(this Type type, object instance, string fieldName)
        {
            if (type == null)
                goto lbl_def;

            FieldInfo field = type.GetField(fieldName, flag);
            if (field != null)
            {
                object val = field.GetValue(instance);
                if (val != null)
                    return (T)val;
            }
        lbl_def:
            return default(T);
        }
        public static T GetTypePrivateProperty<T>(this Type type, object instance, string propertyName, object[] index)
        {
            if (type == null)
                goto lbl_def;
            PropertyInfo field = type.GetProperty(propertyName, flag);
            if (field != null)
            {
                object val = field.GetValue(instance, index);
                if (val != null)
                    return (T)val;
            }
        lbl_def:
            return default(T);
        }
        public static T CallTypePrivateMethod<T>(this Type type, object instance, string name, Type[] types, params object[] param)
        {
            if (type == null)
                goto lbl_def;
            MethodInfo method = type.GetMethod(name, flag, null, CallingConventions.Any, types == null ? Type.EmptyTypes : types, null);
            object val = method.Invoke(instance, param);
            if (val != null)
                return (T)val;
        lbl_def:
            return default(T);
        }


        public static T GetPrivateField<T>(this object instance, string fieldName)
        {
            if (instance == null)
                goto lbl_def;
            Type type = instance.GetType( );
            return type.GetTypePrivateField<T>(instance, fieldName);
        lbl_def:
            return default(T);
        }
        public static T GetPrivateProperty<T>(this object instance, string propertyName)
        {
            return GetPrivateProperty<T>(instance, propertyName, null);
        }
        public static T GetPrivateProperty<T>(this object instance, string propertyName, object[] index)
        {
            if (instance == null)
                goto lbl_def;
            Type type = instance.GetType( );
            return type.GetTypePrivateProperty<T>(instance, propertyName, index);
        lbl_def:
            return default(T);
        }
        public static void SetPrivateField(this object instance, string fieldName, object value)
        {
            if (instance != null)
            {
                Type type = instance.GetType( );
                type.SetTypePrivateField(instance, fieldName, value);
            }
        }
        public static void SetPrivateProperty(this object instance, string propertyName, object value)
        {
            SetPrivateProperty(instance, propertyName, value, null);
        }
        public static void SetPrivateProperty(this object instance, string propertyName, object value, object[] index)
        {
            if (instance != null)
            {
                Type type = instance.GetType( );
                type.SetTypePrivateProperty(instance, propertyName, value, index);
            }
        }
        public static T CallPrivateMethod<T>(this object instance, string name)
        {
            return CallPrivateMethod<T>(instance, name, null);
        }
        public static T CallPrivateMethod<T>(this object instance, string name, params object[] param)
        {
            return CallPrivateMethod<T>(instance, name, GetParamTypes(param), param);
        }
        public static T CallPrivateMethod<T>(this object instance, string name, Type[] types, params object[] param)
        {
            if (instance == null)
                goto lbl_def;
            Type type = instance.GetType( );
            return type.CallTypePrivateMethod<T>(instance, name, types, param);
        lbl_def:
            return default(T);
        }
        private static Type[] GetParamTypes(object[] param)
        {
            int len = param == null ? 0 : param.Length;
            if (len == 0)
                return Type.EmptyTypes;
            Type[] types = new Type[len];
            for (int i = 0; i < param.Length; i++)
                types[i] = param[i] == null ? typeof(object) : param[i].GetType( );
            return types;
        }

        public static string ToString(this NameValueCollection instance, bool urlencoded)
        {
            int count = instance.Count;
            if (count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder( );
            for (int i = 0; i < count; i++)
            {
                string key = instance.GetKey(i);
                string str3;
                if (urlencoded)
                {
                    key = HttpUtility.UrlEncodeUnicode(key);
                }
                string str2 = !string.IsNullOrEmpty(key) ? (key + "=") : string.Empty;
                ArrayList list = instance.CallPrivateMethod<ArrayList>("BaseGet", i);
                int num3 = (list != null) ? list.Count : 0;
                if (builder.Length > 0)
                {
                    builder.Append('&');
                }
                if (num3 == 1)
                {
                    builder.Append(str2);
                    str3 = (string)list[0];
                    if (urlencoded)
                    {
                        str3 = HttpUtility.UrlEncodeUnicode(str3);
                    }
                    builder.Append(str3);
                }
                else if (num3 == 0)
                {
                    builder.Append(str2);
                }
                else
                {
                    for (int j = 0; j < num3; j++)
                    {
                        if (j > 0)
                        {
                            builder.Append('&');
                        }
                        builder.Append(str2);
                        str3 = (string)list[j];
                        if (urlencoded)
                        {
                            str3 = HttpUtility.UrlEncodeUnicode(str3);
                        }
                        builder.Append(str3);
                    }
                }
            }
            return builder.ToString( );
        }

        public static IEnumerable<IDataReader> GetDataReaderEnumerator(this StoredProcedure sp)
        {
            if (sp != null)
            {
                using (var dr = sp.GetReader( ))
                {
                    while (dr.Read( ))
                        yield return dr;
                }
            }
        }

        public static IEnumerable<IDataReader> GetDataReaderEnumerator(this SqlQuery query)
        {
            if (query != null)
            {
                using (var dr = query.ExecuteReader( ))
                {
                    while (dr.Read( ))
                        yield return dr;
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Func<T, bool> loopFn)
        {
            if (source != null && loopFn != null)
            {
                foreach (T t in source)
                {
                    if (!loopFn(t))
                        break;
                }
            }
        }

        public static Dictionary<string, object> ToDictionary<T>(this ReadOnlyRecord<T> item, bool isLowercase = false) where T : RecordBase<T>, new( )
        {
            Dictionary<string, object> result = new Dictionary<string, object>( );
            if (item != null)
            {
                var columns = item.GetColumnSettings( );
                if (columns != null)
                {
                    foreach (var col in columns)
                        result.Add(isLowercase ? col.ColumnName.ToLower( ) : col.ColumnName, col.CurrentValue);
                }
            }
            return result;
        }
    }
}

namespace System.Runtime.CompilerServices
{
    public class ExtensionAttribute : Attribute { }
}