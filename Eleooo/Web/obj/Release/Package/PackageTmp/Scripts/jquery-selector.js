;(function ($) {
    var defaults = {
        url: '/Public/RestHandler.ashx/Area?pid=',    //get json data url
        sid: 'S1,S2,S2',             //selector's id
        dv: ',,',                    //default value
        ft: '...请选择...,...请选择...,...请选择...',        //first item text
        fv: '0,0,0',                    //first item value
        pid: 0,                       //top level pid
        renderTo: ''
    };
    var optionsArr = [];
    $.fn.regMulitSelector = function (options) {
        //var genCodeUrl = "/WebRestServices/WebRestService.asmx/GetGenCompanyCode";
        defaults = $.extend({}, defaults, options);
        var sid = defaults.sid.split(',');
        defaults.len = sid.length;

        GetA("", defaults.pid, defaults);

        for (var i = 0; i < sid.length; i++) {
            optionsArr[sid[i]] = defaults;
            $("#" + sid[i]).change(function () {
                var val = $(this).attr("value");
                var id = $(this).attr("id");
                GetA(id, val, optionsArr[id]);
                if (optionsArr[id].renderTo != '' && val > 0) {
                    $("#" + optionsArr[id].renderTo).html('');
                    var genCodeUrl = "/Public/GenCompanyCodeHandler.ashx?p=" + new Date();
                    $.ajax({
                        type: "GET",
                        url: genCodeUrl,
                        dataType: "xml", data: "areaID=" + $(this).attr("value"),
                        success: function (xml) {
                            $("#" + optionsArr[id].renderTo).html($(xml).find("string").text());
                        }
                    });
                }
            });
        }
    };

    function GetA(str_id, pid, options) {
        var arrSid = options.sid.split(',');
        var arrDv = options.dv.split(',');
        var arrFt = options.ft.split(',');
        var arrFv = options.fv.split(',');

        var self;
        var kk = 0;
        if (str_id != "") {
            while (kk <= arrSid.length) {
                kk++;
                if (arrSid[kk - 1] == str_id)
                    break;
            }
        }
        self = $("#" + arrSid[kk]);

        for (var i = kk; i <= arrSid.length; i++) {
            $("#" + arrSid[i]).html("");
        }

        if (arrFt[kk] != "")
            self.append("<option value='" + arrFv[kk] + "'>" + arrFt[kk] + "</option>");
        if(pid==-1)
            return;
        $.getJSON(defaults.url + pid, function (result) {
            var data = result.data;
            for (var i = 0; i < data.length; i++) {
                if (data[i].id && data[i].name) {
                    var $opt = $("<option></option>").val(data[i].id).html(data[i].name);
                    if (arrDv[kk] == data[i].id) $opt.attr("selected", "selected");
                    self.append($opt);
                }
            };
            
            if (arrSid[kk + 1] != undefined)
                GetA(arrSid[kk], self.attr("value") !=='0'? self.attr("value") : -1, options);
        });

    }
})(jQuery);     