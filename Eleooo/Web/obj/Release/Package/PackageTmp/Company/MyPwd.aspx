<%@ Page Title="修改密码" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="MyPwd.aspx.cs" Inherits="Eleooo.Web.Company.MyPwd" %>
<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" width="99%" height="1">
        <tbody>
            <tr>
                <td height="2">
                </td>
            </tr>
        </tbody>
    </table>
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
    <table border="0" cellspacing="0" cellpadding="0" width="99%" height="1">
        <tbody>
            <tr>
                <td>
                    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="99%" height="1">
                        <tbody>
                            <tr class="tbl_row">
                                <td align="right">
                                    <label runat="Server" id="lblCompanyPwd">
                                        旧密码</label>
                                </td>
                                <td>
                                    <asp:TextBox MaxLength="6" runat="Server" TextMode="Password" ID="txtCompanyPwd"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr class="tbl_row">
                                <td align="right">
                                    <label runat="Server" id="lblCompanyPwd1">
                                        新密码</label>
                                </td>
                                <td>
                                    <asp:TextBox MaxLength="6" TextMode="Password" runat="Server" ID="txtCompanyPwd1"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr class="tbl_row">
                                <td align="right">
                                    <label runat="Server" id="lblCompanyPwd2">
                                        确认新密码</label>
                                </td>
                                <td>
                                    <asp:TextBox MaxLength="6" TextMode="Password" runat="Server" ID="txtCompanyPwd2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tbl_rows" height="26" bgcolor="#f0f0f5">
                                <td width="580" colspan="2" align="middle">
                                    <input type="button" onclick="__doPostBack('Edit');" style="line-height: 18px; width: 100px;
                                        height: 25px; font-size: 14px; font-weight: bold" accesskey="S" id="btnPost"
                                        value="保存(S)" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td class="tbl_row">
                                    <span id="txtMessage" style="color: Red;" runat="server"></span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
