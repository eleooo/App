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
    /// Strongly-typed collection for the VFinancePayCashCashCompany2 class.
    /// </summary>
    [Serializable]
    public partial class VFinancePayCashCashCompany2Collection : ReadOnlyList<VFinancePayCashCashCompany2, VFinancePayCashCashCompany2Collection>
    {        
        public VFinancePayCashCashCompany2Collection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the v_FinancePayCash_CashCompany2 view.
    /// </summary>
    [Serializable]
    public partial class VFinancePayCashCashCompany2 : ReadOnlyRecord<VFinancePayCashCashCompany2>, IReadOnlyRecord
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
                    BaseSchema = DB.GetSchema("v_FinancePayCash_CashCompany2");
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
	    public VFinancePayCashCashCompany2()
	    {
            //SetDefaults();
            //MarkNew(); 
            if(BaseSchema ==null) 
                BaseSchema = DB.GetSchema("v_FinancePayCash_CashCompany2");
        }
        public VFinancePayCashCashCompany2(bool useDatabaseDefaults)
	    { 
            if(BaseSchema == null)
                BaseSchema = DB.GetSchema("v_FinancePayCash_CashCompany2");
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			//MarkNew();
	    }
	    
	    public VFinancePayCashCashCompany2(object keyID)
	    { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_FinancePayCash_CashCompany2");
		    LoadByKey(keyID);
	    }
    	 
	    public VFinancePayCashCashCompany2(string columnName, object columnValue)
        { 
            if(BaseSchema == null) 
                BaseSchema = DB.GetSchema("v_FinancePayCash_CashCompany2");
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("Id")]
        [Bindable(true)]
        public int Id 
	    {
		    get
		    {
			    return GetColumnValue<int>("ID");
		    }
            set 
		    {
			    SetColumnValue("ID", value);
            }
        }
	      
        [XmlAttribute("CashOrderID")]
        [Bindable(true)]
        public int? CashOrderID 
	    {
		    get
		    {
			    return GetColumnValue<int?>("CashOrderID");
		    }
            set 
		    {
			    SetColumnValue("CashOrderID", value);
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
	      
        [XmlAttribute("CashSum")]
        [Bindable(true)]
        public int CashSum 
	    {
		    get
		    {
			    return GetColumnValue<int>("CashSum");
		    }
            set 
		    {
			    SetColumnValue("CashSum", value);
            }
        }
	      
        [XmlAttribute("OrderCode")]
        [Bindable(true)]
        public string OrderCode 
	    {
		    get
		    {
			    return GetColumnValue<string>("OrderCode");
		    }
            set 
		    {
			    SetColumnValue("OrderCode", value);
            }
        }
	      
        [XmlAttribute("OrderDate")]
        [Bindable(true)]
        public DateTime OrderDate 
	    {
		    get
		    {
			    return GetColumnValue<DateTime>("OrderDate");
		    }
            set 
		    {
			    SetColumnValue("OrderDate", value);
            }
        }
	      
        [XmlAttribute("OrderCard")]
        [Bindable(true)]
        public string OrderCard 
	    {
		    get
		    {
			    return GetColumnValue<string>("OrderCard");
		    }
            set 
		    {
			    SetColumnValue("OrderCard", value);
            }
        }
	      
        [XmlAttribute("OrderMemberID")]
        [Bindable(true)]
        public int OrderMemberID 
	    {
		    get
		    {
			    return GetColumnValue<int>("OrderMemberID");
		    }
            set 
		    {
			    SetColumnValue("OrderMemberID", value);
            }
        }
	      
        [XmlAttribute("ExpendCompanyID")]
        [Bindable(true)]
        public int ExpendCompanyID 
	    {
		    get
		    {
			    return GetColumnValue<int>("ExpendCompanyID");
		    }
            set 
		    {
			    SetColumnValue("ExpendCompanyID", value);
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
	      
        [XmlAttribute("OrderSumOk")]
        [Bindable(true)]
        public decimal? OrderSumOk 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("OrderSumOk");
		    }
            set 
		    {
			    SetColumnValue("OrderSumOk", value);
            }
        }
	      
        [XmlAttribute("OrderRateSale")]
        [Bindable(true)]
        public decimal? OrderRateSale 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("OrderRateSale");
		    }
            set 
		    {
			    SetColumnValue("OrderRateSale", value);
            }
        }
	      
        [XmlAttribute("OrderRate")]
        [Bindable(true)]
        public decimal? OrderRate 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("OrderRate");
		    }
            set 
		    {
			    SetColumnValue("OrderRate", value);
            }
        }
	      
        [XmlAttribute("OrderPoint")]
        [Bindable(true)]
        public decimal? OrderPoint 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("OrderPoint");
		    }
            set 
		    {
			    SetColumnValue("OrderPoint", value);
            }
        }
	      
        [XmlAttribute("OrderPay")]
        [Bindable(true)]
        public decimal? OrderPay 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("OrderPay");
		    }
            set 
		    {
			    SetColumnValue("OrderPay", value);
            }
        }
	      
        [XmlAttribute("OrderPayCash")]
        [Bindable(true)]
        public decimal? OrderPayCash 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("OrderPayCash");
		    }
            set 
		    {
			    SetColumnValue("OrderPayCash", value);
            }
        }
	      
        [XmlAttribute("OrderPayPoint")]
        [Bindable(true)]
        public decimal? OrderPayPoint 
	    {
		    get
		    {
			    return GetColumnValue<decimal?>("OrderPayPoint");
		    }
            set 
		    {
			    SetColumnValue("OrderPayPoint", value);
            }
        }
	      
        [XmlAttribute("OrderStatus")]
        [Bindable(true)]
        public int OrderStatus 
	    {
		    get
		    {
			    return GetColumnValue<int>("OrderStatus");
		    }
            set 
		    {
			    SetColumnValue("OrderStatus", value);
            }
        }
	      
        [XmlAttribute("OrderType")]
        [Bindable(true)]
        public int? OrderType 
	    {
		    get
		    {
			    return GetColumnValue<int?>("OrderType");
		    }
            set 
		    {
			    SetColumnValue("OrderType", value);
            }
        }
	      
        [XmlAttribute("OrderMemo")]
        [Bindable(true)]
        public string OrderMemo 
	    {
		    get
		    {
			    return GetColumnValue<string>("OrderMemo");
		    }
            set 
		    {
			    SetColumnValue("OrderMemo", value);
            }
        }
	    
	    #endregion 
        #region Typed Columns
        
        public static TableSchema.TableColumn
        IdColumn { get { return Schema.Columns[0]; } }
        
        public static TableSchema.TableColumn
        CashOrderIDColumn { get { return Schema.Columns[1]; } }
        
        public static TableSchema.TableColumn
        CashCompanyIDColumn { get { return Schema.Columns[2]; } }
        
        public static TableSchema.TableColumn
        CashSumColumn { get { return Schema.Columns[3]; } }
        
        public static TableSchema.TableColumn
        OrderCodeColumn { get { return Schema.Columns[4]; } }
        
        public static TableSchema.TableColumn
        OrderDateColumn { get { return Schema.Columns[5]; } }
        
        public static TableSchema.TableColumn
        OrderCardColumn { get { return Schema.Columns[6]; } }
        
        public static TableSchema.TableColumn
        OrderMemberIDColumn { get { return Schema.Columns[7]; } }
        
        public static TableSchema.TableColumn
        ExpendCompanyIDColumn { get { return Schema.Columns[8]; } }
        
        public static TableSchema.TableColumn
        OrderSumColumn { get { return Schema.Columns[9]; } }
        
        public static TableSchema.TableColumn
        OrderSumOkColumn { get { return Schema.Columns[10]; } }
        
        public static TableSchema.TableColumn
        OrderRateSaleColumn { get { return Schema.Columns[11]; } }
        
        public static TableSchema.TableColumn
        OrderRateColumn { get { return Schema.Columns[12]; } }
        
        public static TableSchema.TableColumn
        OrderPointColumn { get { return Schema.Columns[13]; } }
        
        public static TableSchema.TableColumn
        OrderPayColumn { get { return Schema.Columns[14]; } }
        
        public static TableSchema.TableColumn
        OrderPayCashColumn { get { return Schema.Columns[15]; } }
        
        public static TableSchema.TableColumn
        OrderPayPointColumn { get { return Schema.Columns[16]; } }
        
        public static TableSchema.TableColumn
        OrderStatusColumn { get { return Schema.Columns[17]; } }
        
        public static TableSchema.TableColumn
        OrderTypeColumn { get { return Schema.Columns[18]; } }
        
        public static TableSchema.TableColumn
        OrderMemoColumn { get { return Schema.Columns[19]; } }
        
        #endregion #region Columns Struct    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string Id = @"ID";
            
            public static string CashOrderID = @"CashOrderID";
            
            public static string CashCompanyID = @"CashCompanyID";
            
            public static string CashSum = @"CashSum";
            
            public static string OrderCode = @"OrderCode";
            
            public static string OrderDate = @"OrderDate";
            
            public static string OrderCard = @"OrderCard";
            
            public static string OrderMemberID = @"OrderMemberID";
            
            public static string ExpendCompanyID = @"ExpendCompanyID";
            
            public static string OrderSum = @"OrderSum";
            
            public static string OrderSumOk = @"OrderSumOk";
            
            public static string OrderRateSale = @"OrderRateSale";
            
            public static string OrderRate = @"OrderRate";
            
            public static string OrderPoint = @"OrderPoint";
            
            public static string OrderPay = @"OrderPay";
            
            public static string OrderPayCash = @"OrderPayCash";
            
            public static string OrderPayPoint = @"OrderPayPoint";
            
            public static string OrderStatus = @"OrderStatus";
            
            public static string OrderType = @"OrderType";
            
            public static string OrderMemo = @"OrderMemo";
            
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
