using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class FinanceListPoint : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string MEMBER_EDITOR_LINK = "/Admin/MemberEdit.aspx?ID={0}";
        const string COMPANY_EDITOR_LINK = "/Admin/CompanyEdit.aspx?ID={0}";

        private decimal dInPoint = 0;
        private decimal dOutPoint = 0;
        private decimal dSetPoint = 0;
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
            SubSonic.SqlQuery query = DB.Select(Utilities.GetTableColumns(Payment.Schema), 
                                            "SYS_MEMBER.MemberFullname", 
                                            "SYS_COMPANY.CompanyName",
                                            Utilities.GetTableColumn(SysMember.MemberPhoneNumberColumn),
                                            Utilities.GetTableColumn(SysCompany.CompanyTelColumn))
                                         .From<Payment>( )
                                         .InnerJoin(SysMember.IdColumn, Payment.PaymentMemberIDColumn)
                                         .InnerJoin(SysCompany.IdColumn, Payment.PaymentCompanyIDColumn)
                                         .Where(Payment.PaymentDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                         .And(Payment.PaymentTypeColumn).IsNotEqualTo((int)PaymentType.CompanyItem)
                                         .And(Payment.PaymentTypeColumn).IsNotEqualTo((int)PaymentType.Import)
                                         .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                         .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                         .CloseEx( )
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
            this.gridView.DataBind();
            this.lblPointDesc.InnerText = string.Format("本期间共计获得了{0}个积分，消费了{1}个积分，结算了{3}个积分，结存{2}个积分", dInPoint, -dOutPoint, dInPoint + dOutPoint + dSetPoint,Math.Abs(dSetPoint));
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
                    result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, rowData[Payment.PaymentCompanyIDColumn.ColumnName]), rowData[column]);
                    break;
                case "PaymentSum":
                    decimal dSum = Convert.ToDecimal(rowData[column]);
                    int iType = Utilities.ToInt(rowData[Payment.PaymentTypeColumn.ColumnName]);
                        //<asp:ListItem Text="消费赠送" Value="1"></asp:ListItem>
                        //<asp:ListItem Text="充值赠送" Value="3"></asp:ListItem>
                        //<asp:ListItem Text="广告奖励" Value="7"></asp:ListItem>
                        //<asp:ListItem Text="推荐奖励" Value="9"></asp:ListItem>
                        //<asp:ListItem Text="消费抵扣" Value="2"></asp:ListItem>
                        //<asp:ListItem Text="系统结算" Value="5"></asp:ListItem>
                    if (iType == 1 || iType == 3 || iType == 7 || iType == 9)
                        dInPoint += dSum;
                    else if (iType == 2)
                        dOutPoint += dSum;
                    else if (iType == 5)
                        dSetPoint += dSum;
                    result = dSum.ToString();
                    break;
                case "PaymentMemo":
                    isRenderedCell = true;
                    string phoneNum = Utilities.ToString(rowData["MemberPhoneNumber"]);
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, CompanyBLL.ReplacePhoneToName(Utilities.ToString(rowData[column]), phoneNum, string.Empty));
                    //result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, rowData[column]);
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}