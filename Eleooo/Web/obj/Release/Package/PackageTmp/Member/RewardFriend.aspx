<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.master"
    AutoEventWireup="true" CodeBehind="RewardFriend.aspx.cs" Inherits="Eleooo.Web.Member.RewardFriend" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="mainTjsj ">
        <div class="tjlcbg">
            <div class="tjlcbgTipword">
                <%=this.RewardMemo %>
                直接把这个链接http://www.eleooo.com/Public/Register.aspx?uid=<%=this.CurrentUser.Id %>发给朋友就可以了，<a
                    class="c_fb7c04 fontBold" href="/Public/Register.aspx?uid=<%=this.CurrentUser.Id %>">开始邀请&gt;&gt;</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript" runat="server">
</asp:Content>
