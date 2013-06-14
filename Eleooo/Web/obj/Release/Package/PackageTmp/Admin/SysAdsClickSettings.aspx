<%@ Page Title="广告浏览设置" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="SysAdsClickSettings.aspx.cs" Inherits="Eleooo.Web.Admin.SysAdsClickSettings" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagName="UcPagePosition" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px">
                    <uc:UcPagePosition ID="UcPagePosition1" runat="server" />
                </td>
            </tr>
            <tr>
                <td id="gridContainer" runat="server">
                </td>
            </tr>
            <tr>
                <td style=" background-color:Highlight;">
                    说明:正数消费层次表示大于等于设置值,如:300,表示大于或等300.负数消费层次表示小于设置值,如:-200,表示小于200（不含等于）.
                </td>
            </tr>
            <tr class='tbl_form_row' height='26' bgcolor='#f0f0f5'>
                <td align='middle' bgcolor='#f0f0f5'>
                    <input type='submit' style='line-height: 18px; width: 100px; height: 25px; font-size: 14px;
                        font-weight: bold' onclick='__doPostBack("Edit");' id='btnSubmit' value='提交(S)' />
                    &nbsp;&nbsp;
                    <input type='button' style='line-height: 18px; width: 100px; height: 25px; font-size: 14px;
                        font-weight: bold' onclick='__onBtnCloseClick();' id='btnClose' value='取消(C)' />
                </td>
            </tr>
            <tr>
                <td>
                    <span id="txtMessage" runat="server" style="color: Red;" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMemberInfo" runat="server">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
