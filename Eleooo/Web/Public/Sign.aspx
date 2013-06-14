<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sign.aspx.cs" Inherits="Eleooo.Web.Public.Sign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员注册</title>
    <link href="/App_Themes/admin/inc.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
    <script type="text/JavaScript">
		<!--

        $(document).ready(function () {
            $("#txtMemberPhone").keyup(function () {

                if ($(this).val().length > 10) {
                    var spFinger1 = "0";
                    var filePath = "/WebService.asmx/GetMemberFinger";
                    var data = "MemberPhone=" + $(this).val();

                    if ("13,15,18".indexOf($(this).val().substring(0, 2)) == -1) {
                        $("#lblMemberPhoneInfo").html("<span style='color:Red'>&nbsp;该手机号码无效！</span>")
                        return;
                    }

                    $.ajax({ type: "post", url: filePath, data: data, dataType: "xml", timeout: 50000,
                        success: function (xml) {
                            spFinger1 = $(xml).find("string").text();

                            if (spFinger1 == "-1") {
                                $("#lblMemberPhoneInfo").html("<span style='color: green'>&nbsp;该号码可以注册！</span>")
                            }
                            else {
                                $("#lblMemberPhoneInfo").html("<span style='color:Red'>&nbsp;该号码已经注册！</span>")
                            }
                        }
                    });
                }
            });
        })

        function closeForm() {

            //window.parent.document.getElementById("windownbg").style.display = 'none';
            //window.parent.document.getElementById("windown-box").style.display = 'none';
        }
			
		//-->
    </script>
    <form id="form1" runat="server">
    <div class="ceng-main" runat="server" id="divMain" style="margin-top: 20px">
        <ul>
            <li>
            <div class="w1">所在商圈：</div>
            <span id="areaContainer" runat="server"></span>
            </li>
            <li>
                <div class="w1" runat="server" id="lblMemberName">
                    您的姓名：</div>
                <asp:TextBox class="Login_input" ID="txtMemberName" size="20" MaxLength="10" runat="server" />
                <span style="color: Red">*(必填)</span> </li>
            <li>
                <div class="w1" runat="server" id="lblMemberPhone">
                    您的手机：</div>
                <asp:TextBox class="Login_input" ID="txtMemberPhone" size="20" MaxLength="11" runat="server" /><span
                    id="lblMemberPhoneInfo"><span style="color: Red">*(必填)</span></span></li>
            <li>
                <div class="w1" runat="server" id="lblMemberEmail">
                    您的邮箱：</div>
                <asp:TextBox class="Login_input" ID="txtMemberEmail" size="20" MaxLength="30" runat="server" /></li>
            <li>
                <div class="w1" runat="server" id="lblMemberPwd1">
                    输入密码：</div>
                <asp:TextBox TextMode="Password" class="Login_input" ID="txtMemberPwd1" MaxLength="6"
                    size="20" runat="server" /><span style="color: Red">*(长度不能小于6位)</span></li>
            <li>
                <div class="w1" runat="server" id="lblMemberPwd2">
                    确认密码：</div>
                <asp:TextBox TextMode="Password" class="Login_input" ID="txtMemberPwd2" MaxLength="6"
                    size="20" runat="server" /></li>
            <li>
                <div class="w1">你的性别：</div>
                <asp:RadioButtonList ID="lblSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio">
                    <asp:ListItem Value="true" Text="男" Selected="True" />
                    <asp:ListItem Value="false" Text="女" />
                </asp:RadioButtonList>
            </li>
        </ul>
        <div class="clear" style="clear: both">
        </div>
        <div class="zc_an">
            <asp:ImageButton ID="btnReg" OnClick="btnReg_Click" runat="server" ImageUrl="/App_Themes/admin/images/Default_images/qr.jpg"
                Width="65" Height="28" border="0" /></div>
        <div class="zc_wc">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label></div>
    </div>
    </form>
</body>
</html>
