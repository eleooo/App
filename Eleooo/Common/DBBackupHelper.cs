using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using SubSonic;
using System.Data;

namespace Eleooo.Common
{
    public class DBBackupHelper
    {
        const string SQL_BACKUP = "backup database [{0}] to disk='{1}'";
        const string SQL_GETDATABASE = "select db_name(dbid) from master.dbo.sysprocesses where spid =  @@spid";
        const string SQL_GETDATABASE2005 = "Select Name From Master..SysDataBases Where DbId=(Select Dbid From Master..SysProcesses Where Spid = @@spid) ";
        const string SQL_GETPROCESS = "SELECT spid FROM sys.sysprocesses ,sys.sysdatabases WHERE sys.sysprocesses.dbid=sys.sysdatabases.dbid AND sys.sysprocesses.spid <>@@spid AND sys.sysdatabases.Name='{0}';";
        const string SQL_KILLPROCESS = "KILL {0}";
        const string SQL_RESTOREDATABASE = "use master; RESTORE DATABASE [{0}] from DISK = '{1}'; ";
        public const string PATH_DATABASE_BACKUP = "/Uploads/Backup/";

        // Methods
        public static bool BackupDataBase(string databasename, string strPath,out string message)
        {
            message = string.Empty;
            try
            {
                QueryCommand cmd = new QueryCommand(string.Format(SQL_BACKUP, databasename, strPath),DAL.DB.Provider.Name);
                DataService.ExecuteQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Logging.Log("DBBackupHelper->BackupDataBase", ex);
                message = ex.Message;
                return false;
            }
        }

        public static string GetDataBase( out string message )
        {
            try
            {
                message = string.Empty;
                QueryCommand cmd = new QueryCommand(SQL_GETDATABASE,DataService.Provider.Name);
                return Utilities.ToHTML(DataService.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                Logging.Log("DBBackupHelper->GetDataBase", ex);
                message = ex.Message;
                return string.Empty;
            }
        }

        public static string GetDataBase2005(out string message)
        {
            try
            {
                message = string.Empty;
                QueryCommand cmd = new QueryCommand(SQL_GETDATABASE2005, DataService.Provider.Name);
                return Utilities.ToHTML(DataService.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                Logging.Log("DBBackupHelper->GetDataBase2005", ex);
                message = ex.Message;
                return string.Empty;
            }
        }

        public static bool RestoreDataBase(string databasename, string databasefile,out string message)
        {
            try
            {
                message = string.Empty;
                QueryCommand cmdProcess = new QueryCommand(string.Format(SQL_GETPROCESS, databasename), DataService.Provider.Name);
                IDataReader reader = DataService.GetReader(cmdProcess);
                try
                {
                    while (reader.Read( ))
                    {
                        QueryCommand cmdKill = new QueryCommand(string.Format(SQL_KILLPROCESS, reader[0]), DataService.Provider.Name);
                        DataService.ExecuteQuery(cmdKill);
                    }
                }
                finally
                {
                    if (!reader.IsClosed)
                        reader.Close( );
                }
                QueryCommand cmdRestore = new QueryCommand(string.Format(SQL_RESTOREDATABASE,databasename,databasefile),DataService.Provider.Name);
                DataService.ExecuteQuery(cmdRestore);
                return true;
            }
            catch (Exception ex)
            {
                Logging.Log("DBBackupHelper->RestoreDataBase", ex);
                message = ex.Message;
                return false;
            }
        }

        public static bool BackupDB(out string fileName,out string message)
        {
            string bakFile = string.Empty;
            fileName = string.Empty;
            try
            {
                string dbName = GetDataBase(out message);
                string path = PathBackup;
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                    di.Create( );
                int num = ((DateTime.Now.Year * 0x2710) + (DateTime.Now.Month * 100)) + DateTime.Now.Day;
                int num2 = ((DateTime.Now.Hour * 0x2710) + (DateTime.Now.Minute * 100)) + DateTime.Now.Second;
                bakFile = string.Format("{0}{1}_{2}.bak", di.FullName, num, num2);
                bool bResult = BackupDataBase(dbName, bakFile, out message);
                if (bResult == false)
                    return false;
                fileName = bakFile.Replace(".bak", ".zip");
                ZipHelper.ZipFile(bakFile, fileName);
                fileName = fileName.Replace(path,string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                Logging.Log("DBBackupHelper->BackupDB", ex);
                message = ex.Message;
                return false;
            }
            finally
            {
                if (File.Exists(bakFile))
                    File.Delete(bakFile);
            }
        }
        public static bool RestoreDB(string zipFile, out string message)
        {
            string bakFile = string.Empty;
            try
            {
                message = string.Empty;
                string strPath = string.Concat(PathBackup, zipFile);
                if (!File.Exists(strPath))
                    throw new ArgumentException(string.Format("{0}文件不存在!", zipFile));
                ZipHelper.UnZip(strPath, PathBackup);
                bakFile = strPath.Replace(".zip", ".bak");
                if (!File.Exists(bakFile))
                    throw new ArgumentException(string.Format("{0}文件不存在!", bakFile));
                string dbName = GetDataBase(out  message);
                return RestoreDataBase(dbName, bakFile, out message);
            }
            catch (Exception ex)
            {
                Logging.Log("DBBackupHelper->RestoreDB", ex);
                message = ex.Message;
                return false;
            }
            finally
            {
                if (File.Exists(bakFile))
                    File.Delete(bakFile);
            }
            
        }
        public static bool DeleteBakFile(string zipFile, out string message)
        {
            try
            {
                message = string.Empty;
                string strPath = string.Concat(PathBackup, zipFile);
                if (!File.Exists(strPath))
                    throw new ArgumentException(string.Format("{0}文件不存在!", zipFile));
                File.Delete(strPath);
                return true;
            }
            catch (Exception ex)
            {
                Logging.Log("DBBackupHelper->DeleteBakFile", ex);
                message = ex.Message;
                return false;
            }
        }
        public static FileInfo[] GetBakFileList( )
        {
            DirectoryInfo di = new DirectoryInfo(PathBackup);
            if (!di.Exists)
                di.Create( );
            return di.GetFiles( );
        }

        public static string PathBackup
        {
            get
            {
                return HttpContext.Current.Server.MapPath( PATH_DATABASE_BACKUP);
            }
        }
    }


}