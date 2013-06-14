using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Controls
{
    public partial class UcOrderMealSelectMansion : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindContent( );
            Utilities.RemoveCookie("addr");
        }
        private void BindContent( )
        {
            if (AppContext.Context.CurrentSubSys != SubSystem.ALL)
            {
                BindLoginContent( );
                noLoginContainer.Visible = false;
            }
            else
            {
                myFavAddrContainer.Visible = false;
            }
        }
        private static IEnumerable<object> GetUserFavAddress( )
        {
            var favAddr = UserBLL.GetUserFavAddress(AppContext.Context.User.Id);
            SysAreaMansion mansion;
            foreach (var addr in favAddr.Take(3))
            {
                mansion = SysAreaMansion.FetchByID(addr.id);
                yield return new
                {
                    id = addr.id,
                    name = addr.name,
                    mansion = mansion != null ? mansion.Name : string.Empty
                };
            }
        }
        private void BindLoginContent( )
        {
            //var favCmps = UserBLL.GetUserFavCompany(AppContext.Context.User.Id);
            rpMyFavAddr.DataSource = GetUserFavAddress( );
            rpMyFavAddr.DataBind( );
        }
        public override void On_ActionDelete( )
        {
            DeleteFavCompany( );
        }
        public void DeleteFavCompany( )
        {
            string postVal = BasePage.EVENTARGUMENT;
            if (!string.IsNullOrEmpty(postVal) && AppContext.Context.CurrentSubSys != SubSystem.ALL)
            {
                var vals = postVal.Split(new char[] { ';', '#' }, StringSplitOptions.RemoveEmptyEntries);
                int mansionId = 0;
                string address = null;
                if (vals.Length > 0)
                    mansionId = Utilities.ToInt(vals[0]);
                if (vals.Length > 1)
                    address = vals[1];
                UserBLL.RemoveUserFavAddress(AppContext.Context.User.Id, mansionId, address);
            }
            BindContent( );
        }
    }
}