using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class CompanyAdsList : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DateTime dt = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day));
                this.txtDateStart.Value = dt.ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = dt.AddMonths(2).AddDays(-1).ToString("yyyy-MM-dd");
            }
            txtDesc.InnerHtml = string.Empty;
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyAd.Schema),
                                  SysCompany.CompanyNameColumn.ColumnName,
                                  SysCompany.CompanyTelColumn.ColumnName
                                  ).From<SysCompanyAd>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyAd.AdsCompanyIDColumn)
                          .Where(SysCompanyAd.AdsDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(SysCompanyAd.IsDeletedColumn).IsEqualTo(false)
                          .AndEx(SysCompany.CompanyTelColumn.ColumnName).Like(filterCompanyTel)
                          .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                          .CloseEx( )
                          .OrderDesc(Utilities.GetTableColumn(SysCompanyAd.AdsIDColumn));
            gridView.DataSource = query;
            gridView.AddCustomColumn("BeginEndDate", "起止日期")
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(SysCompanyAd.AdsTitleColumn)
                    .AddShowColumn(SysCompanyAd.AdsClickedColumn)
                    .AddShowColumn(SysCompanyAd.AdsPointSumColumn)
                    .AddCustomColumn(SysCompanyAd.IsPassColumn.ColumnName, "状态")
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "BeginEndDate":
                    result = string.Format("{0}至{1}", Utilities.ToDate(rowData[SysCompanyAd.Columns.AdsDate]), Utilities.ToDate(rowData[SysCompanyAd.Columns.AdsEndDate]));
                    break;
                case "AdsTitle":
                    result = string.Format("<div style=\"text-align:left\">{0}</div>", Formatter.SubStr(rowData[column], 20));
                    break;
                case "IsPass":
                    bool isPass = Utilities.ToBool(rowData[SysCompanyAd.Columns.IsPass]);
                    result = isPass ? "已审核" : "未审核";
                    break;
                case "Action":
                    result = string.Concat("[", string.Format(ACTION_DLG_TEMPLATE, rowData[SysCompanyAd.Columns.AdsID], "详细"), "]&nbsp;&nbsp;[",
                                           string.Format(ACTION_DEL_TEMPLATE, rowData[SysCompanyAd.Columns.AdsID], "删除"), "]");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            int id = Utilities.ToInt(EVENTARGUMENT);
            if (id > 0)
            {
                SysCompanyAd.Delete(id);
                txtDesc.InnerHtml = "删除成功";
            }
            else
                txtDesc.InnerHtml = "参数错误";
            On_ActionQuery(sender, e);
        }
    }
}