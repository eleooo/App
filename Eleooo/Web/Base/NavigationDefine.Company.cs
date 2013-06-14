﻿using System;
using System.Collections.Generic;
using System.Web;
using Eleooo.Common;

//Company Navigation define

//消费管理
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "会员消费",
    NavUrl = "/Company/SaleAdd.aspx",
    PermissionRequired = true,
    SecName = "消费管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "消费记录",
    NavUrl = "/Company/SaleList.aspx",
    PermissionRequired = true,
    SecName = "消费管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/SaleAdd.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "会员储值",
    NavUrl = "/Company/FinanceCash.aspx",
    PermissionRequired = true,
    SecName = "储值管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/SaleAdd.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "储值记录",
    NavUrl = "/Company/FinanceList.aspx",
    PermissionRequired = true,
    SecName = "储值管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/SaleAdd.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "储值统计",
    NavUrl = "/Company/FinanceSum.aspx",
    PermissionRequired = true,
    SecName = "储值管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/SaleAdd.aspx")]

//会员管理
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "添加会员",
    NavUrl = "/Company/MemberAdd.aspx",
    PermissionRequired = true,
    SecName = "会员管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "补录指纹",
    NavUrl = "/Company/MemberFinger.aspx",
    PermissionRequired = true,
    SecName = "会员管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MemberAdd.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "本店会员",
    NavUrl = "/Company/MemberListSelf.aspx",
    PermissionRequired = true,
    SecName = "会员管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MemberAdd.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "外来会员",
    NavUrl = "/Company/MemberList.aspx",
    PermissionRequired = true,
    SecName = "会员管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MemberAdd.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "导入会员",
    NavUrl = "/Company/MemberExportIn.aspx",
    PermissionRequired = true,
    SecName = "会员管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MemberAdd.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "会员等级",
    NavUrl = "/Company/MemberGrade.aspx",
    PermissionRequired = true,
    SecName = "会员管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MemberAdd.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "会员点评",
    NavUrl = "/Company/MyFacebook.aspx",
    PermissionRequired = true,
    SecName = "会员点评",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MemberAdd.aspx")]

//财务管理
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "现金明细",
    NavUrl = "/Company/FinanceCashList1.aspx",
    PermissionRequired = true,
    SecName = "财务管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "储值明细",
    NavUrl = "/Company/FinanceListCash.aspx",
    PermissionRequired = true,
    SecName = "财务管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/FinanceListCash1.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "积分明细",
    NavUrl = "/Company/FinanceListPoint.aspx",
    PermissionRequired = true,
    SecName = "财务管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/FinanceListCash1.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "导入明细",
    NavUrl = "/Company/FinanceListExportIn.aspx",
    PermissionRequired = true,
    SecName = "财务管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/FinanceListCash1.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "积分结算记录",
    NavUrl = "/Company/FinancePayPoint.aspx",
    PermissionRequired = true,
    SecName = "结算管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/FinanceListCash1.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "佣金结算记录",
    NavUrl = "/Company/FinancePayRate.aspx",
    PermissionRequired = true,
    SecName = "结算管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/FinanceListCash1.aspx")]

//联盟商圈
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "联盟商圈",
    NavUrl = "/Company/MyCompany.aspx",
    PermissionRequired = true,
    SecName = "联盟商圈",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "")]

//系统管理
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "我的资料",
    NavUrl = "/Company/MyInfo.aspx",
    PermissionRequired = true,
    SecName = "系统管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "修改密码",
    NavUrl = "/Company/MyPwd.aspx",
    PermissionRequired = true,
    SecName = "系统管理",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MyInfo.aspx")]

//在线客服
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "未处理的问题",
    NavUrl = "/Company/SupportList.aspx?Status=1",
    PermissionRequired = true,
    SecName = "在线客服",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MyInfo.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "处理中的问题",
    NavUrl = "/Company/SupportList.aspx?Status=2",
    PermissionRequired = true,
    SecName = "在线客服",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MyInfo.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "已结束的问题",
    NavUrl = "/Company/SupportList.aspx?Status=3",
    PermissionRequired = true,
    SecName = "在线客服",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MyInfo.aspx")]

[assembly: NavigationDefine(IsMainNav = false,
    NavName = "我要提问",
    NavUrl = "/Company/SupportEdit.aspx",
    PermissionRequired = true,
    SecName = "在线客服",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/MyInfo.aspx")]

//营销推广
[assembly: NavigationDefine(IsMainNav = true,
    NavName = "提供优惠",
    NavUrl = "/Company/CompanyItemEdit.aspx",
    PermissionRequired = true,
    SecName = "优惠管理",
    OthName = "营销推广",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "优惠列表",
    NavUrl = "/Company/CompanyItemList.aspx",
    PermissionRequired = true,
    SecName = "优惠管理",
    OthName = "营销推广",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/CompanyItemEdit.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "抢购记录",
    NavUrl = "/Company/CompanyItemUsed.aspx",
    PermissionRequired = true,
    SecName = "优惠管理",
    OthName = "营销推广",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/CompanyItemEdit.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "投放广告",
    NavUrl = "/Company/CompanyAdsEdit.aspx",
    PermissionRequired = true,
    SecName = "广告管理",
    OthName = "营销推广",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/CompanyItemEdit.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "广告列表",
    NavUrl = "/Company/CompanyAdsList.aspx",
    PermissionRequired = true,
    SecName = "广告管理",
    OthName = "营销推广",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/CompanyItemEdit.aspx")]
[assembly: NavigationDefine(IsMainNav = false,
    NavName = "浏览记录",
    NavUrl = "/Company/CompanyAdsClicked.aspx",
    PermissionRequired = true,
    SecName = "广告管理",
    OthName = "营销推广",
    SubSys = SubSystem.Company,
    Visible = true,
    P_NavUrl = "/Company/CompanyItemEdit.aspx")]