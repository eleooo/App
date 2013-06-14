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
	/// Strongly-typed collection for the SysNavigation class.
	/// </summary>
    [Serializable]
	public partial class SysNavigationCollection : ActiveList<SysNavigation, SysNavigationCollection>
	{	   
		public SysNavigationCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysNavigationCollection</returns>
		public SysNavigationCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysNavigation o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Navigation table.
	/// </summary>
	[Serializable]
	public partial class SysNavigation : ActiveRecord<SysNavigation>, IActiveRecord
	{
		#region .ctors and Default Settings
		static SysNavigation( )
        {
            BaseSchema = DB.GetSchema("Sys_Navigation");
        }
		public SysNavigation()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysNavigation(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("Sys_Navigation");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysNavigation(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Navigation");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysNavigation(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Navigation");
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
                    BaseSchema = DB.GetSchema("Sys_Navigation");
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
		  
		[XmlAttribute("NavName")]
		[Bindable(true)]
		public string NavName 
		{
			get { return GetColumnValue<string>(Columns.NavName); }
			set { SetColumnValue(Columns.NavName, value); }
		}
		  
		[XmlAttribute("SecName")]
		[Bindable(true)]
		public string SecName 
		{
			get { return GetColumnValue<string>(Columns.SecName); }
			set { SetColumnValue(Columns.SecName, value); }
		}
		  
		[XmlAttribute("OthName")]
		[Bindable(true)]
		public string OthName 
		{
			get { return GetColumnValue<string>(Columns.OthName); }
			set { SetColumnValue(Columns.OthName, value); }
		}
		  
		[XmlAttribute("NavUrl")]
		[Bindable(true)]
		public string NavUrl 
		{
			get { return GetColumnValue<string>(Columns.NavUrl); }
			set { SetColumnValue(Columns.NavUrl, value); }
		}
		  
		[XmlAttribute("NavIcon")]
		[Bindable(true)]
		public string NavIcon 
		{
			get { return GetColumnValue<string>(Columns.NavIcon); }
			set { SetColumnValue(Columns.NavIcon, value); }
		}
		  
		[XmlAttribute("SubSysId")]
		[Bindable(true)]
		public int? SubSysId 
		{
			get { return GetColumnValue<int?>(Columns.SubSysId); }
			set { SetColumnValue(Columns.SubSysId, value); }
		}
		  
		[XmlAttribute("PId")]
		[Bindable(true)]
		public int? PId 
		{
			get { return GetColumnValue<int?>(Columns.PId); }
			set { SetColumnValue(Columns.PId, value); }
		}
		  
		[XmlAttribute("IsMainNav")]
		[Bindable(true)]
		public bool IsMainNav 
		{
			get { return GetColumnValue<bool>(Columns.IsMainNav); }
			set { SetColumnValue(Columns.IsMainNav, value); }
		}
		  
		[XmlAttribute("IsHeader")]
		[Bindable(true)]
		public bool IsHeader 
		{
			get { return GetColumnValue<bool>(Columns.IsHeader); }
			set { SetColumnValue(Columns.IsHeader, value); }
		}
		  
		[XmlAttribute("IsFooter")]
		[Bindable(true)]
		public bool IsFooter 
		{
			get { return GetColumnValue<bool>(Columns.IsFooter); }
			set { SetColumnValue(Columns.IsFooter, value); }
		}
		  
		[XmlAttribute("PermissionRequired")]
		[Bindable(true)]
		public bool PermissionRequired 
		{
			get { return GetColumnValue<bool>(Columns.PermissionRequired); }
			set { SetColumnValue(Columns.PermissionRequired, value); }
		}
		  
		[XmlAttribute("Sort")]
		[Bindable(true)]
		public int Sort 
		{
			get { return GetColumnValue<int>(Columns.Sort); }
			set { SetColumnValue(Columns.Sort, value); }
		}
		  
		[XmlAttribute("Visible")]
		[Bindable(true)]
		public bool Visible 
		{
			get { return GetColumnValue<bool>(Columns.Visible); }
			set { SetColumnValue(Columns.Visible, value); }
		}
		  
		[XmlAttribute("Depth")]
		[Bindable(true)]
		public string Depth 
		{
			get { return GetColumnValue<string>(Columns.Depth); }
			set { SetColumnValue(Columns.Depth, value); }
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
		public static void Insert(string varNavName,string varSecName,string varOthName,string varNavUrl,string varNavIcon,int? varSubSysId,int? varPId,bool varIsMainNav,bool varIsHeader,bool varIsFooter,bool varPermissionRequired,int varSort,bool varVisible,string varDepth,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn)
		{
			SysNavigation item = new SysNavigation();
			
			item.NavName = varNavName;
			
			item.SecName = varSecName;
			
			item.OthName = varOthName;
			
			item.NavUrl = varNavUrl;
			
			item.NavIcon = varNavIcon;
			
			item.SubSysId = varSubSysId;
			
			item.PId = varPId;
			
			item.IsMainNav = varIsMainNav;
			
			item.IsHeader = varIsHeader;
			
			item.IsFooter = varIsFooter;
			
			item.PermissionRequired = varPermissionRequired;
			
			item.Sort = varSort;
			
			item.Visible = varVisible;
			
			item.Depth = varDepth;
			
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
		public static void Update(int varId,string varNavName,string varSecName,string varOthName,string varNavUrl,string varNavIcon,int? varSubSysId,int? varPId,bool varIsMainNav,bool varIsHeader,bool varIsFooter,bool varPermissionRequired,int varSort,bool varVisible,string varDepth,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn)
		{
			SysNavigation item = new SysNavigation();
			
				item.Id = varId;
			
				item.NavName = varNavName;
			
				item.SecName = varSecName;
			
				item.OthName = varOthName;
			
				item.NavUrl = varNavUrl;
			
				item.NavIcon = varNavIcon;
			
				item.SubSysId = varSubSysId;
			
				item.PId = varPId;
			
				item.IsMainNav = varIsMainNav;
			
				item.IsHeader = varIsHeader;
			
				item.IsFooter = varIsFooter;
			
				item.PermissionRequired = varPermissionRequired;
			
				item.Sort = varSort;
			
				item.Visible = varVisible;
			
				item.Depth = varDepth;
			
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
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn NavNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SecNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn OthNameColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NavUrlColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn NavIconColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SubSysIdColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn PIdColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn IsMainNavColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn IsHeaderColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn IsFooterColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn PermissionRequiredColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn SortColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn VisibleColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn DepthColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedByColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedOnColumn
        {
            get { return Schema.Columns[16]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedByColumn
        {
            get { return Schema.Columns[17]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedOnColumn
        {
            get { return Schema.Columns[18]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string NavName = @"NavName";
			 public static string SecName = @"SecName";
			 public static string OthName = @"OthName";
			 public static string NavUrl = @"NavUrl";
			 public static string NavIcon = @"NavIcon";
			 public static string SubSysId = @"SubSys_ID";
			 public static string PId = @"P_ID";
			 public static string IsMainNav = @"IsMainNav";
			 public static string IsHeader = @"IsHeader";
			 public static string IsFooter = @"IsFooter";
			 public static string PermissionRequired = @"PermissionRequired";
			 public static string Sort = @"Sort";
			 public static string Visible = @"Visible";
			 public static string Depth = @"Depth";
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
