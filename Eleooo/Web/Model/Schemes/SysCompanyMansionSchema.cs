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
	/// This is an ActiveRecord class which wraps the Sys_Company_Mansion table.
	/// </summary>
	[Serializable]
	public partial class SysCompanyMansionSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysCompanyMansionSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysCompanyMansionSchema()
            :base("Sys_Company_Mansion")
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
			
			TableSchema.TableColumn colvarMansionID = new TableSchema.TableColumn(this);
			colvarMansionID.ColumnName = "MansionID";
			colvarMansionID.DataType = DbType.Int32;
			colvarMansionID.MaxLength = 0;
			colvarMansionID.AutoIncrement = false;
			colvarMansionID.IsNullable = true;
			colvarMansionID.IsPrimaryKey = false;
			colvarMansionID.IsForeignKey = false;
			colvarMansionID.IsReadOnly = false;
                
			colvarMansionID.DefaultSetting = @"";
			colvarMansionID.ForeignKeyTableName = "";
            colvarMansionID.ApplyExtendedProperties();
			this.Columns.Add(colvarMansionID);
			
		}
		#endregion
	}
}