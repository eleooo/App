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
	/// This is an ActiveRecord class which wraps the v_MealOrders table.
	/// </summary>
	[Serializable]
	public partial class VMealOrderSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VMealOrderSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VMealOrderSchema()
            :base("v_MealOrders")
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
			
			TableSchema.TableColumn colvarOrderSellerID = new TableSchema.TableColumn(this);
			colvarOrderSellerID.ColumnName = "OrderSellerID";
			colvarOrderSellerID.DataType = DbType.Int32;
			colvarOrderSellerID.MaxLength = 0;
			colvarOrderSellerID.AutoIncrement = false;
			colvarOrderSellerID.IsNullable = false;
			colvarOrderSellerID.IsPrimaryKey = false;
			colvarOrderSellerID.IsForeignKey = false;
			colvarOrderSellerID.IsReadOnly = false;
                
			colvarOrderSellerID.DefaultSetting = @"";
			colvarOrderSellerID.ForeignKeyTableName = "";
            colvarOrderSellerID.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderSellerID);
			
			TableSchema.TableColumn colvarOrderProduct = new TableSchema.TableColumn(this);
			colvarOrderProduct.ColumnName = "OrderProduct";
			colvarOrderProduct.DataType = DbType.String;
			colvarOrderProduct.MaxLength = 800;
			colvarOrderProduct.AutoIncrement = false;
			colvarOrderProduct.IsNullable = true;
			colvarOrderProduct.IsPrimaryKey = false;
			colvarOrderProduct.IsForeignKey = false;
			colvarOrderProduct.IsReadOnly = false;
                
			colvarOrderProduct.DefaultSetting = @"";
			colvarOrderProduct.ForeignKeyTableName = "";
            colvarOrderProduct.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderProduct);
			
			TableSchema.TableColumn colvarOrderQty = new TableSchema.TableColumn(this);
			colvarOrderQty.ColumnName = "OrderQty";
			colvarOrderQty.DataType = DbType.Int32;
			colvarOrderQty.MaxLength = 0;
			colvarOrderQty.AutoIncrement = false;
			colvarOrderQty.IsNullable = true;
			colvarOrderQty.IsPrimaryKey = false;
			colvarOrderQty.IsForeignKey = false;
			colvarOrderQty.IsReadOnly = false;
                
			colvarOrderQty.DefaultSetting = @"";
			colvarOrderQty.ForeignKeyTableName = "";
            colvarOrderQty.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderQty);
			
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
			
			TableSchema.TableColumn colvarOrderDateUpload = new TableSchema.TableColumn(this);
			colvarOrderDateUpload.ColumnName = "OrderDateUpload";
			colvarOrderDateUpload.DataType = DbType.DateTime;
			colvarOrderDateUpload.MaxLength = 0;
			colvarOrderDateUpload.AutoIncrement = false;
			colvarOrderDateUpload.IsNullable = false;
			colvarOrderDateUpload.IsPrimaryKey = false;
			colvarOrderDateUpload.IsForeignKey = false;
			colvarOrderDateUpload.IsReadOnly = false;
                
			colvarOrderDateUpload.DefaultSetting = @"";
			colvarOrderDateUpload.ForeignKeyTableName = "";
            colvarOrderDateUpload.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderDateUpload);
			
			TableSchema.TableColumn colvarOrderDateDeliver = new TableSchema.TableColumn(this);
			colvarOrderDateDeliver.ColumnName = "OrderDateDeliver";
			colvarOrderDateDeliver.DataType = DbType.DateTime;
			colvarOrderDateDeliver.MaxLength = 0;
			colvarOrderDateDeliver.AutoIncrement = false;
			colvarOrderDateDeliver.IsNullable = false;
			colvarOrderDateDeliver.IsPrimaryKey = false;
			colvarOrderDateDeliver.IsForeignKey = false;
			colvarOrderDateDeliver.IsReadOnly = false;
                
			colvarOrderDateDeliver.DefaultSetting = @"";
			colvarOrderDateDeliver.ForeignKeyTableName = "";
            colvarOrderDateDeliver.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderDateDeliver);
			
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
			
			TableSchema.TableColumn colvarMansionId = new TableSchema.TableColumn(this);
			colvarMansionId.ColumnName = "MansionId";
			colvarMansionId.DataType = DbType.Int32;
			colvarMansionId.MaxLength = 0;
			colvarMansionId.AutoIncrement = false;
			colvarMansionId.IsNullable = true;
			colvarMansionId.IsPrimaryKey = false;
			colvarMansionId.IsForeignKey = false;
			colvarMansionId.IsReadOnly = false;
                
			colvarMansionId.DefaultSetting = @"";
			colvarMansionId.ForeignKeyTableName = "";
            colvarMansionId.ApplyExtendedProperties();
			this.Columns.Add(colvarMansionId);
			
			TableSchema.TableColumn colvarServiceSum = new TableSchema.TableColumn(this);
			colvarServiceSum.ColumnName = "ServiceSum";
			colvarServiceSum.DataType = DbType.Decimal;
			colvarServiceSum.MaxLength = 0;
			colvarServiceSum.AutoIncrement = false;
			colvarServiceSum.IsNullable = true;
			colvarServiceSum.IsPrimaryKey = false;
			colvarServiceSum.IsForeignKey = false;
			colvarServiceSum.IsReadOnly = false;
                
			colvarServiceSum.DefaultSetting = @"";
			colvarServiceSum.ForeignKeyTableName = "";
            colvarServiceSum.ApplyExtendedProperties();
			this.Columns.Add(colvarServiceSum);
			
			TableSchema.TableColumn colvarIsNonOut = new TableSchema.TableColumn(this);
			colvarIsNonOut.ColumnName = "IsNonOut";
			colvarIsNonOut.DataType = DbType.Boolean;
			colvarIsNonOut.MaxLength = 0;
			colvarIsNonOut.AutoIncrement = false;
			colvarIsNonOut.IsNullable = true;
			colvarIsNonOut.IsPrimaryKey = false;
			colvarIsNonOut.IsForeignKey = false;
			colvarIsNonOut.IsReadOnly = false;
                
			colvarIsNonOut.DefaultSetting = @"";
			colvarIsNonOut.ForeignKeyTableName = "";
            colvarIsNonOut.ApplyExtendedProperties();
			this.Columns.Add(colvarIsNonOut);
			
			TableSchema.TableColumn colvarOrderNum = new TableSchema.TableColumn(this);
			colvarOrderNum.ColumnName = "OrderNum";
			colvarOrderNum.DataType = DbType.Int32;
			colvarOrderNum.MaxLength = 0;
			colvarOrderNum.AutoIncrement = false;
			colvarOrderNum.IsNullable = true;
			colvarOrderNum.IsPrimaryKey = false;
			colvarOrderNum.IsForeignKey = false;
			colvarOrderNum.IsReadOnly = false;
                
			colvarOrderNum.DefaultSetting = @"";
			colvarOrderNum.ForeignKeyTableName = "";
            colvarOrderNum.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderNum);
			
			TableSchema.TableColumn colvarOrderOper = new TableSchema.TableColumn(this);
			colvarOrderOper.ColumnName = "OrderOper";
			colvarOrderOper.DataType = DbType.Int32;
			colvarOrderOper.MaxLength = 0;
			colvarOrderOper.AutoIncrement = false;
			colvarOrderOper.IsNullable = true;
			colvarOrderOper.IsPrimaryKey = false;
			colvarOrderOper.IsForeignKey = false;
			colvarOrderOper.IsReadOnly = false;
                
			colvarOrderOper.DefaultSetting = @"";
			colvarOrderOper.ForeignKeyTableName = "";
            colvarOrderOper.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderOper);
			
			TableSchema.TableColumn colvarOrderModel = new TableSchema.TableColumn(this);
			colvarOrderModel.ColumnName = "OrderModel";
			colvarOrderModel.DataType = DbType.Int32;
			colvarOrderModel.MaxLength = 0;
			colvarOrderModel.AutoIncrement = false;
			colvarOrderModel.IsNullable = true;
			colvarOrderModel.IsPrimaryKey = false;
			colvarOrderModel.IsForeignKey = false;
			colvarOrderModel.IsReadOnly = false;
                
			colvarOrderModel.DefaultSetting = @"";
			colvarOrderModel.ForeignKeyTableName = "";
            colvarOrderModel.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderModel);
			
			TableSchema.TableColumn colvarOrderUpdateOn = new TableSchema.TableColumn(this);
			colvarOrderUpdateOn.ColumnName = "OrderUpdateOn";
			colvarOrderUpdateOn.DataType = DbType.DateTime;
			colvarOrderUpdateOn.MaxLength = 0;
			colvarOrderUpdateOn.AutoIncrement = false;
			colvarOrderUpdateOn.IsNullable = true;
			colvarOrderUpdateOn.IsPrimaryKey = false;
			colvarOrderUpdateOn.IsForeignKey = false;
			colvarOrderUpdateOn.IsReadOnly = false;
                
			colvarOrderUpdateOn.DefaultSetting = @"";
			colvarOrderUpdateOn.ForeignKeyTableName = "";
            colvarOrderUpdateOn.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderUpdateOn);
			
			TableSchema.TableColumn colvarHasOutOfStock = new TableSchema.TableColumn(this);
			colvarHasOutOfStock.ColumnName = "HasOutOfStock";
			colvarHasOutOfStock.DataType = DbType.Boolean;
			colvarHasOutOfStock.MaxLength = 0;
			colvarHasOutOfStock.AutoIncrement = false;
			colvarHasOutOfStock.IsNullable = true;
			colvarHasOutOfStock.IsPrimaryKey = false;
			colvarHasOutOfStock.IsForeignKey = false;
			colvarHasOutOfStock.IsReadOnly = false;
                
			colvarHasOutOfStock.DefaultSetting = @"";
			colvarHasOutOfStock.ForeignKeyTableName = "";
            colvarHasOutOfStock.ApplyExtendedProperties();
			this.Columns.Add(colvarHasOutOfStock);
			
			TableSchema.TableColumn colvarOrderPrePoint = new TableSchema.TableColumn(this);
			colvarOrderPrePoint.ColumnName = "OrderPrePoint";
			colvarOrderPrePoint.DataType = DbType.Decimal;
			colvarOrderPrePoint.MaxLength = 0;
			colvarOrderPrePoint.AutoIncrement = false;
			colvarOrderPrePoint.IsNullable = true;
			colvarOrderPrePoint.IsPrimaryKey = false;
			colvarOrderPrePoint.IsForeignKey = false;
			colvarOrderPrePoint.IsReadOnly = false;
                
			colvarOrderPrePoint.DefaultSetting = @"";
			colvarOrderPrePoint.ForeignKeyTableName = "";
            colvarOrderPrePoint.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderPrePoint);
			
			TableSchema.TableColumn colvarMsnType = new TableSchema.TableColumn(this);
			colvarMsnType.ColumnName = "MsnType";
			colvarMsnType.DataType = DbType.Int32;
			colvarMsnType.MaxLength = 0;
			colvarMsnType.AutoIncrement = false;
			colvarMsnType.IsNullable = true;
			colvarMsnType.IsPrimaryKey = false;
			colvarMsnType.IsForeignKey = false;
			colvarMsnType.IsReadOnly = false;
                
			colvarMsnType.DefaultSetting = @"";
			colvarMsnType.ForeignKeyTableName = "";
            colvarMsnType.ApplyExtendedProperties();
			this.Columns.Add(colvarMsnType);
			
		}
		#endregion
	}
}