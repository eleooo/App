var SendWaitMsnMessage = function (opts) {
    var _opts =
            {
                btnSend: "",
                fnSendHandler: function (fnCallback) { return true }
            };
    var btn;
    function _init() {
        _opts = $.extend(_opts, opts);
        btn = $("#" + _opts.btnSend).click(sendMsn);
    }
    function sendMsn() {
        if (btn.hasClass("dx_gray"))
            return;
        _opts.fnSendHandler(fnCallback);
    }
    function fnCallback() {
        btn.removeClass("dx_link");
        btn.addClass("dx_gray");
        btn.data("Count", 60);
        btn.data("text", btn.attr("text") || btn.text());
        btn.text("60秒后可重发");
        setTimeout(sendPwdTimer, 1000);
    }
    function sendPwdTimer() {
        if (btn.hasClass("dx_link"))
            return;
        var count = (btn.data("Count")) - 1;
        if (count <= 0) {
            btn.removeClass("dx_gray");
            btn.addClass("dx_link");
            btn.text(btn.data("text"));
            return;
        }
        btn.text(count + "秒后可重发");
        btn.data("Count", count);
        setTimeout(sendPwdTimer, 1000);
    }
    _init();
}