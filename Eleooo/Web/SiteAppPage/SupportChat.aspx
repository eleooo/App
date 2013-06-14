<%@ Page Title="" Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        BODY { padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; font-family: Arial, Helvetica, sans-serif; font-size: 12px; padding-top: 0px; }
    </style>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.site.js" type="text/javascript"></script>
</head>
<body>
    <input type="hidden" id="inputMessageCount" value="" />
    <div id="DivMessage">
    </div>
    <script type="text/javascript">
        var sid = <%=this.Request.Params["sid"] %>
        function GetMessageList() {
            $.ajax({
                type: "GET",
                url: "/WebRestServices/WebChat.asmx/Support_GetMessageCount",
                dataType: "xml", data: "sid=" + sid,
                success: function (xml) {

                    if ($("#inputMessageCount").val() != $(xml).find("int").text() || $("#inputMessageCount").val() * 1 == 0) {
                        $("#inputMessageCount").val($(xml).find("int").text());
                        $.ajax({
                            type: "GET",
                            url: "/WebRestServices/WebChat.asmx/Support_GetMessageList",
                            dataType: "xml", data: "sid=" + Request("sid"),
                            success: function (xml) {
                                $("#DivMessage").html($(xml).find("string").text());
                            }
                        });
                    }
                    setTimeout(GetMessageList, 200);
                }
            });
        }
        setTimeout(GetMessageList, 200);
    </script>
</body>
</html>
