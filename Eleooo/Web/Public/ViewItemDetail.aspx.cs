using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Text;
using System.IO;
using Eleooo.Common;

namespace Eleooo.Web.Public
{
    public partial class ViewItemDetail : ActionPage
    {
        string[] SPLITER = new string[]
        {
            "\r\n","\n\n","\n"
        };
        string[] SpaceSpliter = { " " };
        public string AreaTag
        {
            get
            {
                return AreaBLL.GetAreaTag(AreaDepthOrig);
            }
        }
        CompanyType? _companyType = null;
        public CompanyType CompanyType
        {
            get
            {
                if (!_companyType.HasValue)
                    _companyType = Formatter.ToEnum<CompanyType>(Company.CompanyType.Value);
                return _companyType.Value;
            }
        }
        public string ItemType
        {
            get
            {
                //if (CompanyType == CompanyType.UnionCompany)
                //    return "预订";
                //else
                return "抢购";
            }
        }
        private int _itemID = 0;
        public int ItemID
        {
            get
            {
                if (_itemID == 0)
                    _itemID = Utilities.ToInt(Request.Params["ItemID"]);
                return _itemID;
            }
        }
        private string AreaDepthOrig
        {
            get
            {
                var query = DB.Select(SysCompanyItem.Columns.AreaDepth)
                              .From<SysCompanyItem>( )
                              .Where(SysCompanyItem.ItemIDColumn).IsEqualTo(ItemID);
                return query.ExecuteScalar<string>( );
            }
        }
        private SysCompanyItem _item;
        public SysCompanyItem Item
        {
            get
            {
                if (_item == null)
                {
                    var query = DB.Select(Utilities.GetTableColumns(SysCompanyItem.Schema, SysCompanyItem.AreaDepthColumn),
                                          AreaBLL.GetFirstAreaName(SysCompanyItem.AreaDepthColumn, SysCompanyItem.Columns.AreaDepth))
                                  .From<SysCompanyItem>( )
                                  .Where(SysCompanyItem.ItemIDColumn).IsEqualTo(ItemID);
                    _item = query.ExecuteSingle<SysCompanyItem>( );
                }
                return _item;
            }
        }
        private SysCompany _company;
        public SysCompany Company
        {
            get
            {
                if (_company == null && Item != null)
                    _company = SysCompany.FetchByID(Item.CompanyID);
                return _company;
            }
        }
        public int RemindDay
        {
            get
            {
                if (Item == null)
                    return 0;
                TimeSpan ts = Item.ItemEndDate.Value.AddDays(1) - DateTime.Today;
                return ts.Days;
            }
        }
        public string ItemCanDelInfo
        {
            get
            {
                return Item != null && Item.IsCanDel == 1 ? "本单支持退订" : "本单不支持退订";
            }
        }
        public StringBuilder RenderContentToLi(string content)
        {
            if (string.IsNullOrEmpty(content))
                return null;
            string[] arr = HttpUtility.HtmlDecode(content).Split(SPLITER, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder( );
            foreach (string str in arr)
                sb.AppendFormat("<p>{0}</p>", str);
            return sb;
        }
        public string RenderItemImage(int flag)
        {
            if (Item == null)
                return string.Empty;
            string imagePath = Item.GetColumnValue<string>(flag == 0 ? "ItemPic" : "ItemPic" + flag.ToString( ));
            if (string.IsNullOrEmpty(imagePath))
                return null;
            else
                return string.Format("<img alt=\"\" src=\"{0}\" />", imagePath);
        }
        public string RenderItemImageTitle(int flag)
        {
            string img = Item.GetColumnValue<string>("ItemPic" + flag.ToString( ));
            string title = Eleooo.Common.FileUpload.GetImageTitle(img);
            if (string.IsNullOrEmpty(title))
                return title;
            var titleArray = title.Split(SpaceSpliter, StringSplitOptions.RemoveEmptyEntries);
            if (titleArray.Length <= 1)
                return "<h2>·【" + title + "】</h2>";
            else
            {
                return "<h2>·【" + titleArray[0] + "】</h2><p>" + string.Join("", titleArray, 1, titleArray.Length - 1) + "</p>";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Item == null)
            {
                Utilities.ShowMessageRedirect("优惠项目不存在");
                return;
            }
            if (this.CompanyType == Common.CompanyType.MealCompany)
            {
                Utilities.Redirect(string.Format("/Member/OrderCompanyMealItem.aspx?ItemId={0}", Item.ItemID), false);
                return;
            }
            //bool _isLogin = CurrentSubSys != SubSystem.ALL;
            //if (_isLogin && !UserBLL.UserHasArea(CurrentUser))
            //{
            //    Utilities.Redirect("/Member/MyArea.aspx", false);
            //    return;
            //}
        }
        private string GetItemTileAsCol( )
        {
            return string.Format("N'{0}' as ItemTitle", Item == null ? "" : Item.ItemTitle);
        }

        protected object FormatCompanyContent( )
        {
            string result = SubSonic.Sugar.Strings.StripHTML(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(Company.CompanyContent)));
            if (string.IsNullOrEmpty(result))
                return result;
            var arr = result.Split(SpaceSpliter, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder( );
            foreach (string str in arr)
                sb.AppendFormat("<p>　　{0}</p>", str.Trim( ));
            return sb;
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            //company pics
            //rpCompanyImg.DataSource = FileUpload.GetFileRelPaths(SaveType.Company, FileType.Image, Company.Id.ToString( ));
            //rpCompanyImg.DataBind( );

            var queryItemInfo = DB.Select(Utilities.GetTableColumns(SysMemberItem.Schema),
                                  GetItemTileAsCol( ),
                                 CompanyBLL.GetCompanyTypeAsCol(Company),
                                  SysMember.Columns.MemberPhoneNumber)
                          .From<SysMemberItem>( )
                          .InnerJoin(SysMember.IdColumn, SysMemberItem.MemberIDColumn)
                          .Where(SysMemberItem.CompanyItemIDColumn).IsEqualTo(ItemID)
                          .OrderDesc(SysMemberItem.Columns.ItemID);
            //0
            BindEvalHandler((item, exp, val) =>
                {
                    string phNum = Convert.ToString(val);
                    return CurrentSubSys != SubSystem.ALL && Utilities.Compare(phNum, CurrentUser.MemberPhoneNumber) ? phNum : CompanyBLL.EnUserPhoe(phNum);
                })
                //1
            .BindEvalHandler((item, exp, val) => Formatter.SubStr(val, 25))
                //2
            .BindEvalHandler((item, exp, val) =>
                {
                    var rowData = (item as System.Data.DataRowView).Row;
                    return CompanyItemBLL.GetItemStatusText(rowData);
                });
            view_ItemInfo.QuerySource = queryItemInfo;
            view_ItemInfo.DataBind( );

            //点评信息
            //3
            BindEvalHandler((item, exp, val) => HttpUtility.HtmlDecode(Utilities.ToString(val)));
            //if (CurrentSubSys != SubSystem.ALL)
            //{
                var queryFacebook = DB.Select(Utilities.GetTableColumns(SysCompanyFaceBook.Schema),
                                               SysMember.MemberPhoneNumberColumn.ColumnName
                                               ).From<SysCompanyFaceBook>( )
                                      .InnerJoin(SysMember.IdColumn, SysCompanyFaceBook.FaceBookMemberIDColumn)
                                      .Where(SysCompanyFaceBook.FaceBookBizIDColumn).IsEqualTo(ItemID)
                                      .And(SysCompanyFaceBook.FaceBookBizTypeColumn).IsEqualTo((int)FaceBookType.CompanyItem)
                                      .OrderDesc(Utilities.GetTableColumn(SysCompanyFaceBook.IdColumn));
                view_Facebook.QuerySource = queryFacebook;
                view_Facebook.DataBind( );
                sjplTitleContainer.Visible = true;
            //}
            //else
            //    sjdpContainer.Visible = false;
            //最近浏览信息
            //bind rec items
            //4
            BindEvalHandler((item, exp, val) => Formatter.SubStr(val, 15));
            var items = CompanyItemBLL.GetCookieRecViewItems( );
            if (items.Count > 0)
            {
                var queryRecItem = DB.Select(Utilities.GetTableColumns(SysCompanyItem.Schema),
                                              SysCompany.Columns.CompanyName,
                                              SysCompany.Columns.CompanyType)
                                      .From<SysCompanyItem>( )
                                      .InnerJoin(SysCompany.IdColumn, SysCompanyItem.CompanyIDColumn)
                                      .Where(SysCompanyItem.ItemEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                                      .And(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                                      .And(SysCompanyItem.IsPassColumn).IsEqualTo(true)
                                      .And(SysCompanyItem.ItemIDColumn).In(items.ToArray( ));
                view_RecItem.QuerySource = queryRecItem;
                view_RecItem.DataBind( );
            }

            CompanyItemBLL.SetCookieRecViewItem(ItemID);
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            string txtBook = HttpUtility.HtmlEncode(Request.Params["txtFaceBook"]);
            if (string.IsNullOrEmpty(txtBook))
            {
                Utilities.ShowMessageRedirect("请输入点评内容", this.Request.RawUrl);
                return;
            }
            else if (AppContext.Context.CurrentSubSys == SubSystem.ALL)
            {
                Utilities.ShowMessageRedirect("请先登录再发表你的评论.", this.Request.RawUrl);
                return;
            }
            else if (!UserBLL.CheckUserIsOrderInCompany(CurrentUser.Id, Item.CompanyID))
            {
                Utilities.ShowMessageRedirect("您尚未在该商家消费过，暂无权限点评^_^", this.Request.RawUrl);
                return;
            }
            else
            {
                new SysCompanyFaceBook
                {
                    FaceBookBizID = ItemID,
                    FaceBookDate = DateTime.Now,
                    FaceBookMemberID = CurrentUser.Id,
                    FaceBookMemo = txtBook,
                    FaceBookBizType = (int)FaceBookType.CompanyItem
                }.Save( );
                Utilities.ShowMessageRedirect("点评成功!", this.Request.RawUrl);
            }
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            CompanyItemBLL.RemoveRecViewItems( );
            On_ActionQuery(sender, e);
        }
    }
}