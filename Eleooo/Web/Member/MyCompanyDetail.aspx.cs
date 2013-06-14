using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.IO;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class MyCompanyDetail : ActionPage
    {
        public int? LastItemID
        {
            get
            {
                return CompanyItemBLL.GetCompanyLastItemID(CurrentUser.Id, CompanyID);
            }
        }
        public int? LastAdsID
        {
            get
            {
                return CompanyAdsBLL.GetCompanyLastAdsID(CurrentUser.Id, CurrentUser.MemberSex, CompanyID);
            }
        }
        public string AreaType
        {
            get
            {
                string sArea = Request.UrlReferrer != null ? Request.UrlReferrer.Query : "";
                if (sArea.IndexOf("areaType=1", StringComparison.InvariantCultureIgnoreCase) >= 0)
                    return "生活圈";
                else if (sArea.IndexOf("areaType=2", StringComparison.InvariantCultureIgnoreCase) >= 0)
                    return "腐败圈";
                else
                    return "工作圈";
            }
        }
        public int CompanyID
        {
            get
            {
                return Utilities.ToInt(Request.Params["CompanyID"]);
            }
        }
        private SysCompany _company;
        public SysCompany Company
        {
            get
            {
                if (_company == null)
                    _company = SysCompany.FetchByID(CompanyID);
                return _company;
            }
        }
        public string CompanyContent
        {
            get
            {
                return HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(Company.CompanyContent));
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Company == null)
                Utilities.ShowMessageRedirect("商家不存在");
            sjImgContainer.SetRenderMethodDelegate(new RenderMethod(RenderSjImageControl));
        }
        private List<FileInfo> _fis;
        private List<FileInfo> Fis
        {
            get
            {
                if (_fis == null)
                {
                    _fis = Common.FileUpload.GetFileInfos(SaveType.Company, FileType.Image, Request.Params["CompanyID"]);
                }
                return _fis;
            }
        }
        public string GetDetailCompnayPic( )
        {
            if (Fis.Count == 0)
                return Eleooo.Common.FileUpload.GetFilePath(Company.CompanyPhoto, SaveType.Company);
            else
            {
                string relPath = Eleooo.Common.FileUpload.GetSaveRelDir(SaveType.Company);
                FileInfo fi = Fis[Fis.Count - 1];
                Fis.Remove(fi);
                return string.Format("{0}{1}/{2}", relPath, Company.Id, fi.Name);
            }
        }
        void RenderSjImageControl(HtmlTextWriter writer, Control control)
        {
            string relPath = Eleooo.Common.FileUpload.GetSaveRelDir(SaveType.Company);
            if (Fis.Count == 0)
                return;
            writer.Write("<ul class=\"rollList\">");
            foreach (FileInfo fi in Fis)
            {
                writer.Write("<li><a href=\"{0}{1}/{2}\" target=\"_blank\"><img alt=\"\" src=\"{0}{1}/{2}\"/></a></li>",
                             relPath, Company.Id, fi.Name);
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyFaceBook.Schema),
                                   SysMember.MemberPhoneNumberColumn.ColumnName
                                   ).From<SysCompanyFaceBook>( )
                          .InnerJoin(SysMember.IdColumn, SysCompanyFaceBook.FaceBookMemberIDColumn)
                          .Where(SysCompanyFaceBook.FaceBookBizIDColumn).IsEqualTo(CompanyID)
                          .And(SysCompanyFaceBook.FaceBookBizTypeColumn).IsEqualTo((int)FaceBookType.Company)
                          .OrderDesc(Utilities.GetTableColumn(SysCompanyFaceBook.IdColumn));
            this.BindEvalHandler((item, exp, val) =>
                    {
                        string v = Utilities.ToString(val);
                        return Utilities.Compare(v, CurrentUser.MemberPhoneNumber) ? v : CompanyBLL.EnUserPhoe(v);
                    })
                .BindEvalHandler((item, exp, val) =>HttpUtility.HtmlDecode(Utilities.ToString(val)));
            view.QuerySource = query;
            view.DataBind( );
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            string txtBook = HttpUtility.HtmlEncode(Request.Params["txtFaceBook"]);
            if (string.IsNullOrEmpty(txtBook))
            {
                Utilities.ShowMessageRedirect("请输入点评内容", this.Request.RawUrl);
            }
            else if (!UserBLL.CheckUserIsOrderInCompany(CurrentUser.Id, CompanyID))
                Utilities.ShowMessageRedirect("您尚未在该商家消费过，暂无权限点评^_^", this.Request.RawUrl);
            else
            {
                new SysCompanyFaceBook
                {
                    FaceBookBizID = CompanyID,
                    FaceBookDate = DateTime.Now,
                    FaceBookMemberID = CurrentUser.Id,
                    FaceBookMemo = txtBook,
                    FaceBookBizType = (int)FaceBookType.Company
                }.Save( );
                Utilities.ShowMessageRedirect("点评成功!", this.Request.RawUrl);
            }
        }
        public override void GetCurrentNavInfo(out string cNavUrl, out string cNavName)
        {
            cNavUrl = Request.RawUrl;
            SysArea area = AreaBLL.GetAreaByDepth(Company.AreaDepth);
            if (area != null)
                cNavName = area.AreaName;
            else
                cNavName = "我的商圈";
        }
        public override void GetParentNavInfo(out string pNavUrl, out string pNavName)
        {
            if (Request.UrlReferrer != null)
            {
                pNavUrl = Request.UrlReferrer.OriginalString;
                pNavName = AreaType;
            }
            else
            {
                pNavUrl = "/Member/MyCompany.aspx";
                pNavName = "我的商圈";
            }
        }
    }
}