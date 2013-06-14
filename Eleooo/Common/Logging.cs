using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;

namespace Eleooo.Common
{
    public static class Logging
    {
        public static void Log(string source, string messge, string trace, bool isDelay = false)
        {
            SysErrorLog log = new SysErrorLog
            {
                LogDate = DateTime.Now,
                LogUser = AppContextBase.CurrentUserID,
                SubSys = Convert.ToInt32(AppContextBase.CurrentSysId),
                LogSource = source,
                LogMessage = messge,
                LogStackTrace = trace
            };
            if (isDelay)
                BackgroundWorker.DoWork<SysErrorLog>(log, 2, e => e.Save( ));
            else
                log.Save( );
        }
        public static void Log(string source, Exception ex,bool isDelay = false)
        {
            Log(source, ex.Message, ex.StackTrace, isDelay);
        }
    }
}