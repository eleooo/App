using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Eleooo.DAL;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace Eleooo.Client
{
    public partial class UcCompanyItem : UserControlBase
    {
        string picPath = string.Empty;
        string picPath1 = string.Empty;
        string picPath2 = string.Empty;
        public UcCompanyItem( )
        {
            InitializeComponent( );
            txtAreaDepth.IsMulitArea = true;
        }

        SysCompanyItem GetNewCompanyItem(string pic, string pic1, string pic2)
        {
            SysCompanyItem item = new SysCompanyItem
            {
                AreaDepth = txtAreaDepth.MulitAreaDepth,
                CompanyID = AppContext.Company.Id,
                ItemAddr = AppContext.Company.CompanyAddress,
                ItemAmount = txtItemAmount.Value,
                ItemClicked = 0,
                ItemDate = txtItemDate.Value,
                ItemEndDate = txtEndDate.Value,
                ItemLimit = cbAOnce.Checked ? 1 : 2,
                ItemPic = pic,
                ItemPic1 = pic1,
                ItemPic2 = pic2,
                ItemPoint = Convert.ToDecimal(txtPoint.Value),
                ItemSum = Convert.ToDecimal(txtItemSum.Value),
                ItemTitle = txtItemTitle.Text.Trim( ),
                ItemContent = txtItemContent.Text.Trim( ),
                ItemIntro = txtItemIntro.Text.Trim( ),
                ItemTips = txtItemTips.Text.Trim( ),
                ItemUsed = 0,
                IsDeleted = false,
                MemberLimit = GetMemberLimit( ),
                OrderSumLimit = Convert.ToDecimal(txtOrderSumLimit.Value),
                IsPass = false,
                IsCanDel = cbCanDel.Checked ? 1 : 0
            };
            return item;
        }
        public override void DownKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !btnSave.Focused)
            {
                TextBoxX txt = Utilities.GetFoucs( ) as TextBoxX;
                if (txt != null && txt.Multiline)
                    return;
                SendKeys.SendWait("{Tab}");
            }
            else if (e.KeyCode == Keys.F9)
                btnSave_Click(sender, e);
            else if (e.KeyCode == Keys.F5)
                Init( );
        }
        public override void SetFoucs( )
        {
            Init( );
        }
        private int GetMemberLimit( )
        {
            if (cbOwner.Checked)
                return 1;
            else if (cbOuter.Checked)
                return 2;
            else
                return 0;
        }
        private void Init( )
        {
            lblMessage.Text = string.Empty;
            txtItemTitle.Text = string.Empty;
            txtItemDate.Value = DateTime.Today;
            txtItemAmount.ValueObject = null;
            txtItemContent.Text = string.Empty;
            txtItemSum.ValueObject = null;
            txtOrderSumLimit.ValueObject = null;
            txtAreaDepth.AreaDepth = AppContext.Company.AreaDepth;
            txtItemTips.Text = string.Empty;
            txtItemIntro.Text = string.Empty;
            txtPoint.ValueObject = null;
            if (picCompanyItem.Image != null)
                picCompanyItem.Image.Dispose( );
            if (pic1.Image != null)
                pic1.Image.Dispose( );
            if (pic2.Image != null)
                pic2.Image.Dispose( );
            picCompanyItem.Image = null;
            pic1.Image = null;
            pic2.Image = null;
            picPath = picPath1 = picPath2 = string.Empty;
            txtEndDate.Value = DateTime.Today.AddDays(6).Date;
            cbCanDel.Checked = false;
            cbAOnce.Checked = true;
            cbAll.Checked = true;
            txtAreaDepth.ClearItem( );
            ItemTab.SelectedTab = mainTabItem;
        }
        bool ValidateField( )
        {
            if (AppContext.Company.CompanyType.HasValue && AppContext.Company.CompanyType.Value != 1 &&
                AppContext.Company.CompanyType.Value != 2)
            {
                lblMessage.Text = "您暂无权限使用该功能";
                return false;
            }
            if (txtPoint.ValueObject == null || txtPoint.Value <= 0)
            {
                lblMessage.Text = "请输入兑换积分";
                txtPoint.Focus( );
                return false;
            }
            if (txtItemDate.ValueObject == null)
            {
                lblMessage.Text = "请选择优惠开始日期";
                txtItemDate.Focus( );
                return false;
            }
            if (txtEndDate.ValueObject == null)
            {
                lblMessage.Text = "请选择优惠结束日期";
                txtEndDate.Focus( );
                return false;
            }
            if (txtItemDate.Value > txtEndDate.Value)
            {
                lblMessage.Text = "优惠开始日期不能大于结束日期";
                txtEndDate.Focus( );
                return false;
            }
            if (string.IsNullOrEmpty(txtItemTitle.Text.Trim( )))
            {
                lblMessage.Text = "请输入优惠信息";
                txtItemTitle.Focus( );
                return false;
            }
            if (txtItemAmount.Value <= 0)
            {
                lblMessage.Text = "请输入优惠数量";
                txtItemAmount.Focus( );
                return false;
            }
            if (txtItemSum.Value <= 0)
            {
                lblMessage.Text = "请输入兑换额度";
                txtItemSum.Focus( );
                return false;
            }
            if (string.IsNullOrEmpty(txtAreaDepth.MulitAreaDepth))
            {
                lblMessage.Text = "请选择覆盖商圈";
                txtAreaDepth.Focus( );
                return false;
            }
            return true;
        }

        private void btnChangePic_Click(object sender, EventArgs e)
        {
            if (dlgCompanyItemPic.ShowDialog( ) == DialogResult.OK)
            {
                try
                {
                    picCompanyItem.Load(dlgCompanyItemPic.FileName);
                    picPath = dlgCompanyItemPic.FileName;
                }
                catch { }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Init( );
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string picFile = string.Empty, picFile1 = string.Empty, picFile2 = string.Empty;
            string message;
            int saveType = 5, fileType = 1;
            if (!ValidateField( ))
                return;
            if (!string.IsNullOrEmpty(picPath) &&
                !ServiceProvider.Service.UploadFile(picPath, saveType, fileType, out picFile, out message))
            {
                MessageBoxEx.Show(message, "保存失败");
                return;
            }
            if (!string.IsNullOrEmpty(picPath1) &&
                !ServiceProvider.Service.UploadFile(picPath1, saveType, fileType, out picFile1, out message))
            {
                MessageBoxEx.Show(message, "保存失败");
                return;
            }
            if (!string.IsNullOrEmpty(picPath2) &&
                !ServiceProvider.Service.UploadFile(picPath2, saveType, fileType, out picFile2, out message))
            {
                MessageBoxEx.Show(message, "保存失败");
                return;
            }
            try
            {
                SysCompanyItem item = GetNewCompanyItem(picFile, picFile1, picFile2);
                if (ServiceProvider.Service.SaveEntity<SysCompanyItem>(item) > 0)
                {
                    MessageBoxEx.Show("保存成功!");
                    Init( );
                }
                else
                {
                    MessageBoxEx.Show("保存失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
                lblMessage.Text = ex.Message;
            }
        }

        private void btnAddPic1_Click(object sender, EventArgs e)
        {
            if (dlgCompanyItemPic.ShowDialog( ) == DialogResult.OK)
            {
                try
                {
                    pic1.Load(dlgCompanyItemPic.FileName);
                    picPath1 = dlgCompanyItemPic.FileName;
                }
                catch { }
            }
        }

        private void btnAddPic2_Click(object sender, EventArgs e)
        {
            if (dlgCompanyItemPic.ShowDialog( ) == DialogResult.OK)
            {
                try
                {
                    pic2.Load(dlgCompanyItemPic.FileName);
                    picPath2 = dlgCompanyItemPic.FileName;
                }
                catch { }
            }
        }
    }
}
