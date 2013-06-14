using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Eleooo.Client
{
    public partial class FlushForm : DevComponents.DotNetBar.Metro.MetroForm
    {
        const string DEFAULT_TEXT = "正在加载,请稍候...";
        private static BackgroundWorker _worker;
        private static BackgroundWorker Worker
        {
            get
            {
                if (_worker == null)
                {
                    _worker = new System.ComponentModel.BackgroundWorker( );
                    _worker.DoWork += new System.ComponentModel.DoWorkEventHandler(Worker_DoWork);
                }
                return _worker;
            }
        }

        static void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Begin(DEFAULT_TEXT);
        }
        private static FlushForm _instance;
        private static FlushForm instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new FlushForm( );
                return _instance;
            }
        }
        public static void End( )
        {
            if (Worker.IsBusy)
            {
                instance.Invoke(new MethodInvoker(( ) =>
                    {
                        if (instance.Visible)
                            (instance as Form).Close( );
                    }));
            }
        }
        public static void Begin( )
        {
            //Worker.RunWorkerAsync( );
            //Begin(DEFAULT_TEXT);
        }
        public static void Begin(string text)
        {
            if (!instance.Visible)
            {
                instance.SetText(text);
                //if (MainMetroForm.Instance.Visible)
                //{
                //    instance.StartPosition = FormStartPosition.CenterParent;
                //    instance.Show(MainMetroForm.Instance);
                //}
                //else
                //{
                //    instance.StartPosition = FormStartPosition.CenterScreen;
                //    (instance as Form).Show( );
                //}
                if (!instance.Visible)
                    instance.ShowDialog( );
            }
        }
        public FlushForm( )
        {
            InitializeComponent( );
        }
        private void SetText(string text)
        {
            
        }
    }
}
