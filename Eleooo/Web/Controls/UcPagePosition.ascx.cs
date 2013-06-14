using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcPagePosition : UserControlBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.EnableViewState = false;
        }
        private List<SysNavigation> GetParentNavigation( )
        {
            var query = DB.Select( ).From<SysNavigation>( )
                                         .Where(SysNavigation.IdColumn).IsNotEqualTo(BasePage.CurrentNav.Id)
                                         .ConstraintExpression(string.Format("AND (charindex(Depth,N'{0}') > 0)", BasePage.CurrentNav.Depth))
                                         .And(SysNavigation.IdColumn).IsNotEqualTo(BasePage.CurrentNav.Id)
                                         .OrderAsc(SysNavigation.DepthColumn.ColumnName);
            List<SysNavigation> navs = query.ExecuteTypedList<SysNavigation>( );
            return navs;

        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (BasePage.CurrentNav != null && !BasePage.IsDialog)
            {
                if (BasePage.CurrentSubSys == SubSystem.Member)
                {
                    RenderMemberPagePostion(writer);
                }
                else
                {
                    List<SysNavigation> pNavs = GetParentNavigation( );
                    writer.Write("<div class='title'>");
                    writer.Write(ResBLL.GetRes("page_position", "当前位置：", "当前位置描述"));
                    if (pNavs.Count == 1)
                    {
                        writer.Write(string.Format("<a href='{0}' >{1}</a>->", pNavs[0].NavUrl, string.IsNullOrEmpty(BasePage.CurrentNav.SecName) ? pNavs[0].NavName : BasePage.CurrentNav.SecName));
                    }
                    else
                        foreach (SysNavigation nav in pNavs)
                        {
                            writer.Write(string.Format("<a href='{0}' >{1}</a>->", nav.NavUrl, string.IsNullOrEmpty(nav.SecName) ? nav.NavName : nav.SecName));
                        }
                    writer.Write("<b><font style=\"font-size: 14px\" color=\"#bb2200\">" + BasePage.CurrentNav.NavName + "</font></b></div>");
                }
            }
            base.Render(writer);
        }
        private void RenderMemberPagePostion(HtmlTextWriter writer)
        {
            string pNavUrl, pNavName;
            string cNavUrl, cNavName;
            BasePage.GetParentNavInfo(out pNavUrl, out pNavName);
            BasePage.GetCurrentNavInfo(out cNavUrl, out cNavName);
            writer.Write(MemberPosTemplate.InnerHtml, pNavUrl, pNavName, cNavUrl, cNavName);
        }
    }
}