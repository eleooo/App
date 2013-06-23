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
 //Generated on 2013/6/22 19:46:28 by Administrator
// <auto-generated />
namespace Eleooo.DAL
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the Sys_RoleAssignment table.
	/// </summary>
	[Serializable]
	public partial class SysRoleAssignmentSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysRoleAssignmentSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysRoleAssignmentSchema()
            :base("Sys_RoleAssignment")
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
			
			TableSchema.TableColumn colvarRoleID = new TableSchema.TableColumn(this);
			colvarRoleID.ColumnName = "RoleID";
			colvarRoleID.DataType = DbType.Int32;
			colvarRoleID.MaxLength = 0;
			colvarRoleID.AutoIncrement = false;
			colvarRoleID.IsNullable = false;
			colvarRoleID.IsPrimaryKey = false;
			colvarRoleID.IsForeignKey = false;
			colvarRoleID.IsReadOnly = false;
                
			colvarRoleID.DefaultSetting = @"";
			colvarRoleID.ForeignKeyTableName = "";
            colvarRoleID.ApplyExtendedProperties();
			this.Columns.Add(colvarRoleID);
			
			TableSchema.TableColumn colvarNavID = new TableSchema.TableColumn(this);
			colvarNavID.ColumnName = "NavID";
			colvarNavID.DataType = DbType.Int32;
			colvarNavID.MaxLength = 0;
			colvarNavID.AutoIncrement = false;
			colvarNavID.IsNullable = false;
			colvarNavID.IsPrimaryKey = false;
			colvarNavID.IsForeignKey = false;
			colvarNavID.IsReadOnly = false;
                
			colvarNavID.DefaultSetting = @"";
			colvarNavID.ForeignKeyTableName = "";
            colvarNavID.ApplyExtendedProperties();
			this.Columns.Add(colvarNavID);
			
			TableSchema.TableColumn colvarAllow = new TableSchema.TableColumn(this);
			colvarAllow.ColumnName = "Allow";
			colvarAllow.DataType = DbType.Boolean;
			colvarAllow.MaxLength = 0;
			colvarAllow.AutoIncrement = false;
			colvarAllow.IsNullable = false;
			colvarAllow.IsPrimaryKey = false;
			colvarAllow.IsForeignKey = false;
			colvarAllow.IsReadOnly = false;
                
			
			colvarAllow.DefaultSetting = @"((0))";
			colvarAllow.ForeignKeyTableName = "";
            colvarAllow.ApplyExtendedProperties();
			this.Columns.Add(colvarAllow);
			
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