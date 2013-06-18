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
	/// This is an ActiveRecord class which wraps the v_Company_MemberCount table.
	/// </summary>
	[Serializable]
	public partial class VCompanyMemberCountSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new VCompanyMemberCountSchema();
            }
        }
		#region .ctors and Default Settings
		
		public VCompanyMemberCountSchema()
            :base("v_Company_MemberCount")
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
			
			TableSchema.TableColumn colvarMemberCount = new TableSchema.TableColumn(this);
			colvarMemberCount.ColumnName = "MemberCount";
			colvarMemberCount.DataType = DbType.Int32;
			colvarMemberCount.MaxLength = 0;
			colvarMemberCount.AutoIncrement = false;
			colvarMemberCount.IsNullable = true;
			colvarMemberCount.IsPrimaryKey = false;
			colvarMemberCount.IsForeignKey = false;
			colvarMemberCount.IsReadOnly = false;
                
			colvarMemberCount.DefaultSetting = @"";
			colvarMemberCount.ForeignKeyTableName = "";
            colvarMemberCount.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberCount);
			
			TableSchema.TableColumn colvarThisMonthMemberCount = new TableSchema.TableColumn(this);
			colvarThisMonthMemberCount.ColumnName = "ThisMonthMemberCount";
			colvarThisMonthMemberCount.DataType = DbType.Int32;
			colvarThisMonthMemberCount.MaxLength = 0;
			colvarThisMonthMemberCount.AutoIncrement = false;
			colvarThisMonthMemberCount.IsNullable = true;
			colvarThisMonthMemberCount.IsPrimaryKey = false;
			colvarThisMonthMemberCount.IsForeignKey = false;
			colvarThisMonthMemberCount.IsReadOnly = false;
                
			colvarThisMonthMemberCount.DefaultSetting = @"";
			colvarThisMonthMemberCount.ForeignKeyTableName = "";
            colvarThisMonthMemberCount.ApplyExtendedProperties();
			this.Columns.Add(colvarThisMonthMemberCount);
			
		}
		#endregion
	}
}