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
	/// This is an ActiveRecord class which wraps the Sys_Msn_Log table.
	/// </summary>
	[Serializable]
	public partial class SysMsnLogSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysMsnLogSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysMsnLogSchema()
            :base("Sys_Msn_Log")
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
			
			TableSchema.TableColumn colvarPhoneNumber = new TableSchema.TableColumn(this);
			colvarPhoneNumber.ColumnName = "PhoneNumber";
			colvarPhoneNumber.DataType = DbType.String;
			colvarPhoneNumber.MaxLength = 15;
			colvarPhoneNumber.AutoIncrement = false;
			colvarPhoneNumber.IsNullable = true;
			colvarPhoneNumber.IsPrimaryKey = false;
			colvarPhoneNumber.IsForeignKey = false;
			colvarPhoneNumber.IsReadOnly = false;
                
			colvarPhoneNumber.DefaultSetting = @"";
			colvarPhoneNumber.ForeignKeyTableName = "";
            colvarPhoneNumber.ApplyExtendedProperties();
			this.Columns.Add(colvarPhoneNumber);
			
			TableSchema.TableColumn colvarMsnContent = new TableSchema.TableColumn(this);
			colvarMsnContent.ColumnName = "MsnContent";
			colvarMsnContent.DataType = DbType.String;
			colvarMsnContent.MaxLength = 500;
			colvarMsnContent.AutoIncrement = false;
			colvarMsnContent.IsNullable = true;
			colvarMsnContent.IsPrimaryKey = false;
			colvarMsnContent.IsForeignKey = false;
			colvarMsnContent.IsReadOnly = false;
                
			colvarMsnContent.DefaultSetting = @"";
			colvarMsnContent.ForeignKeyTableName = "";
            colvarMsnContent.ApplyExtendedProperties();
			this.Columns.Add(colvarMsnContent);
			
			TableSchema.TableColumn colvarMsnDate = new TableSchema.TableColumn(this);
			colvarMsnDate.ColumnName = "MsnDate";
			colvarMsnDate.DataType = DbType.DateTime;
			colvarMsnDate.MaxLength = 0;
			colvarMsnDate.AutoIncrement = false;
			colvarMsnDate.IsNullable = true;
			colvarMsnDate.IsPrimaryKey = false;
			colvarMsnDate.IsForeignKey = false;
			colvarMsnDate.IsReadOnly = false;
                
			colvarMsnDate.DefaultSetting = @"";
			colvarMsnDate.ForeignKeyTableName = "";
            colvarMsnDate.ApplyExtendedProperties();
			this.Columns.Add(colvarMsnDate);
			
			TableSchema.TableColumn colvarMsnCode = new TableSchema.TableColumn(this);
			colvarMsnCode.ColumnName = "MsnCode";
			colvarMsnCode.DataType = DbType.String;
			colvarMsnCode.MaxLength = 50;
			colvarMsnCode.AutoIncrement = false;
			colvarMsnCode.IsNullable = true;
			colvarMsnCode.IsPrimaryKey = false;
			colvarMsnCode.IsForeignKey = false;
			colvarMsnCode.IsReadOnly = false;
                
			colvarMsnCode.DefaultSetting = @"";
			colvarMsnCode.ForeignKeyTableName = "";
            colvarMsnCode.ApplyExtendedProperties();
			this.Columns.Add(colvarMsnCode);
			
			TableSchema.TableColumn colvarIsChecked = new TableSchema.TableColumn(this);
			colvarIsChecked.ColumnName = "IsChecked";
			colvarIsChecked.DataType = DbType.Boolean;
			colvarIsChecked.MaxLength = 0;
			colvarIsChecked.AutoIncrement = false;
			colvarIsChecked.IsNullable = true;
			colvarIsChecked.IsPrimaryKey = false;
			colvarIsChecked.IsForeignKey = false;
			colvarIsChecked.IsReadOnly = false;
                
			colvarIsChecked.DefaultSetting = @"";
			colvarIsChecked.ForeignKeyTableName = "";
            colvarIsChecked.ApplyExtendedProperties();
			this.Columns.Add(colvarIsChecked);
			
			TableSchema.TableColumn colvarOrderId = new TableSchema.TableColumn(this);
			colvarOrderId.ColumnName = "OrderId";
			colvarOrderId.DataType = DbType.Int32;
			colvarOrderId.MaxLength = 0;
			colvarOrderId.AutoIncrement = false;
			colvarOrderId.IsNullable = true;
			colvarOrderId.IsPrimaryKey = false;
			colvarOrderId.IsForeignKey = false;
			colvarOrderId.IsReadOnly = false;
                
			colvarOrderId.DefaultSetting = @"";
			colvarOrderId.ForeignKeyTableName = "";
            colvarOrderId.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderId);
			
			TableSchema.TableColumn colvarStatus = new TableSchema.TableColumn(this);
			colvarStatus.ColumnName = "Status";
			colvarStatus.DataType = DbType.String;
			colvarStatus.MaxLength = 4000;
			colvarStatus.AutoIncrement = false;
			colvarStatus.IsNullable = true;
			colvarStatus.IsPrimaryKey = false;
			colvarStatus.IsForeignKey = false;
			colvarStatus.IsReadOnly = false;
                
			colvarStatus.DefaultSetting = @"";
			colvarStatus.ForeignKeyTableName = "";
            colvarStatus.ApplyExtendedProperties();
			this.Columns.Add(colvarStatus);
			
		}
		#endregion
	}
}