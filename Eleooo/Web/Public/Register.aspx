<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Eleooo.Web.Public.Register" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <ele:ResLink ID="rs0" runat="server" Src="/App_Themes/ThemesV2/css/inc_3.css" ReplaceSrc="/App_Themes/ThemesV2/css/inc.css" />
    <ele:ResLink ID="rs1" runat="server" Src="/App_Themes/ThemesV2/css/content_3.css" />
    <link href="/Scripts/jquery.tipswindown/jquery.tipswindown.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jquery.tipswindown/jquery.tipswindown.js" type="text/javascript"></script>
    <style type="text/css">
        #windown-content
        {
            position: relative;
            text-align: center;
            overflow: visible;
        }
        .STYLE2
        {
            color: #ff8a00;
            font-weight: bold;
            font-size: 14px;
        }
        .zc-k-left-main li p{ padding-left:10px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <!--内容开始-->
    <div class="main">
        <div class="zc-k">
            <div class="zc-k-left">
                <div class="zc-k-left-title">
                    <span>注册</span>或者<a href="Login.aspx">登录</a></div>
                <div class="zc-k-left-main">
                    <ul>
                        <li>
                            <div class="wc01">
                                账 号</div>
                            <asp:TextBox ID="txtMemberPhone" size="20" MaxLength="11" runat="server" />
                            <p id="lblPhoneInfo" runat="server">
                                请输入您的手机号码
                            </p>
                        </li>
                        <li>
                            <div class="wc01">
                                密 码</div>
                            <asp:TextBox TextMode="Password" ID="txtMemberPwd1" MaxLength="6" size="20" runat="server" />
                            <p id="lblPwd1Info" runat="server">
                                限6位数字
                            </p>
                        </li>
                        <li>
                            <div class="wc">
                                确认密码</div>
                            <asp:TextBox TextMode="Password" ID="txtMemberPwd2" MaxLength="6" size="20" runat="server" />
                            <p id="lblPwd2Info" runat="server">
                            </p>
                        </li>
                        <li>
                            <div class="wc02">
                                推荐人</div>
                            <asp:TextBox class="logon_txt" ID="txtMyFriend" MaxLength="11" size="20" runat="server" />
                            <p id="lblRMember" runat="server">
                                
                            </p>
                        </li>
                    </ul>
                </div>
                <div class="zc-k-left-ydu" style="vertical-align: middle">
                    <input type="checkbox" id="txtChecked" runat="server" checked="checked" style="vertical-align: middle" />
                    <span class="c_1122cc" style="vertical-align: middle">我已阅读并同意<a href="javascript:readHelp();">《乐多分用户协议》</a></span>
                </div>
                <div class="zc-k-left-aniu">
                    <a href="javascript:void(0)" onclick="__doPostBack('Add');">
                        <img src="/App_Themes/ThemesV2/images/zc.png" width="80" height="30" border="0" alt="" /></a></div>
            </div>
            <div class="zc-k-right">
                <div class="zc-k-right-w">
                    <h3>
                        注册有哪些好处？</h3>
                    <span>成为会员后，您可以更方便的叫外卖，还可以抢购附近商家提供的各种优惠，或者看广告赚积分。</span>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!--内容结束-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
    <script type="text/javascript" language="javascript">
        var readHelp = function () {
            var textContent = '<iframe src="EleoooHelp.aspx?IsDlg=1" width="100%" height="95%"' +
                                  'scrolling="auto" frameborder="0" marginheight="0" marginwidth="0"></iframe>' +
                                  '<div style="height: 7%; width: 98%;background-color:White;>' +
                                  '<input type="button" onclick="onAgreeClicked();" class="login_just_regster_btn" value="我同意" /></div>';
            tipsWindown("乐多分用户协议", "text:" + textContent, 700, 500, "true", "", "true", "");
        }
        var onAgreeClicked = function () {
            $("#<%=txtChecked.ClientID %>").attr("checked", true);
            $("#windown-close").click();
        }
        var validateInput = function () {
            if (!$("#<%=txtMemberPhone.ClientID %>").val()) {
                $("#<%=lblPhoneInfo.ClientID %>").html("请输入注册手机号码").css("color","red");
                $("#<%=txtMemberPhone.ClientID %>").focus();
                return false;
            }
            if (!$("#<%=txtMemberPwd1.ClientID %>").val()) {
                $("#<%=lblPwd1Info.ClientID %>").html("请输入登录密码").css("color", "red");
                $("#<%=txtMemberPwd1.ClientID %>").focus();
                return false;
            }
            if (!$("#<%=txtMemberPwd2.ClientID %>").val()) {
                $("#<%=lblPwd2Info.ClientID %>").html("请输入确认密码").css("color", "red");
                $("#<%=txtMemberPwd2.ClientID %>").focus();
                return false;
            }
            if (!$("#<%=txtChecked.ClientID %>").attr("checked")) {
                alert("须同意乐多分用户协议才能提交注册");
                return false;
            }
            return true;
        } 
    </script>
</asp:Content>
