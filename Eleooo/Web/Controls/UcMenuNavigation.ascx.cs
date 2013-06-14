using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Eleooo.DAL;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcMenuNavigation : UserControlBase
    {
        const string STRSECTIONBEGIN = @"<div class='lb-title'>
                                              <span>{0}</span>
                                            </div>
                                            <div class='lb01-main'>
                                             <ul>";

        const string MEMBER_SEC_BEGIN = @"<div class='mynavinfo mt_20'>
                                            <h3>{0}</h3>
                                            <div class='mynavinfomain'>
                                                <ul>";
        const string MEMBER_SEC_END = "</ul></div><div class=\"buttom\"></div></div>";
        const string SEC_IMAGE = "<img class=\"imgtil\" src=\"/App_Themes/Icon/{0}.png\" alt=\"\" />";
        const string STRSECTIONEND = "</ul></div>";
        const string STRITEM = "<li>{3}<a {2} href=\"{0}\">{1}</a></li>";

        //for header and foot
        const string STRLINK = "<a href=\"{0}\">{1}</a>";
        private static readonly string QuerySql = @"
;with Assigned AS(
			SELECT [dbo].[Sys_Navigation].*
			FROM [dbo].[Sys_Navigation] 
			INNER JOIN [dbo].[Sys_RoleAssignment] ON [dbo].[Sys_Navigation].[ID] = [dbo].[Sys_RoleAssignment].[NavID] AND 
					                                  [dbo].[Sys_RoleAssignment].[RoleID] = {0} AND 
                                                      [dbo].[Sys_RoleAssignment].[Allow] = 1			
            WHERE [dbo].[Sys_Navigation].[Visible] = 1 {2} AND
                  ([dbo].[Sys_Navigation].[SubSys_ID] = {1} OR [dbo].[Sys_Navigation].[SubSys_ID] = 0)
		),
     NotRequirementRight AS(
			SELECT [dbo].[Sys_Navigation].*
			FROM [dbo].[Sys_Navigation] 
            WHERE [dbo].[Sys_Navigation].[Visible] = 1 {2} AND
                  [dbo].[Sys_Navigation].[PermissionRequired] = 0 AND
                  ([dbo].[Sys_Navigation].[SubSys_ID] = {1} OR [dbo].[Sys_Navigation].[SubSys_ID] = 0)
		)
select distinct * from(
	select * from Assigned
	UNION ALL
	select * from NotRequirementRight) as t ORDER BY Sort ASC 
";

        private SubSystem _subSys;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.EnableViewState = false;
        }
        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                _subSys = BasePage.CurrentSubSys;
                switch (Region)
                {
                    case MenuRegion.Header:
                        BuildHeaderMenuHTML(writer);
                        break;
                    case MenuRegion.Left:
                        BuildLeftMenuHTML(writer);
                        break;
                    case MenuRegion.Foot:
                        BuildFootMenuHTML(writer);
                        break;
                    default:
                        if (_subSys == SubSystem.Member)
                            BuildMemberMainNavMenu(writer);
                        else
                            BuildMainNavMenuHTML(writer);
                        break;
                }

            }
            catch (Exception ex)
            {
                Logging.Log("UcMenuNavigation->Render", ex);
                Response.Write(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            base.Render(writer);
        }
        void BuildMemberMainNavMenu(HtmlTextWriter writer)
        {
            List<SysNavigation> mainNav = MainNav;
            if (mainNav.Count == 0)
                return;
            writer.Write("<div class=\"mainmenu\"><ul>");
            SysNavigation nav;
            foreach (SysNavigation item in mainNav)
            {
                nav = BasePage.RenderNav(item, MenuRegion.Main);
                writer.Write("<li><a {0} href=\"{1}\">{2}</a></li>",
                    (BasePage.CurrentNav != null && BasePage.CurrentNav.Depth.StartsWith(nav.Depth)) ? "class=\"current\"" : string.Empty,
                    nav.NavUrl,
                    nav.NavName
                    );
            }
            writer.Write("</ul></div>");
        }
        protected virtual void BuildMainNavMenuHTML(HtmlTextWriter writer)
        {
            List<SysNavigation> mainNav = MainNav;
            if (mainNav.Count == 0)
                return;
            writer.Write("<div class=\"menu-qie\" id=\"MenuContainer\">");
            writer.Write("<ul>");
            foreach (SysNavigation nav in mainNav)
            {
                string navName = BasePage.RenderNav(nav, MenuRegion.Main).NavName;
                writer.Write("<li {0}><a href=\"{1}\">{2}</a></li>",
                    (BasePage.CurrentNav != null && BasePage.CurrentNav.Depth.StartsWith(nav.Depth)) ? "class=\"selected\"" : string.Empty,
                    nav.NavUrl,
                    navName
                    );
            }
            writer.Write("</ul>");
            writer.Write("</div>");
        }
        protected virtual void BuildHeaderMenuHTML(HtmlTextWriter writer)
        {
            List<SysNavigation> headerNav = HeaderNav;
            if (headerNav.Count == 0)
                return;
            writer.Write("<div class=\"top-lj\"><span>");
            foreach (SysNavigation nav in headerNav)
            {
                string navName = BasePage.RenderNav(nav, MenuRegion.Header).NavName;
                writer.Write(string.Format(STRLINK, nav.NavUrl, navName));
                nav.NavName = navName;
            }
            writer.Write("</span></div>");
        }

        private string LeftMenuSecBegin
        {
            get
            {
                return _subSys == SubSystem.Member ? MEMBER_SEC_BEGIN : STRSECTIONBEGIN;
            }
        }
        private string LeftMenuSecEnd
        {
            get
            {
                return _subSys == SubSystem.Member ? MEMBER_SEC_END : STRSECTIONEND;
            }
        }
        private string GetLeftMenuItemIconImage(SysNavigation nav)
        {
            if (string.IsNullOrEmpty(nav.NavIcon))
                return string.Empty;
            else
                return string.Format(SEC_IMAGE, nav.NavIcon);
        }
        protected virtual void BuildLeftMenuHTML(HtmlTextWriter writer)
        {
            if (BasePage.CurrentNav == null)
                return;
            List<SysNavigation> navs = LeftNav;
            SysNavigation mainNav = GetCurrentMainNav(navs);
            if (mainNav == null)
                return;
            string strDefSec = string.IsNullOrEmpty(mainNav.SecName) ? mainNav.NavName : mainNav.SecName;
            List<string> lstSec = NavigationBLL.FindDistinctSec(navs, strDefSec);
            Dictionary<string, StringBuilder> secs = new Dictionary<string, StringBuilder>( );
            foreach (string sec in lstSec)
                secs.Add(sec, new StringBuilder(string.Format(LeftMenuSecBegin, sec)));
            //build main nav item to left nav
            string navName = BasePage.RenderNav(mainNav, MenuRegion.Left).NavName;
            secs[strDefSec].AppendFormat(STRITEM, mainNav.NavUrl,
                                                 navName,
                                                 Utilities.Compare(mainNav.NavUrl, BasePage.CurrentNav.NavUrl) ? "class=\"selected\"" : string.Empty,
                                                 GetLeftMenuItemIconImage(mainNav));
            if (navs.Count == 0)
                return;
            List<SysNavigation> cItems = NavigationBLL.FindNavByPid(navs, mainNav.Id);
            BuildLeftMenuSecItem(navs, cItems, secs, strDefSec);
            foreach (StringBuilder sec in secs.Values)
            {
                writer.Write(sec);
                writer.Write(LeftMenuSecEnd);
            }
        }
        private void BuildLeftMenuSecItem(List<SysNavigation> navs, List<SysNavigation> pItems, Dictionary<string, StringBuilder> secs, string defSec)
        {
            foreach (SysNavigation item in pItems)
            {
                string navName = BasePage.RenderNav(item, MenuRegion.Left).NavName;
                string sec = string.IsNullOrEmpty(item.SecName) ? defSec : item.SecName;
                StringBuilder sb = secs[sec];
                sb.AppendFormat(STRITEM, item.NavUrl,
                                        navName,
                                        Utilities.Compare(item.NavUrl, BasePage.CurrentNav.NavUrl) ? "class=\"selected\"" : string.Empty,
                                        GetLeftMenuItemIconImage(item));
                List<SysNavigation> cItems = NavigationBLL.FindNavByPid(navs, item.Id);
                if (cItems != null && cItems.Count > 0)
                    BuildLeftMenuSecItem(navs, cItems, secs, defSec);
            }
        }

        private SysNavigation GetCurrentMainNav(List<SysNavigation> navs)
        {
            if (AppContext.Context.MainNav != null &&
                BasePage.CurrentNav.Depth.Contains(AppContext.Context.MainNav.Depth))
                return AppContext.Context.MainNav;
            SysNavigation mainNav = null;
            foreach (SysNavigation nav in navs)
            {
                if (nav.IsMainNav)
                {
                    mainNav = nav;
                    break;
                }
            }
            return mainNav;
        }

        protected virtual void BuildFootMenuHTML(HtmlTextWriter writer)
        {
            List<SysNavigation> footNav = FootNav;
            if (footNav.Count == 0)
                return;
            writer.Write("<div class=\"kslj01\"><span>");
            List<string> lstItems = new List<string>( );
            foreach (SysNavigation nav in footNav)
            {
                string navName = BasePage.RenderNav(nav, MenuRegion.Foot).NavName;
                lstItems.Add(string.Format(STRLINK, nav.NavUrl, navName));
            }
            writer.Write(string.Join(" | ", lstItems.ToArray( )));
            writer.Write("</span></div>");
        }

        private List<SysNavigation> _mainNav;
        protected virtual List<SysNavigation> MainNav
        {
            get
            {
                if (_mainNav == null)
                {
                    //SubSonic.SqlQuery query = DB.Select( ).From<SysNavigation>( )
                    //                            .LeftOuterJoin(SysRoleAssignment.NavIDColumn, SysNavigation.IdColumn)
                    //                            .Where(SysNavigation.VisibleColumn).IsEqualTo(1)
                    //                            .And(SysNavigation.IsMainNavColumn).IsEqualTo(1)
                    //                            .AndEx("(" + SysNavigation.PermissionRequiredColumn).IsLessThanOrEqualTo(IsAuthorization)
                    //                            .And("(" + SysNavigation.SubSysIdColumn).IsEqualTo(CurContext.CurrentSubSysID)
                    //                            .Or(SysNavigation.SubSysIdColumn).IsEqualTo(0)
                    //                            .CloseExpression( ).CloseExpression( )
                    //                            .OrEx(SysRoleAssignment.RoleIDColumn)
                    //                            .IsEqualTo(CurContext.CurrentRole)
                    //                            .And(SysRoleAssignment.AllowColumn)
                    //                            .IsEqualTo(1)
                    //                            .CloseExpression( ).CloseExpression( )
                    //                            .OrderAsc(SysNavigation.SortColumn.ColumnName);
                    //BasePage.LogMessage(query.ToString( ));
                    //_mainNav = query.ExecuteTypedList<SysNavigation>( );
                    _mainNav = GetSysNavigation(CurContext.CurrentRole, CurContext.CurrentSubSysID, MenuRegion.Main);
                }
                return _mainNav;
            }
        }

        protected virtual List<SysNavigation> HeaderNav
        {
            get
            {
                //SubSonic.SqlQuery query = DB.Select( ).From<SysNavigation>( )
                //                            .LeftOuterJoin(SysRoleAssignment.NavIDColumn, SysNavigation.IdColumn)
                //                            .Where(SysNavigation.VisibleColumn).IsEqualTo(1)
                //                            .And(SysNavigation.IsHeaderColumn).IsEqualTo(1)
                //                            .AndEx("(" + SysNavigation.PermissionRequiredColumn).IsLessThanOrEqualTo(IsAuthorization)
                //                            .And("(" + SysNavigation.SubSysIdColumn).IsEqualTo(CurContext.CurrentSubSysID)
                //                            .Or(SysNavigation.SubSysIdColumn).IsEqualTo(0)
                //                            .CloseExpression( ).CloseExpression( )
                //                            .OrEx(SysRoleAssignment.RoleIDColumn)
                //                            .IsEqualTo(CurContext.CurrentRole)
                //                            .And(SysRoleAssignment.AllowColumn)
                //                            .IsEqualTo(1)
                //                            .CloseExpression( ).CloseExpression( )
                //                            .OrderAsc(SysNavigation.SortColumn.ColumnName);
                //return query.ExecuteTypedList<SysNavigation>( );
                return GetSysNavigation(CurContext.CurrentRole, CurContext.CurrentSubSysID, MenuRegion.Header);
            }
        }

        protected virtual List<SysNavigation> LeftNav
        {
            get
            {
                //string[] arrDepth = BasePage.CurrentNav.Depth.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                //string mainNavId = string.Concat("/", arrDepth[0], "/");
                //SubSonic.SqlQuery query = DB.Select( ).From<SysNavigation>( )
                //                            .LeftOuterJoin(SysRoleAssignment.NavIDColumn, SysNavigation.IdColumn)
                //                            .Where(SysNavigation.VisibleColumn).IsEqualTo(1)
                //                            .ConstraintExpression(string.Format("AND (charindex('{0}',Depth) > 0)", mainNavId))
                //                            .AndEx("(" + SysNavigation.PermissionRequiredColumn).IsLessThanOrEqualTo(IsAuthorization)
                //                            .And("(" + SysNavigation.SubSysIdColumn).IsEqualTo(CurContext.CurrentSubSysID)
                //                            .Or(SysNavigation.SubSysIdColumn).IsEqualTo(0)
                //                            .CloseExpression( ).CloseExpression( )
                //                            .OrEx(SysRoleAssignment.RoleIDColumn)
                //                            .IsEqualTo(CurContext.CurrentRole)
                //                            .And(SysRoleAssignment.AllowColumn)
                //                            .IsEqualTo(1)
                //                            .CloseExpression( ).CloseExpression( )
                //                            .OrderAsc(SysNavigation.SortColumn.ColumnName);
                //return query.ExecuteTypedList<SysNavigation>( );
                return GetSysNavigation(CurContext.CurrentRole, CurContext.CurrentSubSysID, MenuRegion.Left);
            }
        }

        protected virtual List<SysNavigation> FootNav
        {
            get
            {
                //SubSonic.SqlQuery query = DB.Select( ).From<SysNavigation>( )
                //                            .LeftOuterJoin(SysRoleAssignment.NavIDColumn, SysNavigation.IdColumn)
                //                            .Where(SysNavigation.VisibleColumn).IsEqualTo(1)
                //                            .And(SysNavigation.IsFooterColumn).IsEqualTo(1)
                //                            .AndEx("(" + SysNavigation.PermissionRequiredColumn).IsLessThanOrEqualTo(IsAuthorization)
                //                            .And("(" + SysNavigation.SubSysIdColumn).IsEqualTo(CurContext.CurrentSubSysID)
                //                            .Or(SysNavigation.SubSysIdColumn).IsEqualTo(0)
                //                            .CloseExpression( ).CloseExpression( )
                //                            .OrEx(SysRoleAssignment.RoleIDColumn)
                //                            .IsEqualTo(CurContext.CurrentRole)
                //                            .And(SysRoleAssignment.AllowColumn)
                //                            .IsEqualTo(1)
                //                            .CloseExpression( ).CloseExpression( )
                //                            .OrderAsc(SysNavigation.SortColumn.ColumnName);
                //return query.ExecuteTypedList<SysNavigation>( );
                return GetSysNavigation(CurContext.CurrentRole, CurContext.CurrentSubSysID, MenuRegion.Foot);
            }
        }
        protected int IsAuthorization
        {
            get
            {
                return HttpContext.Current.Request.IsAuthenticated && !HttpContext.Current.SkipAuthorization ? 1 : 0;
            }
        }
        public MenuRegion Region { get; set; }
        private static List<SysNavigation> GetSysNavigation(int roleId, int subSys, MenuRegion region)
        {
            string vSql = string.Format(QuerySql, roleId, subSys, GetNavigationRegionFilter(region));
            QueryCommand cmd = new QueryCommand(vSql);
            //AppContext.ActPage.LogMessage(vSql);
            return DataService.ExecuteListTypedResult<SysNavigation>(cmd);
        }
        private static string GetNavigationRegionFilter(MenuRegion region)
        {
            switch (region)
            {
                case MenuRegion.Main:
                    return "AND [Sys_Navigation].[IsMainNav]=1";
                case MenuRegion.Header:
                    return "AND [Sys_Navigation].[IsHeader]=1";
                case MenuRegion.Foot:
                    return "AND [Sys_Navigation].[IsFooter]=1";
                case MenuRegion.Left:
                    string[] arrDepth = AppContext.ActPage.CurrentNav.Depth.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    return string.Format("AND (charindex('/{0}/',Depth) > 0)", arrDepth[0]);
                default:
                    return string.Empty;
            }
        }
    }
}