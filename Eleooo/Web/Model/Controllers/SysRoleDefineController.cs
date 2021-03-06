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
    /// Controller class for Sys_RoleDefine
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysRoleDefineController
    {
        // Preload our schema..
        SysRoleDefine thisSchemaLoad = new SysRoleDefine();
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
        public SysRoleDefineCollection FetchAll()
        {
            SysRoleDefineCollection coll = new SysRoleDefineCollection();
            Query qry = new Query(SysRoleDefine.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysRoleDefineCollection FetchByID(object Id)
        {
            SysRoleDefineCollection coll = new SysRoleDefineCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysRoleDefineCollection FetchByQuery(Query qry)
        {
            SysRoleDefineCollection coll = new SysRoleDefineCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (SysRoleDefine.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (SysRoleDefine.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string RoleName,int SubSysId,bool? IsDefault,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    SysRoleDefine item = new SysRoleDefine();
		    
            item.RoleName = RoleName;
            
            item.SubSysId = SubSysId;
            
            item.IsDefault = IsDefault;
            
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
	    public void Update(int Id,string RoleName,int SubSysId,bool? IsDefault,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    SysRoleDefine item = new SysRoleDefine();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.RoleName = RoleName;
				
			item.SubSysId = SubSysId;
				
			item.IsDefault = IsDefault;
				
			item.CreatedBy = CreatedBy;
				
			item.CreatedOn = CreatedOn;
				
			item.ModifiedBy = ModifiedBy;
				
			item.ModifiedOn = ModifiedOn;
				
	        item.Save(UserName);
	    }
    }
}
