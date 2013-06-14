using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class MealDirectoryEdit : ActionPage
    {
        int DirId
        {
            get
            {
                return Utilities.ToInt(Request.Params["ID"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && DirId > 0)
            {
                //SysTakeawayDirectory dir = SysTakeawayDirectory.FetchByID(DirId);
                var query = DB.Select(SysTakeawayDirectory.DirNameColumn.QualifiedName,
                                      SysCompany.CompanyTelColumn.QualifiedName)
                              .From<SysTakeawayDirectory>( )
                              .InnerJoin(SysCompany.IdColumn, SysTakeawayDirectory.CompanyIDColumn)
                              .Where(SysTakeawayDirectory.IdColumn).IsEqualTo(DirId);
                using (var dr = query.ExecuteReader( ))
                {
                    while (dr.Read( ))
                    {
                        txtDirName.Value = dr.GetString(0);
                        txtCompanyTel.Value = dr.GetString(1);
                        txtCompanyTel.Disabled = true;
                    }
                }
            }
            lblMessage.InnerHtml = string.Empty;
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDirName.Value.Trim( )))
            {
                lblMessage.InnerHtml = "请输入分类名称";
                return;
            }
            SysTakeawayDirectory dir = SysTakeawayDirectory.FetchByID(DirId);
            if (dir == null)
            {
                if (string.IsNullOrEmpty(txtCompanyTel.Value))
                {
                    lblMessage.InnerHtml = "请输入商家账号.";
                    return;
                }
                var company = CompanyBLL.GetCompanyByTel(txtCompanyTel.Value);
                if (company == null)
                {
                    lblMessage.InnerHtml = "你输入的商家账号不存在.";
                    return;
                }
                dir = new SysTakeawayDirectory( );
                dir.CompanyID = company.Id;
            }
            dir.DirName = txtDirName.Value.Trim( );
            dir.Save( );
            lblMessage.InnerHtml = "保存成功";
            if (DirId <= 0)
                txtDirName.Value = string.Empty;
        }
    }
}