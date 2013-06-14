using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SupportList : ActionPage
    {
        const string SUPPORT_STATUS = "<font color=\"#006600\">{0}</font>";
        const string SUPPORT_RATING = "<font color=\"green\"><b>{0}</b></font>";
        const string ONLINE_STATS = "<font color=\"red\" class=\"support_id\" member_id=\"{0}\" ></font>";

        const string NOT_START_SUPPORT_LIST = "/Admin/SupportList.aspx?Status=1";
        const string IN_PROGRESS_SUPPORT_LIST_A = "/Admin/SupportList.aspx?Status=2";
        const string IN_PROGRESS_SUPPORT_LIST_B = "/Admin/SupportList.aspx?Status=4";
        const string COMPLETED_SUPPORT_LIST = "/Admin/SupportList.aspx?Status=3";

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
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(SysSupport.Schema), SysMember.Columns.MemberFullname).From<SysSupport>( )
                                    .InnerJoin(SysMember.IdColumn, SysSupport.SupportFidColumn)
                                    .Where(SysSupport.SupportDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                                    .AndEx(SysMember.Columns.MemberPhoneNumber).Like(filterMemberTel)
                                    .Or(SysMember.MemberFullnameColumn).Like(filterMemberTel)
                                    .CloseEx( )
                                    .OrderDesc(SysSupport.Columns.SupportDate);
            int status = Status;
            if (status <= 0)
                status = 1;
            if (status == 4)
            {
                query = query.And(SysSupport.SupportStatusColumn).IsEqualTo(2)
                             .And(SysSupport.SupportIsReadColumn).IsEqualTo(0);
            }
            else
            {
                query = query.And(SysSupport.SupportStatusColumn).IsEqualTo(status);
            }
            gridView.DataSource = query;
            gridView.AddShowColumn(SysSupport.SupportIdColumn)
                    .AddShowColumn(SysSupport.SupportSubjectColumn)
                    .AddShowColumn(SysMember.MemberFullnameColumn)
                    .AddShowColumn(SysSupport.SupportStatusColumn)
                    .AddShowColumn(SysSupport.SupportRatingColumn)
                    .AddShowColumn(SysSupport.SupportDateColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(WebChatBLL.gridView_OnDataBindColumn);
            gridView.DataBind( );
        }



        protected int Status
        {
            get
            {
                return Utilities.ToInt(Params["Status"]);
            }
        }
        //public override string RenderNavName(SysNavigation item, MenuRegion region)
        //{
        //    if (region == MenuRegion.Left)
        //    {
        //        var query = DB.Select( ).From<SysSupport>( );
        //        switch (item.NavUrl)
        //        {
        //            case NOT_START_SUPPORT_LIST:
        //                query = query.Where(SysSupport.SupportStatusColumn).IsEqualTo(1);
        //                goto lable_exe;
        //            case IN_PROGRESS_SUPPORT_LIST_A:
        //                query = query.Where(SysSupport.SupportStatusColumn).IsEqualTo(2);
        //                goto lable_exe;
        //            case IN_PROGRESS_SUPPORT_LIST_B:
        //                query = query.Where(SysSupport.SupportStatusColumn).IsEqualTo(2)
        //                             .And(SysSupport.SupportIsReadColumn).IsEqualTo(0);
        //                goto lable_exe;
        //            case COMPLETED_SUPPORT_LIST:
        //                query.Where(SysSupport.SupportStatusColumn).IsEqualTo(3);
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
    }
}