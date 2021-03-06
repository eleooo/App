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
	/// Strongly-typed collection for the PaymentRate class.
	/// </summary>
    [Serializable]
	public partial class PaymentRateCollection : ActiveList<PaymentRate, PaymentRateCollection>
	{	   
		public PaymentRateCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>PaymentRateCollection</returns>
		public PaymentRateCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                PaymentRate o = this[i];
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
	/// This is an ActiveRecord class which wraps the PaymentRate table.
	/// </summary>
	[Serializable]
	public partial class PaymentRate : ActiveRecord<PaymentRate>, IActiveRecord
	{
		#region .ctors and Default Settings
		static PaymentRate( )
        {
            BaseSchema = DB.GetSchema("PaymentRate");
        }
		public PaymentRate()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public PaymentRate(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("PaymentRate");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public PaymentRate(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("PaymentRate");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public PaymentRate(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("PaymentRate");
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
                    BaseSchema = DB.GetSchema("PaymentRate");
                }
				return BaseSchema;
			}
		}
		
		#endregion
		
		#region Props
		  
		[XmlAttribute("PaymentRateID")]
		[Bindable(true)]
		public int PaymentRateID 
		{
			get { return GetColumnValue<int>(Columns.PaymentRateID); }
			set { SetColumnValue(Columns.PaymentRateID, value); }
		}
		  
		[XmlAttribute("PaymentRateDate")]
		[Bindable(true)]
		public DateTime? PaymentRateDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.PaymentRateDate); }
			set { SetColumnValue(Columns.PaymentRateDate, value); }
		}
		  
		[XmlAttribute("PaymentRateCompanyID")]
		[Bindable(true)]
		public int? PaymentRateCompanyID 
		{
			get { return GetColumnValue<int?>(Columns.PaymentRateCompanyID); }
			set { SetColumnValue(Columns.PaymentRateCompanyID, value); }
		}
		  
		[XmlAttribute("PaymentRateSale")]
		[Bindable(true)]
		public decimal? PaymentRateSale 
		{
			get { return GetColumnValue<decimal?>(Columns.PaymentRateSale); }
			set { SetColumnValue(Columns.PaymentRateSale, value); }
		}
		  
		[XmlAttribute("PaymentRateSum")]
		[Bindable(true)]
		public decimal? PaymentRateSum 
		{
			get { return GetColumnValue<decimal?>(Columns.PaymentRateSum); }
			set { SetColumnValue(Columns.PaymentRateSum, value); }
		}
		  
		[XmlAttribute("PaymentRateMemo")]
		[Bindable(true)]
		public string PaymentRateMemo 
		{
			get { return GetColumnValue<string>(Columns.PaymentRateMemo); }
			set { SetColumnValue(Columns.PaymentRateMemo, value); }
		}
		  
		[XmlAttribute("PaymentRateStatus")]
		[Bindable(true)]
		public int? PaymentRateStatus 
		{
			get { return GetColumnValue<int?>(Columns.PaymentRateStatus); }
			set { SetColumnValue(Columns.PaymentRateStatus, value); }
		}
		  
		[XmlAttribute("PaymentRateDateStart")]
		[Bindable(true)]
		public DateTime? PaymentRateDateStart 
		{
			get { return GetColumnValue<DateTime?>(Columns.PaymentRateDateStart); }
			set { SetColumnValue(Columns.PaymentRateDateStart, value); }
		}
		  
		[XmlAttribute("PaymentRateDateEnd")]
		[Bindable(true)]
		public DateTime? PaymentRateDateEnd 
		{
			get { return GetColumnValue<DateTime?>(Columns.PaymentRateDateEnd); }
			set { SetColumnValue(Columns.PaymentRateDateEnd, value); }
		}
		  
		[XmlAttribute("PaymentRateCash")]
		[Bindable(true)]
		public decimal? PaymentRateCash 
		{
			get { return GetColumnValue<decimal?>(Columns.PaymentRateCash); }
			set { SetColumnValue(Columns.PaymentRateCash, value); }
		}
		  
		[XmlAttribute("PaymentRateRate")]
		[Bindable(true)]
		public decimal? PaymentRateRate 
		{
			get { return GetColumnValue<decimal?>(Columns.PaymentRateRate); }
			set { SetColumnValue(Columns.PaymentRateRate, value); }
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
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(DateTime? varPaymentRateDate,int? varPaymentRateCompanyID,decimal? varPaymentRateSale,decimal? varPaymentRateSum,string varPaymentRateMemo,int? varPaymentRateStatus,DateTime? varPaymentRateDateStart,DateTime? varPaymentRateDateEnd,decimal? varPaymentRateCash,decimal? varPaymentRateRate,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn)
		{
			PaymentRate item = new PaymentRate();
			
			item.PaymentRateDate = varPaymentRateDate;
			
			item.PaymentRateCompanyID = varPaymentRateCompanyID;
			
			item.PaymentRateSale = varPaymentRateSale;
			
			item.PaymentRateSum = varPaymentRateSum;
			
			item.PaymentRateMemo = varPaymentRateMemo;
			
			item.PaymentRateStatus = varPaymentRateStatus;
			
			item.PaymentRateDateStart = varPaymentRateDateStart;
			
			item.PaymentRateDateEnd = varPaymentRateDateEnd;
			
			item.PaymentRateCash = varPaymentRateCash;
			
			item.PaymentRateRate = varPaymentRateRate;
			
			item.CreatedBy = varCreatedBy;
			
			item.CreatedOn = varCreatedOn;
			
			item.ModifiedBy = varModifiedBy;
			
			item.ModifiedOn = varModifiedOn;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varPaymentRateID,DateTime? varPaymentRateDate,int? varPaymentRateCompanyID,decimal? varPaymentRateSale,decimal? varPaymentRateSum,string varPaymentRateMemo,int? varPaymentRateStatus,DateTime? varPaymentRateDateStart,DateTime? varPaymentRateDateEnd,decimal? varPaymentRateCash,decimal? varPaymentRateRate,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn)
		{
			PaymentRate item = new PaymentRate();
			
				item.PaymentRateID = varPaymentRateID;
			
				item.PaymentRateDate = varPaymentRateDate;
			
				item.PaymentRateCompanyID = varPaymentRateCompanyID;
			
				item.PaymentRateSale = varPaymentRateSale;
			
				item.PaymentRateSum = varPaymentRateSum;
			
				item.PaymentRateMemo = varPaymentRateMemo;
			
				item.PaymentRateStatus = varPaymentRateStatus;
			
				item.PaymentRateDateStart = varPaymentRateDateStart;
			
				item.PaymentRateDateEnd = varPaymentRateDateEnd;
			
				item.PaymentRateCash = varPaymentRateCash;
			
				item.PaymentRateRate = varPaymentRateRate;
			
				item.CreatedBy = varCreatedBy;
			
				item.CreatedOn = varCreatedOn;
			
				item.ModifiedBy = varModifiedBy;
			
				item.ModifiedOn = varModifiedOn;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn PaymentRateIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentRateDateColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentRateCompanyIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentRateSaleColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentRateSumColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentRateMemoColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentRateStatusColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentRateDateStartColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentRateDateEndColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentRateCashColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentRateRateColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedByColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedOnColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedByColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedOnColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string PaymentRateID = @"PaymentRateID";
			 public static string PaymentRateDate = @"PaymentRateDate";
			 public static string PaymentRateCompanyID = @"PaymentRateCompanyID";
			 public static string PaymentRateSale = @"PaymentRateSale";
			 public static string PaymentRateSum = @"PaymentRateSum";
			 public static string PaymentRateMemo = @"PaymentRateMemo";
			 public static string PaymentRateStatus = @"PaymentRateStatus";
			 public static string PaymentRateDateStart = @"PaymentRateDateStart";
			 public static string PaymentRateDateEnd = @"PaymentRateDateEnd";
			 public static string PaymentRateCash = @"PaymentRateCash";
			 public static string PaymentRateRate = @"PaymentRateRate";
			 public static string CreatedBy = @"CreatedBy";
			 public static string CreatedOn = @"CreatedOn";
			 public static string ModifiedBy = @"ModifiedBy";
			 public static string ModifiedOn = @"ModifiedOn";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
