using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using Eleooo.Common;

namespace Eleooo.Web
{
    public static class MsnBLL
    {
        //smsUid	用户名	您的SMS用户名
        //smsPwd	安全操作码	您的SMS安全操作码，登陆进SMS平台可以自己设置(注意,这不是登录密码)
        //smsNumber	手机号码	接收此短信的手机号码
        //smsContent	短信的内容	短信的内容支持长短信，单条最长70个字符(106的通道因要签名,所以是65字记一条)
        //smsWantTime	短信的发送时间	短信的发送时间,可以不写这个参数,不写的话默认为立即发送
        //const string MsnUrl = "http://www.ueswt.com/inter.aspx?smsUid=huige&smsPwd=123456&smsNumber={0}&smsContent={1}";
        const string MsnUrl = "http://60.28.200.150/submitdata/service.asmx/g_Submit?sname=dlhgtcqy&spwd=7zHG7UqY&scorpid=&sprdid=1012818&sdst={0}&smsg={1}";
        const string LoginAccount = "huige";
        const string LoginPwd = "123456";
        static readonly string MsgSuffix = "【乐多分】";
        static readonly Encoding __encoding = Encoding.UTF8;

        const string MsnCodeContent = "亲爱的用户：您的验证码为{0}，请输入验证码，即可完成下单。【乐多分】";
        const string MsnChgNoContent = "亲爱的用户：您的验证码为{0}，输入后即可完成账号修改（一年内限修改两次）。【乐多分】";
        const int MsnCodeValidDate = 5;
        public static int GetMsnCodeForChgNo(string phone, out string message)
        {
            int logId = -1;
            if (string.IsNullOrEmpty(phone))
                message = "请输入有效的手机号码^_^";
            else if (!UserBLL.CheckUserName(phone, out message))
                goto lbl_return;
            else if (UserBLL.CheckUserExist(phone))
            {
                logId = -2;
                goto lbl_return;
            }
            else
            {
                string code = Utilities.GenerateCheckCode(4);
                string content = string.Format(MsnChgNoContent, code);
                if (!SendMessageCore(phone, content, code, 0, out logId, out message))
                    logId = -1;
            }
        lbl_return:
            return logId;
        }
        public static int GetMsnCode(string phone, out string message)
        {
            //if (IsPhoneNumHasCheckCode(phone))
            //{
            //    logId = -1;请输入有效的手机号码^_^
            //    message = "上次发送的验证还在有效期内,请过一段时间后再重新获取验证码.";
            //    return false;
            //}
            int logId = -1;
            if (string.IsNullOrEmpty(phone))
                message = "请输入有效的手机号码^_^";
            else if (!UserBLL.CheckUserName(phone, out message))
                goto lbl_return;
            else if (!IsPhoneNumNeedCheck(phone) && UserBLL.CheckUserExist(phone))
            {
                logId = -2;
                goto lbl_return;
            }
            else
            {
                string code = Utilities.GenerateCheckCode(4);
                string content = string.Format(MsnCodeContent, code);
                if (!SendMessageCore(phone, content, code, 0, out logId, out message))
                    logId = -1;
            }
            lbl_return:
            return logId;
        }
        public static bool IsPhoneNumNeedCheck(string phone)
        {
            var query = DB.Select( ).Top("1").From<SysMsnLog>( )
                          .Where(SysMsnLog.PhoneNumberColumn).IsEqualTo(phone)
                          .And(SysMsnLog.MsnCodeColumn).IsNotNull( )
                          .And(SysMsnLog.IsCheckedColumn).IsEqualTo(true);
            return query.GetRecordCount( ) == 0;
        }
        public static bool IsPhoneNumHasCheckCode(string phone)
        {
            SysMsnLog log = null;
            var query = DB.Select( ).Top("1").From<SysMsnLog>( )
                          .Where(SysMsnLog.PhoneNumberColumn).IsEqualTo(phone)
                          .And(SysMsnLog.MsnCodeColumn).IsNotNull( )
                          .And(SysMsnLog.IsCheckedColumn).IsEqualTo(false)
                          .OrderDesc(SysMsnLog.MsnDateColumn.ColumnName);
            using (var dr = query.ExecuteReader( ))
            {
                if (dr.Read( ))
                {
                    log = new SysMsnLog( );
                    log.Load(dr);
                }
            }
            if (log != null && log.MsnDate.HasValue)
            {
                var ts = DateTime.Now - log.MsnDate.Value;
                return ts.Minutes <= 10;
            }
            return false;
        }
        public static bool CheckPhoneNumCode(string phone, string code, int logId)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            SysMsnLog log = null;
            var query = DB.Select( ).Top("1").From<SysMsnLog>( )
                          .Where(SysMsnLog.PhoneNumberColumn).IsEqualTo(phone)
                          .And(SysMsnLog.MsnCodeColumn).IsEqualTo(code)
                          .And(SysMsnLog.IsCheckedColumn).IsEqualTo(false)
                          .And(SysMsnLog.IdColumn).IsEqualTo(logId)
                          .OrderDesc(SysMsnLog.MsnDateColumn.ColumnName);
            using (var dr = query.ExecuteReader( ))
            {
                if (dr.Read( ))
                {
                    log = new SysMsnLog( );
                    log.Load(dr);
                }
            }
            if (log != null && log.MsnDate.HasValue)
            {
                var ts = DateTime.Now - log.MsnDate.Value;
                log.IsChecked = true;
                log.Save( );
                return ts.Minutes <= 10;
            }
            return false;
        }
        public static bool SendMessage(string phone, string content, int orderId, out string message, out int logId)
        {
            return SendMessageCore(phone, content, null, orderId, out logId, out message);
        }

        private static string EncodeParam(string param)
        {
            return HttpUtility.UrlEncode(param, __encoding);
        }
        private static string BuildRequestParam(string phone, string content)
        {
            return string.Format(MsnUrl, phone, EncodeParam(content));
        }
        private static string FormatContent(string content)
        {
            int index = content.LastIndexOf(MsgSuffix);
            StringBuilder sb = new StringBuilder();
            foreach (var c in content)
            {
                if (c != '\0')
                    sb.Append(c);
            }
            if (index < 0)
                sb.Append(MsgSuffix);
            return sb.ToString( );
        }
        private static bool SendMessageCore(string phone, string content, string checkCode, int orderId, out int logId, out string message)
        {
            //BackgroundWorker.DoWork<int>(logId, (o) =>
            //{
            message = "发送成功";
            content = FormatContent(content);
            SysMsnLog log = new SysMsnLog( )
            {
                MsnDate = DateTime.Now,
                MsnContent = content,
                PhoneNumber = phone,
                OrderId = orderId,
                MsnCode = checkCode,
                IsChecked = false,
                Status = null
            };
            bool isSuccess = true;
#if !NoMsn
            try
            {
                var request = (HttpWebRequest)HttpWebRequest.Create(BuildRequestParam(phone, content));
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                request.Method = "GET";
                request.Timeout = 5000;
                MsnResult result = new MsnResult( );
                System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                using (WebResponse hs = request.GetResponse( ))
                {
                    using (Stream stream = hs.GetResponseStream( ))
                    {
                        //using (StreamReader reader = new StreamReader(stream, __encoding))
                        //{
                        //    message = reader.ReadToEnd( );
                        //}
                        //stream.Position = 0L;
                        using (XmlReader xr = XmlReader.Create(stream))
                        {
                            string name = null;
                            while (xr.Read( ))
                            {
                                if (xr.NodeType == XmlNodeType.Element)
                                {
                                    name = xr.Name;
                                }
                                else if (xr.NodeType == XmlNodeType.Text)
                                    result.SetResult(name, xr.ReadString( ));
                            }
                        }
                    }
                }
                isSuccess = result.State == "0";
                if (!isSuccess)
                    message = result.MsgState;
            }
            catch (Exception ex)
            {
                message = ex.Message + Environment.NewLine + ex.StackTrace;
                Logging.Log("MsnBLL->SendMessageCore", ex);
            }
#endif
            log.Status = message;
            log.Save( );
            logId = log.Id;
            return isSuccess;
            //});
        }
        class MsnResult
        {
            private Dictionary<string, string> _result;
            private Dictionary<string, string> Result
            {
                get
                {
                    if (_result == null)
                        _result = new Dictionary<string, string>( );
                    return _result;
                }
            }
            public void SetResult(string key, string value)
            {
                Result[key] = value;
            }
            public string State
            {
                get
                {
                    if (Result.ContainsKey("State"))
                        return Result["State"];
                    else
                        return null;
                }
            }
            public string MsgID
            {
                get
                {
                    if (Result.ContainsKey("MsgID"))
                        return Result["MsgID"];
                    else
                        return null;
                }
            }
            public string MsgState
            {
                get
                {
                    if (Result.ContainsKey("MsgState"))
                        return Result["MsgState"];
                    else
                        return null;
                }
            }
            public string Reserve
            {
                get
                {
                    if (Result.ContainsKey("Reserve"))
                        return Result["Reserve"];
                    else
                        return null;
                }
            }
        }
    }
}