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
    /// Controller class for PaymentCash
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class PaymentCashController
    {
        // Preload our schema..
        PaymentCash thisSchemaLoad = new PaymentCash();
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
        public PaymentCashCollection FetchAll()
        {
            PaymentCashCollection coll = new PaymentCashCollection();
            Query qry = new Query(PaymentCash.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PaymentCashCollection FetchByID(object Id)
        {
            PaymentCashCollection coll = new PaymentCashCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public PaymentCashCollection FetchByQuery(Query qry)
        {
            PaymentCashCollection coll = new PaymentCashCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (PaymentCash.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (PaymentCash.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string PaymentCashCode,DateTime PaymentCashDate,int? PaymentCashMemberID,int? PaymentCashCompanyID,decimal PaymentCashSum,int PaymentStatus,int? PaymentType,int? PaymentOrderID,string PaymentMemo,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    PaymentCash item = new PaymentCash();
		    
            item.PaymentCashCode = PaymentCashCode;
            
            item.PaymentCashDate = PaymentCashDate;
            
            item.PaymentCashMemberID = PaymentCashMemberID;
            
            item.PaymentCashCompanyID = PaymentCashCompanyID;
            
            item.PaymentCashSum = PaymentCashSum;
            
            item.PaymentStatus = PaymentStatus;
            
            item.PaymentType = PaymentType;
            
            item.PaymentOrderID = PaymentOrderID;
            
            item.PaymentMemo = PaymentMemo;
            
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
	    public void Update(int Id,string PaymentCashCode,DateTime PaymentCashDate,int? PaymentCashMemberID,int? PaymentCashCompanyID,decimal PaymentCashSum,int PaymentStatus,int? PaymentType,int? PaymentOrderID,string PaymentMemo,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn)
	    {
		    PaymentCash item = new PaymentCash();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.PaymentCashCode = PaymentCashCode;
				
			item.PaymentCashDate = PaymentCashDate;
				
			item.PaymentCashMemberID = PaymentCashMemberID;
				
			item.PaymentCashCompanyID = PaymentCashCompanyID;
				
			item.PaymentCashSum = PaymentCashSum;
				
			item.PaymentStatus = PaymentStatus;
				
			item.PaymentType = PaymentType;
				
			item.PaymentOrderID = PaymentOrderID;
				
			item.PaymentMemo = PaymentMemo;
				
			item.CreatedBy = CreatedBy;
				
			item.CreatedOn = CreatedOn;
				
			item.ModifiedBy = ModifiedBy;
				
			item.ModifiedOn = ModifiedOn;
				
	        item.Save(UserName);
	    }
    }
}
