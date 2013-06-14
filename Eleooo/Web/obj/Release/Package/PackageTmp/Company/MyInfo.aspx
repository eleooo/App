<%@ Page Title="商家信息" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="Eleooo.Web.Company.MyInfo" %>

<%@ Register Src="~/Controls/UcFormView.ascx" TagPrefix="uc" TagName="UcFormView" %>
<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                </td>
            </tr>
        </tbody>
    </table>
    <uc:UcFormView ID="formView" runat="server" />
    <span id="txtMessage" runat="server" style="color:Red"></span>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
