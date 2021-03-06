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
// <auto-generated />
namespace Eleooo.DAL
{
	/// <summary>
	/// Strongly-typed collection for the SysMember class.
	/// </summary>
    [Serializable]
	public partial class SysMemberCollection : ActiveList<SysMember, SysMemberCollection>
	{	   
		public SysMemberCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysMemberCollection</returns>
		public SysMemberCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysMember o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the Sys_Member table.
	/// </summary>
	[Serializable]
	public partial class SysMember : ActiveRecord<SysMember>, IActiveRecord
	{
		#region .ctors and Default Settings
		static SysMember( )
        {
            BaseSchema = DB.GetSchema("Sys_Member");
        }
		public SysMember()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysMember(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("Sys_Member");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysMember(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Member");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysMember(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Member");
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
                {
                    BaseSchema = DB.GetSchema("Sys_Member");
                }
				return BaseSchema;
			}
		}
		
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public int Id 
		{
			get { return GetColumnValue<int>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("MemberEmail")]
		[Bindable(true)]
		public string MemberEmail 
		{
			get { return GetColumnValue<string>(Columns.MemberEmail); }
			set { SetColumnValue(Columns.MemberEmail, value); }
		}
		  
		[XmlAttribute("MemberFullname")]
		[Bindable(true)]
		public string MemberFullname 
		{
			get { return GetColumnValue<string>(Columns.MemberFullname); }
			set { SetColumnValue(Columns.MemberFullname, value); }
		}
		  
		[XmlAttribute("MemberPwd")]
		[Bindable(true)]
		public string MemberPwd 
		{
			get { return GetColumnValue<string>(Columns.MemberPwd); }
			set { SetColumnValue(Columns.MemberPwd, value); }
		}
		  
		[XmlAttribute("MemberFinger")]
		[Bindable(true)]
		public string MemberFinger 
		{
			get { return GetColumnValue<string>(Columns.MemberFinger); }
			set { SetColumnValue(Columns.MemberFinger, value); }
		}
		  
		[XmlAttribute("MemberPhoneNumber")]
		[Bindable(true)]
		public string MemberPhoneNumber 
		{
			get { return GetColumnValue<string>(Columns.MemberPhoneNumber); }
			set { SetColumnValue(Columns.MemberPhoneNumber, value); }
		}
		  
		[XmlAttribute("MemberGrade")]
		[Bindable(true)]
		public int? MemberGrade 
		{
			get { return GetColumnValue<int?>(Columns.MemberGrade); }
			set { SetColumnValue(Columns.MemberGrade, value); }
		}
		  
		[XmlAttribute("MemberAddress1")]
		[Bindable(true)]
		public string MemberAddress1 
		{
			get { return GetColumnValue<string>(Columns.MemberAddress1); }
			set { SetColumnValue(Columns.MemberAddress1, value); }
		}
		  
		[XmlAttribute("MemberAddress2")]
		[Bindable(true)]
		public string MemberAddress2 
		{
			get { return GetColumnValue<string>(Columns.MemberAddress2); }
			set { SetColumnValue(Columns.MemberAddress2, value); }
		}
		  
		[XmlAttribute("MemberCountry")]
		[Bindable(true)]
		public string MemberCountry 
		{
			get { return GetColumnValue<string>(Columns.MemberCountry); }
			set { SetColumnValue(Columns.MemberCountry, value); }
		}
		  
		[XmlAttribute("MemberState")]
		[Bindable(true)]
		public string MemberState 
		{
			get { return GetColumnValue<string>(Columns.MemberState); }
			set { SetColumnValue(Columns.MemberState, value); }
		}
		  
		[XmlAttribute("MemberCity")]
		[Bindable(true)]
		public int? MemberCity 
		{
			get { return GetColumnValue<int?>(Columns.MemberCity); }
			set { SetColumnValue(Columns.MemberCity, value); }
		}
		  
		[XmlAttribute("MemberArea")]
		[Bindable(true)]
		public string MemberArea 
		{
			get { return GetColumnValue<string>(Columns.MemberArea); }
			set { SetColumnValue(Columns.MemberArea, value); }
		}
		  
		[XmlAttribute("MemberLocation")]
		[Bindable(true)]
		public string MemberLocation 
		{
			get { return GetColumnValue<string>(Columns.MemberLocation); }
			set { SetColumnValue(Columns.MemberLocation, value); }
		}
		  
		[XmlAttribute("AreaDepth1")]
		[Bindable(true)]
		public string AreaDepth1 
		{
			get { return GetColumnValue<string>(Columns.AreaDepth1); }
			set { SetColumnValue(Columns.AreaDepth1, value); }
		}
		  
		[XmlAttribute("AreaDepth2")]
		[Bindable(true)]
		public string AreaDepth2 
		{
			get { return GetColumnValue<string>(Columns.AreaDepth2); }
			set { SetColumnValue(Columns.AreaDepth2, value); }
		}
		  
		[XmlAttribute("AreaDepth3")]
		[Bindable(true)]
		public string AreaDepth3 
		{
			get { return GetColumnValue<string>(Columns.AreaDepth3); }
			set { SetColumnValue(Columns.AreaDepth3, value); }
		}
		  
		[XmlAttribute("AreaModifyDate")]
		[Bindable(true)]
		public DateTime? AreaModifyDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.AreaModifyDate); }
			set { SetColumnValue(Columns.AreaModifyDate, value); }
		}
		  
		[XmlAttribute("MemberZip")]
		[Bindable(true)]
		public string MemberZip 
		{
			get { return GetColumnValue<string>(Columns.MemberZip); }
			set { SetColumnValue(Columns.MemberZip, value); }
		}
		  
		[XmlAttribute("MemberDate")]
		[Bindable(true)]
		public DateTime? MemberDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.MemberDate); }
			set { SetColumnValue(Columns.MemberDate, value); }
		}
		  
		[XmlAttribute("MemberSum")]
		[Bindable(true)]
		public decimal? MemberSum 
		{
			get { return GetColumnValue<decimal?>(Columns.MemberSum); }
			set { SetColumnValue(Columns.MemberSum, value); }
		}
		  
		[XmlAttribute("MemberBalance")]
		[Bindable(true)]
		public decimal? MemberBalance 
		{
			get { return GetColumnValue<decimal?>(Columns.MemberBalance); }
			set { SetColumnValue(Columns.MemberBalance, value); }
		}
		  
		[XmlAttribute("MemberBalanceCash")]
		[Bindable(true)]
		public decimal? MemberBalanceCash 
		{
			get { return GetColumnValue<decimal?>(Columns.MemberBalanceCash); }
			set { SetColumnValue(Columns.MemberBalanceCash, value); }
		}
		  
		[XmlAttribute("MemberStatus")]
		[Bindable(true)]
		public int? MemberStatus 
		{
			get { return GetColumnValue<int?>(Columns.MemberStatus); }
			set { SetColumnValue(Columns.MemberStatus, value); }
		}
		  
		[XmlAttribute("MemberMemo")]
		[Bindable(true)]
		public string MemberMemo 
		{
			get { return GetColumnValue<string>(Columns.MemberMemo); }
			set { SetColumnValue(Columns.MemberMemo, value); }
		}
		  
		[XmlAttribute("MemberPid")]
		[Bindable(true)]
		public int? MemberPid 
		{
			get { return GetColumnValue<int?>(Columns.MemberPid); }
			set { SetColumnValue(Columns.MemberPid, value); }
		}
		  
		[XmlAttribute("MemberCompanyID")]
		[Bindable(true)]
		public int? MemberCompanyID 
		{
			get { return GetColumnValue<int?>(Columns.MemberCompanyID); }
			set { SetColumnValue(Columns.MemberCompanyID, value); }
		}
		  
		[XmlAttribute("MemberSex")]
		[Bindable(true)]
		public bool? MemberSex 
		{
			get { return GetColumnValue<bool?>(Columns.MemberSex); }
			set { SetColumnValue(Columns.MemberSex, value); }
		}
		  
		[XmlAttribute("MemberBirthday")]
		[Bindable(true)]
		public DateTime? MemberBirthday 
		{
			get { return GetColumnValue<DateTime?>(Columns.MemberBirthday); }
			set { SetColumnValue(Columns.MemberBirthday, value); }
		}
		  
		[XmlAttribute("MemberRoleId")]
		[Bindable(true)]
		public int? MemberRoleId 
		{
			get { return GetColumnValue<int?>(Columns.MemberRoleId); }
			set { SetColumnValue(Columns.MemberRoleId, value); }
		}
		  
		[XmlAttribute("CompanyRoleId")]
		[Bindable(true)]
		public int? CompanyRoleId 
		{
			get { return GetColumnValue<int?>(Columns.CompanyRoleId); }
			set { SetColumnValue(Columns.CompanyRoleId, value); }
		}
		  
		[XmlAttribute("AdminRoleId")]
		[Bindable(true)]
		public int? AdminRoleId 
		{
			get { return GetColumnValue<int?>(Columns.AdminRoleId); }
			set { SetColumnValue(Columns.AdminRoleId, value); }
		}
		  
		[XmlAttribute("LastLoginDate")]
		[Bindable(true)]
		public DateTime? LastLoginDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.LastLoginDate); }
			set { SetColumnValue(Columns.LastLoginDate, value); }
		}
		  
		[XmlAttribute("LastLoginSubSys")]
		[Bindable(true)]
		public int? LastLoginSubSys 
		{
			get { return GetColumnValue<int?>(Columns.LastLoginSubSys); }
			set { SetColumnValue(Columns.LastLoginSubSys, value); }
		}
		  
		[XmlAttribute("CreatedBy")]
		[Bindable(true)]
		public int? CreatedBy 
		{
			get { return GetColumnValue<int?>(Columns.CreatedBy); }
			set { SetColumnValue(Columns.CreatedBy, value); }
		}
		  
		[XmlAttribute("CreatedOn")]
		[Bindable(true)]
		public DateTime? CreatedOn 
		{
			get { return GetColumnValue<DateTime?>(Columns.CreatedOn); }
			set { SetColumnValue(Columns.CreatedOn, value); }
		}
		  
		[XmlAttribute("ModifiedBy")]
		[Bindable(true)]
		public int? ModifiedBy 
		{
			get { return GetColumnValue<int?>(Columns.ModifiedBy); }
			set { SetColumnValue(Columns.ModifiedBy, value); }
		}
		  
		[XmlAttribute("ModifiedOn")]
		[Bindable(true)]
		public DateTime? ModifiedOn 
		{
			get { return GetColumnValue<DateTime?>(Columns.ModifiedOn); }
			set { SetColumnValue(Columns.ModifiedOn, value); }
		}
		  
		[XmlAttribute("CompanyId")]
		[Bindable(true)]
		public int? CompanyId 
		{
			get { return GetColumnValue<int?>(Columns.CompanyId); }
			set { SetColumnValue(Columns.CompanyId, value); }
		}
		  
		[XmlAttribute("MemberMsnPhone")]
		[Bindable(true)]
		public string MemberMsnPhone 
		{
			get { return GetColumnValue<string>(Columns.MemberMsnPhone); }
			set { SetColumnValue(Columns.MemberMsnPhone, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varMemberEmail,string varMemberFullname,string varMemberPwd,string varMemberFinger,string varMemberPhoneNumber,int? varMemberGrade,string varMemberAddress1,string varMemberAddress2,string varMemberCountry,string varMemberState,int? varMemberCity,string varMemberArea,string varMemberLocation,string varAreaDepth1,string varAreaDepth2,string varAreaDepth3,DateTime? varAreaModifyDate,string varMemberZip,DateTime? varMemberDate,decimal? varMemberSum,decimal? varMemberBalance,decimal? varMemberBalanceCash,int? varMemberStatus,string varMemberMemo,int? varMemberPid,int? varMemberCompanyID,bool? varMemberSex,DateTime? varMemberBirthday,int? varMemberRoleId,int? varCompanyRoleId,int? varAdminRoleId,DateTime? varLastLoginDate,int? varLastLoginSubSys,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn,int? varCompanyId,string varMemberMsnPhone)
		{
			SysMember item = new SysMember();
			
			item.MemberEmail = varMemberEmail;
			
			item.MemberFullname = varMemberFullname;
			
			item.MemberPwd = varMemberPwd;
			
			item.MemberFinger = varMemberFinger;
			
			item.MemberPhoneNumber = varMemberPhoneNumber;
			
			item.MemberGrade = varMemberGrade;
			
			item.MemberAddress1 = varMemberAddress1;
			
			item.MemberAddress2 = varMemberAddress2;
			
			item.MemberCountry = varMemberCountry;
			
			item.MemberState = varMemberState;
			
			item.MemberCity = varMemberCity;
			
			item.MemberArea = varMemberArea;
			
			item.MemberLocation = varMemberLocation;
			
			item.AreaDepth1 = varAreaDepth1;
			
			item.AreaDepth2 = varAreaDepth2;
			
			item.AreaDepth3 = varAreaDepth3;
			
			item.AreaModifyDate = varAreaModifyDate;
			
			item.MemberZip = varMemberZip;
			
			item.MemberDate = varMemberDate;
			
			item.MemberSum = varMemberSum;
			
			item.MemberBalance = varMemberBalance;
			
			item.MemberBalanceCash = varMemberBalanceCash;
			
			item.MemberStatus = varMemberStatus;
			
			item.MemberMemo = varMemberMemo;
			
			item.MemberPid = varMemberPid;
			
			item.MemberCompanyID = varMemberCompanyID;
			
			item.MemberSex = varMemberSex;
			
			item.MemberBirthday = varMemberBirthday;
			
			item.MemberRoleId = varMemberRoleId;
			
			item.CompanyRoleId = varCompanyRoleId;
			
			item.AdminRoleId = varAdminRoleId;
			
			item.LastLoginDate = varLastLoginDate;
			
			item.LastLoginSubSys = varLastLoginSubSys;
			
			item.CreatedBy = varCreatedBy;
			
			item.CreatedOn = varCreatedOn;
			
			item.ModifiedBy = varModifiedBy;
			
			item.ModifiedOn = varModifiedOn;
			
			item.CompanyId = varCompanyId;
			
			item.MemberMsnPhone = varMemberMsnPhone;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varMemberEmail,string varMemberFullname,string varMemberPwd,string varMemberFinger,string varMemberPhoneNumber,int? varMemberGrade,string varMemberAddress1,string varMemberAddress2,string varMemberCountry,string varMemberState,int? varMemberCity,string varMemberArea,string varMemberLocation,string varAreaDepth1,string varAreaDepth2,string varAreaDepth3,DateTime? varAreaModifyDate,string varMemberZip,DateTime? varMemberDate,decimal? varMemberSum,decimal? varMemberBalance,decimal? varMemberBalanceCash,int? varMemberStatus,string varMemberMemo,int? varMemberPid,int? varMemberCompanyID,bool? varMemberSex,DateTime? varMemberBirthday,int? varMemberRoleId,int? varCompanyRoleId,int? varAdminRoleId,DateTime? varLastLoginDate,int? varLastLoginSubSys,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn,int? varCompanyId,string varMemberMsnPhone)
		{
			SysMember item = new SysMember();
			
				item.Id = varId;
			
				item.MemberEmail = varMemberEmail;
			
				item.MemberFullname = varMemberFullname;
			
				item.MemberPwd = varMemberPwd;
			
				item.MemberFinger = varMemberFinger;
			
				item.MemberPhoneNumber = varMemberPhoneNumber;
			
				item.MemberGrade = varMemberGrade;
			
				item.MemberAddress1 = varMemberAddress1;
			
				item.MemberAddress2 = varMemberAddress2;
			
				item.MemberCountry = varMemberCountry;
			
				item.MemberState = varMemberState;
			
				item.MemberCity = varMemberCity;
			
				item.MemberArea = varMemberArea;
			
				item.MemberLocation = varMemberLocation;
			
				item.AreaDepth1 = varAreaDepth1;
			
				item.AreaDepth2 = varAreaDepth2;
			
				item.AreaDepth3 = varAreaDepth3;
			
				item.AreaModifyDate = varAreaModifyDate;
			
				item.MemberZip = varMemberZip;
			
				item.MemberDate = varMemberDate;
			
				item.MemberSum = varMemberSum;
			
				item.MemberBalance = varMemberBalance;
			
				item.MemberBalanceCash = varMemberBalanceCash;
			
				item.MemberStatus = varMemberStatus;
			
				item.MemberMemo = varMemberMemo;
			
				item.MemberPid = varMemberPid;
			
				item.MemberCompanyID = varMemberCompanyID;
			
				item.MemberSex = varMemberSex;
			
				item.MemberBirthday = varMemberBirthday;
			
				item.MemberRoleId = varMemberRoleId;
			
				item.CompanyRoleId = varCompanyRoleId;
			
				item.AdminRoleId = varAdminRoleId;
			
				item.LastLoginDate = varLastLoginDate;
			
				item.LastLoginSubSys = varLastLoginSubSys;
			
				item.CreatedBy = varCreatedBy;
			
				item.CreatedOn = varCreatedOn;
			
				item.ModifiedBy = varModifiedBy;
			
				item.ModifiedOn = varModifiedOn;
			
				item.CompanyId = varCompanyId;
			
				item.MemberMsnPhone = varMemberMsnPhone;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberEmailColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberFullnameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberPwdColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberFingerColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberPhoneNumberColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberGradeColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberAddress1Column
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberAddress2Column
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberCountryColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberStateColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberCityColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberAreaColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberLocationColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn AreaDepth1Column
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn AreaDepth2Column
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn AreaDepth3Column
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn AreaModifyDateColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberZipColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberDateColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberSumColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberBalanceColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberBalanceCashColumn
        {
            get { return Schema.Columns[22]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberStatusColumn
        {
            get { return Schema.Columns[23]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberMemoColumn
        {
            get { return Schema.Columns[24]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberPidColumn
        {
            get { return Schema.Columns[25]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberCompanyIDColumn
        {
            get { return Schema.Columns[26]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberSexColumn
        {
            get { return Schema.Columns[27]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberBirthdayColumn
        {
            get { return Schema.Columns[28]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberRoleIdColumn
        {
            get { return Schema.Columns[29]; }
        }
        
        
        
        public static TableSchema.TableColumn CompanyRoleIdColumn
        {
            get { return Schema.Columns[30]; }
        }
        
        
        
        public static TableSchema.TableColumn AdminRoleIdColumn
        {
            get { return Schema.Columns[31]; }
        }
        
        
        
        public static TableSchema.TableColumn LastLoginDateColumn
        {
            get { return Schema.Columns[32]; }
        }
        
        
        
        public static TableSchema.TableColumn LastLoginSubSysColumn
        {
            get { return Schema.Columns[33]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedByColumn
        {
            get { return Schema.Columns[34]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedOnColumn
        {
            get { return Schema.Columns[35]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedByColumn
        {
            get { return Schema.Columns[36]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedOnColumn
        {
            get { return Schema.Columns[37]; }
        }
        
        
        
        public static TableSchema.TableColumn CompanyIdColumn
        {
            get { return Schema.Columns[38]; }
        }
        
        
        
        public static TableSchema.TableColumn MemberMsnPhoneColumn
        {
            get { return Schema.Columns[39]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string MemberEmail = @"MemberEmail";
			 public static string MemberFullname = @"MemberFullname";
			 public static string MemberPwd = @"MemberPwd";
			 public static string MemberFinger = @"MemberFinger";
			 public static string MemberPhoneNumber = @"MemberPhoneNumber";
			 public static string MemberGrade = @"MemberGrade";
			 public static string MemberAddress1 = @"MemberAddress1";
			 public static string MemberAddress2 = @"MemberAddress2";
			 public static string MemberCountry = @"MemberCountry";
			 public static string MemberState = @"MemberState";
			 public static string MemberCity = @"MemberCity";
			 public static string MemberArea = @"MemberArea";
			 public static string MemberLocation = @"MemberLocation";
			 public static string AreaDepth1 = @"AreaDepth1";
			 public static string AreaDepth2 = @"AreaDepth2";
			 public static string AreaDepth3 = @"AreaDepth3";
			 public static string AreaModifyDate = @"AreaModifyDate";
			 public static string MemberZip = @"MemberZip";
			 public static string MemberDate = @"MemberDate";
			 public static string MemberSum = @"MemberSum";
			 public static string MemberBalance = @"MemberBalance";
			 public static string MemberBalanceCash = @"MemberBalanceCash";
			 public static string MemberStatus = @"MemberStatus";
			 public static string MemberMemo = @"MemberMemo";
			 public static string MemberPid = @"MemberPid";
			 public static string MemberCompanyID = @"MemberCompanyID";
			 public static string MemberSex = @"MemberSex";
			 public static string MemberBirthday = @"MemberBirthday";
			 public static string MemberRoleId = @"MemberRole_ID";
			 public static string CompanyRoleId = @"CompanyRole_ID";
			 public static string AdminRoleId = @"AdminRole_ID";
			 public static string LastLoginDate = @"LastLoginDate";
			 public static string LastLoginSubSys = @"LastLoginSubSys";
			 public static string CreatedBy = @"CreatedBy";
			 public static string CreatedOn = @"CreatedOn";
			 public static string ModifiedBy = @"ModifiedBy";
			 public static string ModifiedOn = @"ModifiedOn";
			 public static string CompanyId = @"Company_ID";
			 public static string MemberMsnPhone = @"MemberMsnPhone";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
