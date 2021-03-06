using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
 //Generated on 2013/7/19 0:44:48 by Administrator
// <auto-generated />
namespace Eleooo.DAL
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the Sys_Error_Log table.
	/// </summary>
	[Serializable]
	public partial class SysErrorLogSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysErrorLogSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysErrorLogSchema()
            :base("Sys_Error_Log")
		{
		}
		
		#endregion
		
		#region Schema and Query Accessor	
		
		protected override void Initital() 
        {
			//Schema define
			this.Columns = new TableSchema.TableColumnCollection();
			this.SchemaName = @"dbo";
            this.TableType = TableType.Table;
			//columns
			
			TableSchema.TableColumn colvarId = new TableSchema.TableColumn(this);
			colvarId.ColumnName = "ID";
			colvarId.DataType = DbType.Int32;
			colvarId.MaxLength = 0;
			colvarId.AutoIncrement = true;
			colvarId.IsNullable = false;
			colvarId.IsPrimaryKey = true;
			colvarId.IsForeignKey = false;
			colvarId.IsReadOnly = false;
                
			colvarId.DefaultSetting = @"";
			colvarId.ForeignKeyTableName = "";
            colvarId.ApplyExtendedProperties();
			this.Columns.Add(colvarId);
			
			TableSchema.TableColumn colvarLogDate = new TableSchema.TableColumn(this);
			colvarLogDate.ColumnName = "LogDate";
			colvarLogDate.DataType = DbType.DateTime;
			colvarLogDate.MaxLength = 0;
			colvarLogDate.AutoIncrement = false;
			colvarLogDate.IsNullable = true;
			colvarLogDate.IsPrimaryKey = false;
			colvarLogDate.IsForeignKey = false;
			colvarLogDate.IsReadOnly = false;
                
			colvarLogDate.DefaultSetting = @"";
			colvarLogDate.ForeignKeyTableName = "";
            colvarLogDate.ApplyExtendedProperties();
			this.Columns.Add(colvarLogDate);
			
			TableSchema.TableColumn colvarLogSource = new TableSchema.TableColumn(this);
			colvarLogSource.ColumnName = "LogSource";
			colvarLogSource.DataType = DbType.String;
			colvarLogSource.MaxLength = 250;
			colvarLogSource.AutoIncrement = false;
			colvarLogSource.IsNullable = true;
			colvarLogSource.IsPrimaryKey = false;
			colvarLogSource.IsForeignKey = false;
			colvarLogSource.IsReadOnly = false;
                
			colvarLogSource.DefaultSetting = @"";
			colvarLogSource.ForeignKeyTableName = "";
            colvarLogSource.ApplyExtendedProperties();
			this.Columns.Add(colvarLogSource);
			
			TableSchema.TableColumn colvarLogMessage = new TableSchema.TableColumn(this);
			colvarLogMessage.ColumnName = "LogMessage";
			colvarLogMessage.DataType = DbType.String;
			colvarLogMessage.MaxLength = 2000;
			colvarLogMessage.AutoIncrement = false;
			colvarLogMessage.IsNullable = true;
			colvarLogMessage.IsPrimaryKey = false;
			colvarLogMessage.IsForeignKey = false;
			colvarLogMessage.IsReadOnly = false;
                
			colvarLogMessage.DefaultSetting = @"";
			colvarLogMessage.ForeignKeyTableName = "";
            colvarLogMessage.ApplyExtendedProperties();
			this.Columns.Add(colvarLogMessage);
			
			TableSchema.TableColumn colvarLogStackTrace = new TableSchema.TableColumn(this);
			colvarLogStackTrace.ColumnName = "LogStackTrace";
			colvarLogStackTrace.DataType = DbType.String;
			colvarLogStackTrace.MaxLength = 1073741823;
			colvarLogStackTrace.AutoIncrement = false;
			colvarLogStackTrace.IsNullable = true;
			colvarLogStackTrace.IsPrimaryKey = false;
			colvarLogStackTrace.IsForeignKey = false;
			colvarLogStackTrace.IsReadOnly = false;
                
			colvarLogStackTrace.DefaultSetting = @"";
			colvarLogStackTrace.ForeignKeyTableName = "";
            colvarLogStackTrace.ApplyExtendedProperties();
			this.Columns.Add(colvarLogStackTrace);
			
			TableSchema.TableColumn colvarSubSys = new TableSchema.TableColumn(this);
			colvarSubSys.ColumnName = "SubSys";
			colvarSubSys.DataType = DbType.Int32;
			colvarSubSys.MaxLength = 0;
			colvarSubSys.AutoIncrement = false;
			colvarSubSys.IsNullable = true;
			colvarSubSys.IsPrimaryKey = false;
			colvarSubSys.IsForeignKey = false;
			colvarSubSys.IsReadOnly = false;
                
			colvarSubSys.DefaultSetting = @"";
			colvarSubSys.ForeignKeyTableName = "";
            colvarSubSys.ApplyExtendedProperties();
			this.Columns.Add(colvarSubSys);
			
			TableSchema.TableColumn colvarLogUser = new TableSchema.TableColumn(this);
			colvarLogUser.ColumnName = "LogUser";
			colvarLogUser.DataType = DbType.Int32;
			colvarLogUser.MaxLength = 0;
			colvarLogUser.AutoIncrement = false;
			colvarLogUser.IsNullable = true;
			colvarLogUser.IsPrimaryKey = false;
			colvarLogUser.IsForeignKey = false;
			colvarLogUser.IsReadOnly = false;
                
			colvarLogUser.DefaultSetting = @"";
			colvarLogUser.ForeignKeyTableName = "";
            colvarLogUser.ApplyExtendedProperties();
			this.Columns.Add(colvarLogUser);
			
		}
		#endregion
	}
}