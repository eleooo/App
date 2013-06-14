<%@ Page Title="积分转账" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.master"
    AutoEventWireup="true" CodeBehind="MyBalanceMove.aspx.cs" Inherits="Eleooo.Web.Member.MyBalanceMove" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
    <script type="text/javascript">
        var btnCancelClick = function () {
            $("#<%=this.txtMoveMember.ClientID %>").val();
            $("#<%=this.txtMoveSum.ClientID %>").val();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="mainTjsj">
        <div class="pointZz">
            <ul>
                <li>我要转给&nbsp;<input type="text" value="" name="txtMoveMember" id="txtMoveMember"
                    runat="server" class="points_txt" maxlength="11" />&nbsp;&nbsp;<span style="color: #0896ae;"
                        id="lblPhoneInfo" runat="server">请输入会员账号</span></li>
                <li>转账数额&nbsp;<input type="text" value="" name="txtMoveSum" id="txtMoveSum" runat="server"
                    class="points_txt" />&nbsp;&nbsp;<span style="color: #0896ae;" id="lblPointInfo"
                        runat="server">请输入整数</span></li>
                <li class="submit">
                    <input type="button" class="tj_Btn" value="确 定" onclick="__doPostBack('Add');" />
                    <span id="lblMessage" runat="server" style="color:Red" /></li>
            </ul>
        </div>
        <div class="clearall pointsDetail">
            <div class="czlistSearch" style="margin: 0 auto;">
                按转账日期：
                <input class="cz_txt" style="width: 90px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateStart" />
                &nbsp;至&nbsp;
                <input class="cz_txt" style="width: 90px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateEnd" />
                &nbsp;&nbsp;
                <asp:RadioButtonList runat="server" ID="rblFlag" RepeatLayout="Flow" RepeatColumns="3">
                    <asp:ListItem Text="全部" Selected="True" Value="0"></asp:ListItem>
                    <asp:ListItem Text="转出" Value="1"></asp:ListItem>
                    <asp:ListItem Text="收到" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:Button runat="server" CssClass="czBtn" ID="btnSearch" Text="" />
            </div>
            <ele:DataListExt ID="view" runat="server" AllowPaging="true" ShowFootPaging="true"
                ShowHeadPaging="true" EmptyDataIsShowHeaderAndFooterTemplate="true">
                <HeaderTemplate>
                    <table class="tbczlist" cellspacing="0" cellpadding="0">
                        <thead>
                            <tr>
                                <th style="width: 100px;">
                                    日期
                                </th>
                                <th style="width: 170px;">
                                    会员名称
                                </th>
                                <th style="width: 170px;">
                                    会员账号
                                </th>
                                <th>
                                    积分额度
                                </th>
                                <th>
                                    备注
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
                            <%#Eval("MoverDate","{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%#Eval("MoverMemberIDPoint", 0)%>
                        </td>
                        <td>
                            <%#Eval("MoverMemberIDPoint", 1)%>
                        </td>
                        <td>
                            <%#Eval("MoverMemberIDPoint")%>
                        </td>
                        <td>
                            <%#Eval("MoverMemberIDPoint",2)%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="os">
                        <td>
                            <%#Eval("MoverDate", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            <%#Eval("MoverMemberIDPoint", 0)%>
                        </td>
                        <td>
                            <%#Eval("MoverMemberIDPoint", 1)%>
                        </td>
                        <td>
                            <%#Eval("MoverMemberIDPoint")%>
                        </td>
                        <td>
                            <%#Eval("MoverMemberIDPoint",2)%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </ele:DataListExt>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBottomScript"
    runat="server">
</asp:Content>
