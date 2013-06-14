using System;
using System.Collections.Generic;
using System.Web;

namespace Eleooo.Common
{
    [System.AttributeUsage(System.AttributeTargets.Assembly, AllowMultiple = true)]
    public class NavigationDefineAttribute : System.Attribute
    {
        public string NavName { get; set; }
        public string NavUrl { get; set; }
        public SubSystem SubSys { get; set; }
        public string P_NavUrl { get; set; }
        public string SecName { get; set; }
        public string OthName { get; set; }
        public bool IsMainNav { get; set; }
        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }
        public bool PermissionRequired { get; set; }
        public bool Visible { get; set; }
        public string NavIcon { get; set; }
    }
}