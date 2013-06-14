<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcFooterPaging1.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcFooterPaging1" %>
<p class="pager">
        <a href="javascript:__doPostBack('<%=GetLinkPrevCommandName() %>','')">上一页</a>
        <%for (int i = 1; i <= PageCount; i++)
          {
              if (i == PageIndex)
              {%>
        <a href="javascript:__doPostBack('<%=GetPageIndexCommandName() %>',<%=i %>)"
            class="current">
            <%=i%></a>
        <%}
              else
              { %>
        <a href="javascript:__doPostBack('<%=GetPageIndexCommandName() %>',<%=i %>)">
            <%=i%></a>
        <%}
          } %>
        <a href="javascript:__doPostBack('<%=GetLinkNextCommandName() %>','')">下一页</a>
</p>

