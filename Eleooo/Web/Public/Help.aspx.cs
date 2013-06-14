using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eleooo.Web.Public
{
    public partial class Help : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var s = Request["h"];
            var c = string.IsNullOrEmpty(s) ? s1 : s1.Parent.FindControl(s) ?? s1;
            c.Visible = true;
        }
    }
}