<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCompanyItemSettlementCommunity.ascx.cs" Inherits="Eleooo.Web.Controls.UcCompanyItemSettlementCommunity" %>
<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<table border="0" cellspacing="0" cellpadding="5" width="99%">
    <tbody>
        <tr>
            <td style="line-height: 23px" width="50%">
                <label>会员账号:</label>
                <input runat="server" id="txtMemberPhone" name="txtMemberPhone" maxlength="11" type="text" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <label>
                    会员密码:</label>
                <input runat="server" id="txtMemberPwd" name="txtMemberPwd" maxlength="6" type="password" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" value="确定(S)" accesskey="S" name="btnQuery" id="btnQuery" onclick="__doPostBack('btnQuery');" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" value="取消(C)" accesskey="C" name="btnCancel" id="btnCancel"
                    onclick="__doPostBack('btnCancel');" />
            </td>
        </tr>
    </tbody>
</table>
<asp:HiddenField ID="hdnFinger" runat="server" />
<asp:HiddenField ID="hdnMemberPwd" runat="server" />
<table border="0" cellspacing="0" cellpadding="0" width="99%">
    <tbody>
        <tr>
            <td valign="top">
                <uc:UcGridView ID="gridView" runat="server" AllowPaper="true" IsVisiblePageSize="false" />
            </td>
            <td align="center" valign="bottom" id="tdFingerContainer" runat="server">
                <div style="background-color: #666; padding: 1px 1px 1px 1px; width: 150px;">
                    <object classid="clsid:35515A76-3049-4D2A-8457-FD83173037E9" name="finger" width="150"
                        height="150" id="finger" tabindex="0" title="finger">
                        <embed width="150" height="150"></embed>
                    </object>
                </div>
                <div style="background-color: #f0f0f5; width: 100%; text-align: center;">
                    <input style="line-height: 18px; width: 100px; height: 30px; font-size: 14px; font-weight: bold"
                        id="btnGetFinger" onclick="readFinger('<%=hdnFinger.ClientID %>','<%=txtMessage.ClientID %>');"
                        name="btnGetFinger" value="录入指纹" type="button" />
                </div>
            </td>
        </tr>
    </tbody>
</table>
<table border="0" cellspacing="0" cellpadding="0" width="100%" >
    <tbody>
        <tr>
            <td>
                <span id="txtMessage" runat="server" style="color: Red;"></span>
            </td>
        </tr>
    </tbody>
</table>
