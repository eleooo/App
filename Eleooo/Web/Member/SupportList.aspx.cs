using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class SupportList : ActionPage
    {
        const string SUPPORT_STATUS = "<font color=\"#006600\">{0}</font>";
        const string SUPPORT_RATING = "<font color=\"green\"><b>{0}</b></font>";
        const string ALL_SUPPORT_LIST = "/Member/SupportList.aspx";
        const string NOT_START_SUPPORT_LIST = "/Member/SupportList.aspx?Status=1";
        const string IN_PROGRESS_SUPPORT_LIST_A = "/Member/SupportList.aspx?Status=2";
        const string COMPLETED_SUPPORT_LIST = "/Member/SupportList.aspx?Status=3";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd" );
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            var query = DB.Select(Utilities.GetTableColumns(SysSupport.Schema), SysMember.Columns.MemberFullname).From<SysSupport>( )
                                    .InnerJoin(SysMember.IdColumn, SysSupport.SupportFidColumn)
                                    .Where(SysSupport.SupportDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .And(SysSupport.SupportFidColumn).IsEqualTo(CurrentUser.Id);
            int status = Status;
            if (status > 0)
                query = query.And(SysSupport.SupportStatusColumn).IsEqualTo(status);

            gridView.DataSource = query;
            gridView.AddShowColumn(SysSupport.SupportIdColumn)
                    .AddShowColumn(SysSupport.SupportSubjectColumn)
                    .AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysSupport.SupportStatusColumn)
                    .AddShowColumn(SysSupport.SupportRatingColumn)
                    .AddShowColumn(SysSupport.SupportDateColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }
        public static string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;

            switch (column)
            {
                case "Support_Subject":
                    isRenderedCell = true;
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, string.Format(ACTION_DLG_TEMPLATE, rowData["Support_ID"], rowData[column]));
                    break;
                case "Support_Status":
                    result = string.Format(SUPPORT_STATUS, WebChatBLL.GetStatusString(Convert.ToInt32(rowData[column])));
                    break;
                case "Support_Rating":
                    result = string.Format(SUPPORT_RATING, WebChatBLL.GetRatingString(Convert.ToInt32(rowData[column])));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        //public override string RenderNavName(SysNavigation item, MenuRegion region)
        //{
        //    if (region == MenuRegion.Left)
        //    {
        //        var query = DB.Select( ).From<SysSupport>( )
        //                                .Where(SysSupport.SupportFidColumn).IsEqualTo(CurrentUser.Id);
        //        switch (item.NavUrl)
        //        {
        //            case NOT_START_SUPPORT_LIST:
        //                query = query.And(SysSupport.SupportStatusColumn).IsEqualTo(1);
        //                goto lable_exe;
        //            case IN_PROGRESS_SUPPORT_LIST_A:
        //                query = query.And(SysSupport.SupportStatusColumn).IsEqualTo(2);
        //                goto lable_exe;
        //            case COMPLETED_SUPPORT_LIST:
        //                query = query.And(SysSupport.SupportStatusColumn).IsEqualTo(3);
        //                goto lable_exe;
        //            case ALL_SUPPORT_LIST:
        //                goto lable_exe;
        //        }
        //        return base.RenderNavName(item, region);
        //    lable_exe:
        //        int nCount = query.GetRecordCount( );
        //        return string.Format("{0}({1})", item.NavName, nCount);
        //    }
        //    else
        //        return base.RenderNavName(item, region);
        //}
        protected int Status
        {
            get
            {
                return Utilities.ToInt(Params["Status"]);
            }
        }
    }
}