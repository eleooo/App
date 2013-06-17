using System;
using System.Collections.Generic;
using System.Web;
using SubSonic;
using System.Linq;
using SubSonic.Utilities;
using System.Web.Security;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Eleooo.Common
{
    public static class Utilities
    {
        private static readonly char[] _PathSpliter = new char[] { '/', '\\' };
        private static readonly string _defaultServicesName = "UnknowServices";

        private const string RESULT_MESSAGE =
        @"<Result><Success>{0}</Success><Error_Message>{1}</Error_Message>{2}</Result>";
        private const string ALERT_MESSAGE = @"
<script type='text/javascript'>
    alert('{0}');
</script>";
        private const string REDIRECT_SCRIPT = @"
<script type='text/javascript'>
    document.location.href = '{0}';
</script>";
        private const string REDIRECT_MESSAGE = @"
<script type='text/javascript'>
    alert('{0}');
    document.location.href = '{1}';
</script>";

        public static T GetInstance<T>(string key, Dictionary<string, object> initProperty = null)
        {
            if (HttpContext.Current.Items.Contains(key))
                return (T)HttpContext.Current.Items[key];
            else
            {
                T t = Activator.CreateInstance<T>();
                if (initProperty != null && initProperty.Count > 0)
                {
                    foreach (string initKey in initProperty.Keys)
                        t.SetPrivateProperty(initKey, initProperty[initKey]);
                }
                HttpContext.Current.Items.Add(key, t);
                return t;
            }
        }

        public static string ResolveUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;
            else if (url.StartsWith("/"))
                return url;
            else if (url.StartsWith("~/"))
                return url.Replace("~", string.Empty);
            else if (url.StartsWith("~"))
                return url.Replace("~", "/");
            else
                return url;
        }
        public static string GetQueryString(string url)
        {
            string query = string.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                string[] sec = url.Split('?');
                if (sec.Length > 1)
                {
                    query = sec[sec.Length - 1];
                }
            }
            return query;
        }
        public static string ObjToJSON(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static object JSONToObj(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        }
        public static T JSONToObj<T>(string json)
            where T : new()
        {
            if (string.IsNullOrEmpty(json))
                return default(T);
            else
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        public static string QuoteStr(string sourceStr)
        {
            return string.Format("\"{0}\"", sourceStr);
        }
        public static void AddCache(string key, Object obj)
        {
            AddCache(key, obj, 24);
        }
        public static void AddCache(string key, Object obj, int hoursSpan)
        {
            Object isObj = HttpContext.Current.Cache[key];
            if (isObj != null)
            {
                HttpContext.Current.Cache.Remove(key);
                isObj = null;
            }
            HttpContext.Current.Cache.Add(key,
                obj,
                null,
                DateTime.Now.AddHours(hoursSpan),  //缓存时间为一小时
                System.Web.Caching.Cache.NoSlidingExpiration,
                System.Web.Caching.CacheItemPriority.Default,
                null);
        }
        public static Object GetCache(string key)
        {
            Object obj = HttpContext.Current.Cache[key];
            return obj;
        }
        public static T GetCache<T>(string key)
        {
            object obj = GetCache(key);
            if (obj != null)
                return (T)obj;
            else
                return default(T);
        }
        public static void DelCache(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
        public static void AddCookie(string key, string value)
        {
            HttpCookie cookie = GetCookie(key);
            if (cookie == null)
            {
                cookie = new HttpCookie(key);
                //HttpContext.Current.Response.SetCookie(cookie);
            }
            cookie.Value = value;
            HttpContext.Current.Request.Cookies.Add(cookie);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static string GetCookieValue(string key)
        {
            HttpCookie cookie = GetCookie(key);
            if (cookie == null)
                return string.Empty;
            else
                return cookie.Value;
        }
        public static HttpCookie GetCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie == null)
                return HttpContext.Current.Response.Cookies[key];
            else
                return cookie;
        }
        public static FormsAuthenticationTicket GenFormsAuthenticationTicket(int userId, SubSystem loginSys, LoginSystem sys = LoginSystem.Web)
        {
            double dTimeOut = 24;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket((int)sys,
            userId.ToString(),
            DateTime.Now,
            DateTime.Now.AddHours(dTimeOut == 0 ? 60 : dTimeOut),
            false,
            ((int)loginSys).ToString());
            return ticket;
        }
        public static string GenFormsAuthenticationTicketValue(int userId, SubSystem loginSys, LoginSystem sys = LoginSystem.Web)
        {
            return FormsAuthentication.Encrypt(GenFormsAuthenticationTicket(userId, loginSys, sys));
        }
        public static void LoginSigIn(int userId, SubSystem loginSys, LoginSystem sys = LoginSystem.Web)
        {

            FormsAuthentication.SignOut();
            var ticket = GenFormsAuthenticationTicket(userId, loginSys, sys);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            authCookie.Expires = ticket.Expiration;
            //HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            //HttpContext.Current.Request.Cookies.Clear( );
            HttpContext.Current.Response.Cookies.Add(authCookie);
            HttpContext.Current.Request.Cookies.Add(authCookie);
            //AppContext.Context.RegLoginUser(loginUser);
        }
        public static FormsAuthenticationTicket GetTicket()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (string.IsNullOrEmpty(cookie.Value))
                    return null;
                return FormsAuthentication.Decrypt(cookie.Value);
            }
            catch { return null; }
        }
        public static void LoginOutSigOut()
        {
            //OrderMealBLL.RecycleAdminOrder( );
            FormsAuthentication.SignOut();
            HttpCookie authCookie = HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName];
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            authCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            authCookie.Expires = DateTime.MinValue;
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }
        private static readonly Type _NameObjectCollectionBaseType = typeof(System.Collections.Specialized.NameObjectCollectionBase);
        public static void RemoveCookie(string key)
        {
            var p = HttpContext.Current.Request.Params;
            if (p[key] != null)
            {
                _NameObjectCollectionBaseType.SetTypePrivateProperty(p, "IsReadOnly", false, null);
                p.Remove(key);
                _NameObjectCollectionBaseType.SetTypePrivateProperty(p, "IsReadOnly", true, null);
            }
            var cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Today.AddDays(-1);
                cookie.Value = null;
            }
            cookie = HttpContext.Current.Response.Cookies[key];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Today.AddDays(-1);
                cookie.Value = null;
            }
        }
        public static void AutoLogin(string sAuthCookie)
        {
            string sCookieName = FormsAuthentication.FormsCookieName;
            HttpCookie loginCookie = HttpContext.Current.Request.Cookies.Get(sCookieName);
            if (loginCookie == null && !string.IsNullOrEmpty(sAuthCookie))
            {
                loginCookie = new HttpCookie(sCookieName, sAuthCookie);
                HttpContext.Current.Request.Cookies.Add(loginCookie);
            }
        }

        public static string GetSiteUrl()
        {
            string port = HttpContext.Current.Request.ServerVariables[ServerVariable.SERVER_PORT];

            if (port == null || port == Ports.HTTP || port == Ports.HTTPS)
                port = String.Empty;
            else
                port = String.Concat(":", port);

            string protocol = HttpContext.Current.Request.ServerVariables[ServerVariable.SERVER_PORT_SECURE];
            if (protocol == null || protocol == "0")
                protocol = ProtocolPrefix.HTTP;
            else
                protocol = ProtocolPrefix.HTTPS;

            string appPath = HttpContext.Current.Request.ApplicationPath;

            if (appPath == "/")
                appPath = String.Empty;

            string sOut = protocol + HttpContext.Current.Request.ServerVariables[ServerVariable.SERVER_NAME] + port + appPath;

            return sOut;
        }
        public static bool Compare(string strA, string strB)
        {
            return Compare(strA, strB, true);
        }
        public static bool Compare(string strA, string strB, bool ignoreCase)
        {
            return string.Compare(strA, strB, ignoreCase) == 0;
        }
        public static Actions ParseAction(string actionStr)
        {
            try
            {
                return (Actions)(Enum.Parse(typeof(Actions), actionStr, true));
            }
            catch
            {
                return Actions.Query;
            }
        }

        public static string DESEncrypt(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            else
                return SubSonic.Utilities.Encrypt.DESEncrypt(source);
            //return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5"); ;
        }
        public static string DESDecrypt(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            else
                return SubSonic.Utilities.Encrypt.DESDecrypt(source);
        }
        public static string SHA1(string source)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "SHA1");
        }

        public static void RegisterScript(string key, string scriptPath)
        {
            string script = "<script src=\"{0}\" type=\"text/javascript\"></script>";
            AppContextBase.Page.ClientScript.RegisterClientScriptBlock(HttpContext.Current.Handler.GetType(), key, string.Format(script, scriptPath));
        }
        public static void RegisterScriptBlock(string key, string script)
        {
            string scriptDeclare =
            @"<script type='text/javascript'>
               {0}
              </script>";
            AppContextBase.Page.ClientScript.RegisterClientScriptBlock(HttpContext.Current.Handler.GetType(), key, string.Format(scriptDeclare, script));
        }
        public static void RegisterCSS(string key, string cssPath)
        {
            string css = "<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />";
            AppContextBase.Page.ClientScript.RegisterClientScriptBlock(AppContextBase.Page.GetType(), key, string.Format(css, cssPath));
        }
        public static void ShowMessage(string title, string content)
        {
            ShowMessage(title, content, true);
        }
        public static void ShowMessage(string title, string content, bool regScript)
        {
            if (regScript)
            {
                RegisterScript("tipswindownScript", "/Scripts/jquery.tipswindown/jquery.tipswindown.js");
                RegisterCSS("tipswindownCSS", "/Scripts/jquery.tipswindown/jquery.tipswindown.css");
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine(string.Format("tipsWindown('{0}', 'text:{0}', 400, 250, 'true', '', 'true', '');", title, content));
            sb.AppendLine("</script>");
            HttpContext.Current.Response.Write(sb.ToString());
            //HttpContext.Current.Response.Flush( );
            //HttpContext.Current.Response.End( );
            AppContextBase.Page.Visible = false;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static string GetTableColumns(TableSchema.Table table)
        {
            return GetTableColumns(table, null);
        }
        public static string GetTableColumns(TableSchema.Table table, params TableSchema.TableColumn[] notNeedCols)
        {
            return GetTableColumns(table, true, notNeedCols);
        }
        public static string GetTableColumns(TableSchema.Table table, bool PreTableName = true, params TableSchema.TableColumn[] notNeedCols)
        {
            List<string> columns = new List<string>();
            List<TableSchema.TableColumn> cols = new List<TableSchema.TableColumn>();
            if (notNeedCols != null && notNeedCols.Length > 0)
                cols.AddRange(notNeedCols);
            foreach (TableSchema.TableColumn tc in table.Columns)
            {
                if (!cols.Contains(tc))
                    columns.Add(PreTableName ? string.Concat(table.Name, ".", tc.ColumnName) : tc.ColumnName);
            }
            return string.Join(", ", columns.ToArray());
        }
        public static string GetTableColumn(TableSchema.TableColumn col)
        {
            //return string.Concat(col.Table.Name, ".", col.ColumnName);
            return GetTableColumn(col, string.Empty);
        }
        public static string GetTableColumn(TableSchema.TableColumn col, string aliasName)
        {
            return string.Concat(col.Table.Name, ".", col.ColumnName, string.IsNullOrEmpty(aliasName) ? string.Empty : string.Concat(" AS [", aliasName, "]"));
        }
        public static string[] GetTableColumn(params TableSchema.TableColumn[] cols)
        {
            if (cols == null || cols.Length == 0)
                return new string[0];
            return cols.Select(col => string.Concat(col.Table.TableName, ".", col.ColumnName)).ToArray();
        }
        public static TableSchema.TableColumn GetTableColumn<T>(string colName)
            where T : ReadOnlyRecord<T>, new()
        {
            T t = new T();
            TableSchema.Table table = t.GetSchema();
            return table.Columns.GetColumn(colName);
        }

        public static string ToBase64String(string source)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(source));
        }
        public static string FromBase64String(string source)
        {
            byte[] arr = Convert.FromBase64String(source);
            return System.Text.Encoding.UTF8.GetString(arr);
        }
        public static DateTime ToDateTime(string datetime)
        {
            DateTime dt;
            if (!string.IsNullOrEmpty(datetime) && DateTime.TryParse(datetime, out dt))
                return dt;
            else
                return DateTime.MinValue.AddYears(1753);
        }
        public static DateTime ToDateTime(object datetime)
        {
            return ToDateTime(Convert.ToString(datetime));
        }
        public static string ToDate(string datetime)
        {
            DateTime dt;
            if (DateTime.TryParse(datetime, out dt) && dt > DateTime.MinValue.AddYears(1753))
                return dt.ToString("yyyy-MM-dd");
            else
                return string.Empty;
        }
        public static string ToDate(object datetime)
        {
            return ToDate(ToString(datetime));
        }

        public static T Max<T>(params T[] args)
        {
            T t = default(T);
            if (args != null || args.Length > 0)
            {
                foreach (T item in args)
                {
                    string v1 = Utilities.ToHTML(item);
                    string v2 = Utilities.ToHTML(t);
                    if (string.Compare(v1, v2, true) > 0)
                        t = item;
                }
            }
            return t;
        }
        public static string StripHTML(string content)
        {
            return SubSonic.Sugar.Strings.StripHTML(content);
        }
        public static string ToHTML(string content)
        {
            if (SubSonic.Sugar.Numbers.IsNumber(content))
                return content;
            else if (SubSonic.Sugar.Dates.IsDate(content))
                return ToDate(content);
            else
                return HttpUtility.HtmlDecode(content)
                                  .Replace("\r\n", "<br/>")
                                  .Replace("\n\n", "<br/>")
                                  .Replace("\n", "<br />")
                                  .Replace(" ", "&nbsp;");
        }
        public static string ToHTML(object content)
        {
            if (IsNull(content))
                return string.Empty;
            else
                return ToHTML(content.ToString());
        }
        public static bool IsNull(object val)
        {
            if (val == DBNull.Value)
                return true;
            if (val == null)
                return true;
            return false;
        }

        public static void PrintRestError(string message)
        {
            PrintRestResult(string.Empty, false, message);
        }
        public static void PrintRestResult(string result, bool isSuccess, string message)
        {
            HttpContext.Current.Response.ContentType = "text/xml; charset=utf-8";
            HttpContext.Current.Response.Write(string.Format(RESULT_MESSAGE, isSuccess, message, result));
            //HttpContext.Current.Response.Flush( );
            //HttpContext.Current.Response.End( );
        }
        public static void PrintRestResult(string result)
        {
            if (string.IsNullOrEmpty(result))
                PrintRestError("没找到数据");
            else
                PrintRestResult(result, true, string.Empty);
        }
        public static string ToString(object obj)
        {
            if (Utilities.IsNull(obj))
                return string.Empty;
            else if (obj is string)
                return (string)obj;
            else
                return obj.ToString();
        }

        public static decimal ToDecimal(object obj)
        {
            if (obj == null)
                return 0;
            decimal d;
            decimal.TryParse(obj.ToString(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.CurrentCulture, out d);
            return d;
        }
        public static int ToInt(object obj)
        {
            if (IsNull(obj))
                return 0;
            bool b;
            if (bool.TryParse(obj.ToString(), out b))
                return b ? 1 : 0;
            return (int)ToDecimal(obj);
        }
        public static bool ToBool(object obj)
        {
            if (IsNull(obj))
                return false;
            bool b;
            if (obj is bool)
                return (bool)obj;
            if (bool.TryParse(obj.ToString(), out b))
                return b;
            else if (ToInt(obj) > 0)
                return true;
            else
                return false;
        }
        public static void Redirect(string url, bool endResp)
        {
            Redirect(url);
        }
        public static void Redirect(string url, params object[] args)
        {
            Redirect(string.Format(url, args));
        }
        public static void Redirect(string url)
        {
            var p = AppContextBase.Page;
            HttpContext.Current.Response.ContentType = "text/html; charset=utf-8";
            HttpContext.Current.Response.Output.Write(REDIRECT_SCRIPT, url);
            //HttpContext.Current.Response.Write(string.Format(REDIRECT_SCRIPT, url));
            if (p != null)
                p.Visible = false;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        public static void ShowMessageRedirect(string message, string redirector)
        {
            var p = AppContextBase.Page;
            HttpContext.Current.Response.ContentType = "text/html; charset=utf-8";
            HttpContext.Current.Response.Write(string.Format(REDIRECT_MESSAGE, message, redirector));
            //HttpContext.Current.Response.Flush( );
            if (p != null)
                p.Visible = false;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //AppContext.Page.Response.End( );
        }
        public static void ShowMessageRedirect(string message)
        {
            ShowMessageRedirect(message, "/Default.aspx");
        }
        public static void ShowMessageNoEnd(string message)
        {
            AppContextBase.Page.Response.Write(string.Format(ALERT_MESSAGE, message));
        }
        public static string GetWebServicesResult(int code, string message)
        {
            return string.Concat("{\"code\":", code, ",\"message\":\"", message, "\"}");
        }
        public static string GetAllLikeQuery(string queryText)
        {
            if (string.IsNullOrEmpty(queryText))
                return "%";
            else
                return "%" + queryText + "%";
        }

        public static string GenerateCheckCode(int length)
        {
            Random random = new Random();
            string str = random.Next().ToString().Substring(0, length);
            return str;
        }

        public static string GetTypeVersion(Type type)
        {
            var ass = type.Assembly;
            return File.GetLastWriteTime(ass.Location).Ticks.ToString();
            //return File.GetLastWriteTime(ass.Location).ToString("yyyyMMdd");
        }

        public static ServicesAction GetServicesAction(HttpContext context)
        {
            var pathInfo = (context.Request.PathInfo ?? string.Empty).Split(_PathSpliter, StringSplitOptions.RemoveEmptyEntries);
            string name = pathInfo.Length > 0 ? (pathInfo[0].ToLower() ?? _defaultServicesName) : _defaultServicesName;
            return new ServicesAction { Name = name, Method = (pathInfo.Length > 1 ? pathInfo[1] : Actions.Query.ToString()).ToLower() };
        }

        private static readonly char[] _addressSpliter = new char[] { '|' };
        public static void GetAddrFloorRoom(string addr, out string seat, out string floor, out string room)
        {
            if (!string.IsNullOrEmpty(addr))
            {
                var addrs = addr.Split(_addressSpliter, StringSplitOptions.RemoveEmptyEntries);
                if (addrs.Length == 3)
                {
                    seat = addrs[0];
                    floor = addrs[1];
                    room = addrs[2];
                }
                else
                {
                    floor = addrs.Length == 2 ? addrs[0] : string.Empty;
                    room = addrs.Length == 2 ? addrs[1] : addr;
                    seat = string.Empty;
                }
            }
            else
            {
                seat = floor = room = string.Empty;
            }
        }
        public static string ConcatAddres(object addr)
        {
            return ToString(addr).Replace("||", string.Empty).Replace('|', '楼');
        }

        public static int CalcPageCount(int pageSize, int total)
        {
            int result;
            int pageCount = Math.DivRem(total, pageSize, out result);
            if (result > 0)
                pageCount++;
            return pageCount;
        }
    }
}