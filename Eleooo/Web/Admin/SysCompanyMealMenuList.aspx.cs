using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eleooo.DAL;
using System.Data;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysCompanyMealMenuList : ActionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDesc.InnerHtml = string.Empty;
        }

        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            var query = DB.Select(Utilities.GetTableColumn(SysCompany.CompanyTelColumn, SysCompany.CompanyNameColumn,
                                  SysTakeawayMenu.CompanyIDColumn,
                                  SysTakeawayMenu.DirIDColumn,
                                  SysTakeawayMenu.IdColumn,
                                  SysTakeawayMenu.NameColumn,
                                  SysTakeawayMenu.PriceColumn,
                                  SysTakeawayMenu.IsOutOfStockColumn,
                                  SysTakeawayDirectory.DirNameColumn,
                                  SysTakeawayMenu.CodeColumn))
                          .From<SysTakeawayMenu>( )
                          .InnerJoin(SysCompany.IdColumn, SysTakeawayMenu.CompanyIDColumn)
                          .InnerJoin(SysTakeawayDirectory.IdColumn, SysTakeawayMenu.DirIDColumn)
                          .Where(SysTakeawayMenu.NameColumn).Like(Utilities.GetAllLikeQuery(txtMenuName.Value))
                          .And(SysTakeawayMenu.IsDeletedColumn).IsEqualTo(false)
                          .OrderDesc(SysTakeawayMenu.IdColumn.QualifiedName);
            if (!string.IsNullOrEmpty(txtCompanyName.Value))
            {
                if (!Formatter.IsChinese(txtCompanyName.Value))
                    query.And(SysCompany.CompanyTelColumn).Like(Utilities.GetAllLikeQuery(txtCompanyName.Value));
                else
                    query.And(SysCompany.CompanyNameColumn).Like(Utilities.GetAllLikeQuery(txtCompanyName.Value));
            }
            gridView.DataSource = query;
            gridView.AddShowColumn(SysCompany.CompanyNameColumn)
                    .AddShowColumn(SysCompany.CompanyTelColumn)
                    .AddShowColumn(SysTakeawayDirectory.DirNameColumn)
                    .AddShowColumn(SysTakeawayMenu.NameColumn)
                    .AddShowColumn(SysTakeawayMenu.CodeColumn)
                    .AddShowColumn(SysTakeawayMenu.PriceColumn)
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }

        string gridView_OnDataBindColumn(string column, DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "IsOutOfStock":
                    var isOutOfStock = Utilities.ToBool(rowData[column]);
                    if (isOutOfStock)
                        result = "是";
                    else
                        result = "否";
                    break;
                case "DirName":
                case "Name":
                    result = Formatter.SubStr(rowData[column], 10);
                    break;
                case "Action":
                    var id = rowData["ID"];
                    result = string.Concat("[", string.Format(ACTION_DLG_INDEX_TEMPLATE, id, "编辑", 1), "]&nbsp;&nbsp;[",
                                           string.Format(ACTION_DEL_TEMPLATE, id, "删除"), "]");
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }

        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            SysTakeawayMenu menu = SysTakeawayMenu.FetchByID(Utilities.ToInt(EVENTARGUMENT));
            if (menu == null)
            {
                txtDesc.InnerHtml = string.Format("删除失败! ID {0}不存在!", EVENTARGUMENT);
            }
            else
            {
                menu.IsDeleted = true;
                menu.Save( );
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
                if (dt.Rows.Count == 0)
                    throw new Exception(string.Format("你上传的文件不完整，{0}未提供数据", txtImportFile.PostedFile.FileName));

                MealMenuBLL.ImportCompanyMealMenu(dt, out message);
                txtDesc.InnerHtml = message;
            }
            catch (Exception ex)
            {
                txtDesc.InnerHtml = ex.Message;
                Logging.Log("SysCompanyMealMenuList->On_ActionAdd", ex);
            }
        lbl_return:
            On_ActionQuery(sender, e);
        }
    }
}