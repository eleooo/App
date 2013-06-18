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
	/// This is an ActiveRecord class which wraps the v_PaymentSelf table.
	/// </summary>
	[Serializable]
	public partial class VPaymentSelfSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VPaymentSelfSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VPaymentSelfSchema()
            :base("v_PaymentSelf")
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
			
			TableSchema.TableColumn colvarPaymentCompanyID = new TableSchema.TableColumn(this);
			colvarPaymentCompanyID.ColumnName = "PaymentCompanyID";
			colvarPaymentCompanyID.DataType = DbType.Int32;
			colvarPaymentCompanyID.MaxLength = 0;
			colvarPaymentCompanyID.AutoIncrement = false;
			colvarPaymentCompanyID.IsNullable = true;
			colvarPaymentCompanyID.IsPrimaryKey = false;
			colvarPaymentCompanyID.IsForeignKey = false;
			colvarPaymentCompanyID.IsReadOnly = false;
                
			colvarPaymentCompanyID.DefaultSetting = @"";
			colvarPaymentCompanyID.ForeignKeyTableName = "";
            colvarPaymentCompanyID.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentCompanyID);
			
			TableSchema.TableColumn colvarPaymentMemberID = new TableSchema.TableColumn(this);
			colvarPaymentMemberID.ColumnName = "PaymentMemberID";
			colvarPaymentMemberID.DataType = DbType.Int32;
			colvarPaymentMemberID.MaxLength = 0;
			colvarPaymentMemberID.AutoIncrement = false;
			colvarPaymentMemberID.IsNullable = true;
			colvarPaymentMemberID.IsPrimaryKey = false;
			colvarPaymentMemberID.IsForeignKey = false;
			colvarPaymentMemberID.IsReadOnly = false;
                
			colvarPaymentMemberID.DefaultSetting = @"";
			colvarPaymentMemberID.ForeignKeyTableName = "";
            colvarPaymentMemberID.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentMemberID);
			
			TableSchema.TableColumn colvarPaymentDate = new TableSchema.TableColumn(this);
			colvarPaymentDate.ColumnName = "PaymentDate";
			colvarPaymentDate.DataType = DbType.DateTime;
			colvarPaymentDate.MaxLength = 0;
			colvarPaymentDate.AutoIncrement = false;
			colvarPaymentDate.IsNullable = true;
			colvarPaymentDate.IsPrimaryKey = false;
			colvarPaymentDate.IsForeignKey = false;
			colvarPaymentDate.IsReadOnly = false;
                
			colvarPaymentDate.DefaultSetting = @"";
			colvarPaymentDate.ForeignKeyTableName = "";
            colvarPaymentDate.ApplyExtendedProperties();
			this.Columns.Add(colvarPaymentDate);
			
		}
		#endregion
	}
}