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
	/// This is an ActiveRecord class which wraps the Sys_Member_Config table.
	/// </summary>
	[Serializable]
	public partial class SysMemberConfigSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysMemberConfigSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysMemberConfigSchema()
            :base("Sys_Member_Config")
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
			
			TableSchema.TableColumn colvarMemberId = new TableSchema.TableColumn(this);
			colvarMemberId.ColumnName = "MemberId";
			colvarMemberId.DataType = DbType.Int32;
			colvarMemberId.MaxLength = 0;
			colvarMemberId.AutoIncrement = false;
			colvarMemberId.IsNullable = false;
			colvarMemberId.IsPrimaryKey = true;
			colvarMemberId.IsForeignKey = false;
			colvarMemberId.IsReadOnly = false;
                
			colvarMemberId.DefaultSetting = @"";
			colvarMemberId.ForeignKeyTableName = "";
            colvarMemberId.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberId);
			
			TableSchema.TableColumn colvarAreaModifyDate = new TableSchema.TableColumn(this);
			colvarAreaModifyDate.ColumnName = "AreaModifyDate";
			colvarAreaModifyDate.DataType = DbType.DateTime;
			colvarAreaModifyDate.MaxLength = 0;
			colvarAreaModifyDate.AutoIncrement = false;
			colvarAreaModifyDate.IsNullable = true;
			colvarAreaModifyDate.IsPrimaryKey = false;
			colvarAreaModifyDate.IsForeignKey = false;
			colvarAreaModifyDate.IsReadOnly = false;
                
			colvarAreaModifyDate.DefaultSetting = @"";
			colvarAreaModifyDate.ForeignKeyTableName = "";
            colvarAreaModifyDate.ApplyExtendedProperties();
			this.Columns.Add(colvarAreaModifyDate);
			
			TableSchema.TableColumn colvarAreaModifyCount = new TableSchema.TableColumn(this);
			colvarAreaModifyCount.ColumnName = "AreaModifyCount";
			colvarAreaModifyCount.DataType = DbType.Int32;
			colvarAreaModifyCount.MaxLength = 0;
			colvarAreaModifyCount.AutoIncrement = false;
			colvarAreaModifyCount.IsNullable = true;
			colvarAreaModifyCount.IsPrimaryKey = false;
			colvarAreaModifyCount.IsForeignKey = false;
			colvarAreaModifyCount.IsReadOnly = false;
                
			
			colvarAreaModifyCount.DefaultSetting = @"((0))";
			colvarAreaModifyCount.ForeignKeyTableName = "";
            colvarAreaModifyCount.ApplyExtendedProperties();
			this.Columns.Add(colvarAreaModifyCount);
			
			TableSchema.TableColumn colvarMyFavCompany = new TableSchema.TableColumn(this);
			colvarMyFavCompany.ColumnName = "MyFavCompany";
			colvarMyFavCompany.DataType = DbType.String;
			colvarMyFavCompany.MaxLength = 4000;
			colvarMyFavCompany.AutoIncrement = false;
			colvarMyFavCompany.IsNullable = true;
			colvarMyFavCompany.IsPrimaryKey = false;
			colvarMyFavCompany.IsForeignKey = false;
			colvarMyFavCompany.IsReadOnly = false;
                
			colvarMyFavCompany.DefaultSetting = @"";
			colvarMyFavCompany.ForeignKeyTableName = "";
            colvarMyFavCompany.ApplyExtendedProperties();
			this.Columns.Add(colvarMyFavCompany);
			
			TableSchema.TableColumn colvarMyAddress = new TableSchema.TableColumn(this);
			colvarMyAddress.ColumnName = "MyAddress";
			colvarMyAddress.DataType = DbType.String;
			colvarMyAddress.MaxLength = 4000;
			colvarMyAddress.AutoIncrement = false;
			colvarMyAddress.IsNullable = true;
			colvarMyAddress.IsPrimaryKey = false;
			colvarMyAddress.IsForeignKey = false;
			colvarMyAddress.IsReadOnly = false;
                
			colvarMyAddress.DefaultSetting = @"";
			colvarMyAddress.ForeignKeyTableName = "";
            colvarMyAddress.ApplyExtendedProperties();
			this.Columns.Add(colvarMyAddress);
			
			TableSchema.TableColumn colvarMyRecItems = new TableSchema.TableColumn(this);
			colvarMyRecItems.ColumnName = "MyRecItems";
			colvarMyRecItems.DataType = DbType.String;
			colvarMyRecItems.MaxLength = 1000;
			colvarMyRecItems.AutoIncrement = false;
			colvarMyRecItems.IsNullable = true;
			colvarMyRecItems.IsPrimaryKey = false;
			colvarMyRecItems.IsForeignKey = false;
			colvarMyRecItems.IsReadOnly = false;
                
			colvarMyRecItems.DefaultSetting = @"";
			colvarMyRecItems.ForeignKeyTableName = "";
            colvarMyRecItems.ApplyExtendedProperties();
			this.Columns.Add(colvarMyRecItems);
			
			TableSchema.TableColumn colvarMyRecAds = new TableSchema.TableColumn(this);
			colvarMyRecAds.ColumnName = "MyRecAds";
			colvarMyRecAds.DataType = DbType.String;
			colvarMyRecAds.MaxLength = 1000;
			colvarMyRecAds.AutoIncrement = false;
			colvarMyRecAds.IsNullable = true;
			colvarMyRecAds.IsPrimaryKey = false;
			colvarMyRecAds.IsForeignKey = false;
			colvarMyRecAds.IsReadOnly = false;
                
			colvarMyRecAds.DefaultSetting = @"";
			colvarMyRecAds.ForeignKeyTableName = "";
            colvarMyRecAds.ApplyExtendedProperties();
			this.Columns.Add(colvarMyRecAds);
			
			TableSchema.TableColumn colvarMsnPwdCount = new TableSchema.TableColumn(this);
			colvarMsnPwdCount.ColumnName = "MsnPwdCount";
			colvarMsnPwdCount.DataType = DbType.Int32;
			colvarMsnPwdCount.MaxLength = 0;
			colvarMsnPwdCount.AutoIncrement = false;
			colvarMsnPwdCount.IsNullable = true;
			colvarMsnPwdCount.IsPrimaryKey = false;
			colvarMsnPwdCount.IsForeignKey = false;
			colvarMsnPwdCount.IsReadOnly = false;
                
			colvarMsnPwdCount.DefaultSetting = @"";
			colvarMsnPwdCount.ForeignKeyTableName = "";
            colvarMsnPwdCount.ApplyExtendedProperties();
			this.Columns.Add(colvarMsnPwdCount);
			
			TableSchema.TableColumn colvarArea2ModifyDate = new TableSchema.TableColumn(this);
			colvarArea2ModifyDate.ColumnName = "Area2ModifyDate";
			colvarArea2ModifyDate.DataType = DbType.DateTime;
			colvarArea2ModifyDate.MaxLength = 0;
			colvarArea2ModifyDate.AutoIncrement = false;
			colvarArea2ModifyDate.IsNullable = true;
			colvarArea2ModifyDate.IsPrimaryKey = false;
			colvarArea2ModifyDate.IsForeignKey = false;
			colvarArea2ModifyDate.IsReadOnly = false;
                
			colvarArea2ModifyDate.DefaultSetting = @"";
			colvarArea2ModifyDate.ForeignKeyTableName = "";
            colvarArea2ModifyDate.ApplyExtendedProperties();
			this.Columns.Add(colvarArea2ModifyDate);
			
			TableSchema.TableColumn colvarPhoneModifyDate = new TableSchema.TableColumn(this);
			colvarPhoneModifyDate.ColumnName = "PhoneModifyDate";
			colvarPhoneModifyDate.DataType = DbType.DateTime;
			colvarPhoneModifyDate.MaxLength = 0;
			colvarPhoneModifyDate.AutoIncrement = false;
			colvarPhoneModifyDate.IsNullable = true;
			colvarPhoneModifyDate.IsPrimaryKey = false;
			colvarPhoneModifyDate.IsForeignKey = false;
			colvarPhoneModifyDate.IsReadOnly = false;
                
			colvarPhoneModifyDate.DefaultSetting = @"";
			colvarPhoneModifyDate.ForeignKeyTableName = "";
            colvarPhoneModifyDate.ApplyExtendedProperties();
			this.Columns.Add(colvarPhoneModifyDate);
			
		}
		#endregion
	}
}