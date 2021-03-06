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
	/// Strongly-typed collection for the SysCompanyAd class.
	/// </summary>
    [Serializable]
	public partial class SysCompanyAdCollection : ActiveList<SysCompanyAd, SysCompanyAdCollection>
	{	   
		public SysCompanyAdCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysCompanyAdCollection</returns>
		public SysCompanyAdCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysCompanyAd o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Company_Ads table.
	/// </summary>
	[Serializable]
	public partial class SysCompanyAd : ActiveRecord<SysCompanyAd>, IActiveRecord
	{
		#region .ctors and Default Settings
		static SysCompanyAd( )
        {
            BaseSchema = DB.GetSchema("Sys_Company_Ads");
        }
		public SysCompanyAd()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysCompanyAd(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("Sys_Company_Ads");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysCompanyAd(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Company_Ads");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysCompanyAd(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Company_Ads");
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
                    BaseSchema = DB.GetSchema("Sys_Company_Ads");
                }
				return BaseSchema;
			}
		}
		
		#endregion
		
		#region Props
		  
		[XmlAttribute("AdsID")]
		[Bindable(true)]
		public int AdsID 
		{
			get { return GetColumnValue<int>(Columns.AdsID); }
			set { SetColumnValue(Columns.AdsID, value); }
		}
		  
		[XmlAttribute("AdsTitle")]
		[Bindable(true)]
		public string AdsTitle 
		{
			get { return GetColumnValue<string>(Columns.AdsTitle); }
			set { SetColumnValue(Columns.AdsTitle, value); }
		}
		  
		[XmlAttribute("AdsCompanyID")]
		[Bindable(true)]
		public int? AdsCompanyID 
		{
			get { return GetColumnValue<int?>(Columns.AdsCompanyID); }
			set { SetColumnValue(Columns.AdsCompanyID, value); }
		}
		  
		[XmlAttribute("AdsDate")]
		[Bindable(true)]
		public DateTime? AdsDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.AdsDate); }
			set { SetColumnValue(Columns.AdsDate, value); }
		}
		  
		[XmlAttribute("AdsEndDate")]
		[Bindable(true)]
		public DateTime? AdsEndDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.AdsEndDate); }
			set { SetColumnValue(Columns.AdsEndDate, value); }
		}
		  
		[XmlAttribute("AreaDepth")]
		[Bindable(true)]
		public string AreaDepth 
		{
			get { return GetColumnValue<string>(Columns.AreaDepth); }
			set { SetColumnValue(Columns.AreaDepth, value); }
		}
		  
		[XmlAttribute("SexLimit")]
		[Bindable(true)]
		public int? SexLimit 
		{
			get { return GetColumnValue<int?>(Columns.SexLimit); }
			set { SetColumnValue(Columns.SexLimit, value); }
		}
		  
		[XmlAttribute("IsDeleted")]
		[Bindable(true)]
		public bool? IsDeleted 
		{
			get { return GetColumnValue<bool?>(Columns.IsDeleted); }
			set { SetColumnValue(Columns.IsDeleted, value); }
		}
		  
		[XmlAttribute("AdsClicked")]
		[Bindable(true)]
		public int? AdsClicked 
		{
			get { return GetColumnValue<int?>(Columns.AdsClicked); }
			set { SetColumnValue(Columns.AdsClicked, value); }
		}
		  
		[XmlAttribute("AdsPic")]
		[Bindable(true)]
		public string AdsPic 
		{
			get { return GetColumnValue<string>(Columns.AdsPic); }
			set { SetColumnValue(Columns.AdsPic, value); }
		}
		  
		[XmlAttribute("IsPass")]
		[Bindable(true)]
		public bool? IsPass 
		{
			get { return GetColumnValue<bool?>(Columns.IsPass); }
			set { SetColumnValue(Columns.IsPass, value); }
		}
		  
		[XmlAttribute("AdsClickLimit")]
		[Bindable(true)]
		public int? AdsClickLimit 
		{
			get { return GetColumnValue<int?>(Columns.AdsClickLimit); }
			set { SetColumnValue(Columns.AdsClickLimit, value); }
		}
		  
		[XmlAttribute("AdsMemberLimit")]
		[Bindable(true)]
		public int? AdsMemberLimit 
		{
			get { return GetColumnValue<int?>(Columns.AdsMemberLimit); }
			set { SetColumnValue(Columns.AdsMemberLimit, value); }
		}
		  
		[XmlAttribute("AdsDayLimitAmount")]
		[Bindable(true)]
		public int? AdsDayLimitAmount 
		{
			get { return GetColumnValue<int?>(Columns.AdsDayLimitAmount); }
			set { SetColumnValue(Columns.AdsDayLimitAmount, value); }
		}
		  
		[XmlAttribute("AdsDayLimitSum")]
		[Bindable(true)]
		public decimal? AdsDayLimitSum 
		{
			get { return GetColumnValue<decimal?>(Columns.AdsDayLimitSum); }
			set { SetColumnValue(Columns.AdsDayLimitSum, value); }
		}
		  
		[XmlAttribute("AdsQuestion")]
		[Bindable(true)]
		public string AdsQuestion 
		{
			get { return GetColumnValue<string>(Columns.AdsQuestion); }
			set { SetColumnValue(Columns.AdsQuestion, value); }
		}
		  
		[XmlAttribute("AdsAnswer1")]
		[Bindable(true)]
		public string AdsAnswer1 
		{
			get { return GetColumnValue<string>(Columns.AdsAnswer1); }
			set { SetColumnValue(Columns.AdsAnswer1, value); }
		}
		  
		[XmlAttribute("AdsAnswer2")]
		[Bindable(true)]
		public string AdsAnswer2 
		{
			get { return GetColumnValue<string>(Columns.AdsAnswer2); }
			set { SetColumnValue(Columns.AdsAnswer2, value); }
		}
		  
		[XmlAttribute("AdsAnswer3")]
		[Bindable(true)]
		public string AdsAnswer3 
		{
			get { return GetColumnValue<string>(Columns.AdsAnswer3); }
			set { SetColumnValue(Columns.AdsAnswer3, value); }
		}
		  
		[XmlAttribute("AdsAnswer4")]
		[Bindable(true)]
		public string AdsAnswer4 
		{
			get { return GetColumnValue<string>(Columns.AdsAnswer4); }
			set { SetColumnValue(Columns.AdsAnswer4, value); }
		}
		  
		[XmlAttribute("AdsRightAnswer")]
		[Bindable(true)]
		public int? AdsRightAnswer 
		{
			get { return GetColumnValue<int?>(Columns.AdsRightAnswer); }
			set { SetColumnValue(Columns.AdsRightAnswer, value); }
		}
		  
		[XmlAttribute("AdsPointSum")]
		[Bindable(true)]
		public decimal? AdsPointSum 
		{
			get { return GetColumnValue<decimal?>(Columns.AdsPointSum); }
			set { SetColumnValue(Columns.AdsPointSum, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varAdsTitle,int? varAdsCompanyID,DateTime? varAdsDate,DateTime? varAdsEndDate,string varAreaDepth,int? varSexLimit,bool? varIsDeleted,int? varAdsClicked,string varAdsPic,bool? varIsPass,int? varAdsClickLimit,int? varAdsMemberLimit,int? varAdsDayLimitAmount,decimal? varAdsDayLimitSum,string varAdsQuestion,string varAdsAnswer1,string varAdsAnswer2,string varAdsAnswer3,string varAdsAnswer4,int? varAdsRightAnswer,decimal? varAdsPointSum)
		{
			SysCompanyAd item = new SysCompanyAd();
			
			item.AdsTitle = varAdsTitle;
			
			item.AdsCompanyID = varAdsCompanyID;
			
			item.AdsDate = varAdsDate;
			
			item.AdsEndDate = varAdsEndDate;
			
			item.AreaDepth = varAreaDepth;
			
			item.SexLimit = varSexLimit;
			
			item.IsDeleted = varIsDeleted;
			
			item.AdsClicked = varAdsClicked;
			
			item.AdsPic = varAdsPic;
			
			item.IsPass = varIsPass;
			
			item.AdsClickLimit = varAdsClickLimit;
			
			item.AdsMemberLimit = varAdsMemberLimit;
			
			item.AdsDayLimitAmount = varAdsDayLimitAmount;
			
			item.AdsDayLimitSum = varAdsDayLimitSum;
			
			item.AdsQuestion = varAdsQuestion;
			
			item.AdsAnswer1 = varAdsAnswer1;
			
			item.AdsAnswer2 = varAdsAnswer2;
			
			item.AdsAnswer3 = varAdsAnswer3;
			
			item.AdsAnswer4 = varAdsAnswer4;
			
			item.AdsRightAnswer = varAdsRightAnswer;
			
			item.AdsPointSum = varAdsPointSum;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varAdsID,string varAdsTitle,int? varAdsCompanyID,DateTime? varAdsDate,DateTime? varAdsEndDate,string varAreaDepth,int? varSexLimit,bool? varIsDeleted,int? varAdsClicked,string varAdsPic,bool? varIsPass,int? varAdsClickLimit,int? varAdsMemberLimit,int? varAdsDayLimitAmount,decimal? varAdsDayLimitSum,string varAdsQuestion,string varAdsAnswer1,string varAdsAnswer2,string varAdsAnswer3,string varAdsAnswer4,int? varAdsRightAnswer,decimal? varAdsPointSum)
		{
			SysCompanyAd item = new SysCompanyAd();
			
				item.AdsID = varAdsID;
			
				item.AdsTitle = varAdsTitle;
			
				item.AdsCompanyID = varAdsCompanyID;
			
				item.AdsDate = varAdsDate;
			
				item.AdsEndDate = varAdsEndDate;
			
				item.AreaDepth = varAreaDepth;
			
				item.SexLimit = varSexLimit;
			
				item.IsDeleted = varIsDeleted;
			
				item.AdsClicked = varAdsClicked;
			
				item.AdsPic = varAdsPic;
			
				item.IsPass = varIsPass;
			
				item.AdsClickLimit = varAdsClickLimit;
			
				item.AdsMemberLimit = varAdsMemberLimit;
			
				item.AdsDayLimitAmount = varAdsDayLimitAmount;
			
				item.AdsDayLimitSum = varAdsDayLimitSum;
			
				item.AdsQuestion = varAdsQuestion;
			
				item.AdsAnswer1 = varAdsAnswer1;
			
				item.AdsAnswer2 = varAdsAnswer2;
			
				item.AdsAnswer3 = varAdsAnswer3;
			
				item.AdsAnswer4 = varAdsAnswer4;
			
				item.AdsRightAnswer = varAdsRightAnswer;
			
				item.AdsPointSum = varAdsPointSum;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn AdsIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsTitleColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsCompanyIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsDateColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsEndDateColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn AreaDepthColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SexLimitColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn IsDeletedColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsClickedColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsPicColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn IsPassColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsClickLimitColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsMemberLimitColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsDayLimitAmountColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsDayLimitSumColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsQuestionColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsAnswer1Column
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsAnswer2Column
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsAnswer3Column
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsAnswer4Column
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsRightAnswerColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsPointSumColumn
        {
            get { return Schema.Columns[21]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string AdsID = @"AdsID";
			 public static string AdsTitle = @"AdsTitle";
			 public static string AdsCompanyID = @"AdsCompanyID";
			 public static string AdsDate = @"AdsDate";
			 public static string AdsEndDate = @"AdsEndDate";
			 public static string AreaDepth = @"AreaDepth";
			 public static string SexLimit = @"SexLimit";
			 public static string IsDeleted = @"IsDeleted";
			 public static string AdsClicked = @"AdsClicked";
			 public static string AdsPic = @"AdsPic";
			 public static string IsPass = @"IsPass";
			 public static string AdsClickLimit = @"AdsClickLimit";
			 public static string AdsMemberLimit = @"AdsMemberLimit";
			 public static string AdsDayLimitAmount = @"AdsDayLimitAmount";
			 public static string AdsDayLimitSum = @"AdsDayLimitSum";
			 public static string AdsQuestion = @"AdsQuestion";
			 public static string AdsAnswer1 = @"AdsAnswer1";
			 public static string AdsAnswer2 = @"AdsAnswer2";
			 public static string AdsAnswer3 = @"AdsAnswer3";
			 public static string AdsAnswer4 = @"AdsAnswer4";
			 public static string AdsRightAnswer = @"AdsRightAnswer";
			 public static string AdsPointSum = @"AdsPointSum";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
