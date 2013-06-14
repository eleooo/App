using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class OrderCompanyMealItem : ActionPage
    {
        private int _MemberItemID = 0;
        public int MemberItemID
        {
            get
            {
                if (_MemberItemID == 0)
                    _MemberItemID = Utilities.ToInt(Request.Params["MemberItemID"]);
                return _MemberItemID;
            }
        }
        private SysMemberItem _memberItem;
        public SysMemberItem MemberItem
        {
            get
            {
                if (MemberItemID > 0)
                    _memberItem = SysMemberItem.FetchByID(MemberItemID);
                return _memberItem;
            }
        }

        private int _itemID = 0;
        public int ItemID
        {
            get
            {
                if (MemberItem != null)
                    return MemberItemID;
                else if (_itemID == 0)
                    _itemID = Utilities.ToInt(Request.Params["ItemID"]);
                return _itemID;
            }
        }
        private int _mansionId;
        public int MansionId
        {
            get
            {
                if (_mansionId == 0)
                    _mansionId = Utilities.ToInt(Request.Params["MansionId"]);
                return _mansionId;
            }
        }
        private SysAreaMansion _mansion;
        public SysAreaMansion Mansion
        {
            get
            {
                return _mansion ?? (_mansion = SysAreaMansion.FetchByID(MansionId));
            }
        }
        public CompanyType CmpType
        {
            get
            {
                return Formatter.ToEnum<CompanyType>(Company.CompanyType);
            }
        }
        private SysCompany _company;
        public SysCompany Company
        {
            get
            {
                if (_company == null)
                    _company = SysCompany.FetchByID(CompanyItem.CompanyID);
                return _company;
            }
        }

        private SysCompanyItem _companyItem;
        public SysCompanyItem CompanyItem
        {
            get
            {
                if (_companyItem == null)
                    _companyItem = SysCompanyItem.FetchByID(ItemID);
                return _companyItem;
            }
        }
        protected string ValidateMessage { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CompanyItem == null)
            {
                Utilities.ShowMessageRedirect("优惠项目不存在!");
                return;
            }
            else if (CmpType != CompanyType.MealCompany)
            {
                Utilities.Redirect(string.Format("/Member/OrderCompanyMealItem.aspx?ItemId={0}", ItemID), false);
                return;
            }
            else if (MansionId <= 0)
            {
                Utilities.Redirect("/Public/OrderMealPage.aspx");
                return;
            }
            //btnDel.Disabled = true;
            Order order = null;
            txtAddr.Value = (Mansion != null ? Mansion.Name : string.Empty);
            if (MemberItem != null)
            {
                order = Order.FetchByID(MemberItem.MemberID);
                if (order != null)
                {
                    if (order.OrderStatus == (int)OrderStatus.Completed)
                    {
                        Utilities.ShowMessageRedirect("此订单已经完成订餐!");
                        return;
                    }
                    else if (order.OrderStatus == (int)OrderStatus.Canceled)
                    {
                        Utilities.ShowMessageRedirect("此订单已经取消!");
                        return;
                    }
                    //btnDel.Disabled = false;
                }
            }

            if (order != null)
                txtAddr.Value += Utilities.ConcatAddres(order.OrderProduct);
            else
            {
                var addr = HttpUtility.UrlDecode(Utilities.GetCookieValue("addr"), System.Text.Encoding.UTF8);
                if (!string.IsNullOrEmpty(addr))
                    txtAddr.Value += addr;
                else
                {
                    var address = UserBLL.GetUserDefFavAddress(CurrentUser.Id, MansionId);
                    if(address.HasValue)
                        txtAddr.Value += Utilities.ConcatAddres(address.Value.name);
                }
            }
            ctNeedPay.Visible = CompanyItem.ItemNeedPay > 0;
            ValidateMessage = string.Empty;
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            //if (MemberItem != null)
            //{
            //    var order = Order.FetchByID(MemberItem.MemberID);
            //    if (order != null)
            //    {
            //        if (order.OrderStatus == (int)OrderStatus.Completed)
            //        {
            //            Utilities.ShowMessageRedirect("此订单已经完成订餐!");
            //            return;
            //        }
            //        else if (order.OrderStatus == (int)OrderStatus.Canceled)
            //        {
            //            Utilities.ShowMessageRedirect("此订单已经取消!");
            //            return;
            //        }
            //        string message;
            //        lock (OrderMealBLL.LockScopeAction(order.Id))
            //        {
            //            if (CompanyItemBLL.CancelCompanyItem(CurrentUser, CompanyItem, MemberItem, out message, order))
            //            {
            //                Utilities.ShowMessageRedirect("订单取消成功.", string.Format("/Public/OrderMealPage.aspx?CompanyId={0}", MemberItem.CompanyID));
            //                return;
            //            }
            //            else
            //                ValidateMessage = message;
            //        }
            //    }
            //    else
            //        ValidateMessage = "订单不存在.";
            //}
            //else
            //    ValidateMessage = "订单不存在.";
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            string message;
            if (CmpType != CompanyType.MealCompany)
            {
                Utilities.ShowMessageRedirect("优惠项目类型错误!");
                return;
            }
            if (string.IsNullOrEmpty(txtAddr.Value))
            {
                ValidateMessage = "请输入送餐地址.";
                return;
            }
            decimal sum;
            if (CompanyItemBLL.CanClickCompanyMealItem(Company, CurrentUser, CompanyItem,0,out sum, out message))
            {
                //Utilities.ShowMessageRedirect("抢购成功!", "/Member/OrderMealViewPage.aspx?OrderId=" + orderId.ToString( ));
                Utilities.Redirect("/Public/OrderMealPage.aspx?CompanyId={0}&ItemId={1}&MansionId={2}", Company.Id, CompanyItem.ItemID,MansionId);
                return;
            }
            ValidateMessage = message.Replace("{ItemType}", "抢购");

        }
        protected string GetFormatItemContent( )
        {
            //if (string.IsNullOrEmpty(CompanyItem.ItemContent))
            //    return CompanyItem.ItemTitle;
            //return CompanyItem.ItemContent.Replace("\n", "</p><p>").Replace("。", "。</p><p>");

            //仅需0.5分+9元现金，即享红烧带鱼饭套餐（周一）+配茶树菇炖土鸡汤；先到先得，下手要快！
            System.Text.StringBuilder sb = new System.Text.StringBuilder( );
            sb.AppendFormat("仅需<label class='yellow'>{0:0.##}</label>分", CompanyItem.ItemPoint);
            if (CompanyItem.ItemNeedPay.HasValue && CompanyItem.ItemNeedPay > 0)
                sb.AppendFormat("+<label class='yellow'>{0:0.##}</label>元现金", CompanyItem.ItemNeedPay);
            sb.Append("，即享");
            sb.Append(CompanyItem.ItemTitle);
            sb.Append("；先到先得，下手要快！");
            return sb.ToString( );
        }
    }
}