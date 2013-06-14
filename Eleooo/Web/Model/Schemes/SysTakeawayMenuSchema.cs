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
	/// This is an ActiveRecord class which wraps the Sys_Takeaway_Menu table.
	/// </summary>
	[Serializable]
	public partial class SysTakeawayMenuSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysTakeawayMenuSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysTakeawayMenuSchema()
            :base("Sys_Takeaway_Menu")
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
			
			TableSchema.TableColumn colvarName = new TableSchema.TableColumn(this);
			colvarName.ColumnName = "Name";
			colvarName.DataType = DbType.String;
			colvarName.MaxLength = 250;
			colvarName.AutoIncrement = false;
			colvarName.IsNullable = true;
			colvarName.IsPrimaryKey = false;
			colvarName.IsForeignKey = false;
			colvarName.IsReadOnly = false;
                
			colvarName.DefaultSetting = @"";
			colvarName.ForeignKeyTableName = "";
            colvarName.ApplyExtendedProperties();
			this.Columns.Add(colvarName);
			
			TableSchema.TableColumn colvarPrice = new TableSchema.TableColumn(this);
			colvarPrice.ColumnName = "Price";
			colvarPrice.DataType = DbType.Decimal;
			colvarPrice.MaxLength = 0;
			colvarPrice.AutoIncrement = false;
			colvarPrice.IsNullable = true;
			colvarPrice.IsPrimaryKey = false;
			colvarPrice.IsForeignKey = false;
			colvarPrice.IsReadOnly = false;
                
			colvarPrice.DefaultSetting = @"";
			colvarPrice.ForeignKeyTableName = "";
            colvarPrice.ApplyExtendedProperties();
			this.Columns.Add(colvarPrice);
			
			TableSchema.TableColumn colvarDirID = new TableSchema.TableColumn(this);
			colvarDirID.ColumnName = "DirID";
			colvarDirID.DataType = DbType.Int32;
			colvarDirID.MaxLength = 0;
			colvarDirID.AutoIncrement = false;
			colvarDirID.IsNullable = true;
			colvarDirID.IsPrimaryKey = false;
			colvarDirID.IsForeignKey = false;
			colvarDirID.IsReadOnly = false;
                
			colvarDirID.DefaultSetting = @"";
			colvarDirID.ForeignKeyTableName = "";
            colvarDirID.ApplyExtendedProperties();
			this.Columns.Add(colvarDirID);
			
			TableSchema.TableColumn colvarCode = new TableSchema.TableColumn(this);
			colvarCode.ColumnName = "Code";
			colvarCode.DataType = DbType.String;
			colvarCode.MaxLength = 20;
			colvarCode.AutoIncrement = false;
			colvarCode.IsNullable = true;
			colvarCode.IsPrimaryKey = false;
			colvarCode.IsForeignKey = false;
			colvarCode.IsReadOnly = false;
                
			colvarCode.DefaultSetting = @"";
			colvarCode.ForeignKeyTableName = "";
            colvarCode.ApplyExtendedProperties();
			this.Columns.Add(colvarCode);
			
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
			
			TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(this);
			colvarIsDeleted.ColumnName = "IsDeleted";
			colvarIsDeleted.DataType = DbType.Boolean;
			colvarIsDeleted.MaxLength = 0;
			colvarIsDeleted.AutoIncrement = false;
			colvarIsDeleted.IsNullable = true;
			colvarIsDeleted.IsPrimaryKey = false;
			colvarIsDeleted.IsForeignKey = false;
			colvarIsDeleted.IsReadOnly = false;
                
			colvarIsDeleted.DefaultSetting = @"";
			colvarIsDeleted.ForeignKeyTableName = "";
            colvarIsDeleted.ApplyExtendedProperties();
			this.Columns.Add(colvarIsDeleted);
			
			TableSchema.TableColumn colvarIsOutOfStock = new TableSchema.TableColumn(this);
			colvarIsOutOfStock.ColumnName = "IsOutOfStock";
			colvarIsOutOfStock.DataType = DbType.Boolean;
			colvarIsOutOfStock.MaxLength = 0;
			colvarIsOutOfStock.AutoIncrement = false;
			colvarIsOutOfStock.IsNullable = true;
			colvarIsOutOfStock.IsPrimaryKey = false;
			colvarIsOutOfStock.IsForeignKey = false;
			colvarIsOutOfStock.IsReadOnly = false;
                
			colvarIsOutOfStock.DefaultSetting = @"";
			colvarIsOutOfStock.ForeignKeyTableName = "";
            colvarIsOutOfStock.ApplyExtendedProperties();
			this.Columns.Add(colvarIsOutOfStock);
			
			TableSchema.TableColumn colvarOutOfStockDate = new TableSchema.TableColumn(this);
			colvarOutOfStockDate.ColumnName = "OutOfStockDate";
			colvarOutOfStockDate.DataType = DbType.DateTime;
			colvarOutOfStockDate.MaxLength = 0;
			colvarOutOfStockDate.AutoIncrement = false;
			colvarOutOfStockDate.IsNullable = true;
			colvarOutOfStockDate.IsPrimaryKey = false;
			colvarOutOfStockDate.IsForeignKey = false;
			colvarOutOfStockDate.IsReadOnly = false;
                
			colvarOutOfStockDate.DefaultSetting = @"";
			colvarOutOfStockDate.ForeignKeyTableName = "";
            colvarOutOfStockDate.ApplyExtendedProperties();
			this.Columns.Add(colvarOutOfStockDate);
			
		}
		#endregion
	}
}