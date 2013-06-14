using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using SubSonic;
using System.Transactions;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class MyBalanceMove : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
                lblPhoneInfo.InnerHtml = "请输入会员账号";
                lblPointInfo.InnerHtml = "请输入整数";
                lblMessage.InnerHtml = "";
            }
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMoveMember.Value))
            {
                lblPhoneInfo.InnerHtml = "请输入会员账号";
                goto lbl_return;
            }
            if (string.IsNullOrEmpty(txtMoveSum.Value))
            {
                lblPointInfo.InnerHtml = "请输入整数";
                goto lbl_return;
            }
            int dMoveSum;
            if (!int.TryParse(txtMoveSum.Value, out dMoveSum) || dMoveSum <= 0)
            {
                lblPointInfo.InnerHtml = "请输入大于零整数";
                goto lbl_return;
            }
            SysMember toUser = UserBLL.GetUserByPhoneNum(txtMoveMember.Value);
            if (toUser == null)
            {
                lblPhoneInfo.InnerHtml = "输入的会员账号不存在.";
                goto lbl_return;
            }
            SysMember user = SysMember.FetchByID(CurrentUser.Id);
            if (dMoveSum > user.MemberBalance)
            {
                lblMessage.InnerHtml = string.Format("你的余额为:{0},本次需要转移:{1},余额不足以支付本次转移.", user.MemberBalance, dMoveSum);
                goto lbl_return;
            }
            if (toUser.Id == user.Id)
            {
                lblMessage.InnerHtml = "你不能将积分转移给自己,请输入其他会员的账号!";
                goto lbl_return;
            }
            TransactionScope ts = new TransactionScope( );
            SharedDbConnectionScope ss = new SharedDbConnectionScope( );
            try
            {
                SysMemberMove m = new SysMemberMove
                {
                    MoverMemberIDOld = user.Id,
                    MoverMemberIDNew = toUser.Id,
                    MoverDate = DateTime.Now,
                    MoverMemberIDPoint = dMoveSum
                };
                m.Save( );
                string memo = string.Format("已转出{1}个积分给{0}", toUser.MemberPhoneNumber, dMoveSum);
                new Payment
                {
                    PaymentDate = DateTime.Now,
                    PaymentCode = string.Empty,
                    PaymentEmail = string.Empty,
                    PaymentCompanyID = 0,
                    PaymentMemberID = user.Id,
                    PaymentMemo = memo,
                    PaymentStatus = 1,
                    PaymentSum = -dMoveSum,
                    PaymentType = (int)PaymentType.Move,
                    PaymentOrderID = m.MoveID
                }.Save( );
                new Payment
                {
                    PaymentDate = DateTime.Now,
                    PaymentCompanyID = 0,
                    PaymentCode = string.Empty,
                    PaymentEmail = string.Empty,
                    PaymentMemberID = toUser.Id,
                    PaymentMemo = string.Format("已收到{0}转入的{1:0.00}个积分", user.MemberPhoneNumber, dMoveSum),
                    PaymentStatus = 1,
                    PaymentSum = dMoveSum,
                    PaymentType = (int)PaymentType.Move,
                    PaymentOrderID = m.MoveID
                }.Save( );
                OrderBLL.UpdateBalance( );
                ts.Complete( );
                txtMoveMember.Value = "";
                txtMoveSum.Value = "";
                lblMessage.InnerHtml = "积分转账成功";
            }
            catch (Exception ex)
            {
                Logging.Log("MyBalanceMove->On_ActionAdd", ex, true);
                lblMessage.InnerHtml = ex.Message;
            }
            finally
            {
                ss.Dispose( );
                ts.Dispose( );
            }
        lbl_return:
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            var query = DB.Select( ).From<VSysMemberCashMove>( )
                             .Where(VSysMemberCashMove.Columns.MoverDate).IsBetweenAnd(dtBegin, dtEnd)
                             .OrderDesc(VSysMemberCashMove.Columns.MoveID);
            switch (rblFlag.Text)
            {
                case "1":
                    query = query.And(VSysMemberCashMove.Columns.MoverMemberIDOld).IsEqualTo(AppContext.Context.User.Id);
                    break;
                case "2":
                    query = query.And(VSysMemberCashMove.Columns.MoverMemberIDNew).IsEqualTo(AppContext.Context.User.Id);
                    break;
                default:
                    query = query.AndEx(VSysMemberCashMove.Columns.MoverMemberIDOld).IsEqualTo(AppContext.Context.User.Id)
                                 .Or(VSysMemberCashMove.Columns.MoverMemberIDNew).IsEqualTo(AppContext.Context.User.Id)
                                 .CloseEx( );
                    break;
            }
            this.BindEvalHandler((item, exp, val) =>
                    {
                        System.Data.DataRow rowData = (item as System.Data.DataRowView).Row;
                        return GetCashMoveMember(rowData);
                    })
                .BindEvalHandler((item, exp, val) =>
                    {
                        System.Data.DataRow rowData = (item as System.Data.DataRowView).Row;
                        return GetCashMoveMemberPhone(rowData);
                    })
                .BindEvalHandler((item, exp, val) =>
                {
                    System.Data.DataRow rowData = (item as System.Data.DataRowView).Row;
                    return GetCashMoveType(rowData);
                });
            view.QuerySource = query;
            view.DataBind( );
            if (!Utilities.Compare(lblPhoneInfo.InnerHtml, "请输入会员账号"))
                lblPhoneInfo.Style.Add(HtmlTextWriterStyle.Color, "red");
            if (!Utilities.Compare(lblPointInfo.InnerHtml, "请输入整数"))
                lblPointInfo.Style.Add(HtmlTextWriterStyle.Color, "red");
            //gridView.DataSource = query;
            //gridView.AddShowColumn(VSysMemberCashMove.Columns.MoverDate)
            //        .AddCustomColumn("CashMoveMember", "会员名称")
            //        .AddCustomColumn("CashMoveMemberPhone", "会员账号")
            //        .AddCustomColumn(VSysMemberCashMove.Columns.MoverMemberIDPoint, "积分额度")
            //        .AddCustomColumn("CashMoveType", "备注");
            //gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            //gridView.DataBind( );
        }
        //string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        //{
        //    string result = string.Empty;

        //    switch (column)
        //    {
        //        case "MoverDate":
        //            result = Utilities.ToDate(rowData[column]);
        //            break;
        //        case "CashMoveType":
        //            result = GetCashMoveType(rowData);
        //            break;
        //        case "CashMoveMember":
        //            result = GetCashMoveMember(rowData);
        //            break;
        //        case "CashMoveMemberPhone":
        //            result = GetCashMoveMemberPhone(rowData);
        //            break;
        //        default:
        //            result = Utilities.ToHTML(rowData[column]);
        //            break;
        //    }
        //    return result;
        //}
        private string GetCashMoveMemberPhone(System.Data.DataRow rowData)
        {
            int id = Convert.ToInt32(rowData[VSysMemberCashMove.Columns.MoverMemberIDOld]);
            if (id == AppContext.Context.User.Id)
                return Utilities.ToHTML(rowData[VSysMemberCashMove.Columns.NewPhoneNumber]);
            else
                return Utilities.ToHTML(rowData[VSysMemberCashMove.Columns.OldPhoneNumber]);
        }

        private string GetCashMoveMember(System.Data.DataRow rowData)
        {
            int id = Convert.ToInt32(rowData[VSysMemberCashMove.Columns.MoverMemberIDOld]);
            if (id == AppContext.Context.User.Id)
                return Utilities.ToHTML(rowData[VSysMemberCashMove.Columns.NewName]);
            else
                return Utilities.ToHTML(rowData[VSysMemberCashMove.Columns.OldName]);
        }
        private string GetCashMoveType(System.Data.DataRow rowData)
        {
            int id = Convert.ToInt32(rowData[VSysMemberCashMove.Columns.MoverMemberIDOld]);
            if (id == AppContext.Context.User.Id)
                return "转出";
            else
                return "收到";
        }
    }
}