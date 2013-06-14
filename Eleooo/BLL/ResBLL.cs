using System;
using System.Web;
using System.Configuration;
using Eleooo.DAL;
using System.Collections;
using System.Collections.Generic;
using SubSonic;

namespace Eleooo.Web
{
    public class ResBLL
    {
        private static readonly object _locker = new object( );
        private static Dictionary<string, string> _res;
        public static Dictionary<string, string> Res
        {
            get
            {
                if (_res == null)
                    LoadRes( );
                return _res;
            }
        }
        public static void LoadRes(string lan)
        {
            lock (_locker)
            {
                if (_res != null)
                    _res.Clear( );
                else
                    _res = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                List<SysResource> reses = DB.Select( ).From<SysResource>( )
                                          .Where(SysResource.LanColumn).IsEqualTo(lan)
                                          .ExecuteTypedList<SysResource>( );
                foreach (SysResource res in reses)
                {
                    _res.Add(res.Key, res.ValueX);
                }
            }
        }
        public static void LoadRes( )
        {
            LoadRes(DefLan);
        }
        public static void LoadRes(bool isFocusLoad)
        {
            if (_res == null || _res.Count == 0 || isFocusLoad)
                LoadRes( );
        }
        public static string GetRes(string key, string defValue, string describe)
        {
            SetRes(key, defValue, describe);
            return Res[key];
        }
        public static void SetRes(string key, string defValue, string describe)
        {
            lock (_locker)
            {
                if (!Res.ContainsKey(key) && !string.IsNullOrEmpty(key))
                {
                    SysResource res = new SysResource( );
                    res.Key = key;
                    res.ValueX = defValue;
                    res.Describe = describe;
                    res.Lan = DefLan;
                    res.Save( );
                    Res.Add(key, defValue);
                }
            }
        }
        public static string DefLan
        {
            get
            {
                string lan = ConfigurationManager.AppSettings["Lan"];
                if (string.IsNullOrEmpty(lan))
                    lan = "zh";
                return lan;
            }
        }
        public static string Get(string key)
        {
            if (Res.ContainsKey(key))
                return Res[key];
            else
                return string.Empty;
        }
        public static string GetColumnResKey(TableSchema.TableColumn column)
        {
            return string.Concat(column.Table.Name, ".", column.ColumnName);
        }
        public static string GetColumnRes(TableSchema.TableColumn column)
        {
            return ResBLL.GetRes(GetColumnResKey(column), column.ColumnName, GetColumnDesc(column));
        }
        public static string GetColumnDesc(TableSchema.TableColumn column)
        {
            return string.Concat("表", column.Table.Name, "的", column.ColumnName, "列标题");
        }

    }
}