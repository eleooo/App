<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcTypeAndAreaFilter.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcTypeAndAreaFilter" %>

<input id="UcTypeAndAreaFilter_Value" name="UcTypeAndAreaFilter_Value" runat="server"
    type="hidden" />
<script type="text/javascript">
    var setFilterParam = function (target, postVal) {
        $("#<%=this.UcTypeAndAreaFilter_Value.ClientID %>").val(target + postVal);
        $("#<%= this.ResetPageIndexControl %>").val(1);
        __doPostBack(target);
    }
</script>

