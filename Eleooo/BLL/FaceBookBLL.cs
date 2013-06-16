using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eleooo.Common;
using Eleooo.DAL;
using SubSonic;

namespace Eleooo.Web
{
    public class FaceBookBLL
    {
        public static string GetFilterResource( )
        {
            return ResBLL.GetRes("FaceBookFilterResource", "我操|屌|我叼|你老母", "评价过滤关键字");
        }
        public static void GetOrderMealRateCount(int bizID, DateTime? d1, DateTime? d2, out int good, out int normal, out int bad)
        {
            good = 0; normal = 0; bad = 0;
            var cmd = new QueryCommand(@"
select SUM(case when FaceBookRate <= 1 then 1 else 0 end) as bad,
       SUM(case when FaceBookRate > 1 and FaceBookRate < 5 then 1 else 0 end) as normal,
       SUM(case when FaceBookRate >= 5 then 1 else 0 end) as good
from dbo.Sys_Company_FaceBook WHERE FaceBookDate BETWEEN @d1 AND @d2 AND FaceBookBizID = @FaceBookBizID and FaceBookBizType = @FaceBookBizType;");
            cmd.AddParameter("@FaceBookBizID", bizID, System.Data.DbType.Int32);
            cmd.AddParameter("@FaceBookBizType", (int)FaceBookType.OrderMeal, System.Data.DbType.Int32);
            cmd.AddParameter("@d1", d1 ?? System.Data.SqlTypes.SqlDateTime.MinValue, System.Data.DbType.DateTime);
            cmd.AddParameter("@d2", d2 ?? DateTime.Now, System.Data.DbType.DateTime);

            using (var dr = DataService.GetReader(cmd))
            {
                if (dr.Read( ))
                {
                    bad = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                    normal = dr.IsDBNull(1) ? 0 : dr.GetInt32(1);
                    good = dr.IsDBNull(2) ? 0 : dr.GetInt32(2);
                }
            }
        }
        public static int AddFaceBook(int bizID, int pFaceBookID, string content, FaceBookType? fbType, int? rate, out string message)
        {
            int code = -1;
            if (fbType.HasValue && fbType.Value == FaceBookType.Eleooo)
                bizID = UserBLL.MainCompanyAccount.Id;
            if (!fbType.HasValue || bizID <= 0)
            {
                message = "业务参数错误!";
                goto lbl_return;
            }
            var userID = AppContextBase.CurrentUserID;
            if (userID <= 0)
            {
                message = "你还没有登录,不能发表评论.";
                goto lbl_return;
            }
            DateTime? orderDate = null;
            if (fbType.Value == FaceBookType.OrderMeal)
            {
                orderDate = OrderMealBLL.GetUserLatestOrderDate(userID, bizID);
                if (!orderDate.HasValue)
                {
                    message = "你还没在此商家订过餐,不能发表评论.";
                    goto lbl_return;
                }
            }
            if (string.IsNullOrEmpty(content))
            {
                message = "点评内容不能为空.";
                goto lbl_return;
            }
            new SysCompanyFaceBook
            {
                FaceBookBizID = bizID,
                FaceBookBizType = (int)fbType.Value,
                FaceBookMemberID = userID,
                FaceBookDate = DateTime.Now,
                FaceBookMemo = content,
                FaceBookRate = rate,
                LatestOrderDate = orderDate,
                PBizID = pFaceBookID,
                IsRead = true
            }.Save( );
            message = "点评成功";
            code = 0;
        lbl_return:
            return code;
        }
        public static int ReplyFaceBook(int faceBookID, SysMember replyUser, string replyContent, out string message)
        {
            int code = -1;
            SysCompanyFaceBook faceBook = SysCompanyFaceBook.FetchByID(faceBookID);
            if (faceBook == null)
            {
                message = "评论内容不存在.";
                goto lbl_return;
            }
            var fbType = Formatter.ToEnum<FaceBookType>(faceBook.FaceBookBizType);
            if (fbType == FaceBookType.Eleooo)
            {
                if (!replyUser.AdminRoleId.HasValue || replyUser.AdminRoleId.Value <= 0)
                {
                    message = "吐槽内容,只有管理员才能回复.";
                    goto lbl_return;
                }
            }
            else if (fbType == FaceBookType.OrderMeal)
            {
                if (replyUser.CompanyId != faceBook.FaceBookBizID || replyUser.AdminRoleId.Value <= 0)
                {
                    message = "你不能回复不属于你的评论.";
                    goto lbl_return;
                }
            }
            if (string.IsNullOrEmpty(replyContent))
            {
                message = "请输入回复内容";
                goto lbl_return;
            }
            faceBook.ReplyDate = DateTime.Now;
            faceBook.ReplyMemberID = replyUser.Id;
            faceBook.ReplyMemo = replyContent;
            faceBook.IsRead = false;
            faceBook.Save( );
            message = "回复成功";
        lbl_return:
            return code;
        }
        public static void UpdateReadFlag(object faceBookID, object userId)
        {
            QueryCommand cmd = new QueryCommand("update Sys_Company_FaceBook set IsRead = 1 where id=@id and FaceBookMemberID=@userID;");
            cmd.AddParameter("@id", faceBookID);
            cmd.AddParameter("@userID", userId);
            DataService.ExecuteQuery(cmd);
        }
        public static object GetUnReadCount( )
        {
            QueryCommand cmd = new QueryCommand("select count(*) from  Sys_Company_FaceBook where IsRead=0 and FaceBookMemberID=@userID and FaceBookBizType=@FaceBookBizType;");
            cmd.AddParameter("@userID", AppContextBase.CurrentUserID);
            cmd.AddParameter("@FaceBookBizType", (int)FaceBookType.Eleooo);
            return DataService.ExecuteScalar(cmd);
        }
        public static void SetTopDateValue(object faceBookID, bool isTop)
        {
            QueryCommand cmd;
            if (isTop)
                cmd = new QueryCommand("update Sys_Company_FaceBook set TopDate = GetDate() where id=@id");
            else
                cmd = new QueryCommand("update Sys_Company_FaceBook set TopDate = null where id=@id");
            cmd.AddParameter("@id", faceBookID);
            DataService.ExecuteQuery(cmd);
        }

        public static string GetCurrentUserNick( )
        {
            if (!AppContextBase.Context.User.MemberSex.HasValue || AppContextBase.Context.User.MemberSex.Value)
                return "寡人";
            else
                return "本宫";
        }
    }
}
