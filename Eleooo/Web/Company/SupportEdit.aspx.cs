using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Transactions;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class SupportEdit : ActionPage
    {
        int _id;
        protected int Id
        {
            get
            {
                if (_id <= 0)
                {
                    _id = Utilities.ToInt(Params["ID"]);
                }
                return _id;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSupportMan.InnerText = WebChatBLL.Support_GetWork_Support_IsExists(0) ? "服务人员在线" : "服务人员离线";
            txtMessage.InnerHtml = string.Empty;
        }

        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (Utilities.IsNull(txtSupportSubject.Text))
            {
                txtMessage.InnerHtml = "请输入咨询的标题!";
                return;
            }

            SysSupport support = SysSupport.FetchByID(Id);
            if (support == null)
            {
                support = new SysSupport( );
                support.SupportIsRead = false;
                support.SupportItem = 1;
                support.SupportProductID = 0;
                support.SupportFid = CurrentUser.Id;
                support.SupportPhoto = string.Empty;
                support.SupportRating = 1;
                support.SupportRatingReason = string.Empty;
                support.SupportStatus = 1;
                support.SupportTid = 0;
                support.SupportType = 1;
                support.SupportDate = DateTime.Now;
                support.SupportDateFinish = DateTime.MinValue.AddYears(0x76c);
                support.SupportDateReply = DateTime.MinValue.AddYears(0x76c);
                support.SupportAttach = string.Empty;
            }
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                if (!Utilities.IsNull(txtSupportPhoto.Value) && !string.IsNullOrEmpty(txtSupportPhoto.PostedFile.FileName))
                {
                    string message, phyPath;
                    string filePath = Eleooo.Common.FileUpload.SaveUploadFile(txtSupportPhoto.PostedFile, FileType.Image | FileType.Zip | FileType.Zip, SaveType.Support, out phyPath, out message, true);
                    if (!string.IsNullOrEmpty(message))
                    {
                        txtMessage.InnerHtml = message;
                        return;
                    }
                    support.SupportPhoto = filePath;
                }
                support.SupportSubject = txtSupportSubject.Text;
                support.SupportContent = HttpUtility.HtmlEncode(txtSupportContent.Text);
                support.Save( );
                _id = support.SupportId;
                new SysSupportMessage
                {
                    SupportMsgSid = _id,
                    SupportMsgDate = DateTime.Now,
                    SupportMsgFid = support.SupportFid,
                    SupportMsgTid = 0,
                    SupportMsgIsAsk = false,
                    SupportMsgIsRead = false,
                    SupportMsgPhoto = support.SupportPhoto,
                    SupportMsgMemo = string.IsNullOrEmpty(support.SupportContent) ? support.SupportSubject : support.SupportContent
                }.Save( );
                ts.Complete( );
                txtMessage.InnerHtml = "保存成功!";
            }
            catch (Exception ex)
            {
                Logging.Log("Company.SupportEdit->On_ActionAdd", ex, true);
                txtMessage.InnerHtml = ex.Message;
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
            On_ActionQuery(sender, e);

        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            SysSupport support = SysSupport.FetchByID(Id);
            if (support == null)
                return;
            else if (CurrentUser.Id != support.SupportFid)
            {
                PrintErrorMessage("阁下无权查看其他会员的信息!");
                return;
            }
            txtSupportSubject.Text = support.SupportSubject;
            txtSupportContent.Text = Utilities.ToHTML(support.SupportContent);
            //txtSupportPhoto.Value = support.SupportPhoto;
        }
    }
}