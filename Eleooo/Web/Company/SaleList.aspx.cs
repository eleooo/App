using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Text;
using System.Transactions;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class SaleList : ActionPage
    {
        const string ORDER_SUM = "共计消费{0}元,赠送了{1}个积分<br/>";
        const string ORDER_SUM_IN = "其中本店会员消费{0}元,赠送了{1}个积分<br/>";
        const string ORDER_SUM_OUT = "其中外来会员消费{0}元,赠送了{1}个积分<br/>";
        const string ORDER_SUM_OWNER = "其中非会员消费{0}元";
        const string MEMBER_EDITOR_LINK = "/Company/MemberEdit.aspx?ID={0}";
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        decimal dOrderSumOk = 0, dOrderPoint = 0;
        decimal dOrderSumOk_IN = 0, dOrderPoint_IN = 0;
        decimal dOrderSumOk_OUT = 0, dOrderPoint_OUT = 0;
        decimal dOrderSumOk_OWNER = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
            txtMessage.InnerHtml = string.Empty;
        }
        string GetRenderPhoneNumCol(string flag)
        {
            CompanyType companyType = AppContext.Context.CompanyType.Value;
            if (flag == "1" || companyType == CompanyType.SpecialCompany ||
                companyType == CompanyType.AdCompany)
                return "SYS_MEMBER.MemberPhoneNumber";
            //else if (flag == "2")
            //    return CompanyBLL.RenderUserPhone(SysMember.MemberPhoneNumberColumn);
            else
                return CompanyBLL.RenderUserPhone(Order.OrderMemberIDColumn, Order.OrderSellerIDColumn, SysMember.MemberPhoneNumberColumn, SysMember.MemberCompanyIDColumn);
        }
        string GetRenderIsOwnerUser(string flag)
        {
            if (flag == "1")
                return CompanyBLL.RenderIsOwner("1");
            else if (flag == "2")
                return CompanyBLL.RenderIsOwner("0");
            else
                return CompanyBLL.RenderIsOwner(Order.OrderMemberIDColumn, Order.OrderSellerIDColumn);
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                int orderID = Utilities.ToInt(EVENTARGUMENT);
                string message;
                if (OrderBLL.DeleteMemberOrder(orderID, AppContext.Context.Company, out message))
                {
                    OrderBLL.UpdateBalance( );
                    ts.Complete( );
                }
                txtMessage.InnerHtml = message;
            }
            catch (Exception ex)
            {
                Logging.Log("SaleList->On_ActionDelete", ex, true);
                txtMessage.InnerHtml = ex.Message;
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(Order.Schema),
                                  GetRenderPhoneNumCol(rblFlag.SelectedValue),
                                  GetRenderIsOwnerUser(rblFlag.SelectedValue),
                                  SysMember.Columns.MemberFullname,
                                  SysMember.Columns.CompanyId).From<Order>( )
                                  .InnerJoin(SysMember.IdColumn, Order.OrderMemberIDColumn)
                                  .Where(Order.OrderDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                  .And(Order.OrderTypeColumn).IsEqualTo((int)OrderType.Common)
                                  .And(SysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                                  .And(Order.OrderSellerIDColumn).IsEqualTo(CurrentUser.CompanyId)
                                  .OrderDesc("Orders.ID");
            if (rblFlag.SelectedValue == "1") //本店
            {
                query.ConstraintExpression(CompanyBLL.RenderIsOwner(Order.OrderMemberIDColumn, Order.OrderSellerIDColumn, MemberFilter.Owner))
                     .And(SysMember.CompanyIdColumn).IsEqualTo(0);
            }
            else if (rblFlag.SelectedValue == "2") //外来
            {
                query = query.ConstraintExpression(CompanyBLL.RenderIsOwner(Order.OrderMemberIDColumn, Order.OrderSellerIDColumn, MemberFilter.Outer));
            }
            this.gridView.DataSource = query;
            this.gridView.AddShowColumn(Order.OrderDateColumn) //消费日期
                         .AddShowColumn(Order.OrderCodeColumn) //消费单号
                         .AddShowColumn(SysMember.MemberFullnameColumn) //会员名
                         .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                         .AddShowColumn(Order.OrderSumColumn)           //消费金额
                         .AddShowColumn(Order.OrderRateSaleColumn)      //折扣
                         .AddShowColumn(Order.OrderSumOkColumn)         //实际金额
                         .AddShowColumn(Order.OrderRateColumn)          //赠送比例
                         .AddShowColumn(Order.OrderPointColumn)         //赠送积分
                //.AddShowColumn(Order.OrderPayColumn)           //现金支付
                //.AddShowColumn(Order.OrderPayCashColumn)       //储值支付
                //.AddShowColumn(Order.OrderPayPointColumn);     //积分支付
                         .AddCustomColumn("Action", "操作");
            this.gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBind);
            this.gridView.DataBind( );
            PrintSaleDesc(rblFlag.Text);
        }
        void PrintSaleDesc(string flag)
        {
            if (flag == "1") //本店
                lblSaleDesc.InnerHtml = string.Format(ORDER_SUM_IN, dOrderSumOk_IN, dOrderPoint_IN);
            else if (flag == "2") //外来
                lblSaleDesc.InnerHtml = string.Format(ORDER_SUM_OUT, dOrderSumOk_OUT, dOrderPoint_OUT);
            else
                lblSaleDesc.InnerHtml = string.Concat(
                                                    string.Format(ORDER_SUM, dOrderSumOk, dOrderPoint),
                                                    string.Format(ORDER_SUM_IN, dOrderSumOk_IN, dOrderPoint_IN),
                                                    string.Format(ORDER_SUM_OUT, dOrderSumOk_OUT, dOrderPoint_OUT),
                                                    string.Format(ORDER_SUM_OWNER, dOrderSumOk_OWNER));
        }
        string gridView_OnDataBind(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "MemberFullname":
                    result = Utilities.ToInt(rowData["OrderMemberID"]) == CurrentUser.Id ? "非会员" : Utilities.ToString(rowData[column]);
                    break;
                case "OrderDate":
                    result = Utilities.ToDate(rowData[column]);
                    CalcOrdersInfo(rowData);
                    break;
                case "OrderRateSale":
                    result = (Convert.ToDecimal(rowData[column]) * 10M).ToString("###0.0###");
                    break;
                case "OrderRate":
                    result = string.Format("{0}%", (Utilities.ToDecimal(rowData[column]) * 100M).ToString("####0.####"));
                    break;
                case "MemberPhoneNumber":
                    if (Convert.ToInt32(rowData[CompanyBLL.IS_OWNER]) == 1)
                        result = string.Format(LINK_TEMPLATE, string.Format(MEMBER_EDITOR_LINK, rowData[Order.OrderMemberIDColumn.ColumnName]), rowData[column]);
                    else
                        result = Utilities.ToString(rowData[column]);
                    break;
                case "Action":
                    StringBuilder sb = new StringBuilder( );
                    sb.AppendFormat(ACTION_DEL_TEMPLATE, rowData[Order.Columns.Id], "[删除]");
                    sb.Append("&nbsp;");
                    sb.AppendFormat(ACTION_DLG_TEMPLATE, rowData[Order.Columns.Id], "[详细]");
                    result = sb.ToString( );
                    break;
                default:
                    //if (rowData["OrderCode"].ToString( ) == "SZ0201012012042500002" && (column == "OrderSumOk" || column == "OrderPay"))
                    //    result = "";
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        void CalcOrdersInfo(System.Data.DataRow row)
        {
            decimal dSum = Convert.ToDecimal(row["OrderSumOk"]);
            decimal dPoint = Convert.ToDecimal(row["OrderPoint"]);
            int isOwner = Convert.ToInt32(row[CompanyBLL.IS_OWNER]);
            int MemberID = Convert.ToInt32(row["OrderMemberID"]);
            dOrderSumOk += dSum;
            dOrderPoint += dPoint;

            if (MemberID == CurrentUser.Id)
            {
                dOrderSumOk_OWNER += dSum;
            }
            else if (isOwner == 1)
            {
                dOrderSumOk_IN += dSum;
                dOrderPoint_IN += dPoint;
            }
            else
            {
                dOrderSumOk_OUT += dSum;
                dOrderPoint_OUT += dPoint;
            }
        }
    }
}