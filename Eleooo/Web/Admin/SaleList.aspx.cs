using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SaleList : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string MEMBER_EDITOR_LINK = "/Admin/MemberEdit.aspx?ID={0}";
        const string COMPANY_EDITOR_LINK = "/Admin/CompanyEdit.aspx?ID={0}";
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
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            this.gridView.DataSource = DB.Select(Utilities.GetTableColumns(Order.Schema),
                                          "SYS_MEMBER.MemberFullname",
                                          Utilities.GetTableColumn(SysMember.MemberPhoneNumberColumn),
                                          "SYS_COMPANY.CompanyName",
                                          Utilities.GetTableColumn(SysMember.CompanyIdColumn)).From<Order>( )
                                         .InnerJoin(SysCompany.IdColumn, Order.OrderSellerIDColumn)
                                         .InnerJoin(SysMember.IdColumn, Order.OrderMemberIDColumn)
                                         .Where(Order.OrderDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                        .AndEx(SysMember.MemberPhoneNumberColumn.ColumnName).Like(filterMemberTel)
                                        .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                                        .CloseEx( )
                                        .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                        .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                        .CloseEx( )
                                        .OrderDesc("Orders.Id");
            this.gridView.AddShowColumn(Order.OrderDateColumn) //消费日期
                         .AddShowColumn(Order.OrderCodeColumn) //消费单号
                         .AddShowColumn(SysMember.MemberPhoneNumberColumn) //会员账号
                         .AddShowColumn(SysCompany.CompanyNameColumn)   //商家名
                         .AddShowColumn(Order.OrderSumColumn)           //消费金额
                         .AddShowColumn(Order.OrderRateSaleColumn)      //折扣
                         //.AddShowColumn(Order.OrderSumOkColumn)         //实际金额
                         .AddShowColumn(Order.OrderRateColumn)          //赠送比例
                         .AddShowColumn(Order.OrderPointColumn)         //赠送积分
                //.AddShowColumn("Orders.OrderPay")           //现金支付
                //.AddShowColumn("Orders.OrderPayCash")       //储值支付
                //.AddShowColumn("Orders.OrderPayPoint");     //积分支付
                         .AddCustomColumn("Action", "查看详情");
            this.gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBind);
            this.gridView.DataBind( );
        }

        string gridView_OnDataBind(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "OrderDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                case "OrderRateSale":
                    result = (Convert.ToDecimal(rowData[column]) * 10M).ToString("###0.0###");
                    break;
                case "OrderRate":
                    result = string.Format("{0}%", (Utilities.ToDecimal(rowData[column]) * 100M).ToString("####0.####"));
                    break;
                case "MemberFullname":
                    if (Utilities.ToInt(rowData[SysMember.Columns.CompanyId]) > 0)
                        result = "非会员";
                    else
                        result = string.Format(LINK_TEMPLATE, string.Format(MEMBER_EDITOR_LINK, rowData[Order.OrderMemberIDColumn.ColumnName]), rowData[column]);
                    break;
                case "CompanyName":
                    result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, rowData[Order.OrderSellerIDColumn.ColumnName]), rowData[column]);
                    break;
                case "Action":
                    result = string.Format(ACTION_DLG_TEMPLATE, rowData[Order.Columns.Id], "[详细]");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        protected override void On_ActionDelete(object sender, EventArgs e)
        {

        }
    }
}