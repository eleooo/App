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
    /// Controller class for Sys_MessageType
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysMessageTypeController
    {
        // Preload our schema..
        SysMessageType thisSchemaLoad = new SysMessageType();
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
        public SysMessageTypeCollection FetchAll()
        {
            SysMessageTypeCollection coll = new SysMessageTypeCollection();
            Query qry = new Query(SysMessageType.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysMessageTypeCollection FetchByID(object Id)
        {
            SysMessageTypeCollection coll = new SysMessageTypeCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysMessageTypeCollection FetchByQuery(Query qry)
        {
            SysMessageTypeCollection coll = new SysMessageTypeCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (SysMessageType.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (SysMessageType.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string MessageTypeCode,string MessageTypeName,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    SysMessageType item = new SysMessageType();
		    
            item.MessageTypeCode = MessageTypeCode;
            
            item.MessageTypeName = MessageTypeName;
            
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
	    public void Update(int Id,string MessageTypeCode,string MessageTypeName,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    SysMessageType item = new SysMessageType();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.MessageTypeCode = MessageTypeCode;
				
			item.MessageTypeName = MessageTypeName;
				
			item.CreatedBy = CreatedBy;
				
			item.CreatedOn = CreatedOn;
				
			item.ModifiedBy = ModifiedBy;
				
			item.ModifiedOn = ModifiedOn;
				
	        item.Save(UserName);
	    }
    }
}
