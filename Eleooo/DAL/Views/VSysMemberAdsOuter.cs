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
namespace Eleooo.DAL{
    /// <summary>
    /// Strongly-typed collection for the VSysMemberAdsOuter class.
    /// </summary>
    [Serializable]
    public partial class VSysMemberAdsOuterCollection : ReadOnlyList<VSysMemberAdsOuter, VSysMemberAdsOuterCollection>
    {        
        public VSysMemberAdsOuterCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the v_Sys_Member_Ads_Outer view.
    /// </summary>
    [Serializable]
    public partial class VSysMemberAdsOuter : ReadOnlyRecord<VSysMemberAdsOuter>, IReadOnlyRecord
    {
    
	    #region Default Settings
	    #endregion
        #region Schema Accessor
	    public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                {
                    BaseSchema = DB.GetSchema("v_Sys_Member_Ads_Outer");
                }
                return BaseSchema;
            }
        }
    	
        #endregion
        
        #region Query Accessor
	    public static Query CreateQuery()
	    {
		    return new Query(Schema);
	    }
	    #endregion
	    
	    #region .ctors
	    public VSysMemberAdsOuter()
	    {
            //SetDefaults();
            //MarkNew(); 
            if(BaseSchema ==null) 
                BaseSchema = DB.GetSchema("v_Sys_Member_Ads_Outer");
        }
        public VSysMemberAdsOuter(bool useDatabaseDefaults)
	    { 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("v_Sys_Member_Ads_Outer");
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			//MarkNew();
	    }
	    
	    public VSysMemberAdsOuter(object keyID)
	    { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_Sys_Member_Ads_Outer");
		    LoadByKey(keyID);
	    }
    	 
	    public VSysMemberAdsOuter(string columnName, object columnValue)
        { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_Sys_Member_Ads_Outer");
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("AdsID")]
        [Bindable(true)]
        public int AdsID 
	    {
		    get
		    {
			    return GetColumnValue<int>("AdsID");
		    }
            set 
		    {
			    SetColumnValue("AdsID", value);
            }
        }
	      
        [XmlAttribute("AdsMemberID")]
        [Bindable(true)]
        public int? AdsMemberID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("AdsMemberID");
		    }
            set 
		    {
			    SetColumnValue("AdsMemberID", value);
            }
        }
	      
        [XmlAttribute("CompanyAdsID")]
        [Bindable(true)]
        public int? CompanyAdsID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CompanyAdsID");
		    }
            set 
		    {
			    SetColumnValue("CompanyAdsID", value);
            }
        }
	      
        [XmlAttribute("AdsPoint")]
        [Bindable(true)]
        public decimal? AdsPoint 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("AdsPoint");
		    }
            set 
		    {
			    SetColumnValue("AdsPoint", value);
            }
        }
	      
        [XmlAttribute("AdsDate")]
        [Bindable(true)]
        public DateTime? AdsDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("AdsDate");
		    }
            set 
		    {
			    SetColumnValue("AdsDate", value);
            }
        }
	      
        [XmlAttribute("OrderSum")]
        [Bindable(true)]
        public decimal? OrderSum 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("OrderSum");
		    }
            set 
		    {
			    SetColumnValue("OrderSum", value);
            }
        }
	      
        [XmlAttribute("CompanyID")]
        [Bindable(true)]
        public int? CompanyID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CompanyID");
		    }
            set 
		    {
			    SetColumnValue("CompanyID", value);
            }
        }
	    
	    #endregion 
        #region Typed Columns
        
        public static TableSchema.TableColumn
        AdsIDColumn { get { return Schema.Columns[0]; } }
        
        public static TableSchema.TableColumn
        AdsMemberIDColumn { get { return Schema.Columns[1]; } }
        
        public static TableSchema.TableColumn
        CompanyAdsIDColumn { get { return Schema.Columns[2]; } }
        
        public static TableSchema.TableColumn
        AdsPointColumn { get { return Schema.Columns[3]; } }
        
        public static TableSchema.TableColumn
        AdsDateColumn { get { return Schema.Columns[4]; } }
        
        public static TableSchema.TableColumn
        OrderSumColumn { get { return Schema.Columns[5]; } }
        
        public static TableSchema.TableColumn
        CompanyIDColumn { get { return Schema.Columns[6]; } }
        
        #endregion #region Columns Struct    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string AdsID = @"AdsID";
            
            public static string AdsMemberID = @"AdsMemberID";
            
            public static string CompanyAdsID = @"CompanyAdsID";
            
            public static string AdsPoint = @"AdsPoint";
            
            public static string AdsDate = @"AdsDate";
            
            public static string OrderSum = @"OrderSum";
            
            public static string CompanyID = @"CompanyID";
            
	    }
	    #endregion
	    
	    
	    #region IAbstractRecord Members
        public new CT GetColumnValue<CT>(string columnName) {
            return base.GetColumnValue<CT>(columnName);
        }
        public object GetColumnValue(string columnName) {
            return base.GetColumnValue<object>(columnName);
        }
        #endregion
	    
    }
}
