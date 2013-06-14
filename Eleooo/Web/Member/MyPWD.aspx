<%@ Page Title="修改密码" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="MyPWD.aspx.cs" Inherits="Eleooo.Web.Member.MyPWD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainBase">
        <div class="left">
            <ul>
                <li><span>旧密码：</span>
                    <div>
                        <asp:TextBox MaxLength="6" CssClass="input" runat="Server" TextMode="Password"
                            ID="txtCompanyPwd"></asp:TextBox></div>
                </li>
                <li><span>新密码：</span>
                    <div>
                        <asp:TextBox MaxLength="6" CssClass="input" TextMode="Password" runat="Server" ID="txtCompanyPwd1"></asp:TextBox></div>
                </li>
                <li><span>请确认新密码：</span>
                    <div>
                        <asp:TextBox MaxLength="6" CssClass="input" TextMode="Password" runat="Server" ID="txtCompanyPwd2"></asp:TextBox></div>
                </li>
                <li>
                    <input type="button" style="margin-left: 105px;" class="bc_btn" onclick="__doPostBack('Edit');"
                        value="确认" accesskey="S" id="btnPost" /></li>
                <li><div id="txtMessage" runat="server" style="color:Red;"></div></li>
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
    <style type="text/css">
        .mainBase ul li span
        {
            width: 100px;
        }
        .mainBase ul li div
        {
            width: 235px;
        }
    </style>
</asp:Content>
