﻿<%@ Page Title="区域大厦" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="SysAreaMansionList.aspx.cs" Inherits="Eleooo.Web.Admin.SysAreaMansionList" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/jquery.tipswindown/jquery.tipswindown.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jquery.tipswindown/jquery.tipswindown.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var action_dlg_url = "/Admin/SysAreaMansionEdit.aspx?ID={0}&IsDlg=1";
        var action_dlg_title = "编辑区域大厦";
        action_dlg_height = 180;
        action_dlg_width = 500;
        function btnImportClick() {
            __doPostBack("Add");
        }
    </script>
    <script src="/Scripts/Regaction.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px;">
                    <uc:UcPagePosition ID="pagePos" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                    <%=ResBLL.GetRes("searchBox_title","按条件搜索：","搜索框标题") %>
                    <label>
                        大厦名称：</label><input runat="server" id="txtMansionName" name="txtMansionName" type="text" />
                    <input type="submit" name="btnSearch" value="查找(F)" accesskey="F" />
                    <br/>
                    <label>
                        批量导入:</label>
                    <input type="file" id="txtImportFile" runat="server" size="20" />
                    <input type="button" name="btnImport" value="导入(I)" accesskey="I" onclick="btnImportClick();" />
                    &nbsp;&nbsp; <span class="action_dlg" param="0" index="0">[新建区域大厦]</span>
                </td>
            </tr>
        </tbody>
    </table>
    <uc:UcGridView ID="gridView" runat="server" AllowPaper="true" />
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="100%">
        <tbody>
            <tr>
                <td class="tbl_row">
                    <b>说明</b>： <span id="txtDesc" style="color: Red;" runat="server"></span>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
