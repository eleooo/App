using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class FinanceListPoint : ActionPage
    {
        const string POINTSUM_ITEM = "本期间共计抢优惠消费了{0}个积分</br>其中本店会员抢优惠消费了{1}个积分</br/>其中外来会员抢优惠消费了{2}个积分";
        const string POINTSUM_ADS = "本期间共计浏览广告赠送了{0}个积分</br>其中本店会员赠送了{1}个积分</br/>其中外来会员赠送了{2}个积分";

        const string POINTSUM_ALl = "本期间共计送了{0}个积分，收了{1}个积分<br/>";
        const string POINTSUM_1 = "本期间共计消费赠送了{0}个积分<br/>";
        const string POINTSUM_2 = "本期间共计消费抵扣了{0}个积分<br/>";
        const string POINTSUM_3 = "本期间共计赠送了{0}个积分<br/>";
        const string POINTSUM_4 = "本期间共计结算了{0}个积分<br/>";
        const string POINTSUM_OWNER = "其中本店会员送了{0}个积分，收了{1}个积分<br/>";
        const string POINTSUM_OUTER = "其中外来会员送了{0}个积分，收了{1}个积分 ";

        const string POINTSUM_OWNER_2 = "其中本店会员消费抵扣了{0}个积分</br>";
        const string POINTSUM_OUTER_2 = "其中外来会员消费抵扣了{0}个积分";

        const string POINTSUM_OWNER_3 = "其中本店会员送了{0}个积分</br>";
        const string POINTSUM_OUTER_3 = "其中外来会员送了{0}个积分";

        const string ORIG_MEMBERPHONENUMBER = "Orig_MemberPhoneNumber";

        decimal dItemSum = 0, dItemSum_Owner = 0, dItemSum_Outer = 0;
        decimal dAdsSum = 0, dAdsSum_Owner = 0, dAdsSum_Outer = 0;
        decimal dPointSum = 0, dInPointSum = 0, dOutPointSum_1 = 0, dOutPointSum_3 = 0, dPointSum_Set = 0;
        decimal dInPointSum_Owner = 0, dOutPointSum_Owner = 0;
        decimal dInPointSum_Outer = 0, dOutPointSum_Outer = 0;
        int iCompanyID = AppContext.Context.Company.Id;

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
                                                SysMember.Columns.MemberFullname,
                                                string.Format("{0} as {1}", SysMember.Columns.MemberPhoneNumber, ORIG_MEMBERPHONENUMBER),
                                                CompanyBLL.RenderUserPhone(Payment.PaymentMemberIDColumn, Payment.PaymentCompanyIDColumn, SysMember.MemberPhoneNumberColumn, SysMember.MemberCompanyIDColumn),
                                                CompanyBLL.RenderIsOwner(Payment.PaymentMemberIDColumn, Payment.PaymentCompanyIDColumn))
                                         .From<Payment>( )
                                         .InnerJoin(SysMember.IdColumn, Payment.PaymentMemberIDColumn)
                                         .Where(Payment.PaymentDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                         .And(Payment.PaymentStatusColumn).IsEqualTo(1)
                                         .And(Payment.PaymentTypeColumn).IsNotEqualTo((int)PaymentType.Import)
                                         .AndEx(SysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                                         .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                                         .CloseEx( )
                                         .And(Payment.PaymentCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                                         .OrderDesc(Utilities.GetTableColumn(Payment.IdColumn));
            if (rblFlag.Text != "0")
                query = query.And(Payment.PaymentTypeColumn).IsEqualTo(rblFlag.Text);
            this.gridView.DataSource = query;
            this.gridView.AddShowColumn(Payment.PaymentDateColumn)
                         .AddShowColumn(SysMember.MemberFullnameColumn)
                         .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                         .AddShowColumn(Payment.PaymentMemoColumn)
                         .AddShowColumn(Payment.PaymentSumColumn);
            this.gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBind);
            this.gridView.DataBind( );
            this.lblPointDesc.InnerHtml = string.Concat(GetPointSumInfo(rblFlag.Text), GetPointSumInInfo( ), GetPointSumOutInfo( ));
        }

        string gridView_OnDataBind(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "PaymentDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "PaymentSum":
                    decimal dSum = -Utilities.ToDecimal(rowData[column]);
                    int iType = Utilities.ToInt(rowData[Payment.PaymentTypeColumn.ColumnName]);
                    PaymentType pType = PaymentBLL.GetPaymentType(iType);
                    //if (pType == PaymentType.SetMethod)
                    //dSum = -dSum;
                    CalcPoint(rowData, dSum, pType);
                    result = dSum.ToString( );
                    break;
                case "MemberPhoneNumber":
                    PaymentType type = Formatter.ToEnum<PaymentType>(rowData[Payment.Columns.PaymentType]);
                    if (type == PaymentType.CompanyItem)
                        result = Utilities.ToString(rowData[ORIG_MEMBERPHONENUMBER]);
                    else
                        result = Utilities.ToString(rowData[column]);
                    break;
                case "PaymentMemo":
                    string phoneNum = Utilities.ToString(rowData[ORIG_MEMBERPHONENUMBER]);
                    //string fullName = Utilities.ToString(rowData["MemberFullName"]);
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, CompanyBLL.ReplacePhoneToName(Utilities.ToString(rowData[column]), phoneNum, string.Empty));
                    isRenderedCell = true;
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        void CalcPoint(System.Data.DataRow rowData, decimal dSum, PaymentType pType)
        {
            //decimal dPointSum = 0,dInPointSum = 0,dOutPointSum=0,dPointSum_Set = 0;
            //decimal dInPointSum_In = 0,dOutPointSum_In= 0;
            //decimal dInPointSUm_Out = 0, dOutPointSum_Out = 0;
            //            <asp:ListItem Text="消费赠送" Value="1"></asp:ListItem>
            //<asp:ListItem Text="充值赠送" Value="3"></asp:ListItem>
            //<asp:ListItem Text="抵扣" Value="2"></asp:ListItem>
            //<asp:ListItem Text="结算" Value="5"></asp:ListItem>
            int isOwner = Utilities.ToInt(rowData[CompanyBLL.IS_OWNER]);
            dPointSum += dSum;

            if (pType == PaymentType.CompanyItem)
            {
                dItemSum += Math.Abs(dSum);
                if (isOwner == 1)
                    dItemSum_Owner += Math.Abs(dSum);
                else
                    dItemSum_Outer += Math.Abs(dSum);
            }
            if (pType == PaymentType.AdvsGive)
            {
                dAdsSum += Math.Abs(dSum);
                if (isOwner == 1)
                    dAdsSum_Owner += Math.Abs(dSum);
                else
                    dAdsSum_Outer += Math.Abs(dSum);
            }
            if (pType == PaymentType.ConsumeGive)
                dOutPointSum_1 += Math.Abs(dSum);
            else if (pType == PaymentType.PrepaidGive || pType == PaymentType.AdvsGive || pType == PaymentType.Reward)
                dOutPointSum_3 += Math.Abs(dSum);
            else if (pType == PaymentType.Mortgage || pType == PaymentType.CompanyItem)
                dInPointSum += Math.Abs(dSum);
            else if (pType == PaymentType.SetMethod)
                dPointSum_Set += dSum;

            if (isOwner == 1)
            {
                if (pType == PaymentType.ConsumeGive ||
                    pType == PaymentType.PrepaidGive ||
                    pType == PaymentType.AdvsGive || pType == PaymentType.Reward)
                    dOutPointSum_Owner += Math.Abs(dSum);
                else if (pType == PaymentType.Mortgage || pType == PaymentType.CompanyItem)
                    dInPointSum_Owner += Math.Abs(dSum);
            }
            else
            {
                if (pType == PaymentType.ConsumeGive ||
                    pType == PaymentType.PrepaidGive ||
                    pType == PaymentType.AdvsGive || pType == PaymentType.Reward)
                    dOutPointSum_Outer += Math.Abs(dSum);
                else if (pType == PaymentType.Mortgage || pType == PaymentType.CompanyItem)
                    dInPointSum_Outer += Math.Abs(dSum);
            }
        }
        string GetPointSumInfo(string flag)
        {
            string result = string.Empty;
            switch (flag)
            {
                case "0":
                    result = string.Format(POINTSUM_ALl, dOutPointSum_1 + dOutPointSum_3 + (dPointSum_Set < 0 ? dPointSum_Set : 0), dInPointSum + (dPointSum_Set > 0 ? dPointSum_Set : 0));
                    break;
                case "1":
                    result = string.Format(POINTSUM_1, dOutPointSum_1);
                    break;
                case "2":
                    result = string.Format(POINTSUM_2, dInPointSum);
                    break;
                case "3":
                    result = string.Format(POINTSUM_3, dOutPointSum_3);
                    break;
                case "5":
                    result = string.Format(POINTSUM_4, dPointSum_Set);
                    break;
                case "6":
                    result = string.Format(POINTSUM_ITEM, dItemSum, dItemSum_Owner, dItemSum_Outer);
                    break;
                case "7":
                    result = string.Format(POINTSUM_ADS, dAdsSum, dAdsSum_Owner, dAdsSum_Outer);
                    break;
            }
            return result;
        }
        string GetPointSumInInfo( )
        {
            if (rblFlag.Text == "6" || rblFlag.Text == "7" || rblFlag.Text == "5")
                return string.Empty;
            else if (rblFlag.Text == "2")
                return string.Format(POINTSUM_OWNER_2, dInPointSum_Owner);
            else if (rblFlag.Text == "3" || rblFlag.Text == "1")
                return string.Format(POINTSUM_OWNER_3, dOutPointSum_Owner);
            return string.Format(POINTSUM_OWNER, dOutPointSum_Owner, dInPointSum_Owner);
        }
        string GetPointSumOutInfo( )
        {
            if (rblFlag.Text == "6" || rblFlag.Text == "7" || rblFlag.Text == "5")
                return string.Empty;
            else if (rblFlag.Text == "2")
                return string.Format(POINTSUM_OUTER_2, dInPointSum_Outer);
            else if (rblFlag.Text == "3" || rblFlag.Text == "1")
                return string.Format(POINTSUM_OUTER_3, dOutPointSum_Outer);
            return string.Format(POINTSUM_OUTER, dOutPointSum_Outer, dInPointSum_Outer);
        }
    }
}