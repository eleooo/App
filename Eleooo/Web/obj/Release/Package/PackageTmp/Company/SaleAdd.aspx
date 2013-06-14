<%@ Page Title="开始消费" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master" EnableViewState="false"
    AutoEventWireup="true" CodeBehind="SaleAdd.aspx.cs" Inherits="Eleooo.Web.Company.SaleAdd" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/readFinger.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
            <uc:UcPagePosition runat="server" />
            </tr>
        </tbody>
    </table>
    <asp:RadioButtonList runat="server" ID="rblSaleType" RepeatColumns="3" AutoPostBack="true">
        <asp:ListItem Text="会员消费" Selected="True" Value="1"></asp:ListItem>
        <asp:ListItem Text="抢优惠消费" Value="2"></asp:ListItem>
        <asp:ListItem Text="非会员消费" Value="3"></asp:ListItem>
    </asp:RadioButtonList>
    <div id="SaleContainer" runat="server"></div>
    <span id="txtMessage" runat="server" style="color:Red;"></span>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
