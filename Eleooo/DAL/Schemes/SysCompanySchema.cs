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
	/// This is an ActiveRecord class which wraps the Sys_Company table.
	/// </summary>
	[Serializable]
	public partial class SysCompanySchema : TableSchema.Table
	{
        public static TableSchema.Table Schema
        {
            get
            {
                return new SysCompanySchema();
            }
        }
		#region .ctors and Default Settings
		
		public SysCompanySchema()
            :base("Sys_Company")
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
			
			TableSchema.TableColumn colvarCompanyCode = new TableSchema.TableColumn(this);
			colvarCompanyCode.ColumnName = "CompanyCode";
			colvarCompanyCode.DataType = DbType.String;
			colvarCompanyCode.MaxLength = 10;
			colvarCompanyCode.AutoIncrement = false;
			colvarCompanyCode.IsNullable = true;
			colvarCompanyCode.IsPrimaryKey = false;
			colvarCompanyCode.IsForeignKey = false;
			colvarCompanyCode.IsReadOnly = false;
                
			colvarCompanyCode.DefaultSetting = @"";
			colvarCompanyCode.ForeignKeyTableName = "";
            colvarCompanyCode.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyCode);
			
			TableSchema.TableColumn colvarCompanyName = new TableSchema.TableColumn(this);
			colvarCompanyName.ColumnName = "CompanyName";
			colvarCompanyName.DataType = DbType.String;
			colvarCompanyName.MaxLength = 50;
			colvarCompanyName.AutoIncrement = false;
			colvarCompanyName.IsNullable = false;
			colvarCompanyName.IsPrimaryKey = false;
			colvarCompanyName.IsForeignKey = false;
			colvarCompanyName.IsReadOnly = false;
                
			colvarCompanyName.DefaultSetting = @"";
			colvarCompanyName.ForeignKeyTableName = "";
            colvarCompanyName.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyName);
			
			TableSchema.TableColumn colvarCompanyPwd = new TableSchema.TableColumn(this);
			colvarCompanyPwd.ColumnName = "CompanyPwd";
			colvarCompanyPwd.DataType = DbType.String;
			colvarCompanyPwd.MaxLength = 50;
			colvarCompanyPwd.AutoIncrement = false;
			colvarCompanyPwd.IsNullable = false;
			colvarCompanyPwd.IsPrimaryKey = false;
			colvarCompanyPwd.IsForeignKey = false;
			colvarCompanyPwd.IsReadOnly = false;
                
			colvarCompanyPwd.DefaultSetting = @"";
			colvarCompanyPwd.ForeignKeyTableName = "";
            colvarCompanyPwd.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyPwd);
			
			TableSchema.TableColumn colvarCompanyEmail = new TableSchema.TableColumn(this);
			colvarCompanyEmail.ColumnName = "CompanyEmail";
			colvarCompanyEmail.DataType = DbType.String;
			colvarCompanyEmail.MaxLength = 50;
			colvarCompanyEmail.AutoIncrement = false;
			colvarCompanyEmail.IsNullable = true;
			colvarCompanyEmail.IsPrimaryKey = false;
			colvarCompanyEmail.IsForeignKey = false;
			colvarCompanyEmail.IsReadOnly = false;
                
			colvarCompanyEmail.DefaultSetting = @"";
			colvarCompanyEmail.ForeignKeyTableName = "";
            colvarCompanyEmail.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyEmail);
			
			TableSchema.TableColumn colvarCompanyProvince = new TableSchema.TableColumn(this);
			colvarCompanyProvince.ColumnName = "CompanyProvince";
			colvarCompanyProvince.DataType = DbType.String;
			colvarCompanyProvince.MaxLength = 50;
			colvarCompanyProvince.AutoIncrement = false;
			colvarCompanyProvince.IsNullable = true;
			colvarCompanyProvince.IsPrimaryKey = false;
			colvarCompanyProvince.IsForeignKey = false;
			colvarCompanyProvince.IsReadOnly = false;
                
			colvarCompanyProvince.DefaultSetting = @"";
			colvarCompanyProvince.ForeignKeyTableName = "";
            colvarCompanyProvince.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyProvince);
			
			TableSchema.TableColumn colvarCompanyCity = new TableSchema.TableColumn(this);
			colvarCompanyCity.ColumnName = "CompanyCity";
			colvarCompanyCity.DataType = DbType.Int32;
			colvarCompanyCity.MaxLength = 0;
			colvarCompanyCity.AutoIncrement = false;
			colvarCompanyCity.IsNullable = true;
			colvarCompanyCity.IsPrimaryKey = false;
			colvarCompanyCity.IsForeignKey = false;
			colvarCompanyCity.IsReadOnly = false;
                
			colvarCompanyCity.DefaultSetting = @"";
			colvarCompanyCity.ForeignKeyTableName = "";
            colvarCompanyCity.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyCity);
			
			TableSchema.TableColumn colvarCompanyArea = new TableSchema.TableColumn(this);
			colvarCompanyArea.ColumnName = "CompanyArea";
			colvarCompanyArea.DataType = DbType.String;
			colvarCompanyArea.MaxLength = 50;
			colvarCompanyArea.AutoIncrement = false;
			colvarCompanyArea.IsNullable = true;
			colvarCompanyArea.IsPrimaryKey = false;
			colvarCompanyArea.IsForeignKey = false;
			colvarCompanyArea.IsReadOnly = false;
                
			colvarCompanyArea.DefaultSetting = @"";
			colvarCompanyArea.ForeignKeyTableName = "";
            colvarCompanyArea.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyArea);
			
			TableSchema.TableColumn colvarCompanyLocation = new TableSchema.TableColumn(this);
			colvarCompanyLocation.ColumnName = "CompanyLocation";
			colvarCompanyLocation.DataType = DbType.String;
			colvarCompanyLocation.MaxLength = 50;
			colvarCompanyLocation.AutoIncrement = false;
			colvarCompanyLocation.IsNullable = true;
			colvarCompanyLocation.IsPrimaryKey = false;
			colvarCompanyLocation.IsForeignKey = false;
			colvarCompanyLocation.IsReadOnly = false;
                
			colvarCompanyLocation.DefaultSetting = @"";
			colvarCompanyLocation.ForeignKeyTableName = "";
            colvarCompanyLocation.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyLocation);
			
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
			
			TableSchema.TableColumn colvarCompanyAddress = new TableSchema.TableColumn(this);
			colvarCompanyAddress.ColumnName = "CompanyAddress";
			colvarCompanyAddress.DataType = DbType.String;
			colvarCompanyAddress.MaxLength = 250;
			colvarCompanyAddress.AutoIncrement = false;
			colvarCompanyAddress.IsNullable = true;
			colvarCompanyAddress.IsPrimaryKey = false;
			colvarCompanyAddress.IsForeignKey = false;
			colvarCompanyAddress.IsReadOnly = false;
                
			colvarCompanyAddress.DefaultSetting = @"";
			colvarCompanyAddress.ForeignKeyTableName = "";
            colvarCompanyAddress.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyAddress);
			
			TableSchema.TableColumn colvarCompanyTel = new TableSchema.TableColumn(this);
			colvarCompanyTel.ColumnName = "CompanyTel";
			colvarCompanyTel.DataType = DbType.String;
			colvarCompanyTel.MaxLength = 50;
			colvarCompanyTel.AutoIncrement = false;
			colvarCompanyTel.IsNullable = true;
			colvarCompanyTel.IsPrimaryKey = false;
			colvarCompanyTel.IsForeignKey = false;
			colvarCompanyTel.IsReadOnly = false;
                
			colvarCompanyTel.DefaultSetting = @"";
			colvarCompanyTel.ForeignKeyTableName = "";
            colvarCompanyTel.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyTel);
			
			TableSchema.TableColumn colvarCompanyPhone = new TableSchema.TableColumn(this);
			colvarCompanyPhone.ColumnName = "CompanyPhone";
			colvarCompanyPhone.DataType = DbType.String;
			colvarCompanyPhone.MaxLength = 50;
			colvarCompanyPhone.AutoIncrement = false;
			colvarCompanyPhone.IsNullable = true;
			colvarCompanyPhone.IsPrimaryKey = false;
			colvarCompanyPhone.IsForeignKey = false;
			colvarCompanyPhone.IsReadOnly = false;
                
			colvarCompanyPhone.DefaultSetting = @"";
			colvarCompanyPhone.ForeignKeyTableName = "";
            colvarCompanyPhone.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyPhone);
			
			TableSchema.TableColumn colvarCompanyMsn = new TableSchema.TableColumn(this);
			colvarCompanyMsn.ColumnName = "CompanyMsn";
			colvarCompanyMsn.DataType = DbType.String;
			colvarCompanyMsn.MaxLength = 50;
			colvarCompanyMsn.AutoIncrement = false;
			colvarCompanyMsn.IsNullable = true;
			colvarCompanyMsn.IsPrimaryKey = false;
			colvarCompanyMsn.IsForeignKey = false;
			colvarCompanyMsn.IsReadOnly = false;
                
			colvarCompanyMsn.DefaultSetting = @"";
			colvarCompanyMsn.ForeignKeyTableName = "";
            colvarCompanyMsn.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyMsn);
			
			TableSchema.TableColumn colvarCompanySkype = new TableSchema.TableColumn(this);
			colvarCompanySkype.ColumnName = "CompanySkype";
			colvarCompanySkype.DataType = DbType.String;
			colvarCompanySkype.MaxLength = 50;
			colvarCompanySkype.AutoIncrement = false;
			colvarCompanySkype.IsNullable = true;
			colvarCompanySkype.IsPrimaryKey = false;
			colvarCompanySkype.IsForeignKey = false;
			colvarCompanySkype.IsReadOnly = false;
                
			colvarCompanySkype.DefaultSetting = @"";
			colvarCompanySkype.ForeignKeyTableName = "";
            colvarCompanySkype.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanySkype);
			
			TableSchema.TableColumn colvarCompanyPhoto = new TableSchema.TableColumn(this);
			colvarCompanyPhoto.ColumnName = "CompanyPhoto";
			colvarCompanyPhoto.DataType = DbType.String;
			colvarCompanyPhoto.MaxLength = 50;
			colvarCompanyPhoto.AutoIncrement = false;
			colvarCompanyPhoto.IsNullable = true;
			colvarCompanyPhoto.IsPrimaryKey = false;
			colvarCompanyPhoto.IsForeignKey = false;
			colvarCompanyPhoto.IsReadOnly = false;
                
			colvarCompanyPhoto.DefaultSetting = @"";
			colvarCompanyPhoto.ForeignKeyTableName = "";
            colvarCompanyPhoto.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyPhoto);
			
			TableSchema.TableColumn colvarCompanyIntro = new TableSchema.TableColumn(this);
			colvarCompanyIntro.ColumnName = "CompanyIntro";
			colvarCompanyIntro.DataType = DbType.String;
			colvarCompanyIntro.MaxLength = 250;
			colvarCompanyIntro.AutoIncrement = false;
			colvarCompanyIntro.IsNullable = true;
			colvarCompanyIntro.IsPrimaryKey = false;
			colvarCompanyIntro.IsForeignKey = false;
			colvarCompanyIntro.IsReadOnly = false;
                
			colvarCompanyIntro.DefaultSetting = @"";
			colvarCompanyIntro.ForeignKeyTableName = "";
            colvarCompanyIntro.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyIntro);
			
			TableSchema.TableColumn colvarCompanyContent = new TableSchema.TableColumn(this);
			colvarCompanyContent.ColumnName = "CompanyContent";
			colvarCompanyContent.DataType = DbType.String;
			colvarCompanyContent.MaxLength = 1073741823;
			colvarCompanyContent.AutoIncrement = false;
			colvarCompanyContent.IsNullable = true;
			colvarCompanyContent.IsPrimaryKey = false;
			colvarCompanyContent.IsForeignKey = false;
			colvarCompanyContent.IsReadOnly = false;
                
			colvarCompanyContent.DefaultSetting = @"";
			colvarCompanyContent.ForeignKeyTableName = "";
            colvarCompanyContent.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyContent);
			
			TableSchema.TableColumn colvarIsUseFinger = new TableSchema.TableColumn(this);
			colvarIsUseFinger.ColumnName = "IsUseFinger";
			colvarIsUseFinger.DataType = DbType.Boolean;
			colvarIsUseFinger.MaxLength = 0;
			colvarIsUseFinger.AutoIncrement = false;
			colvarIsUseFinger.IsNullable = true;
			colvarIsUseFinger.IsPrimaryKey = false;
			colvarIsUseFinger.IsForeignKey = false;
			colvarIsUseFinger.IsReadOnly = false;
                
			
			colvarIsUseFinger.DefaultSetting = @"((0))";
			colvarIsUseFinger.ForeignKeyTableName = "";
            colvarIsUseFinger.ApplyExtendedProperties();
			this.Columns.Add(colvarIsUseFinger);
			
			TableSchema.TableColumn colvarCompanyDate = new TableSchema.TableColumn(this);
			colvarCompanyDate.ColumnName = "CompanyDate";
			colvarCompanyDate.DataType = DbType.DateTime;
			colvarCompanyDate.MaxLength = 0;
			colvarCompanyDate.AutoIncrement = false;
			colvarCompanyDate.IsNullable = true;
			colvarCompanyDate.IsPrimaryKey = false;
			colvarCompanyDate.IsForeignKey = false;
			colvarCompanyDate.IsReadOnly = false;
                
			colvarCompanyDate.DefaultSetting = @"";
			colvarCompanyDate.ForeignKeyTableName = "";
            colvarCompanyDate.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyDate);
			
			TableSchema.TableColumn colvarCompanyDateView = new TableSchema.TableColumn(this);
			colvarCompanyDateView.ColumnName = "CompanyDateView";
			colvarCompanyDateView.DataType = DbType.DateTime;
			colvarCompanyDateView.MaxLength = 0;
			colvarCompanyDateView.AutoIncrement = false;
			colvarCompanyDateView.IsNullable = true;
			colvarCompanyDateView.IsPrimaryKey = false;
			colvarCompanyDateView.IsForeignKey = false;
			colvarCompanyDateView.IsReadOnly = false;
                
			colvarCompanyDateView.DefaultSetting = @"";
			colvarCompanyDateView.ForeignKeyTableName = "";
            colvarCompanyDateView.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyDateView);
			
			TableSchema.TableColumn colvarCompanyStatus = new TableSchema.TableColumn(this);
			colvarCompanyStatus.ColumnName = "CompanyStatus";
			colvarCompanyStatus.DataType = DbType.Int32;
			colvarCompanyStatus.MaxLength = 0;
			colvarCompanyStatus.AutoIncrement = false;
			colvarCompanyStatus.IsNullable = true;
			colvarCompanyStatus.IsPrimaryKey = false;
			colvarCompanyStatus.IsForeignKey = false;
			colvarCompanyStatus.IsReadOnly = false;
                
			colvarCompanyStatus.DefaultSetting = @"";
			colvarCompanyStatus.ForeignKeyTableName = "";
            colvarCompanyStatus.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyStatus);
			
			TableSchema.TableColumn colvarCompanyMemo = new TableSchema.TableColumn(this);
			colvarCompanyMemo.ColumnName = "CompanyMemo";
			colvarCompanyMemo.DataType = DbType.String;
			colvarCompanyMemo.MaxLength = 50;
			colvarCompanyMemo.AutoIncrement = false;
			colvarCompanyMemo.IsNullable = true;
			colvarCompanyMemo.IsPrimaryKey = false;
			colvarCompanyMemo.IsForeignKey = false;
			colvarCompanyMemo.IsReadOnly = false;
                
			colvarCompanyMemo.DefaultSetting = @"";
			colvarCompanyMemo.ForeignKeyTableName = "";
            colvarCompanyMemo.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyMemo);
			
			TableSchema.TableColumn colvarCompanyRate = new TableSchema.TableColumn(this);
			colvarCompanyRate.ColumnName = "CompanyRate";
			colvarCompanyRate.DataType = DbType.String;
			colvarCompanyRate.MaxLength = 50;
			colvarCompanyRate.AutoIncrement = false;
			colvarCompanyRate.IsNullable = true;
			colvarCompanyRate.IsPrimaryKey = false;
			colvarCompanyRate.IsForeignKey = false;
			colvarCompanyRate.IsReadOnly = false;
                
			colvarCompanyRate.DefaultSetting = @"";
			colvarCompanyRate.ForeignKeyTableName = "";
            colvarCompanyRate.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyRate);
			
			TableSchema.TableColumn colvarCompanyRateSale = new TableSchema.TableColumn(this);
			colvarCompanyRateSale.ColumnName = "CompanyRateSale";
			colvarCompanyRateSale.DataType = DbType.String;
			colvarCompanyRateSale.MaxLength = 50;
			colvarCompanyRateSale.AutoIncrement = false;
			colvarCompanyRateSale.IsNullable = true;
			colvarCompanyRateSale.IsPrimaryKey = false;
			colvarCompanyRateSale.IsForeignKey = false;
			colvarCompanyRateSale.IsReadOnly = false;
                
			colvarCompanyRateSale.DefaultSetting = @"";
			colvarCompanyRateSale.ForeignKeyTableName = "";
            colvarCompanyRateSale.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyRateSale);
			
			TableSchema.TableColumn colvarCompanyRateMaster = new TableSchema.TableColumn(this);
			colvarCompanyRateMaster.ColumnName = "CompanyRateMaster";
			colvarCompanyRateMaster.DataType = DbType.Decimal;
			colvarCompanyRateMaster.MaxLength = 0;
			colvarCompanyRateMaster.AutoIncrement = false;
			colvarCompanyRateMaster.IsNullable = true;
			colvarCompanyRateMaster.IsPrimaryKey = false;
			colvarCompanyRateMaster.IsForeignKey = false;
			colvarCompanyRateMaster.IsReadOnly = false;
                
			
            colvarCompanyRateMaster.ExtendedProperties.Add(new TableSchema.ExtendedProperty("SSX_COLUMN_DISPLAY_NAME","佣金比例"));
            colvarCompanyRateMaster.DefaultSetting = @"";
			colvarCompanyRateMaster.ForeignKeyTableName = "";
            colvarCompanyRateMaster.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyRateMaster);
			
			TableSchema.TableColumn colvarCompanySaleCount = new TableSchema.TableColumn(this);
			colvarCompanySaleCount.ColumnName = "CompanySaleCount";
			colvarCompanySaleCount.DataType = DbType.Int32;
			colvarCompanySaleCount.MaxLength = 0;
			colvarCompanySaleCount.AutoIncrement = false;
			colvarCompanySaleCount.IsNullable = true;
			colvarCompanySaleCount.IsPrimaryKey = false;
			colvarCompanySaleCount.IsForeignKey = false;
			colvarCompanySaleCount.IsReadOnly = false;
                
			colvarCompanySaleCount.DefaultSetting = @"";
			colvarCompanySaleCount.ForeignKeyTableName = "";
            colvarCompanySaleCount.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanySaleCount);
			
			TableSchema.TableColumn colvarCompanySaleSum = new TableSchema.TableColumn(this);
			colvarCompanySaleSum.ColumnName = "CompanySaleSum";
			colvarCompanySaleSum.DataType = DbType.Decimal;
			colvarCompanySaleSum.MaxLength = 0;
			colvarCompanySaleSum.AutoIncrement = false;
			colvarCompanySaleSum.IsNullable = true;
			colvarCompanySaleSum.IsPrimaryKey = false;
			colvarCompanySaleSum.IsForeignKey = false;
			colvarCompanySaleSum.IsReadOnly = false;
                
			colvarCompanySaleSum.DefaultSetting = @"";
			colvarCompanySaleSum.ForeignKeyTableName = "";
            colvarCompanySaleSum.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanySaleSum);
			
			TableSchema.TableColumn colvarCompanyBalance = new TableSchema.TableColumn(this);
			colvarCompanyBalance.ColumnName = "CompanyBalance";
			colvarCompanyBalance.DataType = DbType.Decimal;
			colvarCompanyBalance.MaxLength = 0;
			colvarCompanyBalance.AutoIncrement = false;
			colvarCompanyBalance.IsNullable = true;
			colvarCompanyBalance.IsPrimaryKey = false;
			colvarCompanyBalance.IsForeignKey = false;
			colvarCompanyBalance.IsReadOnly = false;
                
			colvarCompanyBalance.DefaultSetting = @"";
			colvarCompanyBalance.ForeignKeyTableName = "";
            colvarCompanyBalance.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyBalance);
			
			TableSchema.TableColumn colvarCompanyBalanceCash = new TableSchema.TableColumn(this);
			colvarCompanyBalanceCash.ColumnName = "CompanyBalanceCash";
			colvarCompanyBalanceCash.DataType = DbType.Decimal;
			colvarCompanyBalanceCash.MaxLength = 0;
			colvarCompanyBalanceCash.AutoIncrement = false;
			colvarCompanyBalanceCash.IsNullable = true;
			colvarCompanyBalanceCash.IsPrimaryKey = false;
			colvarCompanyBalanceCash.IsForeignKey = false;
			colvarCompanyBalanceCash.IsReadOnly = false;
                
			colvarCompanyBalanceCash.DefaultSetting = @"";
			colvarCompanyBalanceCash.ForeignKeyTableName = "";
            colvarCompanyBalanceCash.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyBalanceCash);
			
			TableSchema.TableColumn colvarCompanyFacebookCount = new TableSchema.TableColumn(this);
			colvarCompanyFacebookCount.ColumnName = "CompanyFacebookCount";
			colvarCompanyFacebookCount.DataType = DbType.Int32;
			colvarCompanyFacebookCount.MaxLength = 0;
			colvarCompanyFacebookCount.AutoIncrement = false;
			colvarCompanyFacebookCount.IsNullable = true;
			colvarCompanyFacebookCount.IsPrimaryKey = false;
			colvarCompanyFacebookCount.IsForeignKey = false;
			colvarCompanyFacebookCount.IsReadOnly = false;
                
			colvarCompanyFacebookCount.DefaultSetting = @"";
			colvarCompanyFacebookCount.ForeignKeyTableName = "";
            colvarCompanyFacebookCount.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyFacebookCount);
			
			TableSchema.TableColumn colvarCompanyToken = new TableSchema.TableColumn(this);
			colvarCompanyToken.ColumnName = "CompanyToken";
			colvarCompanyToken.DataType = DbType.AnsiString;
			colvarCompanyToken.MaxLength = 1000;
			colvarCompanyToken.AutoIncrement = false;
			colvarCompanyToken.IsNullable = true;
			colvarCompanyToken.IsPrimaryKey = false;
			colvarCompanyToken.IsForeignKey = false;
			colvarCompanyToken.IsReadOnly = false;
                
			colvarCompanyToken.DefaultSetting = @"";
			colvarCompanyToken.ForeignKeyTableName = "";
            colvarCompanyToken.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyToken);
			
			TableSchema.TableColumn colvarCompanyType = new TableSchema.TableColumn(this);
			colvarCompanyType.ColumnName = "CompanyType";
			colvarCompanyType.DataType = DbType.Int32;
			colvarCompanyType.MaxLength = 0;
			colvarCompanyType.AutoIncrement = false;
			colvarCompanyType.IsNullable = true;
			colvarCompanyType.IsPrimaryKey = false;
			colvarCompanyType.IsForeignKey = false;
			colvarCompanyType.IsReadOnly = false;
                
			colvarCompanyType.DefaultSetting = @"";
			colvarCompanyType.ForeignKeyTableName = "";
            colvarCompanyType.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyType);
			
			TableSchema.TableColumn colvarCompanyItem = new TableSchema.TableColumn(this);
			colvarCompanyItem.ColumnName = "CompanyItem";
			colvarCompanyItem.DataType = DbType.String;
			colvarCompanyItem.MaxLength = 1000;
			colvarCompanyItem.AutoIncrement = false;
			colvarCompanyItem.IsNullable = true;
			colvarCompanyItem.IsPrimaryKey = false;
			colvarCompanyItem.IsForeignKey = false;
			colvarCompanyItem.IsReadOnly = false;
                
			colvarCompanyItem.DefaultSetting = @"";
			colvarCompanyItem.ForeignKeyTableName = "";
            colvarCompanyItem.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyItem);
			
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
			
			TableSchema.TableColumn colvarCompanyWorkTime = new TableSchema.TableColumn(this);
			colvarCompanyWorkTime.ColumnName = "CompanyWorkTime";
			colvarCompanyWorkTime.DataType = DbType.String;
			colvarCompanyWorkTime.MaxLength = 500;
			colvarCompanyWorkTime.AutoIncrement = false;
			colvarCompanyWorkTime.IsNullable = true;
			colvarCompanyWorkTime.IsPrimaryKey = false;
			colvarCompanyWorkTime.IsForeignKey = false;
			colvarCompanyWorkTime.IsReadOnly = false;
                
			colvarCompanyWorkTime.DefaultSetting = @"";
			colvarCompanyWorkTime.ForeignKeyTableName = "";
            colvarCompanyWorkTime.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyWorkTime);
			
			TableSchema.TableColumn colvarCompanyServices = new TableSchema.TableColumn(this);
			colvarCompanyServices.ColumnName = "CompanyServices";
			colvarCompanyServices.DataType = DbType.String;
			colvarCompanyServices.MaxLength = 500;
			colvarCompanyServices.AutoIncrement = false;
			colvarCompanyServices.IsNullable = true;
			colvarCompanyServices.IsPrimaryKey = false;
			colvarCompanyServices.IsForeignKey = false;
			colvarCompanyServices.IsReadOnly = false;
                
			colvarCompanyServices.DefaultSetting = @"";
			colvarCompanyServices.ForeignKeyTableName = "";
            colvarCompanyServices.ApplyExtendedProperties();
			this.Columns.Add(colvarCompanyServices);
			
			TableSchema.TableColumn colvarMsnPhoneNum = new TableSchema.TableColumn(this);
			colvarMsnPhoneNum.ColumnName = "MsnPhoneNum";
			colvarMsnPhoneNum.DataType = DbType.String;
			colvarMsnPhoneNum.MaxLength = 15;
			colvarMsnPhoneNum.AutoIncrement = false;
			colvarMsnPhoneNum.IsNullable = true;
			colvarMsnPhoneNum.IsPrimaryKey = false;
			colvarMsnPhoneNum.IsForeignKey = false;
			colvarMsnPhoneNum.IsReadOnly = false;
                
			colvarMsnPhoneNum.DefaultSetting = @"";
			colvarMsnPhoneNum.ForeignKeyTableName = "";
            colvarMsnPhoneNum.ApplyExtendedProperties();
			this.Columns.Add(colvarMsnPhoneNum);
			
			TableSchema.TableColumn colvarOrderElapsed = new TableSchema.TableColumn(this);
			colvarOrderElapsed.ColumnName = "OrderElapsed";
			colvarOrderElapsed.DataType = DbType.String;
			colvarOrderElapsed.MaxLength = 10;
			colvarOrderElapsed.AutoIncrement = false;
			colvarOrderElapsed.IsNullable = true;
			colvarOrderElapsed.IsPrimaryKey = false;
			colvarOrderElapsed.IsForeignKey = false;
			colvarOrderElapsed.IsReadOnly = false;
                
			colvarOrderElapsed.DefaultSetting = @"";
			colvarOrderElapsed.ForeignKeyTableName = "";
            colvarOrderElapsed.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderElapsed);
			
			TableSchema.TableColumn colvarOrderMaxAmount = new TableSchema.TableColumn(this);
			colvarOrderMaxAmount.ColumnName = "OrderMaxAmount";
			colvarOrderMaxAmount.DataType = DbType.Int32;
			colvarOrderMaxAmount.MaxLength = 0;
			colvarOrderMaxAmount.AutoIncrement = false;
			colvarOrderMaxAmount.IsNullable = true;
			colvarOrderMaxAmount.IsPrimaryKey = false;
			colvarOrderMaxAmount.IsForeignKey = false;
			colvarOrderMaxAmount.IsReadOnly = false;
                
			colvarOrderMaxAmount.DefaultSetting = @"";
			colvarOrderMaxAmount.ForeignKeyTableName = "";
            colvarOrderMaxAmount.ApplyExtendedProperties();
			this.Columns.Add(colvarOrderMaxAmount);
			
			TableSchema.TableColumn colvarIsUseMsg = new TableSchema.TableColumn(this);
			colvarIsUseMsg.ColumnName = "IsUseMsg";
			colvarIsUseMsg.DataType = DbType.Boolean;
			colvarIsUseMsg.MaxLength = 0;
			colvarIsUseMsg.AutoIncrement = false;
			colvarIsUseMsg.IsNullable = true;
			colvarIsUseMsg.IsPrimaryKey = false;
			colvarIsUseMsg.IsForeignKey = false;
			colvarIsUseMsg.IsReadOnly = false;
                
			colvarIsUseMsg.DefaultSetting = @"";
			colvarIsUseMsg.ForeignKeyTableName = "";
            colvarIsUseMsg.ApplyExtendedProperties();
			this.Columns.Add(colvarIsUseMsg);
			
			TableSchema.TableColumn colvarOnSetSum = new TableSchema.TableColumn(this);
			colvarOnSetSum.ColumnName = "OnSetSum";
			colvarOnSetSum.DataType = DbType.Decimal;
			colvarOnSetSum.MaxLength = 0;
			colvarOnSetSum.AutoIncrement = false;
			colvarOnSetSum.IsNullable = true;
			colvarOnSetSum.IsPrimaryKey = false;
			colvarOnSetSum.IsForeignKey = false;
			colvarOnSetSum.IsReadOnly = false;
                
			colvarOnSetSum.DefaultSetting = @"";
			colvarOnSetSum.ForeignKeyTableName = "";
            colvarOnSetSum.ApplyExtendedProperties();
			this.Columns.Add(colvarOnSetSum);
			
			TableSchema.TableColumn colvarServiceSum = new TableSchema.TableColumn(this);
			colvarServiceSum.ColumnName = "ServiceSum";
			colvarServiceSum.DataType = DbType.Decimal;
			colvarServiceSum.MaxLength = 0;
			colvarServiceSum.AutoIncrement = false;
			colvarServiceSum.IsNullable = true;
			colvarServiceSum.IsPrimaryKey = false;
			colvarServiceSum.IsForeignKey = false;
			colvarServiceSum.IsReadOnly = false;
                
			colvarServiceSum.DefaultSetting = @"";
			colvarServiceSum.ForeignKeyTableName = "";
            colvarServiceSum.ApplyExtendedProperties();
			this.Columns.Add(colvarServiceSum);
			
			TableSchema.TableColumn colvarMenuDate = new TableSchema.TableColumn(this);
			colvarMenuDate.ColumnName = "MenuDate";
			colvarMenuDate.DataType = DbType.DateTime;
			colvarMenuDate.MaxLength = 0;
			colvarMenuDate.AutoIncrement = false;
			colvarMenuDate.IsNullable = true;
			colvarMenuDate.IsPrimaryKey = false;
			colvarMenuDate.IsForeignKey = false;
			colvarMenuDate.IsReadOnly = false;
                
			colvarMenuDate.DefaultSetting = @"";
			colvarMenuDate.ForeignKeyTableName = "";
            colvarMenuDate.ApplyExtendedProperties();
			this.Columns.Add(colvarMenuDate);
			
			TableSchema.TableColumn colvarSetTopDate = new TableSchema.TableColumn(this);
			colvarSetTopDate.ColumnName = "SetTopDate";
			colvarSetTopDate.DataType = DbType.DateTime;
			colvarSetTopDate.MaxLength = 0;
			colvarSetTopDate.AutoIncrement = false;
			colvarSetTopDate.IsNullable = true;
			colvarSetTopDate.IsPrimaryKey = false;
			colvarSetTopDate.IsForeignKey = false;
			colvarSetTopDate.IsReadOnly = false;
                
			colvarSetTopDate.DefaultSetting = @"";
			colvarSetTopDate.ForeignKeyTableName = "";
            colvarSetTopDate.ApplyExtendedProperties();
			this.Columns.Add(colvarSetTopDate);
			
			TableSchema.TableColumn colvarIsPoint = new TableSchema.TableColumn(this);
			colvarIsPoint.ColumnName = "IsPoint";
			colvarIsPoint.DataType = DbType.Boolean;
			colvarIsPoint.MaxLength = 0;
			colvarIsPoint.AutoIncrement = false;
			colvarIsPoint.IsNullable = true;
			colvarIsPoint.IsPrimaryKey = false;
			colvarIsPoint.IsForeignKey = false;
			colvarIsPoint.IsReadOnly = false;
                
			colvarIsPoint.DefaultSetting = @"";
			colvarIsPoint.ForeignKeyTableName = "";
            colvarIsPoint.ApplyExtendedProperties();
			this.Columns.Add(colvarIsPoint);
			
			TableSchema.TableColumn colvarIsOnSale = new TableSchema.TableColumn(this);
			colvarIsOnSale.ColumnName = "IsOnSale";
			colvarIsOnSale.DataType = DbType.Boolean;
			colvarIsOnSale.MaxLength = 0;
			colvarIsOnSale.AutoIncrement = false;
			colvarIsOnSale.IsNullable = true;
			colvarIsOnSale.IsPrimaryKey = false;
			colvarIsOnSale.IsForeignKey = false;
			colvarIsOnSale.IsReadOnly = false;
                
			colvarIsOnSale.DefaultSetting = @"";
			colvarIsOnSale.ForeignKeyTableName = "";
            colvarIsOnSale.ApplyExtendedProperties();
			this.Columns.Add(colvarIsOnSale);
			
		}
		#endregion
	}
}