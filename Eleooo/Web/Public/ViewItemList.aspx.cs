using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Public
{
    public partial class ViewItemList : ActionPage
    {
        private CompanyType? _cmpType = null;
        public CompanyType? CmpType
        {
            get
            {
                if (_cmpType == null)
                {
                    string p = Request.Params["CompanyType"];
                    if (!string.IsNullOrEmpty(p))
                        _cmpType = Formatter.ToEnum<CompanyType>(p);
                }
                return _cmpType;
            }
        }
        private bool _isLogin;
        const string FIRST_AREA = "FirstAreaName";
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            CompanyItemBLL.RemoveRecViewItems( );
            On_ActionQuery(sender, e);
        }
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
            var query = DB.Select(Utilities.GetTableColumns(SysCompanyItem.Schema),
                                  SysCompany.Columns.CompanyName,
                                  SysCompany.Columns.CompanyType,
                                  AreaBLL.GetFirstAreaName(SysCompanyItem.AreaDepthColumn, FIRST_AREA))
                          .From<SysCompanyItem>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyItem.CompanyIDColumn)
                          .Where(SysCompanyItem.ItemEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                          .And(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyItem.IsPassColumn).IsEqualTo(true)
                          .And(SysCompany.CompanyTypeColumn).IsNotEqualTo((int)CompanyType.MealCompany)
                          .ConstraintExpression(string.Format(" AND({0} - {1} > 0) ", Utilities.GetTableColumn(SysCompanyItem.ItemAmountColumn), Utilities.GetTableColumn(SysCompanyItem.ItemClickedColumn)))
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyItem.AreaDepthColumn, this.City.Depth, true), 1)
                          .OrderDesc(SysCompanyItem.ItemIDColumn.QualifiedName);
            int companyID = Utilities.ToInt(Request.Params["CompanyID"]);
            if (companyID > 0)
                query.And(SysCompany.IdColumn).IsEqualTo(companyID);
            if (!string.IsNullOrEmpty(filter.CurTypeValue))
                query.And(SysCompany.CompanyMemoColumn).IsEqualTo(filter.CurTypeValue);
            if (!string.IsNullOrEmpty(filter.CurAreaValue))
            {
                if (!_isLogin)
                    query.ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyItem.AreaDepthColumn, filter.CurAreaValue, true), 1);
                else
                    query.ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(filter.CurAreaValue, SysCompanyItem.AreaDepthColumn, false), 1);
            }
            if (CmpType.HasValue)
                query.And(SysCompany.CompanyTypeColumn).IsEqualTo((int)CmpType.Value);
            BindEvalHandler((item, exp, val) => Formatter.SubStr(val, 30));
            view.QuerySource = query;
            view.ItemCreated += new RepeaterItemEventHandler(view_ItemCreated);
            view.DataBind( );

            //bind rec items
            var items = CompanyItemBLL.GetCookieRecViewItems( );
            if (items.Count > 0)
            {
                var queryRecItem = DB.Select(Utilities.GetTableColumns(SysCompanyItem.Schema),
                                              SysCompany.Columns.CompanyName,
                                              SysCompany.Columns.CompanyType,
                                              AreaBLL.GetFirstAreaName(SysCompanyItem.AreaDepthColumn, FIRST_AREA))
                                      .From<SysCompanyItem>( )
                                      .InnerJoin(SysCompany.IdColumn, SysCompanyItem.CompanyIDColumn)
                                      .Where(SysCompanyItem.ItemEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                                      .And(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                                      .And(SysCompanyItem.IsPassColumn).IsEqualTo(true)
                                      .And(SysCompanyItem.ItemIDColumn).In(items.ToArray( ));
                viewRecItem.QuerySource = queryRecItem;
                BindEvalHandler((item, exp, val) => Formatter.SubStr(val, 10));
                viewRecItem.DataBind( );
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
                //CompanyType type = Formatter.ToEnum<CompanyType>(rowData[SysCompany.Columns.CompanyType]);
                DataItem["ItemID"] = rowData[SysCompanyItem.Columns.ItemID];
                DataItem["ItemPic"] = rowData[SysCompanyItem.Columns.ItemPic];
                DataItem["ItemTitle"] = rowData[SysCompanyItem.Columns.ItemTitle];
                DataItem["ItemPoint"] = Utilities.ToDecimal(rowData[SysCompanyItem.Columns.ItemPoint]).ToString("#######.###");
                DataItem["ItemSum"] = Utilities.ToDecimal(rowData[SysCompanyItem.Columns.ItemSum]).ToString("#######.###");
                DataItem["ItemClicked"] = rowData[SysCompanyItem.Columns.ItemClicked];
                DataItem["ItemUnClicked"] = Utilities.ToInt(rowData[SysCompanyItem.ItemAmountColumn.ColumnName]) - Utilities.ToInt(rowData[SysCompanyItem.ItemClickedColumn.ColumnName]);
                DataItem["ItemArea"] = string.Concat(rowData[FIRST_AREA], AreaBLL.GetAreaTag(rowData[SysCompanyItem.Columns.AreaDepth]));
                //DataItem["ItemBtnText1"] = type== CompanyType.UnionCompany? "我要订":"我要抢";
                //DataItem["ItemBtnText2"] = type == CompanyType.UnionCompany ? "已预订" : "我要订";
                e.Item.DataItem = DataItem;
            }
        }
        string filter_OnGetTypeName(string typeName)
        {
            var query = DB.Select( ).From<SysCompanyItem>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyItem.CompanyIDColumn)
                          .Where(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyItem.ItemEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                          .And(SysCompanyItem.IsPassColumn).IsEqualTo(true)
                          .And(SysCompany.CompanyMemoColumn).IsEqualTo(typeName)
                          .And(SysCompany.CompanyTypeColumn).IsNotEqualTo((int)CompanyType.MealCompany)
                          .ConstraintExpression(string.Format(" AND({0} - {1} > 0) ", Utilities.GetTableColumn(SysCompanyItem.ItemAmountColumn), Utilities.GetTableColumn(SysCompanyItem.ItemClickedColumn)))
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompany.AreaDepthColumn, this.City.Depth, false), 1);
            //query.And(SysCompanyItem.MemberLimitColumn).IsEqualTo(0);
            if (CmpType.HasValue)
                query.And(SysCompany.CompanyTypeColumn).IsEqualTo((int)CmpType.Value);
            return string.Format("{0}<span>({1})</span>", typeName, query.GetRecordCount( ));
        }

        string filter_OnGetAreaName(string areaName, string depth)
        {
            var query = DB.Select( ).From<SysCompanyItem>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyItem.CompanyIDColumn)
                          .Where(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyItem.ItemEndDateColumn).IsGreaterThanOrEqualTo(DateTime.Today)
                          .And(SysCompanyItem.IsPassColumn).IsEqualTo(true)
                          .And(SysCompany.CompanyTypeColumn).IsNotEqualTo((int)CompanyType.MealCompany)
                          .ConstraintExpression(string.Format(" AND({0} - {1} > 0) ", Utilities.GetTableColumn(SysCompanyItem.ItemAmountColumn), Utilities.GetTableColumn(SysCompanyItem.ItemClickedColumn)));
            if (_isLogin)
                query.ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(depth, SysCompanyItem.AreaDepthColumn, false), 1);
            else
                query.ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyItem.AreaDepthColumn, depth, true), 1);

            //query.And(SysCompanyItem.MemberLimitColumn).IsEqualTo(0);
            if (CmpType.HasValue)
                query.And(SysCompany.CompanyTypeColumn).IsEqualTo((int)CmpType.Value);
            return string.Format("{0}<span>({1})</span>", areaName, query.GetRecordCount( ));
        }
    }
}