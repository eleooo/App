using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eleooo.Web.Admin
{
    public partial class SysOrderMeal : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DateTime dt = DateTime.Today;//.AddDays((double)(1 - DateTime.Today.Day));
                this.txtDateStart.Value = dt.ToString("yyyy-MM-dd");
                this.txtDateEnd.Value = this.txtDateStart.Value;// dt.AddMonths(2).AddDays(-1).ToString("yyyy-MM-dd");
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            int nStatus = Convert.ToInt32(rbStatus.SelectedValue);
            int nModel = Convert.ToInt32(rbModel.SelectedValue);
            App.SetOrderMealQueryView(gridView, txtCompanyName.Value, txtMemberPhone.Value, nStatus, nModel, txtDateStart.Value, txtDateEnd.Value,cbViewAll.Checked);
        }
    }
}