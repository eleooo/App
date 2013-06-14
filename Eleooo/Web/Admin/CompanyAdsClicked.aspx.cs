using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class CompanyAdsClicked : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DateTime dt = DateTime.Today.AddDays((double)(1 - DateTime.Today.Day));
                this.txtDateStart.Value = dt.ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = dt.AddMonths(2).AddDays(-1).ToString("yyyy-MM-dd");
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            string filterCompanyTel = string.IsNullOrEmpty(txtCompanyName.Value) ? "%" : string.Concat(txtCompanyName.Value.Trim( ), "%");
            gridView.DataSource = GetAllQuery(dtBegin, dtEnd, filterMemberTel, filterCompanyTel);
            gridView.AddShowColumn(VSysMemberAdsSum.AdsClickDateColumn)
                    .AddShowColumn(VSysMemberAdsSum.AdsTitleColumn)
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(VSysMemberAdsSum.MemberPhoneNumberColumn)
                    .AddShowColumn(VSysMemberAdsSum.CompanyNameColumn)
                    .AddShowColumn(VSysMemberAdsSum.OrderSumColumn)
                    .AddShowColumn(VSysMemberAdsSum.AdsCountColumn)
                    .AddShowColumn(VSysMemberAdsSum.AdsPointSumColumn);
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "AdsTitle":
                    result = string.Format("<div style=\"text-align:left\">{0}</div>", Formatter.SubStr(rowData[column], 12));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        SubSonic.SqlQuery GetAllQuery(DateTime dtBegin, DateTime dtEnd, string filter1, string filter2)
        {
            var query = DB.Select(Utilities.GetTableColumns(VSysMemberAdsSum.Schema),
                            Utilities.GetTableColumn(SysCompany.CompanyTelColumn))
                           .From<VSysMemberAdsSum>( )
                           .InnerJoin(SysCompany.IdColumn, VSysMemberAdsSum.CompanyIDColumn)
                           .Where(VSysMemberAdsSum.AdsDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                           .And(VSysMemberAdsSum.AdsEndDateColumn).IsLessThan(dtEnd)
                           .AndEx(VSysMemberAdsSum.MemberPhoneNumberColumn).Like(filter1)
                           .Or(VSysMemberAdsSum.MemberFullnameColumn).Like(filter1)
                           .CloseEx( )
                           .AndEx(SysCompany.CompanyTelColumn).Like(filter2)
                           .Or(SysCompany.CompanyNameColumn).Like(filter2)
                           .CloseEx( )
                           .OrderDesc(Utilities.GetTableColumn(VSysMemberAdsSum.AdsClickDateColumn));
            return query;
        }
    }
}