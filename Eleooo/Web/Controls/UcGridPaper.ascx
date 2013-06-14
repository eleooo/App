<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcGridPaper.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcGridPaper" %>
<%@ Import Namespace="Eleooo.Web" %>
<asp:HiddenField ID="txtPageIndex" runat="server" Value="1" />
<%--<table id="tblPage" cellspacing="0" cellpadding="2" width="100%" border="0" runat="server"
    bgcolor="#ffcb6f">
    <tr>
        <td nowrap width="35%">
            &nbsp;
        </td>
        <td nowrap valign="bottom" align="center" width="15%" style="display: none">
            <asp:DropDownList ID="ddlPage" runat="server" Width="40px" AutoPostBack="True">
                <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="left" width="5%" nowrap>
        </td>
        <td align="right" width="35%" nowrap>
        </td>
    </tr>
</table>--%>
<div id="paperHeader" runat="server" class="cztoppager">
    <%--<div class="fl pagerbtn">--%>
    <span class="fl">
        <asp:LinkButton ID="LinkFirst" runat="server" Text=""></asp:LinkButton>&nbsp; &nbsp;
        <asp:LinkButton ID="LinkPrev" runat="server" Text=""></asp:LinkButton>&nbsp; &nbsp;
        <asp:LinkButton ID="LinkNext" runat="server" Text=""></asp:LinkButton>&nbsp; &nbsp;
        <asp:LinkButton ID="LinkLast" runat="server" Text=""></asp:LinkButton>&nbsp;
    </span>
<%--    </div>
    <div class="fr pagertext">--%>
    <span class="fr">
        <label id="lblPageSize" runat="server">
            <%=ResBLL.GetRes("paper_pagesize","每页行数:","每页显示行数") %>
        </label>
        <asp:DropDownList ID="cboPage" CssClass="cz_page_txt" Style="vertical-align: middle;"
            runat="server" AutoPostBack="True">
            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
            <asp:ListItem Value="20">20</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="lblCount" runat="server"></asp:Label>
    </span>
    <%--</div>--%>
</div>
<div id="paperBottom" runat="server" class="pager">
</div>
