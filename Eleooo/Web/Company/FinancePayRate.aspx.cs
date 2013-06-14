using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class FinancePayRate : ActionPage
    {
        decimal dPayRateSum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd" );
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            var query = DB.Select( ).From<PaymentRate>( )
                          .Where(PaymentRate.PaymentRateDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(PaymentRate.PaymentRateCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .OrderDesc(Utilities.GetTableColumn(PaymentRate.PaymentRateIDColumn));
            gridView.DataSource = query;
            gridView.AddShowColumn(PaymentRate.PaymentRateDateColumn)
                    .AddShowColumn(PaymentRate.PaymentRateDateStartColumn)
                    .AddShowColumn(PaymentRate.PaymentRateDateEndColumn)
                    .AddShowColumn(PaymentRate.PaymentRateSaleColumn)
                    .AddShowColumn(PaymentRate.PaymentRateCashColumn)
                    .AddShowColumn(PaymentRate.PaymentRateRateColumn)
                    .AddShowColumn(PaymentRate.PaymentRateSumColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
            lblPointDesc.InnerHtml = string.Format("本期间已结算了{0}元佣金 ", dPayRateSum);
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "PaymentRateDate":
                case "PaymentRateDateStart":
                case "PaymentRateDateEnd":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "PaymentRateSum":
                    decimal d = Utilities.ToDecimal(rowData[column]);
                    dPayRateSum += d;
                    result = d.ToString("#####0.00");
                    break;
                case "PaymentRateRate":
                    result = string.Format("{0}%", (Utilities.ToDecimal(rowData[column]) * 100M).ToString("###0.#####"));
                    break;
                default:
                    result = Utilities.ToString(rowData[column]);
                    break;
            }
            return result;
        }
    }
}