using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Design;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    [DefaultProperty("Src")]
    [ToolboxData("<{0}:ResLink runat=server></{0}:ResLink>")]
    public class ResLink : WebControl
    {
        public static readonly string ResLinkKey = "EleoooResLink";
        private string _src;
        private string _replaceSrc;
        private bool _isAfterUI;
        public static List<KeyValuePair<string, bool>> ResCache
        {
            get
            {
                return Utilities.GetInstance<List<KeyValuePair<string, bool>>>(ResLinkKey);
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [UrlProperty]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Src
        {
            get
            {
                return _src;
            }

            set
            {
                if (!string.IsNullOrEmpty(value) && (string.IsNullOrEmpty(_src) || _src != value))
                    RegResLink(value, IsAfterUI);
                _src = value;

            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [UrlProperty]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ReplaceSrc
        {
            get
            {
                return _replaceSrc;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && (string.IsNullOrEmpty(_replaceSrc) || _replaceSrc != value))
                    RemoveResLink(value);
                _replaceSrc = value;
            }
        }

        public bool IsAfterUI
        {
            get
            {
                return _isAfterUI;
            }
            set
            {
                if (value != _isAfterUI && !string.IsNullOrEmpty(_src))
                    RegResLink(_src, value);
                _isAfterUI = value;
            }
        }

        public static void RegResLink(string name, bool isAfterUI = false)
        {
            //HttpContext.Current.Response.Write(name);
            //HttpContext.Current.Response.Write("<br/>");
            var v = new KeyValuePair<string, bool>(name, isAfterUI);
            if (!ResCache.Contains(v))
                ResCache.Add(v);
        }
        public static void RemoveResLink(string name)
        {
            var v = new KeyValuePair<string, bool>(name, false);
            if (ResCache.Contains(v))
                ResCache.Remove(v);

        }
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            //base.RenderBeginTag(writer);
        }
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            //base.RenderEndTag(writer);
        }
    }
}
