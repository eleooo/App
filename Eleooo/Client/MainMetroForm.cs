using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro.ColorTables;
using DevComponents.DotNetBar;

namespace Eleooo.Client
{
    public partial class MainMetroForm : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        public const DevComponents.DotNetBar.Controls.eSlideSide FROM_SLIDESIDE = DevComponents.DotNetBar.Controls.eSlideSide.Left;
        const int WM_SYSCOMMAND = 0x112;
        const int SC_CLOSE = 0xF060;
        const int SC_MINIMIZE = 0xF020;
        const int SC_MAXIMIZE = 0xF030;

        private static MainMetroForm _instance;
        public static MainMetroForm Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainMetroForm( );
                return _instance;
            }
        }
        private int _oldHeight = 0;
        public UserControlBase ActivateChild { get; set; }
        public MainMetroForm( )
        {
            Icon = Eleooo.Client.Properties.Resources.Eleooo;
            InitializeComponent( );
        }

        private void Init( )
        {
            btnTheme.BuildSubItem(MetroColorGeneratorParameters.GetAllPredefinedThemes( ), "ThemeName", Commands.CmdThemes);
            Controls.Add(PageManager.Home);
            PageManager.Home.SetFoucs( );
#if DEBUG
            metroTitle.SettingsButtonVisible = true;
#endif

        }
        private void metroTitle_HelpButtonClick(object sender, EventArgs e)
        {
            if (metroTitle.HelpButtonText == "收起")
            {
                metroTitle.HelpButtonText = "展开";
                _oldHeight = this.Height;
                this.Height = metroTitle.Height;
            }
            else
            {
                metroTitle.HelpButtonText = "收起";
                this.Height = _oldHeight;
            }

        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32( ) == SC_MAXIMIZE)
                metroTitle.HelpButtonVisible = false;
            else if (!metroTitle.HelpButtonVisible)
                metroTitle.HelpButtonVisible = WindowState != FormWindowState.Maximized; ;
        }

        private void MainMetroForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (ActivateChild != null)
                ActivateChild.DownKey(sender, e);
        }

        private void metroTitle_SettingsButtonClick(object sender, EventArgs e)
        {
            //PageManager.Home.IsOpen = !PageManager.Home.IsOpen;
            //if (ActivateChild != null)
            //    MessageBoxEx.Show(ActivateChild.Bounds.ToString( ));
            //MessageBoxEx.Show(PageManager.Home.Bounds.ToString( ));
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Init( );
        }
        public Rectangle GetHomeWorkSpace( )
        {
            Rectangle bounds = this.Bounds;
            bounds.X = 0;
            bounds.Y = metroTitle.Height;
            bounds.Height = Height - metroTitle.Height - 2;
            bounds.Width = Width - 2;
            return bounds;
        }
        public Rectangle GetWorkSpace( )
        {
            Rectangle bounds = this.Bounds;
            bounds.X = 0;
            bounds.Y = 0;
            bounds.Height = Height - metroTitle.Height - 2;
            bounds.Width = Width - 2;
            return bounds;
        }

        private void MainMetroForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBoxEx.Show("是否要退出程序?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                e.Cancel = true;
        }
    }
}