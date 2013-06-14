using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.Common;

namespace Eleooo.Web.MasterPage
{
    public partial class EleoooPublicMaster : MasterPageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            if (loginBox != null && BasePage.CurrentSubSysID != 0)
                loginBox.Visible = false;
            base.OnLoad(e);
        }
        protected string ValidateDefaultSys(string subSys)
        {
            string strChecked = "checked=\"checked\"";
            if (Utilities.Compare(BasePage.ParamSubSysStr, subSys))
                return strChecked;
            else if (string.IsNullOrEmpty(BasePage.ParamSubSysStr) && Utilities.Compare(subSys, "Member"))
                return strChecked;
            else
                return string.Empty;

        }
    }
}