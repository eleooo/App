using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Eleooo.Client
{
    public partial class UserControlContainer : UserControlBase
    {
        protected DevComponents.DotNetBar.Controls.SlidePanel mainContainer;
        public UserControlContainer( )
        {
            InitializeComponent( );
            Init( );
        }

        private void Init( )
        {
            this.mainContainer = new DevComponents.DotNetBar.Controls.SlidePanel( );
            // 
            // mainContainer
            // 
            this.mainContainer.BackColor = System.Drawing.Color.White;
            this.mainContainer.ForeColor = System.Drawing.Color.Black;
            this.mainContainer.Location = new System.Drawing.Point(0, 30);
            this.mainContainer.Margin = new System.Windows.Forms.Padding(5);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(717, 464);
            this.mainContainer.TabIndex = 1;
            this.mainContainer.Text = "mainContainer";
            this.mainContainer.UsesBlockingAnimation = false;
            this.Controls.Add(this.mainContainer);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            mainContainer.Dock = DockStyle.Fill;
        }
    }
}
