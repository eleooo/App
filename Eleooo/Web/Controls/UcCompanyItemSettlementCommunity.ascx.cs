using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Transactions;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcCompanyItemSettlementCommunity : UserControlBase, ISettlement
    {
        const string SETMETHOD_HTML = "<input type=\"button\" value=\"验证\" name=\"btnSetMethod_{0}\" id=\"btnQuery_{0}\" onclick=\"__doPostBack('Add','{0}');\" />";
        const string ITEMSALE_HTML = "<div class='companyItemSale'>{0}</div>";
        const string CANCEL = "btnCancel";
        const string QUERY = "btnQuery";
        const string SETMETHOD = "btnSetMethod_";
        bool isNoDataFound = true;
        string MemberPhone
        {
            get
            {
                return Request.Params[txtMemberPhone.UniqueID];
            }
        }
        string MemberPwd
        {
            get
            {
                string pwd = Request.Params[txtMemberPwd.UniqueID];
                return string.IsNullOrEmpty(pwd) ? hdnMemberPwd.Value : pwd;
            }
        }
        string MemberFinger
        {
            get
            {
                return hdnFinger.Value;
            }
        }
        public void OnPageLoad(object sender, EventArgs e)
        {
            OnSaleLoad(sender, e);
        }
        protected void OnSaleLoad(object sender, EventArgs e)
        {
            if (!IsPostBack || BasePage.EVENTTARGET == CANCEL || BasePage.EVENTTARGET.Contains("rblSaleType"))
            {
                ReSetFieldValue( );
            }
            QueryMemberItem( );
        }
        void ReSetFieldValue( )
        {
            isNoDataFound = false;
            txtMessage.InnerHtml = string.Empty;
            txtMemberPhone.Value = string.Empty;
            txtMemberPwd.Value = string.Empty;
            txtMemberPhone.Focus( );
            hdnFinger.Value = string.Empty;
            hdnMemberPwd.Value = string.Empty;
        }
        private void QueryMemberItem( )
        {
            if (BasePage.EVENTTARGET == QUERY && string.IsNullOrEmpty(MemberPhone))
            {
                txtMessage.InnerHtml = "请输入会员账号";
                txtMemberPhone.Focus( );
                goto lbl_query;
            }
            if (!string.IsNullOrEmpty(MemberPhone) && string.IsNullOrEmpty(MemberPwd) && string.IsNullOrEmpty(MemberFinger))
            {
                txtMessage.InnerHtml = "请输入会员的密码或指纹";
                txtMemberPwd.Focus( );
                goto lbl_query;
            }
            int flag;
            string message;
        lbl_query:
            gridView.DataSource = CompanyItemBLL.GetOrderCompanyItemQuery(MemberPhone, MemberPwd, MemberFinger, CurContext.Company.Id, out flag, out message);
            if (flag > 0)
            {
                if (!string.IsNullOrEmpty(message))
                    txtMessage.InnerHtml = message;
                if (flag == 1)
                    txtMemberPhone.Focus( );
                else if (flag == 2)
                    txtMemberPwd.Focus( );
            }
            else if (flag == 0)
            {
                hdnFinger.Value = MemberFinger;
                hdnMemberPwd.Value = MemberPwd;
            }
            gridView.AddCustomColumn(SysMemberItem.OrderDateColumn.ColumnName, "日期")
                    .AddCustomColumn(SysCompanyItem.ItemTitleColumn.ColumnName, "优惠描述")
                    .AddCustomColumn(SysCompanyItem.ItemSumColumn.ColumnName, "原价")
                    .AddCustomColumn(SysMemberItem.ItemPointColumn.ColumnName, "积分兑换")
                    .AddCustomColumn("Amount", "数量")
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddCustomColumn(SysMemberItem.ItemDateColumn.ColumnName, "预计到店日期")
                    .AddCustomColumn("Action", "提交");
            //gridView.OnDataBindHeader += new DataBindHeaderHandle(gridView_OnDataBindHeader);
            gridView.OnDataBindColumn += new DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
            if (isNoDataFound)
            {
                if (string.IsNullOrEmpty(txtMessage.InnerHtml))
                    txtMessage.InnerHtml = "此会员没有抢购优惠信息";
                ReSetFieldValue( );
            }
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            isNoDataFound = false;
            string result = string.Empty;
            switch (column)
            {
                case "ItemTitle":
                    //result = string.Format(ITEMSALE_HTML, rowData[column]);
                    result = string.Format(ITEMSALE_HTML, Formatter.SubStr(rowData[column], 25));
                    break;
                case "Action":
                    result = string.Format(SETMETHOD_HTML, rowData[SysMemberItem.Columns.ItemID]);
                    break;
                case "ItemDate":
                case "OrderDate":
                    result = Utilities.ToDate(rowData[column]);
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        #region ISettlement 成员

        public bool Save(out string errMsg)
        {
            errMsg = string.Empty;
            int id = Utilities.ToInt(BasePage.EVENTARGUMENT);
            return CompanyItemBLL.OrderCompanyItem(CurContext.Company, id, MemberPwd, MemberFinger, out errMsg);
        }
        #endregion
    }
}