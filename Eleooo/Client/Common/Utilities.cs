using System;
using System.Collections.Generic;
using System.Text;
using SubSonic;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using SubSonic.Utilities;
using System.IO;

namespace Eleooo.Client
{
    public class Utilities
    {
        public static T ChangeType<T>(object val)
        {
            if (val == DBNull.Value || val == null)
                return default(T);
            else
                return (T)val;
        }
        public static Control GetFoucs( )
        {
            Control focusedControl = null;

            // To get hold of the focused control:
            IntPtr focusedHandle = Eleooo.Client.WinApi.GetFocus( );
            if (focusedHandle != IntPtr.Zero)
                //focusedControl = Control.FromHandle(focusedHandle);
                focusedControl = Control.FromChildHandle(focusedHandle);

            return focusedControl;
        }
        public static string StripHtml(string src)
        {
            return SubSonic.Sugar.Strings.StripHTML(src);
        }
        public static bool CheckEmail(string src, bool allowNull)
        {
            if (allowNull && string.IsNullOrEmpty(src))
                return true;
            else
                return SubSonic.Sugar.Validation.IsEmail(src);
        }
        public static bool CheckEmail(string src)
        {
            return CheckEmail(src, true);
        }
        public static string DESEncrypt(string source)
        {
            return SubSonic.Utilities.Encrypt.DESEncrypt(source);
            //return ServiceProvider.Service.Md5(source);
        }

        public static bool Compare(string strA, string strB)
        {
            return Compare(strA, strB, true);
        }
        public static bool Compare(string strA, string strB, bool ignoreCase)
        {
            return string.Compare(strA, strB, ignoreCase) == 0;
        }
        public static bool IsNumeric(string src)
        {
            return SubSonic.Sugar.Validation.IsNumeric(src);
        }

        public static void CreateDesktopShortCut( )
        {
            try
            {
                string pathDsk = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                DirectoryInfo diDsk = new DirectoryInfo(pathDsk);
                var fiDsk = diDsk.GetFiles("*乐多分*.appref-ms");
                if (fiDsk.Length > 0)
                    return;
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "乐多分");
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                    return;
                foreach (var fi in di.GetFiles("乐多分-客户端程序.appref-ms"))
                {
                    if (fiDsk.Length == 0)
                    {
                        fi.CopyTo(Path.Combine(pathDsk, fi.Name), true);
                        Console.WriteLine("Copyed");
                    }
                    Console.WriteLine(fi.FullName);
                }
            }
            catch { }
        }
    }
}
