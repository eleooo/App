<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="OrderDetail.aspx.cs" Inherits="Eleooo.Web.SiteAppPage.OrderDetail" %>

<%@ Import Namespace="Eleooo.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>餐点明细</title>
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/json/json2.js" type="text/javascript"></script>
    <style type="text/css">
        .tableData { width: 100%; border-collapse: collapse; border: 1px solid #000000; }
        .tableData td, .tableData th { border-collapse: collapse; border: 1px solid #000000; }
        .tableData1 td, .tableData th { border-collapse: collapse; border: 0px solid #000000; }
        .tableLabel { border-right: 1px solid #000000; width: 100px; text-align: center; display: inline-block; }
        .TheadLabel { width: 49%; display: inline-block; }
        span.gray { color: #999; font-size: 12px; font-style: normal; }
    </style>
</head>
<body>
    <div>
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
                <asp:Repeater ID="rpDetail" runat="server" EnableViewState="false">
                    <ItemTemplate>
                        <tr>
                            <td style="font-size: 12px; font-weight: bold;">
                                <%# Formatter.ReplaceQuote(Eval("DirName").ToString( ), "<span class='gray'>{0}</span>")%>
                            </td>
                            <td <%# Utilities.ToBool(Eval("isCompanyItem")) ? "style='color:Red'" : ""%> >
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
                        </table>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
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
