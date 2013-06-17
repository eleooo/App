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
 //Generated on 2013/6/17 22:55:47 by Administrator
// <auto-generated />
namespace Eleooo.DAL
{
	/// <summary>
	/// This is an ActiveRecord class which wraps the v_Sys_Member_Ads_Outer table.
	/// </summary>
	[Serializable]
	public partial class VSysMemberAdsOuterSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VSysMemberAdsOuterSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VSysMemberAdsOuterSchema()
            :base("v_Sys_Member_Ads_Outer")
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
			
			TableSchema.TableColumn colvarAdsID = new TableSchema.TableColumn(this);
			colvarAdsID.ColumnName = "AdsID";
			colvarAdsID.DataType = DbType.Int32;
			colvarAdsID.MaxLength = 0;
			colvarAdsID.AutoIncrement = false;
			colvarAdsID.IsNullable = false;
			colvarAdsID.IsPrimaryKey = false;
			colvarAdsID.IsForeignKey = false;
			colvarAdsID.IsReadOnly = false;
                
			colvarAdsID.DefaultSetting = @"";
			colvarAdsID.ForeignKeyTableName = "";
            colvarAdsID.ApplyExtendedProperties();
			this.Columns.Add(colvarAdsID);
			
			TableSchema.TableColumn colvarAdsMemberID = new TableSchema.TableColumn(this);
			colvarAdsMemberID.ColumnName = "AdsMemberID";
			colvarAdsMemberID.DataType = DbType.Int32;
			colvarAdsMemberID.MaxLength = 0;
			colvarAdsMemberID.AutoIncrement = false;
			colvarAdsMemberID.IsNullable = true;
			colvarAdsMemberID.IsPrimaryKey = false;
			colvarAdsMemberID.IsForeignKey = false;
			colvarAdsMemberID.IsReadOnly = false;
                
			colvarAdsMemberID.DefaultSetting = @"";
			colvarAdsMemberID.ForeignKeyTableName = "";
            colvarAdsMemberID.ApplyExtendedProperties();
			this.Columns.Add(colvarAdsMemberID);
			
			TableSchema.TableColumn colvarCompanyAdsID = new TableSchema.TableColumn(this);
			colvarCompanyAdsID.ColumnName = "CompanyAdsID";
			colvarCompanyAdsID.DataType = DbType.Int32;
			colvarCompanyAdsID.MaxLength = 0;
			colvarCompanyAdsID.AutoIncrement = false;
			colvarCompanyAdsID.IsNullable = true;
			colvarCompanyAdsID.IsPrimaryKey = false;
			colvarCompanyAdsID.IsForeignKey = false;
			colvarCompanyAdsID.IsReadOnly = false;
                
			colvarCompanyAdsID.DefaultSetting = @"";
			colvarCompanyAdsID.ForeignKeyTableName = "";
            colvarCompanyAdsID.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyAdsID);
			
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
			
			TableSchema.TableColumn colvarAdsDate = new TableSchema.TableColumn(this);
			colvarAdsDate.ColumnName = "AdsDate";
			colvarAdsDate.DataType = DbType.DateTime;
			colvarAdsDate.MaxLength = 0;
			colvarAdsDate.AutoIncrement = false;
			colvarAdsDate.IsNullable = true;
			colvarAdsDate.IsPrimaryKey = false;
			colvarAdsDate.IsForeignKey = false;
			colvarAdsDate.IsReadOnly = false;
                
			colvarAdsDate.DefaultSetting = @"";
			colvarAdsDate.ForeignKeyTableName = "";
            colvarAdsDate.ApplyExtendedProperties();
			this.Columns.Add(colvarAdsDate);
			
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