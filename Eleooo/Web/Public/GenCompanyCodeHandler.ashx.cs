using System;
using System.Collections.Generic;
using System.Web;

namespace Eleooo.Web.Public
{
    /// <summary>
    /// GenCompanyCodeHandler1 的摘要说明
    /// </summary>
    public class GenCompanyCodeHandler : IHttpHandler
    {

        #region IHttpHandler Members
        const string RESULT_XML = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n"
                                 +"<string xmlns=\"http://tempuri.org/\">{0}</string>";
        public bool IsReusable
        {
            // 如果无法为其他请求重用托管处理程序，则返回 false。
            // 如果按请求保留某些状态信息，则通常这将为 false。
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //在此写入您的处理程序实现。
            int areaID;
            context.Response.ContentType = "text/xml";
            int.TryParse(context.Request.Params["areaID"], out areaID);
            context.Response.StatusCode = 200;
            context.Response.Output.Write(string.Format(RESULT_XML, AreaBLL.GenCompanyCodeByID(areaID)));
            context.Response.Output.Flush( );
            context.Response.End( );
        }
        #endregion
    }
}