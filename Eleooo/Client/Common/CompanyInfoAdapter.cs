using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace Eleooo.Client
{
    public delegate void WorkerResultHandler(params object[] result);
    public class CompanyInfoAdapter
    {
        private static CompanyInfoAdapter _instance;
        public static CompanyInfoAdapter Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CompanyInfoAdapter( );
                return _instance;
            }
        }
        private BackgroundWorker _worker;
        private BackgroundWorker Worker
        {
            get
            {
                if (_worker == null)
                {
                    _worker = new BackgroundWorker( );
                    Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
                    Worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
                }
                return _worker;
            }
        }
        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arg = (object[])e.Argument;
            int id = Convert.ToInt32(arg[0]);
            string fileName = Convert.ToString(arg[1]);
            object[] result = new object[2] { null, null };
            try
            {
                result[0] = ServiceProvider.Service.GetCompanyInfo(id);
            }
            catch { }
            if (!string.IsNullOrEmpty(fileName))
                try
                {
                    result[1] = ServiceProvider.Service.GetFile(fileName, 1);
                }
                catch { }
            e.Result = result;
        }
        private WorkerResultHandler _resultHandler { get; set; }
        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_resultHandler != null && e.Result != null)
            {
                object[] result = (object[])e.Result;
                _resultHandler(result);
            }
        }
        public void Start(WorkerResultHandler resultHandler)
        {
            Start(false, resultHandler);
        }
        public void Start(bool isGetPic, WorkerResultHandler resultHandler)
        {
            if (Worker.IsBusy)
                return;
            _resultHandler = resultHandler;
            string picName = isGetPic ? AppContext.Company.CompanyPhoto : string.Empty;
            Worker.RunWorkerAsync(new object[] { AppContext.Company.Id, picName });
        }
    }
}
