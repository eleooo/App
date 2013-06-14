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
 //Generated on 2013/6/11 23:04:46 by Administrator
// <auto-generated />
namespace Eleooo.DAL
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the Sys_Article table.
	/// </summary>
	[Serializable]
	public partial class SysArticleSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysArticleSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysArticleSchema()
            :base("Sys_Article")
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
			
			TableSchema.TableColumn colvarTitle = new TableSchema.TableColumn(this);
			colvarTitle.ColumnName = "Title";
			colvarTitle.DataType = DbType.String;
			colvarTitle.MaxLength = 250;
			colvarTitle.AutoIncrement = false;
			colvarTitle.IsNullable = true;
			colvarTitle.IsPrimaryKey = false;
			colvarTitle.IsForeignKey = false;
			colvarTitle.IsReadOnly = false;
                
			colvarTitle.DefaultSetting = @"";
			colvarTitle.ForeignKeyTableName = "";
            colvarTitle.ApplyExtendedProperties();
			this.Columns.Add(colvarTitle);
			
			TableSchema.TableColumn colvarContent = new TableSchema.TableColumn(this);
			colvarContent.ColumnName = "Content";
			colvarContent.DataType = DbType.String;
			colvarContent.MaxLength = 1073741823;
			colvarContent.AutoIncrement = false;
			colvarContent.IsNullable = true;
			colvarContent.IsPrimaryKey = false;
			colvarContent.IsForeignKey = false;
			colvarContent.IsReadOnly = false;
                
			colvarContent.DefaultSetting = @"";
			colvarContent.ForeignKeyTableName = "";
            colvarContent.ApplyExtendedProperties();
			this.Columns.Add(colvarContent);
			
			TableSchema.TableColumn colvarKeyWord = new TableSchema.TableColumn(this);
			colvarKeyWord.ColumnName = "KeyWord";
			colvarKeyWord.DataType = DbType.String;
			colvarKeyWord.MaxLength = 250;
			colvarKeyWord.AutoIncrement = false;
			colvarKeyWord.IsNullable = true;
			colvarKeyWord.IsPrimaryKey = false;
			colvarKeyWord.IsForeignKey = false;
			colvarKeyWord.IsReadOnly = false;
                
			colvarKeyWord.DefaultSetting = @"";
			colvarKeyWord.ForeignKeyTableName = "";
            colvarKeyWord.ApplyExtendedProperties();
			this.Columns.Add(colvarKeyWord);
			
			TableSchema.TableColumn colvarHits = new TableSchema.TableColumn(this);
			colvarHits.ColumnName = "Hits";
			colvarHits.DataType = DbType.Int32;
			colvarHits.MaxLength = 0;
			colvarHits.AutoIncrement = false;
			colvarHits.IsNullable = true;
			colvarHits.IsPrimaryKey = false;
			colvarHits.IsForeignKey = false;
			colvarHits.IsReadOnly = false;
                
			colvarHits.DefaultSetting = @"";
			colvarHits.ForeignKeyTableName = "";
            colvarHits.ApplyExtendedProperties();
			this.Columns.Add(colvarHits);
			
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
			
			TableSchema.TableColumn colvarAuditedBy = new TableSchema.TableColumn(this);
			colvarAuditedBy.ColumnName = "AuditedBy";
			colvarAuditedBy.DataType = DbType.Int32;
			colvarAuditedBy.MaxLength = 0;
			colvarAuditedBy.AutoIncrement = false;
			colvarAuditedBy.IsNullable = true;
			colvarAuditedBy.IsPrimaryKey = false;
			colvarAuditedBy.IsForeignKey = false;
			colvarAuditedBy.IsReadOnly = false;
                
			colvarAuditedBy.DefaultSetting = @"";
			colvarAuditedBy.ForeignKeyTableName = "";
            colvarAuditedBy.ApplyExtendedProperties();
			this.Columns.Add(colvarAuditedBy);
			
			TableSchema.TableColumn colvarAuditedOn = new TableSchema.TableColumn(this);
			colvarAuditedOn.ColumnName = "AuditedOn";
			colvarAuditedOn.DataType = DbType.DateTime;
			colvarAuditedOn.MaxLength = 0;
			colvarAuditedOn.AutoIncrement = false;
			colvarAuditedOn.IsNullable = true;
			colvarAuditedOn.IsPrimaryKey = false;
			colvarAuditedOn.IsForeignKey = false;
			colvarAuditedOn.IsReadOnly = false;
                
			colvarAuditedOn.DefaultSetting = @"";
			colvarAuditedOn.ForeignKeyTableName = "";
            colvarAuditedOn.ApplyExtendedProperties();
			this.Columns.Add(colvarAuditedOn);
			
			TableSchema.TableColumn colvarUId = new TableSchema.TableColumn(this);
			colvarUId.ColumnName = "U_ID";
			colvarUId.DataType = DbType.Int32;
			colvarUId.MaxLength = 0;
			colvarUId.AutoIncrement = false;
			colvarUId.IsNullable = true;
			colvarUId.IsPrimaryKey = false;
			colvarUId.IsForeignKey = false;
			colvarUId.IsReadOnly = false;
                
			colvarUId.DefaultSetting = @"";
			colvarUId.ForeignKeyTableName = "";
            colvarUId.ApplyExtendedProperties();
			this.Columns.Add(colvarUId);
			
			TableSchema.TableColumn colvarCId = new TableSchema.TableColumn(this);
			colvarCId.ColumnName = "C_ID";
			colvarCId.DataType = DbType.Int32;
			colvarCId.MaxLength = 0;
			colvarCId.AutoIncrement = false;
			colvarCId.IsNullable = true;
			colvarCId.IsPrimaryKey = false;
			colvarCId.IsForeignKey = false;
			colvarCId.IsReadOnly = false;
                
			colvarCId.DefaultSetting = @"";
			colvarCId.ForeignKeyTableName = "";
            colvarCId.ApplyExtendedProperties();
			this.Columns.Add(colvarCId);
			
			TableSchema.TableColumn colvarPic = new TableSchema.TableColumn(this);
			colvarPic.ColumnName = "Pic";
			colvarPic.DataType = DbType.String;
			colvarPic.MaxLength = 150;
			colvarPic.AutoIncrement = false;
			colvarPic.IsNullable = true;
			colvarPic.IsPrimaryKey = false;
			colvarPic.IsForeignKey = false;
			colvarPic.IsReadOnly = false;
                
			colvarPic.DefaultSetting = @"";
			colvarPic.ForeignKeyTableName = "";
            colvarPic.ApplyExtendedProperties();
			this.Columns.Add(colvarPic);
			
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