using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysCompanyMealMenuEdit : ActionPage
    {
        int MenuId
        {
            get
            {
                return Utilities.ToInt(Request.Params["ID"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            if (MenuId > 0)
            {
                SysTakeawayMenu menu = SysTakeawayMenu.FetchByID(MenuId);
                if (menu != null)
                {
                    var company = SysCompany.FetchByID(menu.CompanyID);
                    txtCompanyTel.Value = company.CompanyTel;
                    txtCompanyTel.Disabled = true;
                    ddlDirs.DataSource = MealMenuBLL.LoadMenuDirectory(company.Id);
                    ddlDirs.DataBind( );
                    ddlDirs.SelectedValue = menu.DirID.ToString( );
                    txtMenuName.Text = menu.Name;
                    txtPrice.Text = Utilities.ToString(menu.Price);
                    rbOutOfStock.SelectedValue = menu.IsOutOfStock.HasValue && menu.IsOutOfStock.Value ? "1" : "0";
                }
            }
            else if (!string.IsNullOrEmpty(txtCompanyTel.Value) && _company != null)
            {
                ddlDirs.DataSource = MealMenuBLL.LoadMenuDirectory(_company.Id);
                ddlDirs.DataBind( );
            }
        }
        SysCompany _company;
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMenuName.Text.Trim()))
            {
                lblMessage.InnerHtml = "请输入菜单名称.";
                return;
            }

            int dirID = Utilities.ToInt(Request.Params[ddlDirs.UniqueID]);
            if (dirID <= 0)
            {
                lblMessage.InnerHtml = "请选择菜单的菜品系列.";
                return;
            }
            SysTakeawayDirectory dir = SysTakeawayDirectory.FetchByID(dirID);
            if (dir == null)
            {
                lblMessage.InnerHtml = "请选择菜单的菜品系列.";
                return;
            }
            _company = SysCompany.FetchByID(dir.CompanyID);
            SysTakeawayMenu menu = SysTakeawayMenu.FetchByID(MenuId);
            var price = Utilities.ToDecimal(txtPrice.Text);
            if (menu == null)
            {
                menu = new SysTakeawayMenu( );
                menu.Code = null;
            }
            else if (menu.Price.HasValue && price != menu.Price.Value)
            {
                CompanyItemBLL.UpdateCompanyItemSum(MenuId, price, menu.Price.Value);
            }
            menu.Price = price;
            menu.Name = txtMenuName.Text.Trim( );
            menu.DirID = dir.Id;
            menu.CompanyID = dir.CompanyID;
            menu.IsDeleted = false;
            
            menu.IsOutOfStock = rbOutOfStock.SelectedValue == "1";
            if (menu.IsOutOfStock.Value)
                menu.OutOfStockDate = DateTime.Now;
            else
                menu.OutOfStockDate = null;
            menu.Save( );
            if (string.IsNullOrEmpty(menu.Code))
            {
                menu.Code = menu.Id.ToString();
                menu.Save();
            }
            _company.MenuDate = DateTime.Now;
            _company.Save( );
            lblMessage.InnerHtml = "保存成功";
            if (MenuId == 0)
            {
                On_ActionQuery(sender, e);
                ddlDirs.ClearSelection( );
                txtCompanyTel.Value = string.Empty;
                txtPrice.Text = string.Empty;
                txtMenuName.Text = string.Empty;
                rbOutOfStock.SelectedValue = "0";
            }
            else
                On_ActionQuery(sender, e);
        }
    }
}