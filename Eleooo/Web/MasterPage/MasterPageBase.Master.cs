using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web.MasterPage
{
    public partial class MasterPageBase : System.Web.UI.MasterPage
    {
        private static readonly Type __type = typeof(MasterPageBase);
        const string SCRIPT_REF = "<script src=\"{0}?v={1}\" type=\"text/javascript\"></script>";
        const string CSS_REF = "<link href=\"{0}?v={1}\" rel=\"stylesheet\" type=\"text/css\" />";
        private StringBuilder ScriptBuffer { get; set; }
        private StringBuilder LoadedScriptBuffer { get; set; }
        private StringBuilder CssBuffer { get; set; }
        private List<string> ScriptPathBuffer { get; set; }
        private List<string> CssPathBuffer { get; set; }
        private StringBuilder _validateScriptBuff;
        public StringBuilder ValidateScriptBuffer
        {
            get
            {
                if (_validateScriptBuff == null)
                {
                    _validateScriptBuff = new StringBuilder( );
                }
                return _validateScriptBuff;
            }
        }
        private string _version;
        public string Version
        {
            get
            {
                if (string.IsNullOrEmpty(_version))
                    _version = Utilities.GetTypeVersion(__type);
                return _version;
            }
        }

        public StringBuilder AddLoadedScript(string script)
        {
            if (LoadedScriptBuffer == null)
                LoadedScriptBuffer = new StringBuilder( );
            LoadedScriptBuffer.AppendLine(script);
            return LoadedScriptBuffer;
        }
        public StringBuilder AddScriptBlock(string script)
        {
            if (ScriptBuffer == null)
                ScriptBuffer = new StringBuilder( );
            ScriptBuffer.AppendLine(script);
            return ScriptBuffer;
        }
        public StringBuilder AddScriptBlock(string script, params object[] args)
        {
            if (args != null && args.Length > 0)
                return AddScriptBlock(string.Format(script, args));
            else
                return AddScriptBlock(script);
        }
        public void AddScriptPath(string path)
        {
            if (ScriptPathBuffer == null)
                ScriptPathBuffer = new List<string>( );
            if (!ScriptPathBuffer.Contains(path))
                ScriptPathBuffer.Add(path);
        }
        public void AddCssBlock(string css)
        {
            if (CssBuffer == null)
                CssBuffer = new StringBuilder( );
            CssBuffer.AppendLine(css);
        }
        public void AddCssPath(string path)
        {
            if (CssPathBuffer == null)
                CssPathBuffer = new List<string>( );
            if (!CssPathBuffer.Contains(path))
                CssPathBuffer.Add(path);
        }
        protected override void OnInit(EventArgs e)
        {
            if (pageInitor != null)
                pageInitor.SetRenderMethodDelegate(new RenderMethod(pageInitor_Render));
            if (pageInitorAfterUI != null)
                pageInitorAfterUI.SetRenderMethodDelegate(new RenderMethod(pageInitorAfterUI_Render));
            if (dlgSupport != null)
                dlgSupport.Visible = BasePage.IsDialog;
            base.OnInit(e);
        }
        protected void pageInitorAfterUI_Render(HtmlTextWriter output, Control container)
        {
            if (HttpContext.Current.Items.Contains(Eleooo.Web.Controls.ResLink.ResLinkKey))
            {
                foreach (var pair in Eleooo.Web.Controls.ResLink.ResCache)
                {
                    if (!pair.Value)
                        continue;
                    if (pair.Key.ToLower( ).EndsWith(".css"))
                        RenderCSSScriptLink(output, pair.Key, this.Version);
                    else
                        RenderResScriptLink(output, pair.Key, this.Version);
                }
            }
        }
        protected void pageInitor_Render(HtmlTextWriter output, Control container)
        {
            RenderCssPath(output);
            RenderCssBlock(output);
            if (HttpContext.Current.Items.Contains(Eleooo.Web.Controls.ResLink.ResLinkKey))
            {
                foreach (var pair in Eleooo.Web.Controls.ResLink.ResCache)
                {
                    if (pair.Value)
                        continue;
                    if (pair.Key.ToLower( ).EndsWith(".css"))
                        RenderCSSScriptLink(output, pair.Key, this.Version);
                    else
                        RenderResScriptLink(output, pair.Key, this.Version);
                }
            }
            RenderScriptPath(output);
            RenderScriptBlock(output);
            RenderLoadedScript(output);
            ValidateScriptBuffer.Insert(0, "var validateInput = function () {");
            ValidateScriptBuffer.Insert(0, "<script language=\"javascript\" type=\"text/javascript\">");
            ValidateScriptBuffer.AppendLine("return true;");
            ValidateScriptBuffer.AppendLine("}");
            ValidateScriptBuffer.AppendLine("</script>");
            output.WriteLine(ValidateScriptBuffer);
        }
        private void RenderLoadedScript(HtmlTextWriter writer)
        {
            if (LoadedScriptBuffer == null)
                return;
            writer.WriteLine("<script language=\"javascript\" type=\"text/javascript\">");
            writer.WriteLine("$(document).ready(function(){");
            writer.Write(LoadedScriptBuffer);
            writer.WriteLine("});");
            writer.WriteLine("</script>");
        }
        private static void RenderResScriptLink(HtmlTextWriter writer, string path, string version)
        {
            writer.WriteLine(SCRIPT_REF, path, version);
        }
        private static void RenderCSSScriptLink(HtmlTextWriter writer, string path, string version)
        {
            writer.WriteLine(CSS_REF, path, version);
        }
        private void RenderScriptBlock(HtmlTextWriter writer)
        {
            if (ScriptBuffer == null)
                return;
            writer.WriteLine("<script type=\"text/javascript\">");
            writer.WriteLine(ScriptBuffer);
            writer.WriteLine("</script>");
        }
        private void RenderScriptPath(HtmlTextWriter writer)
        {
            if (ScriptPathBuffer == null)
                return;
            foreach (string item in ScriptPathBuffer)
            {
                writer.WriteLine(SCRIPT_REF, item, this.Version);
            }
        }
        private void RenderCssBlock(HtmlTextWriter writer)
        {
            if (CssBuffer == null)
                return;
            writer.WriteLine("<style >");
            writer.WriteLine(CssBuffer);
            writer.WriteLine("</style>");
        }
        private void RenderCssPath(HtmlTextWriter writer)
        {
            if (CssPathBuffer == null)
                return;
            foreach (string item in CssPathBuffer)
                writer.WriteLine(CSS_REF, item, this.Version);
        }
        protected virtual void btnLogout_Click(object sender, EventArgs e)
        {
            //iPublic.UserID = 0;
            Utilities.Redirect("Login.aspx");
        }
        protected virtual void btnLogin_Click(object sender, EventArgs e)
        {
            //iPublic.UserID = 0;
            //base.Response.Redirect("Login.aspx");
        }
        protected virtual void Page_Load(object sender, EventArgs e)
        {
        }
        protected HttpApplication ApplicationInstance
        {
            get
            {
                return this.Context.ApplicationInstance;
            }
        }

        protected System.Web.Profile.DefaultProfile Profile
        {
            get
            {
                return (System.Web.Profile.DefaultProfile)this.Context.Profile;
            }
        }
        public void LoadDlgSupportCss(bool isLoad)
        {
            dlgSupport.Visible = isLoad;
        }
        protected ActionPage BasePage
        {
            get
            {
                return this.Page as ActionPage;
            }
        }
    }
}