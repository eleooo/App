<%@ Page Title="我要抢优惠-我的优惠" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="CompanyMyItems.aspx.cs" Inherits="Eleooo.Web.Member.CompanyMyItems" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainTjsj">
        <div class="czlist">
            <div class="czlistSearch">
                按消费日期：
                <input class="cz_txt" style="width: 90px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateStart" />
                &nbsp;至&nbsp;
                <input class="cz_txt" style="width: 90px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateEnd" />
                &nbsp;&nbsp;按消费状态：
                <asp:DropDownList runat="server" ID="rbStatus">
                    <asp:ListItem Text="全部" Value="0" Selected="True" />
                    <asp:ListItem Text="已抢购" Value="1" />
                    <asp:ListItem Text="已消费" Value="2" />
                    <asp:ListItem Text="已过期" Value="3" />
                </asp:DropDownList>
                <asp:Button runat="server" CssClass="czBtn" ID="btnSearch" Text="" />
            </div>
            <ele:DataListExt ID="view" runat="server" ShowFootPaging="true" ShowHeadPaging="true">
                <HeaderTemplate>
                    <table class="tbczlist" cellspacing="0" cellpadding="0">
                        <thead>
                            <tr>
                                <th style="width: 79px;">
                                    日期
                                </th>
                                <th style="width: 242px;">
                                    优惠描述
                                </th>
                                <th style="width: 137px;">
                                    商家名称
                                </th>
                                <th style="width: 62px;">
                                    原价
                                </th>
                                <th style="width: 62px;">
                                    积分兑换
                                </th>
                                <th style="width: 83px;">
                                    预计到店时间
                                </th>
                                <th>
                                    状态
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("OrderDate","{0:yyyy-MM-dd}") %>
                        </td>
                        <td>
                            <%#Eval("ItemTitle",0) %>
                        </td>
                        <td>
                            <%#Eval("CompanyName")%>
                        </td>
                        <td>
                            <%#Eval("ItemSum") %>
                        </td>
                        <td>
                            <%#Eval("ItemPoint","{0:###.###}") %>
                        </td>
                        <td>
                            <%#Eval("ItemDate","{0:yyyy-MM-dd}") %>
                        </td>
                        <td>
                            <%#Eval("ItemStatus",1) %>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="os">
                        <td>
                            <%#Eval("OrderDate","{0:yyyy-MM-dd}") %>
                        </td>
                        <td>
                            <%#Eval("ItemTitle",0) %>
                        </td>
                        <td>
                            <%#Eval("CompanyName")%>
                        </td>
                        <td>
                            <%#Eval("ItemSum") %>
                        </td>
                        <td>
                            <%#Eval("ItemPoint","{0:###.###}") %>
                        </td>
                        <td>
                            <%#Eval("ItemDate","{0:yyyy-MM-dd}") %>
                        </td>
                        <td>
                            <%#Eval("ItemStatus",1) %>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th class="ta_l fontNormal pl_10" colspan="7">
                            </th>
                        </tr>
                    </tfoot>
                    </table>
                </FooterTemplate>
            </ele:DataListExt>
        </div>
        <div id="orderLinkTemplate" runat="server" visible="false">
            <div style="text-align:  center">
                <a href="OrderCompanyItem.aspx?ItemID={0}&MemberItemID={1}">{2}</a>
            </div>
        </div>
        <div id="orderOutdateLinkTemplate" runat="server" visible="false">
            <div style="text-align: center">
                <a href="/Public/ViewItemDetail.aspx?ItemID={0}">{1}</a>
            </div>
        </div>
        <div id="orderedLinkTemplate" runat="server" visible="false">
            <div style="text-align: center">
                <a href="/Public/ViewItemDetail.aspx?ItemID={0}">{1}</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
    <script type="text/javascript">
        var showOutdateMsg = function () {
            alert("该优惠已过期");
        }
        var showOrderedMsg = function () {
            alert("该优惠已消费");
        }
    </script>
</asp:Content>
