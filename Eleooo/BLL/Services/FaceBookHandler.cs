using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using Eleooo.Common;
using Eleooo.Web;

namespace Eleooo.BLL.Services
{
    class FaceBookHandler : IHandlerServices
    {
        #region IHandlerServices 成员

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
                FaceBookBLL.GetOrderMealRateCount(bizID, out good, out normal, out bad);
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
            return ServicesResult.GetInstance(c, message, null);
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
