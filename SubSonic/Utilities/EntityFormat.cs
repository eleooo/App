using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SubSonic.Utilities
{
    public class EntityFormat
    {
        public static T TableToEntity<T>(DataTable dtData)
            where T : SubSonic.ActiveRecord<T>, new( )
        {
            if (dtData != null && dtData.Rows.Count > 0)
            {
                T t = new T( );
                t.Load(dtData);
                return t;
            }
            else
                return default(T);

        }
        public static DataTable EntityToTable<T>(T t) where T : ActiveRecord<T>, new( )
        {
            if (t == null)
                return null;
            DataTable dt = new DataTable(t.TableName);
            SubSonic.TableSchema.TableColumnCollection columns = t.DirtyColumns.Count == 0 ? t.GetSchema( ).Columns : t.DirtyColumns;
            foreach (TableSchema.TableColumn col in columns)
            {

                dt.Columns.Add(col.ColumnName, col.GetPropertyType( ));
            }
            DataRow row = dt.NewRow( );

            foreach (TableSchema.TableColumnSetting setting in t.GetColumnSettings( ))
            {
                row[setting.ColumnName] = setting.CurrentValue == null ? DBNull.Value : setting.CurrentValue;
            }
            dt.Rows.Add(row);
            return dt;
        }
        public static QueryCommand SqlToCmd(string sql, DataTable dtParam)
        {
            QueryCommand cmd = new QueryCommand(sql, DataService.Provider.Name);
            if (dtParam != null && dtParam.Rows.Count > 0)
            {
                foreach (DataRow row in dtParam.Rows)
                {
                    string paramName = Convert.ToString(row[0]);
                    object paramValue = row[1];
                    DbType type = (DbType)(Convert.ToInt32(row[2]));
                    cmd.Parameters.Add(paramName, paramValue, type, (ParameterDirection)row[3]);
                }
            }
            return cmd;
        }
        public static string CmdToSql(QueryCommand cmd, out DataTable dtParam)
        {
            dtParam = GetParamTable( );
            if (cmd == null)
                return string.Empty;
            if (cmd.Parameters.Count > 0)
            {
                foreach (var p in cmd.Parameters)
                {
                    DataRow row = dtParam.NewRow( );
                    row[0] = p.ParameterName;
                    row[1] = p.ParameterValue;
                    row[2] = Convert.ToInt32(p.DataType);
                    row[3] = p.Mode;
                    dtParam.Rows.Add(row);
                }
            }
            return cmd.CommandSql;
        }
        public static DataTable GetParamTable( )
        {
            DataTable dt = new DataTable("dtParam");
            dt.Columns.Add("paramName", typeof(string));
            dt.Columns.Add("paramValue", typeof(object));
            dt.Columns.Add("paramDbType", typeof(int));
            dt.Columns.Add("IsOutputParam", typeof(ParameterDirection));
            return dt;
        }
        public static void FillCmdToTable(QueryCommand cmd, DataTable dtParam)
        {
            if (cmd == null || dtParam == null)
                return;
            foreach (DataRow row in dtParam.Rows)
            {
                string paramName = Convert.ToString(row[0]);
                QueryParameter p = cmd.Parameters.GetParameter(paramName);
                if (p != null)
                    row[1] = p.ParameterValue;
            }
        }
        public static void FillTableToCmd(QueryCommand cmd, DataTable dtParam)
        {
            if (cmd == null || dtParam == null)
                return;
            foreach (DataRow row in dtParam.Rows)
            {
                string paramName = Convert.ToString(row[0]);
                QueryParameter p = cmd.Parameters.GetParameter(paramName);
                if (p != null)
                    p.ParameterValue = row[1];
            }
        }
        public static DataTable GetUserInfoTable( )
        {
            DataTable dtUserInfo = new DataTable("dtUserInfo");
            dtUserInfo.Columns.Add("Key");
            dtUserInfo.Columns.Add("Value");
            return dtUserInfo;
        }
    }
}
