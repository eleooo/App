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
 //Generated on 2013/6/25 0:29:51 by Administrator
// <auto-generated />
namespace Eleooo.DAL
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the Sys_Member_CompanyR table.
	/// </summary>
	[Serializable]
	public partial class SysMemberCompanyRSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysMemberCompanyRSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysMemberCompanyRSchema()
            :base("Sys_Member_CompanyR")
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
			
			TableSchema.TableColumn colvarCompanyDate = new TableSchema.TableColumn(this);
			colvarCompanyDate.ColumnName = "CompanyDate";
			colvarCompanyDate.DataType = DbType.DateTime;
			colvarCompanyDate.MaxLength = 0;
			colvarCompanyDate.AutoIncrement = false;
			colvarCompanyDate.IsNullable = true;
			colvarCompanyDate.IsPrimaryKey = false;
			colvarCompanyDate.IsForeignKey = false;
			colvarCompanyDate.IsReadOnly = false;
                
			colvarCompanyDate.DefaultSetting = @"";
			colvarCompanyDate.ForeignKeyTableName = "";
            colvarCompanyDate.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyDate);
			
			TableSchema.TableColumn colvarCompanyName = new TableSchema.TableColumn(this);
			colvarCompanyName.ColumnName = "CompanyName";
			colvarCompanyName.DataType = DbType.String;
			colvarCompanyName.MaxLength = 50;
			colvarCompanyName.AutoIncrement = false;
			colvarCompanyName.IsNullable = true;
			colvarCompanyName.IsPrimaryKey = false;
			colvarCompanyName.IsForeignKey = false;
			colvarCompanyName.IsReadOnly = false;
                
			colvarCompanyName.DefaultSetting = @"";
			colvarCompanyName.ForeignKeyTableName = "";
            colvarCompanyName.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyName);
			
			TableSchema.TableColumn colvarCompanyAddress = new TableSchema.TableColumn(this);
			colvarCompanyAddress.ColumnName = "CompanyAddress";
			colvarCompanyAddress.DataType = DbType.String;
			colvarCompanyAddress.MaxLength = 150;
			colvarCompanyAddress.AutoIncrement = false;
			colvarCompanyAddress.IsNullable = true;
			colvarCompanyAddress.IsPrimaryKey = false;
			colvarCompanyAddress.IsForeignKey = false;
			colvarCompanyAddress.IsReadOnly = false;
                
			colvarCompanyAddress.DefaultSetting = @"";
			colvarCompanyAddress.ForeignKeyTableName = "";
            colvarCompanyAddress.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyAddress);
			
			TableSchema.TableColumn colvarCompanyDesc = new TableSchema.TableColumn(this);
			colvarCompanyDesc.ColumnName = "CompanyDesc";
			colvarCompanyDesc.DataType = DbType.String;
			colvarCompanyDesc.MaxLength = 300;
			colvarCompanyDesc.AutoIncrement = false;
			colvarCompanyDesc.IsNullable = true;
			colvarCompanyDesc.IsPrimaryKey = false;
			colvarCompanyDesc.IsForeignKey = false;
			colvarCompanyDesc.IsReadOnly = false;
                
			colvarCompanyDesc.DefaultSetting = @"";
			colvarCompanyDesc.ForeignKeyTableName = "";
            colvarCompanyDesc.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyDesc);
			
			TableSchema.TableColumn colvarCompanyTel = new TableSchema.TableColumn(this);
			colvarCompanyTel.ColumnName = "CompanyTel";
			colvarCompanyTel.DataType = DbType.String;
			colvarCompanyTel.MaxLength = 50;
			colvarCompanyTel.AutoIncrement = false;
			colvarCompanyTel.IsNullable = true;
			colvarCompanyTel.IsPrimaryKey = false;
			colvarCompanyTel.IsForeignKey = false;
			colvarCompanyTel.IsReadOnly = false;
                
			colvarCompanyTel.DefaultSetting = @"";
			colvarCompanyTel.ForeignKeyTableName = "";
            colvarCompanyTel.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyTel);
			
			TableSchema.TableColumn colvarCompanyMemberID = new TableSchema.TableColumn(this);
			colvarCompanyMemberID.ColumnName = "CompanyMemberID";
			colvarCompanyMemberID.DataType = DbType.Int32;
			colvarCompanyMemberID.MaxLength = 0;
			colvarCompanyMemberID.AutoIncrement = false;
			colvarCompanyMemberID.IsNullable = true;
			colvarCompanyMemberID.IsPrimaryKey = false;
			colvarCompanyMemberID.IsForeignKey = false;
			colvarCompanyMemberID.IsReadOnly = false;
                
			colvarCompanyMemberID.DefaultSetting = @"";
			colvarCompanyMemberID.ForeignKeyTableName = "";
            colvarCompanyMemberID.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyMemberID);
			
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
			
			TableSchema.TableColumn colvarCompanyStatus = new TableSchema.TableColumn(this);
			colvarCompanyStatus.ColumnName = "CompanyStatus";
			colvarCompanyStatus.DataType = DbType.Int32;
			colvarCompanyStatus.MaxLength = 0;
			colvarCompanyStatus.AutoIncrement = false;
			colvarCompanyStatus.IsNullable = true;
			colvarCompanyStatus.IsPrimaryKey = false;
			colvarCompanyStatus.IsForeignKey = false;
			colvarCompanyStatus.IsReadOnly = false;
                
			colvarCompanyStatus.DefaultSetting = @"";
			colvarCompanyStatus.ForeignKeyTableName = "";
            colvarCompanyStatus.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyStatus);
			
		}
		#endregion
	}
}