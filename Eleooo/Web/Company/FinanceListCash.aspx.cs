using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class FinanceListCash : ActionPage
    {
        Dictionary<string, string> _payInfo;
        Dictionary<string, string> PayInfo
        {
            get
            {
                if (_payInfo == null)
                {
                    _payInfo = new Dictionary<string, string>( );
                    _payInfo.Add("PAYSUM0", "本期间共计收了{0}元储值，使用了{1}元，结存{2}元<br/>");
                    _payInfo.Add("PAYSUM_OWNER0", "其中本店会员储值了{0}元，使用了{1}元，结存{2}元<br/>");
                    _payInfo.Add("PAYSUM_OUTER0", "其中外来会员储值了{0}元，使用了{1}元，结存{2}元 ");
                    
                    _payInfo.Add("PAYSUM1", "本期间共计收了{0}元储值<br/>");
                    _payInfo.Add("PAYSUM_OWNER1", "其中本店会员储值了{0}元，外来会员储值了{1}元<br/>");

                    _payInfo.Add("PAYSUM2", "本期间共计使用了{0}元储值<br/>");
                    _payInfo.Add("PAYSUM_OWNER2", "其中本店会员使用了{0}元，外来会员使用了{1}元<br/>");
                }
                return _payInfo;
            }
        }

        decimal dPaySum = 0, //总额
                dSum_Owner = 0, //充值
                dSum_Out = 0;  //消费
        decimal dPaySum_Owner = 0, //本店会员总额
                dInSum_Owner = 0,  //本店会员充值金额
                dOutSum_Owner = 0; //本店会员消费金额
        decimal dPaySum_Outer = 0, //外来会员总额
                dInSum_Outer = 0, //外来会员充值金额
                dOutSum_Outer = 0; //外来消费金额
        void PrintPayInfo( )
        {
            StringBuilder sb = new StringBuilder( );
            int flag = Utilities.ToInt(rblFlag.Text);
            if (flag == 1)
            {
                sb.AppendFormat(PayInfo["PAYSUM1"], dSum_Owner);
                sb.AppendFormat(PayInfo["PAYSUM_OWNER1"], dInSum_Owner, dInSum_Outer);
            }
            else if (flag == 2)
            {
                sb.AppendFormat(PayInfo["PAYSUM2"], dSum_Out);
                sb.AppendFormat(PayInfo["PAYSUM_OWNER2"], dOutSum_Owner, dOutSum_Outer);
            }
            else
            {
                sb.AppendFormat(PayInfo["PAYSUM0"], dSum_Owner, dSum_Out, dPaySum);
                sb.AppendFormat(PayInfo["PAYSUM_OWNER0"], dInSum_Owner, dOutSum_Owner, dPaySum_Owner);
                sb.AppendFormat(PayInfo["PAYSUM_OUTER0"], dInSum_Outer, dOutSum_Outer, dPaySum_Outer);
            }
            lblCashDesc.InnerHtml = sb.ToString( );
        }
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
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(PaymentCash.Schema),
                                  SysMember.Columns.MemberFullname,
                                  SysMember.Columns.MemberPhoneNumber,
                                  CompanyBLL.RenderIsOwner(PaymentCash.PaymentCashMemberIDColumn, PaymentCash.PaymentCashCompanyIDColumn)
                                  )
                             .From<PaymentCash>( )
                             .InnerJoin(SysMember.IdColumn, PaymentCash.PaymentCashMemberIDColumn)
                             .Where(PaymentCash.PaymentCashDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                             .And(PaymentCash.PaymentTypeColumn).IsNotEqualTo((int)PaymentCashType.Import)
                             .AndEx(SysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                             .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                             .CloseEx( )
                             .And(PaymentCash.PaymentCashCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                             .OrderDesc(Utilities.GetTableColumn(PaymentCash.IdColumn));
            if (rblFlag.Text != "0")
                query = query.And(PaymentCash.PaymentTypeColumn).IsEqualTo(rblFlag.Text);
            gridView.DataSource = query;
            gridView.AddShowColumn(PaymentCash.PaymentCashDateColumn)
                    .AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(PaymentCash.PaymentMemoColumn)
                    .AddShowColumn(PaymentCash.PaymentCashSumColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
            //lblCashDesc.InnerHtml = string.Concat(string.Format(PAYSUM, dSum_Owner, dSum_Out, dPaySum),
            //                                      string.Format(PAYSUM_OWNER, dInSum_Owner, dOutSum_Owner, dPaySum_Owner),
            //                                      string.Format(PAYSUM_OUTER, dInSum_Outer, dOutSum_Outer, dPaySum_Outer));
            PrintPayInfo( );
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "PaymentCashDate":
                    result = Utilities.ToDate(rowData[column]);
                    CalcPaySum(rowData);
                    break;
                case "PaymentMemo":
                    isRenderedCell = true;
                    string fullName = Utilities.ToString(rowData["MemberFullname"]);
                    string phoneNum = Utilities.ToString(rowData["MemberPhoneNumber"]);
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, CompanyBLL.ReplacePhoneToName(Utilities.ToHTML(rowData[column]), phoneNum, fullName));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        private void CalcPaySum(System.Data.DataRow rowData)
        {
            decimal d = Utilities.ToDecimal(rowData["PaymentCashSum"]);
            dPaySum += d;
            if (d > 0)
                dSum_Owner += d;
            else
                dSum_Out += -d;
            if (Utilities.ToInt(rowData[CompanyBLL.IS_OWNER]) == 1)
            {
                dPaySum_Owner += d;
                if (d > 0)
                    dInSum_Owner += d;
                else
                    dOutSum_Owner += -d;
            }
            else
            {
                dPaySum_Outer += d;
                if (d > 0)
                    dInSum_Outer += d;
                else
                    dOutSum_Outer += -d;
            }
        }
    }
}