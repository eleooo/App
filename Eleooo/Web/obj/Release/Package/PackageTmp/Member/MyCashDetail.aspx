<%@ Page Title="储值记录" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="MyCashDetail.aspx.cs" Inherits="Eleooo.Web.Member.MyCashDetail" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainTjsj">
        <div class="czlist">
            <div class="czlistSearch">
                按充值日期：
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
                ShowHeadPaging="true" EmptyDataIsShowHeaderAndFooterTemplate="true">
                <HeaderTemplate>
                    <table class="tbczlist" cellspacing="0" cellpadding="0">
                        <thead>
                            <tr>
                                <th style="width: 123px;">
                                    充值日期
                                </th>
                                <th style="width: 253px;">
                                    充值金额
                                </th>
                                <th style="width: 157px;">
                                    商家名称
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
                            <%#Eval("CashDate", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%#Eval("CashSum")%>
                        </td>
                        <td>
                            <%#Eval("CompanyName") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="os">
                        <td>
                            <%#Eval("CashDate", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%#Eval("CashSum")%>
                        </td>
                        <td>
                            <%#Eval("CompanyName") %>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </ele:DataListExt>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
</asp:Content>
