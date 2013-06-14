using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Eleooo.Client
{
    public partial class UcMemberReg : UserControlBase
    {
        private static readonly List<string> _PhoneBeginWith = new List<string> { "13", "15", "18" };
        private UserEntity _userData;
        public UserEntity UserData
        {
            get
            {
                if (_userData == null)
                    _userData = new UserEntity( );
                return _userData;
            }
        }
        public UcMemberReg( )
        {
            InitializeComponent( );
            //grade
            cbUserGrade.Items.AddRange(GradeBLL.GetGradeNames( ));
            bsUserEntity.DataSource = UserData;
            bsUserEntity.DataMember = null;
            rowUserGrade.Visible = false;
        }
        public void InitUserData( )
        {
            UserData.InitUserData( );
            cbSex.SetItemChecked(0, true);
            cbSex.SelectedItem = cbSex.Items[0];
            cbUserGrade.SelectedItem = cbUserGrade.Items[0];
            lblMessage.Text = "";
            lblFingerInfo.Text = "";
            btnSave.Enabled = false;
            bsUserEntity.DataSource = UserData;
            bsUserEntity.ResetBindings(false);
            memberContainer.SuspendLayout( );
            memberContainer.InvalidateLayout( );
            memberContainer.ResumeLayout(false);
            lblMessage.ForeColor = Color.Red;
            txtPhone.Focus( );
        }
        private void cbSex_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < cbSex.CheckedIndices.Count; i++)
            {
                if (cbSex.CheckedIndices[i] != e.Index)
                {
                    cbSex.SetItemChecked(cbSex.CheckedIndices[i], false);
                }
            }
        }

        public override void SetFoucs( )
        {
            //InitUserData( );
        }

        public override void DownKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    TextBox txt = Utilities.GetFoucs( ) as TextBox;
                    if (txt != null && txt.Name == txtPhone.Name)
                    {
                        btnQuery_Click(null, null);
                    }
                    if (btnSave.Enabled)
                        SendKeys.Send("{Tab}");
                    break;
                case Keys.F5: //刷新
                    InitUserData( );
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
            string message;
            UserData.MemberPhoneNumber = txtPhone.Text;
            UserData.MemberBirthday = txtBirthday.Value;
            int nRet = UserBLL.SaveUser(UserData, out message);
            lblMessage.Text = message;
            MessageBoxEx.Show(message);
            if (nRet == -1)
            {
                txtPhone.Focus( );
                return;
            }
            else if (nRet == 1)
            {
                txtPwd.Focus( );
                return;
            }
            else if (nRet == 2)
            {
                txtPwdConfirm.Focus( );
                return;
            }
            else if (nRet == -3)
            {
                txtEmail.Focus( );
                return;
            }
            else if (nRet == 0)
            {
                InitUserData( );
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            InitUserData( );
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
            if (!btnSave.Enabled)
            {
                lblMessage.Text = "请先输入会员账号...";
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
                        UserData.UserFinger = code;
                    }
                });

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            txtPwdConfirm.Enabled = false;
            string phoneNum = txtPhone.Text.Trim( );
            if (!string.IsNullOrEmpty(phoneNum))
            {
                if (!Utilities.IsNumeric(phoneNum))
                    message = "账号格式不正确!";
                else if (phoneNum.Length != 11)
                    message = "长度必须为11位手机号码";
                else if (!_PhoneBeginWith.Contains(phoneNum.Substring(0, 2)))
                    message = "手机号必须以13,15,18开头";
                else
                {
                    bool bRet = UserBLL.GetUserByPhone(phoneNum, UserData, out message);
                    btnSave.Enabled = bRet;
                    txtPwdConfirm.Enabled = UserData.UserData.IsNew;
                    if (bRet)
                        UserData.MemberPhoneNumber = phoneNum;
                    //bsUserEntity.DataSource = UserData;
                    bsUserEntity.ResetBindings(false);
                    cbSex.SetItemChecked(cbSex.Items.IndexOf(UserData.MemberSex), btnSave.Enabled);
                    //lblPhoneInfo.Text = "账号可用";
                }
            }
            else
                message = "请输入会员账号!";

            lblMessage.Text = message;
            if (!btnSave.Enabled)
            {
                bsUserEntity.ResetBindings(false);
            }
            //memberContainer.SuspendLayout( );
            //memberContainer.InvalidateLayout( );
            //memberContainer.ResumeLayout(false);
        }

        private void UcMemberReg_Load(object sender, EventArgs e)
        {
            InitUserData( );
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
                    txtName.Focus( );
            }
            else if (phNum.Length < 11 || !SubSonic.Sugar.Numbers.IsWholeNumber(phNum))
                btnSave.Enabled = false;
        }

        private void cbSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cbSex.GetItemChecked(cbSex.SelectedIndex))
            {
                cbSex.SetItemChecked(cbSex.SelectedIndex, true);
                for(int i = 0;i<cbSex.CheckedIndices.Count - 1;i++)
                    if(i != cbSex.SelectedIndex)
                        cbSex.SetItemChecked(i, false);
            }
        }

    }
}
