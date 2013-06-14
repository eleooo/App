(function ($) {
    function individual(_jqobj, _source, _options) {
        function initOptions(_source, _options) {
            function _strToArray(str) {
                return str.replace(/[\s　]+/g, "").split(",")
            }
            function _setRegExpShort(shorten_reg, shorten_min) {
                if (shorten_reg)
                    return shorten_reg;
                var reg = "(?:^|[\\s|　\\[(<「『（【［＜〈《]+)";
                return reg += "(",
                    reg += "https:\\/\\/[^\\s|　\\])>」』）】］＞〉》]{" + (shorten_min - 7) + ",}",
                    reg += "|",
                    reg += "http:\\/\\/[^\\s|　\\])>」』）】］＞〉》]{" + (shorten_min - 6) + ",}",
                    reg += "|",
                    reg += "ftp:\\/\\/[^\\s|　\\])>」』）】］＞〉》]{" + (shorten_min - 5) + ",}",
                    reg += ")",
                    new RegExp(reg, "g")
            }
            function _setTagPattern(tags) {
                function _setTagOptions(tag) {
                    var arr,
                        i;
                    for (tag = $.extend({
                        space: [!0, !0],
                        db_table: _options.db_table,
                        field: _options.field,
                        search_field: _options.search_field,
                        primary_key: _options.primary_key,
                        sub_info: _options.sub_info,
                        sub_as: _options.sub_as,
                        show_field: _options.show_field,
                        hide_field: _options.hide_field
                    }, tag), arr = ["hide_field", "show_field", "search_field"], i = 0; i < arr.length; i++)
                        typeof tag[arr[i]] != "object" && (tag[arr[i]] = _strToArray(tag[arr[i]]));
                    return tag.order_by = tag.order_by == undefined ? _options.order_by : _setOrderbyOption(tag.order_by, tag.field),
                        tag
                }
                function _setRegExpTag(pattern) {
                    function _escapeForReg(text) {
                        return "\\u" + (65536 + text.charCodeAt(0)).toString(16).slice(1)
                    }
                    var esc_left = pattern[0].replace(/[\s\S]*/, _escapeForReg),
                        esc_right = pattern[1].replace(/[\s\S]*/, _escapeForReg);
                    return {
                        left: pattern[0],
                        right: pattern[1],
                        reg_left: new RegExp(esc_left + "((?:(?!" + esc_left + "|" + esc_right + ")[^\\s　])*)$"),
                        reg_right: new RegExp("^((?:(?!" + esc_left + "|" + esc_right + ")[^\\s　])+)"),
                        space_left: new RegExp("^" + esc_left + "$|[\\s　]+" + esc_left + "$"),
                        space_right: new RegExp("^$|^[\\s　]+"),
                        comp_right: new RegExp("^" + esc_right)
                    }
                }
                for (var i = 0; i < tags.length; i++)
                    tags[i] = _setTagOptions(tags[i]), tags[i].pattern = _setRegExpTag(tags[i].pattern, tags[i].space);
                return tags
            }
            function _setOrderbyOption(arg_order, arg_field) {
                var arr = [],
                    i,
                    orders;
                if (typeof arg_order == "object")
                    for (i = 0; i < arg_order.length; i++)
                        orders = $.trim(arg_order[i]).split(" "), arr[i] = orders.length == 2 ? orders : [orders[0], "ASC"];
                else
                    orders = $.trim(arg_order).split(" "), arr[0] = orders.length == 2 ? orders : orders[0].match(/^(ASC|DESC)$/i) ? [arg_field, orders[0]] : [orders[0], "ASC"];
                return arr
            }
            var arr,
                i;
            for (_options = $.extend({
                source: _source,
                lang: "cn",
                plugin_type: "combobox",
                init_record: !1,
                db_table: "tbl",
                field: "name",
                and_or: "AND",
                per_page: 20,
                navi_num: 20,
                primary_key: "id",
                button_img: "/Scripts/acbox/jquery.ajaxComboBox.button.png",
                bind_to: !1,
                navi_simple: !1,
                sub_info: !1,
                sub_as: {},
                show_field: "",
                hide_field: "",
                select_only: !1,
                tags: !1,
                input_padding_left: 0,
                empty_message: "",
                allow_page: false,
                shorten_url: !1,
                shorten_src: "acbox/bitly.php",
                shorten_min: 20,
                shorten_reg: !1
            }, _options), _options.search_field = _options.search_field == undefined ? _options.field : _options.search_field, _options.and_or = _options.and_or.toUpperCase(), arr = ["hide_field", "show_field", "search_field"], i = 0; i < arr.length; i++)
                _options[arr[i]] = _strToArray(_options[arr[i]]);
            return _options.order_by = _options.order_by == undefined ? _options.search_field : _options.order_by,
                _options.order_by = _setOrderbyOption(_options.order_by, _options.field),
                _options.plugin_type == "textarea" && (_options.shorten_reg = _setRegExpShort(_options.shorten_reg, _options.shorten_min)),
                _options.tags && (_options.tags = _setTagPattern(_options.tags)),
                _options
        }
        function initMessages() {
            switch (Opt.lang) {
                case "en":
                    return {
                        add_btn: "Add button",
                        add_title: "add a box",
                        del_btn: "Del button",
                        del_title: "delete a box",
                        next: "Next",
                        next_title: "Next" + Opt.per_page + " (Right key)",
                        prev: "Prev",
                        prev_title: "Prev" + Opt.per_page + " (Left key)",
                        first_title: "First (Shift + Left key)",
                        last_title: "Last (Shift + Right key)",
                        get_all_btn: "Get All (Down key)",
                        get_all_alt: "(button)",
                        close_btn: "Close (Tab key)",
                        close_alt: "(button)",
                        loading: "loading...",
                        loading_alt: "(loading)",
                        page_info: "num_page_top - num_page_end of cnt_whole",
                        select_ng: "Attention : Please choose from among the list.",
                        select_ok: "OK : Correctly selected.",
                        not_found: "not found"
                    };
                case "es":
                    return {
                        add_btn: "Agregar boton",
                        add_title: "Agregar una opcion",
                        del_btn: "Borrar boton",
                        del_title: "Borrar una opcion",
                        next: "Siguiente",
                        next_title: "Proximas " + Opt.per_page + " (tecla derecha)",
                        prev: "Anterior",
                        prev_title: "Anteriores " + Opt.per_page + " (tecla izquierda)",
                        first_title: "Primera (Shift + Left)",
                        last_title: "Ultima (Shift + Right)",
                        get_all_btn: "Ver todos (tecla abajo)",
                        get_all_alt: "(boton)",
                        close_btn: "Cerrar (tecla TAB)",
                        close_alt: "(boton)",
                        loading: "Cargando...",
                        loading_alt: "(Cargando)",
                        page_info: "num_page_top - num_page_end de cnt_whole",
                        select_ng: "Atencion: Elija una opcion de la lista.",
                        select_ok: "OK: Correctamente seleccionado.",
                        not_found: "no encuentre"
                    };
                case "cn":
                    return {
                        add_btn: "添加",
                        add_title: "添加一项",
                        del_btn: "删除",
                        del_title: "删除一项",
                        next: "->",
                        next_title: "向前" + Opt.per_page + "页",
                        prev: "<-",
                        prev_title: "向后" + Opt.per_page + "页",
                        first_title: "第一页",
                        last_title: "最后一页",
                        get_all_btn: "显示全部",
                        get_all_alt: "显示全部",
                        close_btn: "关闭",
                        close_alt: "确定要关闭",
                        loading: "正在查询...",
                        loading_alt: "正在查询...",
                        page_info: "num_page_top - num_page_end 件 (全 cnt_whole 件)",
                        select_ng: "注意否:是重新选择",
                        select_ok: "选择完成。",
                        not_found: "没有找到这个位置，换个名称试试吧^_^"
                    }
            }
        }
        function initCssClassName() {
            var class_name = {
                container: "ac_container",
                container_open: "ac_container_open",
                selected: "ac_selected",
                re_area: "ac_result_area",
                navi: "ac_navi",
                results: "ac_results",
                re_off: "ac_results_off",
                select: "ac_over",
                sub_info: "ac_subinfo",
                select_ok: "ac_select_ok",
                select_ng: "ac_select_ng",
                input_off: "ac_input_off"
            };
            switch (Opt.plugin_type) {
                case "combobox":
                    class_name = $.extend(class_name, {
                        button: "ac_button",
                        btn_on: "ac_btn_on",
                        btn_out: "ac_btn_out",
                        input: "ac_input"
                    });
                    break;
                case "simple":
                    class_name = $.extend(class_name, {
                        input: "ac_s_input"
                    });
                    break;
                case "textarea":
                    class_name = $.extend(class_name, {
                        input: "ac_textarea",
                        btn_short_off: "ac_btn_short_off"
                    })
            }
            return class_name
        }
        function initLocalVars() {
            return {
                timer_valchange: !1,
                is_suggest: !1,
                page_all: 1,
                page_suggest: 1,
                max_all: 1,
                max_suggest: 1,
                is_paging: !1,
                is_loading: !1,
                reserve_btn: !1,
                reserve_click: !1,
                xhr: !1,
                key_paging: !1,
                key_select: !1,
                prev_value: "",
                size_navi: null,
                size_results: null,
                size_li: null,
                size_left: null,
                tag: null
            }
        }
        function initElements(_jqobj) {
            var elems = {},
                hidden_name;
            elems.combo_input = $(_jqobj).attr("autocomplete", "off").addClass(Cls.input),
                elems.container = $(elems.combo_input).parent(),
                elems.clear = $("<div>").css("clear", "left"),
                Opt.plugin_type == "combobox" ? (elems.button = $("<span>").addClass(Cls.button), elems.img = $("<img>").attr("src", Opt.button_img)) : (elems.button = !1, elems.img = !1),
                elems.result_area = $("<div>").addClass(Cls.re_area),
                elems.navi = $("<div>").addClass(Cls.navi),
                elems.navi_info = $("<div>").addClass("info"),
                elems.navi_p = $("<p>"),
                elems.results = $("<ul>").addClass(Cls.results),
                elems.sub_info = $("<div>").addClass(Cls.sub_info),
                Opt.plugin_type == "textarea" ? elems.hidden = !1 : (hidden_name = $(elems.combo_input).attr("name") != undefined ? $(elems.combo_input).attr("name") : $(elems.combo_input).attr("id"), hidden_name.match(/\]$/) ? hidden_name = hidden_name.replace(/\]?$/, "_primary_key]") : hidden_name += "_primary_key", elems.hidden = $('<input type="hidden" />').attr({
                    name: hidden_name,
                    id: hidden_name
                }).val(""));
            switch (Opt.plugin_type) {
                case "combobox":
                    $(elems.combo_input).after(elems.hidden).after(elems.result_area).after(elems.button),
                    $(elems.button).append(elems.img);
                    break;
                case "simple":
                    //$(elems.container).append(elems.clear).append(elems.result_area).append(elems.hidden);
                    $(elems.combo_input).after(elems.hidden).after(elems.result_area).after(elems.button);
                    break;
                case "textarea":
                    $(elems.container).append(elems.clear).append(elems.result_area)
            }
            return $(elems.result_area).append(elems.navi).append(elems.results).append(elems.sub_info),
                Opt.allow_page ? $(elems.navi).append(elems.navi_info).append(elems.navi_p) : $(elems.navi).append(elems.navi_info),
                Opt.plugin_type == "combobox" ? $(elems.button).height($(elems.combo_input).innerHeight()) : null,
                elems
        }
        function btnAttrDefault() {
            Opt.select_only && ($(Elem.combo_input).val() != "" ? Opt.plugin_type != "textarea" && ($(Elem.hidden).val() != "" ? $(Elem.combo_input).attr("title", Msg.select_ok).removeClass(Cls.select_ng).addClass(Cls.select_ok) : $(Elem.combo_input).attr("title", Msg.select_ng).removeClass(Cls.select_ok).addClass(Cls.select_ng)) : (Opt.plugin_type != "textarea" && $(Elem.hidden).val(""), $(Elem.combo_input).removeAttr("title").removeClass(Cls.select_ng))),
                Opt.plugin_type == "combobox" && ($(Elem.button).attr("title", Msg.get_all_btn), $(Elem.img).attr("src", Opt.button_img))
        }
        function btnPositionAdjust() {
            if (Opt.plugin_type == "combobox") {
                var width_btn = $(Elem.button).innerWidth(),
                    height_btn = $(Elem.button).innerHeight() - 1,
                    width_img = $(Elem.img).width(),
                    height_img = $(Elem.img).height(),
                    left = width_btn / 2 - width_img / 2,
                    top = height_btn / 2 - height_img / 2;
                if (left > 0 && top > 0) {
                    $(Elem.img).css({
                        top: top,
                        left: left
                    })
                }
            }
        }
        function setInitRecord() {
            function _afterInit(data) {
                $(Elem.combo_input).val(data[Opt.field]),
                    Opt.plugin_type != "textarea" && $(Elem.hidden).val(data[Opt.primary_key]),
                    Vars.prev_value = data[Opt.field],
                    Opt.select_only && $(Elem.combo_input).attr("title", Msg.select_ok).removeClass(Cls.select_ng).addClass(Cls.select_ok)
            }
            var i,
                data;
            if (Opt.init_record !== !1)
                if (Opt.plugin_type != "textarea" && $(Elem.hidden).val(Opt.init_record), typeof Opt.source == "object") {
                    for (i = 0; i < Opt.source.length; i++)
                        if (Opt.source[i][Opt.primary_key] == Opt.init_record) {
                            data = Opt.source[i];
                            break
                        }
                    _afterInit(data)
                } else if (typeof Opt.init_record != 'string') {
                    _afterInit(Opt.init_record);
                }
                else
                    $.getJSON(Opt.source, {
                        db_table: Opt.db_table,
                        pkey_name: Opt.primary_key,
                        pkey_val: Opt.init_record
                    }, _afterInit)
        }
        function eHandlerForButton() {
            Opt.plugin_type == "combobox" && $(Elem.button).mouseup(function (ev) {
                $(Elem.result_area).is(":hidden") ? (clearInterval(Vars.timer_valchange), Vars.is_suggest = !1, suggest(), $(Elem.combo_input).focus()) : hideResults(),
                        ev.stopPropagation()
            }).mouseover(function () {
                $(Elem.button).addClass(Cls.btn_on).removeClass(Cls.btn_out)
            }).mouseout(function () {
                $(Elem.button).addClass(Cls.btn_out).removeClass(Cls.btn_on)
            }).mouseout()
        }
        function eHandlerForInput() {
            window.opera ? $(Elem.combo_input).keypress(processKey) : $(Elem.combo_input).keydown(processKey),
                $(Elem.combo_input).focus(function () { showEmptyMessage(false); setTimerCheckValue(); })
                                   .focusout(function () { showEmptyMessage(true); })
                                   .click(function () {
                                       cssFocusInput(),
                                       $(Elem.results).children("li").removeClass(Cls.select)
                                   })
        }
        function eHandlerForWhole() {
            var stop_hide = !1;
            $(Elem.container).mousedown(function () {
                stop_hide = !0
            }),
                $("html").mousedown(function () {
                    stop_hide ? stop_hide = !1 : hideResults()
                })
        }
        function eHandlerForResults() {
            $(Elem.results).children("li").mouseover(function () {
                if (Vars.key_select) {
                    Vars.key_select = !1;
                    return
                }
                setSubInfo(this),
                        $(Elem.results).children("li").removeClass(Cls.select),
                        $(this).addClass(Cls.select),
                        cssFocusResults()
            }).click(function (e) {
                if (Vars.key_select) {
                    Vars.key_select = !1;
                    return
                }
                e.preventDefault(),
                        e.stopPropagation(),
                        selectCurrentLine(!1)
            })
        }
        function eHandlerForNaviPaging() {
            $(Elem.navi).find(".navi_first").mouseup(function (ev) {
                $(Elem.combo_input).focus(),
                        ev.preventDefault(),
                        firstPage()
            }),
                $(Elem.navi).find(".navi_prev").mouseup(function (ev) {
                    $(Elem.combo_input).focus(),
                        ev.preventDefault(),
                        prevPage()
                }),
                $(Elem.navi).find(".navi_page").mouseup(function (ev) {
                    $(Elem.combo_input).focus(),
                        ev.preventDefault(),
                        Vars.is_suggest ? Vars.page_suggest = parseInt($(this).text(), 10) : Vars.page_all = parseInt($(this).text(), 10),
                        Vars.is_paging = !0,
                        suggest()
                }),
                $(Elem.navi).find(".navi_next").mouseup(function (ev) {
                    $(Elem.combo_input).focus(),
                        ev.preventDefault(),
                        nextPage()
                }),
                $(Elem.navi).find(".navi_last").mouseup(function (ev) {
                    $(Elem.combo_input).focus(),
                        ev.preventDefault(),
                        lastPage()
                })
        }
        function eHandlerForTextArea() {
            Opt.shorten_url && $(Opt.shorten_url).click(getShortURL)
        }
        function setLoading() {
            $(Elem.navi_info).text(Msg.loading),
                $(Elem.results).html() == "" && ($(Elem.navi).children("p").empty(), calcWidthResults())
        }
        function scrollWindow(enforce) {
            var current_result = getCurrentLine(),
                target_top = current_result && !enforce ? current_result.offset().top : $(Elem.container).offset().top,
                target_size,
                dl;
            Opt.sub_info ? (dl = $(Elem.sub_info).children("dl:visible"), target_size = $(dl).height() + parseInt($(dl).css("border-top-width"), 10) + parseInt($(dl).css("border-bottom-width"), 10)) : (Vars.size_li = $(Elem.results).children("li:first").outerHeight(), target_size = Vars.size_li);
            var client_height = document.documentElement.clientHeight,
                scroll_top = document.documentElement.scrollTop > 0 ? document.documentElement.scrollTop : document.body.scrollTop,
                scroll_bottom = scroll_top + client_height - target_size,
                gap;
            if ($(current_result).length)
                if (target_top < scroll_top || target_size > client_height)
                    gap = target_top - scroll_top;
                else if (target_top > scroll_bottom)
                    gap = target_top - scroll_bottom;
                else
                    return;
            else
                target_top < scroll_top && (gap = target_top - scroll_top);
            window.scrollBy(0, gap)
        }
        function cssFocusInput() {
            $(Elem.results).addClass(Cls.re_off),
                $(Elem.combo_input).removeClass(Cls.input_off),
                $(Elem.sub_info).children("dl").hide()
        }
        function cssFocusResults() {
            $(Elem.results).removeClass(Cls.re_off),
                $(Elem.combo_input).addClass(Cls.input_off)
        }
        function btnShortEnable() {
            $(Opt.shorten_url).removeClass(Cls.btn_short_off).removeAttr("disabled")
        }
        function btnShortDisable() {
            $(Opt.shorten_url).addClass(Cls.btn_short_off).attr("disabled", "disabled")
        }
        function setTimerCheckValue() {
            Vars.timer_valchange = setTimeout(checkValue, 500)
        }
        function showEmptyMessage(isShow) {
            var now_value = $(Elem.combo_input).val();
            if (isShow) {
                if ((!now_value && now_value != Opt.empty_message) || now_value == '')
                    $(Elem.combo_input).val(Opt.empty_message);
            }
            else {
                if (now_value == Opt.empty_message)
                    $(Elem.combo_input).val('');
            }
        }
        function checkValue() {
            var now_value = $(Elem.combo_input).val();
            if (now_value != Vars.prev_value && now_value != Opt.empty_message) {
                Vars.prev_value = now_value;
                if (Opt.plugin_type == 'textarea') {
                    findShort();

                    var tag = findTag(now_value);
                    if (tag) {
                        _setTextAreaNewSearch(tag);
                        suggest(tag);
                    }
                } else {
                    $(Elem.combo_input).removeAttr('sub_info');

                    if (Opt.plugin_type != 'textarea') $(Elem.hidden).val('');

                    if (Opt.select_only) btnAttrDefault();

                    Vars.page_suggest = 1;

                    Vars.is_suggest = true;

                    suggest();
                }
            } else if (
				Opt.plugin_type == 'textarea' &&
				$(Elem.result_area).is(':visible')
			) {
                var new_tag = findTag(now_value);
                if (!new_tag) {
                    hideResults();
                } else if (
					new_tag.str != Vars.tag.str ||
					new_tag.pos_left != Vars.tag.pos_left
				) {
                    _setTextAreaNewSearch(new_tag);
                    suggest();
                }
            }
            setTimerCheckValue();
        }
        function _setTextAreaNewSearch(tag) {
            Vars.tag = tag,
                Vars.page_suggest = 1,
                Opt.search_field = Opt.tags[Vars.tag.type].search_field,
                Opt.order_by = Opt.tags[Vars.tag.type].order_by,
                Opt.primary_key = Opt.tags[Vars.tag.type].primary_key,
                Opt.db_table = Opt.tags[Vars.tag.type].db_table,
                Opt.field = Opt.tags[Vars.tag.type].field,
                Opt.sub_info = Opt.tags[Vars.tag.type].sub_info,
                Opt.sub_as = Opt.tags[Vars.tag.type].sub_as,
                Opt.show_field = Opt.tags[Vars.tag.type].show_field,
                Opt.hide_field = Opt.tags[Vars.tag.type].hide_field
        }
        function findShort() {
            for (var flag = null, arr = null; (arr = Opt.shorten_reg.exec($(Elem.combo_input).val())) != null; ) {
                flag = !0,
                    Opt.shorten_reg.lastIndex = 0;
                break
            }
            flag ? btnShortEnable() : btnShortDisable()
        }
        function getShortURL() {
            var obj_param,
                i;
            $(Elem.combo_input).attr("disabled", "disabled");
            for (var text = $(Elem.combo_input).val(), matches = [], arr = null; (arr = Opt.shorten_reg.exec(text)) != null; )
                matches[matches.length] = arr[1];
            if (matches.length < 1) {
                $(Elem.combo_input).removeAttr("disabled");
                return
            }
            for (obj_param = {}, i = 0; i < matches.length; i++)
                obj_param["p_" + i] = matches[i];
            $.getJSON(Opt.shorten_src, obj_param, function (json) {
                var i = 0,
                        result = text.replace(Opt.shorten_reg, function () {
                            var matched = arguments[0].replace(arguments[1], json[i]);
                            return i++,
                                    matched
                        });
                $(Elem.combo_input).val(result),
                        Vars.prev_value = result,
                        $(Elem.combo_input).focus(),
                        btnShortDisable(),
                        $(Elem.combo_input).removeAttr("disabled")
            })
        }
        function findTag(now_value) {
            var pos = getCaretPos($(Elem.combo_input).get(0)),
                i,
                left,
                pos_left,
                right,
                pos_right,
                str;
            if (window.opera) {
                var textwhole = $(Elem.combo_input).val(),
                    textdouble = textwhole.replace(/\n/g, "\nq"),
                    range = textdouble.substr(0, pos),
                    arr_skip = range.match(/\n/g),
                    len_skip = arr_skip ? arr_skip.length : 0;
                pos = pos - len_skip
            }
            for (i = 0; i < Opt.tags.length; i++)
                if (left = now_value.substring(0, pos), left = left.match(Opt.tags[i].pattern.reg_left), left)
                    return left = left[1], pos_left = pos - left.length, pos_left < 0 && (pos_left = 0), right = now_value.substring(pos, now_value.length), right = right.match(Opt.tags[i].pattern.reg_right), right ? (right = right[1], pos_right = pos + right.length) : (right = "", pos_right = pos), str = left + "" + right, Vars.is_suggest = str == "" ? !1 : !0, {
                        type: i,
                        str: str,
                        pos_left: pos_left,
                        pos_right: pos_right
                    };
            return !1
        }
        function getCaretPos(item) {
            var pos = 0,
                Sel;
            return document.selection ? (item.focus(), Sel = document.selection.createRange(), Sel.moveStart("character", -item.value.length), pos = Sel.text.length) : (item.selectionStart || item.selectionStart == "0") && (pos = item.selectionStart),
                pos
        }
        function setCaretPos(pos) {
            var item = $(Elem.combo_input).get(0),
                range;
            item.setSelectionRange ? (item.focus(), item.setSelectionRange(pos, pos)) : item.createTextRange && (range = item.createTextRange(), range.collapse(!0), range.moveEnd("character", pos), range.moveStart("character", pos), range.select())
        }
        function processKey(e) {
            if ($.inArray(e.keyCode, [27, 38, 40, 9]) > -1 && $(Elem.result_area).is(":visible") || $.inArray(e.keyCode, [37, 39, 13, 9]) > -1 && getCurrentLine() || e.keyCode == 40 && Opt.plugin_type != "textarea") {
                e.preventDefault(),
                    e.stopPropagation(),
                    e.cancelBubble = !0,
                    e.returnValue = !1;
                switch (e.keyCode) {
                    case 37:
                        e.shiftKey ? firstPage() : prevPage();
                        break;
                    case 38:
                        Vars.key_select = !0,
                        prevLine();
                        break;
                    case 39:
                        e.shiftKey ? lastPage() : nextPage();
                        break;
                    case 40:
                        $(Elem.results).children("li").length ? (Vars.key_select = !0, nextLine()) : (Vars.is_suggest = !1, suggest());
                        break;
                    case 9:
                        Vars.key_paging = !0,
                        hideResults();
                        break;
                    case 13:
                        selectCurrentLine(!0);
                        break;
                    case 27:
                        Vars.key_paging = !0,
                        hideResults()
                }
            } else
                e.keyCode != 16 && cssFocusInput(), checkValue()
        }
        function abortAjax() {
            Vars.xhr && (Vars.xhr.abort(), Vars.xhr = !1)
        }
        function suggest() {
            var q_word,
                obj,
                which_page_num;
            if (Opt.plugin_type != "textarea") {
                if (q_word = Vars.is_suggest ? $.trim($(Elem.combo_input).val()) : "", q_word.length < 2 && Vars.is_suggest) {
                    hideResults();
                    return
                }
                q_word = q_word.split(/[\s　]+/)
            } else
                q_word = [Vars.tag.str];
            abortAjax(),
            //setLoading(),
                $(Elem.sub_info).children("dl").hide(),
                Vars.is_paging ? (obj = getCurrentLine(), Vars.is_paging = obj ? $(Elem.results).children("li").index(obj) : -1) : Vars.is_suggest || (Vars.is_paging = 0),
                which_page_num = Vars.is_suggest ? Vars.page_suggest : Vars.page_all,
                typeof Opt.source == "object" ? searchForJSON(q_word, which_page_num) : searchForDB(q_word, which_page_num)
        }
        function searchForDB(q_word, which_page_num) {
            var word = $.isArray(q_word) ? q_word.join() : word;
            Vars.xhr = $.getJSON(Opt.source, {
                q_word: word,
                page_num: which_page_num,
                per_page: Opt.per_page,
                search_field: Opt.search_field,
                and_or: Opt.and_or,
                order_by: Opt.order_by,
                db_table: Opt.db_table
            }, function (json) {
                if (!json || json.code < 0) {
                    hideResults();
                    return;
                }
                json = json.data;
                if (json.candidate = [], json.primary_key = [], json.subinfo = [], typeof json.result != "object") {
                    Vars.xhr = null,
                                notFoundDataBase();
                    return
                }
                if (json.result.length == 0) { notFoundDataBase(); return; }
                $(Elem.navi_info).hide();
                $(Elem.combo_input).attr("result", json.result.length)
                var info = json.info;
                for (json.cnt_page = json.result.length, i = 0; i < json.cnt_page; i++) {
                    json.subinfo[i] = [];
                    if (json.result[i]['fullname'] == word) {
                        $(Elem.hidden).val(json.result[i][Opt.primary_key]);
                    }
                    for (key in json.result[i])
                        if (key == Opt.primary_key && json.primary_key.push(json.result[i][key]), key == Opt.field)
                            json.candidate.push(json.result[i][key]);
                        else if ($.inArray(key, Opt.hide_field) == -1)
                            if (Opt.show_field != "" && $.inArray("*", Opt.show_field) == -1 && $.inArray(key, Opt.show_field) == -1)
                                continue;
                            else
                                json.subinfo[i][key] = json.result[i][key]
                        }
                        delete json.result,
                            Vars.xhr = null,
                            prepareResults(json, q_word, which_page_num);
                        if (info && info.length > 0)
                            $(Elem.sub_info).text(info).show();
                        else
                            $(Elem.sub_info).text('').hide();
                    })
                }
                function searchForJSON(q_word, which_page_num) {
                    function compareASC(a, b) {
                        return a[Opt.order_by[0][0]].localeCompare(b[Opt.order_by[0][0]])
                    }
                    function compareDESC(a, b) {
                        return b[Opt.order_by[0][0]].localeCompare(a[Opt.order_by[0][0]])
                    }
                    var matched = [],
                esc_q = [],
                sorted = [],
                json = {},
                i = 0,
                arr_reg = [],
                flag,
                j,
                start,
                end,
                sub,
                key;
                    do
                        esc_q[i] = q_word[i].replace(/\W/g, "\\$&").toString(), arr_reg[i] = new RegExp(esc_q[i], "gi"), i++;
                    while (i < q_word.length);
                    for (i = 0; i < Opt.source.length; i++) {
                        for (flag = !1, j = 0; j < arr_reg.length; j++)
                            if (Opt.source[i][Opt.field].match(arr_reg[j])) {
                                if (flag = !0, Opt.and_or == "OR")
                                    break
                            } else if (flag = !1, Opt.and_or == "AND")
                                break;
                        flag && matched.push(Opt.source[i])
                    }
                    if (matched.length == undefined) {
                        notFoundDataBase();
                        return
                    }
                    json.cnt_whole = matched.length;
                    var reg1 = new RegExp("^" + esc_q[0] + "$", "gi"),
                reg2 = new RegExp("^" + esc_q[0], "gi"),
                matched1 = [],
                matched2 = [],
                matched3 = [];
                    for (i = 0; i < matched.length; i++)
                        matched[i][Opt.order_by[0][0]].match(reg1) ? matched1.push(matched[i]) : matched[i][Opt.order_by[0][0]].match(reg2) ? matched2.push(matched[i]) : matched3.push(matched[i]);
                    for (Opt.order_by[0][1].match(/^asc$/i) ? (matched1.sort(compareASC), matched2.sort(compareASC), matched3.sort(compareASC)) : (matched1.sort(compareDESC), matched2.sort(compareDESC), matched3.sort(compareDESC)), sorted = sorted.concat(matched1).concat(matched2).concat(matched3), start = (which_page_num - 1) * Opt.per_page, end = start + Opt.per_page, i = start, sub = 0; i < end; i++, sub++) {
                        if (sorted[i] == undefined)
                            break;
                        for (key in sorted[i])
                            if (key == Opt.primary_key && (json.primary_key == undefined && (json.primary_key = []), json.primary_key.push(sorted[i][key])), key == Opt.field)
                                json.candidate == undefined && (json.candidate = []), json.candidate.push(sorted[i][key]);
                            else if ($.inArray(key, Opt.hide_field) == -1) {
                                if (Opt.show_field != "" && $.inArray("*", Opt.show_field) == -1 && $.inArray(key, Opt.show_field) == -1)
                                    continue;
                                json.subinfo == undefined && (json.subinfo = []),
                            json.subinfo[sub] == undefined && (json.subinfo[sub] = []),
                            json.subinfo[sub][key] = sorted[i][key]
                            }
                    }
                    json.cnt_whole > 0 && (json.cnt_page = json.candidate.length, prepareResults(json, q_word, which_page_num))
                }
                function notFoundDataBase() {
                    $(Elem.navi_info).text(Msg.not_found),
                $(Elem.navi_p).hide(),
                $(Elem.results).empty(),
                $(Elem.sub_info).empty(),
                calcWidthResults(),
                cssFocusInput(), $(Elem.combo_input).attr("result", 0)
                }
                function prepareResults(json, q_word, which_page_num) {
                    var idx, limit, obj;
                    if (Opt.allow_page)
                        setNavi(json.cnt_whole, json.cnt_page, which_page_num);
                    json.subinfo && Opt.sub_info || (json.subinfo = !1),
                json.primary_key || (json.primary_key = !1),
                Opt.select_only && json.candidate.length === 1 && json.candidate[0] == q_word[0] && (Opt.plugin_type != "textarea" && $(Elem.hidden).val(json.primary_key[0]), btnAttrDefault()),
                displayResults(json.candidate, json.subinfo, json.primary_key),
                Vars.is_paging === !1 ? cssFocusInput() : (idx = Vars.is_paging, limit = $(Elem.results).children("li").length - 1, idx > limit && (idx = limit), obj = $(Elem.results).children("li").eq(idx), $(obj).addClass(Cls.select), setSubInfo(obj), Vars.is_paging = !1, cssFocusResults())
                }
                function setNavi(cnt_whole, cnt_page, page_num) {
                    var num_page_top = Opt.per_page * (page_num - 1) + 1,
                num_page_end = num_page_top + cnt_page - 1,
                cnt_result = Msg.page_info.replace("cnt_whole", cnt_whole).replace("num_page_top", num_page_top).replace("num_page_end", num_page_end),
                last_page,
                left,
                right,
                num_link;
                    if ($(Elem.navi_info).text(cnt_result), last_page = Math.ceil(cnt_whole / Opt.per_page), last_page > 1) {
                        for ($(Elem.navi_p).empty(), Vars.is_suggest ? Vars.max_suggest = last_page : Vars.max_all = last_page, left = page_num - Math.ceil((Opt.navi_num - 1) / 2), right = page_num + Math.floor((Opt.navi_num - 1) / 2); left < 1; )
                            left++, right++;
                        while (right > last_page)
                            right--;
                        while (right - left < Opt.navi_num - 1 && left > 1)
                            left--;
                        for (page_num == 1 ? (Opt.navi_simple || $("<span><\/span>").text("<< 1").addClass("page_end").appendTo(Elem.navi_p), $("<span><\/span>").text(Msg.prev).addClass("page_end").appendTo(Elem.navi_p)) : (Opt.navi_simple || $("<a><\/a>").attr({
                            href: "javascript:void(0)",
                            "class": "navi_first"
                        }).text("<< 1").attr("title", Msg.first_title).appendTo(Elem.navi_p), $("<a><\/a>").attr({
                            href: "javascript:void(0)",
                            "class": "navi_prev"
                        }).text(Msg.prev).attr("title", Msg.prev_title).appendTo(Elem.navi_p)), i = left; i <= right; i++)
                            num_link = i == page_num ? '<span class="current">' + i + "<\/span>" : i, $("<a><\/a>").attr({
                                href: "javascript:void(0)",
                                "class": "navi_page"
                            }).html(num_link).appendTo(Elem.navi_p);
                        page_num == last_page ? ($("<span><\/span>").text(Msg.next).addClass("page_end").appendTo(Elem.navi_p), Opt.navi_simple || $("<span><\/span>").text(last_page + " >>").addClass("page_end").appendTo(Elem.navi_p)) : ($("<a><\/a>").attr({
                            href: "javascript:void(0)",
                            "class": "navi_next"
                        }).text(Msg.next).attr("title", Msg.next_title).appendTo(Elem.navi_p), Opt.navi_simple || $("<a><\/a>").attr({
                            href: "javascript:void(0)",
                            "class": "navi_last"
                        }).text(last_page + " >>").attr("title", Msg.last_title).appendTo(Elem.navi_p)),
                    $(Elem.navi_p).show(),
                    eHandlerForNaviPaging()
                    } else
                        $(Elem.navi_p).hide()
                }
                function setSubInfo(obj) {
                    var idx,
                t_top,
                t_left;
                    Opt.sub_info && (Vars.size_results = ($(Elem.results).outerHeight() - $(Elem.results).height()) / 2, Vars.size_navi = $(Elem.navi).outerHeight(), Vars.size_li = $(Elem.results).children("li:first").outerHeight(), Vars.size_left = $(Elem.results).outerWidth(), idx = $(Elem.results).children("li").index(obj), $(Elem.sub_info).children("dl").hide(), t_top = 0, $(Elem.navi).css("display") != "none" && (t_top += Vars.size_navi), t_top += Vars.size_results + Vars.size_li * idx, t_left = Vars.size_left, t_top += "px", t_left += "px", $(Elem.sub_info).children("dl").eq(idx).css({
                        position: "absolute",
                        top: t_top,
                        left: t_left,
                        display: "block"
                    }))
                }
                function displayResults(arr_candidate, arr_subinfo, arr_primary_key) {
                    var i,
                list,
                str_subinfo,
                $dl,
                json_key,
                json_val,
                dt,
                dd;
                    for ($(Elem.results).empty(), $(Elem.sub_info).empty(), i = 0; i < arr_candidate.length; i++)
                        if (list = $("<li>").html(arr_candidate[i]).attr({
                            pkey: arr_primary_key[i],
                            title: $(arr_candidate[i]).text()
                        }), Opt.plugin_type != "textarea" && arr_primary_key[i] == $(Elem.hidden).val() && $(list).addClass(Cls.selected), $(Elem.results).append(list), arr_subinfo) {
                            str_subinfo = [],
                        $dl = $("<dl>");
                            for (key in arr_subinfo[i]) {
                                json_key = key.replace("'", "\\'"),
                            arr_subinfo[i][key] == null ? arr_subinfo[i][key] = "" : arr_subinfo[i][key] += "",
                            json_val = arr_subinfo[i][key].replace("'", "\\'"),
                            str_subinfo.push("'" + json_key + "':'" + json_val + "'"),
                            dt = Opt.sub_as[key] != null ? Opt.sub_as[key] : key,
                            dt = $("<dt>").text(dt);
                                //!!! against XSS !!!
                                Opt.sub_info == "simple" && $(dt).addClass("hide"),
                            $dl.append(dt),
                            dd = $("<dd>").text(arr_subinfo[i][key]);
                                //!!! against XSS !!!
                                $dl.append(dd)
                            }
                            str_subinfo = "{" + str_subinfo.join(",") + "}",
                        $(list).attr("sub_info", str_subinfo),
                        $(Elem.sub_info).append($dl),
                        Opt.sub_info == "simple" && $dl.children("dd").text() == "" && $dl.addClass("ac_dl_empty")
                        }
                    calcWidthResults(),
                eHandlerForResults(),
                Opt.plugin_type == "combobox" && $(Elem.button).attr("title", Msg.close_btn)
                }
                function calcWidthResults() {
                    var w, offset;
                    w = Opt.plugin_type == "combobox" ? $(Elem.combo_input).width() + $(Elem.button).width() : $(Elem.combo_input).outerWidth() + 20,
                $(Elem.container).css("position") == "static" ? (offset = $(Elem.combo_input).offset(), $(Elem.result_area).css({
                    top: offset.top + $(Elem.combo_input).outerHeight() + "px",
                    left: offset.left + Opt.input_padding_left + "px"
                })) : $(Elem.result_area).css({
                    top: $(Elem.combo_input).outerHeight() + "px",
                    left: "0px"
                }),
                $(Elem.result_area).width(w).show()
                }
                function hideResults() {
                    Vars.key_paging && (scrollWindow(!0), Vars.key_paging = !1),
                cssFocusInput(),
                $(Elem.results).empty(),
                $(Elem.sub_info).empty(),
                $(Elem.result_area).hide(),
                abortAjax(),
                btnAttrDefault(),
                $(Elem.combo_input).attr("result", 0)
                }
                function firstPage() {
                    Vars.is_suggest ? Vars.page_suggest > 1 && (Vars.page_suggest = 1, Vars.is_paging = !0, suggest()) : Vars.page_all > 1 && (Vars.page_all = 1, Vars.is_paging = !0, suggest())
                }
                function prevPage() {
                    Vars.is_suggest ? Vars.page_suggest > 1 && (Vars.page_suggest--, Vars.is_paging = !0, suggest()) : Vars.page_all > 1 && (Vars.page_all--, Vars.is_paging = !0, suggest())
                }
                function nextPage() {
                    Vars.is_suggest ? Vars.page_suggest < Vars.max_suggest && (Vars.page_suggest++, Vars.is_paging = !0, suggest()) : Vars.page_all < Vars.max_all && (Vars.page_all++, Vars.is_paging = !0, suggest())
                }
                function lastPage() {
                    Vars.is_suggest ? Vars.page_suggest < Vars.max_suggest && (Vars.page_suggest = Vars.max_suggest, Vars.is_paging = !0, suggest()) : Vars.page_all < Vars.max_all && (Vars.page_all = Vars.max_all, Vars.is_paging = !0, suggest())
                }
                function selectCurrentLine(is_enter_key) {
                    var current,
                l_len,
                p_len,
                pos,
                skip;
                    if (scrollWindow(!0), current = getCurrentLine(), current) {
                        if (Opt.plugin_type != "textarea")
                            $(Elem.combo_input).val($(current).text()), Opt.sub_info && $(Elem.combo_input).attr("sub_info", $(current).attr("sub_info")), Opt.select_only && btnAttrDefault(), $(Elem.hidden).val($(current).attr("pkey"));
                        else {
                            var left = Vars.prev_value.substring(0, Vars.tag.pos_left),
                        right = Vars.prev_value.substring(Vars.tag.pos_right),
                        ctext = $(current).text();
                            Opt.tags[Vars.tag.type].space[0] && !left.match(Opt.tags[Vars.tag.type].pattern.space_left) && (p_len = Opt.tags[Vars.tag.type].pattern.left.length, l_len = left.length, left = left.substring(0, l_len - p_len) + " " + left.substring(l_len - p_len)),
                        right.match(Opt.tags[Vars.tag.type].pattern.comp_right) || (right = Opt.tags[Vars.tag.type].pattern.right + right),
                        Opt.tags[Vars.tag.type].space[1] && !right.match(Opt.tags[Vars.tag.type].pattern.space_right) && (p_len = Opt.tags[Vars.tag.type].pattern.right.length, right = right.substring(0, p_len) + " " + right.substring(p_len)),
                        $(Elem.combo_input).val(left + "" + ctext + "" + right),
                        pos = left.length + ctext.length,
                        skip = left + "" + ctext,
                        skip = skip.match(/\n/g),
                        skip = skip ? skip.length : 0,
                        pos += skip,
                        setCaretPos(pos)
                        }
                        Vars.prev_value = $(Elem.combo_input).val(),
                    hideResults()
                    }
                    Opt.bind_to && $(Elem.combo_input).trigger(Opt.bind_to, is_enter_key),
                $(Elem.combo_input).focus(),
                $(Elem.combo_input).change(),
                cssFocusInput()
                }
                function getCurrentLine() {
                    if ($(Elem.result_area).is(":hidden"))
                        return !1;
                    var obj = $(Elem.results).children("li." + Cls.select);
                    return $(obj).length ? obj : !1
                }
                function nextLine() {
                    var obj = getCurrentLine(),
                idx,
                next;
                    obj ? (idx = $(Elem.results).children("li").index(obj), $(obj).removeClass(Cls.select)) : idx = -1,
                idx++,
                idx < $(Elem.results).children("li").length ? (next = $(Elem.results).children("li").eq(idx), setSubInfo(next), $(next).addClass(Cls.select), cssFocusResults()) : cssFocusInput(),
                scrollWindow()
                }
                function prevLine() {
                    var obj = getCurrentLine(),
                idx,
                prev;
                    obj ? (idx = $(Elem.results).children("li").index(obj), $(obj).removeClass(Cls.select)) : idx = $(Elem.results).children("li").length,
                idx--,
                idx > -1 ? (prev = $(Elem.results).children("li").eq(idx), setSubInfo(prev), $(prev).addClass(Cls.select), cssFocusResults()) : cssFocusInput(),
                scrollWindow()
                }
                var Opt = initOptions(_source, _options),
            Msg = initMessages(),
            Cls = initCssClassName(),
            Vars = initLocalVars(),
            Elem = initElements(_jqobj);
                return $.ajaxSetup({
                    cache: !1
                }),
            btnAttrDefault(),
            btnPositionAdjust(),
            setInitRecord(),
            eHandlerForButton(),
            eHandlerForInput(),
            eHandlerForWhole(),
            eHandlerForTextArea(),
            Opt.shorten_url && findShort(),
            Vars
            }
            $.fn.ajaxComboBox = function (_source, _options) {
                var vars, p = this;
                p.setPreValue = function (preVal) {
                    if (vars) {
                        vars.prev_value = preVal;
                    }
                }
                this.each(function () {
                    vars = individual(this, _source, _options);
                })
                return p;
            }
        })(jQuery)