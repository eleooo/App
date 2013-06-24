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
	/// This is an ActiveRecord class which wraps the v_FinancePayCash_LastDate table.
	/// </summary>
	[Serializable]
	public partial class VFinancePayCashLastDateSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VFinancePayCashLastDateSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VFinancePayCashLastDateSchema()
            :base("v_FinancePayCash_LastDate")
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
			
		}
		#endregion
	}
}