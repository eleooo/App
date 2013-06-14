<%@ Page Title="我要赚积分-我的广告" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="CompanyAds.aspx.cs" Inherits="Eleooo.Web.Member.CompanyAds" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="eleooo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainlist ">
        <h1 class="mainlistTil">
            我的广告
            <div class="mainlistTilTip">
                <img alt="" src="/App_Themes/Member/images/mygzTip.png" />
            </div>
        </h1>
        <eleooo:ListViewEx ID="gridView" runat="server" AllowPaper="true" AlternatingRow="3"
            IsShowPageAtFoot="true" IsShowPageAtHead="false" PageSize="12" IsShowHeader="false">
            <ViewBeginTemplate>
                <ul class="clearall qcoupon">
            </ViewBeginTemplate>
            <ViewEndTemplate>
                </ul></ViewEndTemplate>
           <ViewRowTemplate>
               <li><a href="/Member/ViewCompanyAds.aspx?AdsID={0}">
                   <img src="{1}" alt="" /></a>
                   <div class="workDetail">
                       <a href="/Member/ViewCompanyAds.aspx?AdsID={0}">{2}</a>
                   </div>
                   <div class="workOhter ad">
                       <div class="pricePoints ads">
                           看广告，赚积分<br />
                           最高赚{3}个乐多分
                       </div>
                       <div class="myBuy">
                           <a href="/Member/ViewCompanyAds.aspx?AdsID={0}"></a>
                       </div>
                       <div class="address">限{4}{6}会员</div>
                       <div class="syqg">
                           已浏览<span class="c_f39d08">{5}</span>次</div>
                   </div>
               </li>           
           </ViewRowTemplate>
           <ViewAlternatingRowTemplate>
               <li class="last"><a href="/Member/ViewCompanyAds.aspx?AdsID={0}">
                   <img src="{1}" alt="" /></a>
                   <div class="workDetail">
                       <a href="/Member/ViewCompanyAds.aspx?AdsID={0}">{2}</a>
                   </div>
                   <div class="workOhter ad">
                       <div class="pricePoints ads">
                           看广告，赚积分<br />
                           最高赚{3}个乐多分
                       </div>
                       <div class="myBuy">
                           <a href="/Member/ViewCompanyAds.aspx?AdsID={0}"></a>
                       </div>
                       <div class="address">
                           限{4}{6}会员</div>
                       <div class="syqg">
                           已浏览<span class="c_f39d08">{5}</span>次</div>
                   </div>
               </li>           
           </ViewAlternatingRowTemplate>
        </eleooo:ListViewEx>
        <span id="txtMessage" runat="server" style="color: Red;"></span>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
</asp:Content>
