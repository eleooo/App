using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;
using SubSonic;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class WebChatBLL
    {
        const string SUPPORT_STATUS = "<font color=\"#006600\">{0}</font>";
        const string SUPPORT_RATING = "<font color=\"green\"><b>{0}</b></font>";
        const string ONLINE_STATS = "<font color=\"red\" >(在线等待回复)</font>";
        const string ACTION_DLG_TEMPLATE = "<span style=\"cursor:pointer;\" onclick=\"actionDlg($(this));\" param=\"{0}\">{1}</span>";
        const string ALIGN_LEFT_CELL_TEMPLATE = "<td style=\"text-align:left;\">{0}</td>";

        private static readonly object _supportLocker = new object( );
        private static readonly object _sellerLocker = new object( );
        private static readonly Dictionary<int, DateTime> Work_Support = new Dictionary<int, DateTime>( );
        private static readonly Dictionary<int, DateTime> Work_Seller = new Dictionary<int, DateTime>( );

        public static string GetStatusString(int nStatus)
        {

            string str = string.Empty;
            switch (nStatus)
            {
                case 1:
                    return "未回复";

                case 2:
                    return "已回复";

                case 3:
                    return "已完成";
            }
            return str;

        }
        public static string GetRatingString(int nRating)
        {

            string str = string.Empty;
            switch (nRating)
            {
                case 1:
                    return "未评";

                case 2:
                    return "不满意";

                case 3:
                    return "满意";
            }
            return str;
        }
        public static string GetSupportInfo(int SID)
        {

            SqlQuery query = DB.Select(Utilities.GetTableColumns(SysSupport.Schema),
                                       "(CASE Support_Rating WHEN 2 THEN '不满意' WHEN 3 THEN '满意' ELSE '未评' END) AS GetStatusString")
                                       .From<SysSupport>( )
                                       .Where(SysSupport.SupportIdColumn).IsEqualTo(SID);
            string xml = query.ExecuteXML("Sys_Supports", "Sys_Support");
            return xml.Replace("<Sys_Supports>", string.Empty).Replace("</Sys_Supports>", string.Empty);
        }
        public static int GetMessageCount(int sid)
        {
            SqlQuery query = DB.Select( ).From<SysSupportMessage>( )
                               .Where(SysSupportMessage.SupportMsgSidColumn).IsEqualTo(sid);
            return query.GetRecordCount( );
        }
        public static string SendMessage(int sid, string msg, bool ask, string photo)
        {
            try
            {
                SysSupport support = GetSupportByID(sid);
                if (string.IsNullOrEmpty(photo))
                    photo = string.Empty;
                else
                    photo = photo.Trim( );
                SysSupportMessage message = new SysSupportMessage
                {
                    SupportMsgSid = sid,
                    SupportMsgFid = AppContextBase.Context.User.Id,
                    SupportMsgDate = DateTime.Now,
                    SupportMsgTid = 0,
                    SupportMsgIsAsk = ask,
                    SupportMsgIsRead = false,
                    SupportMsgMemo = msg,
                    SupportMsgPhoto = photo
                };
                message.Save( );
            }
            catch (Exception ex)
            {
                Logging.Log("WebChatBLL->SendMessage", ex);
                return ex.Message;
            }
            return "SendOk";
        }

        public static string GetChat_ByReply(DataRow msg)
        {
            StringBuilder sb = new StringBuilder( );
            sb.AppendLine("<hr size=1 color=\"#EEBB77\">");
            sb.AppendLine(string.Format("<B>[{0}#]</B> Reply People：{1}； Reply Time：{2}<BR>", msg["Row"], msg[SysMember.Columns.MemberFullname], msg[SysSupportMessage.Columns.SupportMsgDate]));
            sb.AppendLine("---------------------------------------------------------------<BR>");
            sb.AppendLine("<font color=\"#CC0000\">");
            sb.AppendLine(Utilities.ToHTML(msg[SysSupportMessage.Columns.SupportMsgMemo]).Replace("\r\n", "<br/>").Replace("\n\n", "<br/>"));
            sb.AppendLine("</font>");
            string photo = Utilities.ToString(msg[SysSupportMessage.Columns.SupportMsgPhoto]).Trim( );
            if (string.IsNullOrEmpty(photo))
                return sb.ToString( );
            photo = FileUpload.GetFilePath(photo, SaveType.Support);
            if (FileUpload.IsImage(photo))
            {
                sb.AppendLine(string.Format("<p style=text-align:center><img src=\"{0}\" width=500></p>", photo));
            }
            sb.AppendLine(string.Format("<p><b>Attach:</b><a href=\"{0}\" target=_blank>{0}</a></p>", photo));
            return sb.ToString( );
        }
        public static string GetChat_ByAsk(DataRow msg)
        {
            StringBuilder sb = new StringBuilder( );
            sb.AppendLine("<hr size=1 color=\"#EEBB77\">");
            sb.AppendLine(string.Format("<B>[{0}#]</B> Ask People：{1}； Ask Time：{2}<BR>", msg["Row"], msg[SysMember.Columns.MemberFullname], msg[SysSupportMessage.Columns.SupportMsgDate]));
            sb.AppendLine("---------------------------------------------------------------<BR>");
            sb.AppendLine("<font color=\"#CC0000\">");
            sb.AppendLine(Utilities.ToHTML(msg[SysSupportMessage.Columns.SupportMsgMemo]));
            sb.AppendLine("</font>");
            string photo = Utilities.ToString(msg[SysSupportMessage.Columns.SupportMsgPhoto]).Trim( );
            if (string.IsNullOrEmpty(photo))
                return sb.ToString( );
            photo = FileUpload.GetFilePath(photo, SaveType.Support);
            if (FileUpload.IsImage(photo))
            {
                sb.AppendLine(string.Format("<p style=text-align:center><img src=\"{0}\" width=500></p>", photo));
            }
            sb.AppendLine(string.Format("<p><b>Attach:</b><a href=\"{0}\" target=_blank>{0}</a></p>", photo));
            return sb.ToString( );
        }
        public static string GetMessage(int sid)
        {
            SqlQuery query = DB.Select(Utilities.GetTableColumns(SysSupportMessage.Schema), SysMember.Columns.MemberFullname)
                               .From<SysSupportMessage>( )
                               .InnerJoin(SysMember.IdColumn, SysSupportMessage.SupportMsgFidColumn)
                               .Where(SysSupportMessage.SupportMsgSidColumn).IsEqualTo(sid)
                               .OrderAsc(SysSupportMessage.Columns.SupportMsgId).Paged(1, 10000);
            DataTable dtMess = query.ExecuteDataTable( );
            if (dtMess == null || dtMess.Rows.Count == 0)
                return "无内容";
            StringBuilder sb = new StringBuilder( );
            foreach (DataRow row in dtMess.Rows)
            {
                if (!Convert.ToBoolean(row[SysSupportMessage.Columns.SupportMsgIsAsk]))
                    sb.AppendLine(GetChat_ByAsk(row));
                else
                    sb.AppendLine(GetChat_ByReply(row));
            }
            return sb.ToString( );
        }

        public static string UpdateRating(int sid, int Rating, string RatingReason)
        {
            try
            {
                SysSupport support = GetSupportByID(sid);
                support.SupportRating = Rating;
                support.SupportRatingReason = RatingReason;
                support.Save( );
                return string.Empty;
            }
            catch (Exception exception)
            {
                Logging.Log("WebChatBLL->UpdateRating", exception);
                return exception.Message;
            }
        }

        public static string UpdateStatus(int sid, int Status)
        {
            try
            {
                SysSupport support = GetSupportByID(sid);
                support.SupportStatus = Status;
                support.Save( );
                return string.Empty;
            }
            catch (Exception exception)
            {
                Logging.Log("WebChatBLL->UpdateStatus", exception);
                return exception.Message;
            }
        }
        public static void Support_Work_Seller(int SupportID)
        {
            if (HttpContext.Current.Application["Support_Work_Seller"] == null)
            {
                HttpContext.Current.Application["Support_Work_Seller"] = "," + SupportID.ToString( ) + ",";
            }
            else
            {
                string str = HttpContext.Current.Application["Support_Work_Seller"].ToString( );
                if (str.IndexOf("," + SupportID.ToString( ) + ",") == -1)
                {
                    HttpContext.Current.Application["Support_Work_Seller"] = str + SupportID.ToString( ) + ",";
                }
            }
        }

        public static bool Support_GetWork_Seller_IsExists(int SupportID)
        {
            SysSupport support = GetSupportByID(SupportID);
            return Support_GetWork_Seller_IsExistsByUID(support.SupportFid);
        }
        public static bool Support_GetWork_Seller_IsExistsByUID(int uID)
        {
            lock (_sellerLocker)
            {
                return Work_Seller.ContainsKey(uID);
            }
        }

        public static bool Support_GetWork_Support_IsExists(int SupportID)
        {
            lock (_supportLocker)
            {
                return Work_Support.Count > 0;
            }
        }

        public static void Register_Work_Support( )
        {
            lock (_supportLocker)
            {
                int id = AppContextBase.Context.User.Id;
                if (!Work_Support.ContainsKey(id))
                    Work_Support.Add(id, DateTime.Now);
                else
                    Work_Support[id] = DateTime.Now;
            }
        }
        public static void Register_Work_Seller( )
        {
            lock (_sellerLocker)
            {
                int id = AppContextBase.Context.User.Id;
                if (!Work_Seller.ContainsKey(id))
                    Work_Seller.Add(id, DateTime.Now);
                else
                    Work_Seller[id] = DateTime.Now;
            }
        }
        public static void Remove_Work_Support( )
        {
            lock (_supportLocker)
            {
                List<int> removeLst = new List<int>( );
                foreach (KeyValuePair<int, DateTime> pair in Work_Support)
                {
                    TimeSpan ts = (DateTime.Now - pair.Value);
                    if (ts.Hours > 2)
                        removeLst.Add(pair.Key);
                }
                foreach (int id in removeLst)
                {
                    Work_Support.Remove(id);
                }
            }
        }
        public static void Remove_Work_Seller( )
        {
            lock (_sellerLocker)
            {
                List<int> removeLst = new List<int>( );
                foreach (KeyValuePair<int, DateTime> pair in Work_Seller)
                {
                    TimeSpan ts = (DateTime.Now - pair.Value);
                    if (ts.Hours > 1)
                        removeLst.Add(pair.Key);
                }
                foreach (int id in removeLst)
                {
                    Work_Seller.Remove(id);
                }
            }
        }
        public static string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "Support_Subject":
                    isRenderedCell = true;
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE,
                                    string.Concat(string.Format(ACTION_DLG_TEMPLATE, rowData["Support_ID"], rowData[column]),
                                    Support_GetWork_Seller_IsExistsByUID(Convert.ToInt32(rowData[SysSupport.Columns.SupportFid])) ? ONLINE_STATS : string.Empty)
                                    );
                    break;
                case "Support_Status":
                    result = string.Format(SUPPORT_STATUS, WebChatBLL.GetStatusString(Convert.ToInt32(rowData[column])));
                    break;
                case "Support_Rating":
                    result = string.Format(SUPPORT_RATING, WebChatBLL.GetRatingString(Convert.ToInt32(rowData[column])));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        //public static string GetSupportList(DateTime dtBegin, DateTime dtEnd, int status, int pageIndex, int pageSize, string filter)
        //{
        //    Register_Work_Support( );
        //    Remove_Work_Support( );
        //    Eleooo.Web.Controls.UcGridView gridView = new Controls.UcGridView
        //        {
        //            PageIndex = pageIndex,
        //            PageSize = pageSize,
        //            AllowPaper = true
        //        };
        //    gridView.CallPrivateMethod<object>("OnInit", EventArgs.Empty);
        //    if (status == 4)
        //        status = 2;
        //    if (string.IsNullOrEmpty(filter))
        //        filter = "%";
        //    var query = DB.Select(Utilities.GetTableColumns(SysSupport.Schema), SysMember.Columns.MemberFullname).From<SysSupport>( )
        //                            .InnerJoin(SysMember.IdColumn, SysSupport.SupportFidColumn)
        //                            .Where(SysSupport.SupportStatusColumn).IsEqualTo(status)
        //                            .And(SysSupport.SupportDateColumn).IsBetweenAnd(dtBegin, dtEnd)
        //                            .AndEx(SysMember.Columns.MemberPhoneNumber).Like(filter)
        //                            .Or(SysMember.MemberFullnameColumn).Like(filter)
        //                            .CloseEx( )
        //                            .OrderDesc(SysSupport.Columns.SupportDate);
        //    gridView.DataSource = query;
        //    gridView.AddShowColumn(SysSupport.SupportIdColumn)
        //            .AddShowColumn(SysSupport.SupportSubjectColumn)
        //            .AddShowColumn(SysMember.MemberFullnameColumn)
        //            .AddShowColumn(SysSupport.SupportStatusColumn)
        //            .AddShowColumn(SysSupport.SupportRatingColumn)
        //            .AddShowColumn(SysSupport.SupportDateColumn);
        //    gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
        //    return gridView.GetRenderResult( );
        //}
        private static SysSupport GetSupportByID(int sid)
        {
            SysSupport support = SysSupport.FetchByID(sid);
            if (support == null)
                throw new ArgumentException("咨询编号不存在!");
            return support;
        }

        public class WebChatInfo
        {
            public string Support_ID { get; set; }
            public string Support_FID { get; set; }
            public string Support_TID { get; set; }
            public string Support_Item { get; set; }
            public string Support_Type { get; set; }
            public string Support_ProductID { get; set; }
            public string Support_Subject { get; set; }
            public DateTime Support_Date { get; set; }
            public string Support_Attach { get; set; }
            public string Support_Photo { get; set; }
            public string Support_IsRead { get; set; }
            public string Support_Rating { get; set; }
            public string Support_RatingReason { get; set; }
            public string Support_Status { get; set; }
            public DateTime Support_DateReply { get; set; }
            public DateTime Support_DateFinish { get; set; }
            public string GetStatusString { get; set; }
        }
    }
}