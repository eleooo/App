using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;

namespace Eleooo.Client
{
    public partial class UcCompanyItemOrder : UserControlBase
    {
        public UcCompanyItemOrder( )
        {
            InitializeComponent( );
            GridColumn col = gridItem.PrimaryGrid.Columns["Action"];
            if (col != null)
            {
                //col.EditorType = typeof(CompanyItemOrderButton);
                (col.EditControl as CompanyItemOrderButton).OnOrderCompanyItem += new OrderCompanyItemHandler(UcCompanyItemOrder_OnOrderCompanyItem);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            buttonX1_Click(this, e);
        }
        void UcCompanyItemOrder_OnOrderCompanyItem(int itemID)
        {
            string message;
            if (ServiceProvider.Service.OrderCompanyItem(AppContext.Company.Id, itemID, txtPwd.Text, txtFinger.Text, out message))
            {
                btnQuery_Click(btnQuery, EventArgs.Empty);
                lblFingerInfo.Text = "结算成功";
            }
            else
                lblFingerInfo.Text = message;
        }

        private void btnReadFinger_Click(object sender, EventArgs e)
        {
            if (pbFinger.Image != null)
                pbFinger.Image.Dispose( );
            pbFinger.Image = null;
            pbFinger.Update( );
            if (FingerPrint.Finger.IsBusy)
            {
                FingerPrint.Finger.CancelRead( );
                btnReadFinger.Text = "读取指纹";
                return;
            }
            lblFingerInfo.Text = "请把手指放在传感器上...";
            btnReadFinger.Text = "取消读取";
            FingerPrint.Finger.BeginRead((code, image, message, isSuccess) =>
            {
                lblFingerInfo.Text = message;
                btnReadFinger.Text = "读取指纹";
                if (isSuccess)
                {
                    pbFinger.Image = Bitmap.FromFile(image);
                    lblFingerInfo.Text = string.Format("{0},指纹特征码是:\n\r{1}", message, code);
                    txtFinger.Text = code;
                }
            });
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhoneNum.Text))
            {
                lblFingerInfo.Text = "请输入会员账号";
                txtPhoneNum.Focus( );
                return;
            }
            if (string.IsNullOrEmpty(txtPwd.Text) && string.IsNullOrEmpty(txtFinger.Text))
            {
                lblFingerInfo.Text = "请输入会员登录密码或指纹";
                txtPwd.Focus( );
                return;
            }
            string message;
            int flag;
            DataTable dt = ServiceProvider.Service.GetUserCompanyItems(txtPhoneNum.Text, txtPwd.Text, txtFinger.Text, AppContext.Company.Id, out flag, out message);
            if (flag == 1)
                txtPhoneNum.Focus( );
            else if (flag == 2)
                txtPwd.Focus( );
            if (string.IsNullOrEmpty(message) && dt.Rows.Count == 0)
                lblFingerInfo.Text = "此会员没有可结算的项目!";
            else
                lblFingerInfo.Text = message;
            gridItem.PrimaryGrid.DataSource = dt;
        }
        public override void SetFoucs( )
        {
            buttonX1_Click(null, null);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            gridItem.PrimaryGrid.DataSource = null;
            txtPwd.Text = string.Empty;
            txtFinger.Text = string.Empty;
            txtPhoneNum.Text = string.Empty;
            if (pbFinger.Image != null)
                pbFinger.Image.Dispose( );
            pbFinger.Image = null;
            pbFinger.Update( );
            txtPhoneNum.Focus( );
        }
        public override void DownKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    ButtonX btn = Utilities.GetFoucs( ) as ButtonX;
                    if (btn != null)
                        btn.PerformClick( );
                    else
                        SendKeys.Send("{Tab}");
                    break;
                case Keys.F5:
                    buttonX1_Click(sender, null);
                    break;
                case Keys.F7:
                    btnQuery_Click(sender, null);
                    break;
                case Keys.F8:
                    btnReadFinger_Click(sender, null);
                    break;
            }
        }
    }
}
