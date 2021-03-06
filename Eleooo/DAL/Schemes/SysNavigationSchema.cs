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
	/// This is an ActiveRecord class which wraps the Sys_Navigation table.
	/// </summary>
	[Serializable]
	public partial class SysNavigationSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysNavigationSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysNavigationSchema()
            :base("Sys_Navigation")
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
                
			
            colvarId.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","导航ID"));
            colvarId.DefaultSetting = @"";
			colvarId.ForeignKeyTableName = "";
            colvarId.ApplyExtendedProperties();
			this.Columns.Add(colvarId);
			
			TableSchema.TableColumn colvarNavName = new TableSchema.TableColumn(this);
			colvarNavName.ColumnName = "NavName";
			colvarNavName.DataType = DbType.String;
			colvarNavName.MaxLength = 50;
			colvarNavName.AutoIncrement = false;
			colvarNavName.IsNullable = true;
			colvarNavName.IsPrimaryKey = false;
			colvarNavName.IsForeignKey = false;
			colvarNavName.IsReadOnly = false;
                
			
            colvarNavName.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","导航名称"));
            colvarNavName.DefaultSetting = @"";
			colvarNavName.ForeignKeyTableName = "";
            colvarNavName.ApplyExtendedProperties();
			this.Columns.Add(colvarNavName);
			
			TableSchema.TableColumn colvarSecName = new TableSchema.TableColumn(this);
			colvarSecName.ColumnName = "SecName";
			colvarSecName.DataType = DbType.String;
			colvarSecName.MaxLength = 50;
			colvarSecName.AutoIncrement = false;
			colvarSecName.IsNullable = true;
			colvarSecName.IsPrimaryKey = false;
			colvarSecName.IsForeignKey = false;
			colvarSecName.IsReadOnly = false;
                
			
            colvarSecName.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","显示区块"));
            colvarSecName.DefaultSetting = @"";
			colvarSecName.ForeignKeyTableName = "";
            colvarSecName.ApplyExtendedProperties();
			this.Columns.Add(colvarSecName);
			
			TableSchema.TableColumn colvarOthName = new TableSchema.TableColumn(this);
			colvarOthName.ColumnName = "OthName";
			colvarOthName.DataType = DbType.String;
			colvarOthName.MaxLength = 50;
			colvarOthName.AutoIncrement = false;
			colvarOthName.IsNullable = true;
			colvarOthName.IsPrimaryKey = false;
			colvarOthName.IsForeignKey = false;
			colvarOthName.IsReadOnly = false;
                
			colvarOthName.DefaultSetting = @"";
			colvarOthName.ForeignKeyTableName = "";
            colvarOthName.ApplyExtendedProperties();
			this.Columns.Add(colvarOthName);
			
			TableSchema.TableColumn colvarNavUrl = new TableSchema.TableColumn(this);
			colvarNavUrl.ColumnName = "NavUrl";
			colvarNavUrl.DataType = DbType.String;
			colvarNavUrl.MaxLength = 250;
			colvarNavUrl.AutoIncrement = false;
			colvarNavUrl.IsNullable = true;
			colvarNavUrl.IsPrimaryKey = false;
			colvarNavUrl.IsForeignKey = false;
			colvarNavUrl.IsReadOnly = false;
                
			
            colvarNavUrl.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","导航URL"));
            colvarNavUrl.DefaultSetting = @"";
			colvarNavUrl.ForeignKeyTableName = "";
            colvarNavUrl.ApplyExtendedProperties();
			this.Columns.Add(colvarNavUrl);
			
			TableSchema.TableColumn colvarNavIcon = new TableSchema.TableColumn(this);
			colvarNavIcon.ColumnName = "NavIcon";
			colvarNavIcon.DataType = DbType.String;
			colvarNavIcon.MaxLength = 50;
			colvarNavIcon.AutoIncrement = false;
			colvarNavIcon.IsNullable = true;
			colvarNavIcon.IsPrimaryKey = false;
			colvarNavIcon.IsForeignKey = false;
			colvarNavIcon.IsReadOnly = false;
                
			colvarNavIcon.DefaultSetting = @"";
			colvarNavIcon.ForeignKeyTableName = "";
            colvarNavIcon.ApplyExtendedProperties();
			this.Columns.Add(colvarNavIcon);
			
			TableSchema.TableColumn colvarSubSysId = new TableSchema.TableColumn(this);
			colvarSubSysId.ColumnName = "SubSys_ID";
			colvarSubSysId.DataType = DbType.Int32;
			colvarSubSysId.MaxLength = 0;
			colvarSubSysId.AutoIncrement = false;
			colvarSubSysId.IsNullable = true;
			colvarSubSysId.IsPrimaryKey = false;
			colvarSubSysId.IsForeignKey = false;
			colvarSubSysId.IsReadOnly = false;
                
			
            colvarSubSysId.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","系统ID"));
            colvarSubSysId.DefaultSetting = @"";
			colvarSubSysId.ForeignKeyTableName = "";
            colvarSubSysId.ApplyExtendedProperties();
			this.Columns.Add(colvarSubSysId);
			
			TableSchema.TableColumn colvarPId = new TableSchema.TableColumn(this);
			colvarPId.ColumnName = "P_ID";
			colvarPId.DataType = DbType.Int32;
			colvarPId.MaxLength = 0;
			colvarPId.AutoIncrement = false;
			colvarPId.IsNullable = true;
			colvarPId.IsPrimaryKey = false;
			colvarPId.IsForeignKey = false;
			colvarPId.IsReadOnly = false;
                
			
            colvarPId.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","父导航ID"));
            
			colvarPId.DefaultSetting = @"((0))";
			colvarPId.ForeignKeyTableName = "";
            colvarPId.ApplyExtendedProperties();
			this.Columns.Add(colvarPId);
			
			TableSchema.TableColumn colvarIsMainNav = new TableSchema.TableColumn(this);
			colvarIsMainNav.ColumnName = "IsMainNav";
			colvarIsMainNav.DataType = DbType.Boolean;
			colvarIsMainNav.MaxLength = 0;
			colvarIsMainNav.AutoIncrement = false;
			colvarIsMainNav.IsNullable = false;
			colvarIsMainNav.IsPrimaryKey = false;
			colvarIsMainNav.IsForeignKey = false;
			colvarIsMainNav.IsReadOnly = false;
                
			
			colvarIsMainNav.DefaultSetting = @"((0))";
			colvarIsMainNav.ForeignKeyTableName = "";
            colvarIsMainNav.ApplyExtendedProperties();
			this.Columns.Add(colvarIsMainNav);
			
			TableSchema.TableColumn colvarIsHeader = new TableSchema.TableColumn(this);
			colvarIsHeader.ColumnName = "IsHeader";
			colvarIsHeader.DataType = DbType.Boolean;
			colvarIsHeader.MaxLength = 0;
			colvarIsHeader.AutoIncrement = false;
			colvarIsHeader.IsNullable = false;
			colvarIsHeader.IsPrimaryKey = false;
			colvarIsHeader.IsForeignKey = false;
			colvarIsHeader.IsReadOnly = false;
                
			
			colvarIsHeader.DefaultSetting = @"((0))";
			colvarIsHeader.ForeignKeyTableName = "";
            colvarIsHeader.ApplyExtendedProperties();
			this.Columns.Add(colvarIsHeader);
			
			TableSchema.TableColumn colvarIsFooter = new TableSchema.TableColumn(this);
			colvarIsFooter.ColumnName = "IsFooter";
			colvarIsFooter.DataType = DbType.Boolean;
			colvarIsFooter.MaxLength = 0;
			colvarIsFooter.AutoIncrement = false;
			colvarIsFooter.IsNullable = false;
			colvarIsFooter.IsPrimaryKey = false;
			colvarIsFooter.IsForeignKey = false;
			colvarIsFooter.IsReadOnly = false;
                
			
			colvarIsFooter.DefaultSetting = @"((0))";
			colvarIsFooter.ForeignKeyTableName = "";
            colvarIsFooter.ApplyExtendedProperties();
			this.Columns.Add(colvarIsFooter);
			
			TableSchema.TableColumn colvarPermissionRequired = new TableSchema.TableColumn(this);
			colvarPermissionRequired.ColumnName = "PermissionRequired";
			colvarPermissionRequired.DataType = DbType.Boolean;
			colvarPermissionRequired.MaxLength = 0;
			colvarPermissionRequired.AutoIncrement = false;
			colvarPermissionRequired.IsNullable = false;
			colvarPermissionRequired.IsPrimaryKey = false;
			colvarPermissionRequired.IsForeignKey = false;
			colvarPermissionRequired.IsReadOnly = false;
                
			
            colvarPermissionRequired.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","匿名可见"));
            
			colvarPermissionRequired.DefaultSetting = @"((1))";
			colvarPermissionRequired.ForeignKeyTableName = "";
            colvarPermissionRequired.ApplyExtendedProperties();
			this.Columns.Add(colvarPermissionRequired);
			
			TableSchema.TableColumn colvarSort = new TableSchema.TableColumn(this);
			colvarSort.ColumnName = "Sort";
			colvarSort.DataType = DbType.Int32;
			colvarSort.MaxLength = 0;
			colvarSort.AutoIncrement = false;
			colvarSort.IsNullable = false;
			colvarSort.IsPrimaryKey = false;
			colvarSort.IsForeignKey = false;
			colvarSort.IsReadOnly = false;
                
			
			colvarSort.DefaultSetting = @"((0))";
			colvarSort.ForeignKeyTableName = "";
            colvarSort.ApplyExtendedProperties();
			this.Columns.Add(colvarSort);
			
			TableSchema.TableColumn colvarVisible = new TableSchema.TableColumn(this);
			colvarVisible.ColumnName = "Visible";
			colvarVisible.DataType = DbType.Boolean;
			colvarVisible.MaxLength = 0;
			colvarVisible.AutoIncrement = false;
			colvarVisible.IsNullable = false;
			colvarVisible.IsPrimaryKey = false;
			colvarVisible.IsForeignKey = false;
			colvarVisible.IsReadOnly = false;
                
			
			colvarVisible.DefaultSetting = @"((1))";
			colvarVisible.ForeignKeyTableName = "";
            colvarVisible.ApplyExtendedProperties();
			this.Columns.Add(colvarVisible);
			
			TableSchema.TableColumn colvarDepth = new TableSchema.TableColumn(this);
			colvarDepth.ColumnName = "Depth";
			colvarDepth.DataType = DbType.String;
			colvarDepth.MaxLength = 250;
			colvarDepth.AutoIncrement = false;
			colvarDepth.IsNullable = true;
			colvarDepth.IsPrimaryKey = false;
			colvarDepth.IsForeignKey = false;
			colvarDepth.IsReadOnly = false;
                
			colvarDepth.DefaultSetting = @"";
			colvarDepth.ForeignKeyTableName = "";
            colvarDepth.ApplyExtendedProperties();
			this.Columns.Add(colvarDepth);
			
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