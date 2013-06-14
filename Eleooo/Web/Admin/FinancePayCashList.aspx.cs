using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class FinancePayCashList : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string COMPANY_EDITOR_LINK = "/Admin/CompanyEdit.aspx?ID={0}";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim(), "%");
            gridView.DataSource = DB.Select(Utilities.GetTableColumns(PaymentRateCash.Schema),
                                            SysCompany.Columns.CompanyName,
                                            SysCompany.Columns.CompanyTel)
                                    .From<PaymentRateCash>()
                                    .InnerJoin(SysCompany.IdColumn, PaymentRateCash.PaymentRateCashCompanyIDColumn)
                                    .Where(PaymentRateCash.PaymentRateCashDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .And(SysCompany.CompanyTypeColumn).IsEqualTo(CompanyType.MealCompany)
                                    .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                    .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                    .CloseEx( )
                                    .OrderDesc(PaymentRateCash.Columns.PaymentRateCashID);
            gridView.AddShowColumn(PaymentRateCash.PaymentRateCashDateColumn)
                    .AddShowColumn(PaymentRateCash.PaymentRateCashDateStartColumn)
                    .AddShowColumn(PaymentRateCash.PaymentRateCashDateEndColumn)
                    .AddShowColumn(PaymentRateCash.PaymentRateCash1Column)
                    .AddShowColumn(PaymentRateCash.PaymentRateCash2Column)
                    .AddShowColumn(SysCompany.CompanyNameColumn)
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(PaymentRateCash.PaymentRateCashSumColumn)
                    .AddShowColumn(PaymentRateCash.PaymentRateCashMemoColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind();
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            
            switch (column)
            {
                case "PaymentRateCashDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "PaymentRateCashDateEnd":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "PaymentRateCashDateStart":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "CompanyName":
                    result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, rowData[PaymentRateCash.Columns.PaymentRateCashCompanyID]), rowData[column]);
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}