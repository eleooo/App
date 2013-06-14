<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="EleoooComment.aspx.cs" Inherits="Eleooo.Web.Public.EleoooComment" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <ele:ResLink ID="rs1" runat="server" Src="/App_Themes/ThemesV2.1/css/review.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="container" id="reviewContainer">
        <div class="eLeft">
            <!--吐槽开始-->
            <div class="episode">
                <span class="poselo">想对乐多分说点啥，我们会努力改进滴^_^</span>
                <div class="tab2">
                    <a href="javascript:void(0)" showme="false">大家吐槽</a> <a href="javascript:void(0)"
                        showme="true">我要吐槽</a>
                </div>
                <div class="review_con2">
                    <div style="display: block;" class="r1">
                        <%--                        <ul class="my_dp" id="inputbox0">
                            <li><u>我来说说：</u>
                                <div>
                                    <textarea class="i_input" rows="4" id="fbContent0"></textarea>
                                </div>
                            </li>
                            <li style="padding-left: 90px;"><a href="javascript:void(0);" onclick="FaceBook.submitFaceBook(0)">
                                <img alt="" src="/App_Themes/ThemesV2.1/images/xd-aniu-tj.png" /></a></li>
                        </ul>--%>
                        <div id="reviewList">
                        </div>
                    </div>
                    <div style="display: none;" class="r2">
                        <ul class="my_dp" id="inputboxmy0">
                            <li><u>我来说说：</u>
                                <div>
                                    <textarea class="i_input" rows="4" id="fbContentmy0"></textarea>
                                </div>
                            </li>
                            <li style="padding-left: 90px;"><a href="javascript:void(0);" onclick="FaceBook.submitFaceBook(0)">
                                <img alt="" src="/App_Themes/ThemesV2.1/images/xd-aniu-tj.png" /></a></li>
                        </ul>
                        <div id="reviewListmy">
                        </div>
                    </div>
                </div>
            </div>
            <!--吐槽结束-->
        </div>
        <div class="eRight">
            <div class="r_p">
                <a href="#">
                    <img alt="" src="/App_Themes/ThemesV2/images/eloo_01.jpg" /></a></div>
            <div class="r_p">
                <a href="#">
                    <img alt="" src="/App_Themes/ThemesV2/images/eloo_02.jpg" /></a></div>
            <div class="r_p">
                <a href="#">
                    <img alt="" src="/App_Themes/ThemesV2/images/eloo_03.jpg" /></a></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
    <script type="text/javascript">
        var FaceBook = (function () {
            var inited = false;
            var reviewContainer = false;
            var fbType = '<%=FaceBookType.Eleooo %>';
            var bizID = '0';
            var url = "/Public/RestHandler.ashx/FaceBook";
            var patten = eval("{/<%=FaceBookBLL.GetFilterResource() %>/g}");
            var isFocus = false;
            var tabs = false;
            var currentTab = false;
            var currentDIV = false;
            var isShowMe = false;
            var currentInputBox = false;
            var isSumitting = false;
            function getInputBoxId(boxType) {
                return "inputbox" + (isShowMe ? "my" : "") + boxType;
            }
            function getFbContentId(pBizID) {
                return "fbContent" + (isShowMe ? "my" : "") + pBizID;
            }
            function getReviewListId() {
                return "reviewList" + (isShowMe ? "my" : "");
            }
            function getReviewListItemId(pBizID) {
                return "item" + (isShowMe ? "my" : "") + pBizID;
            }
            function getAddFaceBookService() {
                return url + "/Add";
            }
            function getQueryFaceBookService() {
                return url;
            }
            function execute(svrUrl, data, fnCallback) {
                data["fbType"] = fbType;
                data["bizID"] = bizID;
                data["showme"] = isShowMe;
                $.ajax({
                    type: "POST",
                    url: svrUrl,
                    dataType: "json", data: data,
                    success: function (result) {
                        isSumitting = false;
                        if (result.code >= 0) {
                            fnCallback(result);
                        } else {
                            alert(result.message);
                            if (result.message.indexOf("登录") > 0) {
                                document.location.href = "/Public/Login.aspx?returnUrl=/Public/EleoooComment.aspx";
                            }
                        }
                    }
                });
            }
            function by_spells(my_turn, my_show, ON) {
                if (my_turn.hasClass(ON) && !isFocus)
                    return;
                currentTab = my_turn;
                currentDIV = my_show.eq(my_turn.index());
                currentTab.addClass(ON).siblings().removeClass(ON);
                currentDIV.show().siblings().hide();
                isFocus = false;
                showFaceBookCore(1);
            }
            function getFaceBookContent(fbContent) {
                var contentObj = $("#" + fbContent, currentDIV);
                var content = (contentObj.val() || '');
                if (content.length == 0) {
                    alert("请输入你的吐槽内容.");
                    contentObj.focus();
                    return false;
                }
                var matchs = content.match(patten);
                if (matchs && matchs.length > 0) {
                    alert("你的吐槽内容包含非法字符.\n" + matchs.join(","));
                    contentObj.focus();
                    return false;
                }
                return encodeURIComponent(content);
            }
            function onShow(result) {
                $("#" + getReviewListId(), currentDIV).html(result.data.html);
                $("#" + getInputBoxId('0')).show();
                currentInputBox = false;
            }
            function showFaceBookCore(pageIndex) {
                var data = { pageIndex: pageIndex };
                execute(getQueryFaceBookService(), data, onShow);
            }
            return {
                showFaceBook: function (pageIndex) {
                    showFaceBookCore(pageIndex);
                },
                changeTab: function (index) {
                    tabs.eq(index).click();
                },
                submitFaceBook: function (pBiz) {
                    if (isSumitting) {
                        alert("正在提交...");
                        return;
                    }
                    var fbContent = getFbContentId(pBiz);
                    var content = getFaceBookContent(fbContent);
                    if (!content)
                        return;
                    var data = { content: content, pBiz: pBiz };
                    isSumitting = true;
                    execute(getAddFaceBookService(), data, function (result) {
                        if (result.code == 0) {
                            $("#" + fbContent, currentDIV).val('');
                            alert("吐槽成功!");
                            //tabs.eq(0).click();
                            if (pBiz > 0) {
                                $("#" + getReviewListItemId(pBiz), currentDIV).replaceWith(result.data.html);
                                $("#" + getInputBoxId('0')).show();
                                currentInputBox = false;
                            } else {
                                onShow(result);
                            }
                        } else
                            alert(result.message);
                    });
                },
                showInputBox: function (boxType) {
                    var boxId = getInputBoxId(boxType);
                    if (currentInputBox && currentInputBox.attr("id") == boxId) {
                        currentInputBox.hide();
                        currentInputBox = false;
                        $("#" + getInputBoxId('0')).show();
                    } else {
                        if (currentInputBox)
                            currentInputBox.hide();
                        else
                            $("#" + getInputBoxId('0')).hide();
                        currentInputBox = $("#" + boxId).show();
                        setTimeout(function () {
                            var fbId = getFbContentId(boxType);
                            $("#" + fbId).focus();
                        }, 100);
                    }
                    //return false;
                },
                init: function () {
                    if (inited) {
                        return;
                    }
                    reviewContainer = $("#reviewContainer");
                    tabs = $(".tab2 a", reviewContainer);
                    tabs.click(function () {
                        isShowMe = $(this).attr("showme") == "true";
                        by_spells($(this), $(".review_con2 > div", reviewContainer), "tab_on");
                    });
                    if ('<%=Request["act"] %>' == "1")
                        tabs.eq(1).click();
                    else
                        tabs.eq(0).click();
                    inited = true;
                }
            }
        })();
        $(document).ready(FaceBook.init);
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dlgSupport" runat="server">
</asp:Content>
