using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Text;
using System.Data;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class MyCompany : ActionPage
    {
        const string IMAGE_HTML_TEMPLATE = "<td style=\"width: 200px; height: 120px;line-height:120%;\"><img src=\"{0}\" width=\"200px\" height=\"120px\" /></td>";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            //string companyCode = string.Concat(AreaBLL.GetCompanyCodePrefix(AppContext.Context.Company.CompanyCode), "%");//.Substring(0, AppContext.Context.Company.CompanyCode.Length - 2) + "%";
            var query = DB.Select(GetSelectList( ),
                                  "SUM(LastMonthOrderSum) AS LastMonthOrderSum",
                                  "SUM(ThisMonthOrderSum) AS ThisMonthOrderSum",
                                  "SUM(LastMonthPaymentCashSum) AS LastMonthPaymentCashSum",
                                  "SUM(ThisMonthPaymentCashSum) AS ThisMonthPaymentCashSum"
                                  ).From<SysCompany>( )
                          .LeftOuterJoin(VCompanyMaxCashRate.CashCompanyIDColumn, SysCompany.IdColumn)
                          .LeftOuterJoin(VCompanyLastThisMonthOrderAndCash.CompanyIDColumn, SysCompany.IdColumn)
                          .LeftOuterJoin(VCompanyMemberCount.CompanyIDColumn, SysCompany.IdColumn)
                          .Where(SysCompany.AreaDepthColumn).Like(AreaBLL.GetAreaDepthLike(AppContext.Context.Company.AreaDepth))
                          .And(SysCompany.IdColumn).IsNotEqualTo(UserBLL.MainCompanyAccount.Id)
                          .And(SysCompany.CompanyTypeColumn).IsEqualTo((int)CompanyType.UnionCompany)
                          .ConstraintExpression(" Group By {0} ", GetSelectList( ));
            gridView.DataSource = query;
            gridView.AddCustomColumn("CompanyPic", "商家图片")
                    .AddCustomColumn("CompanyInfo", "商家信息");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }
        string GetSelectList( )
        {
            return @"CompanyName,CompanyPhoto,CompanyAddress,CompanyPhone,CompanyRate,CashRate,MemberCount,ThisMonthMemberCount,CompanyArea,CompanyLocation,CompanyMemo";
        }
        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "CompanyPic":
                    string photo = Utilities.ToHTML(rowData[SysCompany.Columns.CompanyPhoto]);
                    if (!string.IsNullOrEmpty(photo))
                    {
                        isRenderedCell = true;
                        result = string.Format(IMAGE_HTML_TEMPLATE, Eleooo.Common.FileUpload.GetFilePath(photo, SaveType.Company));
                    }
                    break;
                case "CompanyInfo":
                    string template = txtTemplate.InnerHtml;
                    decimal dCashRate;
                    string companyRate;
                    foreach (DataColumn col in rowData.Table.Columns)
                    {
                        if (col.ColumnName == VCompanyMaxCashRate.Columns.CashRate)
                            template = template.Replace(GetColReplacement(col),
                                 (dCashRate = Utilities.ToDecimal(rowData[col])) > 0 ? string.Format("最高{0}折", (dCashRate * 10M).ToString("####.###")) : string.Empty);
                        else if (col.ColumnName == SysCompany.Columns.CompanyRate)
                        {
                            companyRate = string.Empty;
                            if (rowData[col] != null)
                            {
                                string[] rates = Utilities.ToString(rowData[col]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                string rate = Utilities.Max<string>(rates);
                                decimal dRate = Utilities.ToDecimal(rate);
                                if (dRate > 0)
                                    companyRate = string.Concat("最高", (dRate).ToString("#####0.##"), "%");
                            }
                            template = template.Replace(GetColReplacement(col), companyRate);
                        }
                        else
                            template = template.Replace(GetColReplacement(col), Utilities.ToString(rowData[col]));
                    }
                    isRenderedCell = true;
                    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, template);
                    break;
            }
            return result;
        }
        string GetColReplacement(DataColumn col)
        {
            return string.Concat("{", col.ColumnName, "}");
        }
    }
}