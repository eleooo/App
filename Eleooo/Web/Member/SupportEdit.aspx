<%@ Page Title="我要提问" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="SupportEdit.aspx.cs" Inherits="Eleooo.Web.Member.SupportEdit" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript" language="javascript">
    function onBtnCloseClick() {
        if (parent.dlgHandlerCallback) {
            parent.dlgHandlerCallback();
        } else {
            window.location.href = "/Member/SupportList.aspx?Status=1";
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px">
                    <uc:UcPagePosition runat="server" />
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
                <td>
                    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="99%" >
                        <tbody>
                            <tr class="tbl_head">
                                <td colspan="2" align="middle">
                                    <b runat="Server" id="lblSupportType2"></b>
                                </td>
                            </tr>
                            <tr class="tbl_row">
                                <td width="15%" align="right">
                                    服务人员状态:
                                </td>
                                <td>
                                    <font color="#004400" runat="Server" id="lblSupportMan"></font>
                                </td>
                            </tr>
                            <tr class="tbl_row">
                                <td width="15%" align="right">
                                </td>
                                <td>
                                    <asp:Label ID="lblErrorInfo" SkinID="labelRed" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr class="tbl_row">
                                <td align="right">
                                    <label runat="Server" id="lblSupportSubject">
                                        主题</label>
                                </td>
                                <td>
                                    <asp:TextBox MaxLength="80" Width="400" runat="Server" ID="txtSupportSubject"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tbl_row" height="24" bgcolor="#ffffff">
                                <td align="right">
                                    <label runat="Server" id="lblSupportContent">
                                        内容:</label>
                                </td>
                                <td>
                                    <asp:TextBox runat="Server" ID="txtSupportContent" Rows="12" Columns="88" TextMode="MultiLine"></asp:TextBox><br>
                                    （<font color="red">写填写你要提问的内容.</font>）
                                </td>
                            </tr>
                            <tr class="tbl_row" height="24" bgcolor="#ffffff">
                                <td align="right">
                                    <label runat="Server" id="lblSupportPhoto">
                                        附件：
                                    </label>
                                </td>
                                <td>
                                    点这里上传：<input runat="Server" id="txtSupportPhoto" size="60" type="file"/>
                                    <br/>
                                    （要求小于 1500KB，允许的格式有<b>.rar/.zip/.jpg/.gif</b> .）
                                </td>
                            </tr>
                            <tr class="tbl_rows" height="26" bgcolor="#f0f0f5">
                                <td width="580" colspan="2" align="middle">
                                    <input type="button" style="line-height: 18px; width: 100px; height: 30px; font-size: 14px;
                                        font-weight: bold" accessKey="S" ID="btnPost" onclick="__doPostBack('Add');"
                                        value="提交(S)" />
                                    &nbsp; &nbsp;
                                    <input type="button" style="width: 80px" accesskey="C" id="btnBack" onclick="onBtnCloseClick();"
                                        value="返回" /><br />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td class="tbl_row">
                                <span id="txtMessage" style="color:Red;" runat="server"></span>
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
