using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Data;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class CompanyMyAds : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.txtDateStart.Value = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day)).ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            var query = DB.Select(Utilities.GetTableColumns(VSysMemberAdsSum.Schema, VSysMemberAdsSum.CompanyNameColumn),
                                  Utilities.GetTableColumn(SysCompany.CompanyNameColumn))
                          .From<VSysMemberAdsSum>( )
                          .InnerJoin(SysCompany.IdColumn, VSysMemberAdsSum.CompanyIDColumn)
                          .Where(VSysMemberAdsSum.AdsClickDateColumn).IsBetweenAnd(dtBegin, dtEnd)
                          .And(VSysMemberAdsSum.AdsMemberIDColumn).IsEqualTo(CurrentUser.Id)
                          .AndEx(SysCompany.CompanyTelColumn).Like(filterCompanyTel)
                          .Or(SysCompany.CompanyNameColumn).Like(filterCompanyTel)
                          .CloseEx( );
            query.DefaultPagingSort = Utilities.GetTableColumn(VSysMemberAdsSum.AdsClickDateColumn) + " DESC";
            BindEvalHandler((item, exp, val) => Formatter.SubStr(val, 25));
            view.QuerySource = query;
            view.ItemCreated += new RepeaterItemEventHandler(view_ItemCreated);
            _AdsPointSum = 0;
            view.DataBind( );

        }
        decimal _AdsPointSum;
        void view_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                var dataRow = (DataRowView)e.Item.DataItem;
                _AdsPointSum += Utilities.ToDecimal(dataRow[VSysMemberAdsSum.AdsPointSumColumn.ColumnName]);
            }
            else if (e.Item.ItemType == ListItemType.Footer)
                e.Item.DataItem = _AdsPointSum;

        }
    }
}