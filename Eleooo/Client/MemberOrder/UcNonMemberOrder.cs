using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Eleooo.Client
{
    public partial class UcNonMemberOrder : UserControlBase
    {
        private OrderEntity _orderData;
        public OrderEntity OrderData
        {
            get
            {
                if (_orderData == null)
                {
                    _orderData = new OrderEntity { OrderUser = new DAL.SysMember() };
                    AppContext.User.CopyTo(_orderData.OrderUser);
                    _orderData.OrderUser.MemberBalance = 0;
                    _orderData.OrderUser.MemberBalanceCash = 0;
                }
                return _orderData;
            }
        }
        public UcNonMemberOrder( )
        {
            InitializeComponent( );
            rowMemo.Visible = false;
        }
        public void InitOrder( )
        {
            if (DesignMode)
                return;
            OrderData.InitOrder( );
            bsOrderEntity.DataMember = null;
            bsOrderEntity.DataSource = OrderData;
            bsOrderEntity.ResetBindings(false);
            txtOrderSum.Focus( );
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string message;
            OrderData.UserPwd = OrderData.OrderUser.MemberPwd;
            int nRet = OrderBLL.SaveOrder(OrderData.OrderUser, OrderData, out message);
            lblMessage.Text = message;
            if (nRet == 0)
            {
                InitOrder( );
            }
            else
                txtOrderSum.Focus( );
        }
        public override void DownKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    lblMessage.Text = "";
                    SendKeys.Send("{Tab}");
                    break;
                case Keys.F5: //刷新
                    InitOrder( );
                    break;
                case Keys.F9:
                    SendKeys.Send("{Tab}");
                    btnSave.Focus( );
                    btnSave_Click(sender, e);
                    break;
            }
        }
        public override void SetFoucs( )
        {
            InitOrder( );
        }

        private void UcNonMemberOrder_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            InitOrder( );
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            InitOrder( );
        }
    }
}
