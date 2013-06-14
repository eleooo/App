using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class FinanceListPoint1 : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string MEMBER_EDITOR_LINK = "/Admin/MemberEdit.aspx?ID={0}";
        const string COMPANY_EDITOR_LINK = "/Admin/CompanyEdit.aspx?ID={0}";

        private decimal dInPoint = 0;
        private decimal dOutPoint = 0;
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
            SubSonic.SqlQuery query = DB.Select(Utilities.GetTableColumns(Payment.Schema),
                                            "SYS_MEMBER.MemberFullname",
                                            "SYS_COMPANY.CompanyName",
                                            Utilities.GetTableColumn(SysMember.MemberPhoneNumberColumn),
                                            Utilities.GetTableColumn(SysCompany.CompanyTelColumn))
                                         .From<Payment>( )
                                         .InnerJoin(SysMember.IdColumn, Payment.PaymentMemberIDColumn)
                                         .LeftOuterJoin(SysCompany.IdColumn, Payment.PaymentCompanyIDColumn)
                                         .Where(Payment.PaymentDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                         .And(SysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                                         .And(SysMember.CompanyIdColumn).IsEqualTo(0)
                                         .OrderDesc(Utilities.GetTableColumn(Payment.IdColumn));
            if (rblFlag.Text != "0")
                query = query.And(Payment.PaymentTypeColumn).IsEqualTo(rblFlag.Text);
            this.gridView.DataSource = query;
            this.gridView.AddShowColumn(Payment.PaymentDateColumn)
                         .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                         .AddShowColumn(SysCompany.CompanyTelColumn)
                         .AddShowColumn(Payment.PaymentSumColumn)
                         .AddShowColumn(Payment.PaymentMemoColumn);
            this.gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBind);
            this.gridView.DataBind( );
            if (rblFlag.Text == "0")
                this.lblPointDesc.InnerText = string.Format("本期间共计获得了{0}个积分，消费了{1}个积分，结存{2}个积分", dInPoint, -dOutPoint, dInPoint + dOutPoint);
            else
                this.lblPointDesc.InnerText = string.Format("本期间共计获得了{0}个积分，消费了{1}个积分", dInPoint, -dOutPoint);
        }

        string gridView_OnDataBind(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "PaymentDate":
                    result = Convert.ToDateTime(rowData[column]).ToString("yyyy-MM-dd");
                    break;
                case "MemberPhoneNumber":
                    result = string.Format(LINK_TEMPLATE, string.Format(MEMBER_EDITOR_LINK, rowData[Payment.PaymentMemberIDColumn.ColumnName]), rowData[column]);
                    break;
                case "CompanyTel":
                    int cID = Utilities.ToInt(rowData[Payment.PaymentCompanyIDColumn.ColumnName]);
                    if (cID > 0)
                        result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, cID), rowData[column]);
                    break;
                case "PaymentSum":
                    decimal dSum = Convert.ToDecimal(rowData[column]);
                    int iType = Utilities.ToInt(rowData[Payment.PaymentTypeColumn.ColumnName]);
                    //<asp:ListItem Text="消费赠送" Value="1"></asp:ListItem>
                    //<asp:ListItem Text="充值赠送" Value="3"></asp:ListItem>
                    //<asp:ListItem Text="广告奖励" Value="7"></asp:ListItem>
                    //<asp:ListItem Text="推荐奖励" Value="9"></asp:ListItem>
                    //<asp:ListItem Text="消费抵扣" Value="2"></asp:ListItem>
                    if (iType == 1 || iType == 3 || iType == 7 || iType == 9)
                        dInPoint += dSum;
                    else if (iType == 2 || iType == 6)
                        dOutPoint += dSum;
                    result = dSum.ToString( );
                    break;
                case "PaymentMemo":
                    isRenderedCell = true;
                    string phoneNum = Utilities.ToString(rowData["MemberPhoneNumber"]);
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, CompanyBLL.ReplacePhoneToName(Utilities.ToString(rowData[column]), phoneNum, string.Empty));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}