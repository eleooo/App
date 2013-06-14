using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.Common;

namespace Eleooo.Web.MasterPage
{
    public partial class EleoooMaster : MasterPageBase
    {
        static readonly string IgnoreCurrentLinks = "/default.aspx,/Public/ViewItemList.aspx,/Member/OrderCompanyItem.aspx";
        protected override void OnLoad(EventArgs e)
        {
            if (outStatus != null && inStatus != null)
            {
                SubSystem subSys = BasePage.CurrentSubSys;
                outStatus.Visible = subSys == SubSystem.ALL;
                inStatus.Visible = !outStatus.Visible;
                if (userLink != null)
                {
                    switch (subSys)
                    {
                        case SubSystem.Admin:
                            userLink.InnerHtml = "管理后台";
                            userLink.HRef = "/Admin/SaleList.aspx";
                            break;
                        case SubSystem.Company:
                            userLink.InnerHtml = "商家后台";
                            userLink.HRef = "/Company/SaleAdd.aspx";
                            break;
                        case SubSystem.Member:
                            userLink.InnerHtml = "会员中心";
                            userLink.HRef = "/Member/MyCompany.aspx";
                            if (IgnoreCurrentLinks.IndexOf(Request.Path, StringComparison.InvariantCultureIgnoreCase) < 0
                                && Request.Path.IndexOf("/Member/", StringComparison.InvariantCultureIgnoreCase) >= 0)
                                userLink.Visible=false;
                            break;
                    }
                }
            }
            base.OnLoad(e);
        }
        protected string GetCurrentCssClass(string pageFile)
        {
            return this.Request.Path.IndexOf(pageFile, StringComparison.InvariantCultureIgnoreCase) >= 0 ? "class=\"current\"" : string.Empty;
        }
    }
}