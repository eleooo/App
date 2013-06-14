using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    //public delegate void OnRenderCompletedHandler(HtmlTextWriter writer);
    public class UserControlBase : System.Web.UI.UserControl
    {
        protected ActionPage BasePage
        {
            get
            {
                return this.Page as ActionPage;
            }
        }
        protected AppContextBase CurContext
        {
            get
            {
                return AppContext.Context;
            }
        }

        protected object Eval(int index)
        {
            return BasePage.Eval(index);
        }
        protected object Eval( )
        {
            return BasePage.Eval( );
        }

        public virtual void On_ActionDelete( )
        { }
        //public event OnRenderCompletedHandler OnRenderCompleted;

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    writer.WriteLine("UserControlBase.Render1<br/>");
        //    if (OnRenderCompleted != null)
        //        OnRenderCompleted(writer);
        //    base.Render(writer);
        //    writer.WriteLine("UserControlBase.Render2<br/>");
        //}
        
        //protected override void RenderChildren(HtmlTextWriter writer)
        //{
        //    writer.WriteLine("UserControlBase.RenderChildren1<br/>");
        //    base.RenderChildren(writer);
        //    writer.WriteLine("UserControlBase.RenderChildren2<br/>");
        //}
        //public override void RenderControl(HtmlTextWriter writer)
        //{
        //    writer.WriteLine("UserControlBase.RenderControl1<br/>");
        //    base.RenderControl(writer);
        //    writer.WriteLine("UserControlBase.RenderControl2<br/>");
        //}
        //protected override void OnPreRender(EventArgs e)
        //{
        //    Response.Write("UserControlBase.OnPreRender<br/>");
        //    base.OnPreRender(e);
        //}
    }
}