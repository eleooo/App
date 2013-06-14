using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class FinancePayPoint : ActionPage
    {
        decimal dPointSum = 0;
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
            var query = DB.Select( ).From<Payment>( )
                          .Where(Payment.PaymentDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(Payment.PaymentCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .And(Payment.PaymentTypeColumn).IsEqualTo((int)PaymentType.SetMethod)
                          .OrderDesc(Utilities.GetTableColumn(Payment.IdColumn));
            gridView.DataSource = query;
            gridView.AddShowColumn(Payment.PaymentDateColumn)
                    .AddShowColumn(Payment.PaymentMemoColumn)
                    .AddShowColumn(Payment.PaymentSumColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
            lblPointDesc.InnerHtml = string.Format("本期间共计结算了{0}个积分", dPointSum);
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "PaymentDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "PaymentSum":
                    decimal dSum = -Utilities.ToDecimal(rowData[column]);
                    dPointSum += dSum;
                    result = dSum.ToString( );
                    break;
                case "PaymentMemo":
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, rowData[column]);
                    isRenderedCell = true;
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}