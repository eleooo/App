<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="SupportChat.aspx.cs" Inherits="Eleooo.Web.Admin.SupportChat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var sID = Request("ID");
        function SetSupportState() {

            $.ajax({
                type: "GET",
                url: "/WebRestServices/WebChat.asmx/Support_Work_Seller_IsExists",
                dataType: "xml", data: "SupportID=" + sID,
                success: function (xml) {
                    $(".SupportMan").html($(xml).text() == "true" ? "<font color='red'>当前会员在线</font>" : "<font color='green'>当前会员离线</font>");
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

                    var Rating = $(xml).find("Support_Rating").text();
                    var RatingStr = $(xml).find("GetRatingString").text();
                    var Ratingreason = $(xml).find("Support_Ratingreason").text();

                    $("input[name=grade]").get(Status - 1).checked = true;
                    if (Status == 3) {
                        $("#divSupport").hide();
                        $("#divStatus").hide();
                        $("#divRating").show();

                        if (Rating > 1) {
                            $("#divRating").html("客户评分是" + RatingStr + ",客户评价：" + Ratingreason);
                        }
                    }
                    else {
                        $("#divStatus").show();
                        $("#divRating").hide();
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
                alert("请输入消息内容");
            }
            else {
                document.getElementById("btnSend").value = "请稍等";
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
                        photo = json.string;
                        if (json.message||json.message != '') {
                            alert(json.message);
                            document.getElementById("btnSend").disabled = false;
                            return;
                        }
                        $.ajax({
                            type: "GET",
                            url: "/WebRestServices/WebChat.asmx/Support_SendMessage",
                            dataType: "xml", data: "sid=" + sID + "&uid=" + Request("UID") + "&ask=True&msg=" + mess + "&photo=" + photo,
                            success: function (xml) {
                                if ($(xml).find("string").text() == "SendOk") {
                                    document.getElementById("txtSupportContent").value = "";
                                    document.getElementById("txtSupportPhoto").value = "";
                                    document.getElementById("btnSend").value = "提交对话";
                                    document.getElementById("btnSend").disabled = false;
                                }
                                else {
                                    alert($(xml).find("string").text());
                                }
                            }
                        });
                    }
                })
            }
            else {
                $.ajax({
                    type: "GET",
                    url: "/WebRestServices/WebChat.asmx/Support_SendMessage",
                    dataType: "xml", data: "sid=" + sID + "&uid=" + Request("UID") + "&ask=True&msg=" + mess + "&photo=",
                    success: function (xml) {
                        if ($(xml).find("string").text() == "SendOk") {
                            document.getElementById("txtSupportContent").value = "";
                            document.getElementById("txtSupportPhoto").value = "";
                            document.getElementById("btnSend").value = "提交对话";
                            document.getElementById("btnSend").disabled = false;
                        }
                        else {
                            alert($(xml).find("string").text());
                        }
                    }
                });
            }
        }

        function Support_UpdateStatus() {

            var status = $("input[name='grade']:checked").val() * 1;
            if (status == 1) {
                alert("请修改状态！");
                return;
            }

            $.ajax({
                type: "post",
                url: "/WebRestServices/WebChat.asmx/Support_UpdateStatus",
                dataType: "xml", data: "sid=" + Request("ID") + "&Status=" + status,
                success: function (xml) {

                    if (status == 3) {
                        $("#divSupport").hide();
                        $("#divStatus").hide();
                        $("#divRating").show();
                    }
                    else {
                        $("#divStatus").show();
                        $("#divRating").hide();
                    }
                    alert("提交成功!");
                }
            });
        }
        function onBtnCloseClick() {
            if (parent.dlgHandlerCallback) {
                parent.dlgHandlerCallback();
            } else {
                window.location.href = "/Admin/SupportList.aspx?Status=1";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="2" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 20px" colspan="2">
                    <font color="#bb0000"><b>当前主题</b></font>：<font color="#990000" id="lblSupportSubject"
                        runat="server"></font> (类型：<label id="lblSupportType" runat="server"></label>,咨询编号：<b
                            id="lblSupportID" runat="server" class="SupportID"></b>，请在右下角进行评分)
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
                    <b>[友情提示]</b>：<b id="lblSupportMan" class="SupportMan" runat="server">当前服务人员在线</b>
                </td>
            </tr>
            <tr id="trSupportStatus1" runat="server">
                <td style="line-height: 23px" width="68%">
                    <div id="divSupport">
                        <font color="#bb0000"><b>对话</b></font>：（可在此补充或与管理员交流，请开启JavaScript以便更方便地使用此系统）<br>
                        <textarea style="height: 68px" onkeydown="enterBr()" id="txtSupportContent" rows="4"
                            cols="80"></textarea><br />
                        上传附件：<input type="file" id="txtSupportPhoto"
                            name="txtSupportPhoto" />
                        <input style="line-height: 18px; width: 100px; height: 35px; font-size: 14px; font-weight: bold"
                            value="提交对话" onclick="btnSend_onclick();" type="button" id="btnSend" /><br>
                        说明：附件大小必须在1500KB以内，附件必须是<b>.rar/.zip/.jpg/.gif</b>后缀的文件。输入完成后，在IE浏览器中按Ctrl+Enter键可快速提交。
                    </div>
                    若不需要提交咨询，请<a style="color: blue" href="javascript:onBtnCloseClick();"><b>返回列表</b></a>
                </td>
                <td style="border-left: #cccccc 1px solid; padding-left: 6px" valign="top" width="32%">
                    <font style="font-size: 14px" color="#bb0000"><b>改变状态</b></font>：<br />
                    <br />
                    <div id="divStatus">
                        <input id="g1" value="1" checked type="radio" name="grade" /><label for="g1" ><b>未处理</b></label><br>
                        <input id="g3" value="2" type="radio" name="grade" /><label for="g3"><b style="color: red" >处理中</b></label><br>
                        <input id="g2" value="3" type="radio" name="grade" /><label for="g2"><b style="color: green" >完成</b></label><br>
                        <input value="提交状态" type="button" name="SentBtn" onclick="Support_UpdateStatus()" />
                    </div>
                    <div id="divRating" style="display: none">
                        该问题已经结果，等待用户评分！
                    </div>
                </td>
            </tr>
            <tr id="trSupportStatus2" runat="server" style="display: none">
                <td style="line-height: 23px" colspan="2">
                    <b>[友情提示]</b>：此咨询已经处理，您的评分是：<label id="lblSupportRating" runat="server"><font color="green"></font></label>。
                    若仍有问题请<a href="SupportType.aspx"><b><font style="font-size: 14px" color="blue">提交新咨询</font></b></a>，&nbsp;
                    或 <a style="color: blue; font-size: 14px" href="javascript:onBtnCloseClick();"><b>点这里返回列表</b></a>
                </td>
            </tr>
        </tbody>
    </table>
    <table class="tbl_body" border="0" cellspacing="1" cellpadding="5" width="100%">
        <tbody>
            <tr>
                <td class="tbl_row">
                    <b>说明</b>：<br>
                    □当您在此窗口浏览或此窗口未关闭时，管理员的即时回复将视为已经阅读。在管理员回复之后的5天内，允许对回复进行评分或继续在此咨询下对话，超过时间后视为咨询已经完成，不能再评分或对话，需要提交新咨询。<br>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
