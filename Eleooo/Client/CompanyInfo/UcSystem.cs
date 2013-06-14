using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Eleooo.Client
{
    public partial class UcSystem : UserControlBase
    {
        public UcSystem( )
        {
            InitializeComponent( );
        }
        public override void SetFoucs( )
        {
            UserControlBase c = tabSystem.SelectedPanel.Controls[0] as UserControlBase;
            if (c != null)
                c.SetFoucs( );
        }
    }
}
