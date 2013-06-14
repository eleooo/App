using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcFaceBookContent : UserControlBase
    {
        protected bool IsViewMyFaceBook { get; set; }
        private int UserId { get; set; }
        private bool isLogin { get; set; }
        private FaceBookType fbType;

        protected void Page_Load(object sender, EventArgs e)
        {
            fbType = Formatter.ToEnum<FaceBookType>(Request["fbType"], FaceBookType.OrderMeal);
            var bizID = Utilities.ToInt(Request["bizID"]);
            IsViewMyFaceBook = Request["showMe"] == "true";
            isLogin = AppContext.Context.CurrentSubSys != SubSystem.ALL;
            UserId = AppContext.CurrentUserID;
            if (fbType == FaceBookType.OrderMeal)
            {
                rpFbOrderMeal.Visible = true;
                rpFbOrderMeal.PageIndex = Utilities.ToInt(Request["pageIndex"]);
                int beginRate = Utilities.ToInt(Request["beginRate"]);
                int endRate = Utilities.ToInt(Request["endRate"]);
                var query = DB.Select(Utilities.GetTableColumns(SysCompanyFaceBook.Schema), SysMember.Columns.MemberPhoneNumber, SysMember.Columns.MemberFullname)
                              .From<SysCompanyFaceBook>( )
                              .InnerJoin(SysMember.IdColumn, SysCompanyFaceBook.FaceBookMemberIDColumn)
                              .Where(SysCompanyFaceBook.FaceBookBizIDColumn).IsEqualTo(bizID)
                              .And(SysCompanyFaceBook.FaceBookBizTypeColumn).IsEqualTo((int)fbType)
                              .And(SysCompanyFaceBook.FaceBookRateColumn).IsBetweenAnd(beginRate, endRate)
                              .OrderDesc(SysCompanyFaceBook.IdColumn.QualifiedName);
                rpFbOrderMeal.QuerySource = query;
                rpFbOrderMeal.DataBind( );
            }
            else if (fbType == FaceBookType.Eleooo)
            {
                rpFbEleooo.Visible = true;
                var pFaceBookID = Utilities.ToInt(Request["pBiz"]);
                if (pFaceBookID == 0)
                {
                    rpFbEleooo.PageIndex = Utilities.ToInt(Request["pageIndex"]);
                    var query = DB.Select(Utilities.GetTableColumns(SysCompanyFaceBook.Schema), SysMember.Columns.MemberPhoneNumber, SysMember.Columns.MemberFullname)
                                  .From<SysCompanyFaceBook>( )
                                  .InnerJoin(SysMember.IdColumn, SysCompanyFaceBook.FaceBookMemberIDColumn)
                                  .Where(SysCompanyFaceBook.FaceBookBizIDColumn).IsEqualTo(UserBLL.MainCompanyAccount.Id)
                                  .And(SysCompanyFaceBook.FaceBookBizTypeColumn).IsEqualTo((int)fbType)
                                  .And(SysCompanyFaceBook.PBizIDColumn).IsEqualTo(0)
                                  .OrderDesc(SysCompanyFaceBook.TopDateColumn.QualifiedName, SysCompanyFaceBook.IdColumn.QualifiedName);
                    if (IsViewMyFaceBook)
                        query.And(SysCompanyFaceBook.FaceBookMemberIDColumn).IsEqualTo(AppContext.CurrentUserID);
                    rpFbEleooo.QuerySource = query;
                }
                else
                {
                    rpFbEleooo.AllowPaging = false;
                    rpFbEleooo.HeaderTemplate = null;
                    rpFbEleooo.FooterTemplate = null;
                    var query = DB.Select(Utilities.GetTableColumns(SysCompanyFaceBook.Schema), SysMember.Columns.MemberPhoneNumber, SysMember.Columns.MemberFullname)
                                  .From<SysCompanyFaceBook>( )
                                  .InnerJoin(SysMember.IdColumn, SysCompanyFaceBook.FaceBookMemberIDColumn)
                                  .Where(SysCompanyFaceBook.IdColumn).IsEqualTo(pFaceBookID);
                    rpFbEleooo.QuerySource = query;
                }
                rpFbEleooo.DataBind( );
            }
        }
        private int GetCurrentItemPBizId( )
        {
            return (int)Eval(SysCompanyFaceBook.Columns.PBizID);
        }
        private int GetCurrentItemUserId( )
        {
            return (int)Eval(SysCompanyFaceBook.Columns.FaceBookMemberID);
        }
        private int GetCurrentItemPUserId( )
        {
            return (int)Eval(VEleooFaceBook.Columns.PFaceBookMemberID);
        }
        protected string GenFaceBookClientID( )
        {
            return (IsViewMyFaceBook ? "my" : string.Empty) + Eval("ID").ToString( );
        }
        protected string FormatUserPhone( )
        {
            if (fbType != FaceBookType.Eleooo)
                goto lbl_showPhone;
            if (IsMyItem)
                return FaceBookBLL.GetCurrentUserNick( );
            var pBizId = GetCurrentItemPBizId( );
            //if (!isLogin && (pBizId == 0 || (pBizId > 0 && GetCurrentItemUserId( ) == GetCurrentItemPUserId( ))))
            //    return "楼主本人";

        lbl_showPhone:
            var fullName = Utilities.ToString(Eval(SysMember.Columns.MemberFullname));
            if (!string.IsNullOrEmpty(fullName))
                return fullName;
            var phone = Utilities.ToString(Eval("MemberPhoneNumber"));
            var userID = Utilities.ToInt(Eval("FaceBookMemberID"));
            if (userID != AppContext.CurrentUserID)
                return CompanyBLL.EnUserPhoe(phone);
            else
                return phone;
        }
        protected SubSonic.SqlQuery GetSubFaceBookQuery(object pID)
        {
            var query = DB.Select(Utilities.GetTableColumns(VEleooFaceBook.Schema), SysMember.Columns.MemberPhoneNumber, SysMember.Columns.MemberFullname)
                              .From<VEleooFaceBook>( )
                              .InnerJoin(SysMember.IdColumn, VEleooFaceBook.FaceBookMemberIDColumn)
                              .Where(VEleooFaceBook.FaceBookBizIDColumn).IsEqualTo(UserBLL.MainCompanyAccount.Id)
                              .And(VEleooFaceBook.PBizIDColumn).IsEqualTo(pID)
                              .OrderDesc(VEleooFaceBook.TopDateColumn.QualifiedName)
                              .OrderAsc(VEleooFaceBook.IdColumn.QualifiedName);
            return query;
        }
        protected object GetSubFaceBookDataSource(object pID)
        {
            return GetSubFaceBookQuery(pID).ExecuteDataTable( );
        }
        protected string UpdateReadFlag( )
        {
            if (!Utilities.ToBool(Eval("IsRead")))
            {
                FaceBookBLL.UpdateReadFlag(Eval("ID"), UserId);
            }
            return string.Empty;
        }
        protected string GetCommentText( )
        {
            if (IsViewMyFaceBook || IsMyItem)
                return "继续吐槽";
            else
                return "我也吐一下";
        }

        private bool IsMyItem
        {
            get
            {
                return GetCurrentItemUserId( ) == UserId;
            }
        }
    }
}