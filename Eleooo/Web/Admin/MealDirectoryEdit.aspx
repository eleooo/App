<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="MealDirectoryEdit.aspx.cs" Inherits="Eleooo.Web.Admin.MealDirectoryEdit" %>

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
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="99%" height="1">
        <tbody>
            <tr class="tbl_row">
                <td align="right">
                    商家账号
                </td>
                <td>
                    <input type="text" id="txtCompanyTel" runat="server" name="txtCompanyTel" maxlength="11" />
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right">
                    分类名称
                </td>
                <td>
                    <input type="text" id="txtDirName" runat="server" name="txtDirName" />
                </td>
            </tr>
            <tr class="tbl_rows" bgcolor="#f0f0f5">
                <td width="580" colspan="2" align="middle">
                    <input type="button" onclick="__doPostBack('Edit');" style="line-height: 18px; width: 100px;
                        height: 25px; font-size: 14px; font-weight: bold" accesskey="S" id="btnPost"
                        value="保存(S)" />
                    <input type="button" onclick="__onBtnCloseClick();" style="line-height: 18px; width: 100px;
                        height: 25px; font-size: 14px; font-weight: bold" accesskey="C" id="btnClose"
                        value="取消(C)" />
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <span id="lblMessage" runat="server"></span>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
