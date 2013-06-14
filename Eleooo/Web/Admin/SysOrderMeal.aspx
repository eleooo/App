<%@ Page Title="订餐管理" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="SysOrderMeal.aspx.cs" Inherits="Eleooo.Web.Admin.SysOrderMeal" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
    <link href="/Scripts/jquery.tipswindown/jquery.tipswindown.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jquery.tipswindown/jquery.tipswindown.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMemberInfo" runat="server">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px">
                    <uc:UcPagePosition ID="pagePos" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                    <%=ResBLL.GetRes("searchBox_title","按条件搜索：","搜索框标题") %>
                    <label>
                        <%=ResBLL.GetRes("searchBox_beginDate","开始日期：","搜索框开始日期") %></label>
                    <input class="txtDate" onclick="WdatePicker()" runat="server" id="txtDateStart" type="text" />
                    <label>
                        <%=ResBLL.GetRes("searchBox_endDate","结束日期：","搜索框结束日期") %></label>
                    <input class="txtDate" onclick="WdatePicker()" runat="server" id="txtDateEnd" type="text" />
                    &nbsp;&nbsp;
                    <asp:RadioButtonList runat="server" ID="rbModel" RepeatLayout="Flow" RepeatColumns="2">
                        <asp:ListItem Text="人工" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem  Text="自动" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:CheckBox ID="cbViewAll" runat="server" Checked="false" Text="显示所有客服订单" />
                    <br />
                    <label>
                        <%=ResBLL.GetRes("searchBox_companyPhone","商家账号：","搜索框商家账号") %></label>
                    <input type="text" runat="server" id="txtCompanyName" />
                    <label>
                        <%=ResBLL.GetRes("searchBox_memberPhone","会员账号：","搜索框会员账号") %></label>
                    <input runat="server" id="txtMemberPhone" type="text" />
                    &nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnSearch" Text="查找(F)" />
                    <br />
                    <asp:RadioButtonList runat="server" ID="rbStatus" RepeatLayout="Flow" RepeatColumns="6">
                        <asp:ListItem Selected="True" Text="全部" Value="0"></asp:ListItem>
                        <asp:ListItem Text="待处理" Value="2"></asp:ListItem>
                        <asp:ListItem Text="已修改" Value="3"></asp:ListItem>
                        <asp:ListItem Text="处理中" Value="4"></asp:ListItem>
                        <asp:ListItem Text="已取消" Value="5"></asp:ListItem>
                        <asp:ListItem Text="订餐成功" Value="6"></asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <label>
                        自动刷新时间:</label>
                    <input type="text" id="txtSeconds" runat="server" size="2" value="5" style="width: 80px;" />秒(0为不自动刷新)
                </td>
            </tr>
        </tbody>
    </table>
    <div id="viewContainer">
        <uc:UcGridView ID="gridView" runat="server" AllowPaper="true" ShowHeader="true" />
    </div>
    <div id="lblMessage" style="color: Red;">
    </div>
    <script type="text/javascript">
        var OrderMeal = function () {
            var container = "#viewContainer";
            var lblMessage = "#lblMessage";
            var cboPage = "#cboPage";
            var txtPageIndex = "#txtPageIndex";
            var rbStatus = "input:radio[name='<%=rbStatus.UniqueID %>']:checked";
            var txtMemberPhone = "#<%=this.txtMemberPhone.ClientID %>";
            var txtCompanyName = "#<%=this.txtCompanyName.ClientID %>";
            var txtSeconds = "#<%=this.txtSeconds.ClientID %>";
            var detailUrl = "/SiteAppPage/OrderDetail.aspx?OrderId=";
            var editorUrl = "/SiteAppPage/EditOrderPage.aspx?OrderId=";
            var statusUrl = "/SiteAppPage/ViewOrderStatus.aspx?OrderId=";
            var rbModel = "input:radio[name='<%=rbModel.UniqueID %>']:checked";
            var txtDateStart = "#<%=this.txtDateStart.ClientID %>";
            var txtDateEnd = "#<%=this.txtDateEnd.ClientID %>";
            var cbViewAll = "#<%=this.cbViewAll.ClientID %>";
            var isBusy = false;
            this.changeStatus = function (el, orderId) {
                var status = $(el).val();
                if (!status) {
                    alert("请选择正确的状态.");
                    return;
                }
                $.ajax({
                    type: "POST",
                    url: "/Public/OrderMealServices.asmx/ChangeOrderStatus",
                    dataType: "xml", data: { orderId: orderId, nStatus: status },
                    success: function (xml) {
                        var result = __toObject($(xml).text());
                        alert(result.message);
                        $(lblMessage).html(result.message);
                    }
                });
            };
            this.popupOrderDetail = function (orderId) {
                isBusy = true;
                tipsWindown("餐点明细", "iframe:" + detailUrl + orderId, 650, 350, "true", "", "true", "", orderMeal.dlgCallBack);
            };
            this.popupOrderEditor = function (orderId) {
                isBusy = true;
                tipsWindown("回复信息", "iframe:" + editorUrl + orderId, 600, 540, "true", "", "true", "", orderMeal.dlgCallBack);
            };
            this.popupStatus = function (orderId) {
                isBusy = true;
                tipsWindown("订餐进度", "iframe:" + statusUrl + orderId, 370, 370, "true", "", "true", "", orderMeal.dlgCallBack);
            }
            this.dlgCallBack = function () {
                isBusy = false;
                renderView();
            }
            function renderView() {
                if (isBusy)
                    return;
                var seconds = parseInt($(txtSeconds).val());
                if (!seconds || seconds <= 0) {
                    setTimeout(renderView, 2000);
                    return;
                }
                var cbViewAllObj = $(cbViewAll);
                var isViewAll = cbViewAllObj.length > 0 && cbViewAllObj.attr("checked");
                
                var data =
                {
                    txtCompany: $(txtCompanyName).val(),
                    txtMember: $(txtMemberPhone).val(),
                    rbStatus: $(rbStatus).val(),
                    rbModel: $(rbModel).val(),
                    txtDateStart: $(txtDateStart).val(),
                    txtDateEnd: $(txtDateEnd).val(),
                    cboPage: $(cboPage).val(),
                    txtPageIndex: $(txtPageIndex).val(),
                    isViewAll: isViewAll
                };
                $.ajax({
                    type: "POST",
                    url: "/Public/OrderMealServices.asmx/QueryOrderMeal",
                    dataType: "xml", data: data,
                    success: function (xml) {
                        var result = $(xml).text();
                        if (result.charAt(0) == '{') {
                            var error = __toObject(result);
                            $(lblMessage).html(error.message);
                            alert(error.message);
                        } else {
                            $(container).html($(result).find("#container").html());
                        }
                        setTimeout(renderView, (seconds * 1000));
                    }
                });
            };
            function __toObject(arg) {
                return JSON.parse(arg);
            };
            function __toJSON(obj) {
                return JSON.stringify(obj);
            };
            setTimeout(renderView, 2000);
        };
        $(document).ready(function () {
            window["orderMeal"] = new OrderMeal();
        });
    </script>
</asp:Content>
