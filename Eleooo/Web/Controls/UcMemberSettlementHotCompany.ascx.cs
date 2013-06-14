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
    public partial class UcMemberSettlementHotCompany : UserControlBase, ISettlement
    {
        const string MEMBERINFO = @"<b>{2}</b><br>姓名:<b>{3}</b>&nbsp您有<b>{0}</b>个积分，<b>{1}</b>元储值！";
        const string CHECKMEMBER_CMD = "CheckMember";

        private decimal dOrderSum = 0;
        private decimal dCompanyRate = 0;
        private SysMember user = null;
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

        protected override void OnInit(EventArgs e)
        {
            BindData( );
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //tdFingerContainer.Visible = Convert.ToBoolean(CurContext.Company.IsUseFinger);
            if (!IsPostBack)
            {
                ResetField( );
                ResetLabel( );
            }
            else
            {
                if (BasePage.EVENTTARGET == CHECKMEMBER_CMD)
                    btnMemberValidate_Click(sender, e);
                else if (!string.IsNullOrEmpty(BasePage.EVENTTARGET) && BasePage.EVENTTARGET.Contains("rblSaleType"))
                {
                    ResetField( );
                    ResetLabel( );
                }
            }
            txtMemberPhone.Text = hdnMemberPhone.Value;
        }

        void btnMemberValidate_Click(object sender, EventArgs e)
        {
            ValidateData( );
            CheckSale( );
        }
        private bool ValidateData( )
        {
            if (string.IsNullOrEmpty(txtOrderSum.Text))
            {
                lblOrderSumInfo.InnerHtml = "请输入消费金额!";
                txtOrderSum.Focus( );
                return false;
            }
            if (!decimal.TryParse(txtOrderSum.Text, out dOrderSum) || dOrderSum <= 0)
            {
                lblOrderSumInfo.InnerHtml = "你输入的消费金额不正确!";
                txtOrderSum.Focus( );
                return false;
            }
            dOrderSum = Formatter.CalcSumOk(dOrderSum);
            string sCompanyRate = rblCompanyRate.SelectedValue;
            string sCompanyRates = CurContext.Company.CompanyRate;
            if (!string.IsNullOrEmpty(sCompanyRate) && !string.IsNullOrEmpty(sCompanyRates))
            {
                if (!decimal.TryParse(sCompanyRate, out dCompanyRate) ||
                    !(sCompanyRates.Contains(sCompanyRate + ",") || sCompanyRates.Contains("," + sCompanyRate)) ||
                    dCompanyRate < 0)
                {
                    lblCompanyRateInfo.Text = "你输入的积分比例不正确!";
                    return false;
                }
            }
            btnPost.Enabled = false;
            SysMember member = DB.Select( ).From<SysMember>( )
                                 .Where(SysMember.MemberPhoneNumberColumn).IsEqualTo(hdnMemberPhone.Value)
                                 .ExecuteSingle<SysMember>( );
            if (member == null)
            {
                lblMemberInfo.InnerHtml = "会员账号输入不正确!";
                txtOrderSum.Focus( );
                return false;
            }
            user = member;

            if (string.IsNullOrEmpty(hdnMemberPwd.Value))
            {
                lblMemberPwdInfo.InnerHtml = "请输入会员的密码!";
                txtMemberPwd.Focus( );
                return false;
            }
            string passMD5 = Utilities.DESEncrypt(hdnMemberPwd.Value);
            if (!(Utilities.Compare(passMD5, user.MemberPwd) ||
                 Utilities.Compare(hdnMemberPwd.Value, user.MemberPwd)) &&
                !Utilities.Compare(hdnMemberPwd.Value, user.MemberFinger))
            {
                lblMemberPwdInfo.InnerHtml = "会员密码或指纹不正确!";
                txtMemberPwd.Focus( );
                return false;
            }

            string companyCode = string.Concat(AreaBLL.GetCompanyCodePrefix(AppContext.Context.Company.CompanyCode), "%");
            //companyCode = companyCode.Substring(0, companyCode.Length - 2) + "%";
            int dCashSum = DB.Select("sum(CashSum)").From<SysMemberCash>( )
                                        .InnerJoin(SysCompany.IdColumn, SysMemberCash.CashCompanyIDColumn)
                                        .Where(SysMemberCash.CashMemberIDColumn).IsEqualTo(member.Id)
                                        .And(SysCompany.CompanyCodeColumn).Like(companyCode)
                                        .ExecuteScalar<int>( );
            this.lblMemberInfo.InnerHtml = string.Format(MEMBERINFO, user.MemberBalance, dCashSum,
                                                         user.CompanyId > 0 ? "商家账号,不允许消费." : (user.MemberCompanyID == CurContext.Company.Id) ? "本店会员" : "外来会员",
                                                         user.MemberFullname);
            this.hdnMemberID.Value = user.Id.ToString( );
            this.hdnMemberBalance.Value = Convert.ToString(user.MemberBalance);
            this.hdnMemberBalanceCash.Value = dCashSum.ToString( );
            if (member.CompanyId > 0)
            {
                btnPost.Enabled = false;
                this.txtMemberPhone.Enabled = true;
                this.txtMemberPhone.Focus( );
            }
            else
            {
                this.txtMemberPhone.Enabled = false;
                this.txtOrderMemo.Focus( );
                btnPost.Enabled = true;
            }
            return btnPost.Enabled;
        }
        private void ResetField( )
        {
            this.txtMemberPhone.Text = string.Empty;
            this.txtMemberPwd.Text = string.Empty;
            hdnMemberPhone.Value = string.Empty;
            txtOrderMemo.Text = string.Empty;
            txtOrderSum.Text = string.Empty;
            hdnMemberBalance.Value = string.Empty;
            hdnMemberBalanceCash.Value = string.Empty;
            hdnMemberID.Value = string.Empty;
            hdnMemberPwd.Value = string.Empty;
            this.rblPayment.Items.Clear( );
            this.rblPayment.Items.Add(new ListItem("现金支付", "1"));
            this.rblPayment.SelectedIndex = 0;
            this.rblCompanyRate.SelectedIndex = 0;
            this.txtMemberPhone.Enabled = true;
            this.btnPost.Enabled = false;
            this.txtOrderSum.Focus( );
        }
        private void ResetLabel( )
        {
            lblMemberInfo.InnerHtml = string.Empty;
            lblOrderSumInfo.InnerHtml = string.Empty;
            lblMemberPwdInfo.InnerHtml = string.Empty;
            lblCompanyRateInfo.Text = string.Empty;
        }
        private void BindData( )
        {
            //bind companyrate
            string sCompanyRate = string.IsNullOrEmpty(CurContext.Company.CompanyRate) ? string.Empty :
                                    CurContext.Company.CompanyRate.Replace(" ", string.Empty);
            decimal d = 0;
            foreach (string str in sCompanyRate.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (decimal.TryParse(str, out d) && d >= 0)
                    this.rblCompanyRate.Items.Add(new ListItem(d.ToString( ) + "%", str));
            }
        }
        private bool CheckSale( )
        {
            decimal num = Convert.ToDecimal(this.hdnMemberBalanceCash.Value);
            decimal num2 = Convert.ToDecimal(this.hdnMemberBalance.Value);
            decimal num3 = Convert.ToDecimal(this.txtOrderSum.Text);
            decimal num4 = 0M;
            int num5 = (int)(num3 * ((num4 == 0M) ? 1M : num4));


            this.rblPayment.Items.Clear( );
            if (((int)num2) > 0)
            {
                if ((num5 - ((int)num2)) <= 0)
                {
                    this.rblPayment.Items.Add(new ListItem("可用" + ((int)num2) + "个积分，无需现金", "2"));
                }
                else
                {
                    this.rblPayment.Items.Add(new ListItem(string.Concat(new object[] { "可用", (int)num2, "个积分，请再支付", num5 - ((int)num2), "元" }), "2"));
                }
            }
            if (((int)num) > 0)
            {
                if ((num5 - ((int)num)) <= 0)
                {
                    this.rblPayment.Items.Add(new ListItem("可用" + ((int)num) + "元储值，无需现金", "3"));
                }
                else
                {
                    this.rblPayment.Items.Add(new ListItem(string.Concat(new object[] { "可用", (int)num, "元储值，请再支付", num5 - ((int)num), "元" }), "3"));
                }
            }
            if (((((int)num2) > 0) && (((int)num) > 0)) && (((int)num2) < num5))
            {
                if ((((int)num2) + ((int)num)) > num5)
                {
                    this.rblPayment.Items.Add(new ListItem(string.Concat(new object[] { "可用", (int)num2, "个积分和", (int)num, "元储值，无需现金" }), "4"));
                }
                else
                {
                    this.rblPayment.Items.Add(new ListItem(string.Concat(new object[] { "可用", (int)num2, "个积分和", (int)num, "元储值，请再支付", (num5 - ((int)num2)) - ((int)num), "元" }), "4"));
                }
            }
            if (this.rblPayment.Items.FindByValue("3") != null)
            {
                this.rblPayment.SelectedValue = "3";
                if (this.rblCompanyRate.Items.Count > 0)
                {
                    this.rblCompanyRate.SelectedIndex = 0;
                }
            }
            else if (this.rblPayment.Items.FindByValue("4") != null)
            {
                this.rblPayment.SelectedValue = "4";
                if (this.rblCompanyRate.Items.Count > 0)
                {
                    this.rblCompanyRate.SelectedIndex = 0;
                }
            }
            else if (this.rblPayment.Items.FindByValue("2") != null)
            {
                this.rblPayment.SelectedValue = "2";
                this.rblCompanyRate.SelectedIndex = -1;
            }
            else
            {
                this.rblPayment.Items.Add(new ListItem("现金支付", "1"));
                this.rblPayment.SelectedValue = "1";
                this.rblCompanyRate.SelectedIndex = 0;
            }
            if (this.rblPayment.Items.Count == 0)
            {
                this.btnPost.Enabled = false;
            }

            return true;
        }

        private bool SaveData(out string message)
        {

            message = string.Empty;
            if (!ValidateData( ))
            {
                message = "保存失败!";
                return false;
            }
            string payment;
            if (BasePage.Params.ContainsKey(rblPayment.UniqueID))
                payment = BasePage.Params[rblPayment.UniqueID];
            else
                payment = rblPayment.SelectedValue;
            try
            {
                Order data = new Order
                {
                    OrderCode = OrderCode,
                    OrderCard = string.Empty,
                    OrderDate = DateTime.Now,
                    OrderDateDeliver = DateTime.Now,
                    OrderDateUpload = DateTime.Now,
                    OrderMemberID = user.Id,
                    OrderMemo = txtOrderMemo.Text,
                    OrderProduct = "普通消费",
                    OrderQty = 0,
                    OrderRateSale = 0M,
                    OrderRate = dCompanyRate,
                    OrderPoint = 0M,
                    OrderSellerID = AppContext.Context.Company.Id,
                    OrderStatus = 1,
                    OrderSum = dOrderSum,
                    OrderSumOk = dOrderSum,
                    OrderPayPoint = Utilities.ToDecimal(this.hdnMemberBalance.Value),
                    OrderPayCash = Utilities.ToDecimal(this.hdnMemberBalanceCash.Value),
                    OrderType = 1,
                    OrderPay = 0,
                    ServiceSum = 0,
                    MansionId = 0
                };
                if (payment == "1")
                {
                    data.OrderPay = data.OrderSumOk;
                    data.OrderPayPoint = 0M;
                    data.OrderPayCash = 0M;
                }
                else if (payment == "2")
                {
                    data.OrderPayCash = 0M;
                    if (data.OrderPayPoint > data.OrderSumOk)
                    {
                        data.OrderPayPoint = data.OrderSumOk;
                        data.OrderPay = 0M;
                    }
                    else
                    {
                        data.OrderPay = data.OrderSumOk - data.OrderPayPoint;
                    }
                }
                else if (payment == "3")
                {
                    data.OrderPayPoint = 0M;
                    if (data.OrderPayCash > data.OrderSumOk)
                    {
                        data.OrderPayCash = data.OrderSumOk;
                        data.OrderPay = 0M;
                    }
                    else
                    {
                        data.OrderPay = data.OrderSumOk - data.OrderPayCash;
                    }
                }
                else if (payment == "4")
                {
                    if (data.OrderPayPoint > data.OrderSumOk)
                    {
                        data.OrderPayPoint = data.OrderSumOk;
                        data.OrderPayCash = 0M;
                        data.OrderPay = 0M;
                    }
                    else if ((data.OrderPayPoint + data.OrderPayCash) > data.OrderSumOk)
                    {
                        data.OrderPayCash = data.OrderSumOk - data.OrderPayPoint;
                        data.OrderPay = 0M;
                    }
                    else
                    {
                        data.OrderPay = (data.OrderSumOk - data.OrderPayPoint) - data.OrderPayCash;
                    }
                }
                if ((payment == "-1") && (data.OrderPayCash > 0M))
                {
                    lblCompanyRateInfo.Text = "请选择赠送积分比例!";
                    return false;
                }
                else
                {
                    if (data.OrderPayCash == 0M)
                    {
                        data.OrderRate = 0M;
                    }
                    using (TransactionScope ts = new TransactionScope( ))
                    {
                        using (SubSonic.SharedDbConnectionScope ss = new SubSonic.SharedDbConnectionScope( ))
                        {
                            //data.Save( );
                            if (!OrderBLL.SaveSaleRate(data, user, out message))
                                return false;
                            OrderBLL.UpdateBalance( );
                            ts.Complete( );
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log("UcMemberSettlementHotCompany->SaveData", ex, true);
                message = "消费失败，请与管理员联系<br/>" + ex.Message;
                return false;
            }
        }

        #region ISettlement

        public bool Save(out string errMsg)
        {
            bool result = SaveData(out errMsg);
            if (result)
            {
                ResetField( );
                ResetLabel( );
            }
            return result;
        }
        void ISettlement.OnPageLoad(object sender, EventArgs e)
        {
            
        }
        #endregion
    }
}