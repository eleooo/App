using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;
using System.Linq;

namespace Eleooo.Web.Company
{
    public partial class SaleAdd : ActionPage
    {
        private ISettlement _settlement;
        protected override void OnInit(EventArgs e)
        {
            CompanyType t = CompanyBLL.GetCompanyType(AppContext.Context.Company.CompanyType);
            if (t == CompanyType.SpecialCompany || t == CompanyType.AdCompany)
            {
                rblSaleType.Items.RemoveAt(0);
                rblSaleType.Items.RemoveAt(1);
                rblSaleType.Items[0].Selected = true;
            }
            string val = IsPostBack ? Request[rblSaleType.UniqueID] : rblSaleType.Items.OfType<ListItem>( ).FirstOrDefault(item => item.Selected).Value;
            if (val == "3")
                _settlement = LoadControl("/Controls/UcNoneMemberSettlement.ascx") as ISettlement;
            else if (val == "2")
            {
                if (t == CompanyType.MealCompany || t == CompanyType.AdCompany)
                {
                    SaleContainer.InnerHtml = "阁下的商家类型无权使用此功能";
                }
                else
                    _settlement = LoadControl("/Controls/UcCompanyItemSettlementCommunity.ascx") as ISettlement;
            }
            else
                _settlement = LoadControl("/Controls/UcMemberSettlementCommunity.ascx") as ISettlement;
            if (_settlement != null)
                this.SaleContainer.Controls.Add(_settlement as Control);
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMessage.InnerHtml = string.Empty;
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            if (_settlement != null)
                _settlement.OnPageLoad(sender, e);
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            string message;
            if (!_settlement.Save(out message))
                txtMessage.InnerHtml = message;
            else
            {
                txtMessage.InnerHtml = "消费成功!";
            }
            On_ActionQuery(sender, e);
        }
    }
}