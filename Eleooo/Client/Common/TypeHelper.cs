using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Eleooo.Client
{
    public class TypeHelper
    {
        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            try
            {
                PropertyInfo pi = type.GetProperty(propertyName);
                return pi;
            }
            catch
            {
                return null;
            }
        }
        public static object GetPropertyValue(object instance, string propertyName)
        {
            if (instance == null || string.IsNullOrEmpty(propertyName))
                return null;
            Type t = instance.GetType( );
            try
            {
                PropertyInfo pi = GetPropertyInfo(t, propertyName);
                if (pi != null)
                    return pi.GetValue(instance, null);
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
        public static T GetPropertyValue<T>(object instance, string propertyName)
        {
            object value = GetPropertyValue(instance, propertyName);
            if (value == null)
                return default(T);
            else
                return (T)value;
        }
        public static string GetTextValue(object instance)
        {
            return GetPropertyValue<string>(instance, "Text");
        }
        public static string GetNameValue(object instance)
        {
            return GetPropertyValue<string>(instance, "Name");
        }
        public static string GetTitleTextValue(object instance)
        {
            return GetPropertyValue<string>(instance, "TitleText");
        }
        public static string GetStringValue(object instance, string propertyName)
        {
            return GetPropertyValue<string>(instance, propertyName);
        }
    }
}
