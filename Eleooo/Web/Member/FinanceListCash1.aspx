<%@ Page Title="现金明细" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.Master"
    AutoEventWireup="true" CodeBehind="FinanceListCash1.aspx.cs" Inherits="Eleooo.Web.Member.FinanceListCash1" %>

<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="mainTjsj">
        <h1 class="mainlistTil">
            现金明细
            <div class="mainlistTilTip">
                <img alt="" src="/App_Themes/Member/images/cztip.png" />
            </div>
        </h1>
        <div class="czlist">
            <div class="czlistSearch">
                按消费日期查询：
                <input class="cz_txt" style="width: 70px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateStart" />
                &nbsp;至&nbsp;
                <input class="cz_txt" style="width: 70px;" onclick="WdatePicker()" runat="server"
                    type="text" id="txtDateEnd" />
                &nbsp;&nbsp; 按商家名称查询：
                <input runat="server" id="txtCompanyName" style="width: 120px;" type="text" class="cz_txt cz_sj_txt" />
                <asp:Button runat="server" CssClass="czBtn" ID="btnSearch" Text="" />
            </div>
            <uc:UcGridView ID="gridView" runat="server" AllowPaper="true" CssClass="tbczlist"
                IsShowPageAtFoot="true" IsShowPageAtHead="true" />
        </div>
    </div>
    <div id="footerTemplate" runat="server" visible="false">
        <tr>
            <th class="ta_l fontNormal pl_10" colspan="{0}">
                <span class="fontBold">说明：</span>本期间共使用了{1}元现金.
            </th>
        </tr>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript">
</asp:Content>
