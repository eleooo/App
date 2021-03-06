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
	/// This is an ActiveRecord class which wraps the Sys_Doc table.
	/// </summary>
	[Serializable]
	public partial class SysDocSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysDocSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysDocSchema()
            :base("Sys_Doc")
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
			
			TableSchema.TableColumn colvarDocSubject = new TableSchema.TableColumn(this);
			colvarDocSubject.ColumnName = "DocSubject";
			colvarDocSubject.DataType = DbType.String;
			colvarDocSubject.MaxLength = 250;
			colvarDocSubject.AutoIncrement = false;
			colvarDocSubject.IsNullable = false;
			colvarDocSubject.IsPrimaryKey = false;
			colvarDocSubject.IsForeignKey = false;
			colvarDocSubject.IsReadOnly = false;
                
			colvarDocSubject.DefaultSetting = @"";
			colvarDocSubject.ForeignKeyTableName = "";
            colvarDocSubject.ApplyExtendedProperties();
			this.Columns.Add(colvarDocSubject);
			
			TableSchema.TableColumn colvarDocPath = new TableSchema.TableColumn(this);
			colvarDocPath.ColumnName = "DocPath";
			colvarDocPath.DataType = DbType.String;
			colvarDocPath.MaxLength = 50;
			colvarDocPath.AutoIncrement = false;
			colvarDocPath.IsNullable = false;
			colvarDocPath.IsPrimaryKey = false;
			colvarDocPath.IsForeignKey = false;
			colvarDocPath.IsReadOnly = false;
                
			colvarDocPath.DefaultSetting = @"";
			colvarDocPath.ForeignKeyTableName = "";
            colvarDocPath.ApplyExtendedProperties();
			this.Columns.Add(colvarDocPath);
			
			TableSchema.TableColumn colvarDocDate = new TableSchema.TableColumn(this);
			colvarDocDate.ColumnName = "DocDate";
			colvarDocDate.DataType = DbType.DateTime;
			colvarDocDate.MaxLength = 0;
			colvarDocDate.AutoIncrement = false;
			colvarDocDate.IsNullable = false;
			colvarDocDate.IsPrimaryKey = false;
			colvarDocDate.IsForeignKey = false;
			colvarDocDate.IsReadOnly = false;
                
			colvarDocDate.DefaultSetting = @"";
			colvarDocDate.ForeignKeyTableName = "";
            colvarDocDate.ApplyExtendedProperties();
			this.Columns.Add(colvarDocDate);
			
			TableSchema.TableColumn colvarDocStatus = new TableSchema.TableColumn(this);
			colvarDocStatus.ColumnName = "DocStatus";
			colvarDocStatus.DataType = DbType.Int32;
			colvarDocStatus.MaxLength = 0;
			colvarDocStatus.AutoIncrement = false;
			colvarDocStatus.IsNullable = false;
			colvarDocStatus.IsPrimaryKey = false;
			colvarDocStatus.IsForeignKey = false;
			colvarDocStatus.IsReadOnly = false;
                
			colvarDocStatus.DefaultSetting = @"";
			colvarDocStatus.ForeignKeyTableName = "";
            colvarDocStatus.ApplyExtendedProperties();
			this.Columns.Add(colvarDocStatus);
			
			TableSchema.TableColumn colvarDocHots = new TableSchema.TableColumn(this);
			colvarDocHots.ColumnName = "DocHots";
			colvarDocHots.DataType = DbType.Int32;
			colvarDocHots.MaxLength = 0;
			colvarDocHots.AutoIncrement = false;
			colvarDocHots.IsNullable = false;
			colvarDocHots.IsPrimaryKey = false;
			colvarDocHots.IsForeignKey = false;
			colvarDocHots.IsReadOnly = false;
                
			colvarDocHots.DefaultSetting = @"";
			colvarDocHots.ForeignKeyTableName = "";
            colvarDocHots.ApplyExtendedProperties();
			this.Columns.Add(colvarDocHots);
			
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