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
                        <td width="10%">
                            促销时段
                        </td>
                        <td width="3%">
                            数量
                        </td>
                        <td width="9%">
                            手机
                        </td>
                        <td width="25%">
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
                            <p>
                                <%= (CompanyItem.WorkingHours ?? "&nbsp;").Replace(",","</p><p>") %>
                            </p>
                        </td>
                        <td>
                            1
                        </td>
                        <td>
                            <%=CurrentUser.MemberPhoneNumber %>
                        </td>
                        <td>
                            <input type="text" id="txtAddr" runat="server" class="input_w" readonly="readonly" style="width: 200px; margin-left: 0px;" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="jfqg-bottom">
            <span>支付：<b class="yellow"><%=this.CompanyItem.ItemPoint.Value.ToString("0.###")%></b>分
                <label id="ctNeedPay" runat="server">
                    +&nbsp;<b class="yellow"><%=(this.CompanyItem.ItemNeedPay ?? (decimal?)0).Value.ToString("0.###")%></b>元</label>
            </span><a href="javascript:void(0)" onclick="__doPostBack('Add');" id="btnPost" runat="server">
                <img alt="" border="0" src="/App_Themes/ThemesV2/images/xd-aniu-cs3.png" /></a>
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
            if (message && message.length > 0) {
                alert(message);
                document.location.href = "/Public/OrderMealPage.aspx?MansionId=<%=MansionId %>";
            }
        });
    </script>
</asp:Content>
