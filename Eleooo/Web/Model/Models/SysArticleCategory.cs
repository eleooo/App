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
	/// Strongly-typed collection for the SysArticleCategory class.
	/// </summary>
    [Serializable]
	public partial class SysArticleCategoryCollection : ActiveList<SysArticleCategory, SysArticleCategoryCollection>
	{	   
		public SysArticleCategoryCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysArticleCategoryCollection</returns>
		public SysArticleCategoryCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysArticleCategory o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Article_Category table.
	/// </summary>
	[Serializable]
	public partial class SysArticleCategory : ActiveRecord<SysArticleCategory>, IActiveRecord
	{
		#region .ctors and Default Settings
		static SysArticleCategory( )
        {
            BaseSchema = DB.GetSchema("Sys_Article_Category");
        }
		public SysArticleCategory()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysArticleCategory(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("Sys_Article_Category");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysArticleCategory(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Article_Category");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysArticleCategory(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Article_Category");
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
                    BaseSchema = DB.GetSchema("Sys_Article_Category");
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
		  
		[XmlAttribute("Name")]
		[Bindable(true)]
		public string Name 
		{
			get { return GetColumnValue<string>(Columns.Name); }
			set { SetColumnValue(Columns.Name, value); }
		}
		  
		[XmlAttribute("PId")]
		[Bindable(true)]
		public int? PId 
		{
			get { return GetColumnValue<int?>(Columns.PId); }
			set { SetColumnValue(Columns.PId, value); }
		}
		  
		[XmlAttribute("SubSysId")]
		[Bindable(true)]
		public int? SubSysId 
		{
			get { return GetColumnValue<int?>(Columns.SubSysId); }
			set { SetColumnValue(Columns.SubSysId, value); }
		}
		  
		[XmlAttribute("UId")]
		[Bindable(true)]
		public int? UId 
		{
			get { return GetColumnValue<int?>(Columns.UId); }
			set { SetColumnValue(Columns.UId, value); }
		}
		  
		[XmlAttribute("Path")]
		[Bindable(true)]
		public string Path 
		{
			get { return GetColumnValue<string>(Columns.Path); }
			set { SetColumnValue(Columns.Path, value); }
		}
		  
		[XmlAttribute("Sort")]
		[Bindable(true)]
		public int? Sort 
		{
			get { return GetColumnValue<int?>(Columns.Sort); }
			set { SetColumnValue(Columns.Sort, value); }
		}
		  
		[XmlAttribute("KeyWord")]
		[Bindable(true)]
		public string KeyWord 
		{
			get { return GetColumnValue<string>(Columns.KeyWord); }
			set { SetColumnValue(Columns.KeyWord, value); }
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
		public static void Insert(string varName,int? varPId,int? varSubSysId,int? varUId,string varPath,int? varSort,string varKeyWord,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn)
		{
			SysArticleCategory item = new SysArticleCategory();
			
			item.Name = varName;
			
			item.PId = varPId;
			
			item.SubSysId = varSubSysId;
			
			item.UId = varUId;
			
			item.Path = varPath;
			
			item.Sort = varSort;
			
			item.KeyWord = varKeyWord;
			
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
		public static void Update(int varId,string varName,int? varPId,int? varSubSysId,int? varUId,string varPath,int? varSort,string varKeyWord,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn)
		{
			SysArticleCategory item = new SysArticleCategory();
			
				item.Id = varId;
			
				item.Name = varName;
			
				item.PId = varPId;
			
				item.SubSysId = varSubSysId;
			
				item.UId = varUId;
			
				item.Path = varPath;
			
				item.Sort = varSort;
			
				item.KeyWord = varKeyWord;
			
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
        
        
        
        public static TableSchema.TableColumn NameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn PIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn SubSysIdColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn UIdColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn PathColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SortColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn KeyWordColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedByColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedOnColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedByColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedOnColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string Name = @"Name";
			 public static string PId = @"P_ID";
			 public static string SubSysId = @"SubSys_ID";
			 public static string UId = @"U_ID";
			 public static string Path = @"Path";
			 public static string Sort = @"Sort";
			 public static string KeyWord = @"KeyWord";
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
