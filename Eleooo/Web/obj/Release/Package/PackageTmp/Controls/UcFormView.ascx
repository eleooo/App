<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcFormView.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcFormView" %>
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
</script>
