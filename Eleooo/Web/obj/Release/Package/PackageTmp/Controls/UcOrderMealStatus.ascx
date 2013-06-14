<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcOrderMealStatus.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcOrderMealStatus" %>
<div class="zfyx-title">
    <span>看看送餐进度</span></div>
<asp:Repeater ID="rpStatus" runat="server">
    <HeaderTemplate>
        <div class="zfyx-main">
            <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li><span <%# GetItemCssClass(Container.ItemIndex,Container.DataItem) %>><%#Eval("Date", "{0:HH:mm:ss}")%></span>
        <div><%#Eval("Desc") %></div>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        <li class="s_gray">*实时更新送餐进度</li>
        </ul> </div>
    </FooterTemplate>
</asp:Repeater>
<div style="text-align: center; padding-top: 15px;" id="ShowBeforConfirm" runat="server"
    visible="false">
    <a href="javascript:void(0)" title="修改订单" onclick="orderMeal.editOrder();">
        <img border="0" src="/App_Themes/ThemesV2/images/xgdd.png" alt="" /></a> <a href="javascript:void(0)"
            title="取消订单" onclick="orderMeal.cancelOrder();">
            <img border="0" src="/App_Themes/ThemesV2/images/qsdd.png" alt="" /></a>
</div>
<div style="text-align: center; padding-top: 15px;" id="ShowAfterConfirm" runat="server"
    visible="false">
    <a href="javascript:void(0)" title="帮忙催一下" onclick="orderMeal.ugrentOrder();">
        <img border="0" src="/App_Themes/ThemesV2/images/bmcyx.png" alt="" /></a> <a href="javascript:void(0)"
            title="已经收到了" onclick="orderMeal.completeOrder();">
            <img border="0" src="/App_Themes/ThemesV2/images/yjsdl.png" alt="" /></a>
</div>
<div style="text-align: center; padding-top: 15px;" id="ShowWhenNonOut" runat="server"
    visible="false">
    <a href="javascript:void(0)" title="不外送" onclick="orderMeal.nonOutCompany();">
        <img border="0" src="/App_Themes/ThemesV2/images/ok.png" alt="" /></a>
</div>
<div style="text-align: center; padding-top: 15px;" id="ShowWhenOutOfStock" runat="server"
    visible="false">
    <a href="javascript:void(0)" title="重新下单" onclick="orderMeal.redoOrder();">
        <img border="0" src="/App_Themes/ThemesV2/images/cxxd.png" alt="" /></a> <a href="javascript:void(0)"
            title="取消订单" onclick="orderMeal.cancelOrder();">
            <img border="0" src="/App_Themes/ThemesV2/images/qsdd.png" alt="" /></a>
</div>
<div id="ShowWhenOrderCanceled" runat="server" visible="false" style="text-align: center; padding-top: 15px;" >
    <div id="OrderCanceled" style="display:none;" />
</div>
