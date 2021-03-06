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
	/// Strongly-typed collection for the SysCompanyMansion class.
	/// </summary>
    [Serializable]
	public partial class SysCompanyMansionCollection : ActiveList<SysCompanyMansion, SysCompanyMansionCollection>
	{	   
		public SysCompanyMansionCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysCompanyMansionCollection</returns>
		public SysCompanyMansionCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysCompanyMansion o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Company_Mansion table.
	/// </summary>
	[Serializable]
	public partial class SysCompanyMansion : ActiveRecord<SysCompanyMansion>, IActiveRecord
	{
		#region .ctors and Default Settings
		static SysCompanyMansion( )
        {
            BaseSchema = DB.GetSchema("Sys_Company_Mansion");
        }
		public SysCompanyMansion()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysCompanyMansion(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("Sys_Company_Mansion");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysCompanyMansion(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Company_Mansion");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysCompanyMansion(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Company_Mansion");
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
                    BaseSchema = DB.GetSchema("Sys_Company_Mansion");
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
		  
		[XmlAttribute("CompanyID")]
		[Bindable(true)]
		public int? CompanyID 
		{
			get { return GetColumnValue<int?>(Columns.CompanyID); }
			set { SetColumnValue(Columns.CompanyID, value); }
		}
		  
		[XmlAttribute("MansionID")]
		[Bindable(true)]
		public int? MansionID 
		{
			get { return GetColumnValue<int?>(Columns.MansionID); }
			set { SetColumnValue(Columns.MansionID, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(int? varCompanyID,int? varMansionID)
		{
			SysCompanyMansion item = new SysCompanyMansion();
			
			item.CompanyID = varCompanyID;
			
			item.MansionID = varMansionID;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,int? varCompanyID,int? varMansionID)
		{
			SysCompanyMansion item = new SysCompanyMansion();
			
				item.Id = varId;
			
				item.CompanyID = varCompanyID;
			
				item.MansionID = varMansionID;
			
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
        
        
        
        public static TableSchema.TableColumn CompanyIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn MansionIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string CompanyID = @"CompanyID";
			 public static string MansionID = @"MansionID";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
