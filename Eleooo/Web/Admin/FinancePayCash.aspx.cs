using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class FinancePayCash : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string COMPANY_EDITOR_LINK = "/Admin/CompanyEdit.aspx?ID={0}";
        const string COMPANY_PAYCASH_INPUT = "<input name=\"CompanyPayCash_{0}\" value='{1}' type=\"text\" id=\"CompanyPayCash_{0}\" />";
        const string COMPANY_PAYCASH_ACTION = "<input type=\"button\" name=\"CompanyPayCash_Action_{0}\" value=\"{1}\" id=\"CompanyPayCash_Action_{0}\" onclick=\"__doPostBack('Edit','{2}');\" />";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtInfo.InnerHtml = string.Empty;
            if (!IsPostBack)
                this.txtPayDate.Value = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
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
            string sPayInput = Params[string.Format("CompanyPayCash_{0}", companyID)];
            if (string.IsNullOrEmpty(argment))
            {
                txtInfo.InnerText = "结算参数错误,请重试!";
                goto lable_end;
            }

            SysCompany company = SysCompany.FetchByID(companyID);
            if (company == null)
            {
                txtInfo.InnerText = "结算参数错误 company = null,请重试!";
                goto lable_end;
            }
            if (string.IsNullOrEmpty(sPayInput))
                goto lable_end;
            decimal dPay = Convert.ToDecimal(sPayInput);
            if (dPay == 0)
                goto lable_end;
            DateTime dtPayDate = Convert.ToDateTime(txtPayDate.Value);
            if (dtPayDate >= DateTime.Today)
            {
                txtInfo.InnerText = "不能结算当天之后的日期!";
                goto lable_end;
            }
            dtPayDate = dtPayDate.AddDays(1);
            decimal dCompanyCash1 = Convert.ToDecimal(argArr[1]);
            decimal dCompanyCash2 = Convert.ToDecimal(argArr[2]);
            DateTime dtBegin = Utilities.ToDateTime(argArr[3]);
            string payMemo = string.Empty;
            if (dPay < 0)
                payMemo = ResBLL.GetRes("FinancePayCash_PayInMemo", "乐多分向{0}收取了{1}元;", "商家储值结算收取金额描述信息");
            else
                payMemo = ResBLL.GetRes("FinancePayCash_PayOutMemo", "乐多分向{0}支付了{1}元;", "商家储值结算支出金额描述信息");
            payMemo = string.Format(payMemo,company.CompanyName,Math.Abs(dPay));
            new PaymentRateCash
            {
                PaymentRateCashDate = DateTime.Now,
                PaymentRateCash1 = dCompanyCash1,
                PaymentRateCash2 = dCompanyCash2,
                PaymentRateCashDateStart = dtBegin.Date,
                PaymentRateCashDateEnd = dtPayDate.Date,
                PaymentRateCashCompanyID = companyID,
                PaymentRateCashStatus = 1,
                PaymentRateCashSum = dPay,
                PaymentRateCashMemo = payMemo
            }.Save();
            txtInfo.InnerText = "结算成功:" + payMemo;
        lable_end:
            this.On_ActionQuery(sender, e);
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim(), "%");
            var query = GetPayCashQuery( );
            gridView.DataSource = query.AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                    .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                    .CloseEx( );
            gridView.AddShowColumn("Sys_Company.CompanyName", "Sys_Company.CompanyTel", "v_FinancePayCash_LastDate.PaymentRateCashDate")
                    .AddCustomColumn("CompanyCash1", ResBLL.GetRes("FinancePayCash_CompanyCash1", "本店储值<br />外店消费", "商家储值结算本店储值外店消费名称"))
                    .AddCustomColumn("CompanyCash2", ResBLL.GetRes("FinancePayCash_CompanyCash2", "外店储值<br />本店消费", "商家储值结算外店储值本店消费名称"))
                    .AddCustomColumn("FinancePayCash_CompanyPay", ResBLL.GetRes("FinancePayCash_CompanyPay", "本次结算", "商家储值结算列名称"))
                    .AddCustomColumn("FinancePayCash_PayAction", ResBLL.GetRes("FinancePayCash_PayAction", "积分结算", "商家储值结算操作列名称"));
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind();
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            
            switch (column)
            {
                case "CompanyName":
                    result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, rowData["CompanyID"]), rowData[column]);
                    break;
                case "PaymentRateCashDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "FinancePayCash_CompanyPay":
                    decimal dPay = Convert.ToDecimal(rowData["CompanyCash2"]) -
                                   Convert.ToDecimal(rowData["CompanyCash1"]);
                    result = string.Format(COMPANY_PAYCASH_INPUT, rowData["CompanyID"], dPay);
                    break;
                case "FinancePayCash_PayAction":
                    List<string> argList = new List<string>();
                    string id = Utilities.ToHTML(rowData["CompanyID"]);
                    argList.Add(id);
                    argList.Add(Utilities.ToHTML(rowData["CompanyCash1"]));
                    argList.Add(Utilities.ToHTML(rowData["CompanyCash2"]));
                    argList.Add(Utilities.ToDate(rowData["PaymentRateCashDate"]));
                    result = string.Format(COMPANY_PAYCASH_ACTION, id, ResBLL.GetRes("FinancePayCash_PayAction", "储值结算", "商家储值结算操作列名称"), Utilities.ToBase64String(string.Join("|", argList.ToArray())));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        SubSonic.SqlQuery GetPayCashQuery()
        {
            DateTime dtPayDate = string.IsNullOrEmpty(txtPayDate.Value)?DateTime.Today:Utilities.ToDateTime(txtPayDate.Value).AddDays(1);
            string joinOrderDate1 = string.Format("AND (v_FinancePayCash_CashCompany1.OrderDate < '{0}')", dtPayDate.ToString("yyyy-MM-dd"));
            string joinOrderDate2 = string.Format("AND (v_FinancePayCash_CashCompany2.OrderDate < '{0}')", dtPayDate.ToString("yyyy-MM-dd"));
            return DB.Select("Sys_Company.ID AS CompanyID",
                             "Sys_Company.CompanyCode",
                             "Sys_Company.CompanyName",
                             "Sys_Company.CompanyTel",
                             "Sys_Company.CompanyType",
                             "v_FinancePayCash_LastDate.PaymentRateCashDate",
                             "sum(ISNULL(v_FinancePayCash_CashCompany1.OrderPayCash, 0)) as CompanyCash1",
                             "sum(ISNULL(v_FinancePayCash_CashCompany2.OrderPayCash, 0)) as CompanyCash2")
                    .From<SysCompany>()
                    .LeftOuterJoin(VFinancePayCashCashCompany1.Schema.GetColumn("CashCompanyID"), SysCompany.IdColumn, joinOrderDate1)
                    .LeftOuterJoin(VFinancePayCashCashCompany2.Schema.GetColumn("ExpendCompanyID"), SysCompany.IdColumn, joinOrderDate2)
                    .LeftOuterJoin(VFinancePayCashLastDate.Schema.GetColumn("PaymentRateCashCompanyID"), SysCompany.IdColumn)
                    //.Where(SysCompany.CompanyTypeColumn).IsEqualTo(CompanyType.HotCompany)
                    .ConstraintExpression("GROUP BY Sys_Company.ID,CompanyCode,CompanyName,CompanyTel,CompanyType,PaymentRateCashDate");
                    //.OrderDesc("CompanyCash1","CompanyCash2");
        }
    }
}