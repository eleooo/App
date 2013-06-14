using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SubSonic
{
    public class RemoteServicesProvider : DataProvider
    {
        public override string NamedProviderType
        {
            get { return DataProviderTypeName.REMOTE_SERVICES; }
        }

        protected override string GetDatabaseVersion(string providerName)
        {
            throw new NotImplementedException("GetDatabaseVersion");
        }

        public override System.Data.IDataReader GetReader(QueryCommand cmd)
        {
            SetCmdToServerProvider(ref cmd);
            DataTable dt = this.GetDataTable(cmd);
            if (dt != null)
                return dt.CreateDataReader();
            else
                return null;
        }

        public override System.Data.IDataReader GetSingleRecordReader(QueryCommand cmd)
        {
            SetCmdToServerProvider(ref cmd);
            DataTable dt = this.GetDataTable(cmd);
            if (dt == null)
                return null;
            DataRow row = dt.Rows[0];
            dt.Rows.Clear();
            dt.Rows.Add(row);
            return dt.CreateDataReader();

        }

        public override T GetDataSet<T>(QueryCommand qry)
        {
            SetCmdToServerProvider(ref qry);
            return null;
        }

        public override object ExecuteScalar(QueryCommand cmd)
        {
            SetCmdToServerProvider(ref cmd);
            return null;
        }

        public override int ExecuteQuery(QueryCommand cmd)
        {
            this.SetCmdToServerProvider(ref cmd);
            return 0;
        }

        public override TableSchema.Table GetTableSchema(string tableName, TableType tableType)
        {
            throw new NotImplementedException();
        }

        public override string[] GetSPList()
        {
            throw new NotImplementedException();
        }

        public override string[] GetTableNameList()
        {
            throw new NotImplementedException();
        }

        public override string[] GetViewNameList()
        {
            throw new NotImplementedException();
        }

        public override System.Data.IDataReader GetSPParams(string spName)
        {
            throw new NotImplementedException();
            //if (dtParamSql == null)
            //{
            //    QueryCommand cmdSP = new QueryCommand(SP_PARAM_SQL_ALL, Name);
            //    dtParamSql = new DataTable();
            //    dtParamSql.Load(GetReader(cmdSP));
            //}

            //DataView dv = new DataView(dtParamSql)
            //{
            //    RowFilter = String.Format("SPName = '{0}'", spName),
            //    Sort = "OrdinalPosition"
            //};
            //DataTable dtNew = dv.ToTable();
            //return dtNew.CreateDataReader();
        }

        public override string[] GetForeignKeyTables(string tableName)
        {
            throw new NotImplementedException();
        }

        public override string GetTableNameByPrimaryKey(string pkName, string providerName)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList GetPrimaryKeyTableNames(string tableName)
        {
            throw new NotImplementedException();
        }

        public override TableSchema.Table[] GetPrimaryKeyTables(string tableName)
        {
            throw new NotImplementedException();
        }

        public override string GetForeignKeyTableName(string fkColumnName)
        {
            throw new NotImplementedException();
        }

        public override string GetForeignKeyTableName(string fkColumnName, string tableName)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DbType GetDbType(string dataType)
        {
            throw new NotImplementedException();
        }

        public override System.Data.IDbCommand GetCommand(QueryCommand qry)
        {
            throw new NotSupportedException("GetCommand method");
        }

        public override System.Data.Common.DbConnection CreateConnection()
        {
            throw new NotSupportedException("CreateConnection method");
        }

        public override System.Data.Common.DbConnection CreateConnection(string connectionString)
        {
            throw new NotSupportedException("CreateConnection method");
        }

        public override void SetParameter(System.Data.IDataReader dataReader, StoredProcedure.Parameter parameter)
        {
            throw new NotImplementedException();
        }

        public override string GetParameterPrefix()
        {
            throw new NotImplementedException();
        }

        public override void ExecuteTransaction(QueryCommandCollection commands)
        {
            throw new NotImplementedException();
        }

        public override string GetInsertSql(Query qry)
        {
            throw new NotImplementedException();
        }

        public override string GetSelectSql(Query qry)
        {
            throw new NotImplementedException();
        }

        public override string ScriptData(string tableName, string providerName)
        {
            throw new NotImplementedException();
        }

        public override void ReloadSchema()
        {
            throw new NotImplementedException();
        }

        protected virtual void SetCmdToServerProvider(ref QueryCommand cmd)
        {
            if (!string.IsNullOrEmpty(RemoteServerProvider))
                cmd.ProviderName = RemoteServerProvider;
        }

        public override List<object> GetDataBaseAllSchema()
        {
            throw new NotImplementedException();
        }
    }
}
