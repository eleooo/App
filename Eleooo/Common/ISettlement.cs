using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Eleooo.Common
{
    public interface ISettlement : IComponent
    {
        bool Save(out string errMsg);
        void OnPageLoad(object sender, EventArgs e);
    }
}