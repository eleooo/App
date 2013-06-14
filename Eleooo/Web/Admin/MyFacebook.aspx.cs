using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
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
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            int id = Utilities.ToInt(EVENTARGUMENT);
            if (id <= 0)
            {
                txtMessage.InnerText = "参数错误.";
                goto lbl_return;
            }
            SysCompanyFaceBook.Delete(id);
            txtMessage.InnerText = "删除成功";
        lbl_return:
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            var arr = EVENTARGUMENT.Split(',');
            if (arr.Length == 0 || string.IsNullOrEmpty(arr[0]))
            {
                txtMessage.InnerText = "参数错误.";
                goto lbl_return;
            }
            bool isSetop = arr.Length > 1 && arr[1] == "1";
            FaceBookBLL.SetTopDateValue(arr[0], isSetop);
            txtMessage.InnerText = isSetop?"置顶成功":"取消置顶成功";
        lbl_return:
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyFaceBook.Schema), SysMember.Columns.MemberPhoneNumber, SysMember.Columns.MemberFullname, SysCompany.Columns.CompanyTel)
                          .From<SysCompanyFaceBook>( )
                          .InnerJoin(SysMember.IdColumn, SysCompanyFaceBook.FaceBookMemberIDColumn)
                          .InnerJoin(SysCompany.IdColumn, SysCompanyFaceBook.FaceBookBizIDColumn)
                          .Where(SysCompanyFaceBook.FaceBookBizTypeColumn).In((int)FaceBookType.Eleooo, (int)FaceBookType.OrderMeal)
                          .And(SysCompanyFaceBook.FaceBookDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .AndEx(SysMember.MemberPhoneNumberColumn).Like(filterMemberTel)
                          .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                          .CloseEx( )
                          .OrderDesc(Utilities.GetTableColumn(SysCompanyFaceBook.IdColumn));
            gridView.DataSource = query;
            gridView.AddShowColumn(SysCompanyFaceBook.FaceBookDateColumn)
                    .AddShowColumn(SysMember.MemberPhoneNumberColumn)
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(SysCompanyFaceBook.FaceBookMemoColumn)
                    .AddShowColumn(SysCompanyFaceBook.ReplyMemoColumn)
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }
        private string GetSetTopText(System.Data.DataRow row)
        {
            string text, val;
            if (Utilities.IsNull(row[SysCompanyFaceBook.Columns.TopDate]))
            {
                text = "置顶";
                val = row[SysCompanyFaceBook.IdColumn.ColumnName].ToString( ) + ",1";
            }
            else
            {
                text = "取消置顶";
                val = row[SysCompanyFaceBook.IdColumn.ColumnName].ToString( ) + ",0";
            }
            return string.Format(ACTION_EDIT_TEMPLATE, val, text);

        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            isRenderedCell = true;
            switch (column)
            {
                case "FaceBookDate":
                    result = string.Format(CELL_WIDTH_TEMPLATE, Utilities.ToDate(rowData[column]), "13%");
                    break;
                case "ReplyMemo":
                case "FaceBookMemo":
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, Formatter.SubStr(rowData[column], 10));
                    break;
                case "CompanyTel":
                case "MemberPhoneNumber":
                    result = string.Format(CELL_WIDTH_TEMPLATE, rowData[column], "13%");
                    break;
                case "Action":
                    isRenderedCell = false;
                    result = string.Concat("[", string.Format(ACTION_DEL_TEMPLATE, rowData[SysCompanyFaceBook.IdColumn.ColumnName], "删除"), "][", GetSetTopText(rowData), "][",
                                               string.Format(ACTION_DLG_TEMPLATE, rowData[SysCompanyFaceBook.IdColumn.ColumnName], "回复"), "]");
                    break;
            }
            return result;
        }
    }
}