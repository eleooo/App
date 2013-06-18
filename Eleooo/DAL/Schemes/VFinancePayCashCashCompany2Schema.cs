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
	/// This is an ActiveRecord class which wraps the v_FinancePayCash_CashCompany2 table.
	/// </summary>
	[Serializable]
	public partial class VFinancePayCashCashCompany2Schema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VFinancePayCashCashCompany2Schema();
            }
        }
		#region .ctors and Default Settings
		
		public VFinancePayCashCashCompany2Schema()
            :base("v_FinancePayCash_CashCompany2")
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
			colvarId.AutoIncrement = false;
			colvarId.IsNullable = false;
			colvarId.IsPrimaryKey = false;
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
			
			TableSchema.TableColumn colvarOrderCode = new TableSchema.TableColumn(this);
			colvarOrderCode.ColumnName = "OrderCode";
			colvarOrderCode.DataType = DbType.String;
			colvarOrderCode.MaxLength = 50;
			colvarOrderCode.AutoIncrement = false;
			colvarOrderCode.IsNullable = false;
			colvarOrderCode.IsPrimaryKey = false;
			colvarOrderCode.IsForeignKey = false;
			colvarOrderCode.IsReadOnly = false;
                
			colvarOrderCode.DefaultSetting = @"";
			colvarOrderCode.ForeignKeyTableName = "";
            colvarOrderCode.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderCode);
			
			TableSchema.TableColumn colvarOrderDate = new TableSchema.TableColumn(this);
			colvarOrderDate.ColumnName = "OrderDate";
			colvarOrderDate.DataType = DbType.DateTime;
			colvarOrderDate.MaxLength = 0;
			colvarOrderDate.AutoIncrement = false;
			colvarOrderDate.IsNullable = false;
			colvarOrderDate.IsPrimaryKey = false;
			colvarOrderDate.IsForeignKey = false;
			colvarOrderDate.IsReadOnly = false;
                
			colvarOrderDate.DefaultSetting = @"";
			colvarOrderDate.ForeignKeyTableName = "";
            colvarOrderDate.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderDate);
			
			TableSchema.TableColumn colvarOrderCard = new TableSchema.TableColumn(this);
			colvarOrderCard.ColumnName = "OrderCard";
			colvarOrderCard.DataType = DbType.String;
			colvarOrderCard.MaxLength = 50;
			colvarOrderCard.AutoIncrement = false;
			colvarOrderCard.IsNullable = false;
			colvarOrderCard.IsPrimaryKey = false;
			colvarOrderCard.IsForeignKey = false;
			colvarOrderCard.IsReadOnly = false;
                
			colvarOrderCard.DefaultSetting = @"";
			colvarOrderCard.ForeignKeyTableName = "";
            colvarOrderCard.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderCard);
			
			TableSchema.TableColumn colvarOrderMemberID = new TableSchema.TableColumn(this);
			colvarOrderMemberID.ColumnName = "OrderMemberID";
			colvarOrderMemberID.DataType = DbType.Int32;
			colvarOrderMemberID.MaxLength = 0;
			colvarOrderMemberID.AutoIncrement = false;
			colvarOrderMemberID.IsNullable = false;
			colvarOrderMemberID.IsPrimaryKey = false;
			colvarOrderMemberID.IsForeignKey = false;
			colvarOrderMemberID.IsReadOnly = false;
                
			colvarOrderMemberID.DefaultSetting = @"";
			colvarOrderMemberID.ForeignKeyTableName = "";
            colvarOrderMemberID.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderMemberID);
			
			TableSchema.TableColumn colvarExpendCompanyID = new TableSchema.TableColumn(this);
			colvarExpendCompanyID.ColumnName = "ExpendCompanyID";
			colvarExpendCompanyID.DataType = DbType.Int32;
			colvarExpendCompanyID.MaxLength = 0;
			colvarExpendCompanyID.AutoIncrement = false;
			colvarExpendCompanyID.IsNullable = false;
			colvarExpendCompanyID.IsPrimaryKey = false;
			colvarExpendCompanyID.IsForeignKey = false;
			colvarExpendCompanyID.IsReadOnly = false;
                
			colvarExpendCompanyID.DefaultSetting = @"";
			colvarExpendCompanyID.ForeignKeyTableName = "";
            colvarExpendCompanyID.ApplyExtendedProperties();
			this.Columns.Add(colvarExpendCompanyID);
			
			TableSchema.TableColumn colvarOrderSum = new TableSchema.TableColumn(this);
			colvarOrderSum.ColumnName = "OrderSum";
			colvarOrderSum.DataType = DbType.Decimal;
			colvarOrderSum.MaxLength = 0;
			colvarOrderSum.AutoIncrement = false;
			colvarOrderSum.IsNullable = true;
			colvarOrderSum.IsPrimaryKey = false;
			colvarOrderSum.IsForeignKey = false;
			colvarOrderSum.IsReadOnly = false;
                
			colvarOrderSum.DefaultSetting = @"";
			colvarOrderSum.ForeignKeyTableName = "";
            colvarOrderSum.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderSum);
			
			TableSchema.TableColumn colvarOrderSumOk = new TableSchema.TableColumn(this);
			colvarOrderSumOk.ColumnName = "OrderSumOk";
			colvarOrderSumOk.DataType = DbType.Decimal;
			colvarOrderSumOk.MaxLength = 0;
			colvarOrderSumOk.AutoIncrement = false;
			colvarOrderSumOk.IsNullable = true;
			colvarOrderSumOk.IsPrimaryKey = false;
			colvarOrderSumOk.IsForeignKey = false;
			colvarOrderSumOk.IsReadOnly = false;
                
			colvarOrderSumOk.DefaultSetting = @"";
			colvarOrderSumOk.ForeignKeyTableName = "";
            colvarOrderSumOk.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderSumOk);
			
			TableSchema.TableColumn colvarOrderRateSale = new TableSchema.TableColumn(this);
			colvarOrderRateSale.ColumnName = "OrderRateSale";
			colvarOrderRateSale.DataType = DbType.Decimal;
			colvarOrderRateSale.MaxLength = 0;
			colvarOrderRateSale.AutoIncrement = false;
			colvarOrderRateSale.IsNullable = true;
			colvarOrderRateSale.IsPrimaryKey = false;
			colvarOrderRateSale.IsForeignKey = false;
			colvarOrderRateSale.IsReadOnly = false;
                
			colvarOrderRateSale.DefaultSetting = @"";
			colvarOrderRateSale.ForeignKeyTableName = "";
            colvarOrderRateSale.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderRateSale);
			
			TableSchema.TableColumn colvarOrderRate = new TableSchema.TableColumn(this);
			colvarOrderRate.ColumnName = "OrderRate";
			colvarOrderRate.DataType = DbType.Decimal;
			colvarOrderRate.MaxLength = 0;
			colvarOrderRate.AutoIncrement = false;
			colvarOrderRate.IsNullable = true;
			colvarOrderRate.IsPrimaryKey = false;
			colvarOrderRate.IsForeignKey = false;
			colvarOrderRate.IsReadOnly = false;
                
			colvarOrderRate.DefaultSetting = @"";
			colvarOrderRate.ForeignKeyTableName = "";
            colvarOrderRate.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderRate);
			
			TableSchema.TableColumn colvarOrderPoint = new TableSchema.TableColumn(this);
			colvarOrderPoint.ColumnName = "OrderPoint";
			colvarOrderPoint.DataType = DbType.Decimal;
			colvarOrderPoint.MaxLength = 0;
			colvarOrderPoint.AutoIncrement = false;
			colvarOrderPoint.IsNullable = true;
			colvarOrderPoint.IsPrimaryKey = false;
			colvarOrderPoint.IsForeignKey = false;
			colvarOrderPoint.IsReadOnly = false;
                
			colvarOrderPoint.DefaultSetting = @"";
			colvarOrderPoint.ForeignKeyTableName = "";
            colvarOrderPoint.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderPoint);
			
			TableSchema.TableColumn colvarOrderPay = new TableSchema.TableColumn(this);
			colvarOrderPay.ColumnName = "OrderPay";
			colvarOrderPay.DataType = DbType.Decimal;
			colvarOrderPay.MaxLength = 0;
			colvarOrderPay.AutoIncrement = false;
			colvarOrderPay.IsNullable = true;
			colvarOrderPay.IsPrimaryKey = false;
			colvarOrderPay.IsForeignKey = false;
			colvarOrderPay.IsReadOnly = false;
                
			colvarOrderPay.DefaultSetting = @"";
			colvarOrderPay.ForeignKeyTableName = "";
            colvarOrderPay.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderPay);
			
			TableSchema.TableColumn colvarOrderPayCash = new TableSchema.TableColumn(this);
			colvarOrderPayCash.ColumnName = "OrderPayCash";
			colvarOrderPayCash.DataType = DbType.Decimal;
			colvarOrderPayCash.MaxLength = 0;
			colvarOrderPayCash.AutoIncrement = false;
			colvarOrderPayCash.IsNullable = true;
			colvarOrderPayCash.IsPrimaryKey = false;
			colvarOrderPayCash.IsForeignKey = false;
			colvarOrderPayCash.IsReadOnly = false;
                
			colvarOrderPayCash.DefaultSetting = @"";
			colvarOrderPayCash.ForeignKeyTableName = "";
            colvarOrderPayCash.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderPayCash);
			
			TableSchema.TableColumn colvarOrderPayPoint = new TableSchema.TableColumn(this);
			colvarOrderPayPoint.ColumnName = "OrderPayPoint";
			colvarOrderPayPoint.DataType = DbType.Decimal;
			colvarOrderPayPoint.MaxLength = 0;
			colvarOrderPayPoint.AutoIncrement = false;
			colvarOrderPayPoint.IsNullable = true;
			colvarOrderPayPoint.IsPrimaryKey = false;
			colvarOrderPayPoint.IsForeignKey = false;
			colvarOrderPayPoint.IsReadOnly = false;
                
			colvarOrderPayPoint.DefaultSetting = @"";
			colvarOrderPayPoint.ForeignKeyTableName = "";
            colvarOrderPayPoint.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderPayPoint);
			
			TableSchema.TableColumn colvarOrderStatus = new TableSchema.TableColumn(this);
			colvarOrderStatus.ColumnName = "OrderStatus";
			colvarOrderStatus.DataType = DbType.Int32;
			colvarOrderStatus.MaxLength = 0;
			colvarOrderStatus.AutoIncrement = false;
			colvarOrderStatus.IsNullable = false;
			colvarOrderStatus.IsPrimaryKey = false;
			colvarOrderStatus.IsForeignKey = false;
			colvarOrderStatus.IsReadOnly = false;
                
			colvarOrderStatus.DefaultSetting = @"";
			colvarOrderStatus.ForeignKeyTableName = "";
            colvarOrderStatus.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderStatus);
			
			TableSchema.TableColumn colvarOrderType = new TableSchema.TableColumn(this);
			colvarOrderType.ColumnName = "OrderType";
			colvarOrderType.DataType = DbType.Int32;
			colvarOrderType.MaxLength = 0;
			colvarOrderType.AutoIncrement = false;
			colvarOrderType.IsNullable = true;
			colvarOrderType.IsPrimaryKey = false;
			colvarOrderType.IsForeignKey = false;
			colvarOrderType.IsReadOnly = false;
                
			colvarOrderType.DefaultSetting = @"";
			colvarOrderType.ForeignKeyTableName = "";
            colvarOrderType.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderType);
			
			TableSchema.TableColumn colvarOrderMemo = new TableSchema.TableColumn(this);
			colvarOrderMemo.ColumnName = "OrderMemo";
			colvarOrderMemo.DataType = DbType.String;
			colvarOrderMemo.MaxLength = 200;
			colvarOrderMemo.AutoIncrement = false;
			colvarOrderMemo.IsNullable = false;
			colvarOrderMemo.IsPrimaryKey = false;
			colvarOrderMemo.IsForeignKey = false;
			colvarOrderMemo.IsReadOnly = false;
                
			colvarOrderMemo.DefaultSetting = @"";
			colvarOrderMemo.ForeignKeyTableName = "";
            colvarOrderMemo.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderMemo);
			
		}
		#endregion
	}
}