using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using SubSonic;
using System.Transactions;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class FinancePay : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string COMPANY_EDITOR_LINK = "/Admin/CompanyEdit.aspx?ID={0}";
        const string COMPANY_PAYRATE_INPUT = "<input name=\"CompanyPayRate_{0}\" value='{1}' type=\"text\" id=\"CompanyPayRate_{0}\" />";
        const string COMPANY_PAYRATE_ACTION = "<input type=\"button\" name=\"CompanyPayRate_Action_{0}\" value=\"{1}\" id=\"CompanyPayRate_Action_{0}\" onclick=\"__doPostBack('Edit','{2}');\" />";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtInfo.InnerHtml = string.Empty;
            if (!IsPostBack)
                this.txtPayDate.Value = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            #region query sql define
            //SELECT 
            //Sys_Company.ID,
            //Sys_Company.CompanyCode,
            //Sys_Company.CompanyName,
            //Sys_Company.CompanyTel,
            //Sys_Company.CompanyRateMaster * 100 AS CompanyRateMaster,
            //v_FinancePay_LastDate.PaymentRateDate,
            //SUM(CASE Sys_Company.CompanyType WHEN 1 THEN ISNULL(v_FinancePay_MemberCash.CashSum,0) ELSE 0 END  ) AS MemberCashSum,
            //SUM(CASE Sys_Company.CompanyType WHEN 1 THEN ISNULL(v_FinancePay_MemberOrders.OrderPay,0) ELSE ISNULL(v_FinancePay_MemberOrders.OrderPay,0) + ISNULL(v_FinancePay_MemberOrders.OrderPayCash,0) END) AS MemberPaySum
            //from Sys_Company
            //LEFT OUTER JOIN v_FinancePay_LastDate ON .v_FinancePay_LastDate.PaymentRateCompanyID = Sys_Company.ID
            //LEFT OUTER JOIN v_FinancePay_MemberCash ON v_FinancePay_MemberCash.CashCompanyID = Sys_Company.ID 
            //                                       AND v_FinancePay_MemberCash.CashDate > ISNULL(v_FinancePay_LastDate.PaymentRateDate,'1/1/1753')
            //LEFT OUTER JOIN v_FinancePay_MemberOrders ON v_FinancePay_MemberOrders.OrderSellerID = Sys_Company.ID
            //                                       AND v_FinancePay_MemberOrders.OrderDate > ISNULL(v_FinancePay_LastDate.PaymentRateDate,'1/1/1753')  
            //GROUP BY
            //Sys_Company.ID,
            //Sys_Company.CompanyCode,
            //Sys_Company.CompanyName,
            //Sys_Company.CompanyTel,
            //Sys_Company.CompanyRateMaster,
            //v_FinancePay_LastDate.PaymentRateDate 
            #endregion
            #region build sql query
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            DateTime dtPayDate = string.IsNullOrEmpty(txtPayDate.Value) ? DateTime.Today : Utilities.ToDateTime(txtPayDate.Value).AddDays(1);
//            string memberCashFilter = string.Concat(@"AND (v_FinancePay_MemberCash.CashDate > ISNULL(v_FinancePay_LastDate.PaymentRateDate,'1/1/1753'))
//                                                      AND (v_FinancePay_MemberCash.CashOrderID=0)",
//                                                    string.Format(" AND(v_FinancePay_MemberCash.CashDate < '{0}')", dtPayDate.ToString("yyyy-MM-dd")));
            string memberOrderFilter = string.Concat("AND (v_FinancePay_MemberOrders.OrderDate > ISNULL(v_FinancePay_LastDate.PaymentRateDate,'1/1/1753'))",
                                                    string.Format(" AND(v_FinancePay_MemberOrders.OrderDate < '{0}')", dtPayDate.ToString("yyyy-MM-dd")));
            gridView.DataSource = DB.Select("Sys_Company.ID",
                                            "Sys_Company.CompanyCode",
                                            "Sys_Company.CompanyName",
                                            "Sys_Company.CompanyTel",
                                            "Sys_Company.CompanyRateMaster AS CompanyRateMaster",
                                            "v_FinancePay_LastDate.PaymentRateDate",
                                            "SUM(ISNULL(v_FinancePay_MemberOrders.OrderPayCash,0)) AS MemberCashSum",
                                            "SUM(ISNULL(v_FinancePay_MemberOrders.OrderPay,0)) AS MemberPaySum")
                                            .From<SysCompany>( )
                                            .LeftOuterJoin(VFinancePayLastDate.PaymentRateCompanyIDColumn, SysCompany.IdColumn)
                                            .LeftOuterJoin(VFinancePayMemberOrder.OrderSellerIDColumn, SysCompany.IdColumn, memberOrderFilter)
                                            .Where(SysCompany.CompanyTelColumn).Like(filterCompanyTel)
                                            .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                            .ConstraintExpression(@"GROUP BY
                                                                Sys_Company.ID,
                                                                Sys_Company.CompanyCode,
                                                                Sys_Company.CompanyName,
                                                                Sys_Company.CompanyTel,
                                                                Sys_Company.CompanyRateMaster,
                                                                v_FinancePay_LastDate.PaymentRateDate");
            //.OrderDesc("MemberCashSum", "MemberPaySum");
            #endregion
            #region show column settings
            gridView.AddShowColumn(SysCompany.CompanyNameColumn, SysCompany.CompanyTelColumn)
                    .AddShowColumn(VFinancePayLastDate.Schema.GetColumn("PaymentRateDate"))
                    .AddCustomColumn("MemberPaySum", ResBLL.GetRes("FinancePay_MemberPaySum", "销售金额", "佣金结算销售金额列名称"))
                    .AddCustomColumn("MemberCashSum", ResBLL.GetRes("FinancePay_MemberCashSum", "充值金额", "佣金结算充值金额列名称"))
                    .AddShowColumn(SysCompany.CompanyRateMasterColumn)
                    .AddCustomColumn("FinancePayRate_PayAction", ResBLL.GetRes("FinancePayRate_PayAction", "佣金结算", "商家佣金结算操作列名称"));
            #endregion
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "CompanyName":
                    result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, rowData["ID"]), rowData[column]);
                    break;
                case "PaymentRateDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "CompanyRateMaster":
                    result = string.Format(COMPANY_PAYRATE_INPUT, rowData["ID"], rowData[column]);
                    break;
                case "FinancePayRate_PayAction":
                    List<string> argList = new List<string>( );
                    string id = Utilities.ToHTML(rowData["ID"]);
                    argList.Add(id);
                    argList.Add(Utilities.ToHTML(rowData["MemberCashSum"]));
                    argList.Add(Utilities.ToHTML(rowData["MemberPaySum"]));
                    argList.Add(Utilities.ToDate(rowData["PaymentRateDate"]));
                    result = string.Format(COMPANY_PAYRATE_ACTION, id, ResBLL.GetRes("FinancePayRate_PayAction", "佣金结算", "商家佣金结算操作列名称"),
                             Utilities.ToBase64String(string.Join("|", argList.ToArray( ))));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        DateTime GetMinOrderDate(int companyID)
        {
            string vSql = string.Format("select min(OrderDate) from orders where OrderSellerID={0} group by OrderSellerID;", companyID);
            QueryCommand cmd = new QueryCommand(vSql);
            object v= DataService.ExecuteScalar(cmd);
            if (Utilities.IsNull(v))
                return DateTime.MinValue.Date;
            else
                return Convert.ToDateTime(v).Date;
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            string argment = Utilities.FromBase64String(EVENTARGUMENT);
            string[] argArr = argment.Split(new char[] { '|' });
            int companyID = Utilities.ToInt(argArr[0]);
            if (companyID <= 0)
            {
                txtInfo.InnerText = "结算参数错误 companyID = 0,请重试!";
                goto lable_end;
            }
            SysCompany company = SysCompany.FetchByID(companyID);
            if (company == null)
            {
                txtInfo.InnerText = "结算参数错误 company = null,请重试!";
                goto lable_end;
            }
            string sPayRateInput = Params[string.Format("CompanyPayRate_{0}", companyID)];
            if (string.IsNullOrEmpty(sPayRateInput))
            {
                txtInfo.InnerText = "结算金额不能为零或佣金比例不能小于等于零!";
                goto lable_end;
            }
            decimal dPayRate = Convert.ToDecimal(sPayRateInput);
            if (dPayRate == 0)
            {
                txtInfo.InnerText = "结算金额不能为零或佣金比例不能小于等于零!";
                goto lable_end;
            }
            DateTime dtPayDate = Convert.ToDateTime(txtPayDate.Value);
            if (dtPayDate >= DateTime.Today)
            {
                txtInfo.InnerText = "不能结算当天之后的日期!";
                goto lable_end;
            }
            //dtPayDate = dtPayDate.AddDays(1);
            decimal dMemberCashSum = Convert.ToDecimal(argArr[1]);
            decimal dMemberPaySum = Convert.ToDecimal(argArr[2]);
            DateTime dt = GetMinOrderDate(company.Id);
            DateTime dtBegin = Utilities.ToDateTime(argArr[3]);
            if (dtBegin < dt)
                dtBegin = dt;
            decimal dSum = dMemberCashSum + dMemberPaySum;
            if (dPayRate <= 0 || dSum <= 0)
            {
                txtInfo.InnerText = "结算金额不能为零或佣金比例不能小于等于零!";
                goto lable_end;
            }
            decimal dRate = dPayRate / 100M;
            decimal dPaySum = dSum * dRate;
            string payMemo = string.Format(ResBLL.GetRes("FinancePayRate_PayMemo", "乐多分向{0}收取了佣金{1}元;", "商家佣金结算描述信息"), company.CompanyName, dPaySum);
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                new PaymentRate
                {
                    PaymentRateCompanyID = company.Id,
                    PaymentRateDate = DateTime.Now,
                    PaymentRateDateStart = dtBegin,
                    PaymentRateDateEnd = dtPayDate,
                    PaymentRateMemo = payMemo,
                    PaymentRateRate = dRate, //佣金比例
                    PaymentRateStatus = 1,
                    PaymentRateCash = dMemberCashSum, //储值金额
                    PaymentRateSale = dMemberPaySum,  //销售金额
                    PaymentRateSum = dPaySum          //佣金金额
                }.Save( );
                OrderBLL.UpdateBalance( );
                ts.Complete( );
            }
            catch (Exception ex)
            {
                txtInfo.InnerHtml = ex.Message;
                Logging.Log("FinancePay->On_ActionEdit", ex, true);
                goto lable_end;
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
            txtInfo.InnerText = payMemo;
        lable_end:
            this.On_ActionQuery(sender, e);
        }
    }
}