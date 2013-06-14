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
	/// This is an ActiveRecord class which wraps the Sys_Company_Ads_PointSetting table.
	/// </summary>
	[Serializable]
	public partial class SysCompanyAdsPointSettingSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysCompanyAdsPointSettingSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysCompanyAdsPointSettingSchema()
            :base("Sys_Company_Ads_PointSetting")
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
			
			TableSchema.TableColumn colvarAdsID = new TableSchema.TableColumn(this);
			colvarAdsID.ColumnName = "AdsID";
			colvarAdsID.DataType = DbType.Int32;
			colvarAdsID.MaxLength = 0;
			colvarAdsID.AutoIncrement = false;
			colvarAdsID.IsNullable = true;
			colvarAdsID.IsPrimaryKey = false;
			colvarAdsID.IsForeignKey = false;
			colvarAdsID.IsReadOnly = false;
                
			colvarAdsID.DefaultSetting = @"";
			colvarAdsID.ForeignKeyTableName = "";
            colvarAdsID.ApplyExtendedProperties();
			this.Columns.Add(colvarAdsID);
			
			TableSchema.TableColumn colvarOrderSumLimit = new TableSchema.TableColumn(this);
			colvarOrderSumLimit.ColumnName = "OrderSumLimit";
			colvarOrderSumLimit.DataType = DbType.Decimal;
			colvarOrderSumLimit.MaxLength = 0;
			colvarOrderSumLimit.AutoIncrement = false;
			colvarOrderSumLimit.IsNullable = true;
			colvarOrderSumLimit.IsPrimaryKey = false;
			colvarOrderSumLimit.IsForeignKey = false;
			colvarOrderSumLimit.IsReadOnly = false;
                
			colvarOrderSumLimit.DefaultSetting = @"";
			colvarOrderSumLimit.ForeignKeyTableName = "";
            colvarOrderSumLimit.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderSumLimit);
			
			TableSchema.TableColumn colvarAdsPoint = new TableSchema.TableColumn(this);
			colvarAdsPoint.ColumnName = "AdsPoint";
			colvarAdsPoint.DataType = DbType.Decimal;
			colvarAdsPoint.MaxLength = 0;
			colvarAdsPoint.AutoIncrement = false;
			colvarAdsPoint.IsNullable = true;
			colvarAdsPoint.IsPrimaryKey = false;
			colvarAdsPoint.IsForeignKey = false;
			colvarAdsPoint.IsReadOnly = false;
                
			colvarAdsPoint.DefaultSetting = @"";
			colvarAdsPoint.ForeignKeyTableName = "";
            colvarAdsPoint.ApplyExtendedProperties();
			this.Columns.Add(colvarAdsPoint);
			
		}
		#endregion
	}
}