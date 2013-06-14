using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
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
            txtMessage.InnerHtml = string.Empty;
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyFaceBook.Schema), SysCompany.Columns.CompanyName)
                          .From<SysCompanyFaceBook>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyFaceBook.FaceBookBizIDColumn)
                          .Where(SysCompanyFaceBook.FaceBookDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(SysCompanyFaceBook.FaceBookBizTypeColumn).IsEqualTo((int)FaceBookType.Company)
                          .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                          .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                          .CloseEx( )
                          .And(SysCompanyFaceBook.FaceBookMemberIDColumn).IsEqualTo(CurrentUser.Id);
            gridView.DataSource = query;
            gridView.AddShowColumn(SysCompanyFaceBook.FaceBookDateColumn)
                    .AddShowColumn(SysCompany.CompanyNameColumn)
                    .AddShowColumn(SysCompanyFaceBook.FaceBookMemoColumn)
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            //gridView.OnDataBindHeader += new Web.Controls.DataBindHeaderHandle(gridView_OnDataBindHeader);
            gridView.DataBind( );
        }

        void gridView_OnDataBindHeader(string column, ref string caption, ref bool isRenderedCell)
        {
            isRenderedCell = true;
            switch (column)
            {
                case "FaceBookDate":
                    caption = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, caption, "10%");
                    break;
                case "CompanyName":
                    caption = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, caption, "30%");
                    break;
                case "FaceBookMemo":
                    caption = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, caption, "45%");
                    break;
                case "Action":
                    caption = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, caption, "15%");
                    break;
            }
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            int id = Utilities.ToInt(EVENTARGUMENT);
            if (id <= 0)
            {
                txtMessage.InnerHtml = "参数错误!";
                return;
            }
            SysCompanyFaceBook.Delete(id);
            txtMessage.InnerHtml = "删除成功!";
            On_ActionQuery(sender, e);
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            isRenderedCell = true;
            switch (column)
            {
                case "FaceBookDate":
                    result = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, Utilities.ToDate(rowData[column]), "8%");
                    break;
                case "FaceBookMemo":
                    isRenderedCell = true;
                    result = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, Utilities.ToHTML(rowData[column]), "50%");
                    break;
                case "Action":
                    isRenderedCell = false;
                    result = string.Concat(
                                    string.Format(ACTION_DLG_TEMPLATE, rowData[SysCompanyFaceBook.Columns.Id].ToString( ), "[编辑]"),
                                    "&nbsp;",
                                    string.Format(ACTION_DEL_TEMPLATE, rowData[SysCompanyFaceBook.Columns.Id].ToString( ), "[删除]")
                                    );
                    break;
                case "CompanyName":
                    result = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, rowData[column], "27%");
                    break;
            }
            return result;
        }
    }
}