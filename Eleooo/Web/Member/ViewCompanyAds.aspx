<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="ViewCompanyAds.aspx.cs" Inherits="Eleooo.Web.Member.ViewCompanyAds" %>

<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <ele:ResLink ID="rs1" runat="server" Src="/App_Themes/ThemesV2/style/admin.css" />
    <ele:ResLink ID="rs2" runat="server" Src="/App_Themes/ThemesV2/style/public.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="contents">
        <div class="ad_play">
            <div class="ad_pic_text">
                <div class="ad_pic" id="viewPlaceHolder">
                </div>
                <div class="ad_text_right">
                    <div class="ad_wz">
                        <%=GetCompanyAdsTitle() %>
                        <span id="timeOutTip">广告时长：<label id="lblTimeOut" style="font: 18px/44px 'Microsoft Yahei';"><%=this.TimerLength %></label>秒</span>
                    </div>
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <thead>
                            <tr>
                                <th colspan="2">
                                    奖励标准
                                </th>
                            </tr>
                        </thead>
                        <tbody id="pointSetting" runat="server">
                            <tr id="trow" runat="server">
                            </tr>
                        </tbody>
                    </table>
                    <p>
                        <input type="button" class="see_btn" name="btnClickAds" id="btnClickAds" /></p>
                </div>
            </div>
        </div>
        <div id="defaultTpl" runat="server" visible="false">
            <input type="button" class="kzwd_btn" name="btnOk" id="btnOk" />
            <a href="javascript:void(0)">
                <img alt="" src="[ImagePath]" id="imgAds" /></a>
        </div>
        <div id="questionTpl" runat="server" visible="false">
            <h2 class="topich2">
                互动答题</h2>
            <div class="subject">
                {0}
                <input type="button" class="test_tj_btn" name="btnOk" id="btnOk" />
            </div>
            <div class="bottom20">
            </div>
        </div>
        <div id="answerWrongTpl" runat="server" visible="false">
            <div class="answer_wrong" id="dlgView">
                <a style="top: 5px; right: 12px; position: absolute;" href="/Public/ViewAdsList.aspx">
                    ×</a>
                <p>
                    很抱歉，您答错了！</p>
                <p>
                    <input type="button" class="s_w_btn" name="btnOk" id="btnOk" /></p>
            </div>
            <a href="javascript:void(0)">
                <img alt="" src="[ImagePath]" id="imgAds" /></a>
        </div>
        <div id="answerRightTpl1" runat="server" visible="false">
            <div class="answer_right" id="dlgView">
                <a style="top: 5px; right: 12px; position: absolute;" id="btnClose" href="javascript:void(0)">
                    ×</a>
                <p>
                    恭喜您，答对了！</p>
                <p>
                    您已进账<span>{0}</span>分</p>
                <p>
                    该商家有最新优惠哦</p>
                <p>
                    <input type="button" class="s_r_btn" name="btnOk" id="btnOk" url="/Public/ViewItemDetail.aspx?ItemID={1}" /></p>
            </div>
            <a href="javascript:void(0)">
                <img alt="" src="[ImagePath]" id="imgAds" /></a>
        </div>
        <div id="answerRightTpl2" runat="server" visible="false">
            <div class="answer_right" id="dlgView">
                <a style="top: 5px; right: 12px; position: absolute;" id="btnClose" href="javascript:void(0)">
                    ×</a>
                <p class="p1">
                    恭喜您，答对了！</p>
                <p>
                    您已进账<span>{0}</span>分</p>
                <p>
                    <input type="button" class="ok_btn" name="btnOk" id="btnOk" url="/Public/ViewAdsList.aspx" /></p>
            </div>
            <a href="javascript:void(0)">
                <img alt="" src="[ImagePath]" id="imgAds" /></a>
        </div>
        <div id="msgTpl" runat="server" visible="false">
            <div class="answer_right" id="dlgView">
                <a style="top: 5px; right: 12px; position: absolute;" id="btnClose" href="javascript:void(0)">
                    ×</a>
                <p class="p1">
                    {0}</p>
                <p>
                    <input type="button" class="ok_btn" name="btnOk" id="btnOk" /></p>
            </div>
            <a href="javascript:void(0)">
                <img alt="" src="[ImagePath]" id="imgAds" /></a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
    <script type="text/javascript" language="javascript">
        var ViewCompanyAd = function () {
            var Views = { Default: "Default", AnswerQuestion: "AnswerQuestion", AnswerRight: "AnswerRight", AnswerWrong: "AnswerWrong", Message: "Message" };
            var _opts =
            {
                ViewTemplate: { Default: "", AnswerQuestion: "", AnswerRight: "", AnswerWrong: "", Message: "" },
                AdImages: [],
                ImgDisDelay: 3000,
                ViewPlaceHolder: "viewPlaceHolder",
                SubmitAdButton: "btnClickAds",
                HasQuestion: false,
                IsCanSee: false,
                RightAnswer: "",
                CompanyItemID: 0,
                DefaultImage: "/App_Themes/Member/images/logo.jpg",
                LabelTimeout: "lblTimeOut",
                AnswerTimeout: 15
            },
            nState = 0, //当前状态0 初始状态 1播放广告,2答题 3已看
            imgeIndex = 0,
            isInited = false,
            isSubmit = false,
            answer,
            timeoutCounter, //计数器
            timeoutCallback,
            isClearTimeout = false;
            this.init = function (opts) {
                if (isInited)
                    return;
                var o = typeof (opts) == 'string' ? JSON.parse(opts) : opts;
                _opts = $.extend(_opts, o);
                if (typeof (_opts.ViewPlaceHolder) == "string")
                    _opts.ViewPlaceHolder = $("#" + _opts.ViewPlaceHolder);
                showDefaultView();
                imgeIndex = -1;
                $("#" + _opts.SubmitAdButton).click(judgeAnswer);
                isInited = true;
            };
            function showDefaultView() {
                showView(Views.Default, onDefultBtnClick);
                if (_opts.IsCanSee)
                    $("#btnOk").hide();
                else
                    $("#" + _opts.SubmitAdButton).hide();
            };
            function onDefultBtnClick() {
                if (!_opts.HasQuestion) {
                    showMessage("此广告没有互动问答!");
                    return;
                }
                if (_opts.IsCanSee)
                    showAnswerQuestionView();
                else
                    window.location.href = "/Public/ViewAdsList.aspx";
            };
            function showAnswerQuestionView() {
                showView(Views.AnswerQuestion, onAnswerQuestionClick);
                $("#" + _opts.SubmitAdButton).hide();
                $(".subject > dl >dd").click(function () { $(".t_radio", this).attr("checked", true) });
            };
            function onAnswerQuestionClick() {
                var a = $('input:radio[name="answer"]:checked').val();
                if (a == null) {
                    alert("请选择你的答案!");
                    return;
                }
                //answerTimeout();
                answer = a;
                judgeAnswer();
            };
            function beginTimer() {
                answerTimeout(function () {
                    $("#btnOk").show();
                    nState = 2;
                });
                nState = 1;
            };
            function judgeAnswer() {
                if (_opts.IsCanSee == false || nState == 1) {
                    return;
                }
                if (_opts.HasQuestion && nState == 0) {
                    showDefaultView();
                    $("#" + _opts.SubmitAdButton).hide();
                    beginTimer();
                    return;
                }
                if (nState == 2 && answer == null) {
                    alert("请先回答问题");
                    return;
                }
                if (nState == 3) {
                    showMessage("你已经看过此广告!", function () {
                        window.location.href = "/Public/ViewAdsList.aspx";
                    });
                    return;
                }

                if (!_opts.HasQuestion || _opts.RightAnswer == answer) {
                    submitAnswer(function (result) {
                        if (result.code == 0) {
                            nState = 3;
                            showRightAnswerView();
                        }
                        else
                            showMessage(result.message);
                    });
                }
                else {
                    showWrongAnswerView();
                }
            };
            function showRightAnswerView() {
                showView(Views.AnswerRight, function () {
                    var url = $("#btnOk").attr("url");
                    if (url) {
                        window.location.href = url;
                    }
                    else
                        showDefaultView();
                });
            };
            function showWrongAnswerView() {
                showView(Views.AnswerWrong, function () {
                    showDefaultView();
                    beginTimer();
                }, function () {
                    $("#" + _opts.SubmitAdButton).show();
                    nState = 0;
                });
            };
            function showView(view, fnCallBack, fnCloseCallBack) {
                var viewContent = _opts.ViewTemplate[view];
                if (viewContent && viewContent.length > 0) {
                    showViewContent(viewContent);
                    if (typeof (fnCallBack) == "function")
                        $("#btnOk").click(fnCallBack);
                    $("#btnClose").click(function () {
                        $("#dlgView").hide();
                        if (typeof (fnCloseCallBack) == "function")
                            fnCloseCallBack();
                    });
                }
            };
            function showViewContent(viewContent) {
                if (viewContent && viewContent.length > 0) {
                    var img = getCurImgPath();
                    _opts.ViewPlaceHolder.html(viewContent.replace("[ImagePath]", img));
                }
            };
            function getCurImgPath() {
                var path = _opts.AdImages[imgeIndex];
                if (!path)
                    return _opts.DefaultImage;
                else
                    return path;
            };
            function showMessage(message, fnCallBack) {
                var viewContent = _opts.ViewTemplate[Views.Message];
                if (viewContent && viewContent.length > 0) {
                    showViewContent(viewContent.replace("{0}", message));
                    $("#btnOk").click(function () {
                        if (typeof (fnCallBack) == "function")
                            fnCallBack.apply(null, []);
                        else
                            showDefaultView();
                    });
                    $("#btnClose").click(function () {
                        $("#dlgView").hide();
                    });
                }
                else
                    alert(message);
            };
            function showDelayImage() {
                if (timeoutCounter >= 0) {
                    var img = $("#imgAds");
                    if (img.length == 0)
                        return;
                    if (imgeIndex == _opts.AdImages.length - 1)
                        imgeIndex = 0;
                    else
                        imgeIndex++;
                    img.attr("src", getCurImgPath());
                    setTimeout(showDelayImage, _opts.ImgDisDelay);
                }
            };
            function answerTimeout(fnTimeoutCallback) {
                if (typeof (fnTimeoutCallback) == 'function') {
                    isClearTimeout = false;
                    timeoutCounter = _opts.AnswerTimeout;
                    timeoutCallback = fnTimeoutCallback;
                    __timeout();
                    imgeIndex = -1;
                    showDelayImage();
                } else {
                    isClearTimeout = true;
                }
            }
            function __timeout() {
                if (isClearTimeout == false) {
                    if (timeoutCounter >= 0) {
                        $("#" + _opts.LabelTimeout).html(timeoutCounter--);
                        setTimeout(__timeout, 1000);
                    } else if (typeof (timeoutCallback) == 'function') {
                        if (imgeIndex != 0) {
                            imgeIndex = 0;
                            var img = $("#imgAds");
                            img.attr("src", getCurImgPath());
                        }
                        timeoutCallback.apply(null, []);
                    }
                }
            }
            function submitAnswer(fnCallBack) {
                var url = this.document.location.href;
                $.getJSON(url, { answer: answer, Action: "Add", p: (new Date()).toLocaleString() }, fnCallBack);
            }
        }
    </script>
</asp:Content>
