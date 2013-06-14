<%@ Page Title="问题处理" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="SupportChat.aspx.cs" Inherits="Eleooo.Web.Company.SupportChat" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var sID = Request("ID");
        function SetSupportState() {

            $.ajax({
                type: "GET",
                url: "/WebRestServices/WebChat.asmx/Support_Work_Support_IsExists",
                dataType: "xml", data: "SupportID=" + sID,
                success: function (xml) {
                    $(".SupportMan").html($(xml).text() == "true" ? "<font color='red'>服务人员在线.</font>" : "<font color='green'>服务人员离线</font>");
                }
            });

            setTimeout(SetSupportState, 1000);
        }

        $(document).ready(function () {
            setTimeout(SetSupportState, 1000);
        })
    </script>
    <script src="/Scripts/ajaxfileupload.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "GET",
                url: "/WebRestServices/WebChat.asmx/Support_GetInfo",
                dataType: "xml", data: "sid=" + sID,
                success: function (xml) {

                    var Status = $(xml).find("Support_Status").text();
                    var StatusStr = $(xml).find("GetStatusString").text();

                    var Rating = $(xml).find("Support_Rating").text() * 1;
                    var RatingStr = $(xml).find("GetRatingString").text();
                    var Ratingreason = $(xml).find("Support_Ratingreason").text();

                    if (Rating > 1) {
                        $("#divSupport").hide();
                        $("#divStatus").html("评分：<b>" + RatingStr + "</b>,反馈：" + Ratingreason);
                    }
                    else if (Rating == 1 && Status != 1) {
                        $("#divStatus").hide();
                        $("#divRating").show();
                    }
                }
            });
        })

        function enterBr() {
            if (event.ctrlKey && event.keyCode == 13) {
                btnSend_onclick();
                return false;
            }
        }

        function btnSend_onclick() {
            if (document.getElementById("txtSupportContent").value == "") {
                alert("请输入你的信息");
            }
            else {
                document.getElementById("btnSend").value = "请等待...";
                document.getElementById("btnSend").disabled = true;
                setTimeout(SendMod, 500);
            }
        }

        function SendMod() {

            var mess = escape(document.getElementById("txtSupportContent").value);
            var photo = escape(document.getElementById("txtSupportPhoto").value);

            if (photo != "") {
                $.ajaxFileUpload({
                    url: "/SiteAppPage/UploadHandler.ashx?saveType=3&",
                    secureuri: false,
                    fileElementId: "txtSupportPhoto",
                    dataType: "json",
                    success: function (json) {
                        //photo = $(xml).find("#fileName").text();
                        //msg = $(xml).find("#msg").text();
                        photo = json.filename;
                        if (json.message || json.message != '') {
                            alert(json.message);
                            document.getElementById("btnSend").disabled = false;
                            return;
                        }
                        $.ajax({
                            type: "POST",
                            url: "/WebRestServices/WebChat.asmx/Support_SendMessage",
                            dataType: "xml", data: "sid=" + sID + "&uid=" + Request("UID") + "&ask=False&msg=" + mess + "&photo=" + photo,
                            success: function (xml) {
                                if ($(xml).find("string").text() == "SendOk") {
                                    document.getElementById("txtSupportContent").value = "";
                                    document.getElementById("txtSupportPhoto").value = "";
                                    document.getElementById("btnSend").value = "发送";
                                    document.getElementById("btnSend").disabled = false;
                                }
                                else {
                                    alert($(xml).find("string").text());
                                }
                            }
                        });
                    }
                });
            }
            else {
                $.ajax({
                    type: "GET",
                    url: "/WebRestServices/WebChat.asmx/Support_SendMessage",
                    dataType: "xml", data: "sid=" + sID + "&uid=" + Request("UID") + "&ask=False&msg=" + mess + "&photo=",
                    success: function (xml) {
                        if ($(xml).find("string").text() == "SendOk") {
                            document.getElementById("txtSupportContent").value = "";
                            document.getElementById("txtSupportPhoto").value = "";
                            document.getElementById("btnSend").value = "发送";
                            document.getElementById("btnSend").disabled = false;
                        }
                        else {
                            alert($(xml).find("string").text());
                        }
                    }
                });
            }
        }

        function Support_UpdateRating() {

            var Rating = $("input[name='grade'][checked]").val() * 1;
            var RatingReason = $("#txtRatingReason").val();
            if (Rating == 1) {
                alert("谢谢你的评分！");
                return;
            }

            $.ajax({
                type: "post",
                url: "/WebRestServices/WebChat.asmx/Support_UpdateRating",
                dataType: "xml", data: "sid=" + sID + "&Rating=" + Rating + "&RatingReason=" + RatingReason,
                success: function (xml) {
                    $("#divStatus").html("评分：<b>" + $("#Rating" + Rating).html() + "</b>,留言：" + Ratingreason);
                    $("#divStatus").show();
                    $("#divRating").hide();
                    $("#divSupport").hide();
                }
            });
        }
        function onBtnCloseClick() {
            if (parent.dlgHandlerCallback) {
                parent.dlgHandlerCallback();
            } else {
                window.location.href = "/Member/SupportList.aspx?Status=1";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="2" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 20px" colspan="2">
                    <font color="#bb0000"><b>主题</b></font>：<font color="#990000" id="lblSupportSubject"
                        runat="server"></font> (问题ID：<b id="lblSupportID" runat="server" class="SupportID"></b>，请在离开的时候评分和关闭)
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <iframe height="360" frameborder="1" width="100%" runat="server" id="iframeChatList"
                        scrolling="yes"></iframe>
                </td>
            </tr>
            <tr>
                <td style="border-bottom: #cccccc 1px solid; line-height: 23px" colspan="2">
                    <b>[备注]</b>：<b id="lblSupportMan" runat="server" class="SupportMan"> </b>
                </td>
            </tr>
            <tr id="trSupportStatus1" runat="server">
                <td style="line-height: 23px" width="68%">
                    <div id="divSupport">
                        <font color="#bb0000"><b>内容</b></font>：（请输入你的内容.）<br>
                        <textarea style="height: 68px" onkeydown="enterBr()" id="txtSupportContent" rows="4"
                            cols="80"></textarea><br />
                        附件：<input type="file" id="txtSupportPhoto" name="txtSupportPhoto" />
                        <input style="line-height: 18px; width: 100px; height: 35px; font-size: 14px; font-weight: bold"
                            value="发送" onclick="btnSend_onclick();" type="button" id="btnSend" /><br />
                        备注：附件小于1500KB，允许格式为 <b>.rar/.zip/.jpg/.gif</b>
                    </div>
                    <br>
                </td>
                <td style="border-left: #cccccc 1px solid; padding-left: 6px" valign="top" width="32%">
                    <font style="font-size: 14px" color="#bb0000"><b>请评分.</b></font>：<br />
                    <div id="divStatus">
                    </div>
                    <div id="divRating" style="display: none">
                        <input id="g2" value="3" type="radio" name="grade" /><label for="g2"><b id="Rating3"
                            style="color: green">√ 满意</b></label><br />
                        <input id="g1" value="1" checked type="radio" name="grade" /><label for="g1"><b>一般</b></label><br>
                        <input id="g3" value="2" type="radio" name="grade" /><label for="g3" id="Ratin2"><b
                            style="color: red">× 不满意</b></label><br />
                        <b>你的反馈</b>：<br />
                        <input maxlength="50" size="36" id="txtRatingReason" name="gradetext"><br>
                        <input value="Submit" type="button" name="SentBtn" onclick="Support_UpdateRating()">
                    </div>
                </td>
            </tr>
            <tr id="trSupportStatus2" runat="server" style="display: none">
                <td style="line-height: 23px" colspan="2">
                    <b>[Note]</b>：你的问题已经关闭. 你的反馈是：<label id="lblSupportRating" runat="server"><font color="green"></font></label>。
                    如果你还有问题, 请点这里 <a href="/Member/SupportEdit.aspx"><b><font style="font-size: 14px"
                        color="blue">提交一个新问题</font></b></a>，&nbsp; 或 <a style="color: blue; font-size: 14px"
                            href="javascript:onBtnCloseClick();"><b>返回问题列表.</b></a>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
