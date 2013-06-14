using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eleooo.Web.Public
{
    public partial class OrderMealPage : ActionPage
    {
        protected override void OnPreInit(EventArgs e)
        {

            base.OnPreInit(e);
        }
        protected override void OnInit(EventArgs e)
        {
            if ((!string.IsNullOrEmpty(HttpContext.Current.Request.Params["CompanyId"]) && !string.IsNullOrEmpty(Request.Params["MansionId"])) || 
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Params["OrderId"]))
            {
                view.Src = "~/Controls/UcOrderMeal.ascx";
            }
            else if(!string.IsNullOrEmpty(Request.Params["MansionId"]))
                view.Src = "~/Controls/UcOrderMealSelectCompany.ascx";
            else
                view.Src = "~/Controls/UcOrderMealSelectMansion.ascx";
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            view.CreateControl( );
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            var v = view.GetDelegateControl<Eleooo.Web.Controls.UserControlBase>( );
            if (v != null)
            {
                v.On_ActionDelete( );
                return;
            }
        }
    }
}