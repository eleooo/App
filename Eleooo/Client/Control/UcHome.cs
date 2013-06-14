using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Eleooo.Client
{
    public partial class UcHome : UserControlBase
    {
        bool isFoucsRefresh = false;
        public UcHome( )
        {
            InitializeComponent( );
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            tileOrder.Command = Commands.CmdShowPanel;
            tileOrder.CommandParameter = PageManager.MemberOrder;

            tileCash.Command = Commands.CmdShowPanel;
            tileCash.CommandParameter = PageManager.MemberCash;

            tileMember.Command = Commands.CmdShowPanel;
            tileMember.CommandParameter = PageManager.MemberReg;

            tileSystem.Command = Commands.CmdShowPanel;
            tileSystem.CommandParameter = PageManager.SystemCfg;

            tileCompanyItem.Command = Commands.CmdShowPanel;
            tileCompanyItem.CommandParameter = PageManager.CompanyItem;


            tileCompanyAds.Command = Commands.CmdShowPanel;
            tileCompanyAds.CommandParameter = PageManager.CompanyAds;

            tileClose.Command = Commands.CmdCloseApp;
            

            titleEleooo.Command = Commands.CmdEleooo;

            Bounds = PageManager.GetHomeWorkSpace( );
        }
        public override void SetFoucs( )
        {
            //cpMask.
            bool isGetPic = picCompany.Image == null;
            CompanyInfoAdapter.Instance.Start(isGetPic, GetCompanyInfo);
        }
        void GetCompanyInfo(params object[] result)
        {
            if (isFoucsRefresh)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("刷新成功!");
                isFoucsRefresh = false;
            }
            if (result == null || result.Length == 0)
                return;
            if (result[0] != null)
            {
                DataTable dt = (DataTable)result[0];
                this.gridCompanyInfo.PrimaryGrid.DataSource = dt;
                this.gridCompanyInfo.PrimaryGrid.DataMember = dt.TableName;
            }
            if (result.Length >= 2 && result[1] != null)
            {
                byte[] picData = (byte[])result[1];
                if (picData.Length > 0)
                {
                    using (Stream stream = new MemoryStream( ))
                    {
                        stream.Position = 0L;
                        stream.Write(picData, 0, picData.Length);
                        picCompany.Image = Image.FromStream(stream);
                        //PageManager.SystemCfg.ucCompanyInfo1.p
                    }
                }
            }

        }

        private void tileRefresh_Click(object sender, EventArgs e)
        {
            isFoucsRefresh = true;
            FlushForm.Begin( );
            CompanyInfoAdapter.Instance.Start(true, GetCompanyInfo);
        }
    }
}
