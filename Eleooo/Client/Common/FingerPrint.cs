using System;
using System.Collections.Generic;
using System.Text;
using FingerActivex;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Eleooo.Client
{
    public class FingerPrint : IDisposable
    {
        public delegate void FingerResultHandler(string fingerCode, string fingerImg, string message, bool isSuccess);
        [DllImport("ARTH_DLL.dll")]
        private static extern int Match2Fp(ref byte Src, ref byte Dst);
        [DllImport("ARTH_DLL.dll")]
        private static extern int GenChar(ref byte fingerData, ref byte charData);

        [DllImport("ZAZAPIt.dll")]
        private static extern int ZAZOpenDeviceEx(ref IntPtr pHandle, int nDeviceType, int iCom = 1, int iBaud = 1, int nPackageSize = 2, int iDevNum = 0);
        [DllImport("ZAZAPIt.dll")]
        private static extern int ZAZGetImage(IntPtr pHandle, uint nAddr);
        [DllImport("ZAZAPIt.dll")]
        private static extern int ZAZUpChar2File(IntPtr hHandle, uint nAddr, int iBufferID, char[] pFileName);
        [DllImport("ZAZAPIt.dll")]
        private static extern int ZAZGenChar(IntPtr pHandle, uint nAddr, int iBufferID);
        [DllImport("ZAZAPIt.dll")]
        private static extern int ZAZCloseDeviceEx(IntPtr pHandle);
        [DllImport("ZAZAPIt.dll", CharSet = CharSet.Ansi)]
        private static extern string ZAZErr2Str(int nErrCode);

        const string FINGEROCX = "FingerActivex.ocx";
        const string FINGER_IMG = "finger.bmp";
        const string CHARFILE = @"C:\1.dat";
        const uint ADDR = 0xffffffff;
        const uint DIS_LINE = 10;
        const int IMAGE_SIZE = 73728;
        const int CHAR_SIZE = 512;
        const int FINGER_SIZE = 1024;
        const int BUFFER_ID = 0x01;
        const int IMAGE_X =256;
        const int IMAGE_Y =288;
        const int DEF_OFFSET = 1078;

        object spDeviceType = 0;
        object spComPort = 0;
        object spBaudRate = 0;
        private IntPtr devHandle = IntPtr.Zero;
        private BackgroundWorker _reader;
        private BackgroundWorker Reader
        {
            get
            {
                if (_reader == null)
                {
                    _reader = new BackgroundWorker( );
                    _reader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Reader_RunWorkerCompleted);
                    _reader.DoWork += new DoWorkEventHandler(Reader_DoWork);
                    _reader.WorkerReportsProgress = true;
                    _reader.WorkerSupportsCancellation = true;
                }
                return _reader;
            }
        }

        private static FingerPrint _finger;
        public static FingerPrint Finger
        {
            get
            {
                if (_finger == null)
                    _finger = new FingerPrint( );
                return _finger;
            }
        }
        private int nRet = 0;
        FingerResultHandler _resultHandler;
        private FingerXClass _fingerX;
        private FingerXClass fingerX
        {
            get
            {
                if (_fingerX == null)
                {
                    try
                    {
                        _fingerX = new FingerXClass( );
                    }
                    catch
                    {
                        InitFingerActivex( );
                        try
                        {
                            _fingerX = new FingerXClass( );
                        }
                        catch (Exception ex)
                        {
                            _message = string.Format("初始化指纹控件错误,请在乐多分官网下载相应的组件进行注册:{0}", ex.Message);
                        }
                    }
                }
                return _fingerX;
            }
        }
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
        }
        private string _fingerCode;
        public string FingerCode
        {
            get
            {
                return _fingerCode;
            }
        }
        private void InitFingerActivex( )
        {
            string ocxPath = string.Concat(Directory.GetCurrentDirectory( ), "\\", FINGEROCX);
            string regApp = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.System), "\\regsvr32.exe");
            string arg = string.Format("/s {0}", ocxPath);
            ProcessStartInfo info = new ProcessStartInfo(regApp, arg);
            Process process = new Process( );
            process.StartInfo = info;
            process.Start( );
            process.WaitForExit(5000);
        }
        private int ReadFinger( )
        {
            if (fingerX == null)
                return -1;
            object oRet = fingerX.GetImgCode(ref spDeviceType, ref spComPort, ref spBaudRate);
            _message = Convert.ToString(fingerX.Msg);
            return Convert.ToInt32(oRet);
        }
        private void CloseDeviceEx( )
        {
            if (devHandle != IntPtr.Zero)
                nRet = ZAZCloseDeviceEx(devHandle);
            else
                nRet = 0;
            devHandle = IntPtr.Zero;
        }
        private string Err2Str(int nErrCode)
        {
            return ZAZErr2Str(nErrCode);
        }
        private int OpenDeviceEx( )
        {
            if (devHandle == IntPtr.Zero)
                nRet = ZAZOpenDeviceEx(ref devHandle, 0, 0, 0, 2, 0);
            else
                nRet = 0;
            if (nRet != 0)
                devHandle = IntPtr.Zero;
            return nRet;
        }
        public void BeginRead(FingerResultHandler resultHandler)
        {
            if (Reader.IsBusy)
                Reader.CancelAsync( );
            _resultHandler = resultHandler;
            Reader.RunWorkerAsync( );
        }
        public bool MatchFinger(string srcFingerCode, string destFingerCode)
        {
            object src = srcFingerCode;
            object dest = destFingerCode;
            try
            {
                return src != null && dest != null &&
                        fingerX != null &&
                        Convert.ToInt32(fingerX.MatchFinger(ref src, ref dest)) > 50;
            }
            catch
            {
                return false;
            }
        }
        public void CancelRead( )
        {
            if (Reader.IsBusy)
                Reader.CancelAsync( );
        }
        public bool IsBusy
        {
            get
            {
                return Reader.IsBusy;
            }
        }
        public static bool Match2Fp(string srcFinger, string destFinger)
        {
            if (string.IsNullOrEmpty(srcFinger) || string.IsNullOrEmpty(destFinger))
                return false;
            byte[] src = HexStringToByte(srcFinger);
            byte[] dest = HexStringToByte(destFinger);
            return Match2Fp(ref src[0], ref dest[0]) > 50;
        }
        private static byte[] HexStringToByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }


        void Reader_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = -2;
            if ((e.Cancel = Reader.CancellationPending))
            {
                _message = "用户取消操作";
                return;
            }
            if (OpenDeviceEx( ) != 0)
            {
                e.Result = nRet;
                _message = "打开设备失败:" + Err2Str(nRet);
                return;
            }
            while ((nRet = ZAZGetImage(devHandle, ADDR)) == 2 && 
                    !(e.Cancel = Reader.CancellationPending))
            {
            }
            if (e.Cancel)
            {
                CloseDeviceEx();
                _message = "用户取消操作";
                return;
            }
            if (nRet != 0)
            {
                e.Result = nRet;
                _message = "读取指纹出错:" + Err2Str(nRet);
                CloseDeviceEx( );
                return;
            }
            //附加操作
            //ZAZGenChar(devHandle, ADDR, BUFFER_ID);
            //ZAZUpChar2File(devHandle, ADDR, BUFFER_ID, CHARFILE.ToCharArray( ));
            CloseDeviceEx( );
            e.Result = ReadFinger( );
        }
        void Reader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int nResult = e.Cancelled?-2:Convert.ToInt32(e.Result);
            _fingerCode = nResult == -1 ? string.Empty : Convert.ToString(fingerX.FingerCode);
            if (_resultHandler != null)
                _resultHandler(_fingerCode, nResult == 0 ? FINGER_IMG : string.Empty, _message, nResult == 0);
        }
        void Reader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }
        #region IDisposable 成员

        public void Dispose( )
        {
            if (Reader.IsBusy)
                Reader.CancelAsync( );
            Reader.Dispose( );
            CloseDeviceEx( );
        }

        #endregion
    }
}
