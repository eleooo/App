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
	/// This is an ActiveRecord class which wraps the v_FinancePay_LastDate table.
	/// </summary>
	[Serializable]
	public partial class VFinancePayLastDateSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VFinancePayLastDateSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VFinancePayLastDateSchema()
            :base("v_FinancePay_LastDate")
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
			
			TableSchema.TableColumn colvarPaymentRateCompanyID = new TableSchema.TableColumn(this);
			colvarPaymentRateCompanyID.ColumnName = "PaymentRateCompanyID";
			colvarPaymentRateCompanyID.DataType = DbType.Int32;
			colvarPaymentRateCompanyID.MaxLength = 0;
			colvarPaymentRateCompanyID.AutoIncrement = false;
			colvarPaymentRateCompanyID.IsNullable = true;
			colvarPaymentRateCompanyID.IsPrimaryKey = false;
			colvarPaymentRateCompanyID.IsForeignKey = false;
			colvarPaymentRateCompanyID.IsReadOnly = false;
                
			colvarPaymentRateCompanyID.DefaultSetting = @"";
			colvarPaymentRateCompanyID.ForeignKeyTableName = "";
            colvarPaymentRateCompanyID.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateCompanyID);
			
			TableSchema.TableColumn colvarPaymentRateDate = new TableSchema.TableColumn(this);
			colvarPaymentRateDate.ColumnName = "PaymentRateDate";
			colvarPaymentRateDate.DataType = DbType.DateTime;
			colvarPaymentRateDate.MaxLength = 0;
			colvarPaymentRateDate.AutoIncrement = false;
			colvarPaymentRateDate.IsNullable = true;
			colvarPaymentRateDate.IsPrimaryKey = false;
			colvarPaymentRateDate.IsForeignKey = false;
			colvarPaymentRateDate.IsReadOnly = false;
                
			colvarPaymentRateDate.DefaultSetting = @"";
			colvarPaymentRateDate.ForeignKeyTableName = "";
            colvarPaymentRateDate.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentRateDate);
			
		}
		#endregion
	}
}