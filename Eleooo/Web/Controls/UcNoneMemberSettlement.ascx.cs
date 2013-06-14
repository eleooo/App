using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Transactions;
using System.ComponentModel;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcNoneMemberSettlement : UserControlBase, ISettlement
    {
        private decimal dPaySum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack ||
                (!string.IsNullOrEmpty(BasePage.EVENTTARGET) &&
                BasePage.EVENTTARGET.Contains("rblSaleType")))
            {
                ResetField( );
            }
        }
        private void ResetField( )
        {
            txtOrderSum.Text = string.Empty;
            lblOrderSumInfo.InnerHtml = string.Empty;
            txtOrderSum.Focus( );
        }
        private bool ValidateData( )
        {
            if (!decimal.TryParse(txtOrderSum.Text, out dPaySum))
            {
                lblOrderSumInfo.InnerHtml = "消费金额输入不正确!";
                txtOrderSum.Focus( );
                return false;
            }
            return true;
        }
        private bool SaveData(out string message)
        {
            message = string.Empty;
            try
            {
                string orderCode = OrderBLL.GetOrderCode(AppContext.Context.Company);
                SysMember user = AppContext.Context.User;
                Order order = new Order
                {
                    OrderCode = orderCode,
                    OrderCard = string.Empty,
                    OrderDate = DateTime.Now,
                    OrderDateDeliver = DateTime.Now,
                    OrderDateUpload = DateTime.Now,
                    OrderMemberID = user.Id,
                    OrderMemo = txtOrderMemo.Text,
                    OrderProduct = "普通消费",
                    OrderQty = 0,
                    OrderRateSale = 0M,
                    OrderRate = 0M,
                    OrderPoint = 0M,
                    OrderSellerID = AppContext.Context.Company.Id,
                    OrderStatus = 1,
                    OrderSum = dPaySum,
                    OrderSumOk = dPaySum,
                    OrderType = 1,
                    OrderPay = dPaySum,
                    OrderPayPoint = 0M,
                    OrderPayCash = 0M,
                    ServiceSum = 0,
                    MansionId = 0
                };
                using (TransactionScope ts = new TransactionScope( ))
                {
                    using (SubSonic.SharedDbConnectionScope ss = new SubSonic.SharedDbConnectionScope( ))
                    {
                        //order.Save( );
                        if (!OrderBLL.SaveSaleRate(order, user, out message))
                            return false;
                        ts.Complete( );
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log("UcNoneMemberSettlement->SaveData", ex, true);
                message = "消费失败，请与管理员联系<br/>" + ex.Message;
                return false;
            }

        }
        #region ISettlement 成员

        public bool Save(out string errMsg)
        {
            CompanyType cmpType = Formatter.ToEnum<CompanyType>(AppContext.Context.Company.CompanyType);
            if (cmpType != CompanyType.UnionCompany)
            {
                errMsg = "您暂无权限使用该功能";
                return false;
            }
            errMsg = string.Empty;
            if (!ValidateData( ))
            {
                errMsg = "保存失败!";
                return false;
            }
            bool result = SaveData(out errMsg);
            if (result)
                ResetField( );
            return result;
        }

        public void OnPageLoad(object sender, EventArgs e)
        {

        }

        #endregion

    }
}