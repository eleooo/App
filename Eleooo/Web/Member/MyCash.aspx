<%@ Page Title="储值商家" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="MyCash.aspx.cs" Inherits="Eleooo.Web.Member.MyCash" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainTjsj">
        <div class="czlist">
            <div class="czlistSearch">
                按商家名称：
                <input runat="server" id="txtCompanyName" style="width: 120px;" type="text" class="cz_txt cz_sj_txt" />
                <asp:Button runat="server" CssClass="czBtn" ID="btnSearch" Text="" />
            </div>
            <ele:datalistext id="view" runat="server" allowpaging="true" showfootpaging="true"
                showheadpaging="true" EmptyDataIsShowHeaderAndFooterTemplate="true">
                <HeaderTemplate>
                    <table class="tbczlist" cellspacing="0" cellpadding="0">
                        <thead>
                            <tr>
                                <th style="width: 253px;">
                                    商家名称
                                </th>
                                <th style="width: 229px;">
                                    储值金额
                                </th>
                                <th>
                                    储值余额
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th colspan="3">
                            </th>
                        </tr>
                    </tfoot>
                    </table>
                </FooterTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("CompanyName")%>
                        </td>
                        <td>
                            <%#Eval("CashSum")%>
                        </td>
                        <td>
                            <%#Eval("Balance") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="os">
                        <td>
                            <%#Eval("CompanyName")%>
                        </td>
                        <td>
                            <%#Eval("CashSum")%>
                        </td>
                        <td>
                            <%#Eval("Balance") %>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </ele:datalistext>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
</asp:Content>
