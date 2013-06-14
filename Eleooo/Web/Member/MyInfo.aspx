<%@ Page Title="个人资料" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="Eleooo.Web.Member.MyInfo" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainBase">
        <div class="left">
            <ul style="height:370px;">
                <li><span>昵称：</span>
                    <div>
                        <input class="input" type="text" id="txtMemberName" runat="server" /></div>
                </li>
                <li><span>帐号：</span>
                    <div><asp:TextBox ID="txtUserPhone" runat="server" MaxLength="11" CssClass="input" ></asp:TextBox> </div>
                </li>
                <li id="ctCodeContainer"><span>验证：</span>
                    <div>
                        <input type="hidden" id="hdfMsnId" runat="server" />
                        <u class="warm">帐号一年内限修改两次</u><input class="input2" type="text" id="txtValidateCode"
                            runat="server" /><a class="yz" href="javascript:void(0);" id="btnSendCode"><img alt=""
                                src="/App_Themes/ThemesV2/images/hcyzm.png" /></a></div>
                </li>
                <li><span>性别：</span>
                    <div>
                        <asp:RadioButtonList runat="server" ID="rbSex" RepeatColumns="2" RepeatLayout="Table">
                            <asp:ListItem Text="男" Value="true" />
                            <asp:ListItem Text="女" Value="false" />
                        </asp:RadioButtonList>
                    </div>
                </li>
                <li><span>邮箱：</span>
                    <div>
                        <input class="input" type="text" id="txtEmail" runat="server" /></div>
                </li>
                <li><span>生日：</span>
                    <ele:DatetimePicker ID="picker" runat="server" YearCssClass="select1" MonthCssClass="select1"
                        DayCssClass="select1" />
                </li>
                <li>
                    <input class="bc_btn" value="保存" type="button" onclick="__doPostBack('Edit');" /><label
                        id="txtMessage" style="color: Red;" runat="server">
                    </label>
                </li>
            </ul>
        </div>
        <div class="right">
            <h3>
                完善信息有什么好处？</h3>
            <p>
                个人信息越完整，得到的商家优惠越多，还可以获得更多看广告赚积分的机会。</p>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
    <script type="text/javascript">
        var oldPhone = "<%= AppContext.Context.User.MemberPhoneNumber %>";
        var ctPhone = "#<%=txtUserPhone.ClientID %>";
        var ctMsnId = "#<%=hdfMsnId.ClientID %>";
        var ctCode = "#<%=txtValidateCode.ClientID %>";
        var isSending = false;
        var counter = false;
        validateInput = function () {
            var phone = $(ctPhone).val();
            if (!phone || phone.length < 11) {
                alert("请输入正确的手机新账号.");
                $(ctPhone).focus();
                return false;
            }
            if (phone === oldPhone && $(ctMsnId).val()) {
                alert("手机新账号与旧账号不允许相同.");
                $(ctPhone).focus();
                return false;
            } else if (phone != oldPhone && (!$(ctMsnId).val() || $(ctMsnId).val().length == 0)) {
                alert("请获取验证码.");
                return false;
            } else if (phone != oldPhone && (!$(ctCode).val() || $(ctCode).val().length == 0)) {
                alert("请输入验证码.");
                !$(ctCode).focus();
                return false;
            }
            return true;
        }
        function reCounter() {
            counter = counter - 1;
            var btn = $("#btnSendCode");
            if (counter > 0) {
                btn.html(counter + "秒后可重发");
                setTimeout(reCounter, 1000);
            } else {
                btn.removeClass("gray").html('<img alt="" src="/App_Themes/ThemesV2/images/hcyzm.png" />');
            }
        }
        $(document).ready(function () {
            if ($(ctPhone).attr("readonly"))
                $("#ctCodeContainer").hide();
            $("#btnSendCode").click(function () {
                if (isSending) return;
                if (counter > 0) {
                    //alert(counter + "秒后可重新获取验证码.");
                    return;
                }
                var phone = $(ctPhone).val();
                if (!phone || phone.length < 11) {
                    alert("请输入正确的手机新账号.");
                    $(ctPhone).focus();
                    return;
                }
                if (phone === oldPhone) {
                    alert("请输入新账号^_^");
                    $(ctPhone).focus();
                    return;
                }
                $(ctMsnId).val('');
                isSending = true;
                $.ajax({
                    type: "POST",
                    url: "/Public/RestHandler.ashx/OrderMeal/GetMsnCode",
                    dataType: "json", data: { phone: phone, isForChgNo: 1 },
                    success: function (result) {
                        isSending = false;
                        if (result.code > 0) {
                            $(ctMsnId).val(result.code);
                            counter = 60;
                            reCounter();
                            $("#btnSendCode").html("60秒后可重发").addClass("gray");
                            alert(result.message);
                            //                            var btn = $("#btnSendCode");
                            //                            var sendCount = btn.data("sendCount") || 0;
                            //                            if (sendCount >= 1) {
                            //                                btn.hide();
                            //                                return;
                            //                            }
                            //                            btn.data("sendCount", sendCount + 1);
                            //                            fnCallback();
                        } else if (result.code == -2) {
                            alert("你输入的新号码已是乐多分会员^_^");
                            return;
                        }
                        else
                            alert(result.message);
                    }
                });
            });
        });
    </script>
</asp:Content>
