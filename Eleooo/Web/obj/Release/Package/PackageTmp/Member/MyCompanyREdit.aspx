<%@ Page Title="我来推荐商家" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    ValidateRequest="false" AutoEventWireup="true" CodeBehind="MyCompanyREdit.aspx.cs"
    Inherits="Eleooo.Web.Member.MyCompanyREdit" %>

<%@ Register Src="~/Controls/UcFormView.ascx" TagName="UcFormView" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainTjsj ">
        <h1 class="mainlistTil">
            推荐商家
            <div class="mainlistTilTip">
                <img alt="" src="/App_Themes/Member/images/xsjTip1.png" />
            </div>
        </h1>
        <uc:UcFormView ID="formView" runat="server" />
        <span id="txtMessage" runat="server" style="color: Red;"></span>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
</asp:Content>
