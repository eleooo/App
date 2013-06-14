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
	/// This is an ActiveRecord class which wraps the Sys_Company_Member_Grade table.
	/// </summary>
	[Serializable]
	public partial class SysCompanyMemberGradeSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysCompanyMemberGradeSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysCompanyMemberGradeSchema()
            :base("Sys_Company_Member_Grade")
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
			
			TableSchema.TableColumn colvarGradeName = new TableSchema.TableColumn(this);
			colvarGradeName.ColumnName = "GradeName";
			colvarGradeName.DataType = DbType.String;
			colvarGradeName.MaxLength = 150;
			colvarGradeName.AutoIncrement = false;
			colvarGradeName.IsNullable = true;
			colvarGradeName.IsPrimaryKey = false;
			colvarGradeName.IsForeignKey = false;
			colvarGradeName.IsReadOnly = false;
                
			colvarGradeName.DefaultSetting = @"";
			colvarGradeName.ForeignKeyTableName = "";
            colvarGradeName.ApplyExtendedProperties();
			this.Columns.Add(colvarGradeName);
			
			TableSchema.TableColumn colvarGradeOrder = new TableSchema.TableColumn(this);
			colvarGradeOrder.ColumnName = "GradeOrder";
			colvarGradeOrder.DataType = DbType.Int32;
			colvarGradeOrder.MaxLength = 0;
			colvarGradeOrder.AutoIncrement = false;
			colvarGradeOrder.IsNullable = true;
			colvarGradeOrder.IsPrimaryKey = false;
			colvarGradeOrder.IsForeignKey = false;
			colvarGradeOrder.IsReadOnly = false;
                
			
			colvarGradeOrder.DefaultSetting = @"((0))";
			colvarGradeOrder.ForeignKeyTableName = "";
            colvarGradeOrder.ApplyExtendedProperties();
			this.Columns.Add(colvarGradeOrder);
			
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
			
			TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(this);
			colvarCreatedBy.ColumnName = "CreatedBy";
			colvarCreatedBy.DataType = DbType.Int32;
			colvarCreatedBy.MaxLength = 0;
			colvarCreatedBy.AutoIncrement = false;
			colvarCreatedBy.IsNullable = true;
			colvarCreatedBy.IsPrimaryKey = false;
			colvarCreatedBy.IsForeignKey = false;
			colvarCreatedBy.IsReadOnly = false;
                
			colvarCreatedBy.DefaultSetting = @"";
			colvarCreatedBy.ForeignKeyTableName = "";
            colvarCreatedBy.ApplyExtendedProperties();
			this.Columns.Add(colvarCreatedBy);
			
			TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(this);
			colvarCreatedOn.ColumnName = "CreatedOn";
			colvarCreatedOn.DataType = DbType.DateTime;
			colvarCreatedOn.MaxLength = 0;
			colvarCreatedOn.AutoIncrement = false;
			colvarCreatedOn.IsNullable = true;
			colvarCreatedOn.IsPrimaryKey = false;
			colvarCreatedOn.IsForeignKey = false;
			colvarCreatedOn.IsReadOnly = false;
                
			colvarCreatedOn.DefaultSetting = @"";
			colvarCreatedOn.ForeignKeyTableName = "";
            colvarCreatedOn.ApplyExtendedProperties();
			this.Columns.Add(colvarCreatedOn);
			
			TableSchema.TableColumn colvarModifiedBy = new TableSchema.TableColumn(this);
			colvarModifiedBy.ColumnName = "ModifiedBy";
			colvarModifiedBy.DataType = DbType.Int32;
			colvarModifiedBy.MaxLength = 0;
			colvarModifiedBy.AutoIncrement = false;
			colvarModifiedBy.IsNullable = true;
			colvarModifiedBy.IsPrimaryKey = false;
			colvarModifiedBy.IsForeignKey = false;
			colvarModifiedBy.IsReadOnly = false;
                
			colvarModifiedBy.DefaultSetting = @"";
			colvarModifiedBy.ForeignKeyTableName = "";
            colvarModifiedBy.ApplyExtendedProperties();
			this.Columns.Add(colvarModifiedBy);
			
			TableSchema.TableColumn colvarModifiedOn = new TableSchema.TableColumn(this);
			colvarModifiedOn.ColumnName = "ModifiedOn";
			colvarModifiedOn.DataType = DbType.DateTime;
			colvarModifiedOn.MaxLength = 0;
			colvarModifiedOn.AutoIncrement = false;
			colvarModifiedOn.IsNullable = true;
			colvarModifiedOn.IsPrimaryKey = false;
			colvarModifiedOn.IsForeignKey = false;
			colvarModifiedOn.IsReadOnly = false;
                
			colvarModifiedOn.DefaultSetting = @"";
			colvarModifiedOn.ForeignKeyTableName = "";
            colvarModifiedOn.ApplyExtendedProperties();
			this.Columns.Add(colvarModifiedOn);
			
		}
		#endregion
	}
}