using System;
using System.Collections.Generic;
using System.Web;
using System.Reflection;
using Eleooo.DAL;
using SubSonic;
using System.Collections.Specialized;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class NavigationBLL
    {
        public static void RefreshNavigation( )
        {
            NavigationDefineAttribute[] arrNavDefines = Attribute.GetCustomAttributes(Assembly.GetExecutingAssembly( ), typeof(NavigationDefineAttribute)) as NavigationDefineAttribute[];
            if (arrNavDefines == null || arrNavDefines.Length == 0)
                return;
            SysNavigationCollection navs = DB.Select( )
                                             .From<SysNavigation>( )
                                             .ExecuteAsCollection<SysNavigationCollection>( );
            List<NavigationDefineAttribute> navDefines = new List<NavigationDefineAttribute>(arrNavDefines);
            RemoveExistDefine(navs, navDefines);
            RefreshNavCore(ref navDefines, ref navs, null, null);
            RemoveExistDefine(navs, navDefines);
            foreach (NavigationDefineAttribute navDefine in navDefines)
            {
                SysNavigation pNav = FindNavByUrl(navs, navDefine.P_NavUrl);
                AddNewNavigation(navDefine, pNav);
            }
        }

        private static void RefreshNavCore(ref List<NavigationDefineAttribute> navDefines,
                                           ref SysNavigationCollection navs,
                                           NavigationDefineAttribute navDefine,
                                           SysNavigation nav)
        {
            string navUrl = navDefine == null ? string.Empty : navDefine.NavUrl;
            List<NavigationDefineAttribute> partNavDefines = FindNavDefinesByUrl(ref navDefines, navUrl);
            if (partNavDefines.Count == 0 && navDefine == null && nav == null)
            {
                partNavDefines = navDefines;
            }
            foreach (NavigationDefineAttribute partNav in partNavDefines)
            {
                //not exist item and new item to navs
                SysNavigation cNav = FindNavByUrl(navs, partNav.NavUrl);
                if (cNav == null)
                {
                    SysNavigation pNav = FindNavByUrl(navs, partNav.P_NavUrl);
                    cNav = AddNewNavigation(partNav, pNav == null ? nav : pNav);
                    AutoRoleAssignement(cNav);
                    //?
                    navs.Add(cNav);
                }
                RefreshNavCore(ref navDefines, ref navs, partNav, cNav);
            }
        }
        private static SysNavigation AddNewNavigation(NavigationDefineAttribute navDefine, SysNavigation pNav)
        {
            int nPID = 0;
            string sDepth = "/";
            if (pNav != null)
            {
                nPID = Utilities.ToInt(pNav.Id);
                sDepth = pNav.Depth;
            }
            SysNavigation cNav = new SysNavigation( )
            {
                IsFooter = navDefine.IsFooter,
                IsHeader = navDefine.IsHeader,
                IsMainNav = navDefine.IsMainNav,
                NavName = navDefine.NavName,
                NavUrl = navDefine.NavUrl,
                PId = nPID,
                PermissionRequired = navDefine.PermissionRequired,
                SecName = navDefine.SecName,
                OthName = navDefine.OthName,
                SubSysId = Convert.ToInt32(navDefine.SubSys),
                Visible = true,
                Sort = 0,
                Depth = "/",
                NavIcon = navDefine.NavIcon
            };
            cNav.Save(AppContextBase.CurrentUserID);
            cNav.Sort = cNav.Id;
            cNav.Depth = string.Concat(sDepth, cNav.Id, "/");
            cNav.Save( );
            return cNav;
        }
        private static void AutoRoleAssignement(SysNavigation nav)
        {

            int roleDefine = UserBLL.GetDefaultUseRole(nav.SubSysId.Value);
            SysRoleAssignment ass = DB.Select( ).From<SysRoleAssignment>( )
                          .Where(SysRoleAssignment.NavIDColumn).IsEqualTo(nav.Id)
                          .And(SysRoleAssignment.RoleIDColumn).IsEqualTo(roleDefine)
                          .ExecuteSingle<SysRoleAssignment>( );
            if (ass == null)
            {
                new SysRoleAssignment
                {
                    NavID = nav.Id,
                    RoleID = roleDefine,
                    Allow = true
                }.Save( );
            }
        }
        public static SysNavigation FindNavByUrl(SysNavigationCollection navs, string pUrl)
        {
            SysNavigation pNav = null;
            if (navs == null || navs.Count == 0)
                goto lable_end;
            int subSysID = 0;
            foreach (SysNavigation nav in navs)
            {
                subSysID = Convert.ToInt32(nav.SubSysId);
                if (Utilities.Compare(nav.NavUrl, pUrl))
                {
                    pNav = nav;
                    goto lable_end;
                }
            }

        lable_end:
            return pNav;
        }

        public static List<NavigationDefineAttribute> FindNavDefinesByUrl(ref List<NavigationDefineAttribute> navDefines, string pUrl)
        {
            List<NavigationDefineAttribute> lstNavs = navDefines.FindAll((NavigationDefineAttribute match) =>
                {
                    return Utilities.Compare(match.P_NavUrl, pUrl);
                });
            if (lstNavs != null)
            {
                foreach (NavigationDefineAttribute nav in lstNavs)
                {
                    navDefines.Remove(nav);
                }
            }
            return lstNavs;
        }
        public static List<SysNavigation> FindNavByPid(List<SysNavigation> navs, int pid, bool bRemove)
        {
            List<SysNavigation> cList = navs.FindAll((SysNavigation match) =>
                {
                    return match.PId == pid;
                });
            if (cList != null && bRemove)
            {
                foreach (SysNavigation nav in cList)
                {
                    navs.Remove(nav);
                }
            }
            return cList;
        }
        public static List<SysNavigation> FindNavByPid(List<SysNavigation> navs, int pid)
        {
            return FindNavByPid(navs, pid, true);
        }
        public static List<string> FindDistinctSec(List<SysNavigation> navs, string strDefaultSec)
        {
            List<string> lstSec = new List<string>( );
            lstSec.Add(strDefaultSec);
            foreach (SysNavigation nav in navs)
            {
                if (!lstSec.Contains(nav.SecName) && !string.IsNullOrEmpty(nav.SecName))
                    lstSec.Add(nav.SecName);
            }
            return lstSec;
        }
        private static void RemoveExistDefine(SysNavigationCollection navs, List<NavigationDefineAttribute> navDefines)
        {
            foreach (SysNavigation nav in navs)
            {
                NavigationDefineAttribute att = navDefines.Find((NavigationDefineAttribute match) =>
                    {
                        return nav.NavUrl.Contains(match.NavUrl);
                    });
                if (att != null)
                    navDefines.Remove(att);
            }
        }
    }
}