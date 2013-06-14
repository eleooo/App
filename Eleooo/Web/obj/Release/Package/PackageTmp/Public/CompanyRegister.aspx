<%@ Page Title="商家注册" Language="C#" MasterPageFile="~/MasterPage/PublicMaster.master"
    AutoEventWireup="true" CodeBehind="CompanyRegister.aspx.cs" Inherits="Eleooo.Web.Public.CompanyRegister" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagName="UcPagePosition" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcFormView.ascx" TagName="UcFormView" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="title">
        <span>商家加盟</span>
        <uc:UcPagePosition ID="pagePos" runat="server" />
    </div>
    <br />
    <uc:UcFormView ID="formView" runat="server" />
    <br />
    <span id="lblMessage" runat="server"></span>
</asp:Content>
