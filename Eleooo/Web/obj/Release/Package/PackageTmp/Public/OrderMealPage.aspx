<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="OrderMealPage.aspx.cs" Inherits="Eleooo.Web.Public.OrderMealPage" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <ele:ResLink ID="rs0" runat="server" Src="/App_Themes/ThemesV2/css/inc_2.css" ReplaceSrc="/App_Themes/ThemesV2/css/inc.css" />
    <ele:ResLink ID="rs1" runat="server" Src="/App_Themes/ThemesV2/css/content_2.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="main">
        <ele:ControlDelegate ID="view" runat="server" Src="~/Controls/UcOrderMealSelectMansion.ascx" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
</asp:Content>
