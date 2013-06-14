<%@ Page Title="商家外卖菜单编辑" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="SysCompanyMealMenuEdit.aspx.cs" EnableEventValidation="false"
     Inherits="Eleooo.Web.Admin.SysCompanyMealMenuEdit" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagName="UcPagePosition" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function __onBtnCloseClick() {
            if (onBtnCloseClick) {
                onBtnCloseClick();
            }
            else if (parent.dlgHandlerCallback) {
                parent.dlgHandlerCallback();
            } else {
                window.close();
            }
        }
//        $(document).ready(function () {
//            $("#<%=txtCompanyTel.ClientID %>").focusout(function () {
//                GetCompanyMenuDir();
//            });
//        });
        function GetCompanyMenuDir() {
            var companyTel = $("#<%=txtCompanyTel.ClientID %>").val();
            var tar = $("#<%=ddlDirs.ClientID %>");
            tar.html("");
            if (!companyTel)
                return;
            if (companyTel.length < 11) {
                alert("请输入11位商家账号.");
                return;
            }
            var date = new Date();
            $.getJSON('/Public/RestHandler.ashx/MealMenu?CompanyTel=' + companyTel + "&date=" + date.valueOf(), function (result) {
                var data = result.data;
                for (var i = 0; i < data.length; i++) {
                    if (data[i].id && data[i].name) {
                        var $opt = $("<option></option>").val(data[i].id).html(data[i].name);
                        //if (arrDv[kk] == data[i].id) $opt.attr("selected", "selected");
                        tar.append($opt);
                    }
                };
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <uc:UcPagePosition ID="UcPagePosition1" runat="server" />
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                </td>
            </tr>
        </tbody>
    </table>
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="99%" height="1">
        <tbody>
            <tr class="tbl_row">
                <td align="right">
                    商家账号
                </td>
                <td>
                    <input type="text" id="txtCompanyTel" runat="server" name="txtCompanyTel" maxlength="11"
                        onchange="GetCompanyMenuDir()" />
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right">
                    菜单分类
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlDirs" Width="200" CssClass="vertical-align: middle;"
                        DataValueField="ID" DataTextField="DirName">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right">
                    菜单名称
                </td>
                <td>
                    <asp:TextBox ID="txtMenuName" runat="server" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right">
                    价格
                </td>
                <td>
                    <asp:TextBox ID="txtPrice" runat="server" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right">
                    是否缺货
                </td>
                <td>
                    <asp:RadioButtonList ID="rbOutOfStock" runat="server" RepeatColumns="2">
                        <asp:ListItem Value="0" Text="否"></asp:ListItem>
                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tbl_rows" height="26" bgcolor="#f0f0f5">
                <td width="580" colspan="2" align="middle">
                    <input type="button" onclick="__doPostBack('Edit');" style="line-height: 18px; width: 100px;
                        height: 25px; font-size: 14px; font-weight: bold" accesskey="S" id="btnPost"
                        value="保存(S)" />
                    <input type="button" onclick="__onBtnCloseClick();" style="line-height: 18px; width: 100px;
                        height: 25px; font-size: 14px; font-weight: bold" accesskey="C" id="btnClose"
                        value="取消(C)" />
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <span id="lblMessage" runat="server"></span>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
