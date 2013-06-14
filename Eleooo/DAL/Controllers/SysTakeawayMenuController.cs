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
    /// Controller class for Sys_Takeaway_Menu
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysTakeawayMenuController
    {
        // Preload our schema..
        SysTakeawayMenu thisSchemaLoad = new SysTakeawayMenu();
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
        public SysTakeawayMenuCollection FetchAll()
        {
            SysTakeawayMenuCollection coll = new SysTakeawayMenuCollection();
            Query qry = new Query(SysTakeawayMenu.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysTakeawayMenuCollection FetchByID(object Id)
        {
            SysTakeawayMenuCollection coll = new SysTakeawayMenuCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysTakeawayMenuCollection FetchByQuery(Query qry)
        {
            SysTakeawayMenuCollection coll = new SysTakeawayMenuCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (SysTakeawayMenu.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (SysTakeawayMenu.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Name,decimal? Price,int? DirID,string Code,int? CompanyID,bool? IsDeleted,bool? IsOutOfStock,DateTime? OutOfStockDate)
	    {
		    SysTakeawayMenu item = new SysTakeawayMenu();
		    
            item.Name = Name;
            
            item.Price = Price;
            
            item.DirID = DirID;
            
            item.Code = Code;
            
            item.CompanyID = CompanyID;
            
            item.IsDeleted = IsDeleted;
            
            item.IsOutOfStock = IsOutOfStock;
            
            item.OutOfStockDate = OutOfStockDate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,string Name,decimal? Price,int? DirID,string Code,int? CompanyID,bool? IsDeleted,bool? IsOutOfStock,DateTime? OutOfStockDate)
	    {
		    SysTakeawayMenu item = new SysTakeawayMenu();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.Name = Name;
				
			item.Price = Price;
				
			item.DirID = DirID;
				
			item.Code = Code;
				
			item.CompanyID = CompanyID;
				
			item.IsDeleted = IsDeleted;
				
			item.IsOutOfStock = IsOutOfStock;
				
			item.OutOfStockDate = OutOfStockDate;
				
	        item.Save(UserName);
	    }
    }
}
