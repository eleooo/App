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
	/// This is an ActiveRecord class which wraps the Sys_RoleDefine table.
	/// </summary>
	[Serializable]
	public partial class SysRoleDefineSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysRoleDefineSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysRoleDefineSchema()
            :base("Sys_RoleDefine")
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
                
			
            colvarId.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","角色ID"));
            colvarId.DefaultSetting = @"";
			colvarId.ForeignKeyTableName = "";
            colvarId.ApplyExtendedProperties();
			this.Columns.Add(colvarId);
			
			TableSchema.TableColumn colvarRoleName = new TableSchema.TableColumn(this);
			colvarRoleName.ColumnName = "RoleName";
			colvarRoleName.DataType = DbType.String;
			colvarRoleName.MaxLength = 50;
			colvarRoleName.AutoIncrement = false;
			colvarRoleName.IsNullable = false;
			colvarRoleName.IsPrimaryKey = false;
			colvarRoleName.IsForeignKey = false;
			colvarRoleName.IsReadOnly = false;
                
			
            colvarRoleName.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","角色名称"));
            colvarRoleName.DefaultSetting = @"";
			colvarRoleName.ForeignKeyTableName = "";
            colvarRoleName.ApplyExtendedProperties();
			this.Columns.Add(colvarRoleName);
			
			TableSchema.TableColumn colvarSubSysId = new TableSchema.TableColumn(this);
			colvarSubSysId.ColumnName = "SubSys_ID";
			colvarSubSysId.DataType = DbType.Int32;
			colvarSubSysId.MaxLength = 0;
			colvarSubSysId.AutoIncrement = false;
			colvarSubSysId.IsNullable = false;
			colvarSubSysId.IsPrimaryKey = false;
			colvarSubSysId.IsForeignKey = false;
			colvarSubSysId.IsReadOnly = false;
                
			
            colvarSubSysId.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","系统ID"));
            colvarSubSysId.DefaultSetting = @"";
			colvarSubSysId.ForeignKeyTableName = "";
            colvarSubSysId.ApplyExtendedProperties();
			this.Columns.Add(colvarSubSysId);
			
			TableSchema.TableColumn colvarIsDefault = new TableSchema.TableColumn(this);
			colvarIsDefault.ColumnName = "IsDefault";
			colvarIsDefault.DataType = DbType.Boolean;
			colvarIsDefault.MaxLength = 0;
			colvarIsDefault.AutoIncrement = false;
			colvarIsDefault.IsNullable = true;
			colvarIsDefault.IsPrimaryKey = false;
			colvarIsDefault.IsForeignKey = false;
			colvarIsDefault.IsReadOnly = false;
                
			
			colvarIsDefault.DefaultSetting = @"((0))";
			colvarIsDefault.ForeignKeyTableName = "";
            colvarIsDefault.ApplyExtendedProperties();
			this.Columns.Add(colvarIsDefault);
			
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