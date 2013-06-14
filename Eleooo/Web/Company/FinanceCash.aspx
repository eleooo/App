<%@ Page Title="会员储值" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="FinanceCash.aspx.cs" Inherits="Eleooo.Web.Company.FinanceCash" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/jscript" language="javascript">
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
        var checkMemberCash = function () {
            if (!$("#<%=txtMemberCash.ClientID %>").val()) {
                $("#<%=lblMemberCashInfo.ClientID%>").html("请输入会员充值金额!");
                $("#<%=txtMemberCash.ClientID %>").focus();
                return false;
            }
            if ($("#<%=txtMemberCash.ClientID %>").val() <= 0) {
                $("#<%=lblMemberCashInfo.ClientID%>").html("请输入会员消费充值不能小于或等于零!");
                $("#<%=txtMemberCash.ClientID %>").focus();
                return false;
            }
            return true;
        }
        var checkMemberRate = function () {
            if (!$("#<%=txtMemberRate.ClientID %>").val())
                return true
            if ($("#<%=txtMemberRate.ClientID %>").val() < 0 || $("#<%=txtMemberRate.ClientID %>").val() >= 100) {
                $("#<%=lblMemberRateInfo.ClientID %>").html("请输入1－99之内的数字!");
                $("#<%=txtMemberRate.ClientID %>").focus();
                return false;
            }
            return true;
        }

        var checkMemberPoint = function () {
            if (!$("#<%=txtMemberPoint.ClientID %>").val())
                return true;
            if ($("#<%=txtMemberPoint.ClientID %>").val() < 0) {
                $("#<%=lblMemberPointInfo.ClientID %>").html("赠送积分不能小于零!");
                $("#<%=txtMemberPoint.ClientID %>").focus();
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
                    $("#<%=btnMemberValidate.ClientID %>").click();
            });

            $("#<%=txtMemberCash.ClientID %>").keydown(function () {
                $("#<%=lblMemberCashInfo.ClientID%>").html("");
                if (arguments[0].keyCode != 13)
                    return;
                if (checkMemberCash()) {
                    if ($("#<%=hdnCompanyType.ClientID %>").val() == '2')
                        $("#<%=cbMemberGrade.ClientID %>").focus();
                    else
                        $("#<%=txtMemberRate.ClientID %>").focus();
                }
            });
            $("#<%=txtMemberRate.ClientID %>").keydown(function () {
                $("#<%=lblMemberRateInfo.ClientID%>").html("");
                if (arguments[0].keyCode != 13)
                    return;
                if (checkMemberRate())
                    $("#<%=rblCompanyRate.ClientID %>").focus();
            });
            $("#<%=txtMemberPoint.ClientID %>").keydown(function () {
                $("#<%=lblMemberPointInfo.ClientID%>").html("");
                if (arguments[0].keyCode != 13)
                    return;
                if (checkMemberPoint())
                    $("#<%=cbMemberGrade.ClientID %>").focus();
            });
            $("#<%=cbMemberGrade.ClientID %>").keydown(function () {
                if (arguments[0].keyCode == 13)
                    $("#<%=btnPost.ClientID %>").click();
            });
        });
        onMemberValidateClick = function () {
            if (checkMemberPhone()) {
                __doPostBack('CheckMember');
            }
            else
                return false;
        }
        onSubmitClick = function () {
            if ($("#<%=txtMemberPhone.ClientID %>").attr("disabled") == false) {
                $("#<%=lblMemberRateInfo.ClientID %>").html("请先验证会员!");
                return false;
            }
            if (checkMemberPhone() && checkMemberCash() && checkMemberRate() && checkMemberPoint()) {
                $('#<%=btnPost.ClientID %>').attr('disabled', 'true');
                __doPostBack('Add');
            }
        }
        onCancleClick = function () {
            if (!confirm('您确定要放弃吗？')) { return false; }
            window.location.href = window.location.href;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
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
    <asp:HiddenField runat="server" ID="hdnCompanyType" />
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="99%">
        <tbody>
            <tr class="tbl_row">
                <td align="right" style="width: 20%;">
                    <label>
                        会员帐号</label>
                </td>
                <td>
                    <asp:HiddenField runat="Server" ID="hdnMemberPhone" Value="0"></asp:HiddenField>
                    <asp:TextBox MaxLength="11" Width="150" runat="Server" ID="txtMemberPhone"></asp:TextBox>
                    <asp:Button Style="width: 80px" runat="server" ID="btnMemberValidate" UseSubmitBehavior="false"
                        OnClientClick="return onMemberValidateClick();" Text="验证" />&nbsp;&nbsp;&nbsp;
                    <label runat="server" id="lblMemberInfo" style="color: red">
                    </label>
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right">
                    <label>
                        充值金额</label>
                </td>
                <td>
                    <asp:TextBox MaxLength="20" Width="150" runat="Server" ID="txtMemberCash"></asp:TextBox>
                    <label runat="server" id="lblMemberCashInfo" style="color: red">
                    </label>
                </td>
            </tr>
            <tr class="tbl_row" runat="server" id="trMemberRate">
                <td align="right">
                    <label>
                        折扣比例</label>
                </td>
                <td>
                    <asp:TextBox MaxLength="2" Width="150" runat="Server" ID="txtMemberRate"></asp:TextBox>（请输入1－99之内的数字,留空代表不打折）<br />
                    <label runat="server" id="lblMemberRateInfo" style="color: red">
                    </label>
                </td>
            </tr>
            <tr class="tbl_row" runat="server" id="tr1">
                <td align="right">
                    <label>
                        积分比例</label>
                </td>
                <td>
                    <asp:RadioButtonList RepeatColumns="9" runat="Server" ID="rblCompanyRate">
                    </asp:RadioButtonList>                    
                    <label runat="server" id="lblRateSaleInfo" style="color: red">
                    </label>
                </td>
            </tr>
            <tr class="tbl_row" runat="server" id="trMemberPoint">
                <td align="right">
                    <label>
                        赠送积分</label>
                </td>
                <td>
                    <asp:TextBox MaxLength="5" Width="150" runat="Server" ID="txtMemberPoint"></asp:TextBox>
                    <label runat="server" id="lblMemberPointInfo" style="color: red">
                    </label>
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right">
                    <label>
                        会员级别</label>
                </td>
                <td>
                    <asp:DropDownList Width="150" ID="cbMemberGrade" runat="server"></asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr class="tbl_row" style="display: none">
                <td align="right">
                    <label>
                        充值方式</label>
                </td>
                <td>
                    <asp:RadioButtonList runat="Server" ID="rblPayMent" RepeatColumns="9">
                        <asp:ListItem Text="支付宝" Value="2" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="财付通" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tbl_rows" bgcolor="#f0f0f5">
                <td width="580" colspan="2" align="middle">
                    <asp:Button Enabled="false" UseSubmitBehavior="false" Style="line-height: 18px; width: 100px;
                        height: 30px; font-size: 14px; font-weight: bold" OnClientClick="return onSubmitClick();"
                        runat="server" AccessKey="S" ID="btnPost" Text="提交(S)" />
                    &nbsp; &nbsp;
                    <input type="button" style="width: 80px" accesskey="C" id="btnBack" onclick="return onCancleClick();"
                        value="取消" /><br />
                </td>
            </tr>
        </tbody>
    </table>
    <span id="txtMessage" runat="server" style="color: Red;"></span>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
