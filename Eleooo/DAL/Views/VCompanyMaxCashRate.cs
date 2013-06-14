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
    /// Strongly-typed collection for the VCompanyMaxCashRate class.
    /// </summary>
    [Serializable]
    public partial class VCompanyMaxCashRateCollection : ReadOnlyList<VCompanyMaxCashRate, VCompanyMaxCashRateCollection>
    {        
        public VCompanyMaxCashRateCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the v_Company_MaxCashRate view.
    /// </summary>
    [Serializable]
    public partial class VCompanyMaxCashRate : ReadOnlyRecord<VCompanyMaxCashRate>, IReadOnlyRecord
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
                    BaseSchema = DB.GetSchema("v_Company_MaxCashRate");
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
	    public VCompanyMaxCashRate()
	    {
            //SetDefaults();
            //MarkNew(); 
            if(BaseSchema ==null) 
                BaseSchema = DB.GetSchema("v_Company_MaxCashRate");
        }
        public VCompanyMaxCashRate(bool useDatabaseDefaults)
	    { 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("v_Company_MaxCashRate");
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			//MarkNew();
	    }
	    
	    public VCompanyMaxCashRate(object keyID)
	    { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_Company_MaxCashRate");
		    LoadByKey(keyID);
	    }
    	 
	    public VCompanyMaxCashRate(string columnName, object columnValue)
        { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_Company_MaxCashRate");
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("CashRate")]
        [Bindable(true)]
        public decimal? CashRate 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("CashRate");
		    }
            set 
		    {
			    SetColumnValue("CashRate", value);
            }
        }
	      
        [XmlAttribute("CashCompanyID")]
        [Bindable(true)]
        public int? CashCompanyID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CashCompanyID");
		    }
            set 
		    {
			    SetColumnValue("CashCompanyID", value);
            }
        }
	    
	    #endregion 
        #region Typed Columns
        
        public static TableSchema.TableColumn
        CashRateColumn { get { return Schema.Columns[0]; } }
        
        public static TableSchema.TableColumn
        CashCompanyIDColumn { get { return Schema.Columns[1]; } }
        
        #endregion #region Columns Struct    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string CashRate = @"CashRate";
            
            public static string CashCompanyID = @"CashCompanyID";
            
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
