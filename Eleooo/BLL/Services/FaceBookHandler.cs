using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using Eleooo.Common;
using Eleooo.Web;
using Eleooo.DAL;

namespace Eleooo.BLL.Services
{
    class FaceBookHandler : IHandlerServices
    {
        #region IHandlerServices 成员

        public Common.ServicesResult Get(System.Web.HttpContext context)
        {
            var Request = context.Request;
            var fbType = Formatter.ToEnum<FaceBookType>(Request["t"], FaceBookType.OrderMeal);
            var bizID = Utilities.ToInt(Request["b"]);
            var pageIndex = Utilities.ToInt(Request["i"]);
            var pageSize = Utilities.ToInt(Request["s"]);
            DateTime? d1 = null, d2 = null;
            var s1 = Request["d1"];
            if (!string.IsNullOrEmpty(s1))
                d1 = Utilities.ToDateTime(s1);
            else
                d1 = DateTime.MinValue;
            s1 = Request["d2"];
            if (!string.IsNullOrEmpty(s1))
                d2 = Utilities.ToDateTime(s1);
            else
                d2 = DateTime.Today.AddDays(1);
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 1;
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyFaceBook.Schema),
                                  SysMember.Columns.MemberPhoneNumber,
                                  SysMember.Columns.MemberFullname,
                                  DbCommonFn.RenderFnGetUserOrderInfoAsColumn(
                                        SysCompanyFaceBook.FaceBookMemberIDColumn,
                                        SysCompanyFaceBook.FaceBookBizIDColumn,
                                        SysCompanyFaceBook.FaceBookDateColumn,
                                        "<p><span>下单次数：<b>[count]</b></span><span>总金额：<b>[sum]</b></span></p>",
                                        "OrderInfo"
                                    ))
                          .From<SysCompanyFaceBook>( )
                          .InnerJoin(SysMember.IdColumn, SysCompanyFaceBook.FaceBookMemberIDColumn)
                          .Where(SysCompanyFaceBook.FaceBookBizIDColumn).IsEqualTo(bizID)
                          .And(SysCompanyFaceBook.FaceBookBizTypeColumn).IsEqualTo((int)fbType)
                          .And(SysCompanyFaceBook.FaceBookDateColumn).IsBetweenAnd(d1, d2)
                          .And(SysCompanyFaceBook.PBizIDColumn).IsEqualTo(0)
                          .OrderDesc(SysCompanyFaceBook.IdColumn.QualifiedName)
                          .Paged(pageIndex, pageSize);
            bool isReply;
            List<object> resultList = new List<object>( );
            foreach (var dr in query.GetDataReaderEnumerator( ))
            {
                StringBuilder sb = new StringBuilder( );
                isReply = !Utilities.IsNull(dr[SysCompanyFaceBook.Columns.ReplyDate]);
                sb.AppendFormat("<li id=\"item{0}\" data-reply=\"{1}\">", dr[SysCompanyFaceBook.Columns.Id], isReply);
                sb.Append("<div class=\"rw1\">");
                sb.Append("<img src=\"images/user.png\" alt=\"\" align=\"absmiddle\" />");
                sb.AppendFormat("<p><span class=\"user\">{0}</span></p>", dr[SysMember.Columns.MemberPhoneNumber]);
                sb.Append(dr["OrderInfo"]);
                sb.Append("</div>");
                sb.AppendFormat("<div class=\"rw2\">点评：{0}<span class=\"time\">{1:MM-dd HH:mm:ss}</span></div>", dr[SysCompanyFaceBook.Columns.FaceBookMemo], dr[SysCompanyFaceBook.Columns.FaceBookDate]);
                if (!isReply)
                {
                    sb.AppendFormat("<div class=\"rw4\"><input type=\"button\" class=\"thoughtbot\" value=\"回复\" tap=\"showReplyBox\" data-id=\"{0}\" /></div>", dr[SysCompanyFaceBook.Columns.Id]);
                    sb.AppendFormat("<div class=\"rw5\" style=\"display:none\" id=\"box{0}\">", dr[SysCompanyFaceBook.Columns.Id]);
                    sb.AppendFormat("<textarea name=\"\" id=\"txtbox{0}\" class=\"r_input\"></textarea>", dr[SysCompanyFaceBook.Columns.Id]);
                    sb.AppendFormat("<input type=\"button\" class=\"thoughtbot\" value=\"提交\" tap=\"replyFaceBook\" data-id=\"{0}\" />", dr[SysCompanyFaceBook.Columns.Id]);
                    sb.Append("</div>");
                }
                else
                    sb.AppendFormat("<div class=\"rw3\">回复：{0}<span class=\"time\">{1:MM-dd HH:mm:ss}</span></div>", dr[SysCompanyFaceBook.Columns.ReplyMemo], dr[SysCompanyFaceBook.Columns.ReplyDate]);
                sb.Append("</li>");
                resultList.Add(dr[SysCompanyFaceBook.Columns.Id]);
                resultList.Add(sb.ToString( ));
            }
            int good, normal, bad;
            FaceBookBLL.GetOrderMealRateCount(bizID, d1, d2, out good, out normal, out bad);
            return ServicesResult.GetInstance(new
            {
                good = good,
                normal = normal,
                bad = bad,
                html = resultList
            });
        }

        public Common.ServicesResult Query(System.Web.HttpContext context)
        {
            StringBuilder sb = new StringBuilder( );
            using (Page page = new Page( ))
            {
                var control = page.LoadControl("~/Controls/UcFaceBookContent.ascx");
                page.Controls.Add(control);
                page.EnableViewState = false;
                using (TextWriter writer = new StringWriter(sb))
                {
                    context.Server.Execute(page, writer, true);
                }
            }
            var fbType = Formatter.ToEnum<FaceBookType>(context.Request["fbType"], FaceBookType.OrderMeal);
            if (fbType == FaceBookType.OrderMeal)
            {
                int good, normal, bad;
                int bizID = Utilities.ToInt(context.Request["bizID"]);
                FaceBookBLL.GetOrderMealRateCount(bizID, null, null, out good, out normal, out bad);
                return ServicesResult.GetInstance(new
                        {
                            good = good,
                            normal = normal,
                            bad = bad,
                            html = sb.ToString( )
                        });
            }
            else
            {
                return ServicesResult.GetInstance(new
                        {
                            html = sb.ToString( )
                        });
            }
        }

        public Common.ServicesResult Add(System.Web.HttpContext context)
        {
            string message;
            var bizID = Utilities.ToInt(context.Request["bizID"]);
            int? rate = null;
            if (!string.IsNullOrEmpty(context.Request["rate"]))
                rate = Utilities.ToInt(context.Request["rate"]);
            FaceBookType? fbType = null;
            if (!string.IsNullOrEmpty(context.Request["fbType"]))
                fbType = Formatter.ToEnum<FaceBookType>(context.Request["fbType"]);
            var code = FaceBookBLL.AddFaceBook(bizID, Utilities.ToInt(context.Request["pBiz"]), context.Server.UrlDecode(context.Request["content"]), fbType, rate, out message);
            return code < 0 || fbType.HasValue && fbType.Value != FaceBookType.Eleooo ? ServicesResult.GetInstance(code, message, null) : Query(context);
        }

        public Common.ServicesResult Edit(System.Web.HttpContext context)
        {
            var fbID = Utilities.ToInt(context.Request["fbID"]);
            string message;
            var c = FaceBookBLL.ReplyFaceBook(fbID, AppContextBase.Context.User, context.Server.UrlDecode(context.Request["content"]), out message);
            return ServicesResult.GetInstance(c, message, string.Format("<div class=\"rw3\">回复：[0]<span class=\"time\">{0:MM-dd HH:mm:ss}</span></div>", DateTime.Now));
        }

        public Common.ServicesResult GetUnReadCount(System.Web.HttpContext context)
        {
            return Common.ServicesResult.GetInstance(FaceBookBLL.GetUnReadCount( ));
        }

        public Common.ServicesResult Delete(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public Common.ServicesResult Login(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public Common.ServicesResult Logout(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
