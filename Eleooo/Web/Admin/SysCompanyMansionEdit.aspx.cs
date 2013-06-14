using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysCompanyMansionEdit : ActionPage
    {
        int? _companyMansionId;
        int CompanyMansionId
        {
            get
            {
                if (_companyMansionId == null)
                    _companyMansionId = Utilities.ToInt(Request.Params["ID"]);
                return _companyMansionId.Value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            //if (CompanyMansionId > 0)
            //{
            //    ddlMansions.DataSource = MansionBLL.LoadAreaMansions( );
            //    ddlMansions.DataBind( );
            //    SysCompanyMansion companyMansion = SysCompanyMansion.FetchByID(CompanyMansionId);
            //    if (companyMansion != null)
            //    {
            //        ddlMansions.SelectedValue = companyMansion.MansionID.ToString( );
            //        SysCompany cmp = company == null ? SysCompany.FetchByID(companyMansion.CompanyID) : company;
            //        txtCompanyTel.Value = cmp.CompanyTel;
            //        txtCompanyTel.Disabled = true;
            //    }
            //}
        }
        SysCompany company;
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            company = CompanyBLL.GetCompanyByTel(txtCompanyTel.Value.Trim( ));
            SysAreaMansion mansion = MansionBLL.GetAreaMansionByName(txtMansion.Value);
            if (company == null)
                lblMessage.InnerHtml = "请输入商家正确的商家账号.";
            else if (mansion == null)
                lblMessage.InnerHtml = "你输入的大厦不存在.";
            else if (MansionBLL.CheckCompanyMansionExist(company.Id, mansion.Id, CompanyMansionId))
                lblMessage.InnerHtml = "此大厦已经属于商家送外卖范围.";
            else
            {
                SysCompanyMansion companyMansion = SysCompanyMansion.FetchByID(CompanyMansionId);
                if (companyMansion == null)
                    companyMansion = new SysCompanyMansion( );
                companyMansion.MansionID = mansion.Id;
                companyMansion.CompanyID = company.Id;
                companyMansion.Save( );
                lblMessage.InnerHtml = "保存成功.";
                if (CompanyMansionId == 0)
                {
                    On_ActionQuery(sender, e);
                    txtCompanyTel.Value = "";
                    return;
                }
            }
            On_ActionQuery(sender, e);
        }
    }
}