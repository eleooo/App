using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
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
            if (AppContext.Context.CompanyType == CompanyType.MealCompany)
            {
                txtDesc.InnerHtml = "阁下的商家类型无权使用此功能";
                return;
            }
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyAd.Schema)).From<SysCompanyAd>( )
                          .Where(SysCompanyAd.AdsDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(SysCompanyAd.AdsCompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .And(SysCompanyAd.IsDeletedColumn).IsEqualTo(false)
                          .OrderDesc(Utilities.GetTableColumn(SysCompanyAd.AdsIDColumn));
            gridView.DataSource = query;
            gridView.AddCustomColumn("BeginEndDate", "起止日期")
                    .AddShowColumn(SysCompanyAd.AdsTitleColumn)
                    .AddShowColumn(SysCompanyAd.AdsClickedColumn)
                    .AddShowColumn(SysCompanyAd.AdsPointSumColumn)
                    .AddCustomColumn(SysCompanyAd.IsPassColumn.ColumnName, "状态");
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
                    result = string.Format("<div style=\"text-align:left\">{0}</div>", Formatter.SubStr(rowData[column], 28));
                    break;
                case "Action":
                    //result = string.Concat("[", string.Format(ACTION_DLG_TEMPLATE, rowData[SysCompanyAd.Columns.AdsID], "详细"), "]&nbsp;&nbsp;[",
                    //                       string.Format(ACTION_DEL_TEMPLATE, rowData[SysCompanyAd.Columns.AdsID], "删除"), "]");
                case "IsPass":
                    bool isPass = Utilities.ToBool(rowData[SysCompanyAd.Columns.IsPass]);
                    result = isPass ? "已审核" : "未审核";
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            if (CompanyBLL.GetCompanyType(AppContext.Context.Company.CompanyType) == CompanyType.MealCompany)
            {
                txtDesc.InnerHtml = "你暂不允许使用此功能";
                return;
            }
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