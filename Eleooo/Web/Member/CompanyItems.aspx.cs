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
    public partial class CompanyItems : ActionPage
    {
        const string FIRST_AREA = "FirstAreaName";
        private int _areaDepth = 0;
        public int AreaDepth
        {
            get
            {
                if (_areaDepth == 0)
                    _areaDepth = Utilities.ToInt(Request.Params["areaType"]);
                return _areaDepth;
            }
        }
        private string MemberAreaDepth
        {
            get
            {
                if (AreaDepth == 1)
                    return CurrentUser.AreaDepth2;
                else if (AreaDepth == 2)
                    return CurrentUser.AreaDepth3;
                else
                    return CurrentUser.AreaDepth1;
            }
        }
        decimal _userLastOrderSum = -1;
        decimal userLastOrderSum
        {
            get
            {
                if (_userLastOrderSum == -1)
                    _userLastOrderSum = UserBLL.GetUserLastMonthOrderSum(CurrentUser.Id);
                return _userLastOrderSum;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            filter.OnGetAreaName += new Web.Controls.OnGetAreaNameHandler(filter_OnGetAreaName);
            filter.OnGetTypeName += new Web.Controls.OnGetTypeNameHandler(filter_OnGetTypeName);

            var query = DB.Select(Utilities.GetTableColumns(SysCompanyItem.Schema),
                                  SysCompany.Columns.CompanyName,
                                  SysCompany.Columns.CompanyType,
                                  AreaBLL.GetFirstAreaName(SysCompanyItem.AreaDepthColumn, FIRST_AREA))
                          .From<SysCompanyItem>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyItem.CompanyIDColumn)
                          .Where(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyItem.IsPassColumn).IsEqualTo(true)
                          .ConstraintExpression(string.Format(" AND({0} - {1} > 0) ", Utilities.GetTableColumn(SysCompanyItem.ItemAmountColumn), Utilities.GetTableColumn(SysCompanyItem.ItemClickedColumn)))
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(this.MemberAreaDepth, SysCompanyItem.AreaDepthColumn, true), 1)
                          .ConstraintExpression(CompanyItemBLL.RenderCheckItemFunc(CurrentUser.Id, userLastOrderSum, 1))
                          .OrderDesc(Utilities.GetTableColumn(SysCompanyItem.ItemIDColumn));
            int companyID = Utilities.ToInt(Request.Params["CompanyID"]);
            if (companyID > 0)
            {
                query = query.And(SysCompany.IdColumn).IsEqualTo(companyID);
            }
            if (!string.IsNullOrEmpty(filter.CurTypeValue))
                query.And(SysCompany.CompanyMemoColumn).Like("%" + filter.CurTypeValue + "%");
            if (!string.IsNullOrEmpty(filter.CurAreaValue))
                query.ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyItem.AreaDepthColumn, filter.CurAreaValue, true), 1);
            gridView.DataSource = query;
            gridView.AddCustomColumn("Action", "我要抢");
            gridView.OnBuildRow += new Web.Controls.BuildListViewRowHandler(gridView_OnBuildRow);
            gridView.DataBind( );
        }

        void gridView_OnBuildRow(int index, System.Data.DataRow rowData, bool isAlterRow, string rowTemplate, System.Text.StringBuilder buffer)
        {
            CompanyType companyType = Formatter.ToEnum<CompanyType>(rowData[SysCompany.Columns.CompanyType]);
            int count = Utilities.ToInt(rowData[SysCompanyItem.ItemAmountColumn.ColumnName]) - Utilities.ToInt(rowData[SysCompanyItem.ItemClickedColumn.ColumnName]);
            buffer.AppendFormat(rowTemplate, rowData[SysCompanyItem.Columns.ItemPic],
                                   rowData[FIRST_AREA],
                                   Formatter.SubStr(rowData[SysCompanyItem.Columns.ItemTitle], 30),
                                   Utilities.ToDecimal(rowData[SysCompanyItem.Columns.ItemSum]).ToString("#######.###"),
                                   Utilities.ToDecimal(rowData[SysCompanyItem.Columns.ItemPoint]).ToString("#######.###"),
                                   rowData[SysCompanyItem.Columns.ItemID],
                                   rowData[SysCompanyItem.Columns.ItemClicked],
                                   count,
                                   AreaBLL.GetAreaTag(rowData[SysCompanyItem.Columns.AreaDepth]),
                                   GetClassName(companyType),
                                   GetItemType(companyType));
        }
        string GetItemType(CompanyType companyType)
        {
            return "抢购";
            //if (companyType == CompanyType.SpecialCompany)
            //    return "抢购";
            //else
            //    return "预订";
        }
        string GetClassName(CompanyType companyType)
        {
            if (companyType == CompanyType.SpecialCompany)
                return "我来抢"; //我来抢
            else
                return "我要订"; //我要订
        }
        string filter_OnGetTypeName(string typeName)
        {
            var query = DB.Select( ).From<SysCompanyItem>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyItem.CompanyIDColumn)
                          .Where(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyItem.IsPassColumn).IsEqualTo(true)
                          .And(SysCompany.CompanyMemoColumn).Like("%" + typeName + "%")
                          .ConstraintExpression(string.Format(" AND({0} - {1} > 0) ", Utilities.GetTableColumn(SysCompanyItem.ItemAmountColumn), Utilities.GetTableColumn(SysCompanyItem.ItemClickedColumn)))
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(this.MemberAreaDepth, SysCompanyItem.AreaDepthColumn, true), 1)
                          .ConstraintExpression(CompanyItemBLL.RenderCheckItemFunc(CurrentUser.Id, userLastOrderSum, 1));
            LogMessage(query.ToString( ));
            return string.Format("{0}({1})", typeName, query.GetRecordCount( ));
        }

        string filter_OnGetAreaName(string areaName, string depth)
        {
            var query = DB.Select( ).From<SysCompanyItem>( )
                          .InnerJoin(SysCompany.IdColumn, SysCompanyItem.CompanyIDColumn)
                          .Where(SysCompanyItem.IsDeletedColumn).IsEqualTo(false)
                          .And(SysCompanyItem.IsPassColumn).IsEqualTo(true)
                          .ConstraintExpression(string.Format(" AND({0} - {1} > 0) ", Utilities.GetTableColumn(SysCompanyItem.ItemAmountColumn), Utilities.GetTableColumn(SysCompanyItem.ItemClickedColumn)))
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompanyItem.AreaDepthColumn, depth, true), 1)
                          .ConstraintExpression(CompanyItemBLL.RenderCheckItemFunc(CurrentUser.Id, userLastOrderSum, 1))
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(this.MemberAreaDepth, SysCompanyItem.AreaDepthColumn, true), 1);
            LogMessage(query.ToString( ));
            return string.Format("{0}({1})", areaName, query.GetRecordCount( ));
        }
    }
}