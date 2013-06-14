using System;
using System.Collections.Generic;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Eleooo.Common
{
    public class ExcelHelper
    {

        public static DataTable ReadExcelSheets(string PathExcel, out string message)
        {
            message = string.Empty;
            try
            {
                using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + PathExcel))
                {
                    connection.Open( );
                    object[] restrictions = new object[4];
                    restrictions[3] = "Table";
                    DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
                    connection.Close( );
                    return oleDbSchemaTable;
                }
            }
            catch (Exception ex)
            {
                Logging.Log("ExcelHelper->ExportCsvIn", ex);
                message = ex.Message;
                return null;
            }
        }
        public static DataSet ExportCsvIn(string PathCsv, out string message)
        {
            message = string.Empty;
            try
            {
                string str = PathCsv.Substring(0, PathCsv.LastIndexOf(@"\"));
                string str2 = PathCsv.Substring(PathCsv.LastIndexOf(@"\") + 1);
                DataSet dataSet = new DataSet( );
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + str + ";Extended Properties='TEXT;HDR=Yes;FMT=Delimited;'";
                new OleDbConnection(connectionString).Open( );
                new OleDbDataAdapter("SELECT * FROM " + str2, connectionString).Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                Logging.Log("ExcelHelper->ExportCsvIn", ex);
                message = ex.Message;
                return null;
            }
        }

        public static DataTable ExportExcelInDT(string PathExcel, out string message)
        {
            DataSet ds = ExportExcelIn(PathExcel, out message);
            if (ds != null)
                return ds.Tables[0];
            else
                return null;
        }

        public static DataSet ExportExcelIn(string PathExcel,out string message)
        {
            return ExportExcelIn(PathExcel, "Sheet1$",out message);
        }

        public static DataSet ExportExcelIn(string PathExcel, string SheetName,out string message)
        {
            message = string.Empty;
            try
            {
                string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False;Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"", PathExcel);
                //new OleDbConnection(connectionString).Open( );
                using (OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [" + SheetName + "]", connectionString))
                {
                    DataSet dataSet = new DataSet( );
                    adapter.Fill(dataSet, "table1");
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                Logging.Log("ExcelHelper->ExportExcelIn", ex);
                message = ex.Message;
                return null;
            }
        }
    }
}