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
	/// This is an ActiveRecord class which wraps the v_Company_MaxCashRate table.
	/// </summary>
	[Serializable]
	public partial class VCompanyMaxCashRateSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VCompanyMaxCashRateSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VCompanyMaxCashRateSchema()
            :base("v_Company_MaxCashRate")
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
			
		}
		#endregion
	}
}