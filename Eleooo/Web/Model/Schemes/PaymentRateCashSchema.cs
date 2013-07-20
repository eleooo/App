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
	/// This is an ActiveRecord class which wraps the PaymentRateCash table.
	/// </summary>
	[Serializable]
	public partial class PaymentRateCashSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new PaymentRateCashSchema();
            }
        }
		#region .ctors and Default Settings
		
		public PaymentRateCashSchema()
            :base("PaymentRateCash")
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
			
			TableSchema.TableColumn colvarPaymentRateCashID = new TableSchema.TableColumn(this);
			colvarPaymentRateCashID.ColumnName = "PaymentRateCashID";
			colvarPaymentRateCashID.DataType = DbType.Int32;
			colvarPaymentRateCashID.MaxLength = 0;
			colvarPaymentRateCashID.AutoIncrement = true;
			colvarPaymentRateCashID.IsNullable = false;
			colvarPaymentRateCashID.IsPrimaryKey = true;
			colvarPaymentRateCashID.IsForeignKey = false;
			colvarPaymentRateCashID.IsReadOnly = false;
                
			colvarPaymentRateCashID.DefaultSetting = @"";
			colvarPaymentRateCashID.ForeignKeyTableName = "";
            colvarPaymentRateCashID.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCashID);
			
			TableSchema.TableColumn colvarPaymentRateCashDate = new TableSchema.TableColumn(this);
			colvarPaymentRateCashDate.ColumnName = "PaymentRateCashDate";
			colvarPaymentRateCashDate.DataType = DbType.DateTime;
			colvarPaymentRateCashDate.MaxLength = 0;
			colvarPaymentRateCashDate.AutoIncrement = false;
			colvarPaymentRateCashDate.IsNullable = true;
			colvarPaymentRateCashDate.IsPrimaryKey = false;
			colvarPaymentRateCashDate.IsForeignKey = false;
			colvarPaymentRateCashDate.IsReadOnly = false;
                
			colvarPaymentRateCashDate.DefaultSetting = @"";
			colvarPaymentRateCashDate.ForeignKeyTableName = "";
            colvarPaymentRateCashDate.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCashDate);
			
			TableSchema.TableColumn colvarPaymentRateCashDateStart = new TableSchema.TableColumn(this);
			colvarPaymentRateCashDateStart.ColumnName = "PaymentRateCashDateStart";
			colvarPaymentRateCashDateStart.DataType = DbType.DateTime;
			colvarPaymentRateCashDateStart.MaxLength = 0;
			colvarPaymentRateCashDateStart.AutoIncrement = false;
			colvarPaymentRateCashDateStart.IsNullable = true;
			colvarPaymentRateCashDateStart.IsPrimaryKey = false;
			colvarPaymentRateCashDateStart.IsForeignKey = false;
			colvarPaymentRateCashDateStart.IsReadOnly = false;
                
			colvarPaymentRateCashDateStart.DefaultSetting = @"";
			colvarPaymentRateCashDateStart.ForeignKeyTableName = "";
            colvarPaymentRateCashDateStart.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCashDateStart);
			
			TableSchema.TableColumn colvarPaymentRateCashDateEnd = new TableSchema.TableColumn(this);
			colvarPaymentRateCashDateEnd.ColumnName = "PaymentRateCashDateEnd";
			colvarPaymentRateCashDateEnd.DataType = DbType.DateTime;
			colvarPaymentRateCashDateEnd.MaxLength = 0;
			colvarPaymentRateCashDateEnd.AutoIncrement = false;
			colvarPaymentRateCashDateEnd.IsNullable = true;
			colvarPaymentRateCashDateEnd.IsPrimaryKey = false;
			colvarPaymentRateCashDateEnd.IsForeignKey = false;
			colvarPaymentRateCashDateEnd.IsReadOnly = false;
                
			colvarPaymentRateCashDateEnd.DefaultSetting = @"";
			colvarPaymentRateCashDateEnd.ForeignKeyTableName = "";
            colvarPaymentRateCashDateEnd.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCashDateEnd);
			
			TableSchema.TableColumn colvarPaymentRateCashCompanyID = new TableSchema.TableColumn(this);
			colvarPaymentRateCashCompanyID.ColumnName = "PaymentRateCashCompanyID";
			colvarPaymentRateCashCompanyID.DataType = DbType.Int32;
			colvarPaymentRateCashCompanyID.MaxLength = 0;
			colvarPaymentRateCashCompanyID.AutoIncrement = false;
			colvarPaymentRateCashCompanyID.IsNullable = true;
			colvarPaymentRateCashCompanyID.IsPrimaryKey = false;
			colvarPaymentRateCashCompanyID.IsForeignKey = false;
			colvarPaymentRateCashCompanyID.IsReadOnly = false;
                
			colvarPaymentRateCashCompanyID.DefaultSetting = @"";
			colvarPaymentRateCashCompanyID.ForeignKeyTableName = "";
            colvarPaymentRateCashCompanyID.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCashCompanyID);
			
			TableSchema.TableColumn colvarPaymentRateCash1 = new TableSchema.TableColumn(this);
			colvarPaymentRateCash1.ColumnName = "PaymentRateCash1";
			colvarPaymentRateCash1.DataType = DbType.Decimal;
			colvarPaymentRateCash1.MaxLength = 0;
			colvarPaymentRateCash1.AutoIncrement = false;
			colvarPaymentRateCash1.IsNullable = true;
			colvarPaymentRateCash1.IsPrimaryKey = false;
			colvarPaymentRateCash1.IsForeignKey = false;
			colvarPaymentRateCash1.IsReadOnly = false;
                
			colvarPaymentRateCash1.DefaultSetting = @"";
			colvarPaymentRateCash1.ForeignKeyTableName = "";
            colvarPaymentRateCash1.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCash1);
			
			TableSchema.TableColumn colvarPaymentRateCash2 = new TableSchema.TableColumn(this);
			colvarPaymentRateCash2.ColumnName = "PaymentRateCash2";
			colvarPaymentRateCash2.DataType = DbType.Decimal;
			colvarPaymentRateCash2.MaxLength = 0;
			colvarPaymentRateCash2.AutoIncrement = false;
			colvarPaymentRateCash2.IsNullable = true;
			colvarPaymentRateCash2.IsPrimaryKey = false;
			colvarPaymentRateCash2.IsForeignKey = false;
			colvarPaymentRateCash2.IsReadOnly = false;
                
			colvarPaymentRateCash2.DefaultSetting = @"";
			colvarPaymentRateCash2.ForeignKeyTableName = "";
            colvarPaymentRateCash2.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCash2);
			
			TableSchema.TableColumn colvarPaymentRateCashSum = new TableSchema.TableColumn(this);
			colvarPaymentRateCashSum.ColumnName = "PaymentRateCashSum";
			colvarPaymentRateCashSum.DataType = DbType.Decimal;
			colvarPaymentRateCashSum.MaxLength = 0;
			colvarPaymentRateCashSum.AutoIncrement = false;
			colvarPaymentRateCashSum.IsNullable = true;
			colvarPaymentRateCashSum.IsPrimaryKey = false;
			colvarPaymentRateCashSum.IsForeignKey = false;
			colvarPaymentRateCashSum.IsReadOnly = false;
                
			colvarPaymentRateCashSum.DefaultSetting = @"";
			colvarPaymentRateCashSum.ForeignKeyTableName = "";
            colvarPaymentRateCashSum.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCashSum);
			
			TableSchema.TableColumn colvarPaymentRateCashMemo = new TableSchema.TableColumn(this);
			colvarPaymentRateCashMemo.ColumnName = "PaymentRateCashMemo";
			colvarPaymentRateCashMemo.DataType = DbType.String;
			colvarPaymentRateCashMemo.MaxLength = 50;
			colvarPaymentRateCashMemo.AutoIncrement = false;
			colvarPaymentRateCashMemo.IsNullable = true;
			colvarPaymentRateCashMemo.IsPrimaryKey = false;
			colvarPaymentRateCashMemo.IsForeignKey = false;
			colvarPaymentRateCashMemo.IsReadOnly = false;
                
			colvarPaymentRateCashMemo.DefaultSetting = @"";
			colvarPaymentRateCashMemo.ForeignKeyTableName = "";
            colvarPaymentRateCashMemo.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCashMemo);
			
			TableSchema.TableColumn colvarPaymentRateCashStatus = new TableSchema.TableColumn(this);
			colvarPaymentRateCashStatus.ColumnName = "PaymentRateCashStatus";
			colvarPaymentRateCashStatus.DataType = DbType.Int32;
			colvarPaymentRateCashStatus.MaxLength = 0;
			colvarPaymentRateCashStatus.AutoIncrement = false;
			colvarPaymentRateCashStatus.IsNullable = true;
			colvarPaymentRateCashStatus.IsPrimaryKey = false;
			colvarPaymentRateCashStatus.IsForeignKey = false;
			colvarPaymentRateCashStatus.IsReadOnly = false;
                
			colvarPaymentRateCashStatus.DefaultSetting = @"";
			colvarPaymentRateCashStatus.ForeignKeyTableName = "";
            colvarPaymentRateCashStatus.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCashStatus);
			
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