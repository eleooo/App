using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class FinanceListExportIn : ActionPage
    {
        const string CASHSUMINFO = "共计导入了{0}元储值 ";
        const string POINTSUMINFO = "共计导入了{0}个积分 ";
        decimal dCashSum = 0, dPointSum = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            if (rblFlag.Text == "2")
                OnQueryPayment( );
            else
                OnQueryPaymentCash( );
        }
        void OnQueryPaymentCash( )
        {
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(PaymentCash.Schema), 
                                 SysMember.Columns.MemberFullname, 
                                 SysMember.Columns.MemberPhoneNumber)
                 .From<PaymentCash>( )
                 .InnerJoin(SysMember.IdColumn, PaymentCash.PaymentCashMemberIDColumn)
                 .Where(PaymentCash.PaymentTypeColumn).IsEqualTo((int)PaymentCashType.Import)
                 .AndEx(SysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                 .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                 .CloseEx( )
                 .And(PaymentCash.PaymentCashCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId);
            //if (rblFlag.Text != "0")
            //    query = query.And(PaymentCash.PaymentTypeColumn).IsEqualTo(rblFlag.Text);
            gridView.DataSource = query;
            gridView.AddShowColumn(PaymentCash.PaymentCashDateColumn)
                    .AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(PaymentCash.PaymentMemoColumn)
                    .AddShowColumn(PaymentCash.PaymentCashSumColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
            lblDesc.InnerHtml = string.Format(CASHSUMINFO, dCashSum);
        }
        void OnQueryPayment( )
        {
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(Payment.Schema), 
                                        SysMember.Columns.MemberFullname, 
                                        SysMember.Columns.MemberPhoneNumber)
                                         .From<Payment>( )
                                         .InnerJoin(SysMember.IdColumn, Payment.PaymentMemberIDColumn)
                                         .Where(Payment.PaymentTypeColumn).IsEqualTo((int)PaymentType.Import)
                                         .AndEx(SysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                                         .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                                         .CloseEx( )
                                         .And(Payment.PaymentCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId);
            //if (rblFlag.Text != "0")
            //    query = query.And(Payment.PaymentTypeColumn).IsEqualTo(rblFlag.Text);
            this.gridView.DataSource = query;
            this.gridView.AddShowColumn(Payment.PaymentDateColumn)
                         .AddShowColumn(SysMember.MemberFullnameColumn)
                         .AddShowColumn(SysMember.MemberPhoneNumberColumn)                         
                         .AddShowColumn(Payment.PaymentMemoColumn)
                         .AddShowColumn(Payment.PaymentSumColumn);
            this.gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            this.gridView.DataBind( );
            lblDesc.InnerHtml = string.Format(POINTSUMINFO, dPointSum);
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "PaymentDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "PaymentCashDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "PaymentSum":
                    decimal d1 = Utilities.ToDecimal(rowData[column]);
                    dPointSum += d1;
                    result = d1.ToString("#####0.00");
                    break;
                case "PaymentCashSum":
                    decimal d2 = Utilities.ToDecimal(rowData[column]);
                    dCashSum += d2;
                    result = d2.ToString("#####0.00");
                    break;
                case "PaymentMemo":
                    isRenderedCell = true;
                    string fullName = Utilities.ToString(rowData["MemberFullname"]);
                    string phoneNum = Utilities.ToString(rowData["MemberPhoneNumber"]);
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, Utilities.ToHTML(rowData[column]).Replace(phoneNum, fullName));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}