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
	/// Strongly-typed collection for the SysMemberReward class.
	/// </summary>
    [Serializable]
	public partial class SysMemberRewardCollection : ActiveList<SysMemberReward, SysMemberRewardCollection>
	{	   
		public SysMemberRewardCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysMemberRewardCollection</returns>
		public SysMemberRewardCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysMemberReward o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Member_Reward table.
	/// </summary>
	[Serializable]
	public partial class SysMemberReward : ActiveRecord<SysMemberReward>, IActiveRecord
	{
		#region .ctors and Default Settings
		static SysMemberReward( )
        {
            BaseSchema = DB.GetSchema("Sys_Member_Reward");
        }
		public SysMemberReward()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysMemberReward(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("Sys_Member_Reward");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysMemberReward(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Member_Reward");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysMemberReward(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Member_Reward");
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
                    BaseSchema = DB.GetSchema("Sys_Member_Reward");
                }
				return BaseSchema;
			}
		}
		
		#endregion
		
		#region Props
		  
		[XmlAttribute("RewardID")]
		[Bindable(true)]
		public int RewardID 
		{
			get { return GetColumnValue<int>(Columns.RewardID); }
			set { SetColumnValue(Columns.RewardID, value); }
		}
		  
		[XmlAttribute("RewardMemberID")]
		[Bindable(true)]
		public int? RewardMemberID 
		{
			get { return GetColumnValue<int?>(Columns.RewardMemberID); }
			set { SetColumnValue(Columns.RewardMemberID, value); }
		}
		  
		[XmlAttribute("OrderMemberID")]
		[Bindable(true)]
		public int? OrderMemberID 
		{
			get { return GetColumnValue<int?>(Columns.OrderMemberID); }
			set { SetColumnValue(Columns.OrderMemberID, value); }
		}
		  
		[XmlAttribute("OrderCompanyID")]
		[Bindable(true)]
		public int? OrderCompanyID 
		{
			get { return GetColumnValue<int?>(Columns.OrderCompanyID); }
			set { SetColumnValue(Columns.OrderCompanyID, value); }
		}
		  
		[XmlAttribute("OrderID")]
		[Bindable(true)]
		public int? OrderID 
		{
			get { return GetColumnValue<int?>(Columns.OrderID); }
			set { SetColumnValue(Columns.OrderID, value); }
		}
		  
		[XmlAttribute("OrderSumOk")]
		[Bindable(true)]
		public decimal? OrderSumOk 
		{
			get { return GetColumnValue<decimal?>(Columns.OrderSumOk); }
			set { SetColumnValue(Columns.OrderSumOk, value); }
		}
		  
		[XmlAttribute("RewardPoint")]
		[Bindable(true)]
		public decimal? RewardPoint 
		{
			get { return GetColumnValue<decimal?>(Columns.RewardPoint); }
			set { SetColumnValue(Columns.RewardPoint, value); }
		}
		  
		[XmlAttribute("RewardDate")]
		[Bindable(true)]
		public DateTime? RewardDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.RewardDate); }
			set { SetColumnValue(Columns.RewardDate, value); }
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
		public static void Insert(int? varRewardMemberID,int? varOrderMemberID,int? varOrderCompanyID,int? varOrderID,decimal? varOrderSumOk,decimal? varRewardPoint,DateTime? varRewardDate,int? varPaymentID)
		{
			SysMemberReward item = new SysMemberReward();
			
			item.RewardMemberID = varRewardMemberID;
			
			item.OrderMemberID = varOrderMemberID;
			
			item.OrderCompanyID = varOrderCompanyID;
			
			item.OrderID = varOrderID;
			
			item.OrderSumOk = varOrderSumOk;
			
			item.RewardPoint = varRewardPoint;
			
			item.RewardDate = varRewardDate;
			
			item.PaymentID = varPaymentID;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varRewardID,int? varRewardMemberID,int? varOrderMemberID,int? varOrderCompanyID,int? varOrderID,decimal? varOrderSumOk,decimal? varRewardPoint,DateTime? varRewardDate,int? varPaymentID)
		{
			SysMemberReward item = new SysMemberReward();
			
				item.RewardID = varRewardID;
			
				item.RewardMemberID = varRewardMemberID;
			
				item.OrderMemberID = varOrderMemberID;
			
				item.OrderCompanyID = varOrderCompanyID;
			
				item.OrderID = varOrderID;
			
				item.OrderSumOk = varOrderSumOk;
			
				item.RewardPoint = varRewardPoint;
			
				item.RewardDate = varRewardDate;
			
				item.PaymentID = varPaymentID;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn RewardIDColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn RewardMemberIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn OrderMemberIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn OrderCompanyIDColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn OrderIDColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn OrderSumOkColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn RewardPointColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn RewardDateColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn PaymentIDColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string RewardID = @"RewardID";
			 public static string RewardMemberID = @"RewardMemberID";
			 public static string OrderMemberID = @"OrderMemberID";
			 public static string OrderCompanyID = @"OrderCompanyID";
			 public static string OrderID = @"OrderID";
			 public static string OrderSumOk = @"OrderSumOk";
			 public static string RewardPoint = @"RewardPoint";
			 public static string RewardDate = @"RewardDate";
			 public static string PaymentID = @"PaymentID";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
