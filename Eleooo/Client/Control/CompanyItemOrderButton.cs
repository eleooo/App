using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar.SuperGrid;

namespace Eleooo.Client
{
    public delegate void OrderCompanyItemHandler(int itemID);
    public class CompanyItemOrderButton : GridButtonXEditControl
    {
        public CompanyItemOrderButton( )
        {
            //UseCellValueAsButtonText = false;
            ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb;
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (OnOrderCompanyItem != null && !EditorCell.IsValueNull)
                OnOrderCompanyItem(Convert.ToInt32(EditorCell.Value));
        }
        public override string Text
        {
            get
            {
                return "验证";
            }
            set
            {
                base.Text = value;
            }
        }
        public event OrderCompanyItemHandler OnOrderCompanyItem;
    }
}
