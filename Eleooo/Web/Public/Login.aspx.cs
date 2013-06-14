using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.Common;

namespace Eleooo.Web.Public
{
    public partial class Login : ActionPage
    {
        private SubSystem? _loginSys;
        protected SubSystem LoginSys
        {
            get
            {
                if (!_loginSys.HasValue)
                {
                    string sys = Request.Params["SubSys"];
                    _loginSys = Formatter.ToEnum<SubSystem>(sys, SubSystem.Member);
                    if (_loginSys != SubSystem.Member && _loginSys != SubSystem.Company)
                        _loginSys = SubSystem.Member;
                }
                return _loginSys.Value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            memberLoginTitle.Visible = LoginSys == SubSystem.Member;
            memberLoginDesc.Visible = memberLoginTitle.Visible;
            companyLoginTitle.Visible = !memberLoginTitle.Visible;
            companyLoginDesc.Visible = companyLoginTitle.Visible;
            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["phone"]))
                this.Params["edtUserName"] = Request.QueryString["phone"];
        }
    }
}