using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class MyFacebook : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            int id = Utilities.ToInt(Request.Params["id"]);
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyFaceBook.Schema), SysMember.Columns.MemberPhoneNumber, SysMember.Columns.MemberFullname)
                          .From<SysCompanyFaceBook>( )
                          .InnerJoin(SysMember.IdColumn, SysCompanyFaceBook.FaceBookMemberIDColumn)
                          .Where(SysCompanyFaceBook.FaceBookBizIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .And(SysCompanyFaceBook.FaceBookBizTypeColumn).In((int)FaceBookType.Company,(int)FaceBookType.OrderMeal)
                          .AndEx(SysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                          .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                          .CloseEx( )
                          .OrderDesc(Utilities.GetTableColumn(SysCompanyFaceBook.IdColumn));
            if (id > 0)
                query.And(SysCompanyFaceBook.FaceBookMemberIDColumn).IsEqualTo(id);
            else
                query.And(SysCompanyFaceBook.FaceBookDateColumn).IsBetweenAnd(dtBegin, dtEnd);
            gridView.DataSource = query;
            gridView.AddShowColumn(SysCompanyFaceBook.FaceBookDateColumn)
                     .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(SysCompanyFaceBook.FaceBookMemoColumn)
                    .AddShowColumn(SysCompanyFaceBook.ReplyMemoColumn)
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            isRenderedCell = true;
            switch (column)
            {
                case "FaceBookDate":
                    result = string.Format(CELL_WIDTH_TEMPLATE, Utilities.ToDate(rowData[column]), "15%");
                    break;
                case "ReplyMemo":
                case "FaceBookMemo":
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, Formatter.SubStr(rowData[column], 20));
                    break;
                case "MemberPhoneNumber":
                    result = string.Format(CELL_WIDTH_TEMPLATE, rowData[column], "15%");
                    break;
                case "MemberFullname":
                    result = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, rowData[column], "15%");
                    break;
                case "Action":
                    isRenderedCell = false;
                    result = string.Concat("[", string.Format(ACTION_DEL_TEMPLATE, rowData[SysCompanyFaceBook.IdColumn.ColumnName], "删除"), "][",
                                               string.Format(ACTION_DLG_TEMPLATE, rowData[SysCompanyFaceBook.IdColumn.ColumnName], "回复"), "]");
                    break;
            }
            return result;
        }

        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            int id = Utilities.ToInt(EVENTARGUMENT);
            if (id <= 0)
            {
                txtMessage.InnerText = "参数错误.";
                goto lbl_return;
            }
            var faceBook = SysCompanyFaceBook.FetchByID(id);
            if (faceBook == null)
            {
                txtMessage.InnerText = "参数错误.";
                goto lbl_return;
            }
            else if (faceBook.FaceBookBizID != CurrentUser.CompanyId)
            {
                txtMessage.InnerText = "你不能删除不属于自己的评论.";
                goto lbl_return;
            }
            SysCompanyFaceBook.Delete(id);
            txtMessage.InnerText = "删除成功";
        lbl_return:
            On_ActionQuery(sender, e);
        }
    }
}