<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="ViewItemDetail.aspx.cs" Inherits="Eleooo.Web.Public.ViewItemDetail" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="eleooo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <eleooo:ResLink ID="rs1" runat="server" Src="/App_Themes/ThemesV2/css/sub.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="sub_con">
        <div class="left">
            <div class="left_wrap">
                <div class="share">
                    <!-- Baidu Button BEGIN -->
                    <div id="bdshare" class="bdshare_t bds_tools get-codes-bdshare">
                        <span class="bds_more">分享到：</span> <a class="bds_tsina">新浪微博</a> <a class="bds_qzone">
                            QQ空间</a> <a class="bds_tqq">腾讯微博</a> <a class="bds_renren">人人网</a> <a class="bds_kaixin001">
                                开心网</a>
                    </div>
                    <script id="bdshare_js" type="text/javascript" data="type=tools&amp;uid=0"></script>
                    <script id="bdshell_js" type="text/javascript"></script>
                    <script type="text/javascript">
                        document.getElementById("bdshell_js").src = "http://bdimg.share.baidu.com/static/js/shell_v2.js?cdnversion=" + Math.ceil(new Date() / 3600000);
</script>
                    <!-- Baidu Button END -->
                </div>
                <div class="left_wrap_in">
                    <div class="title">
                        【限<%=Item.AreaDepth %><%=AreaTag %>会员】<%--仅<span class="yellow"><%=Item.ItemPoint.Value.ToString("#####.###")%></span>个乐多分，--%><%=Item.ItemTitle %>
                    </div>
                    <div class="yh_intro">
                        <div class="left">
                            <div class="show_price">
                                <input class="qian_btn" type="button" onclick="window.location.href='/Member/OrderCompanyItem.aspx?ItemID=<%=ItemID %>';" /><span
                                    class="yellow">￥<%=Item.ItemSum.Value.ToString("#####.###") %></span><br />
                                仅<span class="gray"><%=Item.ItemPoint.Value.ToString("#####.###")%></span>个乐多分即可抢购</div>
                            <ul>
                                <li><span class="pic">
                                    <img alt="" src="/App_Themes/ThemesV2/images/clock.png" /></span>剩余时间：<span class="purple">
                                        <%=RemindDay %>
                                    </span>天</li>
                                <li><span class="pic">
                                    <img alt="" src="/App_Themes/ThemesV2/images/Users.png" /></span>已抢购<span class="purple">
                                        <%=Item.ItemClicked %>
                                    </span>个,剩<span class="purple">
                                        <%=Item.ItemAmount - Item.ItemClicked %>
                                    </span>个</li>
                                <li><span class="pic">
                                    <img alt="" src="/App_Themes/ThemesV2/images/tui.png" /></span><span class="purple"><%=ItemCanDelInfo %></span></li>
                            </ul>
                        </div>
                        <div class="right">
                            <a href="/Member/OrderCompanyItem.aspx?ItemID=<%=ItemID %>">
                                <%=RenderItemImage(0) %>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="left_wrap">
                <div class="leftTop">
                    优惠详情</div>
                <div class="left_wrap_in">
                    <%=RenderContentToLi(Item.ItemContent) %>
                    <%=RenderItemImageTitle(1) %>
                    <p><%=RenderItemImage(1) %></p>
                    <%=RenderItemImageTitle(2) %>
                    <p><%=RenderItemImage(2) %></p>
                    <%=RenderItemImageTitle(3) %>
                    <p><%=RenderItemImage(3) %></p>
                    <%=RenderItemImageTitle(4) %>
                    <p><%=RenderItemImage(4) %></p>
                    <%=RenderItemImageTitle(5) %>
                    <p><%=RenderItemImage(5) %></p>
                </div>
            </div>
            <div class="left_wrap">
                <div class="leftTop">
                    商家简介</div>
                <div class="left_wrap_in">
                    <%=FormatCompanyContent()%>
                    <%=RenderItemImageTitle(6) %>
                    <p><%=RenderItemImage(6) %></p>
                    <%=RenderItemImageTitle(7) %>
                    <p><%=RenderItemImage(7) %></p>
                </div>
            </div>
            <div class="left_wrap">
                <div class="leftTop">
                    消费指南</div>
                <div class="left_wrap_in warm">
                    <%=RenderContentToLi(Item.ItemIntro) %>
                </div>
            </div>
            <div class="left_wrap">
                <div class="leftTop">
                    温馨提示</div>
                <div class="left_wrap_in warm">
                    <%=RenderContentToLi(Item.ItemTips) %>
                </div>
            </div>
            <eleooo:DataListExt ID="view_ItemInfo" runat="server" AllowPaging="true" ShowHeadPaging="false"
                EmptyDataIsShowHeaderAndFooterTemplate="false" ShowFootPaging="true" FooterPagingTemplate="~/Controls/UcFooterPaging1.ascx">
                <HeaderTemplate>
                    <div class="left_wrap">
                        <div class="leftTop">
                            看看抢购情况</div>
                        <div class="qk_wrap">
                            <ul>
                                <li class="qk_title">
                                    <div class="d1">
                                        会员帐号</div>
                                    <div class="d2">
                                        优惠描述</div>
                                    <div class="d3">
                                        成交时间</div>
                                    <div class="d4">
                                        状态</div>
                                </li>
                </HeaderTemplate>
                <FooterTemplate>
                    </ul></div></div>
                </FooterTemplate>
                <ItemTemplate>
                    <li>
                        <div class="d1">
                            <%#Eval("MemberPhoneNumber",0) %></div>
                        <div class="d2">
                            <%#Eval("ItemTitle",1) %></div>
                        <div class="d3">
                            <%#Eval("OrderDate", "{0:yyyy-MM-dd HH:mm}")%></div>
                        <div class="d4">
                            <%#Eval("ItemStatus",2) %></div>
                    </li>
                </ItemTemplate>
            </eleooo:DataListExt>
            <div class="left_wrap" id="sjplTitleContainer" runat="server" visible="false">
                <div class="leftTop">
                    看看最新评论</div>
                <div class="sjpl">
                    <eleooo:DataListExt ID="view_Facebook" runat="server" AllowPaging="true" ShowFootPaging="true"
                        ShowHeadPaging="false" EmptyDataIsShowHeaderAndFooterTemplate="false" FooterPagingTemplate="~/Controls/UcFooterPaging1.ascx">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <FooterTemplate>
                            </ul></FooterTemplate>
                        <ItemTemplate>
                            <li>
                                <div>
                                    <span class="c_fb7c04">会员：<%#Eval("MemberPhoneNumber",0) %></span>
                                </div>
                                <p>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <%#Eval("FaceBookMemo",3) %>
                                </p>
                                <div class="fr_time">
                                    <%#Eval("FaceBookDate","{0:yyyy年MM月dd日 HH:mm}")%></div>
                            </li>
                        </ItemTemplate>
                    </eleooo:DataListExt>
                    <div class="sjdp" id="sjdpContainer" runat="server">
                        <h3>
                            我要点评</h3>
                        <textarea class="txt_dp" name="txtFaceBook" rows="2" cols="2"></textarea>
                        <span class="c_0896ae txt_dp_tipp">50-1000字</span>
                        <div>
                            <input type="button" class="sjplsubmit" value="提 交" onclick="__doPostBack('Add');" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="want_q">
                <div class="q_left">
                    ￥<%=Item.ItemSum.Value.ToString("#####.###") %>元
                </div>
                <div class="q_right">
                    <a class="q_btn" href="/Member/OrderCompanyItem.aspx?ItemID=<%=ItemID %>">我要抢&gt;&gt;</a>
                    <p>
                        仅<span><%=Item.ItemPoint.Value.ToString("#####.###")%></span>个乐多分即可抢购</p>
                    <p>
                        【限<%=Item.AreaDepth %><%=AreaTag %>会员】
                    </p>
                </div>
            </div>
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
                <eleooo:DataListExt ID="view_RecItem" runat="server" AllowPaging="false" EmptyDataIsShowHeaderAndFooterTemplate="false">
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
                                        <%#Eval("ItemTitle",4) %></a></div>
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
    <!--[if IE 6]>
<script type="text/javascript" src="script/DD_belatedPNG.js"></script>
<script language="javascript" type="text/javascript">
               DD_belatedPNG.fix(".todayTopDetailL,h5");
</script>
<![endif]-->
</asp:Content>
