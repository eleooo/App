using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Reflection;
using Eleooo.DAL;
using System.Web.UI;
using System.Collections;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class ActionPage : System.Web.UI.Page
    {
        protected const string ACTION_DEL_TEMPLATE = "<a href='javascript:void(0)' class=\"action_del\" param=\"{0}\" >{1}</a>";
        protected const string ACTION_DLG_TEMPLATE = "<a href='javascript:void(0)' class=\"action_dlg\" param=\"{0}\">{1}</a>";
        protected const string ACTION_DLG_INDEX_TEMPLATE = "<a href='javascript:void(0)' class=\"action_dlg\" param=\"{0}\" index=\"{2}\">{1}</a>";
        protected const string ACTION_EDIT_TEMPLATE = "<a href='javascript:void(0)' class=\"action_edit\" param=\"{0}\">{1}</a>";
        protected const string ALIGN_LEFT_CELL_TEMPLATE = "<td style=\"text-align:left;\">{0}</td>";
        protected const string ALIGN_LEFT_CELL_WIDTH_TEMPLATE = "<td style=\"text-align:left;width:{1}\">{0}</td>";
        protected const string CELL_WIDTH_TEMPLATE = "<td style=\"width:{1}\">{0}</td>";
        protected const string ERROR_MESSAGE_TEMPLATE = "<span style=\"text-align:left;color:read;\">{0}</span>";
        private static readonly Type _typePage = typeof(System.Web.UI.Page);
        protected override void OnPreInit(EventArgs e)
        {
            try
            {
                if (IsDialog)
                    this.MasterPageFile = "~/MasterPage/MasterPageBase.Master";
                if (CurrentNav != null && CurrentNav.IsMainNav && CurrentNav.Visible)
                    AppContext.Context.MainNav = CurrentNav;
                base.OnPreInit(e);
                if (!PermissionCheck( ))
                {
                    Utilities.ShowMessageRedirect("对不起,你不被授权查看此页!", "/");
                }
            }
            catch (Exception ex)
            {
                Logging.Log("ActionPage->OnPreInit", ex);
                Response.Write(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            var act = ParamAction;
            try
            {
                _typePage.CallTypePrivateMethod<object>(this, "RegisterPostBackScript", Type.EmptyTypes, null);
                base.OnLoad(e);
                if (!Visible) { return; }
                switch (act)
                {
                    case Actions.Add:
                        On_ActionAdd(this, e);
                        break;
                    case Actions.Delete:
                        On_ActionDelete(this, e);
                        break;
                    case Actions.Edit:
                        On_ActionEdit(this, e);
                        break;
                    case Actions.Login:
                        On_ActionLogin(this, e);
                        break;
                    case Actions.Logout:
                        On_ActionLogout(this, e);
                        break;
                    default:
                        On_ActionQuery(this, e);
                        break;
                }
                if (Response.IsRequestBeingRedirected)
                    goto lbl_return;
                if (CurrentUser.Id > 0)
                {
                    if (CurrentUser.DirtyColumns.Count > 0)
                        CurrentUser.MarkClean( );
                    CurrentUser.MarkOld( );
                    CurrentUser.LastLoginDate = DateTime.Now;
                    CurrentUser.LastLoginSubSys = CurrentSubSysID;
                    CurrentUser.ValidateWhenSaving = false;
                    CurrentUser.Save( );
                }
            }
            catch (Exception ex)
            {
                Logging.Log("ActionPage->OnLoad->" + act.ToString( ), ex);
                Response.Write(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        lbl_return:
            return;
        }

        protected virtual void On_ActionQuery(object sender, EventArgs e)
        {
        }

        protected virtual void On_ActionDelete(object sender, EventArgs e)
        {
        }
        protected virtual void On_ActionEdit(object sender, EventArgs e)
        {
        }
        protected virtual void On_ActionAdd(object sender, EventArgs e)
        {
        }
        protected virtual void On_ActionLogin(object sender, EventArgs e)
        {
            UserBLL.UserWebLogin( );
        }
        protected virtual void On_ActionLogout(object sender, EventArgs e)
        {
            Utilities.LoginOutSigOut( );
            Utilities.Redirect("/");
            //Utilities.ShowMessageRedirect("已经安全退出,确定后进入首页!", "/");
        }

        public virtual void GetParentNavInfo(out string pNavUrl, out string pNavName)
        {
            pNavUrl = ParentNav.NavUrl;
            pNavName = ParentNav.SecName;
        }
        public virtual void GetCurrentNavInfo(out string cNavUrl, out string cNavName)
        {
            cNavUrl = CurrentNav.NavUrl;
            cNavName = CurrentNav.NavName;
        }
        public virtual bool PermissionCheck( )
        {

            SysNavigation nav = CurrentNav;
            if (nav != null && nav.PermissionRequired == true && nav.SubSysId != 0)
            {
                return PageRoleAssignment == RoleAssignment.Allow;
            }
            return true;
        }
        public SysMember CurrentUser
        {
            get
            {
                return AppContext.Context.User;
            }
        }
        public string PageRelativePath
        {
            get
            {
                return this.ResolveUrl(this.Request.AppRelativeCurrentExecutionFilePath);
            }
        }
        private SysNavigation _currentNav;
        public SysNavigation CurrentNav
        {
            get
            {
                if (_currentNav == null)
                {
                    SysNavigationCollection navs = DB.Select( ).From<SysNavigation>( )
                                                     .Where(SysNavigation.NavUrlColumn).Like(string.Concat(PageRelativePath, "%"))
                        //.AndExpression(SysNavigation.SubSysIdColumn.ColumnName).IsEqualTo(0)
                        //.Or(SysNavigation.SubSysIdColumn.ColumnName).IsEqualTo(CurrentSubSysID)
                        //.CloseExpression( )
                                                     .ExecuteAsCollection<SysNavigationCollection>( );
                    char[] spliter = new char[] { '&' };
                    string pageParams = Request.QueryString.ToString( );
                    foreach (SysNavigation nav in navs)
                    {
                        if (_currentNav == null)
                            _currentNav = nav;
                        try
                        {
                            string strQuery = Utilities.GetQueryString(nav.NavUrl);
                            NameValueCollection querys = HttpUtility.ParseQueryString(strQuery);
                            string act = querys["Cmd"];
                            if (string.IsNullOrEmpty(act))
                                act = querys["Action"];
                            if (Utilities.Compare(act, ActionStr))
                            {
                                _currentNav = nav;
                                if (nav.SubSysId == CurrentSubSysID)
                                    break;
                            }
                            string[] queries = strQuery.Split(spliter, StringSplitOptions.RemoveEmptyEntries);
                            bool bFound = true;
                            foreach (string qry in queries)
                            {
                                bFound &= pageParams.Contains(qry);
                            }
                            if (queries.Length > 0 && bFound)
                            {
                                _currentNav = nav;
                                if (nav.SubSysId == CurrentSubSysID)
                                    break;
                            }
                        }
                        catch { }
                    }
                }
                return _currentNav;
            }
        }
        private SysNavigation _parentNav;
        public SysNavigation ParentNav
        {
            get
            {
                if (_parentNav == null)
                {
                    if (CurrentNav.PId.HasValue && CurrentNav.PId.Value > 0)
                        _parentNav = SysNavigation.FetchByID(CurrentNav.PId);
                    else
                        _parentNav = CurrentNav;
                }
                return _parentNav;
            }
        }
        public SubSystem CurrentSubSys
        {
            get
            {
                return AppContext.Context.CurrentSubSys;
            }
        }
        public int CurrentSubSysID
        {
            get
            {
                return AppContext.Context.CurrentSubSysID;
            }
        }
        public int CurrentRole
        {
            get
            {
                return AppContext.Context.CurrentRole;
            }
        }
        public RoleAssignment PageRoleAssignment
        {
            get
            {
                if (CurrentNav != null)
                {
                    SysRoleAssignment roleAss = DB.Select( )
                                                  .From<SysRoleAssignment>( )
                                                  .Where(SysRoleAssignment.RoleIDColumn)
                                                  .IsEqualTo(CurrentRole)
                                                  .And(SysRoleAssignment.NavIDColumn)
                                                  .IsEqualTo(CurrentNav.Id)
                                                  .ExecuteSingle<SysRoleAssignment>( );
                    if (roleAss != null && roleAss.Allow)
                        return RoleAssignment.Allow;
                    else
                        return RoleAssignment.Reject;
                }
                else
                    return RoleAssignment.Allow;
            }
        }
        private string _siteUrl;
        public string SiteUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_siteUrl))
                    _siteUrl = Utilities.GetSiteUrl( );
                return _siteUrl;
            }
        }
        private string _actionStr;
        public virtual Actions InternalAction
        {
            get
            {
                return Actions.Query;
            }
        }
        public string ActionStr
        {
            get
            {
                if (string.IsNullOrEmpty(_actionStr))
                {
                    _actionStr = EVENTTARGET;
                    if (string.IsNullOrEmpty(_actionStr))
                        _actionStr = this.Request.Params["Action"];
                    if (string.IsNullOrEmpty(_actionStr))
                        _actionStr = this.Request.Params["Cmd"];
                    if (string.IsNullOrEmpty(_actionStr))
                        _actionStr = "Query";
                }
                return _actionStr;
            }
        }
        public Actions Action
        {
            get
            {
                Actions act = ParamAction;
                return act == Actions.Query ? InternalAction : act;
            }
        }
        public Actions ParamAction
        {
            get
            {
                return Utilities.ParseAction(ActionStr);
            }
        }
        public SubSystem ParamSubSys
        {
            get
            {
                return (SubSystem)Enum.Parse(typeof(SubSystem), ParamSubSysStr, true);
            }
        }
        public string ParamSubSysStr
        {
            get
            {
                return Request.Params["subSys"];
            }
        }
        public bool IsDialog
        {
            get
            {
                return Request.Params["IsDlg"] == "1";
            }
        }
        private NameValueCollection _message;
        public NameValueCollection Message
        {
            get
            {
                if (_message == null)
                    _message = new NameValueCollection( );
                return _message;
            }
        }
        private StringDictionary _params;
        public StringDictionary Params
        {
            get
            {
                if (_params == null)
                {
                    _params = new StringDictionary( );
                    foreach (string sKey in Request.Params.AllKeys)
                    {
                        _params.Add(sKey, Request.Params[sKey]);
                    }
                }
                return _params;
            }
        }
        public string EVENTTARGET
        {
            get
            {
                return this.Request.Params[System.Web.UI.Page.postEventSourceID];
            }
        }
        public string EVENTARGUMENT
        {
            get
            {
                return this.Request.Params[System.Web.UI.Page.postEventArgumentID];
            }
        }
        public virtual SysNavigation RenderNav(SysNavigation item, MenuRegion region)
        {
            if (region == MenuRegion.Main)
            {
                if (string.IsNullOrEmpty(item.OthName))
                    item.NavName = item.SecName;
                else
                    item.NavName = item.OthName;
                if (Utilities.Compare(item.NavUrl, "/Company/CompanyItemEdit.aspx") && AppContext.CurrentSysId == (int)SubSystem.Company)
                {
                    var t = CompanyBLL.GetCompanyTypeById(AppContext.Context.User.CompanyId.Value);
                    if (t == CompanyType.AdCompany)
                    {
                        item.NavUrl = "/Company/CompanyAdsClicked.aspx";
                    }
                    else if (t == CompanyType.SpecialCompany)
                    {
                        item.NavUrl = "/Company/CompanyItemUsed.aspx";
                    }
                }
            }

            return item;
        }
        private MasterPage.MasterPageBase _masterPage;
        public MasterPage.MasterPageBase MasterPage
        {
            get
            {
                if (_masterPage == null)
                {
                    System.Web.UI.MasterPage master = this.Master;
                    while (master != null && !string.IsNullOrEmpty(master.MasterPageFile) && (master = master.Master) != null) ;
                    _masterPage = master as MasterPage.MasterPageBase;
                }
                return _masterPage;
            }
        }
        public void PrintErrorMessage(string message)
        {
            Response.Write(string.Format(ERROR_MESSAGE_TEMPLATE, message));
            this.Visible = false;
            //Response.Flush( );
            //Response.End( );
        }
        private SysArea _city;
        public SysArea City
        {
            get
            {
                if (_city == null)
                    _city = AreaBLL.GetCurrentCity( );
                return _city;
            }
        }
        public void LogMessage(string message)
        {
            //if (Request.QueryString["Debug"] == "1")
            {
                Response.Output.Write("<div>{0}</div>", message);
            }
        }
        public bool IsUserLink { get; set; }
        private List<Func<object, string, object, object>> _dataBindHandler;
        public ActionPage BindEvalHandler(Func<object, string, object, object> handler)
        {
            if (handler == null)
                return this;
            if (_dataBindHandler == null)
                _dataBindHandler = new List<Func<object, string, object, object>>( );
            _dataBindHandler.Add(handler);
            return this;
        }
        public object Eval(string expression, int fmtHandlerIndex)
        {
            object dataItem = GetDataItem( );
            object val = DataBinder.Eval(dataItem, expression);
            if (fmtHandlerIndex >= 0 && _dataBindHandler != null && _dataBindHandler.Count > fmtHandlerIndex)
                return _dataBindHandler[fmtHandlerIndex](dataItem, expression, val);
            else
                return val;
        }
        public object Eval(int index)
        {
            var binder = GetDataItem( );
            var container = binder as IDataItemContainer;
            IList dataItem = container != null ? container.DataItem as IList : binder as IList;
            if (dataItem != null && dataItem.Count > index)
                return dataItem[index];
            else
                return null;
        }
        public object Eval( )
        {
            var binder = GetDataItem( );
            var container = binder as IDataItemContainer;
            return container != null ? container.DataItem : binder;
        }
    }
}