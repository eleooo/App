using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Linq;
using System.IO;
using Eleooo.DAL;
using System.Web.UI;
using System.Text;
using Eleooo.Web.Controls;
using Eleooo.Common;

namespace Eleooo.Web.Public
{
    /// <summary>
    /// OrderMealServices 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class OrderMealServices : System.Web.Services.WebService
    {

        [WebMethod]
        public string AddFavCompany(int companyId)
        {
            if (companyId > 0 && AppContext.Context.CurrentSubSys != SubSystem.ALL)
            {
                var ret = UserBLL.AddUserFavCompany(AppContext.Context.User.Id, companyId);
                if (ret==1)
                {
                    UserBLL.RemoveUserFavCompany(AppContext.Context.User.Id, companyId);
                    return Utilities.GetWebServicesResult(1, "已从你的餐厅里删除");
                }
                else if(ret==0)
                    return Utilities.GetWebServicesResult(0, "收藏成功");
                else
                    return Utilities.GetWebServicesResult(-1, "你最多只允许收藏五个商家.");
            }
            else
                return Utilities.GetWebServicesResult(-1, "请选择商家");
        }
        //[WebMethod]
        //public string GetMsnCode(string phone)
        //{
        //    string message;
        //    int logId;
        //    if (!MsnBLL.GetMsnCode(phone, out message, out logId))
        //        return Utilities.GetWebServicesResult(-1, message);
        //    else
        //        return Utilities.GetWebServicesResult(logId, string.Format("验证码已经发送到:{0}", phone));
        //}
        [WebMethod]
        public string OrderMeal(string userData, string orderData)
        {
            string message = "失败";
            int orderId;
            int code = OrderMealBLL.SaveOrderMeal(userData, orderData, out orderId, out message);
            //code = -1 fail 1 success 2 success and is new user.
            var result = new
            {
                code = code,
                orderId = orderId,
                message = message
            };
            return Utilities.ObjToJSON(result);
        }

        [WebMethod]
        public string CancelOrder(int orderId)
        {
            string message;
            lock (OrderMealBLL.LockScopeAction(orderId))
            {
                int code = OrderMealBLL.CancelOrder(orderId, out message) ? 0 : -1;
                return Utilities.GetWebServicesResult(code, message);
            }
        }

        [WebMethod]
        public string CompleteOrder(int orderId)
        {
            string message;
            lock (OrderMealBLL.LockScopeAction(orderId))
            {
                int code = OrderMealBLL.CompleteOrder(orderId, out message) ? 0 : -1;
                return Utilities.GetWebServicesResult(code, message);
            }
        }

        [WebMethod]
        public string UgrentOrder(int orderId)
        {
            string message;
            int code = OrderMealBLL.UgrentOrder(orderId, out message);
            return Utilities.GetWebServicesResult(code, message);
        }

        [WebMethod]
        public string RenderOrderProgress(int orderId)
        {
            Page page = new Page( );
            int code = -1;
            try
            {
                StringBuilder sb = new StringBuilder( );
                var control = page.LoadControl("~/Controls/UcOrderMealStatus.ascx") as Controls.UcOrderMealStatus;
                page.Controls.Add(control);
                page.EnableViewState = false;
                using (TextWriter writer = new StringWriter(sb))
                {
                    Context.Server.Execute(page, writer, true);
                }
                if (control.IsNeedReload)
                    return "1";
                else
                    return sb.ToString( );
            }
            catch (Exception ex)
            {
                Logging.Log("OrderMealServices->RenderOrderProgress", ex);
                return Utilities.GetWebServicesResult(code, ex.Message);
            }
            finally
            {
                page.Dispose( );
            }
        }
        //[WebMethod]
        //public string SendMessage(int orderId, long orderSessionVal, int msnType, string message, string orders)
        //{
        //    string result;
        //    int code = OrderMealBLL.ConfirmOrder(orderId, orderSessionVal, msnType, message, orders, out result);
        //    return Utilities.GetWebServicesResult(code, result);
        //}

        [WebMethod]
        public string QueryOrderMeal(string txtCompany, string txtMember, int rbStatus, int rbModel, string txtDateStart, string txtDateEnd, int cboPage, int txtPageIndex, bool isViewAll)
        {
            try
            {
                //if (AppContext.Context.CurrentSubSys != SubSystem.Admin ||
                //    !AppContext.Context.User.AdminRoleId.HasValue ||
                //      AppContext.Context.User.AdminRoleId.Value <= 0)
                //    return Utilities.GetWebServicesResult(-1, "你无权使用此服务.");
                using (Page page = new Page( )
                    {
                        EnableViewState = false
                    })
                {
                    System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm( );
                    page.Controls.Add(form);
                    var gridView = (UcGridView)(page.LoadControl("~/Controls/UcGridView.ascx"));
                    var container = new System.Web.UI.HtmlControls.HtmlGenericControl("div")
                    {
                        ID = "container"
                    };
                    gridView.AllowPaper = true;
                    gridView.ShowHeader = true;
                    form.Controls.Add(container);
                    container.Controls.Add(gridView);
                    form.ID = "xForm";
                    page.Load += new EventHandler((sender, e) =>
                    {
                        App.SetOrderMealQueryView(gridView, txtCompany, txtMember, rbStatus, rbModel, txtDateStart, txtDateEnd,isViewAll);
                    });
                    StringBuilder sb = new StringBuilder( );
                    using (var writer = new StringWriter(sb))
                    {
                        HttpContext.Current.Server.Execute(page, writer, true);
                    }
                    return sb.ToString( );
                }
            }
            catch (Exception ex)
            {
                Logging.Log("OrderMealServices->QueryOrderMeal", ex);
                return Utilities.GetWebServicesResult(-1, ex.Message);
            }
        }

        [WebMethod]
        public string ChangeOrderStatus(int orderId, int nStatus)
        {
            string message = "提交成功";
            int code = -1;
            lock (OrderMealBLL.LockScopeAction(orderId))
            {
                if (nStatus == (int)OrderStatus.Canceled)
                    code = OrderMealBLL.CancelOrder(orderId, out message, true) ? 0 : -1;
                else if (nStatus == (int)OrderStatus.Completed)
                    code = OrderMealBLL.CompleteOrder(orderId, out message, true) ? 0 : -1;
            }
            //else
            //{
            //    code = 0;
            //    OrderMealBLL.UpdateOrderStatus(orderId, nStatus);
            //}
            return Utilities.GetWebServicesResult(code, message);
        }
    }
}
