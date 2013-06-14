<%@ Page Title="我的推荐" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.master"
    AutoEventWireup="true" CodeBehind="MyCompanyR.aspx.cs" Inherits="Eleooo.Web.Member.MyCompanyR" %>

<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderPagePos" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="mainTjsj ">
        <h1 class="mainlistTil">
             我的推荐
            <div class="mainlistTilTip">
                <img alt="生活圈" src="/App_Themes/Member/images/xsjTip1.png" />
            </div>
        </h1>
        <div class="my_rec_table">
            <br />
            <div class="czlistSearch" style="margin: 0 auto;">
                        按推荐日期查询：
                        <input class="cz_txt" style="width: 70px;" onclick="WdatePicker()" runat="server"
                            type="text" id="txtDateStart" />
                        &nbsp;至&nbsp;
                        <input class="cz_txt" style="width: 70px;" onclick="WdatePicker()" runat="server"
                            type="text" id="txtDateEnd" />
                        &nbsp; 按商家名称查询：
                        <input runat="server" style="width: 120px;" id="txtCompanyName" type="text" class="cz_txt cz_sj_txt" />
                        &nbsp;
                        <asp:Button runat="server" CssClass="czBtn" ID="btnSearch" Text="" />
            </div>
            <uc:UcGridView ID="gridView" CssClass="tbczlist" runat="server" IsShowPageAtFoot="true"
                IsShowPageAtHead="false" AllowPaper="true" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBottomScript"
    runat="server">
</asp:Content>
