using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.IO;
using System.Drawing;

namespace Eleooo.Common
{
    public static class FileUpload
    {
        private const string Bak = ".bak";
        private const string Image = ".bmp.jpg.jpeg.png.gif";
        private const string Zip = ".zip.rar.7z";
        private const string Doc = ".doc.docx.xls.xlsx.ppt.pptx.txt.rtf.csv";
        private const string Media = ".mp3.mov.avi.rm.rmvb.wmv";
        private const string SAVE_DIR = "/Uploads/";
        public const int MAX_ALLOW_SIZE = 2 * 1024 * 1024;
        private const int IMAGE_TITLE_PROP_ID = 0x010E;
        private static readonly System.Text.Encoding _encoding = System.Text.Encoding.UTF8;
        public static string GetMaxAllowSize( )
        {
            return string.Format("{0}M", MAX_ALLOW_SIZE / 1024 / 1024);
        }
        public static string GetAllowFileType(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Bak:
                    return Bak;
                case FileType.Zip:
                    return Zip;
                case FileType.Doc:
                    return Doc;
                case FileType.Image:
                    return Image;
                case FileType.Media:
                    return Media;
                default:
                    return string.Format("{0}{1}{2}{3}{4}{5}", Bak, Image, Zip, Doc, Media);
            }
        }
        public static bool IsAllowFileType(FileType fileType, string filePath)
        {
            string ext = GetFileExt(filePath);
            bool bResult = false;
            if ((fileType & FileType.All) == FileType.All)
                return Bak.Contains(ext) ||
                        Image.Contains(ext) ||
                          Zip.Contains(ext) ||
                            Doc.Contains(ext) || Media.Contains(ext);
            else if ((fileType & FileType.Bak) == FileType.Bak)
                bResult |= Bak.Contains(ext);
            else if ((fileType & FileType.Doc) == FileType.Doc)
                bResult |= Doc.Contains(ext);
            else if ((fileType & FileType.Image) == FileType.Image)
                bResult |= Image.Contains(ext);
            else if ((fileType & FileType.Media) == FileType.Media)
                bResult |= Media.Contains(ext);
            else if ((fileType & FileType.Zip) == FileType.Zip)
                bResult |= Zip.Contains(ext);
            return bResult;
        }
        public static string GetFileExt(string filePath)
        {
            int pos = 0;
            if (string.IsNullOrEmpty(filePath) || (pos = filePath.LastIndexOf('.')) < 0)
                return string.Empty;
            return filePath.Substring(pos);
        }
        private static string FormatFileName(string fileName)
        {
            string newFileName, fileExt;
            fileExt = GetFileExt(fileName);
            Random random = new Random(DateTime.Now.Millisecond);
            newFileName = DateTime.Now.ToString("yyyyMMddhhmmss_") + random.Next(10000) + fileExt;
            return newFileName;
        }
        public static string GetSaveRelDir(SaveType saveType)
        {
            return string.Concat(SAVE_DIR, saveType.ToString( ), "/");
        }
        public static string GetSaveAbsDir(SaveType saveType)
        {
            return HttpContext.Current.Server.MapPath(GetSaveRelDir(saveType));
        }

        public static string SaveUploadFile(HtmlInputFile file, FileType fileType, SaveType saveType, out string phyPath, out string message, bool isAutoName, string folderName = "")
        {
            return SaveUploadFile(file.PostedFile, fileType, saveType, out phyPath, out message, isAutoName, folderName);
        }
        public static string SaveUploadFile(HttpPostedFile file, FileType fileType, SaveType saveType, out string phyPath, out string message, bool isAutoName, string folderName = "")
        {
            return SaveUploadFile(file.InputStream, fileType, saveType, file.FileName, out phyPath, out message, isAutoName, folderName);
        }
        public static string SaveUploadFile(Stream fileStream, FileType fileType, SaveType saveType, string fileName, out string phyPath, out string message, bool isAutoName, string folderName = "")
        {
            byte[] buff = new byte[fileStream.Length];
            fileStream.Position = 0L;
            fileStream.Read(buff, 0, buff.Length);
            return SaveUploadFile(buff, fileType, saveType, fileName, out phyPath, out message, isAutoName, folderName);
        }
        public static string SaveUploadFile(byte[] buff, FileType fileType, SaveType saveType, string fileName, out string phyPath, out string message, bool isAutoName, string folderName = "")
        {
            phyPath = string.Empty;
            try
            {
                message = string.Empty;
                if (!IsAllowFileType(fileType, fileName))
                {
                    message = "文件格式不合法!";
                    return string.Empty;
                }
                if (buff == null || buff.Length == 0)
                {
                    message = "文件内容不能为空!";
                    return string.Empty;
                }
                if (buff.Length > MAX_ALLOW_SIZE)
                {
                    message = string.Format("文件大小超过了限制{0}", GetMaxAllowSize( ));
                    return string.Empty;
                }
                if (isAutoName)
                    fileName = FormatFileName(fileName);
                string absPath = string.Concat(GetSaveAbsDir(saveType), folderName, !string.IsNullOrEmpty(folderName) ? "\\" : "");
                string relPath = string.Concat(GetSaveRelDir(saveType), folderName, !string.IsNullOrEmpty(folderName) ? "/" : "", fileName);
                string absFilePath = string.Concat(absPath, fileName);
                if (!Directory.Exists(absPath))
                    Directory.CreateDirectory(absPath);
                using (FileStream stream = new FileStream(absFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    stream.Write(buff, 0, buff.Length);
                    stream.Flush( );
                }
                phyPath = absFilePath;
                return relPath;
            }
            catch (Exception ex)
            {
                Logging.Log("FileUpload->SaveUploadFile", ex);
                message = ex.Message;
                return string.Empty;
            }
        }
        public static byte[] GetSaveFile(string fileName, SaveType saveType)
        {
            string filePath = HttpContext.Current.Request.MapPath(GetFilePath(fileName, saveType));

            if (!File.Exists(filePath))
                return null;
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                byte[] buff = new byte[stream.Length];
                stream.Position = 0L;
                stream.Read(buff, 0, buff.Length);
                return buff;
            }
        }
        public static bool IsImage(string filePath)
        {
            return IsAllowFileType(FileType.Image, filePath);
        }

        public static string GetFilePath(string photo, SaveType saveType)
        {
            if (string.IsNullOrEmpty(photo) || string.IsNullOrEmpty(photo.Trim( )))
                return string.Empty;
            photo = photo.Trim( );
            if (photo.ToLower( ).StartsWith(SAVE_DIR.ToLower( )))
                return photo;
            return string.Concat(GetSaveRelDir(saveType), photo);
        }
        public static void RenameFolder(SaveType saveType, string oldName, string newName)
        {
            if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(newName))
                return;
            string absPath = GetSaveAbsDir(saveType);
            string newPath = absPath + newName;
            DirectoryInfo diOld = new DirectoryInfo(absPath + oldName);
            DirectoryInfo diNew = new DirectoryInfo(newPath);
            if (diOld.Exists)
            {
                if (diNew.Exists)
                    diNew.Delete(true);
                diOld.MoveTo(newPath);
            }
        }
        public static List<string> GetFileRelPaths(SaveType saveType, FileType fileType, string folderName = "")
        {
            List<string> result = new List<string>( );
            List<FileInfo> fis = GetFileInfos(saveType, fileType, folderName);
            foreach (var fi in fis)
                result.Add(string.IsNullOrEmpty(folderName) ? string.Concat(GetSaveRelDir(saveType), fi) :
                                string.Concat(GetSaveRelDir(saveType), folderName, "/", fi));
            return result;
        }
        public static List<FileInfo> GetFileInfos(SaveType saveType, FileType fileType, string folderName = "")
        {
            string absPath = GetSaveAbsDir(saveType);
            List<FileInfo> fis = new List<FileInfo>( );
            DirectoryInfo di = new DirectoryInfo(absPath + folderName);
            if (di.Exists)
            {
                foreach (FileInfo fi in di.GetFiles( ))
                {
                    if (IsAllowFileType(fileType, fi.FullName))
                        fis.Add(fi);
                }
            }
            return fis;
        }
        public static string GetFileRelPath(SaveType saveType, FileType fileType, string folderName = "", bool isOrderDesc = false)
        {
            List<FileInfo> fis = GetFileInfos(saveType, fileType, folderName);
            if (fis.Count > 0)
            {
                FileInfo fi = isOrderDesc ? fis[fis.Count - 1] : fis[0];
                return string.IsNullOrEmpty(folderName) ? string.Concat(GetSaveRelDir(saveType), fi) :
                                string.Concat(GetSaveRelDir(saveType), folderName, "/", fi);
            }
            else
                return string.Empty;
        }
        public static FileInfo GetFileInfo(SaveType saveType, FileType fileType, string folderName = "")
        {
            List<FileInfo> fis = GetFileInfos(saveType, fileType, folderName);
            return fis.Count > 0 ? fis[fis.Count - 1] : null;
        }
        public static bool DeleteFile(SaveType saveType, string fileName, out string message, string folderName = "")
        {
            string absPath = string.Concat(GetSaveAbsDir(saveType), folderName, string.IsNullOrEmpty(folderName) ? "" : "\\", fileName);
            FileInfo fi = new FileInfo(absPath);
            if (!fi.Exists || string.IsNullOrEmpty(fileName))
            {
                message = "文件不存在!";
                return false;
            }
            try
            {
                fi.Delete( );
                message = "删除成功";
                return true;
            }
            catch (Exception ex)
            {
                Logging.Log("FileUpload->DeleteFile", ex);
                message = ex.Message;
                return false;
            }
        }
        public static string GetImageTitle(string relPath)
        {
            string result = string.Empty;
            try
            {
                var img = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(relPath));
                var prop = img.GetPropertyItem(IMAGE_TITLE_PROP_ID);
                result = _encoding.GetString(prop.Value);
            }
            catch { }
            return result;
        }
    }
}