<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcOrderMealSelectCompany.ascx.cs" Inherits="Eleooo.Web.Controls.UcOrderMealSelectCompany" %><%@ Import
    Namespace="Eleooo.Web" %><%@ Register Src="~/Controls/UcOrderMealFlow.ascx" TagPrefix="uc" TagName="UcOrderMealFlow" %><%@ Register Namespace="Eleooo.Web.Controls"
        Assembly="Eleooo.Web" TagPrefix="ele" %><ele:ResLink ID="rs1" Src="/Scripts/acbox/jquery.ajaxComboBox.6.1.js" runat="server" /><ele:ResLink ID="ResLink1" Src="/Scripts/acbox/Style.css"
            runat="server" /><uc:UcOrderMealFlow ID="orderMealFlow" runat="server" />
<ele:DataListExt ID="rpJf" runat="server" AllowPaging="false" AlternatingItemIndex="3" EmptyDataIsShowHeaderAndFooterTemplate="false">
    <HeaderTemplate>
        <div class="jf">
            <div class="jf-title">
                <div class="jf-new">
                    <img src="/App_Themes/ThemesV2/images/jf-new.png" alt="" /></div>
                <span>积分抢购</span>
                <div class="jf_select">
                    <input name="cbjf" value="0" <% RenderCheckBox(0,'0'); %> type="radio" />全部
                    <input name="cbjf" value="1" <% RenderCheckBox(0,'1'); %> type="radio" />热卖中
                    <input name="cbjf" value="2" <% RenderCheckBox(0,'2'); %> type="radio" />免送餐费</div>
            </div>
            <div class="jf-main">
                <ul>
    </HeaderTemplate>
    <FooterTemplate>
        </ul>
        <div class="clear">
        </div>
        </div> </div></FooterTemplate>
    <ItemTemplate>
        <li>
            <div class="jf-main-tu">
                <a href="/Member/OrderCompanyMealItem.aspx?ItemID=<%# Eval("ItemID") %>&MansionId=<%=MansionId %>" target="_blank">
                    <img width="238" height="149" border="0" src="<%#Eval("ItemPic") %>" alt="" /></a></div>
            <div class="jf-main-w">
                <h3>
                    <a href="/Member/OrderCompanyMealItem.aspx?ItemID=<%# Eval("ItemID") %>&MansionId=<%=MansionId %>" target="_blank">
                        <%# Formatter.SubStr(Eval("ItemTitle"),20) %></a></h3>
                <div class="jf-mian-bq">
                    <span>原价<%# Eval("ItemSum") %>元</span>
                    <p>
                        <%#Eval("ItemPoint","{0:0.##}") %>分<%# Formatter.Join("+",Formatter.ValueIf<string>(Utilities.ToDecimal(Eval("ItemNeedPay")).ToString("0.##"), "0", null),"元即享") ?? "兑换"%></p>
                </div>
                <div class="jf-main-aniu">
                    <a target="_blank" href="/Member/OrderCompanyMealItem.aspx?ItemID=<%# Eval("ItemID") %>&MansionId=<%=MansionId %>">
                        <img alt="" width="59" height="32" border="0" src="/App_Themes/ThemesV2/images/<%# Utilities.ToDecimal(Eval("ItemClicked")) < Utilities.ToDecimal(Eval("ItemAmount")) ? "wyq.png":"wyq_out.png"%>" /></a></div>
            </div>
        </li>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <li>
            <div class="jf-main-tu01">
                <a href="/Member/OrderCompanyMealItem.aspx?ItemID=<%# Eval("ItemID") %>&MansionId=<%=MansionId %>" target="_blank">
                    <img width="238" height="149" border="0" src="<%#Eval("ItemPic") %>" alt="" /></a></div>
            <div class="jf-main-w01">
                <h3>
                    <a href="/Member/OrderCompanyMealItem.aspx?ItemID=<%# Eval("ItemID") %>&MansionId=<%=MansionId %>" target="_blank">
                        <%# Formatter.SubStr(Eval("ItemTitle"),20) %></a></h3>
                <div class="jf-mian-bq">
                    <span>
                        <%# Eval("ItemSum") %>元</span>
                    <p>
                        <%#Eval("ItemPoint", "{0:0.##}")%>分<%# Formatter.Join("+", Formatter.ValueIf<string>(Utilities.ToDecimal(Eval("ItemNeedPay")).ToString("0.##"), "0", null), "元即享") ?? "兑换"%></p>
                </div>
                <div class="jf-main-aniu">
                    <a target="_blank" href="/Member/OrderCompanyMealItem.aspx?ItemID=<%# Eval("ItemID") %>&MansionId=<%=MansionId %>">
                        <img alt="" width="59" height="32" border="0" src="/App_Themes/ThemesV2/images/<%# Utilities.ToDecimal(Eval("ItemClicked")) < Utilities.ToDecimal(Eval("ItemAmount")) ? "wyq.png":"wyq_out.png"%>" /></a></div>
            </div>
        </li>
    </AlternatingItemTemplate>
</ele:DataListExt>
<ele:DataListExt ID="rpFavCompany" runat="server" AllowPaging="false" AlternatingItemIndex="4" EmptyDataIsShowHeaderAndFooterTemplate="false">
    <HeaderTemplate>
        <div class="rm">
            <div class="rm-title">
                <div class="jf-new">
                    <img src="/App_Themes/ThemesV2/images/jf-hot.png" alt="" /></div>
                <span>我的餐厅</span>
                <div class="jf_select">
                    <input name="cbMyCompany" value="0" <%RenderCheckBox(1,'0'); %> type="radio" />全部
                    <input name="cbMyCompany" value="1" <%RenderCheckBox(1,'1'); %> type="radio" />营业中
                    <input name="cbMyCompany" value="2" <%RenderCheckBox(1,'2'); %> type="radio" />免送餐费</div>
            </div>
            <div class="rm-main">
                <ul>
    </HeaderTemplate>
    <FooterTemplate>
        </ul>
        <div class="clear">
        </div>
        </div> </div></FooterTemplate>
    <ItemTemplate>
        <li>
            <div class="rm-main-tu">
                <%#BindCompanyInfo()%>
                <a href="/Public/OrderMealPage.aspx?CompanyId=<%#Eval("ID") %>&MansionId=<%=MansionId %>" target="_blank">
                    <img border="0" src="<%#Eval("CompanyPhoto") %>" alt="" /></a></div>
            <div class="rm-main-w">
                <div class="rm-main-w-tu">
                    <a href="javascript:void(0);" title="从我的餐厅删除" onclick="__doPostBack('Delete',<%#Eval("ID") %>);"></a>
                </div>
                <p>
                    外卖时间：<%#Eval("CompanyWorkTime") %></p>
                <p>
                    起送金额：<%#Eval("OnSetSum") %>元</p>
                <p>
                    送餐速度：约<%#Eval("OrderElapsed")%>分钟</p>
            </div>
        </li>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <li class="wx01">
            <div class="rm-main-tu">
                <%#BindCompanyInfo()%>
                <a href="/Public/OrderMealPage.aspx?CompanyId=<%#Eval("ID") %>&MansionId=<%=MansionId %>" target="_blank">
                    <img border="0" src="<%#Eval("CompanyPhoto") %>" alt="" /></a></div>
            <div class="rm-main-w01">
                <div class="rm-main-w-tu">
                    <a href="javascript:void(0);" title="从我的餐厅删除" onclick="__doPostBack('Delete',<%#Eval("ID") %>);"></a>
                </div>
                <p>
                    外卖时间：<%#Eval("CompanyWorkTime") %></p>
                <p>
                    起送金额：<%#Eval("OnSetSum") %>元</p>
                <p>
                    送餐速度：约<%#Eval("OrderElapsed")%>分钟</p>
            </div>
        </li>
    </AlternatingItemTemplate>
</ele:DataListExt>
<ele:DataListExt ID="rpRmCompany" runat="server" AllowPaging="false" AlternatingItemIndex="4" EmptyDataIsShowHeaderAndFooterTemplate="false">
    <HeaderTemplate>
        <div class="rm">
            <div class="rm-title">
                <div class="jf-new">
                    <img src="/App_Themes/ThemesV2/images/jf-hot.png" alt="" /></div>
                <span>热卖餐厅</span>
                <div class="jf_select">
                    <input name="cbCompany" value="0" <%RenderCheckBox(2,'0'); %> type="radio" />全部
                    <input name="cbCompany" value="1" <%RenderCheckBox(2,'1'); %> type="radio" />营业中
                    <input name="cbCompany" value="2" <%RenderCheckBox(2,'2'); %> type="radio" />免送餐费
                </div>
            </div>
        </div>
        <div class="rm-main">
            <ul>
    </HeaderTemplate>
    <FooterTemplate>
        </ul>
        <div class="clear">
        </div>
        </div> </div></FooterTemplate>
    <ItemTemplate>
        <li>
            <div class="rm-main-tu">
                <%#BindCompanyInfo()%>
                <a href="/Public/OrderMealPage.aspx?CompanyId=<%#Eval("ID") %>&MansionId=<%=MansionId %>" target="_blank">
                    <img border="0" src="<%#Eval("CompanyPhoto") %>" alt="" /></a></div>
            <div class="rm-main-w">
                <p>
                    外卖时间：<%#Eval("CompanyWorkTime") %></p>
                <p>
                    起送金额：<%#Eval("OnSetSum") %>元</p>
                <p>
                    送餐速度：约<%#Eval("OrderElapsed")%>分钟</p>
            </div>
        </li>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <li class="wx01">
            <div class="rm-main-tu">
                <%#BindCompanyInfo()%>
                <a href="/Public/OrderMealPage.aspx?CompanyId=<%#Eval("ID") %>&MansionId=<%=MansionId %>" target="_blank">
                    <img border="0" src="<%#Eval("CompanyPhoto") %>" alt="" /></a></div>
            <div class="rm-main-w01">
                <p>
                    外卖时间：<%#Eval("CompanyWorkTime") %></p>
                <p>
                    起送金额：<%#Eval("OnSetSum") %>元</p>
                <p>
                    送餐速度：约<%#Eval("OrderElapsed")%>分钟</p>
            </div>
        </li>
    </AlternatingItemTemplate>
</ele:DataListExt>
<script type="text/javascript" language="javascript">
    $(".main input[type='radio']").click(function () {
        setTimeout(function () {
            __doPostBack('Query', '');
        }, 100);
        return true;
    });
</script>
