<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master" AutoEventWireup="true" CodeBehind="CompanyAdsEdit.aspx.cs" Inherits="Eleooo.Web.Admin.CompanyAdsEdit" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagName="UcPagePosition" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcFormView.ascx" TagName="UcFormView" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Scripts/editablegrid-2.0.1/editablegrid-2.0.1.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/editablegrid-2.0.1/editablegrid-2.0.1.js" type="text/javascript"></script>
    <script src="/Scripts/editablegrid-2.0.1/editablegrid-ext.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-selector.js" type="text/javascript"></script>
    <script src="/Scripts/TextboxList/TextboxList.js" type="text/javascript"></script>
    <link href="/Scripts/TextboxList/TextboxList.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.TextboxList.area.js" type="text/javascript"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="/Scripts/jquery.tipswindown/jquery.tipswindown.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jquery.tipswindown/jquery.tipswindown.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnAdsImage").click(function () {
                var url = "/SiteAppPage/FileManager.aspx?IsDlg=1&maxLimit=5&saveType=CompanyAds&fileType=Image&folderName=" + $("#AdsImages").val()
                tipsWindown("广告图片", "iframe:" + url, 600, 500, "true", "", "true", "");
            })

        });
    </script>
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
    <uc:UcFormView ID="formView" runat="server" />
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td class="tbl_row">
                    <span id="txtMessage" runat="server"></span>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
