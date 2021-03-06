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
	/// This is an ActiveRecord class which wraps the Sys_Company_FaceBook table.
	/// </summary>
	[Serializable]
	public partial class SysCompanyFaceBookSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysCompanyFaceBookSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysCompanyFaceBookSchema()
            :base("Sys_Company_FaceBook")
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
			
			TableSchema.TableColumn colvarFaceBookDate = new TableSchema.TableColumn(this);
			colvarFaceBookDate.ColumnName = "FaceBookDate";
			colvarFaceBookDate.DataType = DbType.DateTime;
			colvarFaceBookDate.MaxLength = 0;
			colvarFaceBookDate.AutoIncrement = false;
			colvarFaceBookDate.IsNullable = true;
			colvarFaceBookDate.IsPrimaryKey = false;
			colvarFaceBookDate.IsForeignKey = false;
			colvarFaceBookDate.IsReadOnly = false;
                
			colvarFaceBookDate.DefaultSetting = @"";
			colvarFaceBookDate.ForeignKeyTableName = "";
            colvarFaceBookDate.ApplyExtendedProperties();
			this.Columns.Add(colvarFaceBookDate);
			
			TableSchema.TableColumn colvarFaceBookMemo = new TableSchema.TableColumn(this);
			colvarFaceBookMemo.ColumnName = "FaceBookMemo";
			colvarFaceBookMemo.DataType = DbType.AnsiString;
			colvarFaceBookMemo.MaxLength = 2147483647;
			colvarFaceBookMemo.AutoIncrement = false;
			colvarFaceBookMemo.IsNullable = true;
			colvarFaceBookMemo.IsPrimaryKey = false;
			colvarFaceBookMemo.IsForeignKey = false;
			colvarFaceBookMemo.IsReadOnly = false;
                
			colvarFaceBookMemo.DefaultSetting = @"";
			colvarFaceBookMemo.ForeignKeyTableName = "";
            colvarFaceBookMemo.ApplyExtendedProperties();
			this.Columns.Add(colvarFaceBookMemo);
			
			TableSchema.TableColumn colvarFaceBookBizID = new TableSchema.TableColumn(this);
			colvarFaceBookBizID.ColumnName = "FaceBookBizID";
			colvarFaceBookBizID.DataType = DbType.Int32;
			colvarFaceBookBizID.MaxLength = 0;
			colvarFaceBookBizID.AutoIncrement = false;
			colvarFaceBookBizID.IsNullable = true;
			colvarFaceBookBizID.IsPrimaryKey = false;
			colvarFaceBookBizID.IsForeignKey = false;
			colvarFaceBookBizID.IsReadOnly = false;
                
			colvarFaceBookBizID.DefaultSetting = @"";
			colvarFaceBookBizID.ForeignKeyTableName = "";
            colvarFaceBookBizID.ApplyExtendedProperties();
			this.Columns.Add(colvarFaceBookBizID);
			
			TableSchema.TableColumn colvarFaceBookMemberID = new TableSchema.TableColumn(this);
			colvarFaceBookMemberID.ColumnName = "FaceBookMemberID";
			colvarFaceBookMemberID.DataType = DbType.Int32;
			colvarFaceBookMemberID.MaxLength = 0;
			colvarFaceBookMemberID.AutoIncrement = false;
			colvarFaceBookMemberID.IsNullable = true;
			colvarFaceBookMemberID.IsPrimaryKey = false;
			colvarFaceBookMemberID.IsForeignKey = false;
			colvarFaceBookMemberID.IsReadOnly = false;
                
			colvarFaceBookMemberID.DefaultSetting = @"";
			colvarFaceBookMemberID.ForeignKeyTableName = "";
            colvarFaceBookMemberID.ApplyExtendedProperties();
			this.Columns.Add(colvarFaceBookMemberID);
			
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
			
			TableSchema.TableColumn colvarFaceBookBizType = new TableSchema.TableColumn(this);
			colvarFaceBookBizType.ColumnName = "FaceBookBizType";
			colvarFaceBookBizType.DataType = DbType.Int32;
			colvarFaceBookBizType.MaxLength = 0;
			colvarFaceBookBizType.AutoIncrement = false;
			colvarFaceBookBizType.IsNullable = true;
			colvarFaceBookBizType.IsPrimaryKey = false;
			colvarFaceBookBizType.IsForeignKey = false;
			colvarFaceBookBizType.IsReadOnly = false;
                
			colvarFaceBookBizType.DefaultSetting = @"";
			colvarFaceBookBizType.ForeignKeyTableName = "";
            colvarFaceBookBizType.ApplyExtendedProperties();
			this.Columns.Add(colvarFaceBookBizType);
			
			TableSchema.TableColumn colvarReplyMemberID = new TableSchema.TableColumn(this);
			colvarReplyMemberID.ColumnName = "ReplyMemberID";
			colvarReplyMemberID.DataType = DbType.Int32;
			colvarReplyMemberID.MaxLength = 0;
			colvarReplyMemberID.AutoIncrement = false;
			colvarReplyMemberID.IsNullable = true;
			colvarReplyMemberID.IsPrimaryKey = false;
			colvarReplyMemberID.IsForeignKey = false;
			colvarReplyMemberID.IsReadOnly = false;
                
			colvarReplyMemberID.DefaultSetting = @"";
			colvarReplyMemberID.ForeignKeyTableName = "";
            colvarReplyMemberID.ApplyExtendedProperties();
			this.Columns.Add(colvarReplyMemberID);
			
			TableSchema.TableColumn colvarReplyMemo = new TableSchema.TableColumn(this);
			colvarReplyMemo.ColumnName = "ReplyMemo";
			colvarReplyMemo.DataType = DbType.AnsiString;
			colvarReplyMemo.MaxLength = 2147483647;
			colvarReplyMemo.AutoIncrement = false;
			colvarReplyMemo.IsNullable = true;
			colvarReplyMemo.IsPrimaryKey = false;
			colvarReplyMemo.IsForeignKey = false;
			colvarReplyMemo.IsReadOnly = false;
                
			colvarReplyMemo.DefaultSetting = @"";
			colvarReplyMemo.ForeignKeyTableName = "";
            colvarReplyMemo.ApplyExtendedProperties();
			this.Columns.Add(colvarReplyMemo);
			
			TableSchema.TableColumn colvarReplyDate = new TableSchema.TableColumn(this);
			colvarReplyDate.ColumnName = "ReplyDate";
			colvarReplyDate.DataType = DbType.DateTime;
			colvarReplyDate.MaxLength = 0;
			colvarReplyDate.AutoIncrement = false;
			colvarReplyDate.IsNullable = true;
			colvarReplyDate.IsPrimaryKey = false;
			colvarReplyDate.IsForeignKey = false;
			colvarReplyDate.IsReadOnly = false;
                
			colvarReplyDate.DefaultSetting = @"";
			colvarReplyDate.ForeignKeyTableName = "";
            colvarReplyDate.ApplyExtendedProperties();
			this.Columns.Add(colvarReplyDate);
			
			TableSchema.TableColumn colvarFaceBookRate = new TableSchema.TableColumn(this);
			colvarFaceBookRate.ColumnName = "FaceBookRate";
			colvarFaceBookRate.DataType = DbType.Int32;
			colvarFaceBookRate.MaxLength = 0;
			colvarFaceBookRate.AutoIncrement = false;
			colvarFaceBookRate.IsNullable = true;
			colvarFaceBookRate.IsPrimaryKey = false;
			colvarFaceBookRate.IsForeignKey = false;
			colvarFaceBookRate.IsReadOnly = false;
                
			colvarFaceBookRate.DefaultSetting = @"";
			colvarFaceBookRate.ForeignKeyTableName = "";
            colvarFaceBookRate.ApplyExtendedProperties();
			this.Columns.Add(colvarFaceBookRate);
			
			TableSchema.TableColumn colvarLatestOrderDate = new TableSchema.TableColumn(this);
			colvarLatestOrderDate.ColumnName = "LatestOrderDate";
			colvarLatestOrderDate.DataType = DbType.DateTime;
			colvarLatestOrderDate.MaxLength = 0;
			colvarLatestOrderDate.AutoIncrement = false;
			colvarLatestOrderDate.IsNullable = true;
			colvarLatestOrderDate.IsPrimaryKey = false;
			colvarLatestOrderDate.IsForeignKey = false;
			colvarLatestOrderDate.IsReadOnly = false;
                
			colvarLatestOrderDate.DefaultSetting = @"";
			colvarLatestOrderDate.ForeignKeyTableName = "";
            colvarLatestOrderDate.ApplyExtendedProperties();
			this.Columns.Add(colvarLatestOrderDate);
			
			TableSchema.TableColumn colvarPBizID = new TableSchema.TableColumn(this);
			colvarPBizID.ColumnName = "PBizID";
			colvarPBizID.DataType = DbType.Int32;
			colvarPBizID.MaxLength = 0;
			colvarPBizID.AutoIncrement = false;
			colvarPBizID.IsNullable = true;
			colvarPBizID.IsPrimaryKey = false;
			colvarPBizID.IsForeignKey = false;
			colvarPBizID.IsReadOnly = false;
                
			colvarPBizID.DefaultSetting = @"";
			colvarPBizID.ForeignKeyTableName = "";
            colvarPBizID.ApplyExtendedProperties();
			this.Columns.Add(colvarPBizID);
			
			TableSchema.TableColumn colvarIsRead = new TableSchema.TableColumn(this);
			colvarIsRead.ColumnName = "IsRead";
			colvarIsRead.DataType = DbType.Boolean;
			colvarIsRead.MaxLength = 0;
			colvarIsRead.AutoIncrement = false;
			colvarIsRead.IsNullable = true;
			colvarIsRead.IsPrimaryKey = false;
			colvarIsRead.IsForeignKey = false;
			colvarIsRead.IsReadOnly = false;
                
			colvarIsRead.DefaultSetting = @"";
			colvarIsRead.ForeignKeyTableName = "";
            colvarIsRead.ApplyExtendedProperties();
			this.Columns.Add(colvarIsRead);
			
			TableSchema.TableColumn colvarTopDate = new TableSchema.TableColumn(this);
			colvarTopDate.ColumnName = "TopDate";
			colvarTopDate.DataType = DbType.DateTime;
			colvarTopDate.MaxLength = 0;
			colvarTopDate.AutoIncrement = false;
			colvarTopDate.IsNullable = true;
			colvarTopDate.IsPrimaryKey = false;
			colvarTopDate.IsForeignKey = false;
			colvarTopDate.IsReadOnly = false;
                
			colvarTopDate.DefaultSetting = @"";
			colvarTopDate.ForeignKeyTableName = "";
            colvarTopDate.ApplyExtendedProperties();
			this.Columns.Add(colvarTopDate);
			
		}
		#endregion
	}
}