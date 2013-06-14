using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using Eleooo.Common;

namespace Eleooo.Web.Member
{
    public partial class MyFacebookEdit : ActionPage
    {
        const string DISABLE_INPUT_TEXT = "<input name='{0}' type='text' value='{1}' id='{0}' disabled='disabled' />";
        const string TEXTAREA_INPUT = "<textarea name=\"{0}\" id=\"{0}\" style=\"width: 400px; height: 100px;\" rows=\"2\" cols=\"20\">{1}</textarea>";

        int FaceBookID
        {
            get
            {
                return Utilities.ToInt(Request.Params["ID"]);
            }
        }
        int CompanyID
        {
            get
            {
                return Utilities.ToInt(Request.Params["CompanyID"]);
            }
        }
        string CompanyName
        {
            get
            {
                return Request.Params["CompanyName"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            formView.DataSource = DB.Select(Utilities.GetTableColumns(SysCompanyFaceBook.Schema), SysCompany.Columns.CompanyName)
                                    .From<SysCompanyFaceBook>( )
                                    .InnerJoin(SysCompany.IdColumn, SysCompanyFaceBook.FaceBookBizIDColumn)
                                    .Where(SysCompanyFaceBook.IdColumn).IsEqualTo(FaceBookID)
                                    .And(SysCompanyFaceBook.FaceBookBizTypeColumn).IsEqualTo((int)FaceBookType.Company);
            formView.AddShowColumn(SysCompany.CompanyNameColumn)
                    .AddShowColumn(SysCompanyFaceBook.FaceBookMemoColumn);
            formView.OnDataBindRow += new Web.Controls.OnFormViewDataBindRow(formView_OnDataBindRow);
            formView.OnBeforeSaved += new Web.Controls.OnFormViewSaveHandle(formView_OnBeforeSaved);
            txtMessage.InnerHtml = string.Empty;
        }

        void formView_OnBeforeSaved(object item)
        {
            SysCompanyFaceBook faceBook = item as SysCompanyFaceBook;
            faceBook.FaceBookMemberID = CurrentUser.Id;
            if (CompanyID > 0)
            {
                faceBook.FaceBookDate = DateTime.Now;
                faceBook.FaceBookBizID = CompanyID;
                faceBook.FaceBookBizType = (int)FaceBookType.Company;
            }
        }

        void formView_OnDataBindRow(string columnName, Controls.UcFormView.FormViewRow viewRow)
        {
            switch (columnName)
            {
                case "CompanyName":
                    viewRow.RenderHtml = string.Format(DISABLE_INPUT_TEXT, viewRow.ParamName, string.IsNullOrEmpty(viewRow.Value) ? CompanyName : viewRow.Value);
                    break;
                case "FaceBookMemo":
                    viewRow.RenderHtml = string.Format(TEXTAREA_INPUT, viewRow.ParamName, viewRow.Value);
                    break;
            }
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            formView.DataBind( );
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (formView.Save<SysCompanyFaceBook>(0) == 0)
                txtMessage.InnerHtml = "保存成功";
            else
                txtMessage.InnerHtml = "保存失败";
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            if (formView.Save<SysCompanyFaceBook>(FaceBookID) == 0)
                txtMessage.InnerHtml = "保存成功";
            else
                txtMessage.InnerHtml = "保存失败";
            On_ActionQuery(sender, e);
        }
    }
}