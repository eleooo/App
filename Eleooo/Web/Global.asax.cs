using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AppContext.InitContext( );
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            ResBLL.LoadRes(false);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string action = Server.UrlDecode(Request.Params["HTTP_SOAPACTION"]);
            if ((!string.IsNullOrEmpty(Context.Request.RawUrl) && Context.Request.RawUrl.Length == 1 && Context.Request.RawUrl[0] == '/') ||
                (!string.IsNullOrEmpty(action) && Utilities.Compare(action, "\"http://tempuri.org/ClientLogin\"")))
                HttpContext.Current.SkipAuthorization = true;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //var ex = Server.GetLastError( );
            //Logging.Log("Application_Error", ex);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}