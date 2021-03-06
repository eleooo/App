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
    /// Strongly-typed collection for the VCompanyLastThisMonthOrderAndCash class.
    /// </summary>
    [Serializable]
    public partial class VCompanyLastThisMonthOrderAndCashCollection : ReadOnlyList<VCompanyLastThisMonthOrderAndCash, VCompanyLastThisMonthOrderAndCashCollection>
    {        
        public VCompanyLastThisMonthOrderAndCashCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the v_Company_LastThisMonthOrderAndCash view.
    /// </summary>
    [Serializable]
    public partial class VCompanyLastThisMonthOrderAndCash : ReadOnlyRecord<VCompanyLastThisMonthOrderAndCash>, IReadOnlyRecord
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
                    BaseSchema = DB.GetSchema("v_Company_LastThisMonthOrderAndCash");
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
	    public VCompanyLastThisMonthOrderAndCash()
	    {
            //SetDefaults();
            //MarkNew(); 
            if(BaseSchema ==null) 
                BaseSchema = DB.GetSchema("v_Company_LastThisMonthOrderAndCash");
        }
        public VCompanyLastThisMonthOrderAndCash(bool useDatabaseDefaults)
	    { 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("v_Company_LastThisMonthOrderAndCash");
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			//MarkNew();
	    }
	    
	    public VCompanyLastThisMonthOrderAndCash(object keyID)
	    { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_Company_LastThisMonthOrderAndCash");
		    LoadByKey(keyID);
	    }
    	 
	    public VCompanyLastThisMonthOrderAndCash(string columnName, object columnValue)
        { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_Company_LastThisMonthOrderAndCash");
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
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
	      
        [XmlAttribute("LastMonthOrderSum")]
        [Bindable(true)]
        public decimal? LastMonthOrderSum 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("LastMonthOrderSum");
		    }
            set 
		    {
			    SetColumnValue("LastMonthOrderSum", value);
            }
        }
	      
        [XmlAttribute("ThisMonthOrderSum")]
        [Bindable(true)]
        public decimal? ThisMonthOrderSum 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("ThisMonthOrderSum");
		    }
            set 
		    {
			    SetColumnValue("ThisMonthOrderSum", value);
            }
        }
	      
        [XmlAttribute("LastMonthPaymentCashSum")]
        [Bindable(true)]
        public decimal? LastMonthPaymentCashSum 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("LastMonthPaymentCashSum");
		    }
            set 
		    {
			    SetColumnValue("LastMonthPaymentCashSum", value);
            }
        }
	      
        [XmlAttribute("ThisMonthPaymentCashSum")]
        [Bindable(true)]
        public decimal? ThisMonthPaymentCashSum 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("ThisMonthPaymentCashSum");
		    }
            set 
		    {
			    SetColumnValue("ThisMonthPaymentCashSum", value);
            }
        }
	    
	    #endregion 
        #region Typed Columns
        
        public static TableSchema.TableColumn
        CompanyIDColumn { get { return Schema.Columns[0]; } }
        
        public static TableSchema.TableColumn
        LastMonthOrderSumColumn { get { return Schema.Columns[1]; } }
        
        public static TableSchema.TableColumn
        ThisMonthOrderSumColumn { get { return Schema.Columns[2]; } }
        
        public static TableSchema.TableColumn
        LastMonthPaymentCashSumColumn { get { return Schema.Columns[3]; } }
        
        public static TableSchema.TableColumn
        ThisMonthPaymentCashSumColumn { get { return Schema.Columns[4]; } }
        
        #endregion #region Columns Struct    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string CompanyID = @"CompanyID";
            
            public static string LastMonthOrderSum = @"LastMonthOrderSum";
            
            public static string ThisMonthOrderSum = @"ThisMonthOrderSum";
            
            public static string LastMonthPaymentCashSum = @"LastMonthPaymentCashSum";
            
            public static string ThisMonthPaymentCashSum = @"ThisMonthPaymentCashSum";
            
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
