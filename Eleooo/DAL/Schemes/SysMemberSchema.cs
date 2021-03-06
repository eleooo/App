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
	/// This is an ActiveRecord class which wraps the Sys_Member table.
	/// </summary>
	[Serializable]
	public partial class SysMemberSchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysMemberSchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysMemberSchema()
            :base("Sys_Member")
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
			
			TableSchema.TableColumn colvarMemberEmail = new TableSchema.TableColumn(this);
			colvarMemberEmail.ColumnName = "MemberEmail";
			colvarMemberEmail.DataType = DbType.String;
			colvarMemberEmail.MaxLength = 50;
			colvarMemberEmail.AutoIncrement = false;
			colvarMemberEmail.IsNullable = true;
			colvarMemberEmail.IsPrimaryKey = false;
			colvarMemberEmail.IsForeignKey = false;
			colvarMemberEmail.IsReadOnly = false;
                
			colvarMemberEmail.DefaultSetting = @"";
			colvarMemberEmail.ForeignKeyTableName = "";
            colvarMemberEmail.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberEmail);
			
			TableSchema.TableColumn colvarMemberFullname = new TableSchema.TableColumn(this);
			colvarMemberFullname.ColumnName = "MemberFullname";
			colvarMemberFullname.DataType = DbType.String;
			colvarMemberFullname.MaxLength = 50;
			colvarMemberFullname.AutoIncrement = false;
			colvarMemberFullname.IsNullable = true;
			colvarMemberFullname.IsPrimaryKey = false;
			colvarMemberFullname.IsForeignKey = false;
			colvarMemberFullname.IsReadOnly = false;
                
			colvarMemberFullname.DefaultSetting = @"";
			colvarMemberFullname.ForeignKeyTableName = "";
            colvarMemberFullname.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberFullname);
			
			TableSchema.TableColumn colvarMemberPwd = new TableSchema.TableColumn(this);
			colvarMemberPwd.ColumnName = "MemberPwd";
			colvarMemberPwd.DataType = DbType.String;
			colvarMemberPwd.MaxLength = 50;
			colvarMemberPwd.AutoIncrement = false;
			colvarMemberPwd.IsNullable = false;
			colvarMemberPwd.IsPrimaryKey = false;
			colvarMemberPwd.IsForeignKey = false;
			colvarMemberPwd.IsReadOnly = false;
                
			colvarMemberPwd.DefaultSetting = @"";
			colvarMemberPwd.ForeignKeyTableName = "";
            colvarMemberPwd.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberPwd);
			
			TableSchema.TableColumn colvarMemberFinger = new TableSchema.TableColumn(this);
			colvarMemberFinger.ColumnName = "MemberFinger";
			colvarMemberFinger.DataType = DbType.AnsiString;
			colvarMemberFinger.MaxLength = 600;
			colvarMemberFinger.AutoIncrement = false;
			colvarMemberFinger.IsNullable = true;
			colvarMemberFinger.IsPrimaryKey = false;
			colvarMemberFinger.IsForeignKey = false;
			colvarMemberFinger.IsReadOnly = false;
                
			colvarMemberFinger.DefaultSetting = @"";
			colvarMemberFinger.ForeignKeyTableName = "";
            colvarMemberFinger.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberFinger);
			
			TableSchema.TableColumn colvarMemberPhoneNumber = new TableSchema.TableColumn(this);
			colvarMemberPhoneNumber.ColumnName = "MemberPhoneNumber";
			colvarMemberPhoneNumber.DataType = DbType.String;
			colvarMemberPhoneNumber.MaxLength = 50;
			colvarMemberPhoneNumber.AutoIncrement = false;
			colvarMemberPhoneNumber.IsNullable = false;
			colvarMemberPhoneNumber.IsPrimaryKey = false;
			colvarMemberPhoneNumber.IsForeignKey = false;
			colvarMemberPhoneNumber.IsReadOnly = false;
                
			colvarMemberPhoneNumber.DefaultSetting = @"";
			colvarMemberPhoneNumber.ForeignKeyTableName = "";
            colvarMemberPhoneNumber.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberPhoneNumber);
			
			TableSchema.TableColumn colvarMemberGrade = new TableSchema.TableColumn(this);
			colvarMemberGrade.ColumnName = "MemberGrade";
			colvarMemberGrade.DataType = DbType.Int32;
			colvarMemberGrade.MaxLength = 0;
			colvarMemberGrade.AutoIncrement = false;
			colvarMemberGrade.IsNullable = true;
			colvarMemberGrade.IsPrimaryKey = false;
			colvarMemberGrade.IsForeignKey = false;
			colvarMemberGrade.IsReadOnly = false;
                
			
			colvarMemberGrade.DefaultSetting = @"((0))";
			colvarMemberGrade.ForeignKeyTableName = "";
            colvarMemberGrade.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberGrade);
			
			TableSchema.TableColumn colvarMemberAddress1 = new TableSchema.TableColumn(this);
			colvarMemberAddress1.ColumnName = "MemberAddress1";
			colvarMemberAddress1.DataType = DbType.String;
			colvarMemberAddress1.MaxLength = 200;
			colvarMemberAddress1.AutoIncrement = false;
			colvarMemberAddress1.IsNullable = true;
			colvarMemberAddress1.IsPrimaryKey = false;
			colvarMemberAddress1.IsForeignKey = false;
			colvarMemberAddress1.IsReadOnly = false;
                
			colvarMemberAddress1.DefaultSetting = @"";
			colvarMemberAddress1.ForeignKeyTableName = "";
            colvarMemberAddress1.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberAddress1);
			
			TableSchema.TableColumn colvarMemberAddress2 = new TableSchema.TableColumn(this);
			colvarMemberAddress2.ColumnName = "MemberAddress2";
			colvarMemberAddress2.DataType = DbType.String;
			colvarMemberAddress2.MaxLength = 200;
			colvarMemberAddress2.AutoIncrement = false;
			colvarMemberAddress2.IsNullable = true;
			colvarMemberAddress2.IsPrimaryKey = false;
			colvarMemberAddress2.IsForeignKey = false;
			colvarMemberAddress2.IsReadOnly = false;
                
			colvarMemberAddress2.DefaultSetting = @"";
			colvarMemberAddress2.ForeignKeyTableName = "";
            colvarMemberAddress2.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberAddress2);
			
			TableSchema.TableColumn colvarMemberCountry = new TableSchema.TableColumn(this);
			colvarMemberCountry.ColumnName = "MemberCountry";
			colvarMemberCountry.DataType = DbType.String;
			colvarMemberCountry.MaxLength = 50;
			colvarMemberCountry.AutoIncrement = false;
			colvarMemberCountry.IsNullable = true;
			colvarMemberCountry.IsPrimaryKey = false;
			colvarMemberCountry.IsForeignKey = false;
			colvarMemberCountry.IsReadOnly = false;
                
			colvarMemberCountry.DefaultSetting = @"";
			colvarMemberCountry.ForeignKeyTableName = "";
            colvarMemberCountry.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberCountry);
			
			TableSchema.TableColumn colvarMemberState = new TableSchema.TableColumn(this);
			colvarMemberState.ColumnName = "MemberState";
			colvarMemberState.DataType = DbType.String;
			colvarMemberState.MaxLength = 50;
			colvarMemberState.AutoIncrement = false;
			colvarMemberState.IsNullable = true;
			colvarMemberState.IsPrimaryKey = false;
			colvarMemberState.IsForeignKey = false;
			colvarMemberState.IsReadOnly = false;
                
			colvarMemberState.DefaultSetting = @"";
			colvarMemberState.ForeignKeyTableName = "";
            colvarMemberState.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberState);
			
			TableSchema.TableColumn colvarMemberCity = new TableSchema.TableColumn(this);
			colvarMemberCity.ColumnName = "MemberCity";
			colvarMemberCity.DataType = DbType.Int32;
			colvarMemberCity.MaxLength = 0;
			colvarMemberCity.AutoIncrement = false;
			colvarMemberCity.IsNullable = true;
			colvarMemberCity.IsPrimaryKey = false;
			colvarMemberCity.IsForeignKey = false;
			colvarMemberCity.IsReadOnly = false;
                
			colvarMemberCity.DefaultSetting = @"";
			colvarMemberCity.ForeignKeyTableName = "";
            colvarMemberCity.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberCity);
			
			TableSchema.TableColumn colvarMemberArea = new TableSchema.TableColumn(this);
			colvarMemberArea.ColumnName = "MemberArea";
			colvarMemberArea.DataType = DbType.String;
			colvarMemberArea.MaxLength = 50;
			colvarMemberArea.AutoIncrement = false;
			colvarMemberArea.IsNullable = true;
			colvarMemberArea.IsPrimaryKey = false;
			colvarMemberArea.IsForeignKey = false;
			colvarMemberArea.IsReadOnly = false;
                
			colvarMemberArea.DefaultSetting = @"";
			colvarMemberArea.ForeignKeyTableName = "";
            colvarMemberArea.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberArea);
			
			TableSchema.TableColumn colvarMemberLocation = new TableSchema.TableColumn(this);
			colvarMemberLocation.ColumnName = "MemberLocation";
			colvarMemberLocation.DataType = DbType.String;
			colvarMemberLocation.MaxLength = 50;
			colvarMemberLocation.AutoIncrement = false;
			colvarMemberLocation.IsNullable = true;
			colvarMemberLocation.IsPrimaryKey = false;
			colvarMemberLocation.IsForeignKey = false;
			colvarMemberLocation.IsReadOnly = false;
                
			colvarMemberLocation.DefaultSetting = @"";
			colvarMemberLocation.ForeignKeyTableName = "";
            colvarMemberLocation.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberLocation);
			
			TableSchema.TableColumn colvarAreaDepth1 = new TableSchema.TableColumn(this);
			colvarAreaDepth1.ColumnName = "AreaDepth1";
			colvarAreaDepth1.DataType = DbType.String;
			colvarAreaDepth1.MaxLength = 50;
			colvarAreaDepth1.AutoIncrement = false;
			colvarAreaDepth1.IsNullable = true;
			colvarAreaDepth1.IsPrimaryKey = false;
			colvarAreaDepth1.IsForeignKey = false;
			colvarAreaDepth1.IsReadOnly = false;
                
			
            colvarAreaDepth1.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","工作圈"));
            colvarAreaDepth1.DefaultSetting = @"";
			colvarAreaDepth1.ForeignKeyTableName = "";
            colvarAreaDepth1.ApplyExtendedProperties();
			this.Columns.Add(colvarAreaDepth1);
			
			TableSchema.TableColumn colvarAreaDepth2 = new TableSchema.TableColumn(this);
			colvarAreaDepth2.ColumnName = "AreaDepth2";
			colvarAreaDepth2.DataType = DbType.String;
			colvarAreaDepth2.MaxLength = 50;
			colvarAreaDepth2.AutoIncrement = false;
			colvarAreaDepth2.IsNullable = true;
			colvarAreaDepth2.IsPrimaryKey = false;
			colvarAreaDepth2.IsForeignKey = false;
			colvarAreaDepth2.IsReadOnly = false;
                
			
            colvarAreaDepth2.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","生活圈"));
            colvarAreaDepth2.DefaultSetting = @"";
			colvarAreaDepth2.ForeignKeyTableName = "";
            colvarAreaDepth2.ApplyExtendedProperties();
			this.Columns.Add(colvarAreaDepth2);
			
			TableSchema.TableColumn colvarAreaDepth3 = new TableSchema.TableColumn(this);
			colvarAreaDepth3.ColumnName = "AreaDepth3";
			colvarAreaDepth3.DataType = DbType.String;
			colvarAreaDepth3.MaxLength = 500;
			colvarAreaDepth3.AutoIncrement = false;
			colvarAreaDepth3.IsNullable = true;
			colvarAreaDepth3.IsPrimaryKey = false;
			colvarAreaDepth3.IsForeignKey = false;
			colvarAreaDepth3.IsReadOnly = false;
                
			colvarAreaDepth3.DefaultSetting = @"";
			colvarAreaDepth3.ForeignKeyTableName = "";
            colvarAreaDepth3.ApplyExtendedProperties();
			this.Columns.Add(colvarAreaDepth3);
			
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
			
			TableSchema.TableColumn colvarMemberZip = new TableSchema.TableColumn(this);
			colvarMemberZip.ColumnName = "MemberZip";
			colvarMemberZip.DataType = DbType.String;
			colvarMemberZip.MaxLength = 50;
			colvarMemberZip.AutoIncrement = false;
			colvarMemberZip.IsNullable = true;
			colvarMemberZip.IsPrimaryKey = false;
			colvarMemberZip.IsForeignKey = false;
			colvarMemberZip.IsReadOnly = false;
                
			colvarMemberZip.DefaultSetting = @"";
			colvarMemberZip.ForeignKeyTableName = "";
            colvarMemberZip.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberZip);
			
			TableSchema.TableColumn colvarMemberDate = new TableSchema.TableColumn(this);
			colvarMemberDate.ColumnName = "MemberDate";
			colvarMemberDate.DataType = DbType.DateTime;
			colvarMemberDate.MaxLength = 0;
			colvarMemberDate.AutoIncrement = false;
			colvarMemberDate.IsNullable = true;
			colvarMemberDate.IsPrimaryKey = false;
			colvarMemberDate.IsForeignKey = false;
			colvarMemberDate.IsReadOnly = false;
                
			colvarMemberDate.DefaultSetting = @"";
			colvarMemberDate.ForeignKeyTableName = "";
            colvarMemberDate.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberDate);
			
			TableSchema.TableColumn colvarMemberSum = new TableSchema.TableColumn(this);
			colvarMemberSum.ColumnName = "MemberSum";
			colvarMemberSum.DataType = DbType.Decimal;
			colvarMemberSum.MaxLength = 0;
			colvarMemberSum.AutoIncrement = false;
			colvarMemberSum.IsNullable = true;
			colvarMemberSum.IsPrimaryKey = false;
			colvarMemberSum.IsForeignKey = false;
			colvarMemberSum.IsReadOnly = false;
                
			colvarMemberSum.DefaultSetting = @"";
			colvarMemberSum.ForeignKeyTableName = "";
            colvarMemberSum.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberSum);
			
			TableSchema.TableColumn colvarMemberBalance = new TableSchema.TableColumn(this);
			colvarMemberBalance.ColumnName = "MemberBalance";
			colvarMemberBalance.DataType = DbType.Decimal;
			colvarMemberBalance.MaxLength = 0;
			colvarMemberBalance.AutoIncrement = false;
			colvarMemberBalance.IsNullable = true;
			colvarMemberBalance.IsPrimaryKey = false;
			colvarMemberBalance.IsForeignKey = false;
			colvarMemberBalance.IsReadOnly = false;
                
			
			colvarMemberBalance.DefaultSetting = @"((0))";
			colvarMemberBalance.ForeignKeyTableName = "";
            colvarMemberBalance.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberBalance);
			
			TableSchema.TableColumn colvarMemberBalanceCash = new TableSchema.TableColumn(this);
			colvarMemberBalanceCash.ColumnName = "MemberBalanceCash";
			colvarMemberBalanceCash.DataType = DbType.Decimal;
			colvarMemberBalanceCash.MaxLength = 0;
			colvarMemberBalanceCash.AutoIncrement = false;
			colvarMemberBalanceCash.IsNullable = true;
			colvarMemberBalanceCash.IsPrimaryKey = false;
			colvarMemberBalanceCash.IsForeignKey = false;
			colvarMemberBalanceCash.IsReadOnly = false;
                
			
			colvarMemberBalanceCash.DefaultSetting = @"((0))";
			colvarMemberBalanceCash.ForeignKeyTableName = "";
            colvarMemberBalanceCash.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberBalanceCash);
			
			TableSchema.TableColumn colvarMemberStatus = new TableSchema.TableColumn(this);
			colvarMemberStatus.ColumnName = "MemberStatus";
			colvarMemberStatus.DataType = DbType.Int32;
			colvarMemberStatus.MaxLength = 0;
			colvarMemberStatus.AutoIncrement = false;
			colvarMemberStatus.IsNullable = true;
			colvarMemberStatus.IsPrimaryKey = false;
			colvarMemberStatus.IsForeignKey = false;
			colvarMemberStatus.IsReadOnly = false;
                
			colvarMemberStatus.DefaultSetting = @"";
			colvarMemberStatus.ForeignKeyTableName = "";
            colvarMemberStatus.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberStatus);
			
			TableSchema.TableColumn colvarMemberMemo = new TableSchema.TableColumn(this);
			colvarMemberMemo.ColumnName = "MemberMemo";
			colvarMemberMemo.DataType = DbType.String;
			colvarMemberMemo.MaxLength = 50;
			colvarMemberMemo.AutoIncrement = false;
			colvarMemberMemo.IsNullable = true;
			colvarMemberMemo.IsPrimaryKey = false;
			colvarMemberMemo.IsForeignKey = false;
			colvarMemberMemo.IsReadOnly = false;
                
			colvarMemberMemo.DefaultSetting = @"";
			colvarMemberMemo.ForeignKeyTableName = "";
            colvarMemberMemo.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberMemo);
			
			TableSchema.TableColumn colvarMemberPid = new TableSchema.TableColumn(this);
			colvarMemberPid.ColumnName = "MemberPid";
			colvarMemberPid.DataType = DbType.Int32;
			colvarMemberPid.MaxLength = 0;
			colvarMemberPid.AutoIncrement = false;
			colvarMemberPid.IsNullable = true;
			colvarMemberPid.IsPrimaryKey = false;
			colvarMemberPid.IsForeignKey = false;
			colvarMemberPid.IsReadOnly = false;
                
			colvarMemberPid.DefaultSetting = @"";
			colvarMemberPid.ForeignKeyTableName = "";
            colvarMemberPid.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberPid);
			
			TableSchema.TableColumn colvarMemberCompanyID = new TableSchema.TableColumn(this);
			colvarMemberCompanyID.ColumnName = "MemberCompanyID";
			colvarMemberCompanyID.DataType = DbType.Int32;
			colvarMemberCompanyID.MaxLength = 0;
			colvarMemberCompanyID.AutoIncrement = false;
			colvarMemberCompanyID.IsNullable = true;
			colvarMemberCompanyID.IsPrimaryKey = false;
			colvarMemberCompanyID.IsForeignKey = false;
			colvarMemberCompanyID.IsReadOnly = false;
                
			colvarMemberCompanyID.DefaultSetting = @"";
			colvarMemberCompanyID.ForeignKeyTableName = "";
            colvarMemberCompanyID.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberCompanyID);
			
			TableSchema.TableColumn colvarMemberSex = new TableSchema.TableColumn(this);
			colvarMemberSex.ColumnName = "MemberSex";
			colvarMemberSex.DataType = DbType.Boolean;
			colvarMemberSex.MaxLength = 0;
			colvarMemberSex.AutoIncrement = false;
			colvarMemberSex.IsNullable = true;
			colvarMemberSex.IsPrimaryKey = false;
			colvarMemberSex.IsForeignKey = false;
			colvarMemberSex.IsReadOnly = false;
                
			colvarMemberSex.DefaultSetting = @"";
			colvarMemberSex.ForeignKeyTableName = "";
            colvarMemberSex.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberSex);
			
			TableSchema.TableColumn colvarMemberBirthday = new TableSchema.TableColumn(this);
			colvarMemberBirthday.ColumnName = "MemberBirthday";
			colvarMemberBirthday.DataType = DbType.DateTime;
			colvarMemberBirthday.MaxLength = 0;
			colvarMemberBirthday.AutoIncrement = false;
			colvarMemberBirthday.IsNullable = true;
			colvarMemberBirthday.IsPrimaryKey = false;
			colvarMemberBirthday.IsForeignKey = false;
			colvarMemberBirthday.IsReadOnly = false;
                
			colvarMemberBirthday.DefaultSetting = @"";
			colvarMemberBirthday.ForeignKeyTableName = "";
            colvarMemberBirthday.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberBirthday);
			
			TableSchema.TableColumn colvarMemberRoleId = new TableSchema.TableColumn(this);
			colvarMemberRoleId.ColumnName = "MemberRole_ID";
			colvarMemberRoleId.DataType = DbType.Int32;
			colvarMemberRoleId.MaxLength = 0;
			colvarMemberRoleId.AutoIncrement = false;
			colvarMemberRoleId.IsNullable = true;
			colvarMemberRoleId.IsPrimaryKey = false;
			colvarMemberRoleId.IsForeignKey = false;
			colvarMemberRoleId.IsReadOnly = false;
                
			
			colvarMemberRoleId.DefaultSetting = @"((0))";
			colvarMemberRoleId.ForeignKeyTableName = "";
            colvarMemberRoleId.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberRoleId);
			
			TableSchema.TableColumn colvarCompanyRoleId = new TableSchema.TableColumn(this);
			colvarCompanyRoleId.ColumnName = "CompanyRole_ID";
			colvarCompanyRoleId.DataType = DbType.Int32;
			colvarCompanyRoleId.MaxLength = 0;
			colvarCompanyRoleId.AutoIncrement = false;
			colvarCompanyRoleId.IsNullable = true;
			colvarCompanyRoleId.IsPrimaryKey = false;
			colvarCompanyRoleId.IsForeignKey = false;
			colvarCompanyRoleId.IsReadOnly = false;
                
			colvarCompanyRoleId.DefaultSetting = @"";
			colvarCompanyRoleId.ForeignKeyTableName = "";
            colvarCompanyRoleId.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyRoleId);
			
			TableSchema.TableColumn colvarAdminRoleId = new TableSchema.TableColumn(this);
			colvarAdminRoleId.ColumnName = "AdminRole_ID";
			colvarAdminRoleId.DataType = DbType.Int32;
			colvarAdminRoleId.MaxLength = 0;
			colvarAdminRoleId.AutoIncrement = false;
			colvarAdminRoleId.IsNullable = true;
			colvarAdminRoleId.IsPrimaryKey = false;
			colvarAdminRoleId.IsForeignKey = false;
			colvarAdminRoleId.IsReadOnly = false;
                
			
			colvarAdminRoleId.DefaultSetting = @"((0))";
			colvarAdminRoleId.ForeignKeyTableName = "";
            colvarAdminRoleId.ApplyExtendedProperties();
			this.Columns.Add(colvarAdminRoleId);
			
			TableSchema.TableColumn colvarLastLoginDate = new TableSchema.TableColumn(this);
			colvarLastLoginDate.ColumnName = "LastLoginDate";
			colvarLastLoginDate.DataType = DbType.DateTime;
			colvarLastLoginDate.MaxLength = 0;
			colvarLastLoginDate.AutoIncrement = false;
			colvarLastLoginDate.IsNullable = true;
			colvarLastLoginDate.IsPrimaryKey = false;
			colvarLastLoginDate.IsForeignKey = false;
			colvarLastLoginDate.IsReadOnly = false;
                
			
            colvarLastLoginDate.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","最后登录时间"));
            colvarLastLoginDate.DefaultSetting = @"";
			colvarLastLoginDate.ForeignKeyTableName = "";
            colvarLastLoginDate.ApplyExtendedProperties();
			this.Columns.Add(colvarLastLoginDate);
			
			TableSchema.TableColumn colvarLastLoginSubSys = new TableSchema.TableColumn(this);
			colvarLastLoginSubSys.ColumnName = "LastLoginSubSys";
			colvarLastLoginSubSys.DataType = DbType.Int32;
			colvarLastLoginSubSys.MaxLength = 0;
			colvarLastLoginSubSys.AutoIncrement = false;
			colvarLastLoginSubSys.IsNullable = true;
			colvarLastLoginSubSys.IsPrimaryKey = false;
			colvarLastLoginSubSys.IsForeignKey = false;
			colvarLastLoginSubSys.IsReadOnly = false;
                
			colvarLastLoginSubSys.DefaultSetting = @"";
			colvarLastLoginSubSys.ForeignKeyTableName = "";
            colvarLastLoginSubSys.ApplyExtendedProperties();
			this.Columns.Add(colvarLastLoginSubSys);
			
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
			
			TableSchema.TableColumn colvarCompanyId = new TableSchema.TableColumn(this);
			colvarCompanyId.ColumnName = "Company_ID";
			colvarCompanyId.DataType = DbType.Int32;
			colvarCompanyId.MaxLength = 0;
			colvarCompanyId.AutoIncrement = false;
			colvarCompanyId.IsNullable = true;
			colvarCompanyId.IsPrimaryKey = false;
			colvarCompanyId.IsForeignKey = false;
			colvarCompanyId.IsReadOnly = false;
                
			
			colvarCompanyId.DefaultSetting = @"((0))";
			colvarCompanyId.ForeignKeyTableName = "";
            colvarCompanyId.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyId);
			
			TableSchema.TableColumn colvarMemberMsnPhone = new TableSchema.TableColumn(this);
			colvarMemberMsnPhone.ColumnName = "MemberMsnPhone";
			colvarMemberMsnPhone.DataType = DbType.String;
			colvarMemberMsnPhone.MaxLength = 50;
			colvarMemberMsnPhone.AutoIncrement = false;
			colvarMemberMsnPhone.IsNullable = true;
			colvarMemberMsnPhone.IsPrimaryKey = false;
			colvarMemberMsnPhone.IsForeignKey = false;
			colvarMemberMsnPhone.IsReadOnly = false;
                
			colvarMemberMsnPhone.DefaultSetting = @"";
			colvarMemberMsnPhone.ForeignKeyTableName = "";
            colvarMemberMsnPhone.ApplyExtendedProperties();
			this.Columns.Add(colvarMemberMsnPhone);
			
		}
		#endregion
	}
}