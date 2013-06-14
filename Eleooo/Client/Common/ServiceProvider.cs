using System;
using System.Collections.Generic;
using System.Text;
using Eleooo.Client.WebServiceProvider;
using Eleooo.DAL;
using System.Data;
using System.Net;
using SubSonic;
using SubSonic.Utilities;
using System.IO;

namespace Eleooo.Client
{
    public class ServiceProvider
    {
        [ThreadStatic]
        private static WebRestService _provider;
        public static WebRestService Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new WebRestService( );
                    _provider.CookieContainer = Cookies;
#if DEBUG           
                    _provider.Url = "http://localhost:4726/WebRestServices/WebRestService.asmx";
#endif
                }
                System.Net.ServicePointManager.Expect100Continue = false;
                return _provider;
            }
        }
        public static void Init( )
        {
            _provider = null;
            _cookies = null;
        }
        private static CookieContainer _cookies;
        public static CookieContainer Cookies
        {
            get
            {
                if (_cookies == null)
                    _cookies = new CookieContainer( );
                return _cookies;
            }
        }
        private static ServiceProvider _service;
        public static ServiceProvider Service
        {
            get
            {
                if (_service == null)
                    _service = new ServiceProvider( );
                return _service;
            }
        }
        private static string _host;
        public static string Host
        {
            get
            {
                if (string.IsNullOrEmpty(_host))
                {
                    Uri uri = new Uri(Provider.Url);
                    _host = string.Concat("http://", uri.Host);
                }
                return _host;
            }
        }
        public int Login(string userName, string userPassword, int subSys, out SysMember user)
        {
            DataTable dtUser;
            int nRet = Provider.ClientLogin(userName, userPassword, subSys, out dtUser);
            user = SubSonic.Utilities.EntityFormat.TableToEntity<SysMember>(dtUser);
            return nRet;
        }
        public bool GetMemberForOrder(int userID, string phoneNum, out SysMember user, out DataTable dtUserInfo, out SysMemberCash userCash, out string message)
        {
            DataTable dtUser;
            DataTable dtCash;
            bool bRet = Provider.GetMemberForOrder(userID, phoneNum, out dtUser, out dtUserInfo, out dtCash, out message);
            user = SubSonic.Utilities.EntityFormat.TableToEntity<SysMember>(dtUser);
            userCash = SubSonic.Utilities.EntityFormat.TableToEntity<SysMemberCash>(dtCash);
            return bRet;
        }
        public bool GetMemberForCash(int userID, string phoneNum, out SysMember user, out DataTable dtUserInfo, out string message)
        {
            DataTable dtUser;
            bool bRet = Provider.GetMemberForCash(userID, phoneNum, out dtUser, out dtUserInfo, out message);
            user = SubSonic.Utilities.EntityFormat.TableToEntity<SysMember>(dtUser);
            return bRet;
        }
        public int SaveOrder(Order orderData, SysMember orderUser, string userPwd, out string message)
        {
            DataTable dtOrder = SubSonic.Utilities.EntityFormat.EntityToTable<Order>(orderData);
            return Provider.SaveOrderForClient(dtOrder, orderUser.Id, userPwd, out message);
        }
        public int SaveCash(SysMemberCash cashData, SysMember cashUser, out string message)
        {
            DataTable dtCash = SubSonic.Utilities.EntityFormat.EntityToTable<SysMemberCash>(cashData);
            return Provider.SaveCashForClient(dtCash, cashUser.Id, out message);
        }
        public bool SaveCompanyAd(SysCompanyAd companyAd, DataTable dtpoint, string fileAbsPath, out int adsID, out string message)
        {
            byte[] buff = null;
            string fileName = string.Empty;
            message = string.Empty;
            adsID = 0;
            if (!string.IsNullOrEmpty(fileAbsPath))
            {
                FileInfo fi = new FileInfo(fileAbsPath);
                if (!fi.Exists)
                {
                    message = "文件不存在!";
                    return false;
                }
                fileName = fi.Name;
                using (Stream stream = fi.OpenRead( ))
                {
                    buff = new byte[stream.Length];
                    stream.Position = 0L;
                    stream.Read(buff, 0, buff.Length);
                }
            }
            DataTable dtAd = SubSonic.Utilities.EntityFormat.EntityToTable<SysCompanyAd>(companyAd);
            try
            {
                return Provider.SaveCompanyAdForClient(dtAd, dtpoint, buff, fileName, out adsID, out message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
        public DataTable GetCompanyInfo(int companyID)
        {
            return Provider.GetCompanyInfo(companyID);
        }
        public bool UploadFile(string fileAbsPath, int saveType, int fileType, out string savedName, out string message)
        {
            return UploadFile(fileAbsPath, saveType, fileType, string.Empty, out savedName, out message);
        }
        public bool UploadFile(string fileAbsPath, int saveType, int fileType,string folderName, out string savedName, out string message)
        {
            byte[] buff = null;
            savedName = string.Empty;
            message = string.Empty;
            FileInfo fi = new FileInfo(fileAbsPath);
            if (!fi.Exists)
            {
                message = "文件不存在!";
                return false;
            }
            using (Stream stream = fi.OpenRead( ))
            {
                buff = new byte[stream.Length];
                stream.Position = 0L;
                stream.Read(buff, 0, buff.Length);
            }
            try
            {
                savedName = Provider.UploadFile(buff, fi.Name, fileType, saveType, folderName, out message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return !string.IsNullOrEmpty(savedName);
        }
        public byte[] GetFile(string fileName, int saveType)
        {
            try
            {
                return Provider.GetSaveFile(fileName, saveType);
            }
            catch { return null; }
        }

        public T ExecuteSingle<T>(SqlQuery query) where T : ActiveRecord<T>, new( )
        {
            if (query == null)
                return default(T);
            else
                return ExecuteSingle<T>(query.BuildCommand( ));
        }
        public T ExecuteSingle<T>(QueryCommand cmd) where T : ActiveRecord<T>, new( )
        {
            if (cmd == null)
                return default(T);
            return EntityFormat.TableToEntity<T>(GetDataTable(cmd));
        }
        public List<T> ExecuteAsCollection<T>(SqlQuery query) where T : ActiveRecord<T>, new( )
        {
            return ExecuteAsCollection<T>(query.BuildCommand( ));
        }
        public List<T> ExecuteAsCollection<T>(QueryCommand cmd) where T : ActiveRecord<T>, new( )
        {
            List<T> lstT = new List<T>( );
            DataTable dt = GetDataTable(cmd);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    T t = new T( );
                    t.Load(row);
                    lstT.Add(t);
                }
            }
            return lstT;
        }
        public int ExecuteQuery(QueryCommand cmd)
        {
            DataTable dtParam;
            string sql = EntityFormat.CmdToSql(cmd, out dtParam);
            return Provider.ExecuteQuery(sql, dtParam);
        }
        public int ExecuteQuery(SqlQuery query)
        {
            return ExecuteQuery(query.BuildCommand( ));
        }
        public object ExecuteScalar(QueryCommand cmd)
        {
            DataTable dtParam;
            string sql = EntityFormat.CmdToSql(cmd, out dtParam);
            return Provider.ExecuteScalar(sql, dtParam);
        }
        public object ExecuteScalar(SqlQuery query)
        {
            return ExecuteScalar(query.BuildCommand( ));
        }
        public int SaveEntity<T>(T t) where T : ActiveRecord<T>, new( )
        {
            if (t == null)
                return 0;

            if (t.IsNew)
            {
                object reVal = ExecuteScalar(t.GetInsertCommand(AppContext.User.Id.ToString( )));
                if (reVal != null && reVal != DBNull.Value)
                {
                    t.SetColumnValue(t.GetSchema( ).PrimaryKey.ColumnName, reVal);
                }
                return 1;
            }
            else
                return ExecuteQuery(t.GetUpdateCommand(AppContext.User.Id.ToString( )));

        }
        public DataTable GetDataTable(QueryCommand cmd)
        {
            if (cmd == null)
                return null;
            DataTable dtParam;
            string sql = EntityFormat.CmdToSql(cmd, out dtParam);
            return DataFormat.ConvertToDataTableUnZip(Provider.GetDataTableByParam(sql, dtParam));
        }
        public DataTable GetDataTable(SqlQuery query)
        {
            return GetDataTable(query.BuildCommand( ));
        }
        public bool IsOwnerUser(int userID, int companyID)
        {
            return Provider.IsOwnerUser(userID, companyID);
        }
        public DataTable GetUserCompanyItems(string phoneNum, string userPwd, string userFinger, int companyID, out int flag, out string message)
        {
            flag = 1;
            try
            {
                byte[] buff = Provider.GetUserCompanyItems(phoneNum, userPwd, userFinger, companyID, out flag, out message);
                return SubSonic.Utilities.DataFormat.ConvertToDataTableUnZip(buff);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
        }
        public bool OrderCompanyItem(int companyID, int itemID, string MemberPwd, string MemberFinger, out string message)
        {
            try
            {
                return Provider.OrderCompanyItem(companyID, itemID, MemberPwd, MemberFinger, out message);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
