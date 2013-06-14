using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Eleooo.Common;

namespace Eleooo.Web.WebRestServices
{
    /// <summary>
    /// WebChat 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebChat : System.Web.Services.WebService
    {

        [WebMethod]
        public void Support_GetInfo(int SID)
        {
            Utilities.PrintRestResult(WebChatBLL.GetSupportInfo(SID));
        }

        [WebMethod]
        public void GetSupportList(DateTime dtBegin,DateTime dtEnd, int status,int pageIndex,int pageSize,string filter)
        {
            //HttpContext.Current.Response.Write(WebChatBLL.GetSupportList(dtBegin, dtEnd, status, pageIndex, pageSize, filter));
        }

        [WebMethod]
        public int Support_GetMessageCount(int sid)
        {
            return WebChatBLL.GetMessageCount(sid);
        }

        [WebMethod]
        public string Support_SendMessage(int sid, string msg, bool ask, string photo)
        {
            return WebChatBLL.SendMessage(sid, msg, ask, photo);
        }

        [WebMethod]
        public string Support_GetMessageList(int sid)
        {
            return WebChatBLL.GetMessage(sid);
        }

        [WebMethod]
        public string Support_UpdateRating(int sid, int Rating, string RatingReason)
        {
            return WebChatBLL.UpdateRating(sid, Rating, RatingReason);
        }
        [WebMethod]
        public string Support_UpdateStatus(int sid, int Status)
        {
            return WebChatBLL.UpdateStatus(sid, Status);
        }

        [WebMethod]
        public bool Support_Work_Seller_IsExists(int SupportID)
        {
            return WebChatBLL.Support_GetWork_Seller_IsExists(SupportID);
        }

        [WebMethod]
        public bool Support_Work_Support_IsExists(int SupportID)
        {
            return WebChatBLL.Support_GetWork_Support_IsExists(SupportID);
        }
    }
}
