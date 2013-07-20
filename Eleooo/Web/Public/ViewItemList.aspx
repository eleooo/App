<%@ Page Title="我要抢优惠" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="ViewItemList.aspx.cs" Inherits="Eleooo.Web.Public.ViewItemList" %>

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
                ShowHeadPaging="false" PageSize="24" EmptyDataIsShowHeaderAndFooterTemplate="false" FooterPagingTemplate="~/Controls/UcFooterPaging1.ascx">
                <HeaderTemplate>
                    <ul class="zjf_list">
                </HeaderTemplate>
                <FooterTemplate>
                    </ul></FooterTemplate>
                <ItemTemplate>
                    <li><a href="ViewItemDetail.aspx?ItemID=<%#Eval("[ItemID]") %>">
                        <img alt="" src="<%#Eval("[ItemPic]") %>" /></a>
                        <p>
                            <a href="ViewItemDetail.aspx?ItemID=<%#Eval("[ItemID]") %>">
                                <%#Eval("[ItemTitle]",0) %></a></p>
                        <dl>
                            <dt><span class="b_yellow">
                                <%#Eval("[ItemPoint]") %>分</span>兑换&#12288;<span class="money">￥</span><%#Eval("[ItemSum]") %><br />
                                <span class="yellow">限<%#Eval("[ItemArea]") %>会员</span></dt>
                            <dd>
                                <a class="wa_link" href="ViewItemDetail.aspx?ItemID=<%#Eval("[ItemID]") %>">我要抢</a>已抢购<span
                                    class="yellow"><%#Eval("[ItemClicked]") %></span> 剩<span class="yellow"><%#Eval("[ItemUnClicked]") %></span>个</dd>
                        </dl>
                    </li>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <li class="no_Mar"><a href="ViewItemDetail.aspx?ItemID=<%#Eval("[ItemID]") %>">
                        <img alt="" src="<%#Eval("[ItemPic]") %>" /></a>
                        <p>
                            <a href="ViewItemDetail.aspx?ItemID=<%#Eval("[ItemID]") %>">
                                <%#Eval("[ItemTitle]",0) %></a></p>
                        <dl>
                            <dt><span class="b_yellow">
                                <%#Eval("[ItemPoint]") %>分</span>兑换&#12288;<span class="money">￥</span><%#Eval("[ItemSum]") %><br />
                                <span class="yellow">限<%#Eval("[ItemArea]") %>会员</span></dt>
                            <dd>
                                <a class="wa_link" href="ViewItemDetail.aspx?ItemID=<%#Eval("[ItemID]") %>">我要抢</a>已抢购<span
                                    class="yellow"><%#Eval("[ItemClicked]") %></span> 剩<span class="yellow"><%#Eval("[ItemUnClicked]") %></span>个</dd>
                        </dl>
                    </li>
                </AlternatingItemTemplate>
            </eleooo:DataListExt>
        </div>
        <div class="right">
            <div class="r_p">
                <a href="#">
                    <img alt="" src="/App_Themes/ThemesV2/images/eloo_01.jpg" /></a></div>
            <div class="r_p">
                <a href="#">
                    <img alt="" src="/App_Themes/ThemesV2/images/eloo_02.jpg" /></a></div>
            <div class="r_p">
                <a href="#">
                    <img alt="" src="/App_Themes/ThemesV2/images/eloo_03.jpg" /></a></div>
            <div class="r_intro">
                <h2>
                    <a class="qk_link" href="javascript:void(0);" onclick="__doPostBack('Delete');">清空</a>最近浏览过</h2>
                <eleooo:DataListExt ID="viewRecItem" runat="server" AllowPaging="false" EmptyDataIsShowHeaderAndFooterTemplate="false">
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
                        <li><a class="tray_pic" href="ViewItemDetail.aspx?ItemID=<%#Eval("ItemID") %>">
                            <img alt="" src="<%#Eval("ItemPic") %>" /></a>
                            <div class="tray_right">
                                <div>
                                    <a href="ViewItemDetail.aspx?ItemID=<%#Eval("ItemID") %>">
                                        <%#Eval("ItemTitle",1) %></a></div>
                                <div class="dh_div">
                                    <span class="yellow">
                                        <%#Eval("ItemPoint") %></span>分兑换<span class="money">￥</span><%#Eval("ItemSum") %></div>
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
                            <b style="font-family: 宋体;">·</b>抢优惠后可以直接去消费么？</p>
                        <p class="p2" style="display: none;">
                            是的。抢购成功后，即可在选定的时间直接到店体验。</p>
                    </li>
                    <li>
                        <p>
                            <b style="font-family: 宋体;">·</b>到店时如何验证？</p>
                        <p class="p2" style="display: none;">
                            到店后，在收银处输入您的账号（手机号码）和密码（登录密码）进行验证即可。</p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>真的没有隐性消费吗？</p>
                        <p class="p2" style="display: none;">
                            乐多分所有合作商家均经过严格审核并签约保障，绝无任何隐性消费。</p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>需要提前电话预约么？
                        </p>
                        <p class="p2" style="display: none;">
                            不用。抢优惠时只需选定预计到店时间即可，如遇客满，商家会提前致电与您沟通。</p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>抢了优惠可以退订么？</p>
                        <p class="p2" style="display: none;">
                            可否退订以商家的提示为准。如果支持退订，您的积分会实时返还到账户中。
                        </p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>抢的优惠过期了怎么办？</p>
                        <p class="p2" style="display: none;">
                            如果在预定的时间未到店消费，您可以尝试与商家沟通，如征得同意，则可另择到店时间。</p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>预计到店时间可以修改吗？</p>
                        <p class="p2" style="display: none;">
                            可以修改一次。请尽量安排好您的时间，以便享受商家提供更周到的服务。</p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>在店内的其他消费也送积分吗？</p>
                        <p class="p2" style="display: none;">
                            凡是已经开通积分奖励的商家，您在店内的其他消费，均给予积分返馈。
                        </p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>所有乐多分合作商家都送积分吗？
                        </p>
                        <p class="p2" style="display: none;">
                            乐多分合作的所有快餐店，以及“我的商圈”里带有积分标识的商家，均提供积分返馈。</p>
                    </li>
                    <li>
                        <p class="p1">
                            <b style="font-family: 宋体;">·</b>如果遇到问题怎么办？
                        </p>
                        <p class="p2" style="display: none;">
                            如遇任何问题，请致电400-080-9095，乐多分客服会在第一时间为您提供满意的服务。
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
