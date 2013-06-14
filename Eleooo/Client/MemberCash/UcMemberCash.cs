using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Eleooo.DAL;

namespace Eleooo.Client
{
    public partial class UcMemberCash : UserControlBase
    {
        private CashBLL _cashHelper;
        protected CashBLL cashHelper
        {
            get
            {
                if (_cashHelper == null)
                    _cashHelper = new CashBLL( );
                return _cashHelper;
            }
        }
        public CashEntity CashData
        {
            get
            {
                return cashHelper.CashData;
            }
        }
        public DataTable UserInfo
        {
            get
            {
                return cashHelper.UserInfo;
            }
        }
        public UcMemberCash( )
        {
            InitializeComponent( );
            bsCashEntity.DataSource = CashData;
            bsCashEntity.DataMember = null;
            bsUserInfo.DataSource = UserInfo;
            bsUserInfo.DataMember = null;
            if (AppContext.Company.CompanyType == 4)
            {
                rowPoint.Visible = false;
                rowRateSale.Visible = false;
            }
            else if (AppContext.Company.CompanyType == 2)
            {
                txtPoint.Enabled = false;
            }
            cbUserGrade.Items.AddRange(GradeBLL.GetGradeNames( ));
            cbMemberRate.Items.Clear( );
            foreach (var item in OrderBLL.CompanyRates)
            {
                cbMemberRate.Items.Add(item);
            }
        }
        public void InitCash( )
        {
            plUserInfo.Text = "";
            lblMessage.Text = "";
            cashHelper.InintCash( );
            SelectcbMemberRateItem(0);
            cbUserGrade.Text = "";
            bsCashEntity.ResetBindings(false );
            bsUserInfo.ResetBindings(false);
            btnSave.Enabled = false;
            plUserInfo.ForeColor = Color.Red;
            lblMessage.ForeColor = Color.Red;
            txtPhone.Focus( );
        }
        public override void SetFoucs( )
        {
            InitCash( );
        }
        void SelectcbMemberRateItem(int index)
        {
            if (cbMemberRate.Items.Count > 0 && index > -1)
            {
                cbMemberRate.SetItemChecked(index, true);
                cbMemberRate.SelectedItem = cbMemberRate.Items[index];
                UnSelectcbMemberRateItem(index);
            }
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
        private void cbMemberRate_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UnSelectcbMemberRateItem(e.Index);
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
                    InitCash( );
                    break;
                case Keys.F7:
                    btnQuery_Click(null, null);
                    break;
                case Keys.F9:
                    SendKeys.Send("{Tab}");
                    btnSave.Focus( );
                    btnSave_Click(sender, e);
                    break;
            }
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string phoNum = txtPhone.Text.Trim( );
            if (string.IsNullOrEmpty(phoNum))
            {
                lblPhoneInfo.Text = "请输入会员账号";
                return;
            }
            string message;
            btnSave.Enabled = cashHelper.GetCashUserByPhone(phoNum, out message);
            plUserInfo.Text = Utilities.StripHtml( message );
            bsUserInfo.DataSource = UserInfo;
            if (!btnSave.Enabled)
            {
                MessageBoxEx.Show(message, "提示");
                return;
            }
            SysMemberCash cash = cashHelper.GetUserLatestCash(out message);
            if (!string.IsNullOrEmpty(message))
            {
                btnSave.Enabled = false;
                MessageBoxEx.Show(message, "提示");
                return;
            }
            CashData.PhoneNum = phoNum;
            if (cash == null)
                return;
            if (cash.CashRateSale.HasValue && cash.CashRateSale.Value > 0)
            {
                int index = OrderBLL.CompanyRates.FindIndex(cash.CashRateSale.Value);
                if (index < 0)
                    index = 0;
                cbMemberRate.SelectedIndex = index;
            }
            if (cash.CashRate.HasValue && cash.CashRate.Value > 0)
            {
                CashData.CashRate = cash.CashRate.Value * 100M;
            }
            if (SubSonic.Sugar.Numbers.IsInteger(cash.CashMemo))
            {
                CashData.CashMemo = GradeBLL.GetNameByID(cash.CashMemo);
                cbUserGrade.Text = CashData.CashMemo;
            }
            else
                CashData.CashMemo = cash.CashMemo;
            bsCashEntity.ResetBindings(false);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            InitCash( );
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cashHelper.CashUser == null)
            {
                lblMessage.Text = "请先查找会员!";
                return;
            }
            if (CashData.CashSum <= 0)
            {
                lblMessage.Text = "充值金额不能小于或等于零";
                txtCashSum.Focus( );
                return;
            }
            bsCashEntity.ResetBindings(false);
            string message;
            CompanyRate rate = (cbMemberRate.SelectedItem as CompanyRate);
            CashData.CashPointRate = rate != null ? rate.Rate : 0;
            CashData.CashMemo = GradeBLL.GetIDByName(cbUserGrade.Text).ToString();
            int nRet = cashHelper.SaveCash(cashHelper.CashUser, CashData.CashData, out message);
            lblMessage.Text = message;
            MessageBoxEx.Show(message);
            if (nRet != 0)
            {
                txtPhone.Focus( );
            }
            else
                InitCash( );
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
                    txtCashSum.Focus( );
            }
            else if (phNum.Length < 11 || !SubSonic.Sugar.Numbers.IsWholeNumber(phNum))
                btnSave.Enabled = false;
        }

        private void cbMemberRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cbMemberRate.GetItemChecked(cbMemberRate.SelectedIndex))
                SelectcbMemberRateItem(cbMemberRate.SelectedIndex);
            //for (int i = 0; i < cbMemberRate.CheckedIndices.Count; i++)
            //{
            //    if (i == cbMemberRate.SelectedIndex)
            //    {
            //        cbMemberRate.SetItemChecked(cbMemberRate.CheckedIndices[i], true);
            //    }
            //    else
            //        cbMemberRate.SetItemChecked(cbMemberRate.CheckedIndices[i], false);
            //}
        }
    }
}
