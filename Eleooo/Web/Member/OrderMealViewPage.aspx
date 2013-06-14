<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="OrderMealViewPage.aspx.cs" Inherits="Eleooo.Web.Member.OrderMealViewPage" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<%@ Register TagPrefix="ele" TagName="UcFaceBookCompany" Src="~/Controls/UcFaceBookCompany.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <ele:ResLink ID="rs0" runat="server" Src="/App_Themes/ThemesV2/css/inc_2.css" ReplaceSrc="/App_Themes/ThemesV2/css/inc.css" />
    <ele:ResLink ID="rs1" runat="server" Src="/App_Themes/ThemesV2/css/content_2.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="main">
        <div class="kk">
            <div class="left" id="leftBox">
                <div class="dc-title">
                    <div class="dc-title-tu">
                        <img src='<%= this.Company.CompanyPhoto %>' alt="" width="138px" height="91px" /></div>
                    <div class="dc-title-w">
                        <span>
                            <%= FormatText(this.Company.CompanyName,"<b>{0}</b>") %></span>
                        <p>
                            地址：<%=this.Company.CompanyAddress %></p>
                        <p>
                            人气：<u class="s_yellow"><%=this.GetOrderCount() %></u>&nbsp;&nbsp; 点评：<a class="s_dp" id="faceBookCount"
                                href="javascript:void(0);"><%=this.GetFaceBookRateCount() %></a></p>
                        <p>
                            积分：<%=OrderMealRewardRate%></p>
                    </div>
                    <div class="dc-title-aniu">
                        <a href="javascript:void(0)" id="btnAddFav">
                            <img width="94" height="24" border="0" src="/App_Themes/ThemesV2/images/tjct.png"
                                alt="" id="favCompanyPic" runat="server" /></a>
                        <p>
                            菜单已于<%=this.GetMenuUpdateDate() %>月更新</p>
                    </div>
                </div>
                <ele:UcFaceBookCompany ID="faceBook" runat="server" />
                <div class="cd-lb" id="menuBox">
                    <asp:Repeater ID="rpMenuDir" runat="server" EnableViewState="false" OnItemDataBound="rpMenuDir_ItemDataBound">
                        <HeaderTemplate>
                            <ul id="menuContainer">
                        </HeaderTemplate>
                        <FooterTemplate>
                            </ul></FooterTemplate>
                        <ItemTemplate>
                            <li><a href="javascript:void(0)" onclick="orderMeal.changeMenuDir(<%#Eval("Key.Key") %>)">
                                <%# FormatMenuDirtext(Eval("Key.Value")) %></a>
                                <ul id="dir_<%#Eval("Key.Key") %>" <%# GetDirAttribute(Eval("Key.Key"),Container.ItemIndex) %>>
                                    <asp:Repeater ID="rpMenu" runat="server" EnableViewState="false">
                                        <ItemTemplate>
                                            <li class="<%#Eval("menuCss") %>">
                                                <%#Eval("menuAmout") %>
                                                <a href="javascript:void(0)">
                                                    <%# FormatMenutext(Eval("MenuName")) %></a> <span>
                                                        <%#Eval("MenuPrice")%></span></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="right" id="rightBox">
                <div class="xd">
                    <div class="xd-title">
                        <span>我的订单</span></div>
                    <div class="xd-main">
                        <div class="my_order">
                            <asp:Repeater ID="rpDetail" runat="server" EnableViewState="false">
                                <HeaderTemplate>
                                    <ul class="order_list">
                                </HeaderTemplate>
                                <FooterTemplate>
                                    </ul></FooterTemplate>
                                <ItemTemplate>
                                    <li><span class="menu_name">
                                        <%#Formatter.SubStr( Eval("MenuName").ToString(),0,10,false) %></span>
                                        <div class="menuRig">
                                            <span>￥<%# Convert.ToDecimal( Eval("OrderQty")) * Convert.ToDecimal( Eval("OrderPrice"))%></span></div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="xd-nr1">
                            <div class="sc">
                                <ul>
                                    <li class="input04">
                                        <input type="text" style="width: 100px;" value="总计：66元" name="" readonly="readonly"
                                            id="disOrderSum" />
                                    </li>
                                    <li class="input05">
                                        <input type="text" style="width: 200px;" value="您本次订餐获赠0.33个积分" name="" id="disOrderPoint"
                                            readonly="readonly" /></li>
                                    <li>
                                        <div style="color: rgb(30, 141, 168); overflow: hidden; zoom: 1;">
                                            <div style="width: 25px; float: left;">
                                                <img src="/App_Themes/ThemesV2/images/lab.png" alt="" /></div>
                                            <div style="width: 260px; float: right;">
                                                <span style="line-height: 24px;">您的餐点预计<label id="disTimeout">预计35分钟</label>分钟左右送达。</span></div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="zfyx" id="statusContainer">
                        </div>
                    </div>
                </div>
                <div class="ssn">
                    <div class="ssn-title">
                        <span>碎碎念</span></div>
                    <div class="dcxxt-main" id="firstOrder" runat="server">
                        <p>
                            首次订餐后，如果您愿意，将成为我们的会员。下次订餐您可以直接登录，系统会自动记录您的送餐地址和联系方式。</p>
                        <p>
                            登录账号是您的手机号码，初始密码为手机后6位数，记得及时修改密码哦！</p>
                    </div>
                    <div class="dcxxt-main" id="afterFirstOrder" runat="server">
                        <p>
                            您的订单已经在及时处理中，请留意查看最新返馈 的送餐进度。
                        </p>
                        <p>
                            趁着等餐的时间，不如去看看附近有哪些商家提供的优惠，或者看看广告，赚点积分吧！
                        </p>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
    <script type="text/javascript">
        var OrderMeal = function (opts) {
            var Opts = $.extend({
                companyId: 0,
                mansionId: 0,
                orderId: 0,
                orderSum: 0,
                orderPointMsg: "",
                orderPoint: 0,
                orderTimeout: "",
                orderStatus: 0
            }, opts);
            var curDir, statusContainer, isProgress = false;
            this.changeMenuDir = function (dirId) {
                var dir = $("#dir_" + dirId);
                var isShow = dir.attr("show") === 'true';
                $("#menuContainer > li > ul[show='true']").hide().attr('show', 'false');
                if (!isShow)
                    dir.show().attr('show', 'true');
                //                if (!curDir) {
                //                    curDir = $("#menuContainer > li > ul").first();
                //                }
                //                if (curDir.attr("id") == id ||
                //                                curDir.attr("show") === true ||
                //                                curDir.attr("show") === undefined) {
                //                    if (curDir.attr("show") === true || curDir.attr("show") === undefined) {
                //                        curDir.hide().attr("show", false);
                //                        if (curDir.attr("id") == id)
                //                            return;
                //                    }
                //                    else {
                //                        curDir.show().attr("show", true)
                //                        return;
                //                    }
                //                }
                //                curDir = $("#" + id).show().attr("show", true);
            };
            //修改订单
            this.editOrder = function () {
                __navigateOrderPage();
            };
            //取消订单
            this.cancelOrder = function () {
                __cancelOrder(function () {
                    //alert("订单已经取消.");
                    setTimeout(__navigateCompanyPage, 2000);
                });
            };
            //重新下单
            this.redoOrder = function () {
                // __cancelOrder(__navigateCompanyPage);
                __navigateOrderPage();
            };
            //不外送
            this.nonOutCompany = function () {
                __navigateSelectCompanyPage();
            };
            //帮忙催一下
            this.ugrentOrder = function () {
                $.ajax({
                    type: "POST",
                    url: "/Public/OrderMealServices.asmx/UgrentOrder",
                    dataType: "xml", data: { orderId: Opts.orderId },
                    success: function (xml) {
                        var result = __toObject($(xml).text());
                        if (result.code >= 0)
                        //alert("Ok,我们会尽快的了!");
                            return;
                        else
                            alert(result.message);
                    }
                });
            };
            //已经收到了
            this.completeOrder = function () {
                $.ajax({
                    type: "POST",
                    url: "/Public/OrderMealServices.asmx/CompleteOrder",
                    dataType: "xml", data: { orderId: Opts.orderId },
                    success: function (xml) {
                        var result = __toObject($(xml).text());
                        if (result.code >= 0)
                        //alert("谢谢，欢迎下次惠顾!");
                            return;
                        else
                            alert(result.message);
                    }
                });
            };

            function __addFavCompany() {
                if (!Opts.companyId || Opts.companyId <= 0) {
                    alert("当前商家不合法");
                    return;
                }
                if (isProgress)
                    return;
                isProgress = true;
                $.ajax({
                    type: "POST",
                    url: "/Public/OrderMealServices.asmx/AddFavCompany",
                    dataType: "xml", data: { companyId: Opts.companyId },
                    success: function (xml) {
                        var result = __toObject($(xml).text());
                        isProgress = false;
                        if (result.code == 0) {
                            $("#<%=favCompanyPic.ClientID%>").attr("src", "/App_Themes/ThemesV2/images/cwdctsc.png");
                        } else if (result.code == 1) {
                            $("#<%=favCompanyPic.ClientID%>").attr("src", "/App_Themes/ThemesV2/images/tjct.png");
                        }
                    }
                });
            };
            function __cancelOrder(onSuccess) {
                $.ajax({
                    type: "POST",
                    url: "/Public/OrderMealServices.asmx/CancelOrder",
                    dataType: "xml", data: { orderId: Opts.orderId },
                    success: function (xml) {
                        var result = __toObject($(xml).text());
                        if (result.code >= 0)
                            onSuccess();
                        else
                            alert(result.message);
                    }
                });
            };
            function __renderStatus() {
                if (isProgress)
                    return;
                isProgress = true;
                $.ajax({
                    type: "POST",
                    url: "/Public/OrderMealServices.asmx/RenderOrderProgress",
                    dataType: "xml", data: { orderId: Opts.orderId },
                    success: function (xml) {
                        var result = $(xml).text();
                        isProgress = false;
                        if (result.charAt(0) == '\n') {
                            statusContainer.html(result);
                        } else if (result.charAt(0) == '1') {
                            document.location.reload();
                            return;
                        }
                        else {
                            var o = __toObject(result);
                            alert(result.message);
                        }
                        if ($("#OrderCanceled").length > 0) {
                            //setTimeout(__navigateCompanyPage, 2000);
                        } else if (Opts.orderStatus == 1)
                            setTimeout(__renderStatus, 2000);
                        $("#leftBox").css("min-height", $("#rightBox").height());
                    }
                });
            };
            function __navigateSelectCompanyPage() {
                window.location.href = "/Public/OrderMealPage.aspx?MansionId=" + Opts.mansionId;
            };
            function __navigateCompanyPage() {
                window.location.href = "/Public/OrderMealPage.aspx?CompanyId=" + Opts.companyId + "&MansionId=" + Opts.mansionId;
            };
            function __navigateOrderPage() {
                window.location.href = "/Public/OrderMealPage.aspx?OrderId=" + Opts.orderId;
            };
            function __toObject(arg) {
                return JSON.parse(arg);
            };
            function __toJSON(obj) {
                return JSON.stringify(obj);
            };
            function __HTMLDeCode(str) {
                var s = "";
                if (str.length == 0) return "";
                s = str.replace(/&gt;/g, "&");
                s = s.replace(/&lt;/g, "<");
                s = s.replace(/&gt;/g, ">");
                s = s.replace(/&nbsp;/g, "    ");
                s = s.replace(/'/g, "\'");
                s = s.replace(/&quot;/g, "\"");
                s = s.replace(/<br>/g, "\n");
                return s;
            };
            function __init() {
                $("#btnAddFav").click(__addFavCompany);
                $("#disOrderSum").val("总计：" + Opts.orderSum + "元");
                $("#disOrderPoint").val(Opts.orderPointMsg);
                $("#disTimeout").text(Opts.orderTimeout);
                statusContainer = $("#statusContainer");
                __renderStatus();
            };
            __init();
        };
    </script>
    <script type="text/javascript">
    $(document).ready(function () {
        window["orderMeal"] = new OrderMeal(<%=this.GetInitData() %>);
        FaceBook.init();
    });
    </script>
</asp:Content>
