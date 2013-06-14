<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcFaceBookCompany.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcFaceBookCompany" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<ele:ResLink ID="rs1" runat="server" Src="/App_Themes/ThemesV2.1/css/review.css" />
<!--点评内容开始-->
<div class="review" id="reviewContainer">
    <a href="javascript:void(0);" class="back_menu">回到菜单</a>
    <div class="tab">
        <a href="javascript:void(0)" class="tab_on">我要点评</a> <a href="javascript:void(0)"
            beginrate="5" endrate="5">好评（<label id="lblFbGood"><%=this.FbGoodCount %></label>）</a>
        <a href="javascript:void(0)" beginrate="2" endrate="4">中评（<label id="lblFbNormal"><%=this.FbNormalCount %></label>）</a>
        <a href="javascript:void(0)" beginrate="0" endrate="1">差评（<label id="lblFbBad"><%=this.FbBadCount %></label>）</a>
    </div>
    <div class="review_con">
        <div class="r1">
            <ul class="my_dp">
                <li><u>给本商家打分：</u>
                    <div>
                        <a href="javascript:void(0)" class="starone" title="很不满意(1)"></a><a href="javascript:void(0)"
                            class="starone" title="不满意(2)"></a><a href="javascript:void(0)" class="starone" title="一般(3)">
                            </a><a href="javascript:void(0)" class="starone" title="满意(4)"></a><a href="javascript:void(0)"
                                class="starone" title="很满意(5)"></a>
                    </div>
                </li>
                <li><u>评价内容：</u>
                    <div>
                        <textarea rows="4" class="i_input" id="fbContent"></textarea>
                    </div>
                </li>
                <li style="padding-left: 90px;"><a href="javascript:void(0);" id="fbSubmit">
                    <img src="/App_Themes/ThemesV2/images/xd-aniu-tj.png" alt="" /></a></li>
            </ul>
        </div>
        <div class="r2" style="display: none">
        </div>
        <div class="r3" style="display: none">
        </div>
        <div class="r4" style="display: none">
        </div>
    </div>
</div>
<script type="text/javascript">
    var FaceBook = (function () {
        var inited = false;
        var reviewContainer = false;
        var fbType = '<%=FaceBookType.OrderMeal %>';
        var bizID = '<%=CompanyID %>';
        var url = "/Public/RestHandler.ashx/FaceBook";
        var patten = eval("{/<%=FaceBookBLL.GetFilterResource() %>/g}");
        var isFocus = false;
        var tabs = false;
        var currentTab = false;
        var currentDIV = false;
        function getAddFaceBookService() {
            return url + "/Add";
        }
        function getQueryFaceBookService() {
            return url;
        }
        function execute(svrUrl, data, fnCallback) {
            data["fbType"] = fbType;
            data["bizID"] = bizID;
            $.ajax({
                type: "POST",
                url: svrUrl,
                dataType: "json", data: data,
                success: function (result) {
                    if (result.code >= 0)
                        fnCallback(result);
                    else
                        alert(result.message);
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
            if (currentTab.attr("beginrate")) {
                showFaceBookCore(1);
            }
        }
        function getFaceBookRate() {
            return $(".star_on", currentDIV).length;
        }
        function getRateIndex(rate) {
            var index = 0;
            tabs.each(function (i) {
                var tab = tabs.eq(i);
                var beginRate = tab.attr("beginrate") || 0;
                var endRate = tab.attr("endrate") || 0;
                if (rate >= beginRate && rate <= endRate) {
                    index = tab.index();
                    return false;
                }
                return true;
            });
            return index;
        }
        function getFaceBookContent() {
            var content = ($("#fbContent", currentDIV).val() || '');
            if (content.length == 0) {
                alert("请输入你的评论内容.");
                return false;
            }
            var matchs = content.match(patten);
            if (matchs && matchs.length > 0) {
                alert("你的评论内容包含非法字符.\n" + matchs.join(","));
                return false;
            }
            return encodeURIComponent(content);
        }
        function submitFaceBook() {
            var rate = getFaceBookRate();
            var content = getFaceBookContent();
            if (!content)
                return;
            var data = { rate: rate, content: content };
            execute(getAddFaceBookService(), data, function (result) {
                $("#fbContent", currentDIV).val('');
                var index = getRateIndex(rate);
                alert("点评成功!");
                tabs.eq(index).click();
            });
        }
        function showFaceBookCore(pageIndex) {
            var beginRate = currentTab.attr("beginrate") || 0;
            var endRate = currentTab.attr("endrate") || 0;
            var data = { beginRate: beginRate, endRate: endRate, pageIndex: pageIndex };
            execute(getQueryFaceBookService(), data, function (result) {
                //{"code":0,"data":{"code":0,"good":0,"normal":0,"bad":0,"html":""}
                $("#faceBookCount").text(result.data.good + result.data.normal + result.data.bad);
                $("#lblFbNormal", reviewContainer).text(result.data.normal);
                $("#lblFbGood", reviewContainer).text(result.data.good);
                $("#lblFbBad", reviewContainer).text(result.data.bad);
                currentDIV.html(result.data.html);
            });
        }
        return {
            showFaceBook: function (pageIndex) {
                showFaceBookCore(pageIndex);
            },
            init: function () {
                if (inited) {
                    return;
                }
                reviewContainer = $("#reviewContainer");
                $("#fbSubmit", reviewContainer).click(submitFaceBook);
                tabs = $(".tab a", reviewContainer);
                tabs.click(function () {
                    by_spells($(this), $(".review_con>div", reviewContainer), "tab_on");
                }).eq(0).click();
                $(".starone", currentDIV).mouseover(function () {
                    $(this).prevAll().add(this).addClass("star_on2");
                }).mouseout(function () {
                    $(this).prevAll().add(this).removeClass("star_on2");
                }).click(function () {
                    var me = $(this);
                    var i = me.index() + 1;
                    var obj = me.parent().children();
                    if (me.hasClass("star_on")) {
                        obj.slice(i, 5).removeClass("star_on");
                    }
                    else {
                        obj.slice(0, i).addClass("star_on");
                    }
                });
                $(".s_dp,.back_menu").click(function () {
                    reviewContainer.toggle();
                    $("#menuBox").toggle();
                })
                inited = true;
            }
        }
    })();
</script>
<!--点评内容结束-->
