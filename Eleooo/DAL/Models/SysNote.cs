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
	/// Strongly-typed collection for the SysNote class.
	/// </summary>
    [Serializable]
	public partial class SysNoteCollection : ActiveList<SysNote, SysNoteCollection>
	{	   
		public SysNoteCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>SysNoteCollection</returns>
		public SysNoteCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                SysNote o = this[i];
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
	/// This is an ActiveRecord class which wraps the Sys_Notes table.
	/// </summary>
	[Serializable]
	public partial class SysNote : ActiveRecord<SysNote>, IActiveRecord
	{
		#region .ctors and Default Settings
		static SysNote( )
        {
            BaseSchema = DB.GetSchema("Sys_Notes");
        }
		public SysNote()
		{
		  //InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public SysNote(bool useDatabaseDefaults)
		{ 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("Sys_Notes");
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public SysNote(object keyID)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Notes");
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public SysNote(string columnName, object columnValue)
		{ 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("Sys_Notes");
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
                    BaseSchema = DB.GetSchema("Sys_Notes");
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
		  
		[XmlAttribute("NoteSubject")]
		[Bindable(true)]
		public string NoteSubject 
		{
			get { return GetColumnValue<string>(Columns.NoteSubject); }
			set { SetColumnValue(Columns.NoteSubject, value); }
		}
		  
		[XmlAttribute("NoteContent")]
		[Bindable(true)]
		public string NoteContent 
		{
			get { return GetColumnValue<string>(Columns.NoteContent); }
			set { SetColumnValue(Columns.NoteContent, value); }
		}
		  
		[XmlAttribute("NoteDate")]
		[Bindable(true)]
		public DateTime? NoteDate 
		{
			get { return GetColumnValue<DateTime?>(Columns.NoteDate); }
			set { SetColumnValue(Columns.NoteDate, value); }
		}
		  
		[XmlAttribute("NoteStatus")]
		[Bindable(true)]
		public int NoteStatus 
		{
			get { return GetColumnValue<int>(Columns.NoteStatus); }
			set { SetColumnValue(Columns.NoteStatus, value); }
		}
		  
		[XmlAttribute("NoteHots")]
		[Bindable(true)]
		public int NoteHots 
		{
			get { return GetColumnValue<int>(Columns.NoteHots); }
			set { SetColumnValue(Columns.NoteHots, value); }
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
		public static void Insert(string varNoteSubject,string varNoteContent,DateTime? varNoteDate,int varNoteStatus,int varNoteHots,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn)
		{
			SysNote item = new SysNote();
			
			item.NoteSubject = varNoteSubject;
			
			item.NoteContent = varNoteContent;
			
			item.NoteDate = varNoteDate;
			
			item.NoteStatus = varNoteStatus;
			
			item.NoteHots = varNoteHots;
			
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
		public static void Update(int varId,string varNoteSubject,string varNoteContent,DateTime? varNoteDate,int varNoteStatus,int varNoteHots,int? varCreatedBy,DateTime? varCreatedOn,int? varModifiedBy,DateTime? varModifiedOn)
		{
			SysNote item = new SysNote();
			
				item.Id = varId;
			
				item.NoteSubject = varNoteSubject;
			
				item.NoteContent = varNoteContent;
			
				item.NoteDate = varNoteDate;
			
				item.NoteStatus = varNoteStatus;
			
				item.NoteHots = varNoteHots;
			
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
        
        
        
        public static TableSchema.TableColumn NoteSubjectColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn NoteContentColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn NoteDateColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NoteStatusColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn NoteHotsColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedByColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedOnColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedByColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn ModifiedOnColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"ID";
			 public static string NoteSubject = @"NoteSubject";
			 public static string NoteContent = @"NoteContent";
			 public static string NoteDate = @"NoteDate";
			 public static string NoteStatus = @"NoteStatus";
			 public static string NoteHots = @"NoteHots";
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
