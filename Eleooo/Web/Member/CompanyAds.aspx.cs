using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Transactions;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class CompanyAds : ActionPage
    {
        const string FIRST_AREA = "FirstAreaName";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMessage.InnerHtml = string.Empty;
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            //int id = Utilities.ToInt(EVENTARGUMENT);
            //string message;
            //CompanyAdsBLL.ClickCompanyAds(CurrentUser, id, out message);
            //txtMessage.InnerHtml = message;
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            decimal userLastOrderSum = UserBLL.GetUserLastMonthOrderSum(CurrentUser.Id);
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyAd.Schema),
                                  CompanyAdsBLL.RenderGetAdPointFunc( ),
                                  AreaBLL.GetFirstAreaName(SysCompanyAd.AreaDepthColumn, FIRST_AREA))
                          .From<SysCompanyAd>( )
                          .Where(SysCompanyAd.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyAd.AdsEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                          .And(SysCompanyAd.IsPassColumn).IsEqualTo(true)
                          .ConstraintExpression("AND({0} = {1})", CompanyAdsBLL.RenderCheckAdsDayLimit( ), 1)
                          .ConstraintExpression(CompanyAdsBLL.RenderCheckAdsFunc(CurrentUser.Id, CurrentUser.MemberSex, userLastOrderSum, 1));
            query.DefaultPagingSort = Utilities.GetTableColumn(SysCompanyAd.AdsIDColumn) + " DESC";
            int companyID = Utilities.ToInt(Request.Params["CompanyID"]);
            if (companyID > 0)
                query = query.And(SysCompanyAd.AdsCompanyIDColumn).IsEqualTo(companyID);
            gridView.DataSource = query;
            gridView.AddCustomColumn("Action", "我要看");
            gridView.OnBuildRow += new Web.Controls.BuildListViewRowHandler(gridView_OnBuildRow);
            gridView.DataBind( );
        }

        void gridView_OnBuildRow(int index, System.Data.DataRow rowData, bool isAlterRow, string rowTemplate, System.Text.StringBuilder buffer)
        {
            string sumAndPoint = Utilities.ToString(rowData[SysCompanyAdsPointSetting.Columns.AdsPoint]);
            buffer.AppendFormat(rowTemplate,
                                   rowData[SysCompanyAd.Columns.AdsID],
                                   rowData[SysCompanyAd.Columns.AdsPic],
                                   Formatter.SubStr(rowData[SysCompanyAd.Columns.AdsTitle], 30),
                                   Utilities.ToDecimal(CompanyAdsBLL.GetMaxOrderSumOrPoint(sumAndPoint, true)).ToString("0.###"),
                                   rowData[FIRST_AREA],
                                   rowData[SysCompanyAd.Columns.AdsClicked],
                                   AreaBLL.GetAreaTag(rowData[SysCompanyAd.Columns.AreaDepth]));
        }

    }
}