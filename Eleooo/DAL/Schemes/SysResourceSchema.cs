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
	/// This is an ActiveRecord class which wraps the Sys_Resources table.
	/// </summary>
	[Serializable]
	public partial class SysResourceSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysResourceSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysResourceSchema()
            :base("Sys_Resources")
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
			
			TableSchema.TableColumn colvarKey = new TableSchema.TableColumn(this);
			colvarKey.ColumnName = "Key";
			colvarKey.DataType = DbType.String;
			colvarKey.MaxLength = 100;
			colvarKey.AutoIncrement = false;
			colvarKey.IsNullable = false;
			colvarKey.IsPrimaryKey = false;
			colvarKey.IsForeignKey = false;
			colvarKey.IsReadOnly = false;
                
			colvarKey.DefaultSetting = @"";
			colvarKey.ForeignKeyTableName = "";
            colvarKey.ApplyExtendedProperties();
			this.Columns.Add(colvarKey);
			
			TableSchema.TableColumn colvarValueX = new TableSchema.TableColumn(this);
			colvarValueX.ColumnName = "Value";
			colvarValueX.DataType = DbType.String;
			colvarValueX.MaxLength = 500;
			colvarValueX.AutoIncrement = false;
			colvarValueX.IsNullable = true;
			colvarValueX.IsPrimaryKey = false;
			colvarValueX.IsForeignKey = false;
			colvarValueX.IsReadOnly = false;
                
			colvarValueX.DefaultSetting = @"";
			colvarValueX.ForeignKeyTableName = "";
            colvarValueX.ApplyExtendedProperties();
			this.Columns.Add(colvarValueX);
			
			TableSchema.TableColumn colvarDescribe = new TableSchema.TableColumn(this);
			colvarDescribe.ColumnName = "Describe";
			colvarDescribe.DataType = DbType.String;
			colvarDescribe.MaxLength = 500;
			colvarDescribe.AutoIncrement = false;
			colvarDescribe.IsNullable = true;
			colvarDescribe.IsPrimaryKey = false;
			colvarDescribe.IsForeignKey = false;
			colvarDescribe.IsReadOnly = false;
                
			colvarDescribe.DefaultSetting = @"";
			colvarDescribe.ForeignKeyTableName = "";
            colvarDescribe.ApplyExtendedProperties();
			this.Columns.Add(colvarDescribe);
			
			TableSchema.TableColumn colvarLan = new TableSchema.TableColumn(this);
			colvarLan.ColumnName = "Lan";
			colvarLan.DataType = DbType.String;
			colvarLan.MaxLength = 8;
			colvarLan.AutoIncrement = false;
			colvarLan.IsNullable = false;
			colvarLan.IsPrimaryKey = false;
			colvarLan.IsForeignKey = false;
			colvarLan.IsReadOnly = false;
                
			
			colvarLan.DefaultSetting = @"(N'zh')";
			colvarLan.ForeignKeyTableName = "";
            colvarLan.ApplyExtendedProperties();
			this.Columns.Add(colvarLan);
			
			TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(this);
			colvarCreatedBy.ColumnName = "CreatedBy";
			colvarCreatedBy.DataType = DbType.Int32;
			colvarCreatedBy.MaxLength = 0;
			colvarCreatedBy.AutoIncrement = false;
			colvarCreatedBy.IsNullable = true;
			colvarCreatedBy.IsPrimaryKey = false;
			colvarCreatedBy.IsForeignKey = false;
			colvarCreatedBy.IsReadOnly = false;
                
			colvarCreatedBy.DefaultSetting = @"";
			colvarCreatedBy.ForeignKeyTableName = "";
            colvarCreatedBy.ApplyExtendedProperties();
			this.Columns.Add(colvarCreatedBy);
			
			TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(this);
			colvarCreatedOn.ColumnName = "CreatedOn";
			colvarCreatedOn.DataType = DbType.DateTime;
			colvarCreatedOn.MaxLength = 0;
			colvarCreatedOn.AutoIncrement = false;
			colvarCreatedOn.IsNullable = true;
			colvarCreatedOn.IsPrimaryKey = false;
			colvarCreatedOn.IsForeignKey = false;
			colvarCreatedOn.IsReadOnly = false;
                
			colvarCreatedOn.DefaultSetting = @"";
			colvarCreatedOn.ForeignKeyTableName = "";
            colvarCreatedOn.ApplyExtendedProperties();
			this.Columns.Add(colvarCreatedOn);
			
			TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(this);
			colvarModifiedBy.ColumnName = "ModifiedBy";
			colvarModifiedBy.DataType = DbType.Int32;
			colvarModifiedBy.MaxLength = 0;
			colvarModifiedBy.AutoIncrement = false;
			colvarModifiedBy.IsNullable = true;
			colvarModifiedBy.IsPrimaryKey = false;
			colvarModifiedBy.IsForeignKey = false;
			colvarModifiedBy.IsReadOnly = false;
                
			colvarModifiedBy.DefaultSetting = @"";
			colvarModifiedBy.ForeignKeyTableName = "";
            colvarModifiedBy.ApplyExtendedProperties();
			this.Columns.Add(colvarModifiedBy);
			
			TableSchema.TableColumn colvarModifiedOn = new TableSchema.TableColumn(this);
			colvarModifiedOn.ColumnName = "ModifiedOn";
			colvarModifiedOn.DataType = DbType.DateTime;
			colvarModifiedOn.MaxLength = 0;
			colvarModifiedOn.AutoIncrement = false;
			colvarModifiedOn.IsNullable = true;
			colvarModifiedOn.IsPrimaryKey = false;
			colvarModifiedOn.IsForeignKey = false;
			colvarModifiedOn.IsReadOnly = false;
                
			colvarModifiedOn.DefaultSetting = @"";
			colvarModifiedOn.ForeignKeyTableName = "";
            colvarModifiedOn.ApplyExtendedProperties();
			this.Columns.Add(colvarModifiedOn);
			
		}
		#endregion
	}
}