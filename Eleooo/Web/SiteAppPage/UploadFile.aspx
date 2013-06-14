<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="Eleooo.Web.SiteAppPage.UploadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>上传附件</title>
    <script type="text/JavaScript">
		<!--

        function closeForm(objTxt, valueTxt, objImg, valueImg) {

            window.parent.document.getElementById(objTxt).value = valueTxt;
            window.parent.document.getElementById(objImg).src = valueImg;
            window.parent.document.getElementById(objImg).style.display = "";

            window.parent.document.getElementById("windownbg").style.display = 'none';
            window.parent.document.getElementById("windown-box").style.display = 'none';
        }
			
		//-->
    </script>
</head>
<body style="background-color: #cccccc; border: 1px solid #cccccc">
    <form id="Form2" runat="server" method="post" enctype="multipart/form-data">
    <input type="file" name="uploadify" id="uploadify" size="30" runat="server" />
    <asp:Button runat="server" ID="btnUpload" Text="上传" OnClick="btnUpload_Click" />
    <br />
    <span id="lblMessage" runat="server" style="color:Red;"></span>
    <span id="lblInfo" runat="server"
        style="color:Maroon;"></span>
    </form>
</body>
</html>
