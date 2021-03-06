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
	/// This is an ActiveRecord class which wraps the v_Latest_Sys_Member_Item table.
	/// </summary>
	[Serializable]
	public partial class VLatestSysMemberItemSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VLatestSysMemberItemSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VLatestSysMemberItemSchema()
            :base("v_Latest_Sys_Member_Item")
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
			
			TableSchema.TableColumn colvarItemID = new TableSchema.TableColumn(this);
			colvarItemID.ColumnName = "ItemID";
			colvarItemID.DataType = DbType.Int32;
			colvarItemID.MaxLength = 0;
			colvarItemID.AutoIncrement = false;
			colvarItemID.IsNullable = false;
			colvarItemID.IsPrimaryKey = false;
			colvarItemID.IsForeignKey = false;
			colvarItemID.IsReadOnly = false;
                
			colvarItemID.DefaultSetting = @"";
			colvarItemID.ForeignKeyTableName = "";
            colvarItemID.ApplyExtendedProperties();
			this.Columns.Add(colvarItemID);
			
			TableSchema.TableColumn colvarCompanyItemID = new TableSchema.TableColumn(this);
			colvarCompanyItemID.ColumnName = "CompanyItemID";
			colvarCompanyItemID.DataType = DbType.Int32;
			colvarCompanyItemID.MaxLength = 0;
			colvarCompanyItemID.AutoIncrement = false;
			colvarCompanyItemID.IsNullable = false;
			colvarCompanyItemID.IsPrimaryKey = false;
			colvarCompanyItemID.IsForeignKey = false;
			colvarCompanyItemID.IsReadOnly = false;
                
			colvarCompanyItemID.DefaultSetting = @"";
			colvarCompanyItemID.ForeignKeyTableName = "";
            colvarCompanyItemID.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyItemID);
			
			TableSchema.TableColumn colvarMemberID = new TableSchema.TableColumn(this);
			colvarMemberID.ColumnName = "MemberID";
			colvarMemberID.DataType = DbType.Int32;
			colvarMemberID.MaxLength = 0;
			colvarMemberID.AutoIncrement = false;
			colvarMemberID.IsNullable = false;
			colvarMemberID.IsPrimaryKey = false;
			colvarMemberID.IsForeignKey = false;
			colvarMemberID.IsReadOnly = false;
                
			colvarMemberID.DefaultSetting = @"";
			colvarMemberID.ForeignKeyTableName = "";
            colvarMemberID.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberID);
			
			TableSchema.TableColumn colvarItemDate = new TableSchema.TableColumn(this);
			colvarItemDate.ColumnName = "ItemDate";
			colvarItemDate.DataType = DbType.DateTime;
			colvarItemDate.MaxLength = 0;
			colvarItemDate.AutoIncrement = false;
			colvarItemDate.IsNullable = false;
			colvarItemDate.IsPrimaryKey = false;
			colvarItemDate.IsForeignKey = false;
			colvarItemDate.IsReadOnly = false;
                
			colvarItemDate.DefaultSetting = @"";
			colvarItemDate.ForeignKeyTableName = "";
            colvarItemDate.ApplyExtendedProperties();
			this.Columns.Add(colvarItemDate);
			
			TableSchema.TableColumn colvarItemStatus = new TableSchema.TableColumn(this);
			colvarItemStatus.ColumnName = "ItemStatus";
			colvarItemStatus.DataType = DbType.Int32;
			colvarItemStatus.MaxLength = 0;
			colvarItemStatus.AutoIncrement = false;
			colvarItemStatus.IsNullable = false;
			colvarItemStatus.IsPrimaryKey = false;
			colvarItemStatus.IsForeignKey = false;
			colvarItemStatus.IsReadOnly = false;
                
			colvarItemStatus.DefaultSetting = @"";
			colvarItemStatus.ForeignKeyTableName = "";
            colvarItemStatus.ApplyExtendedProperties();
			this.Columns.Add(colvarItemStatus);
			
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
			
		}
		#endregion
	}
}