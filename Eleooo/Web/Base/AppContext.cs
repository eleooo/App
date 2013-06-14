using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.DAL;
using System.Web.Security;
using Eleooo.Common;

namespace Eleooo.Web
{
    public class AppContext : AppContextBase
    {
        static AppContext( )
        {
            _unAuthContext = new AppContext( );
            _unAuthContext.User = new SysMember( );
            _contextType = typeof(AppContext);
        }
        private SysMemberConfig _userConfig;
        public override SysMemberConfig UserConfig
        {
            get
            {
                if (_userConfig == null)
                    _userConfig = UserBLL.GetUserConfig(User.Id);
                return _userConfig;
            }
        }

        public override void AddMessage(string key, string message)
        {
            ActPage.Message.Add(key, message);
        }

        public override SubSystem ParamSubSys
        {
            get
            {
                return ActPage.ParamSubSys;
            }
        }

        public static ActionPage ActPage
        {
            get
            {
                return HttpContext.Current.Handler as ActionPage;
            }
        }

        public static void InitContext( ) { }
    }
}