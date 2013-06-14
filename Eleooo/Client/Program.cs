using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Eleooo.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main( )
        {
            bool ret;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out ret);
            if (ret)
            {
                Application.EnableVisualStyles( );
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new FingerTest( ));
                //Application.Run(MainMetroForm.Instance);
                Utilities.CreateDesktopShortCut( );
                AppContext.IsRuning = true;
                ServiceProvider.Init( );
                if (new LoginForm( ).ShowDialog( ) == DialogResult.OK)
                    Application.Run(MainMetroForm.Instance);
                else
                    Application.Exit( );
            }
            else
            {
                MessageBox.Show("已经有相同的应用程序在运行，请不要同时运行多个本程序。\n\n这个程序即将退出。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //   提示信息，可以删除。  
                Application.Exit( );//退出程序  
            }
        }
    }
}
