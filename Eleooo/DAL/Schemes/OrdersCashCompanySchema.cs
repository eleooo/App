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
	/// This is an ActiveRecord class which wraps the Orders_CashCompany table.
	/// </summary>
	[Serializable]
	public partial class OrdersCashCompanySchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new OrdersCashCompanySchema();
            }
        }
		#region .ctors and Default Settings
		
		public OrdersCashCompanySchema()
            :base("Orders_CashCompany")
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
			
			TableSchema.TableColumn colvarCashOrderID = new TableSchema.TableColumn(this);
			colvarCashOrderID.ColumnName = "CashOrderID";
			colvarCashOrderID.DataType = DbType.Int32;
			colvarCashOrderID.MaxLength = 0;
			colvarCashOrderID.AutoIncrement = false;
			colvarCashOrderID.IsNullable = true;
			colvarCashOrderID.IsPrimaryKey = false;
			colvarCashOrderID.IsForeignKey = false;
			colvarCashOrderID.IsReadOnly = false;
                
			colvarCashOrderID.DefaultSetting = @"";
			colvarCashOrderID.ForeignKeyTableName = "";
            colvarCashOrderID.ApplyExtendedProperties();
			this.Columns.Add(colvarCashOrderID);
			
			TableSchema.TableColumn colvarCashCompanyID = new TableSchema.TableColumn(this);
			colvarCashCompanyID.ColumnName = "CashCompanyID";
			colvarCashCompanyID.DataType = DbType.Int32;
			colvarCashCompanyID.MaxLength = 0;
			colvarCashCompanyID.AutoIncrement = false;
			colvarCashCompanyID.IsNullable = true;
			colvarCashCompanyID.IsPrimaryKey = false;
			colvarCashCompanyID.IsForeignKey = false;
			colvarCashCompanyID.IsReadOnly = false;
                
			
            colvarCashCompanyID.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","储值商家ID"));
            colvarCashCompanyID.DefaultSetting = @"";
			colvarCashCompanyID.ForeignKeyTableName = "";
            colvarCashCompanyID.ApplyExtendedProperties();
			this.Columns.Add(colvarCashCompanyID);
			
			TableSchema.TableColumn colvarCashSum = new TableSchema.TableColumn(this);
			colvarCashSum.ColumnName = "CashSum";
			colvarCashSum.DataType = DbType.Int32;
			colvarCashSum.MaxLength = 0;
			colvarCashSum.AutoIncrement = false;
			colvarCashSum.IsNullable = false;
			colvarCashSum.IsPrimaryKey = false;
			colvarCashSum.IsForeignKey = false;
			colvarCashSum.IsReadOnly = false;
                
			colvarCashSum.DefaultSetting = @"";
			colvarCashSum.ForeignKeyTableName = "";
            colvarCashSum.ApplyExtendedProperties();
			this.Columns.Add(colvarCashSum);
			
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