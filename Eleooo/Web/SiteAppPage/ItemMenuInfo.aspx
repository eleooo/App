<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemMenuInfo.aspx.cs" Inherits="Eleooo.Web.SiteAppPage.ItemMenuInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/Scripts/multi-select/css/multi-select.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/multi-select/jquery.multi-select.js" type="text/javascript"></script>
</head>
<body>
    <div style="line-height: 40px; border-bottom-width: 2px; border-bottom-color: Orange;
        border-bottom-style: solid;">
        &nbsp;&nbsp;<input type="button" value="确定选择" onclick="getMenuInfo();" id="btnReturn" /></div>
    <div style="margin-top: 10px;">
        <asp:Repeater ID="rpMenuDir" runat="server" DataSource='<%#GetMenu( ) %>'>
            <HeaderTemplate>
                <select id="slMenu" multiple="multiple"></HeaderTemplate>
            <FooterTemplate>
                </select></FooterTemplate>
            <ItemTemplate>
                <optgroup label="<%#Eval("Key.Value") %>" data-label="<%#HttpUtility.UrlEncodeUnicode(Eval("Key.Value").ToString()) %>" id="g_<%#Eval("Key.Key") %>">
                    <asp:Repeater ID="rpMenu" runat="server" DataSource='<%# Container.DataItem %>'>
                        <ItemTemplate>
                            <option data-label="<%# HttpUtility.UrlEncodeUnicode(Eval("name").ToString()) %>" value="<%#Eval("id") %>" data-value="<%#Eval("id") %>" data-price="<%#Eval("price") %>">
                                <%#Eval("name") %></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </optgroup>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <script type="text/javascript">
        var getMenuInfo = function(){
            $("#slMenu").multiSelect("vals",function(items){
                var vals = [];
                var titleInfo=[],dSum = 0;
                $.each(items,function(i,item){
                    vals.push(item.data("ms-value"));
                    titleInfo.push(unescape(item.attr("data-label")));
                    dSum += parseFloat(item.attr("data-price"));
                });
                if($.isFunction(parent.onItemMenuInfoClosed))
                    parent.onItemMenuInfoClosed('['+vals.join(",")+']',dSum,titleInfo.join("+") );
            });
        }
        $(document).ready(function () {
            var m = $("#slMenu");
            if(m.find("option").length == 0){
                $("#btnReturn").hide();
            }
            m.multiSelect({selectableHeader:"<b>餐点列表</b>",selectionHeader:"<b>已选餐点</b>"}).multiSelect("select",<%=GetItemInfo() %>);

        });
    </script>
</body>
</html>
