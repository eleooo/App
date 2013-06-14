<%@ Page Title="我的点评" Language="C#" MasterPageFile="~/MasterPage/MainMasterV2.Master"
    AutoEventWireup="true" CodeBehind="MyFacebook.aspx.cs" Inherits="Eleooo.Web.Member.MyFacebook" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
    <link href="/Scripts/jquery.tipswindown/jquery.tipswindown.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jquery.tipswindown/jquery.tipswindown.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var action_dlg_url = "/Member/MyFacebookEdit.aspx?IsDlg=1&ID={0}";
        var action_dlg_title = "我要点评";
        var action_dlg_width = 600;
        var action_dlg_height = 200;
    </script>
    <script src="/Scripts/Regaction.js" type="text/javascript"></script>
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
                    <%=ResBLL.GetRes("searchBox_title","按条件搜索：","搜索框标题") %>
                    <label>
                        <%=ResBLL.GetRes("searchBox_beginDate","开始日期：","搜索框开始日期") %></label><input class="txtDate"
                            onclick="WdatePicker()" runat="server" id="txtDateStart" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <label><%=ResBLL.GetRes("searchBox_endDate","结束日期：","搜索框结束日期") %></label><input
                                class="txtDate" onclick="WdatePicker()" runat="server" id="txtDateEnd" />
                    <br />
                    <label>
                        <%=ResBLL.GetRes("searchBox_companyPhone","商家账号：","搜索框商家账号") %></label><input runat="server"
                            id="txtCompanyName" />
                    <asp:Button runat="server" ID="btnSearch" Text="查找(F)" />
                </td>
            </tr>
        </tbody>
    </table>
    <uc:UcGridView ID="gridView" runat="server" AllowPaper="true" />
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tbody>
            <tr>
                <td height="3">
                    <span id="txtMessage" runat="server" style="color: Red;"></span>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
