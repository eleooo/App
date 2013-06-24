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
	/// This is an ActiveRecord class which wraps the v_Company_LastThisMonthOrderAndCash table.
	/// </summary>
	[Serializable]
	public partial class VCompanyLastThisMonthOrderAndCashSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VCompanyLastThisMonthOrderAndCashSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VCompanyLastThisMonthOrderAndCashSchema()
            :base("v_Company_LastThisMonthOrderAndCash")
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
			
			TableSchema.TableColumn colvarCompanyID = new TableSchema.TableColumn(this);
			colvarCompanyID.ColumnName = "CompanyID";
			colvarCompanyID.DataType = DbType.Int32;
			colvarCompanyID.MaxLength = 0;
			colvarCompanyID.AutoIncrement = false;
			colvarCompanyID.IsNullable = true;
			colvarCompanyID.IsPrimaryKey = false;
			colvarCompanyID.IsForeignKey = false;
			colvarCompanyID.IsReadOnly = false;
                
			colvarCompanyID.DefaultSetting = @"";
			colvarCompanyID.ForeignKeyTableName = "";
            colvarCompanyID.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyID);
			
			TableSchema.TableColumn colvarLastMonthOrderSum = new TableSchema.TableColumn(this);
			colvarLastMonthOrderSum.ColumnName = "LastMonthOrderSum";
			colvarLastMonthOrderSum.DataType = DbType.Decimal;
			colvarLastMonthOrderSum.MaxLength = 0;
			colvarLastMonthOrderSum.AutoIncrement = false;
			colvarLastMonthOrderSum.IsNullable = true;
			colvarLastMonthOrderSum.IsPrimaryKey = false;
			colvarLastMonthOrderSum.IsForeignKey = false;
			colvarLastMonthOrderSum.IsReadOnly = false;
                
			colvarLastMonthOrderSum.DefaultSetting = @"";
			colvarLastMonthOrderSum.ForeignKeyTableName = "";
            colvarLastMonthOrderSum.ApplyExtendedProperties();
			this.Columns.Add(colvarLastMonthOrderSum);
			
			TableSchema.TableColumn colvarThisMonthOrderSum = new TableSchema.TableColumn(this);
			colvarThisMonthOrderSum.ColumnName = "ThisMonthOrderSum";
			colvarThisMonthOrderSum.DataType = DbType.Decimal;
			colvarThisMonthOrderSum.MaxLength = 0;
			colvarThisMonthOrderSum.AutoIncrement = false;
			colvarThisMonthOrderSum.IsNullable = true;
			colvarThisMonthOrderSum.IsPrimaryKey = false;
			colvarThisMonthOrderSum.IsForeignKey = false;
			colvarThisMonthOrderSum.IsReadOnly = false;
                
			colvarThisMonthOrderSum.DefaultSetting = @"";
			colvarThisMonthOrderSum.ForeignKeyTableName = "";
            colvarThisMonthOrderSum.ApplyExtendedProperties();
			this.Columns.Add(colvarThisMonthOrderSum);
			
			TableSchema.TableColumn colvarLastMonthPaymentCashSum = new TableSchema.TableColumn(this);
			colvarLastMonthPaymentCashSum.ColumnName = "LastMonthPaymentCashSum";
			colvarLastMonthPaymentCashSum.DataType = DbType.Decimal;
			colvarLastMonthPaymentCashSum.MaxLength = 0;
			colvarLastMonthPaymentCashSum.AutoIncrement = false;
			colvarLastMonthPaymentCashSum.IsNullable = true;
			colvarLastMonthPaymentCashSum.IsPrimaryKey = false;
			colvarLastMonthPaymentCashSum.IsForeignKey = false;
			colvarLastMonthPaymentCashSum.IsReadOnly = false;
                
			colvarLastMonthPaymentCashSum.DefaultSetting = @"";
			colvarLastMonthPaymentCashSum.ForeignKeyTableName = "";
            colvarLastMonthPaymentCashSum.ApplyExtendedProperties();
			this.Columns.Add(colvarLastMonthPaymentCashSum);
			
			TableSchema.TableColumn colvarThisMonthPaymentCashSum = new TableSchema.TableColumn(this);
			colvarThisMonthPaymentCashSum.ColumnName = "ThisMonthPaymentCashSum";
			colvarThisMonthPaymentCashSum.DataType = DbType.Decimal;
			colvarThisMonthPaymentCashSum.MaxLength = 0;
			colvarThisMonthPaymentCashSum.AutoIncrement = false;
			colvarThisMonthPaymentCashSum.IsNullable = true;
			colvarThisMonthPaymentCashSum.IsPrimaryKey = false;
			colvarThisMonthPaymentCashSum.IsForeignKey = false;
			colvarThisMonthPaymentCashSum.IsReadOnly = false;
                
			colvarThisMonthPaymentCashSum.DefaultSetting = @"";
			colvarThisMonthPaymentCashSum.ForeignKeyTableName = "";
            colvarThisMonthPaymentCashSum.ApplyExtendedProperties();
			this.Columns.Add(colvarThisMonthPaymentCashSum);
			
		}
		#endregion
	}
}