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
 //Generated on 2013/6/18 20:31:45 by Administrator
// <auto-generated />
namespace Eleooo.DAL
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the Sys_SupportType table.
	/// </summary>
	[Serializable]
	public partial class SysSupportTypeSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysSupportTypeSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysSupportTypeSchema()
            :base("Sys_SupportType")
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
			
			TableSchema.TableColumn colvarSupportTypeId = new TableSchema.TableColumn(this);
			colvarSupportTypeId.ColumnName = "SupportType_ID";
			colvarSupportTypeId.DataType = DbType.Int32;
			colvarSupportTypeId.MaxLength = 0;
			colvarSupportTypeId.AutoIncrement = true;
			colvarSupportTypeId.IsNullable = false;
			colvarSupportTypeId.IsPrimaryKey = true;
			colvarSupportTypeId.IsForeignKey = false;
			colvarSupportTypeId.IsReadOnly = false;
                
			colvarSupportTypeId.DefaultSetting = @"";
			colvarSupportTypeId.ForeignKeyTableName = "";
            colvarSupportTypeId.ApplyExtendedProperties();
			this.Columns.Add(colvarSupportTypeId);
			
			TableSchema.TableColumn colvarSupportTypeName = new TableSchema.TableColumn(this);
			colvarSupportTypeName.ColumnName = "SupportType_Name";
			colvarSupportTypeName.DataType = DbType.String;
			colvarSupportTypeName.MaxLength = 50;
			colvarSupportTypeName.AutoIncrement = false;
			colvarSupportTypeName.IsNullable = false;
			colvarSupportTypeName.IsPrimaryKey = false;
			colvarSupportTypeName.IsForeignKey = false;
			colvarSupportTypeName.IsReadOnly = false;
                
			colvarSupportTypeName.DefaultSetting = @"";
			colvarSupportTypeName.ForeignKeyTableName = "";
            colvarSupportTypeName.ApplyExtendedProperties();
			this.Columns.Add(colvarSupportTypeName);
			
			TableSchema.TableColumn colvarSupportTypeDesc = new TableSchema.TableColumn(this);
			colvarSupportTypeDesc.ColumnName = "SupportType_Desc";
			colvarSupportTypeDesc.DataType = DbType.String;
			colvarSupportTypeDesc.MaxLength = 255;
			colvarSupportTypeDesc.AutoIncrement = false;
			colvarSupportTypeDesc.IsNullable = false;
			colvarSupportTypeDesc.IsPrimaryKey = false;
			colvarSupportTypeDesc.IsForeignKey = false;
			colvarSupportTypeDesc.IsReadOnly = false;
                
			colvarSupportTypeDesc.DefaultSetting = @"";
			colvarSupportTypeDesc.ForeignKeyTableName = "";
            colvarSupportTypeDesc.ApplyExtendedProperties();
			this.Columns.Add(colvarSupportTypeDesc);
			
			TableSchema.TableColumn colvarSupportTypePhoto = new TableSchema.TableColumn(this);
			colvarSupportTypePhoto.ColumnName = "SupportType_Photo";
			colvarSupportTypePhoto.DataType = DbType.String;
			colvarSupportTypePhoto.MaxLength = 50;
			colvarSupportTypePhoto.AutoIncrement = false;
			colvarSupportTypePhoto.IsNullable = false;
			colvarSupportTypePhoto.IsPrimaryKey = false;
			colvarSupportTypePhoto.IsForeignKey = false;
			colvarSupportTypePhoto.IsReadOnly = false;
                
			colvarSupportTypePhoto.DefaultSetting = @"";
			colvarSupportTypePhoto.ForeignKeyTableName = "";
            colvarSupportTypePhoto.ApplyExtendedProperties();
			this.Columns.Add(colvarSupportTypePhoto);
			
			TableSchema.TableColumn colvarSupportTypePid = new TableSchema.TableColumn(this);
			colvarSupportTypePid.ColumnName = "SupportType_PID";
			colvarSupportTypePid.DataType = DbType.Int32;
			colvarSupportTypePid.MaxLength = 0;
			colvarSupportTypePid.AutoIncrement = false;
			colvarSupportTypePid.IsNullable = false;
			colvarSupportTypePid.IsPrimaryKey = false;
			colvarSupportTypePid.IsForeignKey = false;
			colvarSupportTypePid.IsReadOnly = false;
                
			colvarSupportTypePid.DefaultSetting = @"";
			colvarSupportTypePid.ForeignKeyTableName = "";
            colvarSupportTypePid.ApplyExtendedProperties();
			this.Columns.Add(colvarSupportTypePid);
			
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