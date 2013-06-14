using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Text;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class MyCompany : ActionPage
    {
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
                    return CurrentUser.AreaDepth1;
                else if (AreaDepth == 2)
                    return CurrentUser.AreaDepth3;
                else
                    return CurrentUser.AreaDepth2;
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            //filter.OnGetAreaName += new Web.Controls.OnGetAreaNameHandler(filter_OnGetAreaName);
            filter.OnGetTypeName += new Web.Controls.OnGetTypeNameHandler(filter_OnGetTypeName);
            filter.ResetPageIndexControl = view.PageIndexControlId;
            var query = DB.Select(Utilities.GetTableColumns(SysCompany.Schema),
                                  VCompanyMaxCashRate.Columns.CashRate,
                                  SysArea.Columns.AreaName
                                  ).From<SysCompany>( )
                          .LeftOuterJoin(VCompanyMaxCashRate.CashCompanyIDColumn, SysCompany.IdColumn)
                          .LeftOuterJoin(SysArea.DepthColumn, SysCompany.AreaDepthColumn)
                          .Where(SysCompany.CompanyTypeColumn).In((int)CompanyType.UnionCompany, (int)CompanyType.SpecialCompany)
                          .And(SysCompany.IdColumn).IsNotEqualTo(UserBLL.MainCompanyAccount.Id)
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompany.AreaDepthColumn, this.MemberAreaDepth, true), 1)
                          .OrderDesc(Utilities.GetTableColumn(SysCompany.IdColumn));
            if (!string.IsNullOrEmpty(filter.CurTypeValue))
                query.And(SysCompany.CompanyMemoColumn).Like("%" + filter.CurTypeValue + "%");
            //if (!string.IsNullOrEmpty(filter.CurAreaValue))
            //    query.ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompany.AreaDepthColumn, filter.CurAreaValue, true), 1);
            view.QuerySource = query;
            view.DataBind( );
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
        protected void view_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                var rowData = ((System.Data.DataRowView)e.Item.DataItem).Row;
                string[] rates = Utilities.ToString(rowData[SysCompany.Columns.CompanyRate]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string rate = Utilities.Max<string>(rates);
                if (!string.IsNullOrEmpty(rate))
                    rate = "最高" + Utilities.ToDecimal(rate).ToString("#####0.##") + "%";
                string cashRate = (Utilities.ToDecimal(rowData[VCompanyMaxCashRate.Columns.CashRate]) * 100M).ToString("######.##");
                if (!string.IsNullOrEmpty(cashRate))
                    cashRate = "最高" + cashRate + "折";
                int? itemID = CompanyItemBLL.GetCompanyLastItemID(CurrentUser.Id, Utilities.ToInt(rowData[SysCompany.Columns.Id]));
                //int? adsID = CompanyAdsBLL.GetCompanyLastAdsID(CurrentUser.Id, CurrentUser.MemberSex, Utilities.ToInt(rowData[SysCompany.Columns.Id]));
                DataItem["ID"] = rowData[SysCompany.Columns.Id];
                DataItem["CompanyName"] = rowData[SysCompany.Columns.CompanyName];
                DataItem["CompanyMemo"] = rowData[SysCompany.Columns.CompanyMemo];
                DataItem["CompanyArea"] = rowData[SysArea.Columns.AreaName];
                DataItem["CompanyPhone"] = rowData[SysCompany.Columns.CompanyPhone];
                DataItem["CompanyAddress"] = rowData[SysCompany.Columns.CompanyAddress];
                DataItem["Photo"] = Common.FileUpload.GetFilePath(Utilities.ToString(rowData[SysCompany.Columns.CompanyPhoto]), SaveType.Company);
                DataItem["Rate"] = rate;
                DataItem["CashRate"] = cashRate;
                DataItem["ItemID"] = itemID;
                e.Item.DataItem = DataItem;
            }
        }

        string filter_OnGetTypeName(string typeName)
        {
            var query = DB.Select( ).From<SysCompany>( )
                          .Where(SysCompany.CompanyMemoColumn).Like("%" + typeName + "%")
                          .And(SysCompany.IdColumn).IsGreaterThan(0)
                          .And(SysCompany.IdColumn).IsNotEqualTo(UserBLL.MainCompanyAccount.Id)
                          .And(SysCompany.CompanyTypeColumn).In((int)CompanyType.UnionCompany, (int)CompanyType.SpecialCompany)
                          .ConstraintExpression("AND({0} = {1})", AreaBLL.RenderCheckAreaDepthFunc(SysCompany.AreaDepthColumn, this.MemberAreaDepth, true), 1);
            return string.Format("{0}<span>({1})</span>", typeName, query.GetRecordCount( ));
        }

    }
}