<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcNoneMemberSettlement.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcNoneMemberSettlement" EnableViewState="false" %>
<script type="text/javascript" language="javascript">
    var checkOrderSum = function () {
        if (!$("#<%=txtOrderSum.ClientID %>").val()) {
            $("#<%=lblOrderSumInfo.ClientID%>").html("请输入会员消费金额!");
            $("#<%=txtOrderSum.ClientID %>").focus();
            return false;
        }
        if ($("#<%=txtOrderSum.ClientID %>").val() <= 0) {
            $("#<%=lblOrderSumInfo.ClientID%>").html("请输入会员消费金额不能小于或等于零!");
            $("#<%=txtOrderSum.ClientID %>").focus();
            return false;
        }
        return true;
    }
    $(document).ready(function () {
        $("#<%=txtOrderSum.ClientID %>").keydown(function () {
            if (arguments[0].keyCode != 13)
                return;
            if (checkOrderSum())
                $("#<%=txtOrderMemo.ClientID %>").focus();
        });
        $("#<%=txtOrderMemo.ClientID %>").keydown(function () {
            if (arguments[0].keyCode == 13)
                $("#<%=btnPost.ClientID %>").click();
        });
    });

    onSubmitClick = function () {
        if (checkOrderSum()) {
            $('#<%=btnPost.ClientID %>').attr('disabled', 'true');
            __doPostBack('Add');
        }
        else
            return false;
    }
    onCancleClick = function () {
        if (!confirm('您确定要放弃吗？')) { return false; }
        $("#<%=txtOrderSum.ClientID %>").val('');
        $("#<%=txtOrderSum.ClientID %>").focus();
    }
</script>
<table border="0" cellspacing="0" cellpadding="0" width="99%" >
    <tbody>
        <tr>
            <td valign="top">
                <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="99%">
                    <tbody>
                        <tr class="tbl_row">
                            <td align="right" style="width:30%;">
                                <label>
                                    消费金额</label>
                            </td>
                            <td>
                                <asp:TextBox MaxLength="30" Width="100" runat="Server" ID="txtOrderSum"></asp:TextBox>
                                <label runat="server" id="lblOrderSumInfo" style="color: Red">
                                </label>
                            </td>
                        </tr>
                        <tr class="tbl_row" style="height: 26px;">
                            <td align="right">
                                <label>
                                    消费备注</label>
                            </td>
                            <td>
                                <asp:TextBox MaxLength="20" Width="97%" runat="Server" ID="txtOrderMemo"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tbl_rows" height="26" bgcolor="#f0f0f5">
                            <td align="middle" colspan="2">
                                <asp:Button ID="btnPost" Style="line-height: 18px; width: 100px; height: 30px; font-size: 14px;
                                    font-weight: bold" runat="server" AccessKey="S" UseSubmitBehavior="false" OnClientClick="return onSubmitClick();"
                                    Text="提交消费(S)" />
                                &nbsp; &nbsp;
                                <input type="button" style="width: 80px" accesskey="C" id="btnBack" onclick="return onCancleClick();"
                                    value="取消" /><br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
