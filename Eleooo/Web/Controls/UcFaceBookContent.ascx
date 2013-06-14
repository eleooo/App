<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcFaceBookContent.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcFaceBookContent" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<ele:DataListExt ID="rpFbOrderMeal" runat="server" AllowPaging="true" ShowFootPaging="true"
    ShowHeadPaging="false" FooterPagingTemplate="~/Controls/UcFooterPaging2.ascx"
    FetchType="DataTable" Visible="false" EmptyDataIsShowHeaderAndFooterTemplate="false">
    <EmptyDataTemplate>
        <div class="no_review">
            该餐厅暂无点评^_^</div>
    </EmptyDataTemplate>
    <HeaderTemplate>
        <ul class="reviewList">
    </HeaderTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
    <ItemTemplate>
        <li>
            <%#UpdateReadFlag( )%>
            <div class="rLeft">
                <div class="user">
                    <%#FormatUserPhone() %></div>
                <div>
                    下单时间</div>
                <div class="gray_time">
                    <%#Eval("LatestOrderDate", "{0:yyyy年MM月dd日}")%></div>
            </div>
            <div class="rRight">
                <div class="corner">
                </div>
                <h2>
                    <div class="time">
                        <%#Eval("FaceBookDate", "{0:yyyy-MM-dd HH:mm}")%></div>
                    <img src="/App_Themes/ThemesV2.1/images/start<%#Eval("FaceBookRate") %>.gif" alt="" /></h2>
                <div class="comment-txt">
                    <%# Server.HtmlEncode( Utilities.ToString(Eval("FaceBookMemo")))%></div>
                <div class="reply" id="replyContainer" runat="server" visible='<%# !Utilities.IsNull(Eval("ReplyDate")) %>'>
                    <h3>
                        <div class="time">
                            <%#Eval("ReplyDate", "{0:yyyy-MM-dd HH:mm}")%></div>
                        餐厅回复：</h3>
                    <div>
                        <%# Server.HtmlEncode( Utilities.ToString(Eval("ReplyMemo")))%></div>
                </div>
            </div>
        </li>
    </ItemTemplate>
</ele:DataListExt>
<ele:DataListExt ID="rpFbEleooo" runat="server" AllowPaging="true" ShowFootPaging="true"
    ShowHeadPaging="false" FooterPagingTemplate="~/Controls/UcFooterPaging2.ascx"
    FetchType="DataTable" Visible="false">
    <HeaderTemplate>
        <ul class="reviewList">
    </HeaderTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
    <ItemTemplate>
        <%#UpdateReadFlag( )%>
        <li id="item<%#GenFaceBookClientID() %>">
            <div class="rLeft">
                <div class="user">
                    <%#FormatUserPhone() %></div>
                <div class="gray_time">
                    <%#Eval("FaceBookDate", "{0:yyyy年MM月dd日}")%></div>
            </div>
            <div class="rRight">
                <div class="corner">
                </div>
                <div class="comment-txt">
                    <%# Server.HtmlEncode( Utilities.ToString( Eval("FaceBookMemo")))%></div>
                <div class="m_tc">
                    <a href="javascript:void(0);" onclick="FaceBook.showInputBox('<%#Eval("ID") %>');">
                        <%#GetCommentText() %></a></div>
                <ul style="display: none;" class="my_dp" id="inputbox<%#GenFaceBookClientID() %>">
                    <li><u>我来说说：</u>
                        <div>
                            <textarea class="i_input" rows="4" id="fbContent<%#GenFaceBookClientID() %>"></textarea>
                        </div>
                    </li>
                    <li style="padding-left: 90px;"><a href="javascript:void(0);" onclick="FaceBook.submitFaceBook(<%#Eval("ID") %>)">
                        <img alt="" src="/App_Themes/ThemesV2.1/images/xd-aniu-tj.png" /></a></li>
                </ul>
                <div class="reply" id="replyContainer" runat="server" visible='<%# !Utilities.IsNull(Eval("ReplyDate")) %>'>
                    <h3>
                        <div class="time">
                            <%#Eval("ReplyDate", "{0:yyyy-MM-dd HH:mm}")%></div>
                        乐多分回复：</h3>
                    <div>
                        <%# Server.HtmlEncode( Utilities.ToString( Eval("ReplyMemo")))%></div>
                </div>
                <ele:DataListExt ID="subFbEleooo" runat="server" AllowPaging="false" DataSource='<%#GetSubFaceBookDataSource(Eval("ID")) %>'>
                    <ItemTemplate>
                        <%#UpdateReadFlag( )%>
                        <div class="reply gt">
                            <h3>
                                <div class="time">
                                    <%#Eval("FaceBookDate", "{0:yyyy-MM-dd HH:mm}")%></div>
                                <%#FormatUserPhone() %></h3>
                            <div>
                                <%#Server.HtmlEncode( Utilities.ToString( Eval("FaceBookMemo")))%></div>
                        </div>
                        <div class="reply" id="replyContainer" runat="server" visible='<%# !Utilities.IsNull(Eval("ReplyDate")) %>'>
                            <h3>
                                <div class="time">
                                    <%#Eval("ReplyDate", "{0:yyyy-MM-dd HH:mm}")%></div>
                                乐多分回复：</h3>
                            <div>
                                <%#Server.HtmlEncode( Utilities.ToString( Eval("ReplyMemo")))%></div>
                        </div>
                    </ItemTemplate>
                </ele:DataListExt>
            </div>
        </li>
    </ItemTemplate>
</ele:DataListExt>
