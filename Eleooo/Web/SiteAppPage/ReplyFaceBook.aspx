<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplyFaceBook.aspx.cs"
    Inherits="Eleooo.Web.SiteAppPage.ReplyFaceBook" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>回复</title>
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/json/json2.js" type="text/javascript"></script>
    <link href="/App_Themes/Admin/images/style.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/Admin/content.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/Admin/inc.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .i_input
        {
            border: 1px solid #b9b9b9;
            padding: 5px;
            width: 410px;
        }
    </style>
</head>
<body>
    <table width="100%" cellspacing="1" cellpadding="3" border="0" class="tbl_body">
        <thead>
            <tr class="tbl_form_row">
                <th colspan="2" style="background-color: Orange; font-weight: bold; text-align: left;">
                    回复会员点评
                </th>
            </tr>
        </thead>
        <tbody>
            <tr class="tbl_form_row">
                <td style="width: 10%" align="center">
                    会员账号:
                </td>
                <td>
                    <input type="text" value="<%=GetUserPhoneNumber() %>" disabled="disabled" />
                </td>
            </tr>
            <tr id="trFaceBookRate" runat="server" visible="false" class="tbl_form_row">
                <td style="width: 10%" align="center">
                    评价星级
                </td>
                <td>
                    <img src="/App_Themes/ThemesV2.1/images/start<%=this.FaceBook.FaceBookRate %>.gif"
                        alt="" />
                </td>
            </tr>
            <tr class="tbl_form_row">
                <td style="width: 10%" align="center">
                    点评内容:
                </td>
                <td>
                    <textarea rows="4" class="i_input" disabled="disabled"><%=Server.UrlDecode(FaceBook.FaceBookMemo) %></textarea>
                </td>
            </tr>
            <tr class="tbl_form_row">
                <td style="width: 10%" align="center">
                    回复内容:
                </td>
                <td>
                    <textarea rows="4" class="i_input" id="fbContent"><%=Server.UrlDecode(FaceBook.ReplyMemo) %></textarea>
                </td>
            </tr>
            <tr bgcolor="#f0f0f5" class="tbl_form_row">
                <td bgcolor="#f0f0f5" align="center" colspan="2">
                    <input type="button" value="点击回复(S)" id="btnSubmit" style="line-height: 18px; width: 100px;
                        height: 25px; font-size: 14px; font-weight: bold" />
                </td>
            </tr>
        </tbody>
    </table>
    <script type="text/javascript">
        var FaceBook = (function () {
            var url = "/Public/RestHandler.ashx/FaceBook/Edit";
            var fbID = '<%=FbID %>';
            var inited = false;
            function execute(data, fnCallback) {
                data["fbID"] = fbID;
                $.ajax({
                    type: "POST",
                    url: url,
                    dataType: "json", data: data,
                    success: function (result) {
                        if (result.code >= 0)
                            fnCallback(result);
                        else
                            alert(result.message);
                    }
                });
            }
            function getFaceBookContent() {
                var content = ($("#fbContent").val() || '');
                if (content.length == 0) {
                    alert("请输入你的回复内容.");
                    return false;
                }
                return encodeURIComponent(content);
            }
            return {
                init: function () {
                    if (inited) return;
                    $("#btnSubmit").click(function () {
                        var content = getFaceBookContent();
                        if (!content)
                            return;
                        execute({ content: content }, function (result) {
                            alert("回复成功");
                        });
                    });
                }
            };
        })();
        $(document).ready(FaceBook.init);
    </script>
</body>
</html>
