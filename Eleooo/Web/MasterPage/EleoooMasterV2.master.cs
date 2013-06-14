using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.Common;

namespace Eleooo.Web.MasterPage
{
    public partial class EleoooMasterV2 : MasterPageBase
    {
        static readonly string IgnoreCurrentLinks = "/default.aspx,/Public/ViewItemList.aspx,/Member/OrderCompanyItem.aspx";
        protected override void OnLoad(EventArgs e)
        {
            if (outStatus != null && inStatus != null)
            {
                BasePage.IsUserLink = true;
                SubSystem subSys = BasePage.CurrentSubSys;
                outStatus.Visible = subSys == SubSystem.ALL;
                inStatus.Visible = !outStatus.Visible;
                if (inStatus.Visible)
                {
                    var addrs = UserBLL.GetUserFavAddress(AppContext.Context.User.Id);
                    if (addrs.Count == 1)
                    {
                        mealPageLink.HRef = mealPageLink.HRef + "?MansionId=" + addrs[0].id.ToString( );
                        mealPageLink.Attributes.Add("addr", addrs[0].name);
                    }
                }
                if (userLink != null)
                {
                    switch (subSys)
                    {
                        case SubSystem.Admin:
                            userLink.InnerHtml = "<span>管理后台</span>";
                            userLink.HRef = "/Admin/SaleList.aspx";
                            break;
                        case SubSystem.Company:
                            userLink.InnerHtml = "<span>商家后台</span>";
                            userLink.HRef = "/Company/SaleAdd.aspx";
                            break;
                        case SubSystem.Member:
                            userLink.InnerHtml = "<span>我的乐多分</span>";
                            userLink.HRef = "/Member/MyCompany.aspx";
                            if (BasePage.IsUserLink)
                            {
                                userLink.Visible = true;
                                break;
                            }
                            if (IgnoreCurrentLinks.IndexOf(Request.Path, StringComparison.InvariantCultureIgnoreCase) < 0
                                && Request.Path.IndexOf("/Member/", StringComparison.InvariantCultureIgnoreCase) >= 0)
                                userLink.Visible = false;
                            break;
                    }
                }
            }
            base.OnLoad(e);
        }

        protected string GetCurrentCssClass(string pageFile)
        {
            return this.Request.Path.IndexOf(pageFile, StringComparison.InvariantCultureIgnoreCase) >= 0 ? "class=\"selected\"" : string.Empty;
        }
    }
}