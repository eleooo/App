<%@ Page Title="推荐奖励" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.master"
    AutoEventWireup="true" CodeBehind="MyReward.aspx.cs" Inherits="Eleooo.Web.Member.MyReward" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainTjsj">
        <div class="czlist">
            <div class="czlistSearch">
                按日期查询：
                <input class="cz_txt" style="width: 90px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateStart" />
                &nbsp;至&nbsp;
                <input class="cz_txt" style="width: 90px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateEnd" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" CssClass="czBtn" ID="btnSearch" Text="" />
            </div>
            <ele:DataListExt ID="view" runat="server" AllowPaging="true" ShowFootPaging="true"
                ShowHeadPaging="false">
                <HeaderTemplate>
                    <table class="tbczlist" cellspacing="0" cellpadding="0">
                        <thead>
                            <tr>
                                <th width="80">
                                    日期
                                </th>
                                <th width="110">
                                    好友账号
                                </th>
                                <th width="250" align="center">
                                    消费地点
                                </th>
                                <th width="80">
                                    消费金额
                                </th>
                                <th width="80">
                                    奖励积分
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th colspan="5">
                            </th>
                        </tr>
                    </tfoot>
                    </table>
                </FooterTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("RewardDate","{0:yyyy-MM-dd}") %>
                        </td>
                        <td>
                            <%#Eval("MemberPhoneNumber") %>
                        </td>
                        <td>
                            <%#Eval("CompanyName") %>
                        </td>
                        <td>
                            <%#Eval("OrderSumOk") %>元
                        </td>
                        <td>
                            <%#Eval("RewardPoint") %>分
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="os">
                        <td>
                            <%#Eval("RewardDate","{0:yyyy-MM-dd}") %>
                        </td>
                        <td>
                            <%#Eval("MemberPhoneNumber") %>
                        </td>
                        <td>
                            <%#Eval("CompanyName") %>
                        </td>
                        <td>
                            <%#Eval("OrderSumOk") %>元
                        </td>
                        <td>
                            <%#Eval("RewardPoint") %>分
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </ele:DataListExt>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
</asp:Content>
