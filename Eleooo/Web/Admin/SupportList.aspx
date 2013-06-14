<%@ Page Title="客服管理" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="SupportList.aspx.cs" Inherits="Eleooo.Web.Admin.SupportList" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcGridView.ascx" TagPrefix="uc" TagName="UcGridView" %>
<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/jquery.datePicker.css" />
    <link href="/Scripts/jquery.tipswindown/jquery.tipswindown.css" rel="stylesheet"
        type="text/css" />
    <script src="/Scripts/jquery.tipswindown/jquery.tipswindown.js" type="text/javascript">
    </script>
    <script type="text/javascript" language="javascript">
        var action_dlg_url = "/Admin/SupportChat.aspx?ID={0}&IsDlg=1";
        var action_dlg_title = "客服管理";
        action_dlg_width = 900;
        action_dlg_height = 600;
        var pageSize = <%=this.gridView.PageSize %>;
        var pageIndex = <%=this.gridView.PageIndex %>;
        var containerID = '<%=this.gridContainer.ClientID %>';
        var status = Request("Status");
        var dtBeginID = '<%=this.txtDateStart.ClientID %>';
        var dtEndID = '<%=this.txtDateEnd.ClientID %>';
        var filterID = '<%=this.txtMemberPhone.ClientID %>';

        function GetSupportList() {
            var dtBegin = $('#'+dtBeginID).val();
            var dtEnd = $('#'+dtEndID).val();
            var filter = $('#'+filterID).val();
            var param = "status=" + status + "&pageindex=" + pageIndex + "&pagesize=" + pageSize +"&dtBegin="+dtBegin+"&dtEnd="+dtEnd+"&filter="+filter;
            $.ajax({
                type: "GET",
                url: "/WebRestServices/WebChat.asmx/GetSupportList",
                dataType: "html", data: param,
                success: function (html) {
                    $("#" + containerID).html(html);
                }
            });
            //autoRefresh();
        }

        function autoRefresh() {
            if (($("#txtAutoRefresh").attr('checked')) == true) {
                setTimeout(GetSupportList, 1000);
            }
        }

    </script>
    <script src="/Scripts/Regaction.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px">
                    <uc:UcPagePosition runat="server" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                    <%=ResBLL.GetRes("searchBox_title","按条件搜索：","搜索框标题") %><label><%=ResBLL.GetRes("searchBox_beginDate","开始日期：","搜索框开始日期") %></label>
                    <input class="txtDate" onclick="WdatePicker()" runat="server" type="text" id="txtDateStart" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <label>
                        <%=ResBLL.GetRes("searchBox_endDate","结束日期：","搜索框结束日期") %></label>
                    <input class="txtDate" onclick="WdatePicker()" runat="server" type="text" id="txtDateEnd" />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <label>
                        <%=ResBLL.GetRes("searchBox_memberPhone","会员账号：","搜索框会员账号") %></label>
                    <input runat="server" id="txtMemberPhone" type="text" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnSearch" Text="查找(F)" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <%--<label>
                        自动刷新</label>
                    <input type="checkbox" id="txtAutoRefresh" onclick="autoRefresh();" />--%>
                </td>
            </tr>
        </tbody>
    </table>
    <div id="gridContainer" runat="server">
        <uc:UcGridView ID="gridView" runat="server" AllowPaper="true" />
    </div>
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="100%">
        <tbody>
            <tr>
                <td class="tbl_row">
                    <b>说明</b>：<br>
                    □问题提交后请等待我们的回复，若提交错了，在管理处理前允许在发布30分钟内删除。请不要重复提交相同问题，若重复提交拒绝回复。<br>
                    □在状态中：“未处理”表示管理员未查看此咨询；“处理中”表示管理员正在查看咨询或作处理中，可能很快就有处理结果；“稍后处理”表示管理员暂时无法处理，可能需要等到下一个工作日才能处理，请等待；“已回复”表示已经处理或回复；“已经完成”表示咨询已经完成并且用户已经评分。若咨询为相同内容的重复提交或咨询中包括攻击、辱骂等不良言语，状态将是“拒绝回复”。<br>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
