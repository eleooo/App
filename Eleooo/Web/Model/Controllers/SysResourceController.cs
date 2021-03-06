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
    /// Controller class for Sys_Resources
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysResourceController
    {
        // Preload our schema..
        SysResource thisSchemaLoad = new SysResource();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public SysResourceCollection FetchAll()
        {
            SysResourceCollection coll = new SysResourceCollection();
            Query qry = new Query(SysResource.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysResourceCollection FetchByID(object Id)
        {
            SysResourceCollection coll = new SysResourceCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysResourceCollection FetchByQuery(Query qry)
        {
            SysResourceCollection coll = new SysResourceCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (SysResource.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (SysResource.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Key,string ValueX,string Describe,string Lan,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    SysResource item = new SysResource();
		    
            item.Key = Key;
            
            item.ValueX = ValueX;
            
            item.Describe = Describe;
            
            item.Lan = Lan;
            
            item.CreatedBy = CreatedBy;
            
            item.CreatedOn = CreatedOn;
            
            item.ModifiedBy = ModifiedBy;
            
            item.ModifiedOn = ModifiedOn;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,string Key,string ValueX,string Describe,string Lan,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    SysResource item = new SysResource();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.Key = Key;
				
			item.ValueX = ValueX;
				
			item.Describe = Describe;
				
			item.Lan = Lan;
				
			item.CreatedBy = CreatedBy;
				
			item.CreatedOn = CreatedOn;
				
			item.ModifiedBy = ModifiedBy;
				
			item.ModifiedOn = ModifiedOn;
				
	        item.Save(UserName);
	    }
    }
}
