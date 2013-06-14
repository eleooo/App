using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using SubSonic;
using Eleooo.Common;

namespace Eleooo.Web.Company
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
            if (AppContext.Context.CompanyType == CompanyType.MealCompany )
            {
                lblMessage.InnerHtml = "阁下的商家类型无权使用此功能";
                return;
            }
            DateTime dtBegin = Utilities.ToDateTime(txtDateStart.Value);
            DateTime dtEnd = Utilities.ToDateTime(txtDateEnd.Value).AddDays(1).Date;
            string filterMemberTel = string.IsNullOrEmpty(txtMemberPhone.Value) ? "%" : string.Concat(txtMemberPhone.Value.Trim( ), "%");
            gridView.DataSource = GetAdsQuery(rblFlag.SelectedValue, dtBegin, dtEnd, filterMemberTel);
            gridView.AddShowColumn(VSysMemberAdsSum.AdsClickDateColumn)
                    .AddShowColumn(VSysMemberAdsSum.AdsTitleColumn)
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
                    result = string.Format("<div style=\"text-align:left\">{0}</div>", Formatter.SubStr(rowData[column], 25));
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        SqlQuery GetAdsQuery(string flag, DateTime dtBegin, DateTime dtEnd, string filter)
        {
            if (flag == "2")
                return GetOuterQuery(dtBegin, dtEnd, filter);
            else if (flag == "1")
                return GetOwnerQuery(dtBegin, dtEnd, filter);
            else
                return GetAllQuery(dtBegin, dtEnd, filter);
        }

        SqlQuery GetAllQuery(DateTime dtBegin, DateTime dtEnd, string filter)
        {
            var query = DB.Select(Utilities.GetTableColumns(VSysMemberAdsSum.Schema))
                          .From<VSysMemberAdsSum>( )
                          .Where(VSysMemberAdsSum.AdsDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                          .And(VSysMemberAdsSum.AdsEndDateColumn).IsLessThan(dtEnd)
                          .And(VSysMemberAdsSum.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .AndEx(VSysMemberAdsSum.MemberPhoneNumberColumn).Like(filter)
                          .Or(VSysMemberAdsSum.MemberFullnameColumn).Like(filter)
                          .CloseEx( )
                          .OrderDesc(Utilities.GetTableColumn(VSysMemberAdsSum.AdsClickDateColumn));
            return query;
        }
        SqlQuery GetOuterQuery(DateTime dtBegin, DateTime dtEnd, string filter)
        {
            var query = DB.Select(Utilities.GetTableColumns(VSysMemberAdsSum.Schema))
                          .From<VSysMemberAdsSum>( )
                           .Where(VSysMemberAdsSum.AdsDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                          .And(VSysMemberAdsSum.AdsEndDateColumn).IsLessThan(dtEnd)
                          .And(VSysMemberAdsSum.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .ConstraintExpression(CompanyBLL.RenderIsOwner(VSysMemberAdsSum.AdsMemberIDColumn, VSysMemberAdsSum.CompanyIDColumn, MemberFilter.Outer))
                          .AndEx(VSysMemberAdsSum.MemberPhoneNumberColumn).Like(filter)
                          .Or(VSysMemberAdsSum.MemberFullnameColumn).Like(filter)
                          .CloseEx( )
                          .OrderDesc(Utilities.GetTableColumn(VSysMemberAdsSum.AdsClickDateColumn));
            return query;
        }
        SqlQuery GetOwnerQuery(DateTime dtBegin, DateTime dtEnd, string filter)
        {
            var query = DB.Select(Utilities.GetTableColumns(VSysMemberAdsSum.Schema))
                          .From<VSysMemberAdsSum>( )
                          .Where(VSysMemberAdsSum.AdsDateColumn).IsGreaterThanOrEqualTo(dtBegin)
                          .And(VSysMemberAdsSum.AdsEndDateColumn).IsLessThan(dtEnd)
                          .And(VSysMemberAdsSum.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId)
                          .ConstraintExpression(CompanyBLL.RenderIsOwner(VSysMemberAdsSum.AdsMemberIDColumn, VSysMemberAdsSum.CompanyIDColumn, MemberFilter.Owner))
                          .AndEx(VSysMemberAdsSum.MemberPhoneNumberColumn).Like(filter)
                          .Or(VSysMemberAdsSum.MemberFullnameColumn).Like(filter)
                          .CloseEx( )
                          .OrderDesc(Utilities.GetTableColumn(VSysMemberAdsSum.AdsClickDateColumn));
            return query;
        }
    }
}