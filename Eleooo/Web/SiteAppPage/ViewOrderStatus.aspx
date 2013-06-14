<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewOrderStatus.aspx.cs"
    Inherits="Eleooo.Web.SiteAppPage.ViewOrderStatus" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>查看送餐进度</title>
    <link type="text/css" href="/App_Themes/ThemesV2/css/inc_2.css" rel="stylesheet" />
    <link type="text/css" href="/App_Themes/ThemesV2/css/content_2.css" rel="stylesheet" />
    <style type="text/css">
        body
        {
            border: 1px solid #d6d6d6;
        }
    </style>
</head>
<body>
    <div class="zfyx">
        <div class="zfyx-title">
            <span>看看送餐进度</span></div>
        <asp:Repeater ID="rpStatus" runat="server">
            <HeaderTemplate>
                <div class="zfyx-main">
                    <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li><span <%# GetItemCssClass(Container.ItemIndex,Container.DataItem) %>>
                    <%#Eval("Date", "{0:HH:mm:ss}")%></span>
                    <div>
                        <%#Eval("Desc") %></div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                <li class="s_gray">*实时更新送餐进度</li>
                </ul> </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div id="autoRun" runat="server">
        <script type="text/javascript" language="javascript">
            (function () {
                setTimeout(function () {
                    document.location.reload();
                }, 4000);
            })();
        </script>
    </div>
</body>
</html>
