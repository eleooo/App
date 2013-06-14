using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class FinanceList : ActionPage
    {
        const string FLAG_ALL = "本期间共计收入了{0}个积分，支出了{1}个积分";
        const string FLAG_1 = "本期间共计收入了{0}个积分";
        const string FLAG_2 = "本期间共计支出了{0}个积分";
        decimal dAll_In = 0, dAll_Out = 0;

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
            //string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            var query = DB.Select( ).From<Payment>( )
                          .LeftOuterJoin(SysCompany.IdColumn, Payment.PaymentCompanyIDColumn)
                          .Where(Payment.PaymentDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(Payment.PaymentMemberIDColumn).IsEqualTo(AppContext.Context.User.Id)
                          .OrderDesc(Utilities.GetTableColumn(Payment.IdColumn));
            //if (!string.IsNullOrEmpty(txtCompanyName.Value))
            //    query.AndEx(SysCompany.CompanyTelColumn).Like(filterCompanyTel)
            //         .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
            //         .CloseEx( );
            if (rblFlag.Text == "1")
                query.And(Payment.PaymentSumColumn).IsGreaterThan(0);
            else if (rblFlag.Text == "2")
                query.And(Payment.PaymentSumColumn).IsLessThan(0);
            view.QuerySource = query;
            view.ItemCreated += new RepeaterItemEventHandler(view_ItemCreated);
            view.DataBind( );
        }

        void view_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                var row = (e.Item.DataItem as System.Data.DataRowView).Row;
                CalcPayment(row);
            }
            else if (e.Item.ItemType == ListItemType.Footer)
                e.Item.DataItem = GetPaySumMessage( );
        }

        private void CalcPayment(System.Data.DataRow rowData)
        {
            //int flag = Convert.ToInt32(rowData[Payment.Columns.PaymentType]);
            //PaymentType pType = PaymentBLL.GetPaymentType(flag);
            decimal paySum = Convert.ToDecimal(rowData[Payment.Columns.PaymentSum]);
            if (paySum > 0)
                dAll_In += paySum;
            else
                dAll_Out += Math.Abs(paySum);
            //<asp:ListItem Selected="True" Text="全部" Value="0"></asp:ListItem>
            //<asp:ListItem Text="消费赠送" Value="1"></asp:ListItem>
            //<asp:ListItem Text="充值赠送" Value="3"></asp:ListItem>
            //<asp:ListItem Text="广告奖励" Value="7"></asp:ListItem>
            //<asp:ListItem Text="推荐奖励" Value="9"></asp:ListItem>
            //<asp:ListItem Text="积分转账" Value="4"></asp:ListItem>
            //<asp:ListItem Text="消费抵扣" Value="2"></asp:ListItem>
            //switch (pType)
            //{
            //    case PaymentType.ConsumeGive:
            //        dFlag_1 += Math.Abs(paySum);
            //        break;
            //    case PaymentType.Mortgage:
            //        dFlag_2 += Math.Abs(paySum);
            //        break;
            //    case PaymentType.PrepaidGive:
            //        dFlag_3 += Math.Abs(paySum);
            //        break;
            //    case PaymentType.Move:
            //        if (paySum > 0)
            //            dFlag_4_in += paySum;
            //        else
            //            dFlag_4_out += Math.Abs(paySum);
            //        //rowData[Payment.Columns.PaymentSum] = -paySum;
            //        break;
            //    //case PaymentType.SetMethod:
            //    //    dPayFlag5 += paySum;
            //    //    break;
            //    case PaymentType.CompanyItem:
            //        dFlag_2 += Math.Abs(paySum);
            //        break;
            //    case PaymentType.AdvsGive:
            //        dFlag_7 += Math.Abs(paySum);
            //        break;
            //    case PaymentType.Reward:
            //        dFlag_9 += Math.Abs(paySum);
            //        break;
            //    //case PaymentType.Import:
            //    //    dPayFlag1_3 += paySum;
            //    //    break;
            //}
        }
        private string GetPaySumMessage( )
        {
            switch (rblFlag.Text)
            {
                case "1":
                    return string.Format(FLAG_1, dAll_In);
                case "2":
                    return string.Format(FLAG_2, dAll_Out);
                default:
                    return string.Format(FLAG_ALL, dAll_In, dAll_Out);
            }
        }

    }
}