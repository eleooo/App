<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcHeaderPaging.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcHeaderPaging" %>
<%@ Import Namespace="Eleooo.Web" %>
<div class="cztoppager">
    <div class=" fl pagerbtn">
        <asp:LinkButton ID="LinkFirst_UcHeaderPaging" runat="server" Text=""></asp:LinkButton>&nbsp; &nbsp;
        <asp:LinkButton ID="LinkPrev_UcHeaderPaging" runat="server" Text=""></asp:LinkButton>&nbsp; &nbsp;
        <asp:LinkButton ID="LinkNext_UcHeaderPaging" runat="server" Text=""></asp:LinkButton>&nbsp; &nbsp;
        <asp:LinkButton ID="LinkLast_UcHeaderPaging" runat="server" Text=""></asp:LinkButton>&nbsp;
    </div><div class="fr pagertext">
        <label id="lblPageSize" runat="server">
            <%=ResBLL.GetRes("paper_pagesize","每页行数:","每页显示行数") %>
        </label>
        <asp:DropDownList ID="cboPage_UcHeaderPaging" CssClass="cz_page_txt" Style="vertical-align: middle;"
            runat="server" AutoPostBack="True">
            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
            <asp:ListItem Value="20">20</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="lblCount" runat="server"></asp:Label>
    </div>
</div>
