<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcFooterPaging2.ascx.cs" Inherits="Eleooo.Web.Controls.UcFooterPaging2" %>
<p class="pager">
    <a href="javascript:FaceBook.showFaceBook('<%=PageIndex -1 %>')">上一页</a>
    <%for (int i = GetStartIndex(); i <= GetEndIndex(); i++)
          {
              if (i == PageIndex)
              {%>
    <a href="javascript:FaceBook.showFaceBook('<%=i %>')" class="current">
        <%=i%></a>
    <%}
              else
              { %>
    <a href="javascript:FaceBook.showFaceBook('<%=i %>')">
        <%=i%></a>
    <%}
          } %>
    <a href="javascript:FaceBook.showFaceBook('<%=PageIndex +1 %>')">下一页</a>
</p>
