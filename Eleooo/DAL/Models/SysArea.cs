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
	/// Strongly-typed collection for the SysArea class.
	/// </summary>
    [Serializable]
	public partial class SysAreaCollection : ActiveList<SysArea, SysAreaCollection>
	{	   
		public SysAreaCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysAreaCollection</returns>
		public SysAreaCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysArea o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Area table.
	/// </summary>
	[Serializable]
	public partial class SysArea : ActiveRecord<SysArea>, IActiveRecord
	{
		#region .ctors and Default Settings
		static SysArea( )
        {
            BaseSchema = DB.GetSchema("Sys_Area");
        }
		public SysArea()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysArea(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("Sys_Area");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysArea(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Area");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysArea(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Area");
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
                    BaseSchema = DB.GetSchema("Sys_Area");
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
		  
		[XmlAttribute("AreaName")]
		[Bindable(true)]
		public string AreaName 
		{
			get { return GetColumnValue<string>(Columns.AreaName); }
			set { SetColumnValue(Columns.AreaName, value); }
		}
		  
		[XmlAttribute("PId")]
		[Bindable(true)]
		public int? PId 
		{
			get { return GetColumnValue<int?>(Columns.PId); }
			set { SetColumnValue(Columns.PId, value); }
		}
		  
		[XmlAttribute("Depth")]
		[Bindable(true)]
		public string Depth 
		{
			get { return GetColumnValue<string>(Columns.Depth); }
			set { SetColumnValue(Columns.Depth, value); }
		}
		  
		[XmlAttribute("AreaCode")]
		[Bindable(true)]
		public string AreaCode 
		{
			get { return GetColumnValue<string>(Columns.AreaCode); }
			set { SetColumnValue(Columns.AreaCode, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varAreaName,int? varPId,string varDepth,string varAreaCode)
		{
			SysArea item = new SysArea();
			
			item.AreaName = varAreaName;
			
			item.PId = varPId;
			
			item.Depth = varDepth;
			
			item.AreaCode = varAreaCode;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varAreaName,int? varPId,string varDepth,string varAreaCode)
		{
			SysArea item = new SysArea();
			
				item.Id = varId;
			
				item.AreaName = varAreaName;
			
				item.PId = varPId;
			
				item.Depth = varDepth;
			
				item.AreaCode = varAreaCode;
			
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
        
        
        
        public static TableSchema.TableColumn AreaNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn PIdColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn DepthColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn AreaCodeColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string AreaName = @"Area_Name";
			 public static string PId = @"P_ID";
			 public static string Depth = @"Depth";
			 public static string AreaCode = @"Area_Code";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
