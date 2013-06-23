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
 //Generated on 2013/6/22 19:46:28 by Administrator
// <auto-generated />
namespace Eleooo.DAL
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the Sys_Ads_ClickSetting table.
	/// </summary>
	[Serializable]
	public partial class SysAdsClickSettingSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysAdsClickSettingSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysAdsClickSettingSchema()
            :base("Sys_Ads_ClickSetting")
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
			
			TableSchema.TableColumn colvarClickCountLimit = new TableSchema.TableColumn(this);
			colvarClickCountLimit.ColumnName = "ClickCountLimit";
			colvarClickCountLimit.DataType = DbType.Int32;
			colvarClickCountLimit.MaxLength = 0;
			colvarClickCountLimit.AutoIncrement = false;
			colvarClickCountLimit.IsNullable = true;
			colvarClickCountLimit.IsPrimaryKey = false;
			colvarClickCountLimit.IsForeignKey = false;
			colvarClickCountLimit.IsReadOnly = false;
                
			colvarClickCountLimit.DefaultSetting = @"";
			colvarClickCountLimit.ForeignKeyTableName = "";
            colvarClickCountLimit.ApplyExtendedProperties();
			this.Columns.Add(colvarClickCountLimit);
			
		}
		#endregion
	}
}