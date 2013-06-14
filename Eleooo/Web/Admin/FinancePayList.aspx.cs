using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class FinancePayList : ActionPage
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
            gridView.DataSource = DB.Select(Utilities.GetTableColumns(PaymentRate.Schema),
                                            "Sys_Company.CompanyName",
                                            "Sys_Company.CompanyTel")
                                    .From<PaymentRate>()
                                    .InnerJoin(SysCompany.IdColumn, PaymentRate.PaymentRateCompanyIDColumn)
                                    .Where(PaymentRate.PaymentRateDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                    .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                    .CloseEx( )
                                    .OrderDesc(PaymentRate.Columns.PaymentRateDate);
            gridView.AddShowColumn(SysCompany.CompanyNameColumn)
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(PaymentRate.PaymentRateDateColumn)
                    .AddShowColumn(PaymentRate.PaymentRateDateStartColumn)
                    .AddShowColumn(PaymentRate.PaymentRateDateEndColumn)
                    .AddShowColumn(PaymentRate.PaymentRateCashColumn)
                    .AddShowColumn(PaymentRate.PaymentRateSaleColumn)
                    .AddShowColumn(PaymentRate.PaymentRateRateColumn)
                    .AddShowColumn(PaymentRate.PaymentRateSumColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind();
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "PaymentRateDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "PaymentRateDateStart":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "PaymentRateDateEnd":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "CompanyName":
                    result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, rowData[PaymentRate.Columns.PaymentRateID]), rowData[column]);
                    break;
                case "PaymentRateRate":
                    result = string.Format("{0}%", (Utilities.ToDecimal(rowData[column]) * 100M).ToString("###0.#####"));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}