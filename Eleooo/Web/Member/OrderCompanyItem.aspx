<%@ Page Title="积分抢购" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="OrderCompanyItem.aspx.cs" Inherits="Eleooo.Web.Member.OrderCompanyItem" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
    <ele:ResLink ID="rs2" runat="server" Src="/App_Themes/ThemesV2/css/inc_2.css" ReplaceSrc="~/App_Themes/ThemesV2/css/inc.css" />
    <ele:ResLink ID="rs1" runat="server" Src="/App_Themes/ThemesV2/css/content_2.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="main">
        <div class="jfqg">
            <asp:HiddenField ID="txtMemberItemID" runat="server" />
            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                <tbody>
                    <tr style="background: rgb(243, 243, 243);">
                        <td width="44%">
                            优惠描述
                        </td>
                        <td width="5%">
                            原价
                        </td>
                        <td width="7%">
                            积分兑换
                        </td>
                        <td width="5%">
                            数量
                        </td>
                        <td width="13%">
                            可否退订
                        </td>
                        <td width="21%">
                            预计到店时间
                        </td>
                        <td width="5%">
                            操作
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="tbtu">
                                <div class="tbtu-left">
                                    <img alt="" src="<%=this.CompanyItem.ItemPic %>" width="149px" height="111px" />
                                </div>
                                <div class="tbtu-right">
                                    <p>
                                       <a href="/Public/ViewItemDetail.aspx?ItemID=<%=this.ItemID %>"> <%=  Formatter.SubStr( this.CompanyItem.ItemTitle,30) %></a></p>
                                    <p style="padding-top: 10px; word-spacing: 0.4em;">
                                        <%=string.Format("{0}至{1}",CompanyItem.ItemDate.Value.ToString("yyyy-MM-dd"),CompanyItem.ItemEndDate.Value.ToString("yyyy-MM-dd")) %>
                                    </p>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </td>
                        <td>
                            <span class="money">￥</span><%=this.CompanyItem.ItemSum.Value.ToString("#####.###")%>
                        </td>
                        <td>
                            <%=this.CompanyItem.ItemPoint.Value.ToString("#####.###")%>
                        </td>
                        <td>
                            1
                        </td>
                        <td>
                            <%=this.CompanyItem.IsCanDel == 1?"支持":"不支持" %>
                        </td>
                        <td align="center">
                            <%=this.RenderOrderDateInput() %>
                        </td>
                        <td>
                            <a href="javascript:__doPostBack('Delete');">删除</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="jfqg-bottom">
            <span>积分支付：<b class="yellow"><%=this.CompanyItem.ItemPoint.Value.ToString("#####.###") %></b>分</span>
            <span id="btnPost" runat="server"><a href="javascript:void(0)" onclick="__doPostBack('<%=this.AddOrEdit %>');">
                <img border="0" src="/App_Themes/ThemesV2/images/xd-aniu-cs2.png" alt="" id="imgButton" runat="server" /></a></span>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var message = "<%=ValidateMessage %>";
            if (message && message.length > 0)
                alert(message);
        });
    </script>
</asp:Content>
