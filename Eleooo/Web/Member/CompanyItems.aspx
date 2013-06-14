<%@ Page Title="我要抢优惠-优惠列表" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="CompanyItems.aspx.cs" Inherits="Eleooo.Web.Member.CompanyItems" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="eleooo" %>
<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<%@ Register Src="~/Controls/UcTypeAndAreaFilter.ascx" TagName="UcTypeAndAreaFilter"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <uc:UcTypeAndAreaFilter ID="filter" runat="server" />
    <div class="mainlist ">
        <h1 class="mainlistTil">
            <%=this.CurrentNav.NavName %>
            <div class="mainlistTilTip">
                <img alt="" src="/App_Themes/Member/images/myqyhTip2.png" />
            </div>
        </h1>
        <h3 class="mainlistChildTil">
            <div class="mainlistChildTilNew">
            </div>
            <span class="fr mr_10"></span>最新优惠</h3>
        <eleooo:ListViewEx ID="gridView" runat="server" AllowPaper="true" AlternatingRow="3"
            IsShowPageAtFoot="true" IsShowPageAtHead="false" PageSize="12" IsShowHeader="false">
            <ViewBeginTemplate>
                <ul class="clearall qcoupon">
            </ViewBeginTemplate>
            <ViewEndTemplate>
                </ul></ViewEndTemplate>
            <ViewRowTemplate>
                <li><a href="/Public/ViewItemDetail.aspx?ItemID={5}">
                    <img alt="" src="{0}" /></a>
                    <div class="workDetail">
                        <a href="/Public/ViewItemDetail.aspx?ItemID={5}">{2}</a>
                    </div>
                    <div class="workOhter">
                        <div class="pricePoints">
                            <p>
                                {3}元</p>
                            <p>
                                仅需{4}个乐多分</p>
                        </div>
                        <div class="myBuy">
                            <a href="/Public/ViewItemDetail.aspx?ItemID={5}">{9}</a>
                        </div>
                        <div class="address">
                            限{1}{8}会员</div>
                        <div class="syqg">
                            已{10}<span class="c_f39d08">{6}</span> 剩<span class="c_f39d08">{7}</span></div>
                    </div>
                </li>
            </ViewRowTemplate>
            <ViewAlternatingRowTemplate>
                <li class="last"><a href="/Public/ViewItemDetail.aspx?ItemID={5}">
                    <img alt="" src="{0}" /></a>
                    <div class="workDetail">
                        <a href="/Public/ViewItemDetail.aspx?ItemID={5}">{2}</a>
                    </div>
                    <div class="workOhter">
                        <div class="pricePoints">
                            <p>
                                {3}元</p>
                            <p>
                                仅需{4}个乐多分</p>
                        </div>
                        <div class="myBuy">
                            <a href="/Public/ViewItemDetail.aspx?ItemID={5}">{9}</a>
                        </div>
                        <div class="address">
                            限{1}{8}会员</div>
                        <div class="syqg">
                            已{10}<span class="c_f39d08">{6}</span> 剩<span class="c_f39d08">{7}</span></div>
                    </div>
                </li>
            </ViewAlternatingRowTemplate>
        </eleooo:ListViewEx>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
</asp:Content>
