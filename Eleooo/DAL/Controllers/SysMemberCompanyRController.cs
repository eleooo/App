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
    /// Controller class for Sys_Member_CompanyR
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysMemberCompanyRController
    {
        // Preload our schema..
        SysMemberCompanyR thisSchemaLoad = new SysMemberCompanyR();
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
        public SysMemberCompanyRCollection FetchAll()
        {
            SysMemberCompanyRCollection coll = new SysMemberCompanyRCollection();
            Query qry = new Query(SysMemberCompanyR.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysMemberCompanyRCollection FetchByID(object Id)
        {
            SysMemberCompanyRCollection coll = new SysMemberCompanyRCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysMemberCompanyRCollection FetchByQuery(Query qry)
        {
            SysMemberCompanyRCollection coll = new SysMemberCompanyRCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (SysMemberCompanyR.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (SysMemberCompanyR.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(DateTime? CompanyDate,string CompanyName,string CompanyAddress,string CompanyDesc,string CompanyTel,int? CompanyMemberID,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn,int? CompanyStatus)
	    {
		    SysMemberCompanyR item = new SysMemberCompanyR();
		    
            item.CompanyDate = CompanyDate;
            
            item.CompanyName = CompanyName;
            
            item.CompanyAddress = CompanyAddress;
            
            item.CompanyDesc = CompanyDesc;
            
            item.CompanyTel = CompanyTel;
            
            item.CompanyMemberID = CompanyMemberID;
            
            item.CreatedBy = CreatedBy;
            
            item.CreatedOn = CreatedOn;
            
            item.ModifiedBy = ModifiedBy;
            
            item.ModifiedOn = ModifiedOn;
            
            item.CompanyStatus = CompanyStatus;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,DateTime? CompanyDate,string CompanyName,string CompanyAddress,string CompanyDesc,string CompanyTel,int? CompanyMemberID,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn,int? CompanyStatus)
	    {
		    SysMemberCompanyR item = new SysMemberCompanyR();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.CompanyDate = CompanyDate;
				
			item.CompanyName = CompanyName;
				
			item.CompanyAddress = CompanyAddress;
				
			item.CompanyDesc = CompanyDesc;
				
			item.CompanyTel = CompanyTel;
				
			item.CompanyMemberID = CompanyMemberID;
				
			item.CreatedBy = CreatedBy;
				
			item.CreatedOn = CreatedOn;
				
			item.ModifiedBy = ModifiedBy;
				
			item.ModifiedOn = ModifiedOn;
				
			item.CompanyStatus = CompanyStatus;
				
	        item.Save(UserName);
	    }
    }
}
