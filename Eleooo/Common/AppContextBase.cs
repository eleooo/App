using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using Eleooo.DAL;

namespace Eleooo.Common
{
    public class AppContextBase
    {
        private static readonly string __CURRENTSUBSYSID = "CurrentSubSysID";
        private static readonly string __FormsAuthenticationTicket = "__FormsAuthenticationTicket";

        const string CONTEXT_KEY = "AppContext";

        private static FormsAuthenticationTicket _unAuthTicket;
        protected static AppContextBase _unAuthContext;
        protected static Type _contextType;

        static AppContextBase()
        {
            _unAuthTicket = new FormsAuthenticationTicket((int)LoginSystem.Web,
                "0",
                DateTime.Now,
                DateTime.Now.AddHours(60),
                false,
                ((int)SubSystem.ALL).ToString());
        }

        protected SysMember _user;
        public SysMember User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }
        public SysNavigation MainNav { get; set; }
        private SysCompany _company;
        public SysCompany Company
        {
            get
            {
                if (_company == null || _company.Id != User.CompanyId.Value)
                    _company = SysCompany.FetchByID(User.CompanyId);
                return _company;
            }
        }
        public virtual SysMemberConfig UserConfig { get { return null; } }

        public int CurrentSubSysID
        {
            get
            {
                return CurrentSysId;
            }
        }
        public SubSystem CurrentSubSys
        {
            get
            {
                return (SubSystem)CurrentSubSysID;
            }
        }
        private CompanyType? _companyType = null;
        public CompanyType? CompanyType
        {
            get
            {
                if (!_companyType.HasValue && Company != null)
                    _companyType = Formatter.ToEnum<CompanyType>(Company.CompanyType);
                return _companyType;
            }
        }

        public int CurrentRole
        {
            get
            {
                int roleID = -1;
                if (User != null)
                {
                    switch (CurrentSubSys)
                    {
                        case SubSystem.Admin:
                            roleID = Utilities.ToInt(User.AdminRoleId);
                            break;
                        case SubSystem.Company:
                            roleID = Utilities.ToInt(User.CompanyRoleId);
                            break;
                        case SubSystem.Member:
                            roleID = Utilities.ToInt(User.MemberRoleId);
                            break;
                        default:
                            roleID = -1;
                            break;
                    }
                }
                return roleID;
            }
        }

        public virtual SubSystem ParamSubSys { get { return SubSystem.ALL; } }
        public virtual void AddMessage(string key, string message)
        {
        }

        public static AppContextBase Context
        {
            get
            {
                if (HttpContext.Current == null)
                    return _unAuthContext;
                AppContextBase _context = null;
                int userID = CurrentUserID;
                if (!HttpContext.Current.Items.Contains(CONTEXT_KEY))
                {
                    if (userID > 0)
                    {
                        _context = (AppContextBase)Activator.CreateInstance(_contextType);
                        _context._user = SysMember.FetchByID(userID);
                        if (_context._user == null)
                        {
                            Utilities.LoginOutSigOut();
                            return _unAuthContext;
                        }
                        else
                            HttpContext.Current.Items.Add(CONTEXT_KEY, _context);
                    }
                    else
                        return _unAuthContext;
                }
                else
                    _context = (AppContextBase)HttpContext.Current.Items[CONTEXT_KEY];
                return _context;
            }
        }

        public static string RootPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("/");
            }
        }
        public static HttpCookie AuthCookie
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                return cookie == null || cookie.Expires <= DateTime.Now ? HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName] : cookie;
            }
        }

        public static int CurrentSysId
        {
            get
            {
                int sysID = 0;
                if (HttpContext.Current != null)
                {
                    if (!HttpContext.Current.Items.Contains(__CURRENTSUBSYSID))
                    {
                        FormsAuthenticationTicket ticket = GetTicket();
                        if (ticket != null)
                            sysID = Utilities.ToInt(ticket.UserData);
                        HttpContext.Current.Items.Add(__CURRENTSUBSYSID, sysID);
                    }
                    else
                        sysID = (int)HttpContext.Current.Items[__CURRENTSUBSYSID];
                }
                return sysID;
            }
        }

        public static int CurrentLoginSystem
        {
            get
            {
                return GetTicket().Version;
            }
        }

        public static int CurrentUserID
        {
            get
            {
                return Convert.ToInt32(GetTicket().Name);
            }
        }

        public static System.Web.UI.Page Page
        {
            get
            {
                return HttpContext.Current.Handler as System.Web.UI.Page;
            }
        }

        public static AppContextBase GetUnAuthContext()
        {
            return _unAuthContext;
        }

        public static FormsAuthenticationTicket GetTicket()
        {
            if (HttpContext.Current.Items.Contains(__FormsAuthenticationTicket))
                return (FormsAuthenticationTicket)HttpContext.Current.Items[__FormsAuthenticationTicket];
            else if (HttpContext.Current.User != null &&
                        HttpContext.Current.User.Identity != null &&
                        HttpContext.Current.Request.IsAuthenticated)
            {
                var ticket = Utilities.GetTicket();
                HttpContext.Current.Items.Add(__FormsAuthenticationTicket, ticket);
                return ticket;
            }
            else
                return _unAuthTicket;
        }

        public static void SetFormsAuthenticationTicket(string ticketVal)
        {
            var ticket = System.Web.Security.FormsAuthentication.Decrypt(ticketVal);
            HttpContext.Current.Items[__FormsAuthenticationTicket] = ticket;
        }
    }
}
