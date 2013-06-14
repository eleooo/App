<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Eleooo.Web.Public.Login" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<%@ Import Namespace="Eleooo.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <ele:ResLink ID="rs1" runat="server" Src="/App_Themes/ThemesV2/css/content_3.css" />
    <ele:ResLink ID="rs2" runat="server" Src="/Scripts/SendMsn.js" />
    <style type="text/css">
        .red{ color:Red;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <!--内容开始-->
    <div class="main">
        <div class="zc-k">
            <div class="zc-k-left">
                <div class="zc-k-left-title" id="memberLoginTitle" runat="server">
                    <span>登录</span>或者<a href="Register.aspx">注册</a>
                </div>
                <div class="zc-k-left-title" id="companyLoginTitle" runat="server">
                    <span>商家登录</span>
                </div>
                <div class="zc-k-left-main">
                    <ul>
                        <li>
                            <div class="wc01">
                                账 号</div>
                            <input type="text" maxlength="11" class="logon_txt" value="<%=this.Params["edtUserName"] %>"
                                name="edtUserName" id="edtUserName" size="20" />
                            <label class="red" id="lblUserNameInfo">
                                <%=this.Message["check_UserName"]%>
                            </label>
                        </li>
                        <li>
                            <div class="wc01">
                                密 码</div>
                            <input type="password" maxlength="6" class="logon_txt" value="<%=this.Params["edtPassWord"] %>"
                                name="edtPassWord" id="edtPassWord" size="20" />
                            <p>
                                <label class="red" id="lblPassWordInfo"><%=this.Message["check_password"]%></label>
                                &nbsp;
                                <a href="javascript:void(0)" style="color: #0896ae" onclick="$('#btnSendPwd').parent().show();">
                                    忘了密码？</a></p>
                        </li>
                        <li style="padding-left: 70px; display: none;"><a href="javascript:void(0)" class="dx_link"
                            id="btnSendPwd">短信获取密码</a> </li>
                        <li class="in08">
                            <div class="wc02">
                                验证码</div>
                            <input type="text" class="logon_txt validataCode" name="edtCheckOut" id="edtCheckOut"
                                size="10" />
                            <img src="" id="codeImg" alt="" title="" class="codeImg" align="absmiddle" />
                            <a class="changeValidataCode" id="changeCode" href="javascript:void(0)" style="color: #0896ae">
                                看不清楚？换一张</a>
                            <label class="red" id="lblCheckOutInfo">
                                <%=this.Message["check_login_code"]%>
                            </label>
                            <input type="hidden" name="subSys" value="<%=this.LoginSys %>" /></li>
                    </ul>
                </div>
                <div class="zc-k-left-aniu">
                    <a href="javascript:void(0)" onclick="__doPostBack('Login');">
                        <img src="/App_Themes/ThemesV2/images/dl.png" width="80" height="30" border="0" alt="" /></a></div>
            </div>
            <div class="zc-k-right">
                <div class="zc-k-right-w" id="memberLoginDesc" runat="server">
                    <h3>
                        还没注册乐多分账号？</h3>
                    <span><a href="Register.aspx" class="go_zc">>>马上注册，仅需15秒</a></span>
                </div>
                <div class="zc-k-right-w" id="companyLoginDesc" runat="server">
                    <h3>
                        合作加盟？</h3>
                    <span>店面服务你做主，营销推广交给我。 如果您是本地化生活服务类优质商家，请与我们联络：<a style="color: rgb(8, 150, 174);"
                        href="#">hz@eleooo.com</a> </span>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--内容结束-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
    <script type="text/javascript">
        validateInput = function () {
            $("#lblUserNameInfo").html('');
            $("#lblPassWordInfo").html('');
            $("#lblCheckOutInfo").html('');
            if (!$("#edtUserName").val()) {
                $("#lblUserNameInfo").html('<%=ResBLL.Res["check_UserName_empty"] %>');
                $("#edtUserName").focus();
                return false;
            }

            if (!$("#edtPassWord").val()) {
                $("#lblPassWordInfo").html('<%=ResBLL.Res["check_PassWord_empty"] %>');
                $("#edtPassWord").focus();
                return false;
            }
            if (!$("#edtCheckOut").val()) {
                $("#lblCheckOutInfo").html('<%=ResBLL.Res["check_Code_empty"] %>');
                $("#edtCheckOut").focus();
                return false;
            }
            return true;
        }
        var url = "/Public/CreateCodeHandler.ashx";
        $(document).ready(function () {
            $("#edtCheckOut").keydown(function () {
                if (arguments[0].keyCode == 13) {
                    __doPostBack('Login');
                }
            });
            $("#codeImg").click(function () {
                var date = (new Date()).valueOf();
                $("#codeImg").attr("src", url + "?sn=" + date);
            });
            $("#changeCode").click(function () {
                $("#codeImg").click();
            });
            $("#codeImg").attr("src", url + "?sn=" + (new Date()).valueOf());
            //$("#btnSendPwd").click(sendPassword);
            new SendWaitMsnMessage(
            {
                btnSend: 'btnSendPwd',
                fnSendHandler: function (fnCallback) {
                    var txtPhone = $("#edtUserName");
                    var phone = txtPhone.val();
                    if (!phone) {
                        alert("请输入你的登录账号.");
                        txtPhone.focus();
                        return;
                    } else if (phone.length < 11) {
                        alert("请输入11位的登录账号.");
                        txtPhone.focus();
                        return;
                    }
                    $.getJSON('/Public/RestHandler.ashx/SendPassword?Phone=' + phone +'&date='+(new Date()).valueOf(), function (data) {
                        if (data.code < 0) {
                            alert(data.message);
                            return;
                        }
                        fnCallback();
                        //alert(data.message);
                    });
                }
            });
        });
    </script>
</asp:Content>
