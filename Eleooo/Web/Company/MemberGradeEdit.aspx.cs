using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Company
{
    public partial class MemberGradeEdit : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            formView.DataSource = DB.Select( ).From<SysCompanyMemberGrade>( )
                                    .Where(SysCompanyMemberGrade.IdColumn).IsEqualTo(Utilities.ToInt(Request.Params["id"]))
                                    .And(SysCompanyMemberGrade.CompanyIDColumn).IsEqualTo(CurrentUser.CompanyId);
            formView.AddShowColumn(SysCompanyMemberGrade.GradeNameColumn)
                    .AddShowColumn(SysCompanyMemberGrade.GradeOrderColumn);
            formView.OnBeforeSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnBeforeSaved);
        }

        void formView_OnBeforeSaved(object item)
        {
            SysCompanyMemberGrade grade = (SysCompanyMemberGrade)item;
            if (grade.IsNew)
            {
                grade.CompanyID = CurrentUser.CompanyId;
            }
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (formView.Save<SysCompanyMemberGrade>(Utilities.ToInt(Request.Params["id"])) == 0)
                txtMessage.InnerHtml = "保存成功";
            else
                txtMessage.InnerHtml = "保存失败";
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            On_ActionAdd(sender, e);
        }
    }
}