using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Eleooo.Common;

namespace Eleooo.Web.SiteAppPage
{
    public partial class FileManager : ActionPage
    {
        const string DELETE_LINK = "<a href=\"javascript:__doPostBack('Delete','{0}')\">[删除]</a>";
        string folderName
        {
            get
            {
                return Request.Params["folderName"];
            }
        }
        FileType? _fileType = null;
        FileType fileType
        {
            get
            {
                if (!_fileType.HasValue)
                {
                    string pFileType = Request.Params["fileType"];
                    if (!string.IsNullOrEmpty(pFileType))
                    {
                        _fileType = Formatter.ToEnum<FileType>(pFileType);
                    }
                    else
                        _fileType = FileType.Image;
                }
                return _fileType.Value;
            }
        }
        SaveType? _saveType = null;
        SaveType saveType
        {
            get
            {
                if (!_saveType.HasValue)
                {
                    string pSaveType = Request.Params["saveType"];
                    if (!string.IsNullOrEmpty(pSaveType))
                    {
                        _saveType = Formatter.ToEnum<SaveType>(pSaveType);
                    }
                    else
                        _saveType = SaveType.Custome;
                }
                return _saveType.Value;
            }
        }
        int MaxLimit
        {
            get
            {
                return Utilities.ToInt(Request.Params["maxLimit"]);
            }
        }
        DataTable _data;
        DataTable Data
        {
            get
            {
                if (_data == null)
                    _data = FillDataTable( );
                return _data;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblInfo.InnerHtml = string.Format("允许上传的文件类型为:{0},允许上传的文件大小于为:{1}", Eleooo.Common.FileUpload.GetAllowFileType(fileType), Eleooo.Common.FileUpload.GetMaxAllowSize( ));
            if (!IsDialog)
                MasterPage.LoadDlgSupportCss(true);
        }
        protected override void On_ActionDelete(object sender, EventArgs e)
        {
            string message;
            Eleooo.Common.FileUpload.DeleteFile(saveType, EVENTARGUMENT, out message, folderName);
            lblMessage.InnerHtml = message;
            On_ActionQuery(sender, e);
        }
        protected override void On_ActionQuery(object sender, EventArgs e)
        {
            gridView.DataSource = Data;
            gridView.AddCustomColumn("File", "文件")
                    .AddCustomColumn("FileName", "名称")
                    .AddCustomColumn("FileSize", "大小")
                    .AddCustomColumn("CreatedOn", "日期")
                    .AddCustomColumn("Action", "操作");
            gridView.OnDataBindColumn += new Web.Controls.DataBindColumnHandle(gridView_OnDataBindColumn);
            gridView.DataBind( );
        }

        string gridView_OnDataBindColumn(string column, DataRow rowData, ref bool isRenderedCell)
        {
            string result = string.Empty;
            switch (column)
            {
                case "File":
                    result = string.Format(imgTemplate.InnerHtml, rowData["FileLink"]);
                    break;
                case "Action":
                    result = string.Format(ACTION_DEL_TEMPLATE, rowData["FileName"], "[删除]");
                    break;
                default:
                    result = Utilities.ToString(rowData[column]);
                    break;
            }
            return result;
        }
        protected override void On_ActionAdd(object sender, EventArgs e)
        {
            int maxLimit = MaxLimit;
            if (maxLimit > 0 && Data.Rows.Count >= maxLimit)
            {
                lblMessage.InnerHtml = string.Format("最多允许上传{0}个文件.", maxLimit);
                goto lbl_end;
            }
            string message, phyPath;
            string fileName = Eleooo.Common.FileUpload.SaveUploadFile(uploadify, fileType, saveType, out phyPath, out message, true, folderName);
            if (!string.IsNullOrEmpty(fileName))
            {
                message = "上传成功!";
                _data = null;
            }
            lblMessage.InnerHtml = message;
        lbl_end:
            On_ActionQuery(sender, e);
        }
        string _relPath;
        string RelPath
        {
            get
            {
                if (string.IsNullOrEmpty(_relPath))
                    _relPath = string.Concat(Eleooo.Common.FileUpload.GetSaveRelDir(saveType), folderName, string.IsNullOrEmpty(folderName) ? "" : "/");
                return _relPath;
            }
        }
        private DataTable FillDataTable( )
        {
            DataTable dt = InitDataTable( );
            List<FileInfo> fis = Eleooo.Common.FileUpload.GetFileInfos(saveType, fileType, folderName);
            for (int i = fis.Count - 1; i >= 0; i--)
            {
                FileInfo fi = fis[i];
                DataRow row = dt.NewRow( );
                row["FileName"] = fi.Name;
                row["FileLink"] = string.Concat(RelPath, fi.Name);
                row["CreatedOn"] = fi.LastWriteTime;
                row["FileSize"] = string.Format("{0} KB", Math.Round(Convert.ToDecimal(fi.Length) / (1024), 3));
                dt.Rows.Add(row);
            }
            return dt;
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