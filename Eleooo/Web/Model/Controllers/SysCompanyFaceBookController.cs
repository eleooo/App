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
    /// Controller class for Sys_Company_FaceBook
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysCompanyFaceBookController
    {
        // Preload our schema..
        SysCompanyFaceBook thisSchemaLoad = new SysCompanyFaceBook();
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
        public SysCompanyFaceBookCollection FetchAll()
        {
            SysCompanyFaceBookCollection coll = new SysCompanyFaceBookCollection();
            Query qry = new Query(SysCompanyFaceBook.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysCompanyFaceBookCollection FetchByID(object Id)
        {
            SysCompanyFaceBookCollection coll = new SysCompanyFaceBookCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysCompanyFaceBookCollection FetchByQuery(Query qry)
        {
            SysCompanyFaceBookCollection coll = new SysCompanyFaceBookCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (SysCompanyFaceBook.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (SysCompanyFaceBook.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(DateTime? FaceBookDate,string FaceBookMemo,int? FaceBookBizID,int? FaceBookMemberID,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn,int? FaceBookBizType,int? ReplyMemberID,string ReplyMemo,DateTime? ReplyDate,int? FaceBookRate,DateTime? LatestOrderDate,int? PBizID,bool? IsRead,DateTime? TopDate)
	    {
		    SysCompanyFaceBook item = new SysCompanyFaceBook();
		    
            item.FaceBookDate = FaceBookDate;
            
            item.FaceBookMemo = FaceBookMemo;
            
            item.FaceBookBizID = FaceBookBizID;
            
            item.FaceBookMemberID = FaceBookMemberID;
            
            item.CreatedBy = CreatedBy;
            
            item.CreatedOn = CreatedOn;
            
            item.ModifiedBy = ModifiedBy;
            
            item.ModifiedOn = ModifiedOn;
            
            item.FaceBookBizType = FaceBookBizType;
            
            item.ReplyMemberID = ReplyMemberID;
            
            item.ReplyMemo = ReplyMemo;
            
            item.ReplyDate = ReplyDate;
            
            item.FaceBookRate = FaceBookRate;
            
            item.LatestOrderDate = LatestOrderDate;
            
            item.PBizID = PBizID;
            
            item.IsRead = IsRead;
            
            item.TopDate = TopDate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,DateTime? FaceBookDate,string FaceBookMemo,int? FaceBookBizID,int? FaceBookMemberID,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn,int? FaceBookBizType,int? ReplyMemberID,string ReplyMemo,DateTime? ReplyDate,int? FaceBookRate,DateTime? LatestOrderDate,int? PBizID,bool? IsRead,DateTime? TopDate)
	    {
		    SysCompanyFaceBook item = new SysCompanyFaceBook();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.FaceBookDate = FaceBookDate;
				
			item.FaceBookMemo = FaceBookMemo;
				
			item.FaceBookBizID = FaceBookBizID;
				
			item.FaceBookMemberID = FaceBookMemberID;
				
			item.CreatedBy = CreatedBy;
				
			item.CreatedOn = CreatedOn;
				
			item.ModifiedBy = ModifiedBy;
				
			item.ModifiedOn = ModifiedOn;
				
			item.FaceBookBizType = FaceBookBizType;
				
			item.ReplyMemberID = ReplyMemberID;
				
			item.ReplyMemo = ReplyMemo;
				
			item.ReplyDate = ReplyDate;
				
			item.FaceBookRate = FaceBookRate;
				
			item.LatestOrderDate = LatestOrderDate;
				
			item.PBizID = PBizID;
				
			item.IsRead = IsRead;
				
			item.TopDate = TopDate;
				
	        item.Save(UserName);
	    }
    }
}
