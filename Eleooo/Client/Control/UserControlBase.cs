using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Eleooo.Client
{
    public partial class UserControlBase : DevComponents.DotNetBar.Controls.SlidePanel
    {
        public UserControlBase( )
        {
            InitializeComponent( );
        }

        public virtual void SetFoucs( )
        {
        }
        public virtual void DownKey(object sender, KeyEventArgs e)
        {
        }
        protected override void OnLoad(EventArgs e)
        {
            plTitle.Visible = !string.IsNullOrEmpty(Text);
            if (plTitle.Visible)
            {
                lblTitle.Text = Text;
                btnSlide.Command = Commands.CmdClosePanel;
                btnSlide.CommandParameter = this;
            }
            //System.Diagnostics.Debug.WriteLine("UserControlBase_Load");
            this.Bounds = PageManager.GetWorkSpace( );
            this.OpenBounds = Bounds;
            base.OnLoad(e);
        }
    }
}
