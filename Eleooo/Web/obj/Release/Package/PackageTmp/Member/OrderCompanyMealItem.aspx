<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master" AutoEventWireup="true" CodeBehind="OrderCompanyMealItem.aspx.cs" Inherits="Eleooo.Web.Member.OrderCompanyMealItem" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %><%@ Import Namespace="Eleooo.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <ele:ResLink ID="rs2" runat="server" Src="/App_Themes/ThemesV2/css/inc_2.css" ReplaceSrc="~/App_Themes/ThemesV2/css/inc.css" /><ele:ResLink ID="rs1" runat="server"
        Src="/App_Themes/ThemesV2/css/content_2.css" />
    <style type="text/css">
        .STYLE2 { color: #ff8a00; font-weight: bold; font-size: 14px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="main">
        <div class="jfqg">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr style="background: #f3f3f3;">
                        <td width="44%">
                            优惠描述
                        </td>
                        <td width="4%">
                            原价
                        </td>
                        <td width="6%">
                            积分兑换
                        </td>
                        <td width="4%">
                            数量
                        </td>
                        <td width="10%">
                            手机
                        </td>
                        <td width="27%">
                            送餐地址
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="tbtu">
                                <div class="tbtu-left">
                                    <img alt="" src="<%=this.CompanyItem.ItemPic %>" width="149px" height="111px" /></div>
                                <div class="tbtu-right" style="width: 245px;">
                                    <p>
                                        <%=  GetFormatItemContent()%></p>
                                    <p style="padding-top: 10px; word-spacing: 0.4em">
                                        已抢购<span class="STYLE2"><%=this.CompanyItem.ItemClicked %></span> 剩<span class="STYLE2"><%=this.CompanyItem.ItemAmount - this.CompanyItem.ItemClicked%></span></p>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </td>
                        <td>
                            ￥<%=this.CompanyItem.ItemSum.Value.ToString("#####.###")%>
                        </td>
                        <td>
                            <%=this.CompanyItem.ItemPoint.Value.ToString("####0.###")%>
                        </td>
                        <td>
                            1
                        </td>
                        <td>
                            <%=CurrentUser.MemberPhoneNumber %>
                        </td>
                        <td align="left">
                            <div style="height: 30px; padding-left: 20px;">
                                <input type="text" id="txtAddr" runat="server" class="input_w" readonly="readonly" /></div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="jfqg-bottom">
            <span>积分支付：<b class="yellow"><%=this.CompanyItem.ItemPoint.Value.ToString("#####.###")%></b>分</span> 
            <span>现金支付：<b class="yellow"><%=(this.CompanyItem.ItemNeedPay ?? (decimal?)0).Value.ToString("#####.###")%></b>元</span>
            <a href="javascript:void(0)" onclick="__doPostBack('Add');" id="btnPost" runat="server">
                <img alt="" border="0" src="/App_Themes/ThemesV2/images/xd-aniu-cs2.png" /></a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            //            var addrCtr = $("#<%=txtAddr.ClientID %>");
            //            var mansionName = "<%=Mansion != null ? Mansion.Name : string.Empty %>";
            //            function _checkUserAddrInput() {
            //                setTimeout(function () {
            //                    var val = addrCtr.val();
            //                    if (!val || val.indexOf(mansionName) == -1)
            //                        addrCtr.val(mansionName);
            //                }, 100);
            //            }
            //            var el = document.getElementById(addrCtr.attr("id"));
            //            if ("\v" == "v") {
            //                el.onpropertychange = _checkUserAddrInput;
            //            } else {
            //                el.addEventListener("input", _checkUserAddrInput, false);
            //            }

            var message = "<%=ValidateMessage %>";
            if (message && message.length > 0)
                alert(message);
        });
    </script>
</asp:Content>
