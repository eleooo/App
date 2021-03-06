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
    /// Strongly-typed collection for the VFinancePayCashLastDate class.
    /// </summary>
    [Serializable]
    public partial class VFinancePayCashLastDateCollection : ReadOnlyList<VFinancePayCashLastDate, VFinancePayCashLastDateCollection>
    {        
        public VFinancePayCashLastDateCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the v_FinancePayCash_LastDate view.
    /// </summary>
    [Serializable]
    public partial class VFinancePayCashLastDate : ReadOnlyRecord<VFinancePayCashLastDate>, IReadOnlyRecord
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
                    BaseSchema = DB.GetSchema("v_FinancePayCash_LastDate");
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
	    public VFinancePayCashLastDate()
	    {
            //SetDefaults();
            //MarkNew(); 
            if(BaseSchema ==null) 
                BaseSchema = DB.GetSchema("v_FinancePayCash_LastDate");
        }
        public VFinancePayCashLastDate(bool useDatabaseDefaults)
	    { 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("v_FinancePayCash_LastDate");
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			//MarkNew();
	    }
	    
	    public VFinancePayCashLastDate(object keyID)
	    { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_FinancePayCash_LastDate");
		    LoadByKey(keyID);
	    }
    	 
	    public VFinancePayCashLastDate(string columnName, object columnValue)
        { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_FinancePayCash_LastDate");
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("PaymentRateCashCompanyID")]
        [Bindable(true)]
        public int? PaymentRateCashCompanyID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("PaymentRateCashCompanyID");
		    }
            set 
		    {
			    SetColumnValue("PaymentRateCashCompanyID", value);
            }
        }
	      
        [XmlAttribute("PaymentRateCashDate")]
        [Bindable(true)]
        public DateTime? PaymentRateCashDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("PaymentRateCashDate");
		    }
            set 
		    {
			    SetColumnValue("PaymentRateCashDate", value);
            }
        }
	    
	    #endregion 
        #region Typed Columns
        
        public static TableSchema.TableColumn
        PaymentRateCashCompanyIDColumn { get { return Schema.Columns[0]; } }
        
        public static TableSchema.TableColumn
        PaymentRateCashDateColumn { get { return Schema.Columns[1]; } }
        
        #endregion #region Columns Struct    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string PaymentRateCashCompanyID = @"PaymentRateCashCompanyID";
            
            public static string PaymentRateCashDate = @"PaymentRateCashDate";
            
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
