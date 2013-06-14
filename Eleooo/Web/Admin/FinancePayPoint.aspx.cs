using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Transactions;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class FinancePayPoint : ActionPage
    {
        const string LINK_TEMPLATE = "<a href='{0}' target='_blank'>{1}</a>";
        const string COMPANY_EDITOR_LINK = "/Admin/CompanyEdit.aspx?ID={0}";
        const string COMPANY_PAY_INPUT = "<input name=\"CompanyPay_{0}\" type=\"text\" id=\"CompanyPay_{0}\" value=\"{1}\" />";
        const string COMPANY_PAY_ACTION = "<input type=\"button\" name=\"CompanyPay_Action_{0}\" value=\"{1}\" id=\"CompanyPay_Action_{0}\" onclick=\"__doPostBack('Edit','{0}');\" />";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtMessage.InnerHtml = string.Empty;
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            gridView.DataSource = DB.Select( ).From<SysCompany>( )
                                    .Where(SysCompany.CompanyBalanceColumn).IsNotEqualTo(0)
                                    .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                                    .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                                    .CloseEx( );
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBind);
            gridView.AddShowColumn(SysCompany.CompanyNameColumn) //商家名称
                    .AddShowColumn(SysCompany.CompanyTelColumn)  //电话
                    .AddShowColumn(SysCompany.CompanySaleSumColumn) //销售金额
                    .AddShowColumn(SysCompany.CompanyBalanceCashColumn) //储值余额
                    .AddShowColumn(SysCompany.CompanyBalanceColumn) //积分余额
                    .AddCustomColumn("FinancePayPoint_CompanyPay", ResBLL.GetRes("FinancePayPoint_CompanyPay", "本次结算", "商家积分结算列名称"))
                    .AddCustomColumn("FinancePayPoint_PayAction", ResBLL.GetRes("FinancePayPoint_PayAction", "积分结算", "商家积分结算操作列名称"));
            gridView.DataBind( );
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            int companyID = Utilities.ToInt(EVENTARGUMENT);
            if (companyID <= 0)
            {
                txtMessage.InnerHtml = "结算商家参数错误!";
                goto lable_end;
            }
            string sPayInput = Params[string.Format("CompanyPay_{0}", companyID)];
            if (string.IsNullOrEmpty(sPayInput))
            {
                txtMessage.InnerHtml = "请输入结算金额!";
                goto lable_end;
            }
            decimal dPay = Math.Round(Utilities.ToDecimal(sPayInput));
            if (dPay == 0)
            {
                txtMessage.InnerHtml = "结算金额不能为零";
                goto lable_end;
            }

            SysCompany company = SysCompany.FetchByID(companyID);
            if (company == null)
            {
                txtMessage.InnerHtml = "结算商家参数错误!";
                goto lable_end;
            }
            if (Math.Abs(dPay) > Math.Abs(Utilities.ToDecimal(company.CompanyBalance)))
            {
                txtMessage.InnerHtml = "结算积分不能大于商家现有的积分余额!";
                goto lable_end;
            }
            string payMemo;
            //本次结算积分差额为{0}分，已向【{1}】支付{2}元
            //本次结算积分差额为{0}分，已向【{1}】收取{2}元
            if (dPay > 0)
                payMemo = string.Format("本次结算积分差额为{0}分，【{1}】已向【乐多分】收取{2}元", Math.Abs(dPay), company.CompanyName, Math.Abs(dPay));
            else
                payMemo = string.Format("本次结算积分差额为{0}分，【{1}】已向【乐多分】支付{2}元", Math.Abs(dPay), company.CompanyName, Math.Abs(dPay));
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                SysMember mainAccount = UserBLL.MainAccount;
                new Payment
                {
                    PaymentCode = string.Empty,
                    PaymentDate = DateTime.Now,
                    PaymentCompanyID = companyID,
                    PaymentMemberID = mainAccount.Id,
                    PaymentSum = dPay,
                    PaymentStatus = 1,
                    PaymentType = (int)(PaymentType.SetMethod),
                    PaymentOrderID = 0,
                    PaymentEmail = string.Empty,
                    PaymentMemo = payMemo
                }.Save(AppContext.Context.User.Id);
                OrderBLL.UpdateBalance( );
                ts.Complete( );
            }
            catch (Exception ex)
            {
                txtMessage.InnerHtml = ex.Message;
                Logging.Log("FinancePayPoint->On_ActionEdit", ex, true);
                goto lable_end;
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
            txtMessage.InnerHtml = payMemo;
        lable_end:
            this.On_ActionQuery(sender, e);
        }
        string gridView_OnDataBind(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "CompanyName":
                    result = string.Format(LINK_TEMPLATE, string.Format(COMPANY_EDITOR_LINK, rowData[SysCompany.IdColumn.ColumnName]), rowData[column]);
                    break;
                case "FinancePayPoint_CompanyPay":
                    result = string.Format(COMPANY_PAY_INPUT, rowData[SysCompany.IdColumn.ColumnName], rowData[SysCompany.Columns.CompanyBalance]);
                    break;
                case "FinancePayPoint_PayAction":
                    result = string.Format(COMPANY_PAY_ACTION, rowData[SysCompany.IdColumn.ColumnName], ResBLL.GetRes("FinancePayPoint_PayAction", "积分结算", "商家积分结算操作列名称"));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
    }
}