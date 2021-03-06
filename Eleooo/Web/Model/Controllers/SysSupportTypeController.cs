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
    /// Controller class for Sys_SupportType
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysSupportTypeController
    {
        // Preload our schema..
        SysSupportType thisSchemaLoad = new SysSupportType();
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
        public SysSupportTypeCollection FetchAll()
        {
            SysSupportTypeCollection coll = new SysSupportTypeCollection();
            Query qry = new Query(SysSupportType.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysSupportTypeCollection FetchByID(object SupportTypeId)
        {
            SysSupportTypeCollection coll = new SysSupportTypeCollection().Where("SupportType_ID", SupportTypeId).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysSupportTypeCollection FetchByQuery(Query qry)
        {
            SysSupportTypeCollection coll = new SysSupportTypeCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object SupportTypeId)
        {
            return (SysSupportType.Delete(SupportTypeId) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object SupportTypeId)
        {
            return (SysSupportType.Destroy(SupportTypeId) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string SupportTypeName,string SupportTypeDesc,string SupportTypePhoto,int SupportTypePid,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    SysSupportType item = new SysSupportType();
		    
            item.SupportTypeName = SupportTypeName;
            
            item.SupportTypeDesc = SupportTypeDesc;
            
            item.SupportTypePhoto = SupportTypePhoto;
            
            item.SupportTypePid = SupportTypePid;
            
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
	    public void Update(int SupportTypeId,string SupportTypeName,string SupportTypeDesc,string SupportTypePhoto,int SupportTypePid,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    SysSupportType item = new SysSupportType();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.SupportTypeId = SupportTypeId;
				
			item.SupportTypeName = SupportTypeName;
				
			item.SupportTypeDesc = SupportTypeDesc;
				
			item.SupportTypePhoto = SupportTypePhoto;
				
			item.SupportTypePid = SupportTypePid;
				
			item.CreatedBy = CreatedBy;
				
			item.CreatedOn = CreatedOn;
				
			item.ModifiedBy = ModifiedBy;
				
			item.ModifiedOn = ModifiedOn;
				
	        item.Save(UserName);
	    }
    }
}
