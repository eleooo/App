using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eleooo.Web.Controls
{
    [DefaultProperty("Src")]
    [ToolboxData("<{0}:ControlDelegate runat=server></{0}:ControlDelegate>")]
    public class ControlDelegate : WebControl
    {
        private Control _control;
        private string _src;
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [UrlProperty]
        public string Src
        {
            get
            {
                return _src;
            }

            set
            {
                _src = value;
            }
        }
        public T GetDelegateControl<T>( ) where T : Control
        {
            return _control as T;
        }
        public void CreateControl( )
        {
            if (!string.IsNullOrEmpty(_src) && _control == null)
            {
                _control = Page.LoadControl(_src);
                this.Controls.Add(_control);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            CreateControl( );
            base.OnLoad(e);
        }
        protected override void CreateChildControls( )
        {
            CreateControl( );
            base.CreateChildControls( );
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
