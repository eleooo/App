﻿<%@ Page Title="现金明细" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="FinanceCashList1.aspx.cs" Inherits="Eleooo.Web.Company.FinanceCashList1" %>

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
                    <uc:UcPagePosition ID="UcPagePosition1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                    <%=ResBLL.GetRes("searchBox_title","按条件搜索：","搜索框标题") %><label><%=ResBLL.GetRes("searchBox_beginDate","开始日期：","搜索框开始日期") %></label>
                    <input class="txtDate" onclick="WdatePicker()" runat="server" type="text" id="txtDateStart" />
                    <label>
                        <%=ResBLL.GetRes("searchBox_endDate","结束日期：","搜索框结束日期") %></label>
                    <input class="txtDate" onclick="WdatePicker()" runat="server" type="text" id="txtDateEnd" />
                    <br />
                    <label>
                        <%=ResBLL.GetRes("searchBox_memberPhone","会员账号：","搜索框会员账号") %></label>
                    <input runat="server" id="txtMemberPhone" type="text" />
                    <asp:RadioButtonList runat="server" ID="rblFlag" RepeatLayout="Flow" RepeatColumns="3">
                        <asp:ListItem Selected="True" Text="全部" Value="0"></asp:ListItem>
                        <asp:ListItem Text="本店" Value="1"></asp:ListItem>
                        <asp:ListItem Text="外来" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="Button1" Text="查找(F)" />
                </td>
            </tr>
        </tbody>
    </table>
    <uc:UcGridView ID="gridView" runat="server" AllowPaper="true" />
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="100%">
        <tbody>
            <tr>
                <td class="tbl_row">
                    <b>说明</b>：<br /> <span runat="server" id="lblCashDesc"></span>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
