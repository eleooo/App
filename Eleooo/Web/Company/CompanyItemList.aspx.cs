using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class CompanyItemList : ActionPage
    {
        public string ItemType
        {
            get
            {
                CompanyType type = Formatter.ToEnum<CompanyType>(AppContext.Context.Company.CompanyType.Value);
                if (type == CompanyType.UnionCompany)
                    return "已订";
                else
                    return "已购";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DateTime dt = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day));
                this.txtDateStart.Value = (AppContext.Context.User.MemberDate ?? (DateTime?)dt).Value.ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = dt.AddMonths(2).AddDays(-1).ToString("yyyy-MM-dd");
            }
            txtDesc.InnerHtml = string.Empty;
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            if (CompanyBLL.GetCompanyType(AppContext.Context.Company.CompanyType) == CompanyType.AdCompany)
            {
                txtDesc.InnerHtml = "阁下为广告商家类型,不允许使用此功能";
            }
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            var query = DB.Select( ).From<SysCompanyItem>( )
                          .Where(SysCompanyItem.ItemDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyItem.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .OrderDesc(Utilities.GetTableColumn(SysCompanyItem.ItemIDColumn));
            gridView.DataSource = query;
            gridView.AddCustomColumn("BeginEndDate", "起止日期")
                    .AddShowColumn(SysCompanyItem.ItemTitleColumn)
                    .AddCustomColumn(SysCompanyItem.ItemAmountColumn.ColumnName, "数量")
                    .AddShowColumn(SysCompanyItem.ItemSumColumn)
                    .AddCustomColumn(SysCompanyItem.ItemPointColumn.ColumnName, "兑换")
                    .AddCustomColumn(SysCompanyItem.ItemClickedColumn.ColumnName, ItemType)
                    .AddCustomColumn(SysCompanyItem.ItemUsedColumn.ColumnName, "已消费")
                    .AddCustomColumn("Action", "状态");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
            if (Request.Params["Debug"] == "1")
            {
                Response.Write(AppContext.Context.Company.Id);
                Response.Write("<br />");
                Response.Write(CurrentUser.Id);
                Response.Write("<br />");
            }
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "BeginEndDate":
                    result = string.Format("{0}至{1}", Utilities.ToDate(rowData[SysCompanyItem.Columns.ItemDate]), Utilities.ToDate(rowData[SysCompanyItem.Columns.ItemEndDate]));
                    break;
                case "ItemTitle":
                    result = string.Format("<div style=\"text-align:left\">{0}</div>", Formatter.SubStr(rowData[column], 18));
                    break;
                case "Action":
                    //result = string.Concat("[", string.Format(ACTION_DLG_TEMPLATE, rowData[SysCompanyItem.Columns.ItemID], "详细"), "]&nbsp;&nbsp;[",
                    //                       string.Format(ACTION_DEL_TEMPLATE, rowData[SysCompanyItem.Columns.ItemID], "删除"), "]");
                    bool isPass = Utilities.ToBool(rowData[SysCompanyItem.Columns.IsPass]);
                    result = isPass ? "已审核" : "未审核";
                    break;
                case "RemindAmount":
                    result = (Utilities.ToInt(rowData[SysCompanyItem.Columns.ItemAmount]) - Utilities.ToInt(rowData[SysCompanyItem.Columns.ItemClicked])).ToString( );
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            if (CompanyBLL.GetCompanyType(AppContext.Context.Company.CompanyType) == CompanyType.AdCompany)
            {
                txtDesc.InnerHtml = "阁下为广告商家类型,不允许使用此功能";
                return;
            }
            int id = Utilities.ToInt(EVENTARGUMENT);
            if (id > 0)
            {
                SysCompanyItem.Delete(id);
                txtDesc.InnerHtml = "删除成功";
            }
            else
                txtDesc.InnerHtml = "参数错误";
            On_ActionQuery(sender, e);
        }
    }
}