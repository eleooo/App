using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Eleooo.DAL;
using System.IO;
using DevComponents.DotNetBar;

namespace Eleooo.Client
{
    public partial class UcCompanyInfo : UserControlBase
    {

        private string NewPicPath { get; set; }
        public SysCompany Company
        {
            get
            {
                return AppContext.Company;
            }
        }
        public UcCompanyInfo( )
        {
            InitializeComponent( );
            if (AppContext.IsRuning)
            {
                bsCompany.DataMember = null;
                bsCompany.DataSource = Company;
            }
        }
        private void btnChangePic_Click(object sender, EventArgs e)
        {
            if (dlgCompanyPic.ShowDialog( ) == DialogResult.OK)
            {
                try
                {
                    picCompany.Load(dlgCompanyPic.FileName);
                    NewPicPath = dlgCompanyPic.FileName;
                }
                catch { }
            }
        }
        public override void SetFoucs( )
        {

            if (Company.DirtyColumns.Count > 0)
            {
                Company.MarkClean( );
                Company.MarkOld( );
            }
            NewPicPath = string.Empty;
            if (PageManager.Home.picCompany.Image != null)
                picCompany.Image = (Image)PageManager.Home.picCompany.Image.Clone( );

            bsCompany.ResetBindings(true);
            txtPhone.Focus( );
        }
        public override void DownKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !btnSave.Focused)
                SendKeys.Send("{Tab}");
            else if (e.KeyCode == Keys.F9)
                btnSave_Click(sender, e);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string picFile = string.Empty;
            string message;
            int saveType = 1, fileType = 1;
            if (!string.IsNullOrEmpty(NewPicPath) &&
                !ServiceProvider.Service.UploadFile(NewPicPath, saveType, fileType, out picFile, out message))
            {
                MessageBoxEx.Show(message, "保存失败");
                return;
            }
            if (!string.IsNullOrEmpty(picFile))
                Company.CompanyPhoto = picFile;
            if (Company.DirtyColumns.Count > 0)
            {
                Company.IsNew = false;
                //if (AppContext.User.DirtyColumns.Count > 0)
                //    AppContext.User.MarkClean( );
                AppContext.User.MarkOld( );
                AppContext.User.MemberAddress1 = Company.CompanyLocation;
                AppContext.User.MemberEmail = Company.CompanyEmail;
                if (ServiceProvider.Service.SaveEntity<SysCompany>(Company) > 0 &&
                    ServiceProvider.Service.SaveEntity<SysMember>(AppContext.User) > 0)
                {
                    MessageBoxEx.Show("保存成功");
                    if (!string.IsNullOrEmpty(picFile))
                        PageManager.Home.picCompany.Load(NewPicPath);
                }
                else
                {
                    MessageBoxEx.Show("保存失败");
                }
            }
            else
                MessageBoxEx.Show("没有可保存的数据!");
        }
    }
}
