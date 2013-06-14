using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class FinanceListCash : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string MEMBER_EDITOR_LINK = "/Admin/MemberEdit.aspx?ID={0}";
        const string COMPANY_EDITOR_LINK = "/Admin/CompanyEdit.aspx?ID={0}";

        private decimal dPutCash = 0;
        private decimal dOutCash = 0;
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
            var query = DB.Select(Utilities.GetTableColumns(PaymentCash.Schema), 
                                  "SYS_MEMBER.MemberFullname", 
                                  "SYS_COMPANY.CompanyName",
                                  Utilities.GetTableColumn(SysMember.MemberPhoneNumberColumn),
                                  Utilities.GetTableColumn(SysCompany.CompanyTelColumn))
                                         .From<PaymentCash>( )
                                         .InnerJoin(SysMember.IdColumn, PaymentCash.PaymentCashMemberIDColumn)
                                         .InnerJoin(SysCompany.IdColumn, PaymentCash.PaymentCashCompanyIDColumn)
                                         .Where(PaymentCash.PaymentCashDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                         .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                         .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                         .CloseEx( )
                                         .OrderDesc(Utilities.GetTableColumn(PaymentCash.IdColumn));
            if (rblFlag.Text != "0")
                query = query.And(PaymentCash.PaymentTypeColumn).IsEqualTo(rblFlag.Text);
            this.gridView.DataSource = query;
            this.gridView.AddShowColumn(PaymentCash.PaymentCashDateColumn)
                         .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                         .AddShowColumn(SysCompany.CompanyTelColumn)
                         .AddShowColumn(PaymentCash.PaymentCashSumColumn)
                         .AddShowColumn(PaymentCash.PaymentMemoColumn);
            this.gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBind);
            this.gridView.DataBind();
            this.lblCashDesc.InnerText = string.Format("本期间共计充了{0}元储值，消费了{1}元储值，结存{2}元储值", dPutCash, -dOutCash, dPutCash + dOutCash);
        }

        string gridView_OnDataBind(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            
            switch (column)
            {
                case "PaymentCashDate":
                    result = Convert.ToDateTime(rowData[column]).ToString("yyyy-MM-dd");
                    break;
                case "MemberPhoneNumber":
                    result = string.Format(LINK_TEMPLATE, string.Format(MEMBER_EDITOR_LINK, rowData[PaymentCash.PaymentCashMemberIDColumn.ColumnName]), rowData[column]);
                    break;
                case "CompanyTel":
                    result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, rowData[PaymentCash.PaymentCashCompanyIDColumn.ColumnName]), rowData[column]);
                    break;
                case "PaymentCashSum":
                    decimal dSum = Convert.ToDecimal(rowData[column]);
                    int iType = Utilities.ToInt(rowData[PaymentCash.PaymentTypeColumn.ColumnName]);
                    if (iType == 1)
                        dPutCash += dSum;
                    else if (iType == 2)
                        dOutCash += dSum;
                    result = dSum.ToString();
                    break;
                case "PaymentMemo":
                    isRenderedCell = true;
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, rowData[column]);
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}