using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class MyCompanyR : ActionPage
    {
        const string MOD_ROW_TEMPLATE = @"<tr class='os' align='middle'>
                                            {0}
                                          </tr>";
        const string SIG_ROW_TEMPLATE = @"<tr>
                                            {0}
                                          </tr>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
            //txtMessage.InnerHtml = string.Empty;
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            var query = DB.Select( ).From<SysMemberCompanyR>( )
                          .Where(SysMemberCompanyR.CompanyMemberIDColumn).IsEqualTo(CurrentUser.Id)
                          .And(SysMemberCompanyR.CompanyDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .AndEx(SysMemberCompanyR.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                          .Or(SysMemberCompanyR.CompanyNameColumn).Like(filterCompanyTel)
                          .CloseEx( )
                          .OrderDesc(SysMemberCompanyR.IdColumn.ColumnName);
            gridView.DataSource = query;
            gridView.AddShowColumn(SysMemberCompanyR.CompanyDateColumn)
                    .AddShowColumn(SysMemberCompanyR.CompanyNameColumn)
                    .AddShowColumn(SysMemberCompanyR.CompanyTelColumn)
                //.AddShowColumn(SysMemberCompanyR.CompanyAddressColumn)
                //.AddShowColumn(SysMemberCompanyR.CompanyDescColumn);
                    .AddShowColumn(SysMemberCompanyR.CompanyStatusColumn);
                    //.AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.OnDataBindRow += new Web.Controls.DataBindRowHandler(gridView_OnDataBindRow);
            gridView.DataBind( );
        }
        string gridView_OnDataBindRow(int rowIndex, System.Data.DataRow rowData, ref bool isRenderedRow)
        {
            isRenderedRow = false;
            int mod;
            Math.DivRem(rowIndex + 1, 2, out mod);
            if (mod == 0)
                return MOD_ROW_TEMPLATE;
            else
                return SIG_ROW_TEMPLATE;
        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            //isRenderedCell = true;
            switch (column)
            {
                //case "CompanyDate":
                //    result = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, Utilities.ToDate(rowData[column]), "8%");
                //    break;
                //case "CompanyName":
                //    result = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, rowData[column], "18%");
                //    break;
                //case "CompanyAddress":
                //    result = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, rowData[column], "20%");
                //    break;
                //case "CompanyTel":
                //    result = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, rowData[column], "12%");
                //    break;
                //case "CompanyDesc":
                //    result = string.Format(ALIGN_LEFT_CELL_WIDTH_TEMPLATE, Utilities.ToHTML(rowData[column]), "32%");
                //    break;
                case "Action":
                    isRenderedCell = false;
                    result = string.Concat(
                                    string.Format(ACTION_DLG_TEMPLATE, rowData[SysMemberCompanyR.Columns.Id].ToString( ), "[编辑]"),
                                    "&nbsp;&nbsp;",
                                    string.Format(ACTION_DEL_TEMPLATE, rowData[SysMemberCompanyR.Columns.Id].ToString( ), "[删除]")
                                    );
                    break;
                case "CompanyStatus":
                    result = GetCompanyRStatus(rowData[SysMemberCompanyR.Columns.CompanyStatus]);
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        public static string GetCompanyRStatus(object status)
        {
            CompanyRStatus s = Formatter.ToEnum<CompanyRStatus>(status);
            if (s == CompanyRStatus.InProgress)
                return "洽谈中";
            else
                return "已加盟";
        }
    }
}