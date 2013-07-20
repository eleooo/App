using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eleooo.Web;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.BLL.Services
{
    class AddressHandler : IHandlerServices
    {
        #region IHandlerServices 成员

        public Common.ServicesResult Query(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public Common.ServicesResult Add(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public Common.ServicesResult Edit(System.Web.HttpContext context)
        {
            var result = new Common.ServicesResult( ) { code = -1 };
            var floor = context.Request["Floor"].Trim( );
            var room = context.Request["Room"].Trim( );
            var seat = context.Request["Seat"].Trim( );
            if (string.IsNullOrEmpty(floor))
            {
                result.message = "请输入送餐楼层信息";
                goto lbl_return;
            }
            if (string.IsNullOrEmpty(room))
            {
                result.message = "请输入送餐详细地址信息.";
                goto lbl_return;
            }
            var order = Order.FetchByID(Utilities.ToInt(context.Request["OrderID"]));
            if (order == null)
            {
                result.message = "参数错误.";
                goto lbl_return;
            }
            var newAddress = seat + "||" + floor + "|" + room;
            if (Utilities.Compare(newAddress, order.OrderProduct))
            {
                result.message = "新地址与原地址相同";
                goto lbl_return;
            }
            UserBLL.ModifyUserFavAddress(order.OrderMemberID, order.MansionId.Value, order.OrderProduct, newAddress);
            order.OrderProduct = newAddress;
            order.Save( );
            result.message = "修改成功";
            result.code = 0;
        lbl_return:
            return result;
        }

        public Common.ServicesResult Delete(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public Common.ServicesResult Login(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public Common.ServicesResult Logout(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
