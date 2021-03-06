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
    /// Strongly-typed collection for the VFinancePayLastDate class.
    /// </summary>
    [Serializable]
    public partial class VFinancePayLastDateCollection : ReadOnlyList<VFinancePayLastDate, VFinancePayLastDateCollection>
    {        
        public VFinancePayLastDateCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the v_FinancePay_LastDate view.
    /// </summary>
    [Serializable]
    public partial class VFinancePayLastDate : ReadOnlyRecord<VFinancePayLastDate>, IReadOnlyRecord
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
                    BaseSchema = DB.GetSchema("v_FinancePay_LastDate");
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
	    public VFinancePayLastDate()
	    {
            //SetDefaults();
            //MarkNew(); 
            if(BaseSchema ==null) 
                BaseSchema = DB.GetSchema("v_FinancePay_LastDate");
        }
        public VFinancePayLastDate(bool useDatabaseDefaults)
	    { 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("v_FinancePay_LastDate");
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			//MarkNew();
	    }
	    
	    public VFinancePayLastDate(object keyID)
	    { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_FinancePay_LastDate");
		    LoadByKey(keyID);
	    }
    	 
	    public VFinancePayLastDate(string columnName, object columnValue)
        { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_FinancePay_LastDate");
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("PaymentRateCompanyID")]
        [Bindable(true)]
        public int? PaymentRateCompanyID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("PaymentRateCompanyID");
		    }
            set 
		    {
			    SetColumnValue("PaymentRateCompanyID", value);
            }
        }
	      
        [XmlAttribute("PaymentRateDate")]
        [Bindable(true)]
        public DateTime? PaymentRateDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime?>("PaymentRateDate");
		    }
            set 
		    {
			    SetColumnValue("PaymentRateDate", value);
            }
        }
	    
	    #endregion 
        #region Typed Columns
        
        public static TableSchema.TableColumn
        PaymentRateCompanyIDColumn { get { return Schema.Columns[0]; } }
        
        public static TableSchema.TableColumn
        PaymentRateDateColumn { get { return Schema.Columns[1]; } }
        
        #endregion #region Columns Struct    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string PaymentRateCompanyID = @"PaymentRateCompanyID";
            
            public static string PaymentRateDate = @"PaymentRateDate";
            
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
