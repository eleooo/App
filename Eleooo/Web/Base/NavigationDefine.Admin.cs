using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.Common;

//admin navigation define
#region 消费管理
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "消费记录",
    NavUrl = "/Admin/SaleList.aspx",
    PermissionRequired = true,
    SecName = "消费管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "")]
#endregion

#region 财务管理
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "会员储值查询",
    NavUrl = "/Admin/FinanceListCash1.aspx",
    PermissionRequired = true,
    SecName = "财务管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "商家储值查询",
    NavUrl = "/Admin/FinanceListCash.aspx",
    PermissionRequired = true,
    SecName = "财务管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/FinanceListCash1.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "商家积分查询",
    NavUrl = "/Admin/FinanceListPoint.aspx",
    PermissionRequired = true,
    SecName = "财务管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/FinanceListCash1.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "会员积分查询",
    NavUrl = "/Admin/FinanceListPoint1.aspx",
    PermissionRequired = true,
    SecName = "财务管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/FinanceListCash1.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "商家积分结算",
    NavUrl = "/Admin/FinancePayPoint.aspx",
    PermissionRequired = true,
    SecName = "财务结算",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/FinanceListCash1.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "商家积分结算查询",
    NavUrl = "/Admin/FinancePayPointList.aspx",
    PermissionRequired = true,
    SecName = "财务结算",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/FinanceListCash1.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "商家储值结算",
    NavUrl = "/Admin/FinancePayCash.aspx",
    PermissionRequired = true,
    SecName = "财务结算",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/FinanceListCash1.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "商家储值结算查询",
    NavUrl = "/Admin/FinancePayCashList.aspx",
    PermissionRequired = true,
    SecName = "财务结算",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/FinanceListCash1.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "商家佣金结算",
    NavUrl = "/Admin/FinancePay.aspx",
    PermissionRequired = true,
    SecName = "财务结算",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/FinanceListCash1.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "商家佣金结算查询",
    NavUrl = "/Admin/FinancePayList.aspx",
    PermissionRequired = true,
    SecName = "财务结算",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/FinanceListCash1.aspx")]
#endregion

#region 会员管理
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "会员查询",
    NavUrl = "/Admin/MemberList.aspx",
    PermissionRequired = true,
    SecName = "会员管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "商家查询",
    NavUrl = "/Admin/CompanyList.aspx",
    PermissionRequired = true,
    SecName = "会员管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/MemberList.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "添加商家",
    NavUrl = "/Admin/CompanyAdd.aspx",
    PermissionRequired = true,
    SecName = "会员管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/MemberList.aspx")]
#endregion

#region 客服管理
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "未处理的消息",
    NavUrl = "/Admin/SupportList.aspx?Status=1",
    PermissionRequired = true,
    SecName = "客服管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "处理中的消息",
    NavUrl = "/Admin/SupportList.aspx?Status=2",
    PermissionRequired = true,
    SecName = "客服管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/SupportList.aspx?Status=1")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "处理中的消息(需要回复)",
    NavUrl = "/Admin/SupportList.aspx?Status=4",
    PermissionRequired = true,
    SecName = "客服管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/SupportList.aspx?Status=1")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "已经完成的消息",
    NavUrl = "/Admin/SupportList.aspx?Status=3",
    PermissionRequired = true,
    SecName = "客服管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/SupportList.aspx?Status=1")]
#endregion

#region 系统管理
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "在线用户",
    NavUrl = "/Admin/SysOnLine.aspx",
    PermissionRequired = true,
    SecName = "系统管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "数据备份",
    NavUrl = "/Admin/SysBackup.aspx",
    PermissionRequired = true,
    SecName = "系统管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/SysOnLine.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "区域维护",
    NavUrl = "/Admin/SysAreaList.aspx",
    PermissionRequired = true,
    SecName = "系统管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/SysOnLine.aspx")]
#endregion

//营销推广
#region 营销推广
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "提供优惠",
    NavUrl = "/Admin/CompanyItemEdit.aspx",
    PermissionRequired = true,
    SecName = "优惠管理",
    OthName = "营销推广",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "优惠列表",
    NavUrl = "/Admin/CompanyItemList.aspx",
    PermissionRequired = true,
    SecName = "优惠管理",
    OthName = "营销推广",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/CompanyItemEdit.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "投放广告",
    NavUrl = "/Admin/CompanyAdsEdit.aspx",
    PermissionRequired = true,
    SecName = "广告管理",
    OthName = "营销推广",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/CompanyItemEdit.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "广告列表",
    NavUrl = "/Admin/CompanyAdsList.aspx",
    PermissionRequired = true,
    SecName = "广告管理",
    OthName = "营销推广",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/CompanyItemEdit.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "广告浏览设置",
    NavUrl = "/Admin/SysAdsClickSettings.aspx",
    PermissionRequired = true,
    SecName = "广告管理",
    OthName = "营销推广",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/CompanyItemEdit.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "好友推荐设置",
    NavUrl = "/Admin/SysRewardList.aspx",
    PermissionRequired = true,
    SecName = "好友推荐设置",
    OthName = "营销推广",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/CompanyItemEdit.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "抢购记录",
    NavUrl = "/Admin/CompanyItemUsed.aspx",
    PermissionRequired = true,
    SecName = "优惠管理",
    OthName = "营销推广",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/CompanyItemEdit.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "浏览记录",
    NavUrl = "/Admin/CompanyAdsClicked.aspx",
    PermissionRequired = true,
    SecName = "广告管理",
    OthName = "营销推广",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/CompanyItemEdit.aspx")]
#endregion

#region 订餐管理
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "订餐记录",
    NavUrl = "/Admin/SysOrderMeal.aspx",
    PermissionRequired = true,
    SecName = "订餐记录",
    OthName = "订餐管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "外卖菜单管理",
    NavUrl = "/Admin/SysCompanyMealMenuList.aspx",
    PermissionRequired = true,
    SecName = "外卖管理",
    OthName = "订餐管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/SysOrderMeal.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "外卖大厦管理",
    NavUrl = "/Admin/SysCompanyMansionList.aspx",
    PermissionRequired = true,
    SecName = "外卖管理",
    OthName = "订餐管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/SysOrderMeal.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "外卖菜单分类",
    NavUrl = "/Admin/MealDirectoryList.aspx",
    PermissionRequired = true,
    SecName = "外卖管理",
    OthName = "订餐管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/SysOrderMeal.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "区域大厦信息",
    NavUrl = "/Admin/SysAreaMansionList.aspx",
    PermissionRequired = true,
    SecName = "外卖管理",
    OthName = "订餐管理",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/SysOrderMeal.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "会员点评与吐槽",
    NavUrl = "/Admin/MyFacebook.aspx",
    PermissionRequired = true,
    SecName = "外卖管理",
    OthName = "会员点评与吐槽",
    SubSys = SubSystem.Admin,
    Visible = true,
    P_NavUrl = "/Admin/SysOrderMeal.aspx")]
#endregion