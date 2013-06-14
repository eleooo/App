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
	/// This is an ActiveRecord class which wraps the Sys_Area_Mansion table.
	/// </summary>
	[Serializable]
	public partial class SysAreaMansionSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysAreaMansionSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysAreaMansionSchema()
            :base("Sys_Area_Mansion")
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
			
			TableSchema.TableColumn colvarAreaID = new TableSchema.TableColumn(this);
			colvarAreaID.ColumnName = "AreaID";
			colvarAreaID.DataType = DbType.Int32;
			colvarAreaID.MaxLength = 0;
			colvarAreaID.AutoIncrement = false;
			colvarAreaID.IsNullable = true;
			colvarAreaID.IsPrimaryKey = false;
			colvarAreaID.IsForeignKey = false;
			colvarAreaID.IsReadOnly = false;
                
			colvarAreaID.DefaultSetting = @"";
			colvarAreaID.ForeignKeyTableName = "";
            colvarAreaID.ApplyExtendedProperties();
			this.Columns.Add(colvarAreaID);
			
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
			
			TableSchema.TableColumn colvarAreaDepth = new TableSchema.TableColumn(this);
			colvarAreaDepth.ColumnName = "AreaDepth";
			colvarAreaDepth.DataType = DbType.String;
			colvarAreaDepth.MaxLength = 50;
			colvarAreaDepth.AutoIncrement = false;
			colvarAreaDepth.IsNullable = true;
			colvarAreaDepth.IsPrimaryKey = false;
			colvarAreaDepth.IsForeignKey = false;
			colvarAreaDepth.IsReadOnly = false;
                
			colvarAreaDepth.DefaultSetting = @"";
			colvarAreaDepth.ForeignKeyTableName = "";
            colvarAreaDepth.ApplyExtendedProperties();
			this.Columns.Add(colvarAreaDepth);
			
			TableSchema.TableColumn colvarCode = new TableSchema.TableColumn(this);
			colvarCode.ColumnName = "Code";
			colvarCode.DataType = DbType.String;
			colvarCode.MaxLength = 50;
			colvarCode.AutoIncrement = false;
			colvarCode.IsNullable = true;
			colvarCode.IsPrimaryKey = false;
			colvarCode.IsForeignKey = false;
			colvarCode.IsReadOnly = false;
                
			colvarCode.DefaultSetting = @"";
			colvarCode.ForeignKeyTableName = "";
            colvarCode.ApplyExtendedProperties();
			this.Columns.Add(colvarCode);
			
			TableSchema.TableColumn colvarAddress = new TableSchema.TableColumn(this);
			colvarAddress.ColumnName = "Address";
			colvarAddress.DataType = DbType.String;
			colvarAddress.MaxLength = 250;
			colvarAddress.AutoIncrement = false;
			colvarAddress.IsNullable = true;
			colvarAddress.IsPrimaryKey = false;
			colvarAddress.IsForeignKey = false;
			colvarAddress.IsReadOnly = false;
                
			colvarAddress.DefaultSetting = @"";
			colvarAddress.ForeignKeyTableName = "";
            colvarAddress.ApplyExtendedProperties();
			this.Columns.Add(colvarAddress);
			
		}
		#endregion
	}
}