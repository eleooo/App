using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using SubSonic;
using System.Data;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysCompanyMansionList : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDesc.InnerHtml = string.Empty;
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            var query = DB.Select(Utilities.GetTableColumn(
                                            SysCompany.CompanyTelColumn,
                                            SysCompany.CompanyNameColumn,
                                            SysArea.AreaNameColumn,
                                            SysAreaMansion.NameColumn,
                                            SysCompanyMansion.IdColumn))
                          .From<SysCompany>( )
                          .InnerJoin(SysCompanyMansion.CompanyIDColumn, SysCompany.IdColumn)
                          .InnerJoin(SysAreaMansion.IdColumn, SysCompanyMansion.MansionIDColumn)
                          .InnerJoin(SysArea.IdColumn, SysAreaMansion.AreaIDColumn)
                          .Where(SysCompany.CompanyTypeColumn).IsEqualTo((int)CompanyType.MealCompany)
                          .OrderDesc(SysCompanyMansion.IdColumn.QualifiedName);
            if (!string.IsNullOrEmpty(txtCompanyName.Value) && !string.IsNullOrEmpty(txtMansionName.Value))
            {
                query.And(SysCompany.CompanyTelColumn).IsEqualTo(txtCompanyName.Value)
                     .And(SysAreaMansion.NameColumn).Like(string.Concat("%", txtMansionName.Value, "%"));
            }
            else if (!string.IsNullOrEmpty(txtCompanyName.Value))
                query.And(SysCompany.CompanyTelColumn).IsEqualTo(txtCompanyName.Value);
            else if (!string.IsNullOrEmpty(txtMansionName.Value))
                query.And(SysAreaMansion.NameColumn).Like(string.Concat("%", txtMansionName.Value, "%"));
            gridView.DataSource = query;
            gridView.AddShowColumn(SysCompany.CompanyNameColumn)
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(SysArea.AreaNameColumn)
                    .AddShowColumn(SysAreaMansion.NameColumn)
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }

        string gridView_OnDataBindColumn(string column, DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                //case "Area_Name":
                //    isRenderedCell = true;
                //    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, rowData["Area_Name"]);
                //    break;
                //case "Address":
                //    isRenderedCell = true;
                //    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, rowData["Address"]);
                //    break;
                //case "Name":
                //    isRenderedCell = true;
                //    result = string.Format(ALIGN_LEFT_CELL_TEMPLATE, rowData["Name"]);
                //    break;
                case "Action":
                    var id = rowData["ID"];
                    //result = string.Concat("[", string.Format(ACTION_DLG_INDEX_TEMPLATE, id, "编辑", 1), "]&nbsp;&nbsp;[",
                    //                       string.Format(ACTION_DEL_TEMPLATE, id, "删除"), "]");
                    result = string.Concat("[", string.Format(ACTION_DEL_TEMPLATE, id, "删除"), "]");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            SysCompanyMansion companyMansion = SysCompanyMansion.FetchByID(Utilities.ToInt(EVENTARGUMENT));
            if (companyMansion == null)
            {
                txtDesc.InnerHtml = string.Format("删除失败! ID {0}不存在!", EVENTARGUMENT);
            }
            else
            {
                SysCompanyMansion.Delete(companyMansion.Id);
                txtDesc.InnerHtml = "删除成功";
            }
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            if (txtImportFile.PostedFile == null || txtImportFile.PostedFile.ContentLength == 0)
            {
                txtDesc.InnerHtml = "上传的文件不合法.";
                goto lbl_return;
            }
            try
            {
                string message, absPath;
                var result = Eleooo.Common.FileUpload.SaveUploadFile(txtImportFile.PostedFile, FileType.Doc, SaveType.Custome, out message, true);
                if (!string.IsNullOrEmpty(message))
                    throw new Exception(message);
                DataTable dt = ExcelHelper.ExportExcelInDT(result.PhyPath, out message);
                if (!string.IsNullOrEmpty(message))
                    throw new Exception(message);
                if (dt == null)
                    throw new Exception("读入文件错误!");
                if (dt.Rows.Count > 0)
                    this.txtDesc.InnerHtml = string.Format("成功读取到{0}条大厦信息", dt.Rows.Count);
                else
                    throw new Exception(string.Format("你上传的文件不完整，{0}未提供数据", txtImportFile.PostedFile.FileName));

                if (!MansionBLL.ImportCompanyMansions(dt, out message))
                    txtDesc.InnerHtml = message;

            }
            catch (Exception ex)
            {
                txtDesc.InnerHtml = ex.Message;
                Logging.Log("SysCompanyMansionList->On_ActionAdd", ex);
            }
        lbl_return:
            On_ActionQuery(sender, e);
        }
    }
}