<%@ Page Title="添加会员" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="MemberAdd.aspx.cs" Inherits="Eleooo.Web.Company.MemberAdd" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<%@ Register Src="~/Controls/UcFormView.ascx" TagPrefix="uc" TagName="UcFormView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/readFinger.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px">
                    <uc:UcPagePosition runat="server" ID="ucPos" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                </td>
            </tr>
        </tbody>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="99%">
        <tbody>
            <tr>
                <td valign="top">
                    <uc:UcFormView ID="formView" runat="server" />
                </td>
                <td width="256px" valign="top" id="tdFingerContainer" runat="server">
                    <div style="background-color: #666; padding: 1px 1px 1px 1px; width: 256px;">
                        <object classid="clsid:35515A76-3049-4D2A-8457-FD83173037E9"
                            name="finger" width="256" height="288" id="finger" accesskey="a" tabindex="0"
                            title="finger">
                            <embed width="256" height="288"></embed>
                        </object>
                    </div>
                    <div style="background-color: #f0f0f5; width: 100%; text-align: center;">
                        <input style="line-height: 18px; width: 100px; height: 30px; font-size: 14px; font-weight: bold"
                            id="btnGetFinger" onclick="readFinger('<%=formView.GetViewRow("MemberFinger").ParamName %>','<%=txtMessage.ClientID %>');"
                            name="btnGetFinger" value="录入指纹" type="button" />
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <span id="txtMessage" style="color: Red;" runat="server"></span>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
