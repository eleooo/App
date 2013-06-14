using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Eleooo.Client
{
    public partial class UcOrder : UserControlBase
    {
        public UcOrder( )
        {
            InitializeComponent( );
            this.Dock = DockStyle.Fill;
        }

        private void UcOrder_Load(object sender, EventArgs e)
        {
            //NonMemberOrder.Visible = false;
        }

        private void tabOrder_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
        {
            SetFoucs( );
        }

        public override void SetFoucs( )
        {
            UserControlBase c = tabOrder.SelectedPanel.Controls[0] as UserControlBase;
            if (c != null)
                c.SetFoucs( );
        }
        public override void DownKey(object sender, KeyEventArgs e)
        {
            if (e.KeyValue > 48 && e.KeyValue < 52 && e.Control)
            {
                tabOrder.SelectedTabIndex = e.KeyValue - 49;
            }
            else
            {
                UserControlBase c = tabOrder.SelectedPanel.Controls[0] as UserControlBase;
                if (c != null)
                    c.DownKey(sender, e);
            }
        }
    }
}
