using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.Common;

namespace Eleooo.Web.SiteAppPage
{
    public partial class UploadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string pFileType = Request.Params["fileType"];
                FileType fileType = FileType.Image;
                if (!string.IsNullOrEmpty(pFileType))
                    fileType = (FileType)Utilities.ToInt(pFileType);
                lblInfo.InnerHtml = string.Format("允许上传的文件类型为:{0},允许上传的文件大小于为:{1}", Eleooo.Common.FileUpload.GetAllowFileType(fileType), Eleooo.Common.FileUpload.GetMaxAllowSize( ));
            }
            lblMessage.InnerHtml = string.Empty;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string pSaveType = Request.Params["saveType"];
            SaveType saveType = SaveType.Support;
            if (!string.IsNullOrEmpty(pSaveType))
                saveType = (SaveType)Utilities.ToInt(pSaveType);
            string pFileType = Request.Params["fileType"];
            FileType fileType = FileType.Image;
            if (!string.IsNullOrEmpty(pFileType))
                fileType = (FileType)Utilities.ToInt(pFileType);
            string message, phyPath;
            string fileName = Eleooo.Common.FileUpload.SaveUploadFile(uploadify, fileType, saveType, out phyPath, out message, true);
            if (!string.IsNullOrEmpty(fileName))
                base.ClientScript.RegisterStartupScript(base.GetType( ), "upload", string.Format("closeForm('{0}','{1}','{2}','{3}')", this.Txt, fileName, this.Img, fileName), true);
            else
                lblMessage.InnerHtml = message;
        }
        protected string Txt
        {
            get
            {
                return Request.Params["txt"];
            }
        }
        protected string Img
        {
            get
            {
                return Request.Params["Img"];
            }
        }
    }
}