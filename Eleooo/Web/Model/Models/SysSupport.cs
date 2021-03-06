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
	/// Strongly-typed collection for the SysSupport class.
	/// </summary>
    [Serializable]
	public partial class SysSupportCollection : ActiveList<SysSupport, SysSupportCollection>
	{	   
		public SysSupportCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysSupportCollection</returns>
		public SysSupportCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysSupport o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Support table.
	/// </summary>
	[Serializable]
	public partial class SysSupport : ActiveRecord<SysSupport>, IActiveRecord
	{
		#region .ctors and Default Settings
		static SysSupport( )
        {
            BaseSchema = DB.GetSchema("Sys_Support");
        }
		public SysSupport()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysSupport(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("Sys_Support");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysSupport(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Support");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysSupport(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Support");
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
                    BaseSchema = DB.GetSchema("Sys_Support");
                }
				return BaseSchema;
			}
		}
		
		#endregion
		
		#region Props
		  
		[XmlAttribute("SupportId")]
		[Bindable(true)]
		public int SupportId 
		{
			get { return GetColumnValue<int>(Columns.SupportId); }
			set { SetColumnValue(Columns.SupportId, value); }
		}
		  
		[XmlAttribute("SupportFid")]
		[Bindable(true)]
		public int SupportFid 
		{
			get { return GetColumnValue<int>(Columns.SupportFid); }
			set { SetColumnValue(Columns.SupportFid, value); }
		}
		  
		[XmlAttribute("SupportTid")]
		[Bindable(true)]
		public int SupportTid 
		{
			get { return GetColumnValue<int>(Columns.SupportTid); }
			set { SetColumnValue(Columns.SupportTid, value); }
		}
		  
		[XmlAttribute("SupportItem")]
		[Bindable(true)]
		public int SupportItem 
		{
			get { return GetColumnValue<int>(Columns.SupportItem); }
			set { SetColumnValue(Columns.SupportItem, value); }
		}
		  
		[XmlAttribute("SupportType")]
		[Bindable(true)]
		public int SupportType 
		{
			get { return GetColumnValue<int>(Columns.SupportType); }
			set { SetColumnValue(Columns.SupportType, value); }
		}
		  
		[XmlAttribute("SupportProductID")]
		[Bindable(true)]
		public int SupportProductID 
		{
			get { return GetColumnValue<int>(Columns.SupportProductID); }
			set { SetColumnValue(Columns.SupportProductID, value); }
		}
		  
		[XmlAttribute("SupportSubject")]
		[Bindable(true)]
		public string SupportSubject 
		{
			get { return GetColumnValue<string>(Columns.SupportSubject); }
			set { SetColumnValue(Columns.SupportSubject, value); }
		}
		  
		[XmlAttribute("SupportContent")]
		[Bindable(true)]
		public string SupportContent 
		{
			get { return GetColumnValue<string>(Columns.SupportContent); }
			set { SetColumnValue(Columns.SupportContent, value); }
		}
		  
		[XmlAttribute("SupportAttach")]
		[Bindable(true)]
		public string SupportAttach 
		{
			get { return GetColumnValue<string>(Columns.SupportAttach); }
			set { SetColumnValue(Columns.SupportAttach, value); }
		}
		  
		[XmlAttribute("SupportPhoto")]
		[Bindable(true)]
		public string SupportPhoto 
		{
			get { return GetColumnValue<string>(Columns.SupportPhoto); }
			set { SetColumnValue(Columns.SupportPhoto, value); }
		}
		  
		[XmlAttribute("SupportIsRead")]
		[Bindable(true)]
		public bool SupportIsRead 
		{
			get { return GetColumnValue<bool>(Columns.SupportIsRead); }
			set { SetColumnValue(Columns.SupportIsRead, value); }
		}
		  
		[XmlAttribute("SupportRating")]
		[Bindable(true)]
		public int? SupportRating 
		{
			get { return GetColumnValue<int?>(Columns.SupportRating); }
			set { SetColumnValue(Columns.SupportRating, value); }
		}
		  
		[XmlAttribute("SupportRatingReason")]
		[Bindable(true)]
		public string SupportRatingReason 
		{
			get { return GetColumnValue<string>(Columns.SupportRatingReason); }
			set { SetColumnValue(Columns.SupportRatingReason, value); }
		}
		  
		[XmlAttribute("SupportStatus")]
		[Bindable(true)]
		public int SupportStatus 
		{
			get { return GetColumnValue<int>(Columns.SupportStatus); }
			set { SetColumnValue(Columns.SupportStatus, value); }
		}
		  
		[XmlAttribute("SupportDate")]
		[Bindable(true)]
		public DateTime SupportDate 
		{
			get { return GetColumnValue<DateTime>(Columns.SupportDate); }
			set { SetColumnValue(Columns.SupportDate, value); }
		}
		  
		[XmlAttribute("SupportDateReply")]
		[Bindable(true)]
		public DateTime? SupportDateReply 
		{
			get { return GetColumnValue<DateTime?>(Columns.SupportDateReply); }
			set { SetColumnValue(Columns.SupportDateReply, value); }
		}
		  
		[XmlAttribute("SupportDateFinish")]
		[Bindable(true)]
		public DateTime? SupportDateFinish 
		{
			get { return GetColumnValue<DateTime?>(Columns.SupportDateFinish); }
			set { SetColumnValue(Columns.SupportDateFinish, value); }
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
		public static void Insert(int varSupportFid,int varSupportTid,int varSupportItem,int varSupportType,int varSupportProductID,string varSupportSubject,string varSupportContent,string varSupportAttach,string varSupportPhoto,bool varSupportIsRead,int? varSupportRating,string varSupportRatingReason,int varSupportStatus,DateTime varSupportDate,DateTime? varSupportDateReply,DateTime? varSupportDateFinish,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn)
		{
			SysSupport item = new SysSupport();
			
			item.SupportFid = varSupportFid;
			
			item.SupportTid = varSupportTid;
			
			item.SupportItem = varSupportItem;
			
			item.SupportType = varSupportType;
			
			item.SupportProductID = varSupportProductID;
			
			item.SupportSubject = varSupportSubject;
			
			item.SupportContent = varSupportContent;
			
			item.SupportAttach = varSupportAttach;
			
			item.SupportPhoto = varSupportPhoto;
			
			item.SupportIsRead = varSupportIsRead;
			
			item.SupportRating = varSupportRating;
			
			item.SupportRatingReason = varSupportRatingReason;
			
			item.SupportStatus = varSupportStatus;
			
			item.SupportDate = varSupportDate;
			
			item.SupportDateReply = varSupportDateReply;
			
			item.SupportDateFinish = varSupportDateFinish;
			
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
		public static void Update(int varSupportId,int varSupportFid,int varSupportTid,int varSupportItem,int varSupportType,int varSupportProductID,string varSupportSubject,string varSupportContent,string varSupportAttach,string varSupportPhoto,bool varSupportIsRead,int? varSupportRating,string varSupportRatingReason,int varSupportStatus,DateTime varSupportDate,DateTime? varSupportDateReply,DateTime? varSupportDateFinish,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn)
		{
			SysSupport item = new SysSupport();
			
				item.SupportId = varSupportId;
			
				item.SupportFid = varSupportFid;
			
				item.SupportTid = varSupportTid;
			
				item.SupportItem = varSupportItem;
			
				item.SupportType = varSupportType;
			
				item.SupportProductID = varSupportProductID;
			
				item.SupportSubject = varSupportSubject;
			
				item.SupportContent = varSupportContent;
			
				item.SupportAttach = varSupportAttach;
			
				item.SupportPhoto = varSupportPhoto;
			
				item.SupportIsRead = varSupportIsRead;
			
				item.SupportRating = varSupportRating;
			
				item.SupportRatingReason = varSupportRatingReason;
			
				item.SupportStatus = varSupportStatus;
			
				item.SupportDate = varSupportDate;
			
				item.SupportDateReply = varSupportDateReply;
			
				item.SupportDateFinish = varSupportDateFinish;
			
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
        
        
        public static TableSchema.TableColumn SupportIdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportFidColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportTidColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportItemColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportTypeColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportProductIDColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportSubjectColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportContentColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportAttachColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportPhotoColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportIsReadColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportRatingColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportRatingReasonColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportStatusColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportDateColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportDateReplyColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn SupportDateFinishColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedByColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedOnColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedByColumn
        {
            get { return Schema.Columns[19]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedOnColumn
        {
            get { return Schema.Columns[20]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string SupportId = @"Support_ID";
			 public static string SupportFid = @"Support_FID";
			 public static string SupportTid = @"Support_TID";
			 public static string SupportItem = @"Support_Item";
			 public static string SupportType = @"Support_Type";
			 public static string SupportProductID = @"Support_ProductID";
			 public static string SupportSubject = @"Support_Subject";
			 public static string SupportContent = @"Support_Content";
			 public static string SupportAttach = @"Support_Attach";
			 public static string SupportPhoto = @"Support_Photo";
			 public static string SupportIsRead = @"Support_IsRead";
			 public static string SupportRating = @"Support_Rating";
			 public static string SupportRatingReason = @"Support_RatingReason";
			 public static string SupportStatus = @"Support_Status";
			 public static string SupportDate = @"Support_Date";
			 public static string SupportDateReply = @"Support_DateReply";
			 public static string SupportDateFinish = @"Support_DateFinish";
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
