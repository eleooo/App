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
	/// This is an ActiveRecord class which wraps the v_Sys_Company_Ads_Max_Point table.
	/// </summary>
	[Serializable]
	public partial class VSysCompanyAdsMaxPointSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VSysCompanyAdsMaxPointSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VSysCompanyAdsMaxPointSchema()
            :base("v_Sys_Company_Ads_Max_Point")
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
			colvarAdsPoint.DataType = DbType.Int32;
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