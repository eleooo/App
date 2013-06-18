using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web.SiteAppPage
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        //const string RESULT_XML = "<?xml version=\"1.0\" encoding=\"utf-8\"?><div xmlns=\"http://tempuri.org/\"><span id='fileName'>{0}</span><span id='msg'>{1}</span></div>";
        const string RESULT_JSON = "{{\"filename\":\"{0}\",\"message\":\"{1}\"}}";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            try
            {
                string pSaveType = context.Request.Params["saveType"];
                SaveType saveType = SaveType.Support;
                if (!string.IsNullOrEmpty(pSaveType))
                    saveType = (SaveType)Utilities.ToInt(pSaveType);
                HttpPostedFile file = context.Request.Files[0];
                string message;
                var result = FileUpload.SaveUploadFile(file, FileType.All, saveType, out message, true);
                context.Response.Write(GetResult(result != null ? result.RelPath : string.Empty, message));
            }
            catch (Exception ex)
            {
                Logging.Log("UploadHandler->ProcessRequest", ex);
                context.Response.Write(GetResult(string.Empty, ex.Message));
            }
        }
        string GetResult(string fileName, string message)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = string.Empty;
            if (string.IsNullOrEmpty(message))
                message = string.Empty;
            return string.Format(RESULT_JSON, fileName, message);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}