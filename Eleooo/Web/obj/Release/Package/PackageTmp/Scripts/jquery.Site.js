(function ($, document, undefined) {

    var pluses = /\+/g;

    function raw(s) {
        return s;
    }

    function decoded(s) {
        return decodeURIComponent(s.replace(pluses, ' '));
    }

    var config = $.cookie = function (key, value, options) {

        // write
        if (value !== undefined) {
            options = $.extend({}, config.defaults, options);

            if (value === null) {
                options.expires = -1;
            }

            if (typeof options.expires === 'number') {
                var days = options.expires, t = options.expires = new Date();
                t.setDate(t.getDate() + days);
            }

            value = config.json ? escape(JSON.stringify(value)) : escape(value);

            return (document.cookie = [
                    escape(key), '=', value,
                    options.expires ? '; expires=' + options.expires.toUTCString() : '', // use expires attribute, max-age is not supported by IE
                    options.path ? '; path=' + options.path : '',
                    options.domain ? '; domain=' + options.domain : '',
                    options.secure ? '; secure' : ''
                    ].join(''));
        }

        // read
        //        var decode = config.raw ? raw : decoded;
        //        var cookies = document.cookie.split('; ');
        //        for (var i = 0, l = cookies.length; i < l; i++) {
        //            var parts = cookies[i].split('=');
        //            if (decode(parts.shift()) === key) {
        //                var cookie = decode(parts.join('='));
        //                return config.json ? JSON.parse(cookie) : cookie;
        //            }
        //        }

        var cookie_start = document.cookie.indexOf(escape(key));
        var cookie_end = document.cookie.indexOf(";", cookie_start);
        if (cookie_start == -1) {
            return null;
        } else {
            var v = document.cookie.substring(cookie_start + name.length + 1, (cookie_end > cookie_start ? cookie_end : document.cookie.length));
            return unescape(v);
        }
    };

    config.defaults = {};

    $.removeCookie = function (key, options) {
        if ($.cookie(key) !== null) {
            $.cookie(key, null, options);
            return true;
        }
        return false;
    };
})(jQuery, document);

function GetParam(url, str) {
    if (str === undefined) {
        str = url;
        url = document.location.href;
    }
    var r = "";
    if (url == null) return r;
    if (str == null) return r;

    var i = url.indexOf('?'); if (i < 0) return r; url = url.substring(i);
    i = url.indexOf(str); if (i < 0) return r; url = url.substring(i + str.length);
    i = url.indexOf('&');

    if (i > -1)
        return url.substring(1, i);
    else
        return url.substring(1);
}

function Request(str) {
    var url = location.href;
    var r = "";

    if (url == null) return r;
    if (str == null) return r;

    var i = url.indexOf('?'); if (i < 0) return r; else url = url.substring(i);
    i = url.indexOf(str + "="); if (i < 0) return r; else url = url.substring(i + str.length + 1);

    i = url.indexOf('&');
    if (i > -1)
        return url.substring(0, i);
    else
        return url;
}

 