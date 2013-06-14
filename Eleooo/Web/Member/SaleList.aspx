<%@ Page Title="消费记录" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="SaleList.aspx.cs" Inherits="Eleooo.Web.Member.SaleList" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
    <link href="/Scripts/jquery.tipswindown/jquery.tipswindown.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jquery.tipswindown/jquery.tipswindown.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var action_dlg_url = ["/SiteAppPage/pupSale.aspx?ID={0}"];
        var action_dlg_title = "消费明细";
        var action_dlg_width = 300;
        var action_dlg_height = 150;
    </script>
    <script src="/Scripts/Regaction.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainTjsj">
        <div class="czlist">
            <div class="czlistSearch">
                按消费日期：
                <input class="cz_txt" style="width: 90px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateStart" />
                &nbsp;至&nbsp;
                <input class="cz_txt" style="width: 90px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateEnd" />
                &nbsp;&nbsp; 按商家名称：
                <input runat="server" id="txtCompanyName" style="width: 120px;" type="text" class="cz_txt cz_sj_txt" />
                <asp:Button runat="server" CssClass="czBtn" ID="btnSearch" Text="" />
            </div>
            <ele:DataListExt ID="view" runat="server" AllowPaging="true" ShowFootPaging="true"
                EmptyDataIsShowHeaderAndFooterTemplate="true" ShowHeadPaging="true">
                <HeaderTemplate>
                    <table class="tbczlist" cellspacing="0" cellpadding="0">
                        <thead>
                            <tr>
                                <th style="width: 79px;">
                                    日期
                                </th>
                                <th style="width: 169px;">
                                    消费单号
                                </th>
                                <th style="width: 157px;">
                                    商家名称
                                </th>
                                <th style="width: 82px;">
                                    消费金额
                                </th>
                                <th style="width: 78px;">
                                    返馈比例
                                </th>
                                <th style="width: 83px;">
                                    获赠积分
                                </th>
                                <th>
                                    查看详情
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th class="ta_l fontNormal pl_10" colspan="7">
                                <span class="fontBold">说明：</span>本期间共计消费<%#Eval(0) %>元,并获赠<%#Eval(1) %>个积分
                            </th>
                        </tr>
                    </tfoot>
                    </table>
                </FooterTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("OrderDate","{0:yyyy-MM-dd}") %>
                        </td>
                        <td>
                            <%#Eval("OrderCode",3) %>
                        </td>
                        <td>
                            <%#Eval("CompanyName") %>
                        </td>
                        <td>
                            <%#Eval("OrderSumOk")%>
                        </td>
                        <td>
                            <%#Eval("OrderRate",0)%>
                        </td>
                        <td>
                            <%#Eval("OrderPoint")%>
                        </td>
                        <td>
                            <%#Eval("ID",1)%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="os">
                        <td>
                            <%#Eval("OrderDate","{0:yyyy-MM-dd}") %>
                        </td>
                        <td>
                            <%#Eval("OrderCode",3) %>
                        </td>
                        <td>
                            <%#Eval("CompanyName") %>
                        </td>
                        <td>
                            <%#Eval("OrderSumOk")%>
                        </td>
                        <td>
                            <%#Eval("OrderRate",0)%>
                        </td>
                        <td>
                            <%#Eval("OrderPoint")%>
                        </td>
                        <td>
                            <%#Eval("ID",1)%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </ele:DataListExt>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
</asp:Content>
