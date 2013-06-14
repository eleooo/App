<%@ Page Title="我要赚积分" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="ViewAdsList.aspx.cs" Inherits="Eleooo.Web.Public.ViewAdsList" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="eleooo" %>
<%@ Register Src="~/Controls/UcTypeAndAreaFilter.ascx" TagName="UcTypeAndAreaFilter"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <eleooo:ResLink ID="rs1" runat="server" Src="/App_Themes/ThemesV2/css/sub.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="sub_con">
        <div class="left">
            <uc:UcTypeAndAreaFilter ID="filter" runat="server" CssClass="typeItemNav" IsShowLoginInfo="true" />
            <eleooo:DataListExt ID="view" runat="server" AllowPaging="true" ShowFootPaging="true"
                ShowHeadPaging="false" EmptyDataIsShowHeaderAndFooterTemplate="false" FooterPagingTemplate="~/Controls/UcFooterPaging1.ascx">
                <HeaderTemplate>
                    <ul class="zjf_list">
                </HeaderTemplate>
                <FooterTemplate>
                    </ul></FooterTemplate>
                <ItemTemplate>
                    <li><a href="/Member/ViewCompanyAds.aspx?AdsID=<%#Eval("[AdsID]") %>">
                        <img alt="" src="<%#Eval("[AdsPic]") %>" /></a>
                        <p>
                            <a href="/Member/ViewCompanyAds.aspx?AdsID=<%#Eval("[AdsID]") %>">
                                <%#Eval("[AdsTitle]",0) %></a></p>
                        <dl>
                            <dt>最高赚<span class="b_yellow">
                                <%#Eval("[AdsPoint]") %>分</span><br />
                                <span class="yellow">限<%#Eval("[AdsArea]") %>会员</span></dt>
                            <dd>
                                <a class="wa_link" href="/Member/ViewCompanyAds.aspx?AdsID=<%#Eval("[AdsID]") %>">我要看</a>
                                已浏览<span class="yellow"><%#Eval("[AdsClicked]") %></span></dd>
                        </dl>
                    </li>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <li class="no_Mar"><a href="/Member/ViewCompanyAds.aspx?AdsID=<%#Eval("[AdsID]") %>">
                        <img alt="" src="<%#Eval("[AdsPic]") %>" /></a>
                        <p>
                            <a href="/Member/ViewCompanyAds.aspx?AdsID=<%#Eval("[AdsID]") %>">
                                <%#Eval("[AdsTitle]",0) %></a></p>
                        <dl>
                            <dt>最高赚<span class="b_yellow">
                                <%#Eval("[AdsPoint]") %>分</span><br />
                                <span class="yellow">限<%#Eval("[AdsArea]") %>会员</span></dt>
                            <dd>
                                <a class="wa_link" href="/Member/ViewCompanyAds.aspx?AdsID=<%#Eval("[AdsID]") %>">我要看</a>
                                已浏览<span class="yellow"><%#Eval("[AdsClicked]") %></span></dd>
                        </dl>
                    </li>
                </AlternatingItemTemplate>
            </eleooo:DataListExt>
        </div>
        <div class="right">
            <div class="r_p">
                <a href="#">
                    <img alt="" src="/App_Themes/ThemesV2/images/ad01.jpg" /></a></div>
            <div class="r_p">
                <a href="#">
                    <img alt="" src="/App_Themes/ThemesV2/images/ad02.jpg" /></a></div>
            <div class="r_p">
                <a href="#">
                    <img alt="" src="/App_Themes/ThemesV2/images/ad03.jpg" /></a></div>
            <div class="r_intro">
                <h2>
                    <a class="qk_link" href="javascript:void(0);" onclick="__doPostBack('Delete');">清空</a>最近浏览过</h2>
                <eleooo:DataListExt ID="viewRecAds" runat="server" AllowPaging="false" EmptyDataIsShowHeaderAndFooterTemplate="false">
                    <HeaderTemplate>
                        <ul class="tray_list">
                    </HeaderTemplate>
                    <FooterTemplate>
                        </ul></FooterTemplate>
                    <EmptyDataTemplate>
                        <div class="no_tray">
                            暂无浏览记录
                        </div>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <li><a class="tray_pic" href="/Member/ViewCompanyAds.aspx?AdsID=<%#Eval("AdsID") %>">
                            <img alt="" src="<%#Eval("AdsPic") %>" /></a>
                            <div class="tray_right">
                                <div>
                                    <a href="/Member/ViewCompanyAds.aspx?AdsID=<%#Eval("AdsID") %>">
                                        <%#Eval("AdsTitle",0) %></a></div>
                                <div class="dh_div">
                                    最高赚 <span class="yellow">
                                        <%#Eval("AdsPoint",1) %></span>分</div>
                            </div>
                        </li>
                    </ItemTemplate>
                </eleooo:DataListExt>
            </div>
            <div class="r_intro">
                <h2>
                    乐多分小助手</h2>
                <ul class="x_help">
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>怎样看广告才能得到积分奖励？
                        </p>
                        <p class="p2">
                            浏览广告后，您只需回答广告主提出的一个问题，答对即可获得积分奖励。</p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style=" font-family:宋体;">·</b>每次看广告都能奖励积分么？</p>
                        <p class="p2">
                            是的，只要您符合广告主的要求，完整浏览并答对问题，即可获得积分奖励。
                        </p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style=" font-family:宋体;">·</b>看一条广告奖励多少积分？</p>
                        <p class="p2">
                            取决于广告主给出的奖励标准，以及您所拥有的权限。您上月在乐多分合作商家的累计消费额度越高，浏览广告的单次奖励额度越高。</p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>所有广告都可以看吗？
                        </p>
                        <p class="p2">
                            广告主将设定投放区域，浏览者性别、年龄、上月消费额等，如果您符合相关条件，即可获得积分奖励。请尽量完善个人信息，以获得更多看广告的机会。
                        </p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>可以重复看一条广告吗？</p>
                        <p class="p2">
                            广告主将设定投放周期，在该投放周期内，您只能获得一次有效浏览和奖励的机会。</p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>每天能看多少条广告？
                        </p>
                        <p class="p2">
                            新注册会员只能浏览1条广告。已消费过的会员，上月消费额200元以下，每天最高可浏览2条广告；200元以上，最高可浏览3条广告； 300元以上，最高可浏览5条广告。
                        </p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>如何获得更高权限？
                        </p>
                        <p class="p2">
                            您的个人信息越全面，过往累积消费额度越高，看广告的权限越大。
                        </p>
                    </li>
                </ul>
                <p>
                </p>
                <p>
                </p>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".x_help li").click(function () {
                var p2 = $(this).find(".p2");
                var isShow = p2.css("display") != 'none';
                if (isShow)
                    p2.hide();
                else
                    p2.show().end().siblings().find(".p2").hide();
            });
        })
    </script>
</asp:Content>
