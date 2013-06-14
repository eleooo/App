using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.Common;

//Public navigation define
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "关于我们",
    NavUrl = "/Public/EleoooAboutUs.aspx",
    PermissionRequired = false,
    SecName = "关于我们",
    SubSys = SubSystem.ALL,
    Visible = true,
    P_NavUrl = "", IsFooter = true)]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "联系我们",
    NavUrl = "/Public/EleoooContactUs.aspx",
    PermissionRequired = false,
    SecName = "联系我们",
    SubSys = SubSystem.ALL,
    Visible = true,
    P_NavUrl = "", IsFooter = true)]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "服务协议",
    NavUrl = "/Public/EleoooHelp.aspx",
    PermissionRequired = false,
    SecName = "服务协议",
    SubSys = SubSystem.ALL,
    Visible = true,
    P_NavUrl = "", IsFooter = true)]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "商家加盟",
    NavUrl = "/Public/EleoooJoin.aspx",
    PermissionRequired = false,
    SecName = "商家加盟",
    SubSys = SubSystem.ALL,
    Visible = true,
    P_NavUrl = "", IsFooter = true)]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "乐多分客户端",
    NavUrl = "/Public/EleoooClient.aspx",
    PermissionRequired = false,
    SecName = "乐多分客户端",
    SubSys = SubSystem.ALL,
    Visible = true,
    P_NavUrl = "", IsFooter = true)]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "安全退出",
    NavUrl = "/Default.aspx?Action=Logout",
    PermissionRequired = true,
    SecName = "安全退出",
    SubSys = SubSystem.ALL,
    Visible = true,
    P_NavUrl = "", IsFooter = true,IsHeader=true)]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "首页",
    NavUrl = "/Default.aspx",
    PermissionRequired = false,
    SecName = "首页",
    SubSys = SubSystem.ALL,
    Visible = true,
    P_NavUrl = "", IsFooter = true)]