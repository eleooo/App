<%@ Page Title="导入会员" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="MemberExportIn.aspx.cs" Inherits="Eleooo.Web.Company.MemberExportIn" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px">
                    <uc:UcPagePosition ID="pagePos" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                    上传文件:<input type="file" id="txtCustomer" runat="server" size="36" />
                    <asp:Button Style="font-size: 14px; font-weight: bold" runat="server" AccessKey="E"
                        CommandArgument="UploadFile" UseSubmitBehavior="false"
                        ID="btnExportIn" Text="读取会员名单" />
                    仅支持(*.cvs,*.xls)格式
                    <br />
                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblErrorInfo" SkinID="labelRed" runat="server"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <table class="tbl_body" runat="server" id="tbPost" visible="false" border="0" cellspacing="1"
        cellpadding="5" width="99%">
        <tbody>
            <tr class="tbl_rows" height="26" bgcolor="#f0f0f5">
                <td width="580" colspan="2" align="middle">
                    <asp:Button Style="line-height: 18px; height: 30px; font-size: 14px; font-weight: bold"
                        runat="server" AccessKey="S" ID="btnCheck" CommandArgument="CheckMember" UseSubmitBehavior="false"
                        Text="检查会员名单" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button Style="line-height: 18px; height: 30px; font-size: 14px; font-weight: bold"
                        UseSubmitBehavior="false" runat="server" CommandArgument="ImportMember" AccessKey="S"
                        ID="btnPost" Text="导入会员名单" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblErrorInfo2" SkinID="labelRed" runat="server"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <uc:UcGridView runat="server" ID="gridView" AllowPaper="false" Visible="false" />
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="100%">
        <tbody>
            <tr>
                <td class="tbl_row">
                    <b>说明</b>：会员导入只能操作一次，请一次性导入完整的会员名单！
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
