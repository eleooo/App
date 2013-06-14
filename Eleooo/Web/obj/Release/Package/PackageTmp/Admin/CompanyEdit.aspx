<%@ Page Title="商家管理" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="CompanyEdit.aspx.cs" Inherits="Eleooo.Web.Admin.CompanyEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagName="UcPagePosition" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcFormView.ascx" TagName="UcFormView" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />    
    <script src="/App_Xheditor/xheditor-1.1.12-zh-cn.min.js" type="text/javascript"></script>
    <link href="/Scripts/jquery.tipswindown/jquery.tipswindown.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jquery.tipswindown/jquery.tipswindown.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnUpload").click(function () {
                var url = "/SiteAppPage/UploadFile.aspx?saveType=1&fileType=1&txt=" + $(".txtUpload").attr("id") + "&img=" + $(".imgUpload").attr("id");
                tipsWindown("上传图片", "iframe:" + url, 500, 150, "true", "", "true", "");
            });
            $("#btnCompanyPic").click(function () {
                var url = "/SiteAppPage/FileManager.aspx?IsDlg=1&saveType=1&fileType=1&folderName=" + $("#CompanyPic").val()
                tipsWindown("商家实景图片", "iframe:" + url, 600, 500, "true", "", "true", "");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
            <uc:UcPagePosition runat="server" />
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                </td>
            </tr>
        </tbody>
    </table>
    <uc:UcFormView ID="formView" runat="server" />
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="100%">
        <tbody>
            <tr>
                <td class="tbl_row">
                    <span id="lblMessage" runat="server"></span>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
