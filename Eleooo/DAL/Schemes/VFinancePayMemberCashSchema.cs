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
 //Generated on 2013/6/17 22:55:47 by Administrator
// <auto-generated />
namespace Eleooo.DAL
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the v_FinancePay_MemberCash table.
	/// </summary>
	[Serializable]
	public partial class VFinancePayMemberCashSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VFinancePayMemberCashSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VFinancePayMemberCashSchema()
            :base("v_FinancePay_MemberCash")
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
			
			TableSchema.TableColumn colvarCashID = new TableSchema.TableColumn(this);
			colvarCashID.ColumnName = "CashID";
			colvarCashID.DataType = DbType.Int32;
			colvarCashID.MaxLength = 0;
			colvarCashID.AutoIncrement = false;
			colvarCashID.IsNullable = false;
			colvarCashID.IsPrimaryKey = false;
			colvarCashID.IsForeignKey = false;
			colvarCashID.IsReadOnly = false;
                
			colvarCashID.DefaultSetting = @"";
			colvarCashID.ForeignKeyTableName = "";
            colvarCashID.ApplyExtendedProperties();
			this.Columns.Add(colvarCashID);
			
			TableSchema.TableColumn colvarCashDate = new TableSchema.TableColumn(this);
			colvarCashDate.ColumnName = "CashDate";
			colvarCashDate.DataType = DbType.DateTime;
			colvarCashDate.MaxLength = 0;
			colvarCashDate.AutoIncrement = false;
			colvarCashDate.IsNullable = true;
			colvarCashDate.IsPrimaryKey = false;
			colvarCashDate.IsForeignKey = false;
			colvarCashDate.IsReadOnly = false;
                
			colvarCashDate.DefaultSetting = @"";
			colvarCashDate.ForeignKeyTableName = "";
            colvarCashDate.ApplyExtendedProperties();
			this.Columns.Add(colvarCashDate);
			
			TableSchema.TableColumn colvarCashMemberID = new TableSchema.TableColumn(this);
			colvarCashMemberID.ColumnName = "CashMemberID";
			colvarCashMemberID.DataType = DbType.Int32;
			colvarCashMemberID.MaxLength = 0;
			colvarCashMemberID.AutoIncrement = false;
			colvarCashMemberID.IsNullable = true;
			colvarCashMemberID.IsPrimaryKey = false;
			colvarCashMemberID.IsForeignKey = false;
			colvarCashMemberID.IsReadOnly = false;
                
			colvarCashMemberID.DefaultSetting = @"";
			colvarCashMemberID.ForeignKeyTableName = "";
            colvarCashMemberID.ApplyExtendedProperties();
			this.Columns.Add(colvarCashMemberID);
			
			TableSchema.TableColumn colvarCashCompanyID = new TableSchema.TableColumn(this);
			colvarCashCompanyID.ColumnName = "CashCompanyID";
			colvarCashCompanyID.DataType = DbType.Int32;
			colvarCashCompanyID.MaxLength = 0;
			colvarCashCompanyID.AutoIncrement = false;
			colvarCashCompanyID.IsNullable = true;
			colvarCashCompanyID.IsPrimaryKey = false;
			colvarCashCompanyID.IsForeignKey = false;
			colvarCashCompanyID.IsReadOnly = false;
                
			colvarCashCompanyID.DefaultSetting = @"";
			colvarCashCompanyID.ForeignKeyTableName = "";
            colvarCashCompanyID.ApplyExtendedProperties();
			this.Columns.Add(colvarCashCompanyID);
			
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
			
			TableSchema.TableColumn colvarCashSum = new TableSchema.TableColumn(this);
			colvarCashSum.ColumnName = "CashSum";
			colvarCashSum.DataType = DbType.Decimal;
			colvarCashSum.MaxLength = 0;
			colvarCashSum.AutoIncrement = false;
			colvarCashSum.IsNullable = true;
			colvarCashSum.IsPrimaryKey = false;
			colvarCashSum.IsForeignKey = false;
			colvarCashSum.IsReadOnly = false;
                
			colvarCashSum.DefaultSetting = @"";
			colvarCashSum.ForeignKeyTableName = "";
            colvarCashSum.ApplyExtendedProperties();
			this.Columns.Add(colvarCashSum);
			
			TableSchema.TableColumn colvarCashRate = new TableSchema.TableColumn(this);
			colvarCashRate.ColumnName = "CashRate";
			colvarCashRate.DataType = DbType.Decimal;
			colvarCashRate.MaxLength = 0;
			colvarCashRate.AutoIncrement = false;
			colvarCashRate.IsNullable = true;
			colvarCashRate.IsPrimaryKey = false;
			colvarCashRate.IsForeignKey = false;
			colvarCashRate.IsReadOnly = false;
                
			colvarCashRate.DefaultSetting = @"";
			colvarCashRate.ForeignKeyTableName = "";
            colvarCashRate.ApplyExtendedProperties();
			this.Columns.Add(colvarCashRate);
			
			TableSchema.TableColumn colvarCashPoint = new TableSchema.TableColumn(this);
			colvarCashPoint.ColumnName = "CashPoint";
			colvarCashPoint.DataType = DbType.Decimal;
			colvarCashPoint.MaxLength = 0;
			colvarCashPoint.AutoIncrement = false;
			colvarCashPoint.IsNullable = true;
			colvarCashPoint.IsPrimaryKey = false;
			colvarCashPoint.IsForeignKey = false;
			colvarCashPoint.IsReadOnly = false;
                
			colvarCashPoint.DefaultSetting = @"";
			colvarCashPoint.ForeignKeyTableName = "";
            colvarCashPoint.ApplyExtendedProperties();
			this.Columns.Add(colvarCashPoint);
			
			TableSchema.TableColumn colvarCashMemo = new TableSchema.TableColumn(this);
			colvarCashMemo.ColumnName = "CashMemo";
			colvarCashMemo.DataType = DbType.String;
			colvarCashMemo.MaxLength = 50;
			colvarCashMemo.AutoIncrement = false;
			colvarCashMemo.IsNullable = true;
			colvarCashMemo.IsPrimaryKey = false;
			colvarCashMemo.IsForeignKey = false;
			colvarCashMemo.IsReadOnly = false;
                
			colvarCashMemo.DefaultSetting = @"";
			colvarCashMemo.ForeignKeyTableName = "";
            colvarCashMemo.ApplyExtendedProperties();
			this.Columns.Add(colvarCashMemo);
			
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