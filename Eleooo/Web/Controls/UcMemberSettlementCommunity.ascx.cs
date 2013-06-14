using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Transactions;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcMemberSettlementCommunity : UserControlBase, ISettlement
    {
        const string MEMBERINFO = @"<b>{2}</b><br><b>{3}</b>&nbsp您有<b>{1}</b>元储值，<b>{0}</b>个积分！";
        const string CHECKMEMBER_CMD = "CheckMember";
        CompanyRateCollection _rates;
        CompanyRateCollection Rates
        {
            get
            {
                if (_rates == null)
                    _rates = CompanyBLL.GetCompanyRates(AppContext.Context.Company);
                return _rates;
            }
        }
        private decimal dOrderSum = 0;
        private decimal dOrderSumOk = 0;
        private decimal dRateSale = 0;
        private SysMember user = null;
        private string _userGradeInfo = string.Empty;
        private string UserGradeInfo(SysMember member)
        {
            if (!string.IsNullOrEmpty(_userGradeInfo))
                return _userGradeInfo;
            _userGradeInfo = UserBLL.UserGradeInfo(member);
            return _userGradeInfo;
        }
        private string _orderCode;
        protected string OrderCode
        {
            get
            {
                if (string.IsNullOrEmpty(_orderCode))
                    _orderCode = OrderBLL.GetOrderCode(CurContext.Company);
                return _orderCode;
            }
        }
        string payment
        {
            get
            {
                if (BasePage.Params.ContainsKey(rblPayment.UniqueID))
                    return BasePage.Params[rblPayment.UniqueID];
                else
                    return rblPayment.SelectedValue;
            }
        }
        private decimal dCompanyRate = 0;
        private decimal dPayPoint = 0;
        private decimal dPayCash = 0;

        protected override void OnInit(EventArgs e)
        {
            BindSaleData( );
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.EnableViewState = false;
            if (!IsPostBack)
            {
                ReSetField( );
                ReSetLabel( );
                txtMemberPhone.Focus( );
            }
            else
            {
                if (BasePage.EVENTTARGET == CHECKMEMBER_CMD)
                    btnMemberValidate_Click(sender, e);
                else if (!string.IsNullOrEmpty(BasePage.EVENTTARGET) && BasePage.EVENTTARGET.Contains("rblSaleType"))
                {
                    ReSetField( );
                    ReSetLabel( );
                    txtMemberPhone.Focus( );
                }
            }
            txtMemberPhone.Text = hdnMemberPhone.Value;
        }
        void btnMemberValidate_Click(object sender, EventArgs e)
        {
            if (ValidateMember( ))
                CheckSale( );
        }

        private bool ValidateMember( )
        {
            this.txtOrderSumOk.Text = this.txtOrderSum.Text;
            string phoneNum = hdnMemberPhone.Value;
            SysMember member = UserBLL.GetUserByPhoneNum(hdnMemberPhone.Value);
            if (member == null)
            {
                this.txtMemberPhone.Enabled = true;
                this.hdnMemberID.Value = "0";
                this.hdnMemberBalance.Value = "0";
                this.hdnMemberBalanceCash.Value = "0";
                this.lblMemberInfo.InnerHtml = "您输入的手机号码不存在！";
                txtMemberPhone.Focus( );
                return false;
            }
            if (AppContext.Context.CompanyType.Value != CompanyType.UnionCompany &&
                AppContext.Context.CompanyType.Value != CompanyType.SpecialCompany)
            {
                this.lblMemberInfo.InnerHtml = "您暂无权限使用该功能";
                return false;
            }
            if (Utilities.Compare(this.hdnMemberPhone.Value, this.hdnCompanyPhone.Value))
            {
                this.lblMemberInfo.InnerHtml = "";
            }
            else
            {
                this.lblMemberInfo.InnerHtml = string.Format(MEMBERINFO, member.MemberBalance, 0,
                                                                        UserGradeInfo(member),
                                                                        member.MemberFullname);
                //<b>{2}</b><br><b>{3}</b>&nbsp您有<b>{1}</b>元储值，<b>{0}</b>个积分！
            }
            this.hdnMemberID.Value = Convert.ToString(member.Id);
            this.hdnMemberPwd.Value = member.MemberPwd;
            this.hdnMemberBalance.Value = AppContext.Context.CompanyType == CompanyType.UnionCompany ? Convert.ToString((int)member.MemberBalance.Value) : "0";
            this.hdnMemberBalanceCash.Value = "0";
            decimal num3 = Formatter.Round(txtOrderSum.Text);
            this.txtOrderSumOk.Text = num3.ToString("#####0.##");
            if (decimal.TryParse(this.txtMemberRateSale.Text, out dRateSale) && dRateSale > 0 && dRateSale < 100)
            {
                this.txtOrderSumOk.Text = Formatter.CalcSumOk((num3 * dRateSale / 100M)).ToString("#####0.##");
            }
            SysMemberCash cash = UserBLL.GetUserLatestCash(member.Id, CurContext.Company.Id);
            if (cash != null)
            {
                cash.CashRate = Utilities.IsNull(cash.CashRate) ? dRateSale : cash.CashRate;
                if (cash.CashRate > 0M && !(dRateSale > 0 && dRateSale < 100))
                {
                    num3 = (num3 * Convert.ToDecimal(cash.CashRate));
                    this.txtMemberRateSale.Text = (Convert.ToDecimal(cash.CashRate) * 100M).ToString("#####0.##");
                    this.txtOrderSumOk.Text = Formatter.CalcSumOk(num3).ToString("####0.##");
                }
                decimal cashSum = UserBLL.GetUserBalanceCash(member.Id, CurContext.Company.Id);
                this.hdnMemberBalanceCash.Value = cashSum.ToString( );
                this.txtOrderMemo.Text = cash.CashMemo;
                var rates = CompanyBLL.GetCompanyRates(AppContext.Context.Company);
                if (this.hdnMemberPhone.Value == this.hdnCompanyPhone.Value)
                {
                    this.lblMemberInfo.InnerHtml = "";
                }
                else
                {
                    this.lblMemberInfo.InnerHtml = string.Format(MEMBERINFO, member.MemberBalance,
                                                                            Formatter.Round(cashSum),
                                                                             UserGradeInfo(member),
                                                                             member.MemberFullname);
                }
                if (cash.CashRateSale.HasValue && cash.CashRateSale.Value > 0)
                {
                    int index = Rates.IndexOf(cash.CashRateSale.Value);
                    if (index >= 0)
                        rblCompanyRate.SelectedIndex = index;
                }
            }
            //lbl_End:
            this.txtMemberPhone.Enabled = false;
            this.rblPayment.SelectedIndex = 0;
            this.txtMemberPwd.Focus( );
            this.btnPost.Enabled = true;
            return true;
        }

        private void CheckSale( )
        {
            string selectedValue = this.rblPayment.SelectedValue;
            int num = (int)Formatter.Low(Utilities.ToDecimal(this.hdnMemberBalanceCash.Value)); //储值余额
            int num2 = (int)Formatter.Low(Utilities.ToDecimal(this.hdnMemberBalance.Value));    //积分余额
            decimal num3 = Utilities.ToDecimal(this.txtOrderSum.Text);      //消费金额
            decimal num4 = Utilities.ToDecimal(txtMemberRateSale.Text); //打折

            decimal num5 = Formatter.CalcSumOk(num3 * ((num4 <= 0M) ? 1M : num4 / 100));
            //this.txtOrderSumOk.Text = num5.ToString( );
            this.rblPayment.Items.Clear( );
            this.rblPayment.Items.Add(new ListItem("现金支付", "1"));
            if (num2 > 0)
            {
                if ((num5 - num2) <= 0)
                {
                    this.rblPayment.Items.Add(new ListItem("可用" + (num2) + "个积分，无需现金", "2"));
                }
                else
                {
                    this.rblPayment.Items.Add(new ListItem(string.Concat(new object[] { "可用", num2 + "个积分，请再支付", num5 - num2, "元" }), "2"));
                }
            }
            if (num > 0)
            {
                if ((num5 - num) <= 0)
                {
                    this.rblPayment.Items.Add(new ListItem("可用" + num + "元储值，无需现金", "3"));
                }
                else
                {
                    this.rblPayment.Items.Add(new ListItem(string.Concat(new object[] { "可用", num, "元储值，请再支付", num5 - num, "元" }), "3"));
                }
            }
            if (((num2 > 0) && ((num) > 0)) && ((num2) < num5))
            {
                if ((num2 + num) > num5)
                {
                    this.rblPayment.Items.Add(new ListItem(string.Concat(new object[] { "可用", num2, "个积分和", num, "元储值，无需现金" }), "4"));
                }
                else
                {
                    this.rblPayment.Items.Add(new ListItem(string.Concat(new object[] { "可用", num2, "个积分和", num, "元储值，请再支付", num5 - num2 - num, "元" }), "4"));
                }
            }
            if (this.rblPayment.Items.FindByValue(selectedValue) != null)
            {
                this.rblPayment.SelectedValue = selectedValue;
            }
            else
            {
                this.rblPayment.SelectedIndex = 0;
            }
            btnPost.Enabled = true;
        }

        private void ReSetField( )
        {
            rowCompanyRate.Visible = AppContext.Context.CompanyType.Value == CompanyType.UnionCompany;
            this.hdnMemberPhone.Value = string.Empty;
            this.txtMemberPwd.Text = string.Empty;
            txtMemberRateSale.Text = string.Empty;
            txtOrderMemo.Text = string.Empty;
            txtOrderSum.Text = string.Empty;
            txtOrderSumOk.Text = string.Empty;
            hdnCompanyID.Value = Convert.ToString(CurContext.User.CompanyId);
            hdnCompanyPhone.Value = CurContext.Company.CompanyTel;
            hdnMemberBalance.Value = string.Empty;
            hdnMemberBalanceCash.Value = string.Empty;
            hdnMemberID.Value = string.Empty;
            hdnMemberPwd.Value = string.Empty;
            this.rblPayment.Items.Clear( );
            this.rblPayment.Items.Add(new ListItem("现金支付", "1"));
            this.rblPayment.SelectedIndex = 0;
            if (rblCompanyRate.Items.Count > 0)
                this.rblCompanyRate.SelectedIndex = 0;
            this.txtMemberPhone.Enabled = true;
            this.btnPost.Enabled = false;
            BindSaleData( );
            txtMemberPhone.Focus( );
        }
        private void ReSetLabel( )
        {
            lblCompanyRateInfo.InnerHtml = string.Empty;
            lblMemberInfo.InnerHtml = string.Empty;
            lblMemberPwdInfo.InnerHtml = string.Empty;
            lblMemberRateSale.InnerHtml = string.Empty;
            lblOrderSumOkInfo.InnerHtml = string.Empty;
        }
        private bool SaveOrder(out string message)
        {
            message = string.Empty;
            Order order = new Order
            {
                OrderCode = this.OrderCode,
                OrderCard = string.Empty,
                OrderDate = DateTime.Now,
                OrderDateDeliver = DateTime.Now,
                OrderDateUpload = DateTime.Now,
                OrderMemberID = user.Id,
                OrderMemo = txtOrderMemo.Text,
                OrderProduct = ddlOrderItem.Visible ? ddlOrderItem.SelectedValue : "普通消费",
                OrderQty = 0,
                OrderRateSale = dRateSale / 100M, // 折扣
                OrderRate = dCompanyRate / 100M,  //赠送比例
                OrderPoint = dOrderSumOk * dCompanyRate / 100M, //赠送积分
                OrderSellerID = CurContext.Company.Id,
                OrderStatus = 1,
                OrderSum = dOrderSum,
                OrderSumOk = dOrderSumOk,
                OrderPayCash = dPayCash, //储值支付
                OrderPayPoint = dPayPoint,//积分支付
                OrderType = 1,
                ServiceSum = 0,
                MansionId = 0
            };
            if (payment == "1")
            {
                order.OrderPay = order.OrderSumOk;
                order.OrderPayCash = 0M;
                order.OrderPayPoint = 0M;
            }
            else if (payment == "2")
            {
                order.OrderPayCash = 0M;
                if (order.OrderPayPoint > order.OrderSumOk)
                {
                    order.OrderPayPoint = order.OrderSumOk;
                    order.OrderPay = 0M;
                }
                else
                {
                    order.OrderPay = order.OrderSumOk - order.OrderPayPoint;
                }
            }
            else if (payment == "3")
            {
                order.OrderPayPoint = 0M;
                if (order.OrderPayCash > order.OrderSumOk)
                {
                    order.OrderPayCash = order.OrderSumOk;
                    order.OrderPay = 0M;
                }
                else
                {
                    order.OrderPay = order.OrderSumOk - order.OrderPayCash;
                }
            }
            else if (payment == "4")
            {
                if (order.OrderPayPoint > order.OrderSumOk)
                {
                    order.OrderPayPoint = order.OrderSumOk;
                    order.OrderPayCash = 0M;
                    order.OrderPay = 0M;
                }
                else if ((order.OrderPayPoint + order.OrderPayCash) > order.OrderSumOk)
                {
                    order.OrderPayCash = order.OrderSumOk - order.OrderPayPoint;
                    order.OrderPay = 0M;
                }
                else
                {
                    order.OrderPay = (order.OrderSumOk - order.OrderPayPoint) - order.OrderPayCash;
                }
            }
            if ((order.OrderPay == 0M) && (order.OrderPayCash == 0M))
            {
                order.OrderRate = 0M;
            }

            try
            {
                using (TransactionScope ts = new TransactionScope( ))
                {
                    using (SubSonic.SharedDbConnectionScope ss = new SubSonic.SharedDbConnectionScope( ))
                    {
                        //order.Save( );
                        if (!OrderBLL.SaveSaleRate(order, user, out message))
                            return false;
                        OrderBLL.UpdateBalance( );
                        ts.Complete( );
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log("UcMemberSettlementCommunity->SaveOrder", ex, true);
                message = ex.Message;
                Response.Output.WriteLine(ex.StackTrace);
                return false;
            }
        }


        #region ISettlement

        public bool Save(out string errMsg)
        {
            errMsg = string.Empty;
            if (!ValidateFieldData( ))
            {
                errMsg = "保存失败!";
                btnPost.Enabled = true;
                return false;
            }
            if (!SaveOrder(out errMsg))
            {
                btnPost.Enabled = true;
                return false;
            }
            ReSetField( );
            ReSetLabel( );
            return true;
        }
        #endregion

        private void BindSaleData( )
        {
            if (AppContext.Context.CompanyType.Value != CompanyType.UnionCompany)
                return;
            //bind companyrate
            if (rblCompanyRate.Items.Count == 0)
                rblCompanyRate.Items.AddRange(Rates.ToListItem( ));
            if (rblCompanyRate.Items.Count > 0)
                rblCompanyRate.SelectedIndex = 0;
            return;
            //bind companyitem
            //string sCompanyItem = CurContext.Company.CompanyItem;
            //if (!string.IsNullOrEmpty(sCompanyItem))
            //{
            //    trOrderItem.Visible = true;
            //    ddlOrderItem.SelectedIndex = -1;
            //    foreach (string str2 in sCompanyItem.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //    {
            //        if (str2.Trim( ) != string.Empty)
            //        {
            //            this.ddlOrderItem.Items.Add(new ListItem(str2, str2));
            //        }
            //    }
            //}
        }
        #region ValidateFieldData
        private bool ValidateFieldData( )
        {
            decimal _dRate = Utilities.ToDecimal(rblCompanyRate.UniqueID);
            int index = Rates.IndexOf(_dRate);
            if (index >= 0)
                rblCompanyRate.SelectedIndex = index;
            txtOrderSumOk.Text = BasePage.Params[txtOrderSumOk.UniqueID];
            if (string.IsNullOrEmpty(hdnMemberPhone.Value))
            {
                lblMemberInfo.InnerHtml = "会员账号不能为空!";
                return false;
            }

            user = UserBLL.GetUserByPhoneNum(hdnMemberPhone.Value);
            if (user == null)
            {
                lblMemberInfo.InnerHtml = "你输入的会员账号不存在! ";
                txtMemberPhone.Focus( );
                return false;
            }
            if (user.CompanyId > 0)
            {
                lblMemberInfo.InnerHtml = "你不能用此账号进行消费,此账号属于商家账号!";
                return false;
            }
            if (payment != "1")
            {
                if (string.IsNullOrEmpty(txtMemberPwd.Text))
                {
                    lblMemberPwdInfo.InnerText = "会员密码不能为空!";
                    txtMemberPwd.Focus( );
                    return false;
                }

                if (!UserBLL.CheckUserPwd(user, txtMemberPwd.Text.Trim( )) &&
                     !UserBLL.CheckUserFinger(user, txtMemberPwd.Text.Trim( )))
                {
                    lblMemberPwdInfo.InnerHtml = "会员密码或指纹不正确!";
                    txtMemberPwd.Focus( );
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtOrderSum.Text))
            {
                lblOrderSumInfo.InnerHtml = "会员消费金额不能为空!";
                return false;
            }
            if (!decimal.TryParse(txtOrderSum.Text, out dOrderSum) || dOrderSum <= 0)
            {
                lblOrderSumInfo.InnerHtml = "会员消费金额不正确,不能小于或等于0";
                return false;
            }
            //if (string.IsNullOrEmpty(txtOrderSumOk.Text))
            //{
            //    lblOrderSumOkInfo.InnerHtml = "会员实际消费金额不能为空!";
            //    return false;
            //}
            //if (!decimal.TryParse(txtOrderSumOk.Text, out dOrderSumOk) || dOrderSumOk < 0)
            //{
            //    lblOrderSumOkInfo.InnerHtml = "会员实际消费金额不正确,不能小于零!";
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtMemberRateSale.Text))
                txtMemberRateSale.Text = "0";
            decimal.TryParse(txtMemberRateSale.Text, out dRateSale);
            if (dRateSale < 0 || dRateSale >= 100)
            {
                lblMemberRateSale.InnerHtml = "消费折扣输入不正确,请输入1-99之间的数!";
                return false;
            }
            string sCompanyRate = BasePage.Params[rblCompanyRate.UniqueID];
            string sCompanyRates = "," + CurContext.Company.CompanyRate;
            if (!string.IsNullOrEmpty(sCompanyRate) && !string.IsNullOrEmpty(sCompanyRates))
            {
                if (!decimal.TryParse(sCompanyRate, out dCompanyRate) ||
                    !(sCompanyRates.Contains(sCompanyRate + ",") || sCompanyRates.Contains("," + sCompanyRate)) ||
                    dCompanyRate < 0)
                {
                    if (dCompanyRate != _dRate)
                    {
                        lblCompanyRateInfo.InnerHtml = "你输入的积分比例不正确!";
                        return false;
                    }
                }
            }
            dPayPoint = Formatter.Low(this.hdnMemberBalance.Value);
            dPayCash = Formatter.Low(this.hdnMemberBalanceCash.Value);
            dOrderSumOk = dRateSale > 0 && dRateSale < 100 ? Formatter.CalcSumOk(dOrderSum * dRateSale / 100M) : dOrderSum;
            txtOrderSumOk.Text = dOrderSumOk.ToString("#####0.##");
            return true;
        }
        void ISettlement.OnPageLoad(object sender, EventArgs e)
        {

        }
        #endregion
    }
}