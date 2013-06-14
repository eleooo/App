using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.Web.Controls;
using System.Collections.Specialized;
using Eleooo.DAL;
using System.Diagnostics;
using Eleooo.Common;

namespace Eleooo.Web
{
    public partial class WebForm1 : ActionPage
    {

        void container_Render(HtmlTextWriter output, Control container)
        {
            //paper.IsRenderBottom = false;
            //paper.RenderControl(output);
            //paper.IsRenderHead = false;
            //paper.IsRenderBottom = true;
            //output.Write("<br />");
            //paper.RenderControl(output);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //MsnBLL.SendMessage("15875332521", "手机号码:15875332521.送餐地址:嘉里建设广场 广场三楼.餐点内容:[秘制叉烧饭:1份][叉鸡/叉鸭双拼饭:2份][深井烧鹅肶饭:2份][虫草花炖家鸡汤:2份]备注:最好在10点前送到.", 0, out message, out logId);
            //log.InnerHtml = string.Format("{0}-LogID={1}", message, logId);
            //gv.DataSource = DB.Select( ).Top("3").From<SysArea>( ).ExecuteDataTable( );
            //gv.DataBind( );
            //using (AreaSelector.Selector3)
            //{
            //    AreaSelector.Selector3.IsAllowMulti = true;
            //    AreaSelector.Selector3.DefaultMultiValue = "/1/2/,/1/2/3/";
            //    AreaSelector.Selector3.IsLoadScript = true;
            //    //this.container.InnerHtml = AreaSelector.Selector3.RenderResult( ).ToString( );
            //}
            //this.BindEvalHandler((dataItem, exp, val) => string.Format("Handler1:{0}", val))
            //    .BindEvalHandler((dataItem, exp, val) => string.Format("Handler2:{0}", val));
            //Stopwatch watch = new Stopwatch( );
            //watch.Start( );
            //var query = DB.Select( ).From<SysCompany>( );
            //gridView.PageSize = 20;
            //gridView.QuerySource = query;
            //gridView.AddShowColumn(SysCompany.CompanyCodeColumn)
            //        .AddShowColumn(SysCompany.CompanyNameColumn)
            //        .AddShowColumn(SysCompany.CompanyTelColumn)
            //        .DataBind( );
            //watch.Stop( );
            //txtLog.InnerHtml = string.Format("{0}:{1}",gridView.FetchType,watch.ElapsedMilliseconds);
        }

        string gridView_OnDataBindColumn(string column, System.Data.DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "OrderDetail":
                    result = string.Format("<a href=\"SiteAppPage/OrderDetail.aspx?OrderId={0}\">【查看】</a>", rowData["ID"]);
                    break;
                case "Action":
                    result = string.Format("<a href=\"SiteAppPage/EditOrderPage.aspx?OrderId={0}\">【查看】</a>", rowData["ID"]);
                    break;
                case "OrderStatus":
                    result = HtmlControl.GetSelectHtml(OrderMealBLL.OrderStatusSoruce, "OrderStatus", rowData[column]).ToString( );
                    break;
                default:
                    result = Utilities.ToString(rowData[column]);
                    break;
            }
            return result;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
    }
}