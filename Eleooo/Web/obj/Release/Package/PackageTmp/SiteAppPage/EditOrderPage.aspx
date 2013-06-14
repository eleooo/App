<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditOrderPage.aspx.cs" EnableViewState="false" Inherits="Eleooo.Web.SiteAppPage.EditOrderPage" %>

<%@ Import Namespace="Eleooo.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>回复</title>
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/json/json2.js" type="text/javascript"></script>
    <link href="/App_Themes/Admin/images/style.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/Admin/content.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/Admin/inc.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .msgContent { width: 98%; height: 50px; }
        .msgContent0 { width: 380px; }
        .tableData { width: 100%; border-collapse: collapse; border: 1px solid #000000; font-size: 14px; }
        .tableData td, .tableData th { border-collapse: collapse; border: 1px solid #000000; font-size: 14px; }
        .tableData1 td, .tableData th { border-collapse: collapse; border: 0px solid #000000; font-size: 14px; }
        .tableLabel { border-right: 1px solid #000000; width: 100px; text-align: center; display: inline-block; }
        .TheadLabel { width: 49%; display: inline-block; }
        span.gray { color: #999; font-size: 12px; font-style: normal; }
    </style>
</head>
<body>
    <div id="MenuContainer" class="menu-qie" style="width: 100%">
        <ul style="float: left;">
            <li class="selected" view="detailContainer"><a href="javascript:void(0)">餐点明细</a></li>
            <li view="msgContainer"><a href="javascript:void(0)">信息回复</a></li>
        </ul>
    </div>
    <div id="msgContainer" style="display: none;">
        <asp:Repeater ID="rpDetail" runat="server" EnableViewState="false">
            <HeaderTemplate>
                <table width="100%" cellspacing="1" cellpadding="2" border="0" align="center" class="tbl_bg">
                    <thead>
                        <tr>
                            <td colspan="4" style="background-color: Orange; font-weight: bold; text-align: left;">
                                餐点明细
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%">
                                餐点名称
                            </td>
                            <td>
                                价格
                            </td>
                            <td>
                                价格调整
                            </td>
                            <td>
                                是否缺货
                            </td>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr align="center" class="tbl_row">
                    <td <%# Utilities.ToBool(Eval("isCompanyItem")) ? "style='color:Red'" : ""%>>
                        <div style="overflow: hidden;">
                            <%# Eval("MenuName") %></div>
                    </td>
                    <td>
                        <%# Eval("OrderPrice")%>
                    </td>
                    <td>
                        <input type="text" onchange="orderMeal.changePrice(this,'<%#Eval("MenuId") %>');" style="width: 70px;" />
                    </td>
                    <td>
                        <input type="checkbox" <%# Convert.ToBoolean(Eval("IsOutOfStock"))?"checked='checked'":"" %> onclick="orderMeal.outOfStockItem(this,'<%#Eval("MenuId") %>');" style="width: 60px;" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
        <table id="tblReply" runat="server" visible="false" width="100%" cellspacing="1" cellpadding="3" border="0" class="tbl_body">
            <thead>
                <tr class="tbl_form_row">
                    <th colspan="2" style="background-color: Orange; font-weight: bold; text-align: left;">
                        回复信息
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr class="tbl_form_row">
                    <td style="width: 10%" align="center">
                        <input type="radio" name="msnType" id="cbMsn0" value="0" checked="checked" style="width: 50px;" />
                    </td>
                    <td>
                        <input type="text" id="msnMessage0" value="订单已确认，餐厅开始备餐。" class="msgContent0" style="width: 98%" />
                    </td>
                </tr>
                <tr class="tbl_form_row">
                    <td style="width: 10%" align="center">
                        <input type="radio" name="msnType" id="cbMsn1" value="1" style="width: 50px;" />
                    </td>
                    <td>
                        <input type="text" id="msnMessage1" class="msgContent0" value="抱歉，XXXXX今天暂缺，请修改后重新下单。" style="width: 98%" />
                    </td>
                </tr>
                <tr class="tbl_form_row">
                    <td style="width: 10%" align="center">
                        <input type="radio" name="msnType" value="1_1" id="cbMsn1_1" style="width: 50px;" />
                    </td>
                    <td>
                        <textarea id="msnMessage1_1" rows="2" cols="1" class="msgContent">经餐厅确认：XXXXX价格调整为XX元，您的订单总计为XX。</textarea>
                    </td>
                </tr>
                <tr class="tbl_form_row">
                    <td style="width: 10%" align="center">
                        <input type="radio" name="msnType" value="2" style="width: 50px;" id="cbMsn2" />
                    </td>
                    <td>
                        <input type="text" id="msnMessage2" class="msgContent0" value="抱歉，当前暂不外送，请选择其他餐厅。" style="width: 98%" />
                    </td>
                </tr>
                <tr class="tbl_form_row">
                    <td style="width: 10%" align="center">
                        <input type="radio" name="msnType" value="4" style="width: 50px;" id="cbMsn4" />
                    </td>
                    <td>
                        <input type="text" id="msnMessage4" class="msgContent0" value="订单已取消。" style="width: 98%" />
                    </td>
                </tr>
                <tr class="tbl_form_row">
                    <td style="width: 10%" align="center">
                        <input type="radio" name="msnType" value="3" style="width: 50px;" id="cbMsn3" />
                    </td>
                    <td>
                        <textarea id="msnMessage3" rows="2" cols="1" class="msgContent">自定义回复内容</textarea>
                    </td>
                </tr>
                <tr bgcolor="#f0f0f5" class="tbl_form_row">
                    <td bgcolor="#f0f0f5" align="center" colspan="2">
                        <input type="button" value="点击回复(S)" id="btnSubmit" style="line-height: 18px; width: 100px; height: 25px; font-size: 14px; font-weight: bold" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div id="lblMessage" runat="server">
        </div>
    </div>
    <div id="detailContainer">
        <table border="0" cellpadding="2" rules="all" cellspacing="0" class="tableData">
            <thead>
                <tr>
                    <th colspan="4">
                        餐点明细
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td colspan="4">
                        <span class="TheadLabel" style="border-right: 1px solid #000000; width: 27%">会员账号:<%=this.GetMemberPhone() %></span> <span class="TheadLabel" style="width: 69%;">商家电话:<%=this.GetCompanyPhone() %></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <span class="TheadLabel" style="border-right: 1px solid #000000;">下单:<%=this.Meal.OrderDate.ToString("HH:mm:ss") %></span> <span class="TheadLabel">计时:<label id="timer"><%=GetTimespan( )%></label></span>
                    </td>
                </tr>
                <asp:Repeater ID="rpOrderDetail" runat="server" EnableViewState="false">
                    <ItemTemplate>
                        <tr>
                            <td style="font-size: 12px; font-weight: bold;">
                                <%# Formatter.ReplaceQuote(Eval("DirName").ToString( ), "<span class='gray'>{0}</span>")%>
                            </td>
                            <td <%# Utilities.ToBool(Eval("isCompanyItem")) ? "style='color:Red'" : ""%>>
                                <%# Formatter.ReplaceQuote(Eval("MenuName").ToString( ), "<span class='gray'>{0}</span>")%>
                            </td>
                            <td>
                                <%# Eval("OrderQty")%>
                            </td>
                            <td>
                                <%# Eval("OrderPrice")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="4">
                        <table border="0" width="100%" class="tableData1">
                            <tr>
                                <td style="width: 100px;">
                                    <span class="tableLabel">总计</span>
                                </td>
                                <td>
                                    <span id="lblMealItem" runat="server">
                                        <%= Meal.OrderSumOk %>元,其中送餐费<%= Meal.ServiceSum %>元 </span><span id="lblCompanyItem" runat="server">花费<%=Meal.OrderPayPoint %>个积分抢购,需要支付<%= Meal.ServiceSum %>元送餐费
                                        </span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="font-weight: bold; background-color: orange;">
                    <td colspan="4">
                        <table border="0" width="100%" class="tableData1">
                            <tr>
                                <td style="width: 100px;">
                                    <span class="tableLabel">备注</span>
                                </td>
                                <td>
                                    <%=Meal.OrderMemo %>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table border="0" width="100%" class="tableData1">
                            <tr>
                                <td style="width: 100px;">
                                    <span class="tableLabel">地址</span>
                                </td>
                                <td>
                                    <%=GetMansionName() %>
                                    <input type="text" id="txtSeat" value="<%=AddrSeat %>" style="width: 40px;" />
                                    <input type="text" id="txtFloor" value="<%=AddrFloor %>" style="width: 50px;" />楼
                                    <input type="text" id="txtRoom" value="<%=AddrRoom %>" style="width: 120px;" />
                                    <input type="button" id="btnModifyAddr" value="修改" orderid="<%=Meal.Id %>" style="width: 80px;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    <script type="text/javascript">
        var OrderMeal = function (opts) {
            var Opts = $.extend({
                orderId: 0,
                orders: {},
                orderSum: 0,
                serviceSum: 0,
                companyName: '',
                orderSessionVal: 0
            }, opts);
            var chgPriceLog = {}, outOfStockLog = {}, isProcess = false;
            $("#btnSubmit").click(__sendMessage);
            $("#msnMessage3").focusin(function () {
                var val = $("#msnMessage3").val();
                if (val.indexOf('自定义回复内容') >= 0)
                    $("#msnMessage3").val("");
            }).change(function () {
                var val = $("#msnMessage3").val();
                if (val && val.length > 0)
                    _checkBoxItem("cbMsn3");
            });
            this.changePrice = function (el, menuId) {
                var NewPrice = parseFloat($(el).val()) || 0;
                var order = Opts.orders[menuId];
                if (order) {
                    NewPrice = NewPrice < 0 ? 0 : NewPrice;
                    order["NewPrice"] = NewPrice;
                    __appendChgPriceLog(menuId);
                    _checkBoxItem("cbMsn1_1");
                }
            };
            this.outOfStockItem = function (el, menuId) {
                var isOutOfStock = $(el).attr("checked");
                var order = Opts.orders[menuId];
                if (order) {
                    if (isOutOfStock)
                        order["IsOutOfStock"] = true;
                    else
                        order["IsOutOfStock"] = false;
                    __appendOutOfStockLog(menuId);
                    _checkBoxItem("cbMsn1");
                }

            };
            function _checkBoxItem(item) {
                $('input:radio[name="msnType"]:checked').attr("checked", false);
                $("#" + item).attr("checked", true);
            }
            function __appendChgPriceLog(menuId) {
                //XXXXX价格调整为XX元
                var order = Opts.orders[menuId];
                if (order) {
                    var NewPrice = order["NewPrice"];
                    if (NewPrice >= 0) {
                        chgPriceLog[menuId] = order.MenuName + "价格调整为" + NewPrice + "元";
                    } else if (chgPriceLog[menuId])
                        delete chgPriceLog[menuId];
                    __calcOrderSum();
                    __refreshMsnMessage();
                }
            };
            function __appendOutOfStockLog(menuId) {
                //抱歉，XXXXX今天暂缺，请修改后重新下单
                var order = Opts.orders[menuId];
                if (order) {
                    var isOutOfStock = order["IsOutOfStock"];
                    if (isOutOfStock) {
                        outOfStockLog[menuId] = order.MenuName;
                    } else if (outOfStockLog[menuId])
                        delete outOfStockLog[menuId];
                    __refreshMsnMessage();
                }
            };
            function __calcOrderSum() {
                var order;
                var sum = 0;
                var itemSum = 0;
                var price;
                for (var o in Opts.orders) {
                    order = Opts.orders[o];
                    price = order["NewPrice"] || order.OrderPrice;
                    if (order.IsCompanyItem && itemSum == 0) {
                        itemSum = order.OrderSum;
                    }
                    sum += (order.IsCompanyItem ? order.OrderQty - 1 : order.OrderQty) * price;
                }
                Opts.orderSum = __round(sum + itemSum, 2);
            };
            function __getChgPriceLog() {
                var log1 = [];
                for (var log in chgPriceLog) {
                    log1.push(chgPriceLog[log]);
                }
                return log1;
            };
            function __getOutOfStockLog() {
                var log2 = [];
                for (var log in outOfStockLog) {
                    log2.push(outOfStockLog[log]);
                }
                return log2;
            };
            function __round(dec, num) {
                var dd = 1;
                var tempnum;
                for (i = 0; i < num; i++) {
                    dd *= 10;
                }
                tempnum = dec * dd;
                tempnum = Math.round(tempnum);
                return tempnum / dd;
            };
            function __toObject(arg) {
                return JSON.parse(arg);
            };
            function __toJSON(obj) {
                return JSON.stringify(obj);
            };
            function __refreshMsnMessage() {
                var log1 = __getChgPriceLog();
                var log2 = __getOutOfStockLog();

                var content = "";
                if (log1.length > 0) {
                    content = "经餐厅确认：" + log1.join("，") + "，您的订单总计为" + (Opts.orderSum + Opts.serviceSum) + "元。";
                    $("#msnMessage1_1").val(content);
                }
                if (log2.length > 0) {
                    content = "很抱歉，" + log2.join("，") + "今天暂缺，请修改后重新下单。";
                    $("#msnMessage1").val(content);
                    $("#msnMessage1").attr("message", "很抱歉，" + Opts.companyName + "表示，" + log2.join("，") + "今天暂缺，请修改后重新下单。");
                }
            };

            function __sendMessage() {
                if (isProcess)
                    return;
                var type = $('input:radio[name="msnType"]:checked').val();
                if (type == "1" && __getChgPriceLog().length == 0 && __getOutOfStockLog().length == 0) {
                    alert("你还没有调整价信息或缺货信息.");
                    return;
                };
                var box = $("#msnMessage" + type);
                var message = box.attr("message") || box.val();
                if (!message || message.length == 0) {
                    alert("请输入回复内容.");
                    return;
                }
                if (type === '1_1')
                    type = '1';
                var args =
                {
                    orderId: Opts.orderId,
                    orderSessionVal: Opts.orderSessionVal,
                    msnType: type,
                    message: message,
                    orders: __toJSON(Opts.orders)
                };
                isProcess = true;
                $.ajax({
                    type: "POST",
                    url: "/Public/RestHandler.ashx/OrderMeal/SendMessage",
                    dataType: "json", data: args,
                    success: function (result) {
                        alert(result.message);
                        if (result.code == -2) {
                            document.location.reload();
                            return;
                        } else if (result.code == 0) {
                            Opts.orderSessionVal = result.data.orderSessionVal;
                        }
                        isProcess = false;
                    }
                });
            };
        };
        $(document).ready(function () {
            window["orderMeal"] = new OrderMeal(<%=this.GetInitData() %>);
            $("#MenuContainer ul>li").click(function () {
                var slView = $("#MenuContainer ul> .selected");
                if ($(this).attr("view") != slView.attr("view")) {
                    slView.removeClass("selected");
                    $("#" + slView.attr("view")).hide();
                    $(this).addClass("selected");
                    $("#" + $(this).attr("view")).show();
                }
                return false;
            });
            $("#btnModifyAddr").click(function () {
                var seat = $("#txtSeat").val();
                var room = $("#txtRoom").val();
                var floor = $("#txtFloor").val();
                var orderId = $(this).attr("orderid");
                if (!floor || floor.length == 0) {
                    alert("请输入送餐楼层信息.");
                    return;
                }
                if (!room || room.length == 0) {
                    alert("请输入送餐地址详细信息.");
                    return;
                }
                $.ajax({
                    type: "POST",
                    url: "/Public/RestHandler.ashx/Address/Edit",
                    dataType: "json", data: { OrderId: orderId, Floor: floor, Room: room, Seat: seat },
                    success: function (result) {
                        alert(result.message);
                    }
                });
            });
        });
    </script>
</body>
</html>
