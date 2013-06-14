using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Eleooo.DAL;
using DevComponents.DotNetBar;

namespace Eleooo.Client
{
    public partial class UcCompanyAds : UserControlBase
    {
        string picPath = string.Empty;
        string picPath1 = string.Empty;
        string picPath2 = string.Empty;
        string picPath3 = string.Empty;
        string picPath4 = string.Empty;
        DataTable _dtPointSetting;
        DataTable DtPointSetting
        {
            get
            {
                if (_dtPointSetting == null)
                {
                    _dtPointSetting = new DataTable("DtPointSetting");
                    DataColumn col1 = _dtPointSetting.Columns.Add( );
                    col1.ColumnName = SysCompanyAdsPointSetting.Columns.OrderSumLimit;
                    col1.Caption = "消费层次";
                    col1.DataType = SysCompanyAdsPointSetting.OrderSumLimitColumn.GetPropertyType( );
                    DataColumn col2 = _dtPointSetting.Columns.Add( );
                    col2.ColumnName = SysCompanyAdsPointSetting.Columns.AdsPoint;
                    col2.Caption = "奖励积分";
                    col2.DataType = SysCompanyAdsPointSetting.AdsPointColumn.GetPropertyType( );
                }
                return _dtPointSetting;
            }
        }
        public UcCompanyAds( )
        {
            InitializeComponent( );
            txtAreaDepth.IsMulitArea = true;
            ucPointSettings.DataSource = DtPointSetting;
            btnChangePic.CommandParameter = picCompanyAds;
            btnAddPic1.CommandParameter = pic1;
            btnAddPic2.CommandParameter = pic2;
            btnAddPic3.CommandParameter = pic3;
            btnAddPic4.CommandParameter = pic4;
        }
        public override void DownKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !btnSave.Focused)
                SendKeys.Send("{Tab}");
            else if (e.KeyCode == Keys.F9)
                btnSave_Click(sender, e);
            else if (e.KeyCode == Keys.F5)
                Init( );
        }
        public override void SetFoucs( )
        {
            Init( );
        }
        private int GetSexLimit( )
        {
            if (cbMan.Checked)
                return 1;
            else if (cbFemale.Checked)
                return 2;
            else
                return 0;
        }
        private int GetMemberLimit( )
        {
            if (cbAll.Checked)
                return 0;
            else if (cbOwner.Checked)
                return 1;
            else
                return 2;
        }
        private int GetAdsLimit( )
        {
            if (cbAOnce.Checked)
                return 1;
            else
                return 2;
        }
        private int GetRightAnswer( )
        {
            if (cbA.Checked)
                return 1;
            else if (cbB.Checked)
                return 2;
            else if (cbC.Checked)
                return 3;
            else
                return 4;
        }
        private void Init( )
        {
            lblMessage.Text = string.Empty;
            txtAdsTitle.Text = string.Empty;
            txtAreaDepth.AreaDepth = AppContext.Company.AreaDepth;
            if (picCompanyAds.Image != null)
                picCompanyAds.Image.Dispose( );
            picCompanyAds.Image = null;
            if (pic1.Image != null)
                pic1.Image.Dispose( );
            pic1.Image = null;
            if (pic2.Image != null)
                pic2.Image.Dispose( );
            pic2.Image = null;
            if (pic3.Image != null)
                pic3.Image.Dispose( );
            pic3.Image = null;
            if (pic4.Image != null)
                pic4.Image.Dispose( );
            pic4.Image = null;
            picPath = string.Empty;
            picPath1 = string.Empty;
            picPath2 = string.Empty;
            picPath3 = string.Empty;
            picPath4 = string.Empty;
            txtQuestion.Text = string.Empty;
            txtAnswerA.Text = string.Empty;
            txtAnswerB.Text = string.Empty;
            txtAnswerC.Text = string.Empty;
            txtAnswerD.Text = string.Empty;
            txtDayLimitAmount.ValueObject = null;
            txtDayLimitSum.ValueObject = null;
            txtDate.Value = DateTime.Today;
            txtEndDate.Value = DateTime.Today.AddDays(7).Date;
            DtPointSetting.Rows.Clear( );
            txtAreaDepth.ClearItem( );
        }
        bool ValidateField( )
        {
            if (AppContext.Company.CompanyType.HasValue && AppContext.Company.CompanyType.Value ==4)
            {
                lblMessage.Text = "您暂无权限使用该功能";
                return false;
            }
            if (txtDate.ValueObject == null)
            {
                lblMessage.Text = "请选择开始日期";
                txtDate.Focus( );
                return false;
            }
            if (txtEndDate.ValueObject == null)
            {
                lblMessage.Text = "请选择结束日期";
                txtEndDate.Focus( );
                return false;
            }
            if (txtEndDate.Value < txtDate.Value)
            {
                lblMessage.Text = "结束日期不能小于当前日期";
                txtEndDate.Focus( );
                return false;
            }
            if (string.IsNullOrEmpty(txtAdsTitle.Text.Trim( )))
            {
                lblMessage.Text = "请输入广告信息";
                txtAdsTitle.Focus( );
                return false;
            }
            if (string.IsNullOrEmpty(txtAreaDepth.MulitAreaDepth))
            {
                lblMessage.Text = "请选择覆盖商圈";
                txtAreaDepth.Focus( );
                return false;
            }
            if (txtDayLimitSum.Value == 0)
            {
                lblMessage.Text = "请输入日最高投放额";
                txtDayLimitSum.Focus( );
                return false;
            }
            #region point setting
            if (DtPointSetting.Rows.Count == 0)
            {
                lblMessage.Text = "请输入奖励设置";
                ucPointSettings.Focus( );
                return false;
            }
            foreach (DataRow row in DtPointSetting.Rows)
            {
                if (row[0] == DBNull.Value || row[0] == null)
                {
                    lblMessage.Text = "奖励设置的消费层次不能为空!";
                    ucPointSettings.Focus( );
                    return false;
                }
                //decimal d = Utilities.ChangeType<decimal>(row[0]);
                //if (d < 0)
                //{
                //    lblMessage.Text = "奖励设置的消费层次不能小于零!";
                //    ucPointSettings.Focus( );
                //    return false;
                //}
                if (row[1] == DBNull.Value || row[1] == null)
                {
                    lblMessage.Text = "奖励设置的奖励积分不能为空!";
                    ucPointSettings.Focus( );
                    return false;
                }
                var n = Formatter.ToDecimal(row[1]);
                if (n <= 0)
                {
                    lblMessage.Text = "奖励设置的奖励积分必须大于零!";
                    ucPointSettings.Focus( );
                    return false;
                }
            }
            #endregion
            return true;
        }

        SysCompanyAd GetNewCompanyAds( )
        {
            SysCompanyAd ad = new SysCompanyAd
            {
                AdsClicked = 0,
                AdsCompanyID = AppContext.Company.Id,
                AdsDate = txtDate.Value,
                AdsEndDate = txtEndDate.Value,
                AdsPic = string.Empty,
                AdsTitle = txtAdsTitle.Text.Trim( ),
                AreaDepth = txtAreaDepth.MulitAreaDepth,
                IsDeleted = false,
                AdsPointSum = 0,
                SexLimit = GetSexLimit( ),
                AdsMemberLimit = GetMemberLimit( ),
                AdsClickLimit = GetAdsLimit( ),
                AdsDayLimitAmount = txtDayLimitAmount.Value,
                AdsDayLimitSum = Formatter.ToDecimal(txtDayLimitSum.Value),
                AdsQuestion = txtQuestion.Text.Trim( ),
                AdsAnswer1 = txtAnswerA.Text.Trim( ),
                AdsAnswer2 = txtAnswerB.Text.Trim( ),
                AdsAnswer3 = txtAnswerC.Text.Trim( ),
                AdsAnswer4 = txtAnswerD.Text.Trim( ),
                AdsRightAnswer = GetRightAnswer( ),
                IsPass = false
            };
            return ad;
        }
        private void SaveAdsPics(int adsID)
        {
            if (adsID <= 0)
                return;
            int saveType = 6, fileType = 1;
            string path,message;
            string folderName = adsID.ToString();
            if (!string.IsNullOrEmpty(picPath1))
                ServiceProvider.Service.UploadFile(picPath1, saveType, fileType, folderName, out path, out message);
            if (!string.IsNullOrEmpty(picPath2))
                ServiceProvider.Service.UploadFile(picPath2, saveType, fileType, folderName, out path, out message);
            if (!string.IsNullOrEmpty(picPath3))
                ServiceProvider.Service.UploadFile(picPath3, saveType, fileType, folderName, out path, out message);
            if (!string.IsNullOrEmpty(picPath4))
                ServiceProvider.Service.UploadFile(picPath4, saveType, fileType, folderName, out path, out message);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string message;
            if (!ValidateField( ))
                return;
            try
            {
                int adsID;
                bool b = ServiceProvider.Service.SaveCompanyAd(GetNewCompanyAds( ), DtPointSetting, picPath,out adsID, out message);
                if (b)
                {
                    SaveAdsPics(adsID);
                    MessageBoxEx.Show("保存成功!");
                    Init( );
                }
                else
                    MessageBoxEx.Show(message);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
                lblMessage.Text = ex.Message;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Init( );
        }
        private void btnChangePic_Click(object sender, EventArgs e)
        {
            if (dlgCompanyAdsPic.ShowDialog( ) == DialogResult.OK)
            {
                try
                {
                    ButtonX btn = sender as ButtonX;
                    PictureBox picBox = btn.CommandParameter as PictureBox;
                    picBox.Load(dlgCompanyAdsPic.FileName);
                    if (btn == btnChangePic)
                        picPath = dlgCompanyAdsPic.FileName;
                    else if (btn == btnAddPic1)
                        picPath1 = dlgCompanyAdsPic.FileName;
                    else if (btn == btnAddPic2)
                        picPath2 = dlgCompanyAdsPic.FileName;
                    else if (btn == btnAddPic3)
                        picPath3 = dlgCompanyAdsPic.FileName;
                    else if (btn == btnAddPic4)
                        picPath4 = dlgCompanyAdsPic.FileName;
                }
                catch { }
            }
        }

    }
}
