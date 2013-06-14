<%@ Page Title="联盟商家" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="MyCompany.aspx.cs" Inherits="Eleooo.Web.Company.MyCompany" %>

<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px">
                    <uc:UcPagePosition ID="UcPagePosition1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                </td>
            </tr>
        </tbody>
    </table>
    <uc:UcGridView ID="gridView" runat="server" AllowPaper="true" />
    <div id="txtTemplate" runat="server" visible="false">
        <div class="companyInfo">
            <div>
                <label class='companyName'>
                    {CompanyName}</label>
                <br />
                <label class='companyArea'>
                    【{CompanyArea} {CompanyLocation} {CompanyMemo}】</label>
            </div>
            <br />
            <div>
                <span>
                    <label class='lbl_dot'>
                        {CompanyPhone}</label>
                    <br />
                    <label class='lbl_dot'>
                        {CompanyAddress}</label>
                </span><span>
                    <label class='lbl_dot'>
                        折扣：{CashRate}
                    </label>
                    <br />
                    <label class='lbl_dot'>
                        积分：{CompanyRate}</label>
                </span><span>
                    <label class='lbl_dot'>
                        当前会员总数：{MemberCount}
                    </label>
                    <br />
                    <label class='lbl_dot'>
                        上月新增消费金额：{LastMonthOrderSum}</label>
                </span>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
