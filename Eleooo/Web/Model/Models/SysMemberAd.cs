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
	/// Strongly-typed collection for the SysMemberAd class.
	/// </summary>
    [Serializable]
	public partial class SysMemberAdCollection : ActiveList<SysMemberAd, SysMemberAdCollection>
	{	   
		public SysMemberAdCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysMemberAdCollection</returns>
		public SysMemberAdCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysMemberAd o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Member_Ads table.
	/// </summary>
	[Serializable]
	public partial class SysMemberAd : ActiveRecord<SysMemberAd>, IActiveRecord
	{
		#region .ctors and Default Settings
		static SysMemberAd( )
        {
            BaseSchema = DB.GetSchema("Sys_Member_Ads");
        }
		public SysMemberAd()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysMemberAd(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("Sys_Member_Ads");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysMemberAd(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Member_Ads");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysMemberAd(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Member_Ads");
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
                    BaseSchema = DB.GetSchema("Sys_Member_Ads");
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
		  
		[XmlAttribute("AdsMemberID")]
		[Bindable(true)]
		public int? AdsMemberID 
		{
			get { return GetColumnValue<int?>(Columns.AdsMemberID); }
			set { SetColumnValue(Columns.AdsMemberID, value); }
		}
		  
		[XmlAttribute("CompanyAdsID")]
		[Bindable(true)]
		public int? CompanyAdsID 
		{
			get { return GetColumnValue<int?>(Columns.CompanyAdsID); }
			set { SetColumnValue(Columns.CompanyAdsID, value); }
		}
		  
		[XmlAttribute("AdsPoint")]
		[Bindable(true)]
		public decimal? AdsPoint 
		{
			get { return GetColumnValue<decimal?>(Columns.AdsPoint); }
			set { SetColumnValue(Columns.AdsPoint, value); }
		}
		  
		[XmlAttribute("AdsDate")]
		[Bindable(true)]
		public DateTime? AdsDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.AdsDate); }
			set { SetColumnValue(Columns.AdsDate, value); }
		}
		  
		[XmlAttribute("OrderSum")]
		[Bindable(true)]
		public decimal? OrderSum 
		{
			get { return GetColumnValue<decimal?>(Columns.OrderSum); }
			set { SetColumnValue(Columns.OrderSum, value); }
		}
		  
		[XmlAttribute("CompanyID")]
		[Bindable(true)]
		public int? CompanyID 
		{
			get { return GetColumnValue<int?>(Columns.CompanyID); }
			set { SetColumnValue(Columns.CompanyID, value); }
		}
		  
		[XmlAttribute("PaymentID")]
		[Bindable(true)]
		public int? PaymentID 
		{
			get { return GetColumnValue<int?>(Columns.PaymentID); }
			set { SetColumnValue(Columns.PaymentID, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varAdsMemberID,int? varCompanyAdsID,decimal? varAdsPoint,DateTime? varAdsDate,decimal? varOrderSum,int? varCompanyID,int? varPaymentID)
		{
			SysMemberAd item = new SysMemberAd();
			
			item.AdsMemberID = varAdsMemberID;
			
			item.CompanyAdsID = varCompanyAdsID;
			
			item.AdsPoint = varAdsPoint;
			
			item.AdsDate = varAdsDate;
			
			item.OrderSum = varOrderSum;
			
			item.CompanyID = varCompanyID;
			
			item.PaymentID = varPaymentID;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varAdsID,int? varAdsMemberID,int? varCompanyAdsID,decimal? varAdsPoint,DateTime? varAdsDate,decimal? varOrderSum,int? varCompanyID,int? varPaymentID)
		{
			SysMemberAd item = new SysMemberAd();
			
				item.AdsID = varAdsID;
			
				item.AdsMemberID = varAdsMemberID;
			
				item.CompanyAdsID = varCompanyAdsID;
			
				item.AdsPoint = varAdsPoint;
			
				item.AdsDate = varAdsDate;
			
				item.OrderSum = varOrderSum;
			
				item.CompanyID = varCompanyID;
			
				item.PaymentID = varPaymentID;
			
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
        
        
        
        public static TableSchema.TableColumn AdsMemberIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn CompanyAdsIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsPointColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn AdsDateColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn OrderSumColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn CompanyIDColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentIDColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string AdsID = @"AdsID";
			 public static string AdsMemberID = @"AdsMemberID";
			 public static string CompanyAdsID = @"CompanyAdsID";
			 public static string AdsPoint = @"AdsPoint";
			 public static string AdsDate = @"AdsDate";
			 public static string OrderSum = @"OrderSum";
			 public static string CompanyID = @"CompanyID";
			 public static string PaymentID = @"PaymentID";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
