<%@ Page Title="提供商家优惠" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="CompanyItemEdit.aspx.cs" Inherits="Eleooo.Web.Admin.CompanyItemEdit" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagName="UcPagePosition" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcFormView.ascx" TagName="UcFormView" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/jquery-selector.js" type="text/javascript"></script>
    <script src="/Scripts/TextboxList/TextboxList.js" type="text/javascript"></script>
    <link href="/Scripts/TextboxList/TextboxList.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.TextboxList.area.js" type="text/javascript"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="/Scripts/jquery.tipswindown/jquery.tipswindown.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jquery.tipswindown/jquery.tipswindown.js" type="text/javascript"></script>
    <script type="text/javascript">
        var itemInfoId = "#<%=hdfItemInfo.ClientID %>";
        var getItemMenuInfo = function () {
            var itemToken = GetParam("id") || "0" + $("#CompanyTel").val();
            if (itemToken == "" || itemToken == undefined || itemToken == "0") {
                alert("请输入商家账号.");
                return;
            }
            var url = "/SiteAppPage/ItemMenuInfo.aspx?itemInfo=" + escape($(itemInfoId).val()) + "&itemToken=" + itemToken;
            tipsWindown("选择菜单", "iframe:" + url, 580, 430, "true", "", "true", "");
        }
        var onItemMenuInfoClosed = function (items, itemSum, itemDesc) {
            $(itemInfoId).val(items);
            $("#Sys_Company_Item_ItemSum").val(itemSum).attr("readonly", itemSum > 0 ? true : false);
            $("#Sys_Company_Item_ItemTitle").val(itemDesc);
            $("#windown-close").trigger("click");
        }
        var hasItemMenuInfo = function () {
            var v = $(itemInfoId).val();
            return !(v == undefined || v == "" || v == "[]");
        }
        $(document).ready(function () {
            $(".btnUpload").click(function () {
                var url = "/SiteAppPage/UploadFile.aspx?saveType=5&fileType=1&txt=" + $(this).attr("txt") + "&img=" + $(this).attr("img");
                tipsWindown("上传图片", "iframe:" + url, 500, 150, "true", "", "true", "");
            });
            $("#Sys_Company_Item_ItemSum").parent().append("<input type='button' value='选择菜单' style='width:90px' onClick='getItemMenuInfo();'/>")
            $("#Sys_Company_Item_ItemSum").attr("readonly", hasItemMenuInfo());
            //$("form").submit(validateInput);
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
    <asp:HiddenField ID="hdfItemInfo" runat="server" />
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td class="tbl_row">
                    <span id="txtMessage" runat="server"></span>
                </td>
            </tr>
        </tbody>
    </table>
    <script type="text/javascript">
        function showMessage(ctx, message) {
            ctx.find("span").remove();
            ctx.append("<span class='red'>" + message + "</span>");
            console.log("showMessage");
        }
        var _validateInput = validateInput;
        validateInput = function () {
            if (hasItemMenuInfo()) {
                var itemPoint = parseFloat($("#Sys_Company_Item_ItemPoint").val()) || 0;
                if (itemPoint < 0.5 || itemPoint > 1.5) {
                    showMessage($("#Sys_Company_Item_ItemPoint").parent(),"兑换积分限定在0.5-1.5分之间.");
                    $("#Sys_Company_Item_ItemPoint").focus();
                    return false;
                }
                var itemNeedPay = parseFloat($("#Sys_Company_Item_ItemNeedPay").val()) || 0;
                var itemSum = parseFloat($("#Sys_Company_Item_ItemSum").val()) || 0;
                if (itemNeedPay < 0) {
                    showMessage($("#Sys_Company_Item_ItemNeedPay").parent(),"现金支付金额必须大于零且不能大于总价.");
                    $("#Sys_Company_Item_ItemNeedPay").focus();
                    return false;
                } else if (itemNeedPay > itemSum) {
                    showMessage($("#Sys_Company_Item_ItemNeedPay").parent(), "现金支付金额不能大于总价.");
                    $("#Sys_Company_Item_ItemNeedPay").focus();
                    return false;
                }
            }
            return _validateInput();
        };
    </script>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
