<%@ Page Title="会员储值查询" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="FinanceListCash1.aspx.cs" Inherits="Eleooo.Web.Admin.FinanceListCash1" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px">
                    <uc:UcPagePosition ID="pagePos" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 23px">
                    <%=ResBLL.GetRes("searchBox_title","按条件搜索：","搜索框标题") %>
                    <label><%=ResBLL.GetRes("searchBox_beginDate","开始日期：","搜索框开始日期") %></label><input class="txtDate" onclick="WdatePicker()" runat="server"
                        id="txtDateStart"><label><%=ResBLL.GetRes("searchBox_endDate","结束日期：","搜索框结束日期") %></label><input class="txtDate" onclick="WdatePicker()"
                            runat="server" id="txtDateEnd">
                    <br />
                    <label>
                        <%=ResBLL.GetRes("searchBox_memberPhone","会员账号：","搜索框会员账号") %></label><input runat="server" id="txtMemberPhone">
                    <asp:RadioButtonList runat="server" ID="rblFlag" RepeatLayout="Flow" RepeatColumns="5">
                        <asp:ListItem Text="全部" Selected="True" Value="0"></asp:ListItem>
                        <asp:ListItem Text="充值" Value="1"></asp:ListItem>
                        <asp:ListItem Text="支付" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Button runat="server" ID="btnSearch" Text="查找(F)" />
                </td>
            </tr>
        </tbody>
    </table>
    <uc:UcGridView ID="gridView" runat="server" AllowPaper="true" />
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="100%">
        <tbody>
            <tr>
                <td class="tbl_row">
                     <span runat="server" id="lblCashDesc"></span>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo runat="server" />
</asp:Content>
