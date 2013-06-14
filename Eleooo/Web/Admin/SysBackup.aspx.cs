using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Eleooo.Common;

namespace Eleooo.Web.Admin
{
    public partial class SysBackup : ActionPage
    {
        const string DOWN_LINK = " <a href=\"{0}\" target=\"_blank\">{1}</a> ";
        const string RESTORE_LINK = "<a href=\"javascript:__doPostBack('Edit','{0}')\" >[还原]</a> ";
        const string DELETE_LINK = "<a href=\"javascript:__doPostBack('Delete','{0}')\">[删除]</a>";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMessage.InnerHtml = string.Empty;
        }
        protected override void On_ActionEdit(object sender, EventArgs e)
        {
            string message;
            if (!DBBackupHelper.RestoreDB(EVENTARGUMENT, out message))
                txtMessage.InnerHtml = message;
            else
                txtMessage.InnerHtml = string.Format("文件{0}已经成功恢复到数据库.", EVENTARGUMENT);
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            string message, fileName;
            if (!DBBackupHelper.BackupDB(out fileName, out message))
                txtMessage.InnerHtml = message;
            else
                txtMessage.InnerHtml = string.Format("备份成功,备份文件为 {0}", fileName);
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            string message;
            if (!DBBackupHelper.DeleteBakFile(EVENTARGUMENT, out message))
                txtMessage.InnerHtml = message;
            else
                txtMessage.InnerHtml = string.Format("成功删除备份文件{0}", EVENTARGUMENT);

            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            FileInfo[] files = DBBackupHelper.GetBakFileList( );
            DataTable dt = InitDataTable( );
            for (int i = files.Length - 1; i >= 0; i--)
            {
                FileInfo fi = files[i];
                DataRow row = dt.NewRow( );
                row["FileName"] = fi.Name;
                row["FileLink"] = string.Concat(DBBackupHelper.PATH_DATABASE_BACKUP, fi.Name);
                row["CreatedOn"] = fi.LastWriteTime;
                row["FileSize"] = string.Format("{0} MB", Math.Round(Convert.ToDecimal(fi.Length) / (1024 * 1024), 3));
                dt.Rows.Add(row);
            }
            gridView.DataSource = dt;
            gridView.AddCustomColumn("FileName", "文件名称");
            gridView.AddCustomColumn("FileSize", "大小");
            gridView.AddCustomColumn("CreatedOn", "备份日期");
            gridView.AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
        }

        string gridView_OnDataBindColumn(string column, DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            
            switch (column)
            {
                case "Action":
                    result = string.Concat(
                        string.Format(DOWN_LINK, rowData["FileLink"], "[下载]"), "|",
                        string.Format(RESTORE_LINK, rowData["FileName"]), "|",
                        string.Format(DELETE_LINK, rowData["FileName"]));
                    break;
                case "FileName":
                    result = string.Format(DOWN_LINK, rowData["FileLink"], rowData["FileName"]);
                    break;
                default:
                    result = Utilities.ToHTML(rowData[column]);
                    break;
            }
            return result;
        }
        private DataTable InitDataTable( )
        {
            DataTable dt = new DataTable( );
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("FileLink", typeof(string));
            dt.Columns.Add("FileSize", typeof(string));
            dt.Columns.Add("CreatedOn", typeof(DateTime));
            return dt;
        }

    }
}