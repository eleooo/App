using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class FinanceListCash : ActionPage
    {
        const string MOD_ROW_TEMPLATE = @"<tr class='os' align='middle'>
                                            {0}
                                          </tr>";
        const string SIG_ROW_TEMPLATE = @"<tr>
                                            {0}
                                          </tr>";
        decimal dSum = 0, dSum_in = 0, dSum_out = 0, dSum_im = 0;
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
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(PaymentCash.Schema), "SYS_COMPANY.CompanyName")
                             .From<PaymentCash>( )
                             .InnerJoin(SysCompany.IdColumn, PaymentCash.PaymentCashCompanyIDColumn)
                             .Where(PaymentCash.PaymentCashDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                             .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                             .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                             .CloseEx( )
                             .And(PaymentCash.PaymentCashMemberIDColumn).IsEqualTo(AppContext.Context.User.Id)
                             .OrderDesc(Utilities.GetTableColumn(PaymentCash.IdColumn));
            if (rblFlag.Text != "0")
                query = query.And(PaymentCash.PaymentTypeColumn).IsEqualTo(rblFlag.Text);
            gridView.DataSource = query;
            gridView.AddShowColumn(PaymentCash.PaymentCashDateColumn)
                    .AddShowColumn(PaymentCash.PaymentMemoColumn)
                    .AddShowColumn(PaymentCash.PaymentCashSumColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.OnDataBindHeader += new Web.Controls.DataBindHeaderHandle(gridView_OnDataBindHeader);
            gridView.OnDataBindFooter += new Web.Controls.DataBindFooterHandler(gridView_OnDataBindFooter);
            gridView.OnDataBindRow += new Web.Controls.DataBindRowHandler(gridView_OnDataBindRow);
            gridView.DataBind( );
        }

        string gridView_OnDataBindRow(int rowIndex, System.Data.DataRow rowData, ref bool isRenderedRow)
        {
            isRenderedRow = false;
            int mod;
            Math.DivRem(rowIndex + 1, 2, out mod);
            if (mod == 0)
                return MOD_ROW_TEMPLATE;
            else
                return SIG_ROW_TEMPLATE;
        }

        string gridView_OnDataBindFooter( )
        {
            return string.Format(footerTemplate.InnerHtml, gridView.ColumnCount, dSum_in, dSum_out, dSum_im, dSum);
        }
        void gridView_OnDataBindHeader(string column, ref string caption, ref bool isRenderedCell)
        {
            if (column == "PaymentCashSum")
                caption = "备注";
            else if (column == "PaymentMemo")
                caption = "储值信息";
        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "PaymentMemo":
                    CalcSum(rowData);
                    isRenderedCell = true;
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, Utilities.ToHTML(rowData[column]).Replace(AppContext.Context.User.MemberPhoneNumber, string.Empty));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        void CalcSum(System.Data.DataRow rowData)
        {
            int type = Utilities.ToInt(rowData[PaymentCash.Columns.PaymentType]);
            decimal _dSum = Utilities.ToDecimal(rowData[PaymentCash.Columns.PaymentCashSum]);
            dSum += _dSum;
            if (type == 1)
                dSum_in += _dSum;
            else if (type == 2)
                dSum_out += Math.Abs(_dSum);
            else
                dSum_im += Math.Abs(_dSum);
        }
    }
}