<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageBase.Master"
    Trace="false" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Eleooo.Web.WebForm1" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/json/json2.js" type="text/javascript"></script>
    <link href="/Scripts/TextboxList/TextboxList.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/TextboxList/TextboxList.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.TextboxList.area.js" type="text/javascript"></script>
    <link href="App_Themes/Member/style/eleooo.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Member/style/public.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Member/style/subpage.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-selector.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMemberInfo" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div id="div">
        <input type="text" value="请输入您所在的大厦或小区，如：赛格广场" id="txtInputVal" name="txtInputVal" />
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
    <script type="text/javascript">
        var OrderMeal = function () {
            var container = "#container";
            function renderView() {
                var data =
                {
                    txtCompany: "",
                    txtMember: "",
                    rbStatus: 0,
                    cboPage: 10,
                    txtPageIndex: 1
                };
                $.ajax({
                    type: "POST",
                    url: "/Public/OrderMealServices.asmx/QueryOrderMeal",
                    dataType: "xml", data: data,
                    success: function (xml) {
                        var result = $(xml).text();
                        if (result.charAt(0) == '{') {
                            var error = __toObject(result);
                            alert(error.message);
                        } else {
                            $(viewContainer).html($(result).find("#container").html());
                        }

                    }
                });
            };
            //setTimeout(renderView, 2000);
        };
        $(document).ready(function () {
            window["orderMeal"] = new OrderMeal();
        });
    </script>
</asp:Content>
