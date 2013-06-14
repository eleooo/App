using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.Common;
using Eleooo.DAL;

namespace Eleooo.Web.SiteAppPage
{
    public partial class ReplyFaceBook : System.Web.UI.Page
    {
        private int? _fbID;
        protected int FbID
        {
            get
            {
                if (!_fbID.HasValue)
                    _fbID = Utilities.ToInt(Request["ID"]);
                return _fbID.Value;
            }
        }
        private SysCompanyFaceBook faceBook;
        public SysCompanyFaceBook FaceBook
        {
            get
            {
                if (faceBook == null)
                    faceBook = SysCompanyFaceBook.FetchByID(FbID);
                return faceBook;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (FaceBook == null)
            {
                Response.Write("参数错误");
                this.Visible = false;
            }
            else
            {
                trFaceBookRate.Visible = (int)FaceBookType.OrderMeal == FaceBook.FaceBookBizType;
            }
        }

        protected string GetUserPhoneNumber( )
        {
            if (FaceBook != null)
                return UserBLL.GetUserPhoneById(FaceBook.FaceBookMemberID.Value);
            else
                return string.Empty;
        }
    }
}