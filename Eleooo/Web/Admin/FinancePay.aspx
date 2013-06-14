<%@ Page Title="佣金结算" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="FinancePay.aspx.cs" Inherits="Eleooo.Web.Admin.FinancePay" %>
<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<%@ Register Src="~/Controls/UcPagePosition.ascx" TagName="UcPagePosition" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px">
                 <uc:UcPagePosition runat="server" ID="pagePos" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                    <%=ResBLL.GetRes("searchBox_title","按条件搜索：","搜索框标题") %>
                    <label>
                        <%=ResBLL.GetRes("searchBox_companyPhone","商家账号：","搜索框商家账号") %></label><input runat="server"
                            id="txtCompanyName" />
                    <label>
                        <%=ResBLL.GetRes("searchBox_PayDate","结算日期：","搜索框结算日期") %></label><input class="txtDate"
                            onclick="WdatePicker()" runat="server" id="txtPayDate" />
                    <asp:Button runat="server" ID="btnSearch" Text="搜索(S)" />
                    <br />
                    (注意:只能结算已经过当天之前的日期!)
                </td>
            </tr>
        </tbody>
    </table>
    <uc:UcGridView ID="gridView" runat="server" AllowPaper="true" />
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="100%">
        <tbody>
            <tr>
                <td class="tbl_row">
                    <span id="txtInfo" runat="server"></span><br>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo runat="server" />
</asp:Content>
