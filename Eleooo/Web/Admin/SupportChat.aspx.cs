using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;

namespace Eleooo.Web.Admin
{
    public partial class SupportChat : ActionPage
    {
        private int _supportID = 0;
        protected int SupportID
        {
            get
            {
                if (_supportID == 0)
                {
                    int.TryParse(Params["ID"], out _supportID);
                }
                return _supportID;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            SysSupport support = SysSupport.FetchByID(SupportID);
            if (support == null)
                return;
            lblSupportID.InnerHtml = support.SupportId.ToString( );
            lblSupportSubject.InnerHtml = support.SupportSubject;
            this.iframeChatList.Attributes.Add("src", string.Format("/SiteAppPage/SupportChat.aspx?sid={0}", SupportID));
            this.lblSupportMan.InnerHtml = UserBLL.CheckUserIsOnline(support.SupportFid) ? "当前会员在线" : "当前会员离线.";
        }
    }
}