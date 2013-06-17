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
    /// Controller class for Sys_Company
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class SysCompanyController
    {
        // Preload our schema..
        SysCompany thisSchemaLoad = new SysCompany();
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
        public SysCompanyCollection FetchAll()
        {
            SysCompanyCollection coll = new SysCompanyCollection();
            Query qry = new Query(SysCompany.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysCompanyCollection FetchByID(object Id)
        {
            SysCompanyCollection coll = new SysCompanyCollection().Where("ID", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public SysCompanyCollection FetchByQuery(Query qry)
        {
            SysCompanyCollection coll = new SysCompanyCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (SysCompany.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (SysCompany.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string CompanyCode,string CompanyName,string CompanyPwd,string CompanyEmail,string CompanyProvince,int? CompanyCity,string CompanyArea,string CompanyLocation,string AreaDepth,string CompanyAddress,string CompanyTel,string CompanyPhone,string CompanyMsn,string CompanySkype,string CompanyPhoto,string CompanyIntro,string CompanyContent,bool? IsUseFinger,DateTime? CompanyDate,DateTime? CompanyDateView,int? CompanyStatus,string CompanyMemo,string CompanyRate,string CompanyRateSale,decimal? CompanyRateMaster,int? CompanySaleCount,decimal? CompanySaleSum,decimal? CompanyBalance,decimal? CompanyBalanceCash,int? CompanyFacebookCount,string CompanyToken,int? CompanyType,string CompanyItem,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn,string CompanyWorkTime,string CompanyServices,string MsnPhoneNum,string OrderElapsed,int? OrderMaxAmount,bool? IsUseMsg,decimal? OnSetSum,decimal? ServiceSum,DateTime? MenuDate,DateTime? SetTopDate,bool? IsPoint,bool? IsOnSale,bool? IsSuspend)
	    {
		    SysCompany item = new SysCompany();
		    
            item.CompanyCode = CompanyCode;
            
            item.CompanyName = CompanyName;
            
            item.CompanyPwd = CompanyPwd;
            
            item.CompanyEmail = CompanyEmail;
            
            item.CompanyProvince = CompanyProvince;
            
            item.CompanyCity = CompanyCity;
            
            item.CompanyArea = CompanyArea;
            
            item.CompanyLocation = CompanyLocation;
            
            item.AreaDepth = AreaDepth;
            
            item.CompanyAddress = CompanyAddress;
            
            item.CompanyTel = CompanyTel;
            
            item.CompanyPhone = CompanyPhone;
            
            item.CompanyMsn = CompanyMsn;
            
            item.CompanySkype = CompanySkype;
            
            item.CompanyPhoto = CompanyPhoto;
            
            item.CompanyIntro = CompanyIntro;
            
            item.CompanyContent = CompanyContent;
            
            item.IsUseFinger = IsUseFinger;
            
            item.CompanyDate = CompanyDate;
            
            item.CompanyDateView = CompanyDateView;
            
            item.CompanyStatus = CompanyStatus;
            
            item.CompanyMemo = CompanyMemo;
            
            item.CompanyRate = CompanyRate;
            
            item.CompanyRateSale = CompanyRateSale;
            
            item.CompanyRateMaster = CompanyRateMaster;
            
            item.CompanySaleCount = CompanySaleCount;
            
            item.CompanySaleSum = CompanySaleSum;
            
            item.CompanyBalance = CompanyBalance;
            
            item.CompanyBalanceCash = CompanyBalanceCash;
            
            item.CompanyFacebookCount = CompanyFacebookCount;
            
            item.CompanyToken = CompanyToken;
            
            item.CompanyType = CompanyType;
            
            item.CompanyItem = CompanyItem;
            
            item.CreatedBy = CreatedBy;
            
            item.CreatedOn = CreatedOn;
            
            item.ModifiedBy = ModifiedBy;
            
            item.ModifiedOn = ModifiedOn;
            
            item.CompanyWorkTime = CompanyWorkTime;
            
            item.CompanyServices = CompanyServices;
            
            item.MsnPhoneNum = MsnPhoneNum;
            
            item.OrderElapsed = OrderElapsed;
            
            item.OrderMaxAmount = OrderMaxAmount;
            
            item.IsUseMsg = IsUseMsg;
            
            item.OnSetSum = OnSetSum;
            
            item.ServiceSum = ServiceSum;
            
            item.MenuDate = MenuDate;
            
            item.SetTopDate = SetTopDate;
            
            item.IsPoint = IsPoint;
            
            item.IsOnSale = IsOnSale;
            
            item.IsSuspend = IsSuspend;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(int Id,string CompanyCode,string CompanyName,string CompanyPwd,string CompanyEmail,string CompanyProvince,int? CompanyCity,string CompanyArea,string CompanyLocation,string AreaDepth,string CompanyAddress,string CompanyTel,string CompanyPhone,string CompanyMsn,string CompanySkype,string CompanyPhoto,string CompanyIntro,string CompanyContent,bool? IsUseFinger,DateTime? CompanyDate,DateTime? CompanyDateView,int? CompanyStatus,string CompanyMemo,string CompanyRate,string CompanyRateSale,decimal? CompanyRateMaster,int? CompanySaleCount,decimal? CompanySaleSum,decimal? CompanyBalance,decimal? CompanyBalanceCash,int? CompanyFacebookCount,string CompanyToken,int? CompanyType,string CompanyItem,int? CreatedBy,DateTime? CreatedOn,int? ModifiedBy,DateTime? ModifiedOn,string CompanyWorkTime,string CompanyServices,string MsnPhoneNum,string OrderElapsed,int? OrderMaxAmount,bool? IsUseMsg,decimal? OnSetSum,decimal? ServiceSum,DateTime? MenuDate,DateTime? SetTopDate,bool? IsPoint,bool? IsOnSale,bool? IsSuspend)
	    {
		    SysCompany item = new SysCompany();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.CompanyCode = CompanyCode;
				
			item.CompanyName = CompanyName;
				
			item.CompanyPwd = CompanyPwd;
				
			item.CompanyEmail = CompanyEmail;
				
			item.CompanyProvince = CompanyProvince;
				
			item.CompanyCity = CompanyCity;
				
			item.CompanyArea = CompanyArea;
				
			item.CompanyLocation = CompanyLocation;
				
			item.AreaDepth = AreaDepth;
				
			item.CompanyAddress = CompanyAddress;
				
			item.CompanyTel = CompanyTel;
				
			item.CompanyPhone = CompanyPhone;
				
			item.CompanyMsn = CompanyMsn;
				
			item.CompanySkype = CompanySkype;
				
			item.CompanyPhoto = CompanyPhoto;
				
			item.CompanyIntro = CompanyIntro;
				
			item.CompanyContent = CompanyContent;
				
			item.IsUseFinger = IsUseFinger;
				
			item.CompanyDate = CompanyDate;
				
			item.CompanyDateView = CompanyDateView;
				
			item.CompanyStatus = CompanyStatus;
				
			item.CompanyMemo = CompanyMemo;
				
			item.CompanyRate = CompanyRate;
				
			item.CompanyRateSale = CompanyRateSale;
				
			item.CompanyRateMaster = CompanyRateMaster;
				
			item.CompanySaleCount = CompanySaleCount;
				
			item.CompanySaleSum = CompanySaleSum;
				
			item.CompanyBalance = CompanyBalance;
				
			item.CompanyBalanceCash = CompanyBalanceCash;
				
			item.CompanyFacebookCount = CompanyFacebookCount;
				
			item.CompanyToken = CompanyToken;
				
			item.CompanyType = CompanyType;
				
			item.CompanyItem = CompanyItem;
				
			item.CreatedBy = CreatedBy;
				
			item.CreatedOn = CreatedOn;
				
			item.ModifiedBy = ModifiedBy;
				
			item.ModifiedOn = ModifiedOn;
				
			item.CompanyWorkTime = CompanyWorkTime;
				
			item.CompanyServices = CompanyServices;
				
			item.MsnPhoneNum = MsnPhoneNum;
				
			item.OrderElapsed = OrderElapsed;
				
			item.OrderMaxAmount = OrderMaxAmount;
				
			item.IsUseMsg = IsUseMsg;
				
			item.OnSetSum = OnSetSum;
				
			item.ServiceSum = ServiceSum;
				
			item.MenuDate = MenuDate;
				
			item.SetTopDate = SetTopDate;
				
			item.IsPoint = IsPoint;
				
			item.IsOnSale = IsOnSale;
				
			item.IsSuspend = IsSuspend;
				
	        item.Save(UserName);
	    }
    }
}
