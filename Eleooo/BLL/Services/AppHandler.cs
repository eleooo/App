using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Eleooo.Common;

namespace Eleooo.BLL.Services
{
    class AppHandler : IHandlerServices
    {
        public void Log(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request["source"]) && !string.IsNullOrEmpty(context.Request["message"]))
                Logging.Log(context.Request["source"], context.Request["message"], context.Request["trace"], false);
        }
    }
}
