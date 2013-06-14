<%@ Page Title="积分明细" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="FinanceList.aspx.cs" Inherits="Eleooo.Web.Member.FinanceList" %>

<%@ Import Namespace="Eleooo.Web" %>
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
                &nbsp; 
                按收支情况：
                <asp:DropDownList runat="server" ID="rblFlag" RepeatLayout="Flow" RepeatColumns="5">
                    <asp:ListItem Selected="True" Text="全部" Value="0"></asp:ListItem>
                    <asp:ListItem Text="已收入" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已支出" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button runat="server" CssClass="czBtn" ID="btnSearch" Text="" />
            </div>
            <ele:DataListExt ID="view" runat="server" AllowPaging="true" ShowFootPaging="true"
                ShowHeadPaging="true" EmptyDataIsShowHeaderAndFooterTemplate="true">
                <HeaderTemplate>
                    <table class="tbczlist" cellspacing="0" cellpadding="0">
                        <thead>
                            <tr>
                                <th>
                                    日期
                                </th>
                                <th>
                                    积分信息
                                </th>
                                <th>
                                    备注
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th class="ta_l fontNormal pl_10" colspan="3">
                                <span class="fontBold">说明：</span><%#Eval() %>
                            </th>
                        </tr>
                    </tfoot>
                    </table>
                </FooterTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("PaymentDate","{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%#Eval("PaymentMemo")%>
                        </td>
                        <td>
                            <%#Eval("PaymentSum")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="os">
                        <td>
                            <%#Eval("PaymentDate","{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%#Eval("PaymentMemo")%>
                        </td>
                        <td>
                            <%#Eval("PaymentSum")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </ele:DataListExt>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
</asp:Content>
