<%@ Page Title="我的商圈" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="MyCompany.aspx.cs" Inherits="Eleooo.Web.Member.MyCompany" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcTypeAndAreaFilter.ascx" TagPrefix="uc" TagName="UcTypeAndAreaFilter" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <uc:UcTypeAndAreaFilter ID="filter" runat="server" IsShowAreaFilter="false" />
    <div class="mainlist ">
        <ele:DataListExt ID="view" runat="server" AllowPaging="true" OnItemCreated="view_ItemCreated"
            ShowFootPaging="true" ShowHeadPaging="false">
            <HeaderTemplate>
                <ul class="clearall shlist">
            </HeaderTemplate>
            <FooterTemplate>
                </ul></FooterTemplate>
            <ItemTemplate>
                <li>
                    <div class="listimg">
                        <a href="MyCompanyDetail.aspx?CompanyID=<%#Eval("[ID]") %>">
                            <img alt="" src="<%#Eval("[Photo]") %>" /></a>
                    </div>
                    <div class="listInfo">
                        <h3>
                            <a href="MyCompanyDetail.aspx?CompanyID=<%#Eval("[ID]") %>">
                                <%#Eval("[CompanyName]") %></a></h3>
                        <ul>
                            <li>
                                <div class="fl listInfoLong">
                                    <span>商圈：</span><%#Eval("[CompanyArea]") %></div>
                                <div class="fl listInfoLong">
                                    <span>电话：</span><%#Eval("[CompanyPhone]")%></div>
                                <div class="fl listInfoSort">
                                    <span>
                                        <img alt="" align="absmiddle" src="/App_Themes/ThemesV2/images/jifen.gif">
                                    </span><span class="s1">：<%#Eval("[Rate]") %></span></div>
                                <div class="fl listInfoLong">
                                    <span>地址：</span><%#Eval("[CompanyAddress]")%></div>
                                <div class="fl listInfoSort">
                                    <span>
                                        <img alt="" align="absmiddle" src="/App_Themes/ThemesV2/images/zhekou.gif"></span><span
                                            class="s1"> ：<%#Eval("[CashRate]") %></span></div>
                            </li>
                        </ul>
                    </div>
                    <div class="listBtn">
                        <p>
                            <a href="javascript:void(0)" onclick="openItemLink('<%#Eval("[ItemID]") %>');">
                                <img alt="" align="absmiddle" src="/App_Themes/ThemesV2/images/zxyh.png"><span class="s1">最新优惠</span></a></p>
                        <p>
                            <a href="MyCompanyDetail.aspx?CompanyID=<%#Eval("[ID]") %>">
                                <img alt="" align="absmiddle" src="/App_Themes/ThemesV2/images/wydp.png"><span class="s1">我要点评</span></a></p>
                    </div>
                </li>
            </ItemTemplate>
        </ele:DataListExt>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
    <script type="text/javascript">
        var openItemLink = function (itemId) {
            if (itemId && itemId != '')
                window.location = '/Public/ViewItemDetail.aspx?ItemID=' + itemId;
            else
                alert("该商家暂无优惠提供");
        }
        var openAdsLink = function (companyID, adsID) {
            if (adsID && adsID != '')
                window.location = 'CompanyAds.aspx?CompanyID=' + companyID;
            else
                alert("该商家暂无广告浏览");
        }
    </script>
</asp:Content>
