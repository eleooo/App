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
	/// This is an ActiveRecord class which wraps the Orders_Log table.
	/// </summary>
	[Serializable]
	public partial class OrdersLogSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new OrdersLogSchema();
            }
        }
		#region .ctors and Default Settings
		
		public OrdersLogSchema()
            :base("Orders_Log")
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
			
			TableSchema.TableColumn colvarDateX = new TableSchema.TableColumn(this);
			colvarDateX.ColumnName = "Date";
			colvarDateX.DataType = DbType.DateTime;
			colvarDateX.MaxLength = 0;
			colvarDateX.AutoIncrement = false;
			colvarDateX.IsNullable = true;
			colvarDateX.IsPrimaryKey = false;
			colvarDateX.IsForeignKey = false;
			colvarDateX.IsReadOnly = false;
                
			colvarDateX.DefaultSetting = @"";
			colvarDateX.ForeignKeyTableName = "";
            colvarDateX.ApplyExtendedProperties();
			this.Columns.Add(colvarDateX);
			
			TableSchema.TableColumn colvarDesc = new TableSchema.TableColumn(this);
			colvarDesc.ColumnName = "Desc";
			colvarDesc.DataType = DbType.String;
			colvarDesc.MaxLength = 2000;
			colvarDesc.AutoIncrement = false;
			colvarDesc.IsNullable = true;
			colvarDesc.IsPrimaryKey = false;
			colvarDesc.IsForeignKey = false;
			colvarDesc.IsReadOnly = false;
                
			colvarDesc.DefaultSetting = @"";
			colvarDesc.ForeignKeyTableName = "";
            colvarDesc.ApplyExtendedProperties();
			this.Columns.Add(colvarDesc);
			
			TableSchema.TableColumn colvarIsCurrent = new TableSchema.TableColumn(this);
			colvarIsCurrent.ColumnName = "IsCurrent";
			colvarIsCurrent.DataType = DbType.Int32;
			colvarIsCurrent.MaxLength = 0;
			colvarIsCurrent.AutoIncrement = false;
			colvarIsCurrent.IsNullable = true;
			colvarIsCurrent.IsPrimaryKey = false;
			colvarIsCurrent.IsForeignKey = false;
			colvarIsCurrent.IsReadOnly = false;
                
			colvarIsCurrent.DefaultSetting = @"";
			colvarIsCurrent.ForeignKeyTableName = "";
            colvarIsCurrent.ApplyExtendedProperties();
			this.Columns.Add(colvarIsCurrent);
			
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
			
			TableSchema.TableColumn colvarVoice = new TableSchema.TableColumn(this);
			colvarVoice.ColumnName = "Voice";
			colvarVoice.DataType = DbType.String;
			colvarVoice.MaxLength = 500;
			colvarVoice.AutoIncrement = false;
			colvarVoice.IsNullable = true;
			colvarVoice.IsPrimaryKey = false;
			colvarVoice.IsForeignKey = false;
			colvarVoice.IsReadOnly = false;
                
			colvarVoice.DefaultSetting = @"";
			colvarVoice.ForeignKeyTableName = "";
            colvarVoice.ApplyExtendedProperties();
			this.Columns.Add(colvarVoice);
			
			TableSchema.TableColumn colvarIsPlay = new TableSchema.TableColumn(this);
			colvarIsPlay.ColumnName = "IsPlay";
			colvarIsPlay.DataType = DbType.Boolean;
			colvarIsPlay.MaxLength = 0;
			colvarIsPlay.AutoIncrement = false;
			colvarIsPlay.IsNullable = true;
			colvarIsPlay.IsPrimaryKey = false;
			colvarIsPlay.IsForeignKey = false;
			colvarIsPlay.IsReadOnly = false;
                
			colvarIsPlay.DefaultSetting = @"";
			colvarIsPlay.ForeignKeyTableName = "";
            colvarIsPlay.ApplyExtendedProperties();
			this.Columns.Add(colvarIsPlay);
			
			TableSchema.TableColumn colvarFromUser = new TableSchema.TableColumn(this);
			colvarFromUser.ColumnName = "FromUser";
			colvarFromUser.DataType = DbType.Int32;
			colvarFromUser.MaxLength = 0;
			colvarFromUser.AutoIncrement = false;
			colvarFromUser.IsNullable = true;
			colvarFromUser.IsPrimaryKey = false;
			colvarFromUser.IsForeignKey = false;
			colvarFromUser.IsReadOnly = false;
                
			colvarFromUser.DefaultSetting = @"";
			colvarFromUser.ForeignKeyTableName = "";
            colvarFromUser.ApplyExtendedProperties();
			this.Columns.Add(colvarFromUser);
			
			TableSchema.TableColumn colvarToUser = new TableSchema.TableColumn(this);
			colvarToUser.ColumnName = "ToUser";
			colvarToUser.DataType = DbType.Int32;
			colvarToUser.MaxLength = 0;
			colvarToUser.AutoIncrement = false;
			colvarToUser.IsNullable = true;
			colvarToUser.IsPrimaryKey = false;
			colvarToUser.IsForeignKey = false;
			colvarToUser.IsReadOnly = false;
                
			colvarToUser.DefaultSetting = @"";
			colvarToUser.ForeignKeyTableName = "";
            colvarToUser.ApplyExtendedProperties();
			this.Columns.Add(colvarToUser);
			
		}
		#endregion
	}
}