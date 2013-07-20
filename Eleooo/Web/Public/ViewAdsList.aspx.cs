using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Public
{
    public partial class ViewAdsList : ActionPage
    {
        const string FIRST_AREA = "FirstAreaName";
        private bool _isLogin;
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            _isLogin = CurrentSubSys != SubSystem.ALL;
            //if (_isLogin && !UserBLL.UserHasArea(CurrentUser))
            //{
            //    Utilities.Redirect("/Member/MyArea.aspx", false);
            //    return;
            //}

            filter.ResetPageIndexControl = view.PageIndexControlId;
            filter.OnGetAreaName += new Web.Controls.OnGetAreaNameHandler(filter_OnGetAreaName);
            filter.OnGetTypeName += new Web.Controls.OnGetTypeNameHandler(filter_OnGetTypeName);
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyAd.Schema),
                                  CompanyAdsBLL.RenderGetAdPointFunc( ),
                                  AreaBLL.GetFirstAreaName(SysCompanyAd.AreaDepthColumn, FIRST_AREA))
                          .From<SysCompanyAd>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyAd.AdsCompanyIDColumn)
                          .Where(SysCompanyAd.AdsEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                          .And(SysCompanyAd.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyAd.IsPassColumn).IsEqualTo(true)
                          .ConstraintExpression("AND({0} = {1})", CompanyAdsBLL.RenderCheckAdsDayLimit( ), 1)
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyAd.AreaDepthColumn, this.City.Depth, true), 1)
                          .OrderDesc(Utilities.GetTableColumn(SysCompanyAd.AdsIDColumn));
            int companyID = Utilities.ToInt(Request.Params["CompanyID"]);
            if (companyID > 0)
                query.And(SysCompanyAd.AdsCompanyIDColumn).IsEqualTo(companyID);
            if (!string.IsNullOrEmpty(filter.CurTypeValue))
                query.And(SysCompany.CompanyMemoColumn).IsEqualTo(filter.CurTypeValue);
            if (!string.IsNullOrEmpty(filter.CurAreaValue))
            {
                if (!_isLogin)
                    query.ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyAd.AreaDepthColumn, filter.CurAreaValue, true), 1);
                else
                    query.ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(filter.CurAreaValue, SysCompanyAd.AreaDepthColumn, false), 1);
            }
            if (Request.QueryString["debug"] == "1")
                Response.Write(query.ToString( ));
            view.ItemCreated += new RepeaterItemEventHandler(view_ItemCreated);
            view.QuerySource = query;
            view.DataBind( );

            //bind rec items
            var items = CompanyAdsBLL.GetCookieRecViewAds( );
            if (items.Count > 0)
            {
                var queryRecAds = DB.Select(Utilities.GetTableColumns(SysCompanyAd.Schema),
                                            CompanyAdsBLL.RenderGetAdPointFunc( ),
                                            AreaBLL.GetFirstAreaName(SysCompanyAd.AreaDepthColumn, FIRST_AREA))
                                    .From<SysCompanyAd>( )
                                    .InnerJoin(SysCompany.IdColumn, SysCompanyAd.AdsCompanyIDColumn)
                                    .Where(SysCompanyAd.AdsEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                                    .And(SysCompanyAd.IsDeletedColumn).IsEqualTo(false)
                                    .And(SysCompanyAd.IsPassColumn).IsEqualTo(true)
                                    .And(SysCompanyAd.AdsIDColumn).In(items.ToArray( ))
                                    .ConstraintExpression("AND({0} = {1})", CompanyAdsBLL.RenderCheckAdsDayLimit( ), 1)
                                    .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyAd.AreaDepthColumn, this.City.Depth, true), 1);
                BindEvalHandler((item, exp, val) => Formatter.SubStr(val, 15));
                BindEvalHandler((item, exp, val) => Utilities.ToDecimal(CompanyAdsBLL.GetMaxOrderSumOrPoint(Convert.ToString(val), true)).ToString("0.###"));
                viewRecAds.QuerySource = queryRecAds;
                viewRecAds.DataBind( );
            }
        }
        Dictionary<string, object> _dataItem;
        Dictionary<string, object> DataItem
        {
            get
            {
                if (_dataItem == null)
                    _dataItem = new Dictionary<string, object>( );
                return _dataItem;
            }
        }
        void view_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                var rowData = e.Item.DataItem as System.Data.DataRowView;
                string sumAndPoint = Utilities.ToString(rowData[SysCompanyAdsPointSetting.Columns.AdsPoint]);
                DataItem["AdsID"] = rowData[SysCompanyAd.Columns.AdsID];
                DataItem["AdsPic"] = rowData[SysCompanyAd.Columns.AdsPic];
                DataItem["AdsTitle"] = Formatter.SubStr(rowData[SysCompanyAd.Columns.AdsTitle], 30);
                DataItem["AdsPoint"] = Utilities.ToDecimal(CompanyAdsBLL.GetMaxOrderSumOrPoint(sumAndPoint, true)).ToString("0.###");
                DataItem["AdsArea"] = string.Format("{0}{1}", rowData[FIRST_AREA], AreaBLL.GetAreaTag(rowData[SysCompanyAd.Columns.AreaDepth]));
                DataItem["AdsClicked"] = rowData[SysCompanyAd.Columns.AdsClicked];
                e.Item.DataItem = DataItem;
            }
        }

        string filter_OnGetTypeName(string typeName)
        {
            var query = DB.Select( ).From<SysCompanyAd>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyAd.AdsCompanyIDColumn)
                          .Where(SysCompanyAd.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyAd.AdsEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                          .And(SysCompanyAd.IsPassColumn).IsEqualTo(true)
                          .And(SysCompany.CompanyMemoColumn).Like(typeName)
                          .And(SysCompanyAd.AdsMemberLimitColumn).IsEqualTo(0)
                          .ConstraintExpression("AND({0} = {1})", CompanyAdsBLL.RenderCheckAdsDayLimit( ), 1)
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyAd.AreaDepthColumn, this.City.Depth, false), 1);
            return string.Format("{0}<span>({1})</span>", typeName, query.GetRecordCount( ));
        }

        string filter_OnGetAreaName(string areaName, string depth)
        {
            var query = DB.Select( ).From<SysCompanyAd>( )
                          .Where(SysCompanyAd.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyAd.AdsEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                          .And(SysCompanyAd.IsPassColumn).IsEqualTo(true)
                          .ConstraintExpression("AND({0} = {1})", CompanyAdsBLL.RenderCheckAdsDayLimit( ), 1);
            if (_isLogin)
                query.ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(depth, SysCompanyAd.AreaDepthColumn, false), 1);
            else
                query.ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyAd.AreaDepthColumn, depth, true), 1);
            return string.Format("{0}<span>({1})</span>", areaName, query.GetRecordCount( ));
        }

        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            CompanyAdsBLL.RemoveRecViewAds( );
            On_ActionQuery(sender, e);
        }
    }
}