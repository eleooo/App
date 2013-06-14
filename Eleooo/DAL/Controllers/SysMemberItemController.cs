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
    /// Controller class for Sys_Member_Item
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysMemberItemController
    {
        // Preload our schema..
        SysMemberItem thisSchemaLoad = new SysMemberItem();
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
        public SysMemberItemCollection FetchAll()
        {
            SysMemberItemCollection coll = new SysMemberItemCollection();
            Query qry = new Query(SysMemberItem.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysMemberItemCollection FetchByID(object ItemID)
        {
            SysMemberItemCollection coll = new SysMemberItemCollection().Where("ItemID", ItemID).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysMemberItemCollection FetchByQuery(Query qry)
        {
            SysMemberItemCollection coll = new SysMemberItemCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object ItemID)
        {
            return (SysMemberItem.Delete(ItemID) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object ItemID)
        {
            return (SysMemberItem.Destroy(ItemID) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(int CompanyItemID,int MemberID,DateTime ItemDate,int ItemStatus,DateTime? SetDate,decimal? OrderSum,int? CompanyID,decimal? ItemPoint,int? PaymentID,bool? IsCanModifiedDate,DateTime? OrderDate,int? OrderID)
	    {
		    SysMemberItem item = new SysMemberItem();
		    
            item.CompanyItemID = CompanyItemID;
            
            item.MemberID = MemberID;
            
            item.ItemDate = ItemDate;
            
            item.ItemStatus = ItemStatus;
            
            item.SetDate = SetDate;
            
            item.OrderSum = OrderSum;
            
            item.CompanyID = CompanyID;
            
            item.ItemPoint = ItemPoint;
            
            item.PaymentID = PaymentID;
            
            item.IsCanModifiedDate = IsCanModifiedDate;
            
            item.OrderDate = OrderDate;
            
            item.OrderID = OrderID;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int ItemID,int CompanyItemID,int MemberID,DateTime ItemDate,int ItemStatus,DateTime? SetDate,decimal? OrderSum,int? CompanyID,decimal? ItemPoint,int? PaymentID,bool? IsCanModifiedDate,DateTime? OrderDate,int? OrderID)
	    {
		    SysMemberItem item = new SysMemberItem();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.ItemID = ItemID;
				
			item.CompanyItemID = CompanyItemID;
				
			item.MemberID = MemberID;
				
			item.ItemDate = ItemDate;
				
			item.ItemStatus = ItemStatus;
				
			item.SetDate = SetDate;
				
			item.OrderSum = OrderSum;
				
			item.CompanyID = CompanyID;
				
			item.ItemPoint = ItemPoint;
				
			item.PaymentID = PaymentID;
				
			item.IsCanModifiedDate = IsCanModifiedDate;
				
			item.OrderDate = OrderDate;
				
			item.OrderID = OrderID;
				
	        item.Save(UserName);
	    }
    }
}
