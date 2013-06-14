<%@ Page Title="区域大厦维护" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="SysAreaMansionEdit.aspx.cs" Inherits="Eleooo.Web.Admin.SysAreaMansionEdit" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagName="UcPagePosition" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<%@ Register Src="~/Controls/UcFormView.ascx" TagName="UcFormView" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function __onBtnCloseClick() {
            if (onBtnCloseClick) {
                onBtnCloseClick();
            }
            else if (parent.dlgHandlerCallback) {
                parent.dlgHandlerCallback();
            } else {
                window.close();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <uc:UcPagePosition ID="UcPagePosition1" runat="server" />
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                </td>
            </tr>
        </tbody>
    </table>
    <uc:UcFormView ID="formView" runat="server" />
    <br />
    <span id="lblMessage" runat="server"></span>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
