<%@ Page Title="我要赚积分-我的进账" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="CompanyMyAds.aspx.cs" Inherits="Eleooo.Web.Member.CompanyMyAds" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainTjsj">
        <div class="czlist">
            <div class="czlistSearch">
                按浏览日期：
                <input class="cz_txt" style="width: 90px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateStart" />
                &nbsp;至&nbsp;
                <input class="cz_txt" style="width: 90px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateEnd" />
                &nbsp;&nbsp; 按商家名称：
                <input runat="server" id="txtCompanyName" style="width: 120px;" type="text" class="cz_txt cz_sj_txt" />
                <asp:Button runat="server" CssClass="czBtn" ID="btnSearch" Text="" />
            </div>
            <ele:DataListExt ID="view" runat="server" ShowFootPaging="true" ShowHeadPaging="true">
                <HeaderTemplate>
                    <table class="tbczlist" cellspacing="0" cellpadding="0">
                        <thead>
                            <tr>
                                <th style="width: 83px;">
                                    浏览日期
                                </th>
                                <th style="width: 355px;">
                                    广告描述
                                </th>
                                <th style="width: 120px;">
                                    广告主
                                </th>
                                <th style="width: 80px;">
                                    浏览次数
                                </th>
                                <th style="width: 80px;">
                                    积分收入
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("AdsClickDate", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%#Eval("AdsTitle",0)%>
                        </td>
                        <td>
                            <%#Eval("CompanyName")%>
                        </td>
                        <td>
                            <%#Eval("AdsCount")%>
                        </td>
                        <td>
                            <%#Eval("AdsPointSum")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="os">
                        <td>
                            <%#Eval("AdsClickDate", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%#Eval("AdsTitle",0)%>
                        </td>
                        <td>
                            <%#Eval("CompanyName")%>
                        </td>
                        <td>
                            <%#Eval("AdsCount")%>
                        </td>
                        <td>
                            <%#Eval("AdsPointSum")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th class="ta_l fontNormal pl_10" colspan="7">
                                <span class="fontBold">说明：</span>本期间共计收入了<%#Eval() %>个积分
                            </th>
                        </tr>
                    </tfoot>
                    </table>
                </FooterTemplate>
            </ele:DataListExt>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
</asp:Content>
