using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.Common;

//member navigation define
#region 我的消费
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "消费记录",
    NavUrl = "/Member/SaleList.aspx",
    PermissionRequired = true,
    SecName = "我的消费",
    SubSys = SubSystem.Member,
    NavIcon = "ico_mzb",
    Visible = true,
    P_NavUrl = "")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "储值商家",
    NavUrl = "/Member/MyCash.aspx",
    PermissionRequired = true,
    SecName = "我的消费",
    SubSys = SubSystem.Member,
    NavIcon = "ico_czjl",
    Visible = true,
    P_NavUrl = "/Member/SaleList.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "储值记录",
    NavUrl = "/Member/MyCashDetail.aspx",
    PermissionRequired = true,
    SecName = "我的消费",
    SubSys = SubSystem.Member,
    NavIcon = "ico_czsj",
    Visible = true,
    P_NavUrl = "/Member/SaleList.aspx")]

//[assembly: NavigationDefine(IsMainNav = false,
//    NavName = "积分转移",
//    NavUrl = "/Member/MyCashMove.aspx",
//    PermissionRequired = true,
//    SecName = "我的消费",
//    SubSys = SubSystem.Member,
//    Visible = false,
//    P_NavUrl = "/Member/SaleList.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "现金明细",
    NavUrl = "/Member/FinanceListCash1.aspx",
    PermissionRequired = true,
    SecName = "我的账本",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_myprice",
    P_NavUrl = "/Member/SaleList.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "储值明细",
    NavUrl = "/Member/FinanceListCash.aspx",
    PermissionRequired = true,
    SecName = "我的账本",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_myjl",
    P_NavUrl = "/Member/SaleList.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "积分明细",
    NavUrl = "/Member/FinanceList.aspx",
    PermissionRequired = true,
    SecName = "我的账本",
    SubSys = SubSystem.Member,
    NavIcon = "ico_mypoint",
    Visible = true,
    P_NavUrl = "/Member/SaleList.aspx")]
#endregion

#region 我的商圈
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "生活圈",
    NavUrl = "/Member/MyCompany.aspx",
    PermissionRequired = true,
    SecName = "我的商圈",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_email",
    P_NavUrl = "")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "工作圈",
    NavUrl = "/Member/MyCompany.aspx?areaType=1",
    PermissionRequired = true,
    SecName = "我的商圈",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_work",
    P_NavUrl = "/Member/MyCompany.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "腐败圈",
    NavUrl = "/Member/MyCompany.aspx?areaType=2",
    PermissionRequired = true,
    SecName = "我的商圈",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_fb",
    P_NavUrl = "/Member/MyCompany.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "推荐商家",
    NavUrl = "/Member/MyCompanyREdit.aspx",
    PermissionRequired = true,
    SecName = "我的商圈",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_tj",
    P_NavUrl = "/Member/MyCompany.aspx")]
//[assembly: NavigationDefine(IsMainNav = false,
//    NavName = "我的商圈",
//    NavUrl = "/Member/MyCompanyDetail.aspx",
//    PermissionRequired = false,
//    SecName = "我的商圈",
//    SubSys = SubSystem.Member,
//    Visible = false,
//    NavIcon = "ico_tj",
//    P_NavUrl = "/Member/MyCompany.aspx")]
#endregion

#region 账户设置
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "我的资料",
    NavUrl = "/Member/MyInfo.aspx",
    PermissionRequired = true,
    SecName = "账户设置",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_minfo",
    P_NavUrl = "")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "修改密码",
    NavUrl = "/Member/MyPWD.aspx",
    PermissionRequired = true,
    SecName = "账户设置",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_mpwd",
    P_NavUrl = "/Member/MyInfo.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "修改商圈",
    NavUrl = "/Member/MyArea.aspx",
    PermissionRequired = true,
    SecName = "账户设置",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_coupon",
    P_NavUrl = "/Member/MyInfo.aspx")]
#endregion

#region 在线客服
//[assembly: NavigationDefine(IsMainNav = false,
//    NavName = "未处理的问题",
//    NavUrl = "/Member/SupportList.aspx?Status=1",
//    PermissionRequired = true,
//    SecName = "在线客服",
//    SubSys = SubSystem.Member,
//    Visible = true,
//    P_NavUrl = "/Member/MyInfo.aspx")]

//[assembly: NavigationDefine(IsMainNav = false,
//    NavName = "处理中的问题",
//    NavUrl = "/Member/SupportList.aspx?Status=2",
//    PermissionRequired = true,
//    SecName = "在线客服",
//    SubSys = SubSystem.Member,
//    Visible = true,
//    P_NavUrl = "/Member/MyInfo.aspx")]

//[assembly: NavigationDefine(IsMainNav = false,
//    NavName = "已结束的问题",
//    NavUrl = "/Member/SupportList.aspx?Status=3",
//    PermissionRequired = true,
//    SecName = "在线客服",
//    SubSys = SubSystem.Member,
//    Visible = true,
//    P_NavUrl = "/Member/MyInfo.aspx")]

//[assembly: NavigationDefine(IsMainNav = false,
//    NavName = "我要提问",
//    NavUrl = "/Member/SupportEdit.aspx",
//    PermissionRequired = true,
//    SecName = "在线客服",
//    SubSys = SubSystem.Member,
//    Visible = true,
//    P_NavUrl = "/Member/MyInfo.aspx")]
#endregion

#region 我要抢优惠
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "我家附近",
    NavUrl = "/Member/CompanyItems.aspx",
    PermissionRequired = true,
    SecName = "我要抢优惠",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_myhome",
    P_NavUrl = "")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "公司附近",
    NavUrl = "/Member/CompanyItems.aspx?areaType=1",
    PermissionRequired = true,
    SecName = "我要抢优惠",
    SubSys = SubSystem.Member,
    NavIcon = "ico_workhome",
    Visible = true,
    P_NavUrl = "/Member/CompanyItems.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "我常去的",
    NavUrl = "/Member/CompanyItems.aspx?areaType=2",
    PermissionRequired = true,
    SecName = "我要抢优惠",
    SubSys = SubSystem.Member,
    NavIcon = "ico_mygo",
    Visible = true,
    P_NavUrl = "/Member/CompanyItems.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "优惠收藏",
    NavUrl = "/Member/CompanyMyItems.aspx",
    PermissionRequired = true,
    SecName = "我要抢优惠",
    SubSys = SubSystem.Member,
    NavIcon = "ico_coupon",
    Visible = true,
    P_NavUrl = "/Member/CompanyItems.aspx")]
#endregion

#region 我要赚积分
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "我的广告",
    NavUrl = "/Member/CompanyAds.aspx",
    PermissionRequired = true,
    SecName = "我要赚积分",
    SubSys = SubSystem.Member,
    Visible = true,
    NavIcon = "ico_myad",
    P_NavUrl = "")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "我的进账",
    NavUrl = "/Member/CompanyMyAds.aspx",
    PermissionRequired = true,
    SecName = "我要赚积分",
    SubSys = SubSystem.Member,
    NavIcon = "ico_myjz",
    Visible = true,
    P_NavUrl = "/Member/CompanyAds.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "推荐好友",
    NavUrl = "/Member/RewardFriend.aspx",
    PermissionRequired = true,
    SecName = "我要赚积分",
    SubSys = SubSystem.Member,
    NavIcon = "ico_myfriend",
    Visible = true,
    P_NavUrl = "/Member/CompanyAds.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "我的奖励",
    NavUrl = "/Member/MyReward.aspx",
    PermissionRequired = true,
    SecName = "我要赚积分",
    SubSys = SubSystem.Member,
    NavIcon = "ico_myjl",
    Visible = true,
    P_NavUrl = "/Member/CompanyAds.aspx")]
#endregion