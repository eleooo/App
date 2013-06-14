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
    /// Controller class for Sys_Article
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysArticleController
    {
        // Preload our schema..
        SysArticle thisSchemaLoad = new SysArticle();
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
        public SysArticleCollection FetchAll()
        {
            SysArticleCollection coll = new SysArticleCollection();
            Query qry = new Query(SysArticle.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysArticleCollection FetchByID(object Id)
        {
            SysArticleCollection coll = new SysArticleCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysArticleCollection FetchByQuery(Query qry)
        {
            SysArticleCollection coll = new SysArticleCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (SysArticle.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (SysArticle.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Title,string Content,string KeyWord,int? Hits,int? CreatedBy,DateTime? CreatedOn,int? AuditedBy,DateTime? AuditedOn,int? UId,int? CId,string Pic,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    SysArticle item = new SysArticle();
		    
            item.Title = Title;
            
            item.Content = Content;
            
            item.KeyWord = KeyWord;
            
            item.Hits = Hits;
            
            item.CreatedBy = CreatedBy;
            
            item.CreatedOn = CreatedOn;
            
            item.AuditedBy = AuditedBy;
            
            item.AuditedOn = AuditedOn;
            
            item.UId = UId;
            
            item.CId = CId;
            
            item.Pic = Pic;
            
            item.ModifiedBy = ModifiedBy;
            
            item.ModifiedOn = ModifiedOn;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,string Title,string Content,string KeyWord,int? Hits,int? CreatedBy,DateTime? CreatedOn,int? AuditedBy,DateTime? AuditedOn,int? UId,int? CId,string Pic,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    SysArticle item = new SysArticle();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.Title = Title;
				
			item.Content = Content;
				
			item.KeyWord = KeyWord;
				
			item.Hits = Hits;
				
			item.CreatedBy = CreatedBy;
				
			item.CreatedOn = CreatedOn;
				
			item.AuditedBy = AuditedBy;
				
			item.AuditedOn = AuditedOn;
				
			item.UId = UId;
				
			item.CId = CId;
				
			item.Pic = Pic;
				
			item.ModifiedBy = ModifiedBy;
				
			item.ModifiedOn = ModifiedOn;
				
	        item.Save(UserName);
	    }
    }
}
