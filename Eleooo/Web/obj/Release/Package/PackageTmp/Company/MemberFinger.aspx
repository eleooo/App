<%@ Page Title="补录指纹" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="MemberFinger.aspx.cs" Inherits="Eleooo.Web.Company.MemberFinger" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
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
                    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="99%">
                        <tbody>
                            <tr class="tbl_row">
                                <td align="right" style="width:20%;">
                                    <label>
                                        会员帐号</label>
                                </td>
                                <td>
                                    <asp:TextBox MaxLength="11" Width="150" runat="Server" ID="txtMemberTel"></asp:TextBox><span
                                        style="color: Red" id="lblMemberTelInfo" runat="server"></span>
                                    <br />
                                    （请输入手机号码）
                                </td>
                            </tr>
                            <tr class="tbl_row">
                                <td align="right">
                                    <label>
                                        会员密码</label>
                                </td>
                                <td>
                                    <asp:TextBox MaxLength="20" Width="150" TextMode="Password" runat="Server" ID="txtMemberPwd"></asp:TextBox><span
                                        style="color: Red" id="lblMemberPwdInfo" runat="server"></span>
                                    <br />
                                    （请输入密码）
                                </td>
                            </tr>
                            <tr class="tbl_rows" height="26" bgcolor="#f0f0f5">
                                <td width="580" colspan="2" align="middle">
                                    <input style="line-height: 18px; width: 100px; height: 30px; font-size: 14px; font-weight: bold"
                                        type="button" name="btnGetFinger" id="btnGetFinger" value="录入指纹" onclick="readFinger('<%=hdnMemberFinger.ClientID %>','<%=txtMessage.ClientID %>');" />
                                    <input runat="server" id="hdnMemberFinger" type="hidden" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button Style="line-height: 18px; width: 100px; height: 30px; font-size: 14px;
                                        font-weight: bold" runat="server" UseSubmitBehavior="false" OnClientClick="__doPostBack('Edit');"
                                        AccessKey="S" ID="btnPost" Text="保存(S) " />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td width="256px">
                    <div style="background-color: #666; padding: 1px 1px 1px 1px; width: 256px;">
                        <object classid="clsid:35515A76-3049-4D2A-8457-FD83173037E9"
                            name="finger" width="256" height="288" id="finger" accesskey="a" tabindex="0"
                            title="finger">
                            <embed width="256" height="288"></embed>
                        </object>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <span id="txtMessage" runat="server"></span>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
