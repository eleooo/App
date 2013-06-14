<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcMemberSettlementCommunity.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcMemberSettlementCommunity" EnableViewState="false" %>
<asp:HiddenField runat="Server" ID="hdnCompanyID" />
<asp:HiddenField runat="Server" ID="hdnCompanyPhone" />
<script type="text/javascript">
    var checkMemberPhone = function () {
        if (!$("#<%=txtMemberPhone.ClientID %>").val()) {
            $("#<%=lblMemberInfo.ClientID%>").html("请输入会员账号!");
            $("#<%=txtMemberPhone.ClientID %>").focus();
            return false;
        }
        if ($("#<%=txtMemberPhone.ClientID %>").val().length <= 10) {
            $("#<%=lblMemberInfo.ClientID%>").html("会员账号输入不正确,还要输入" + (11 - $("#<%=txtMemberPhone.ClientID %>").val().length) + "位！");
            $("#<%=txtMemberPhone.ClientID %>").focus();
            return false;
        }
        $("#<%=hdnMemberPhone.ClientID %>").val($("#<%=txtMemberPhone.ClientID %>").val());
        return true;
    }
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
    var checkRateSale = function () {
        if (!$("#<%=txtMemberRateSale.ClientID %>").val())
            return true;
        if ($("#<%=txtMemberRateSale.ClientID %>").val() < 0 || $("#<%=txtMemberRateSale.ClientID %>").val() >= 100) {
            $("#<%=lblMemberRateSale.ClientID %>").html("请输入1－99之内的数字!");
            $("#<%=txtMemberRateSale.ClientID %>").focus();
            return false;
        }
        return true;
    }
    var checkMemberPwd = function () {
        if ($('input[name=<%=rblPayment.UniqueID %>][@checked]').val() == 1)
            return true;
        if (!$("#<%=txtMemberPwd.ClientID %>").val()) {
            $("#<%=lblMemberPwdInfo.ClientID %>").html("请输入会员的密码或指纹!");
            $("#<%=txtMemberPwd.ClientID %>").focus();
            return false;
        }
        return true;
    }
    $(document).ready(function () {
        $("#<%=txtMemberPhone.ClientID %>").keydown(function () {
            $("#<%=lblMemberInfo.ClientID%>").html("");
            if (arguments[0].keyCode != 13)
                return;
            if (checkMemberPhone())
                $("#<%=txtOrderSum.ClientID %>").focus();
        });
        $("#<%=txtOrderSum.ClientID %>").keydown(function () {
            $("#<%=lblOrderSumInfo.ClientID%>").html("");
            if (arguments[0].keyCode != 13)
                return;
            if (checkOrderSum())
                $("#<%=txtMemberRateSale.ClientID %>").focus();
        });
        $("#<%=txtMemberRateSale.ClientID %>").keydown(function () {
            $("#<%=lblMemberRateSale.ClientID %>").html("");
            if (arguments[0].keyCode != 13)
                return;
            if (checkRateSale()) {
                $("#<%=btnMemberValidate.ClientID %>").click();
            }
        });
        $("#<%=this.txtMemberPwd.ClientID %>").keydown(function () {
            $("#<%=lblMemberPwdInfo.ClientID %>").html("");
            if (arguments[0].keyCode == 13 && checkMemberPwd()) {
                //$("#<%=txtOrderMemo.ClientID %>").focus();
                $("#<%=btnPost.ClientID %>").click();
            }
        });
        $("#<%=txtOrderMemo.ClientID %>").keydown(function () {
            if (arguments[0].keyCode == 13)
                $("#<%=btnPost.ClientID %>").click();
        });
    });
    onMemberValidateClick = function () {
        if (checkMemberPhone() && checkOrderSum() && checkRateSale()) {
            __doPostBack('CheckMember');
        }
        else
            return false;
    }
    onSubmitClick = function () {
        if ($("#<%=txtMemberPhone.ClientID %>").attr("disabled") == false) {
            $("#<%=lblMemberRateSale.ClientID %>").html("请先验证会员!");
            return false;
        }
        if (checkMemberPhone() && checkOrderSum() && checkRateSale() && checkMemberPwd()) {
            $('#<%=btnPost.ClientID %>').attr('disabled', 'true');
            __doPostBack('Add');
        }
    }
    onCancleClick = function () {
        if (!confirm('您确定要放弃吗？')) { return false; }
        window.location.href = window.location.href;
    }
</script>
<asp:HiddenField runat="server" ID="hdnMemberPhone" />
<asp:HiddenField runat="Server" ID="hdnMemberID" />
<asp:HiddenField runat="Server" ID="hdnMemberPwd" />
<asp:HiddenField runat="Server" ID="hdnMemberBalance" />
<asp:HiddenField runat="Server" ID="hdnMemberBalanceCash" />
<table border="0" cellspacing="0" cellpadding="0" width="99%">
    <tbody>
        <tr>
            <td>
                <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="99%">
                    <tbody>
                        <tr class="tbl_row">
                            <td align="right" style="width:20%;">
                                <label>
                                    会员账号</label>
                            </td>
                            <td>
                                <asp:TextBox MaxLength="11" autocomplete="off" Width="150" runat="Server" ID="txtMemberPhone"></asp:TextBox>
                                <label runat="server" id="lblMemberInfo" style="color: Red">
                                </label>
                            </td>
                        </tr>
                        <tr class="tbl_row">
                            <td align="right">
                                <label>
                                    消费金额</label>
                            </td>
                            <td>
                                <asp:TextBox MaxLength="30" Width="150" runat="Server" ID="txtOrderSum"></asp:TextBox>
                                <label runat="server" id="lblOrderSumInfo" style="color: Red">
                                </label>
                                <br />
                                （输入消费金额或折扣后，请点计算）
                                
                            </td>
                        </tr>
                        <tr class="tbl_row">
                            <td align="right">
                                <label>
                                    折扣</label>
                            </td>
                            <td>
                                <asp:TextBox MaxLength="2" Width="150" runat="Server" ID="txtMemberRateSale" Text="00"></asp:TextBox>
                                <asp:Button Style="width: 80px" runat="server" ID="btnMemberValidate" UseSubmitBehavior="false" OnClientClick="return onMemberValidateClick();"
                                    Text="验证" />
                                <br />
                                （请输入1－99之内的数字,留空代表不打折）
                                <label runat="server" id="lblMemberRateSale" style="color: Red">
                                    &nbsp;&nbsp;
                                </label>
                            </td>
                        </tr>
                        <tr class="tbl_row">
                            <td align="right">
                                <label>
                                    实际金额</label>
                            </td>
                            <td>
                                <asp:TextBox MaxLength="30" Width="150" runat="Server" ID="txtOrderSumOk" Enabled="false" ReadOnly="true"></asp:TextBox>
                                <label runat="server" id="lblOrderSumOkInfo" style="color: Red">
                                </label>
                            </td>
                        </tr>
                        <tr class="tbl_row" runat="server" id="trOrderItem" visible="false">
                            <td align="right">
                                <label>
                                    消费项目</label>
                            </td>
                            <td>
                                <asp:DropDownList runat="Server" ID="ddlOrderItem">
                                    <asp:ListItem Selected="True" Text="普通消费" Value="普通消费"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tbl_row">
                            <td align="right">
                                <label>
                                    付款方式</label>
                            </td>
                            <td>
                                <asp:RadioButtonList runat="Server" ID="rblPayment">
                                    <asp:ListItem Selected="True" Text="现金支付" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tbl_row" id="rowCompanyRate" runat="server">
                            <td align="right">
                                <label>
                                    积分比例</label>
                            </td>
                            <td>
                                <asp:RadioButtonList RepeatColumns="9" runat="Server" ID="rblCompanyRate">
                                </asp:RadioButtonList>
                                <label runat="server" id="lblCompanyRateInfo" style="color: Red">
                                </label>
                            </td>
                        </tr>
                        <tr class="tbl_row">
                            <td align="right">
                                <label>
                                    会员密码</label>
                            </td>
                            <td>
                                <asp:TextBox MaxLength="7" Width="100" runat="Server" ID="txtMemberPwd" TextMode="Password"></asp:TextBox>
                                <label runat="server" id="lblMemberPwdInfo" style="color: Red"> 
                                </label>
                                <br />
                                <span>(输入密码点消费或点消费后录入指纹)</span>
                                
                                
                            </td>
                        </tr>
                        <tr class="tbl_row" style="height: 26px;display:none;">
                            <td align="right">
                                <label>
                                    消费备注</label>
                            </td>
                            <td>
                                <asp:TextBox MaxLength="20" Width="97%" runat="Server" ID="txtOrderMemo"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tbl_rows" bgcolor="#f0f0f5">
                            <td colspan="2" align="middle">
                                <asp:Button Enabled="false" UseSubmitBehavior="false" Style="line-height: 18px; width: 100px;
                                    height: 30px; font-size: 14px; font-weight: bold" OnClientClick="return onSubmitClick();"
                                    runat="server" AccessKey="S" ID="btnPost" Text="提交消费(S)" />
                                &nbsp; &nbsp;
                                <input type="button" style="width: 80px" accesskey="C" id="btnBack" onclick="return onCancleClick();"
                                    value="取消" /><br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td align="center" valign="top" id="tdFingerContainer" runat="server">
                <div style="background-color: #666; padding: 1px 1px 1px 1px; width: 256px;">
                    <object classid="clsid:35515A76-3049-4D2A-8457-FD83173037E9"
                        name="finger" width="256" height="288" id="finger" tabindex="0" title="finger">
                        <embed width="256" height="288"></embed>
                    </object>
                </div>
                <div style="background-color: #f0f0f5; width: 100%; text-align: center;">
                    <input style="line-height: 18px; width: 100px; height: 30px; font-size: 14px; font-weight: bold"
                        id="btnGetFinger" onclick="readFinger('<%=txtMemberPwd.ClientID %>','<%=lblMemberPwdInfo.ClientID %>');"
                        name="btnGetFinger" value="录入指纹" type="button" />
                </div>
            </td>
        </tr>
    </tbody>
</table>
