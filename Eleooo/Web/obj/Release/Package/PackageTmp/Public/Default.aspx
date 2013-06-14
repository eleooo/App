<%@ Page Language="C#" Title="管理员登录" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Eleooo.Web.Public.Default"
    MasterPageFile="~/MasterPage/MasterPageBase.Master" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcMenuNavigation.ascx" TagName="UcMenuNavigation" TagPrefix="uc" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="/App_Themes/Admin/images/style.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/Admin/content.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/Admin/inc.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/jquery.tipswindown/jquery.tipswindown.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jquery.tipswindown/jquery.tipswindown.js" type="text/javascript"></script>
    <script type="text/javascript">
        validateInput = function(){
            $("#lblUserNameInfo").html('');
            $("#lblPassWordInfo").html('');
            $("#lblCheckOutInfo").html('');
            if(!$("#edtUserName").val()){
                $("#lblUserNameInfo").html('<%=ResBLL.Res["check_UserName_empty"] %>');
                $("#edtUserName").focus();
                return false;
            } 
            if(!$("#edtPassWord").val()){
                $("#lblPassWordInfo").html('<%=ResBLL.Res["check_PassWord_empty"] %>');
                $("#edtPassWord").focus();
                return false;
            }   
            if(!$("#edtCheckOut").val()){
                $("#lblCheckOutInfo").html('<%=ResBLL.Res["check_Code_empty"] %>');
                $("#edtCheckOut").focus();
                return false;
            }
            return true;                        
        }
        var url = "/Public/CreateCodeHandler.ashx";
        $(document).ready(function () {
//            $("#lnkSign").click(function () {
//                var url = "/Public/Sign.aspx";
//                tipsWindown("<%=ResBLL.Res["win_user_reg_title"] %>", "iframe:" + url, 400, 250, "true", "", "true", "");
//            });
            $("#edtCheckOut").keydown(function(){
                if(arguments[0].keyCode==13){
                    __doPostBack('Login');
                }
            });
            $("#codeImg").click(function () {
                var date = (new Date()).valueOf();
                $("#codeImg").attr("src",url+"?sn="+date);
            });
            $("#codeImg").attr("src",url+"?sn="+(new Date()).valueOf());
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolderTop">
    <div class="top">
        <div class="logo">
            <a href="/">
                <img alt="" src="/App_Themes/admin/images/Default_images/logo.jpg" width="980" height="158"
                    border="0" /></a></div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolderMain">
    <div class="bg-k">
        <div class="dl-k">
            <div class="dl">
                <ul>
                    <li><span>
                        <%=ResBLL.Res["login_name"] %></span>
                        <input type="text" maxlength="11" value="<%=this.Params["edtUserName"] %>" name="edtUserName"
                            id="edtUserName" size="20" />
                        <label class="red" id="lblUserNameInfo">
                            <%=this.Message["check_UserName"]%>
                        </label>
                    </li>
                    <li><span>
                        <%=ResBLL.Res["login_pass"] %></span>
                        <input type="password" maxlength="6" value="<%=this.Params["edtPassWord"] %>" name="edtPassWord"
                            id="edtPassWord" size="20" />
                        <label class="red" id="lblPassWordInfo">
                            <%=this.Message["check_password"]%>
                        </label>
                        
                    </li>
                    <li><span>
                        <%=ResBLL.Res["login_code"] %></span>
                        <input type="text" class="Login_input" name="edtCheckOut" id="edtCheckOut" size="10" />
                        <img src="" id="codeImg" alt="" title="" />
                        <label class="red" id="lblCheckOutInfo">
                            <%=this.Message["check_login_code"]%>
                        </label>
                        <input type="hidden" id="sys_admin" name="subSys" value="Admin" />
                    </li>
                    <li>
                        <div class="tt">
                            <div class="tb">
                                <img alt="login" border="0" style="height: 28px; width: 65px; border-width: 0px;
                                    cursor: pointer;" src="/App_Themes/admin/images/Default_images/dl.jpg" id="btnLogin"
                                    name="btnLogin" onclick="__doPostBack('Login');" />
                            </div>
  <%--                          <div class="tb">
                                <a href="javascript:void(0)" id="lnkSign">
                                    <%=ResBLL.Res["login_sign"] %></a>
                            </div>--%>
                        </div>
                    </li>
                    <li>
                        <label class="red" id="lblMessage">
                            <%=this.Message["Message"]%>
                        </label>
                    </li>
                </ul>
            </div>
        </div>
        <%if (!IsDialog)
          {%>
        <uc:UcMenuNavigation ID="navFoot" runat="server" Region="Foot"></uc:UcMenuNavigation>
        <div class="bottom01">
            <%=ResBLL.Res["page_bottom"]%></div>
        <div class="bottom02">
            <img alt="" src="/App_Themes/Admin/images/Default_images/bottom01.jpg" width="994"
                height="28" /></div>
        <%} %>
    </div>
    <asp:LinkButton runat="server" />
</asp:Content>
