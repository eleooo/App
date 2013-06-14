using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Eleooo.DAL;
using System.Diagnostics;

namespace Eleooo.Client
{
    public partial class UcMemberOrder : UserControlBase
    {
        private OrderBLL _orderHelper;
        public OrderBLL CurOrder
        {
            get
            {
                if (_orderHelper == null)
                {
                    _orderHelper = new OrderBLL( );
                }
                return _orderHelper;
            }
        }
        public SysMember OrderUser
        {
            get
            {
                return OrderData.OrderUser;
            }
        }
        public OrderEntity OrderData
        {
            get
            {
                return CurOrder.OrderData;
            }
        }
        public DataTable UserInfo
        {
            get
            {
                return CurOrder.UserInfo;
            }
        }
        public UcMemberOrder( )
        {
            InitializeComponent( );
            //BindData( );
        }
        public void InitOrder( )
        {
            if (!AppContext.IsRuning)
                return;
            CurOrder.InitOrder( );
            lblPhoneInfo.Text = "";
            lblPhoneInfo.ForeColor = Color.Red;
            lblPassInfo.Text = "";
            lblPassInfo.ForeColor = Color.Red;
            lblOrderSumInfo.Text = "";
            lblOrderSumInfo.ForeColor = Color.Red;
            plUserInfo.Text = "";
            plUserInfo.ForeColor = Color.Red;
            lblMessage.ForeColor = Color.Red;
            btnSave.Enabled = false;
            CurOrder.OrderUser = null;
            bsOrderEntity.DataSource = OrderData;
            bsOrderEntity.DataMember = string.Empty;
            bsUserInfo.DataSource = UserInfo;
            ActiveControl = txtPhone;
            bsOrderEntity.ResetBindings(false);
            bsUserInfo.DataSource = null;
            bsUserInfo.ResetBindings(false);
            BindData( );
            orderContainer.SuspendLayout( );
            orderContainer.InvalidateLayout( );
            orderContainer.ResumeLayout(false);
            txtOrderSum.ValueObject = null;
            txtRateSale.ValueObject = null;
            txtPhone.Focus( );
        }
        void BindData( )
        {
            if (!AppContext.IsRuning)
                return;
            cbProduct.DataSource = OrderBLL.OrderProducts;
            //this.rowOrderItem.Visible = OrderBLL.OrderProducts.Count > 1;
            //cbMemberRate.Items.Clear( );
            //foreach (var item in OrderBLL.CompanyRates)
            //{
            //    cbMemberRate.Items.Add(item);
            //}
            //if (AppContext.Company.CompanyType.Value == 4)
            //{
            //    rowRateSale.Visible = false;
            //    rowPayment.Visible = false;
            //}
            SelectcbMemberRateItem(0);
            rowMemo.Visible = false;
            rowOrderItem.Visible = false;
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string phoneNum = txtPhone.Text.Trim( );
            lblPhoneInfo.Text = "";
            if (string.IsNullOrEmpty(phoneNum))
            {
                lblPhoneInfo.Text = "请输入会员账号";
                return;
            }
            string message;
            SysMemberCash cash;
            btnSave.Enabled = CurOrder.GetOrderUserByPhone(phoneNum, out cash, out message);
            plUserInfo.Text = message;
            bsUserInfo.DataSource = UserInfo;
            if (!btnSave.Enabled)
            {
                //bsOrderEntity.ResetBindings(false);
                MessageBoxEx.Show(message, "提示");
            }
            else
            {
                OrderData.PhoneNum = phoneNum;
                txtPhone.Text = phoneNum;
            }
            //plUserInfo.ForeColor = Color.Red;
            SetUserOrderData(cash);
        }
        void SetUserOrderData(SysMemberCash cash)
        {
            if (CurOrder.OrderUser != null)
            {
                //积分
                txtBalance.MaxValue = Formatter.ToInt(CurOrder.OrderUser.MemberBalance);
                txtBalance.Enabled = txtBalance.MaxValue > 0;

                //储值
                txtBalanceCash.MaxValue = Formatter.ToInt(CurOrder.OrderUser.MemberBalanceCash);
                txtBalanceCash.Enabled = txtBalanceCash.MaxValue > 0;
            }
            if (cash != null)
            {
                if (cash.CashRate.HasValue && cash.CashRate.Value > 0)
                {
                    txtRateSale.Value = Formatter.ToInt(cash.CashRate.Value * 100);
                    OrderData.OrderData.OrderRateSale = Formatter.ToInt(cash.CashRate.Value * 100M);
                }
                if (cash.CashRateSale.HasValue && cash.CashRateSale.Value > 0)
                {
                    int index = OrderBLL.CompanyRates.FindIndex(cash.CashRateSale.Value);
                    if (index < 0)
                        index = 0;
                    SelectcbMemberRateItem(index);
                }
            }
            lblMessage.Text = "";
            orderContainer.SuspendLayout( );
            orderContainer.InvalidateLayout( );
            orderContainer.ResumeLayout(false);
        }
        void SelectcbMemberRateItem(int index)
        {
            if (cbMemberRate.Items.Count > 0 && index > -1)
            {
                cbMemberRate.SetItemChecked(index, true);
                if (cbMemberRate.SelectedIndex != index)
                    cbMemberRate.SelectedItem = cbMemberRate.Items[index];
                UnSelectcbMemberRateItem(index);
            }
            else
                rowRate.Visible = false;
        }
        void UnSelectcbMemberRateItem(int curIdex)
        {
            for (int i = 0; i < cbMemberRate.CheckedIndices.Count; i++)
            {
                if (cbMemberRate.CheckedIndices[i] != curIdex)
                {
                    cbMemberRate.SetItemChecked(cbMemberRate.CheckedIndices[i], false);
                }
            }
        }
        private void cbMemberRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cbMemberRate.GetItemChecked(cbMemberRate.SelectedIndex))
                SelectcbMemberRateItem(cbMemberRate.SelectedIndex);
        }
        private void cbMemberRate_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UnSelectcbMemberRateItem(e.Index);
        }

        private void UcMemberOrder_Load(object sender, EventArgs e)
        {
            cbMemberRate.Items.Clear( );
            foreach (var item in OrderBLL.CompanyRates)
            {
                cbMemberRate.Items.Add(item);
            }
            InitOrder( );
        }
        public override void SetFoucs( )
        {
            InitOrder( );
        }
        public override void DownKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    TextBox txt = Utilities.GetFoucs( ) as TextBox;
                    if (txt != null && txt.Name == txtPhone.Name && !string.IsNullOrEmpty(txt.Text))
                    {
                        btnQuery_Click(null, null);
                    }
                    if (btnSave.Enabled)
                        SendKeys.Send("{Tab}");
                    break;
                case Keys.F5: //刷新
                    InitOrder( );
                    break;
                case Keys.F7:
                    btnQuery_Click(null, null);
                    break;
                case Keys.F8:
                    btnReadFinger_Click(sender, e);
                    break;
                case Keys.F9:
                    SendKeys.Send("{Tab}");
                    btnSave.Focus( );
                    btnSave_Click(sender, e);
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBalanceCash.Value > 0 || txtBalance.Value > 0)
            {
                if (string.IsNullOrEmpty(txtPwd.Text) && string.IsNullOrEmpty(OrderData.UserFinger))
                {
                    lblPassInfo.Text = "请输入会员密码";
                    goto lbl_pwd;
                }
                if (!string.IsNullOrEmpty(OrderData.UserFinger) &&
                   !FingerPrint.Finger.MatchFinger(OrderData.OrderUser.MemberFinger, OrderData.UserFinger))
                {
                    lblPassInfo.Text = "指纹验证不通过!";
                    goto lbl_pwd;
                }
            }
            if (!CheckPay( ))
            {
                lblMessage.Text = "消费支付金额不等于实际金额,请重新输入支付金额.";
                return;
            }
            if (txtOrderSum.Value <= 0)
            {
                lblMessage.Text = "结算金额不能小于或等于零";
                return;
            }
            CompanyRate rate;
            if (cbMemberRate.Items.Count > 0 && (rate = cbMemberRate.SelectedItem as CompanyRate) != null)
            {
                OrderData.OrderData.OrderRate = rate.Rate / 100M;
            }
            else
                OrderData.OrderData.OrderRate = 0M;
            bsOrderEntity.ResetBindings(false);

            //set pay data
            OrderData.OrderData.OrderSum = (decimal)txtOrderSum.Value;
            OrderData.OrderData.OrderSumOk = txtSumOk.Value;
            OrderData.OrderData.OrderRateSale = (decimal)txtRateSale.Value;
            OrderData.OrderData.OrderPay = (decimal)txtCash.Value;
            OrderData.OrderData.OrderPayCash = (decimal)txtBalanceCash.Value;
            OrderData.OrderData.OrderPayPoint = (decimal)txtBalance.Value;

            string message;
            int nRet = OrderBLL.SaveOrder(CurOrder.OrderUser, OrderData, out message);
            lblMessage.Text = message;
            if (nRet == 0)
            {
                InitOrder( );
                return;
            }
            else if (nRet == 2)
                goto lbl_pwd;
            txtPhone.Focus( );
            return;
        lbl_pwd:
            txtPwd.Focus( );
            return;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            InitOrder( );
        }

        private void btnReadFinger_Click(object sender, EventArgs e)
        {
            if (FingerPrint.Finger.IsBusy)
            {
                FingerPrint.Finger.CancelRead( );
                btnReadFinger.Text = "读取指纹";
                return;
            }
            if (!btnSave.Enabled)
            {
                lblMessage.Text = "请先输入会员账号...";
                return;
            }
            lblMessage.Text = "请把手指放在传感器上...";
            btnReadFinger.Text = "取消读取";
            FingerPrint.Finger.BeginRead((code, image, message, isSuccess) =>
                {
                    lblMessage.Text = message;
                    btnReadFinger.Text = "读取指纹";
                    if (isSuccess)
                    {
                        OrderData.UserFinger = code;
                    }
                });
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            string phNum = txtPhone.Text.Trim( );
            if (phNum.Length == 11 &&
                SubSonic.Sugar.Numbers.IsWholeNumber(phNum) &&
                !btnSave.Enabled)
            {
                btnQuery_Click(sender, e);
                if (btnSave.Enabled)
                    txtOrderSum.Focus( );
            }
            else if (phNum.Length < 11 || !SubSonic.Sugar.Numbers.IsWholeNumber(phNum))
                btnSave.Enabled = false;
        }
        bool isLock = false;
        void CalcSumOk( )
        {
            int nVer = txtSumOk.Value;
            if (nVer > 0 && OrderUser != null)
            {
                int userBalance = Formatter.ToInt(OrderUser.MemberBalance);
                int userBalanceCash = Formatter.ToInt(OrderUser.MemberBalanceCash);
                if (userBalance >= nVer)
                {
                    txtBalance.Value = nVer;
                    nVer = 0;
                }
                else
                {
                    txtBalance.Value = userBalance;
                    nVer = nVer - userBalance;
                }
                if (nVer > 0 && userBalanceCash > 0)
                {
                    if (userBalanceCash >= nVer)
                    {
                        txtBalanceCash.Value = nVer;
                        nVer = 0;
                    }
                    else
                    {
                        txtBalanceCash.Value = userBalanceCash;
                        nVer = nVer - userBalanceCash;
                    }
                }
                txtCash.Value = nVer;
            }
            else
            {
                txtCash.Value = 0;
                txtBalance.Value = 0;
                txtBalanceCash.Value = 0;
            }
        }
        bool CheckPay( )
        {
            return txtCash.Value + txtBalance.Value + txtBalanceCash.Value == txtSumOk.Value;
        }
        bool CheckUserBalanceCanPay( )
        {
            int userBalance = Formatter.ToInt(OrderUser.MemberBalance);
            int userBalanceCash = Formatter.ToInt(OrderUser.MemberBalanceCash);
            return txtSumOk.Value <= txtCash.Value + userBalance + userBalanceCash;
        }
        private void txtRateSale_ValueChanged(object sender, EventArgs e)
        {
            if (isLock) return;
            isLock = true;
            txtSumOk.Value = Convert.ToInt32(Math.Round((txtRateSale.Value > 0 && txtRateSale.Value < 100 ? (double)txtRateSale.Value : 100D) / 100D * txtOrderSum.Value));
            CalcSumOk( );
            isLock = false;
        }

        private void txtOrderSum_ValueChanged(object sender, EventArgs e)
        {
            if (isLock) return;
            isLock = true;
            Debug.WriteLine((txtRateSale.Value > 0 && txtRateSale.Value < 100 ? txtRateSale.Value : 100d) / 100d);
            txtSumOk.Value = Convert.ToInt32(Math.Round((txtRateSale.Value > 0 && txtRateSale.Value < 100 ? (double)txtRateSale.Value : 100D) / 100D * txtOrderSum.Value));
            CalcSumOk( );
            isLock = false;
        }

        private void txtCash_ValueChanged(object sender, EventArgs e)
        {

        }
        private void txtBalance_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtBalanceCash_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
