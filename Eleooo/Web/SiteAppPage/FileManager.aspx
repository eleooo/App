<%@ Page Title="文件管理器" Language="C#" MasterPageFile="~/MasterPage/MasterPageBase.Master"
    AutoEventWireup="true" CodeBehind="FileManager.aspx.cs" Inherits="Eleooo.Web.SiteAppPage.FileManager" %>

<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/regaction.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
    <input type="file" name="uploadify" id="uploadify" size="30" runat="server" />
    <input type="button" name="btnUpload" id="btnUpload" value="上传" class="button" onclick="__doPostBack('Add');" />
    <br />
    <span id="lblMessage" runat="server" style="color: Red;"></span>
    <br />
    <span id="lblInfo" runat="server" style="color: Maroon;"></span>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc:UcGridView ID="gridView" runat="server" AllowPaper="false" />
    <span id="imgTemplate" runat="server" visible="false"><a href="{0}" target="_blank">
        <img src="{0}" alt="" width="120" height="120" /></a></span>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
</asp:Content>
