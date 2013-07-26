(function ($, document, undefined) {

    /*cookie start*/
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
    //cookie end

    //mp3 start
    jQuery.fn.jmp3 = function (passedOptions) {
        // hard-wired options
        var playerpath = "/Scripts/player/"; 				// SET THIS FIRST: path to singlemp3player.swf

        // passable options
        var options = {
            "url": "", 									// path to MP3 file (default: current directory)
            "width": "0", 									// width of player
            "height": "0",
            "repeat": "no", 									// repeat mp3?
            "volume": "70", 									// mp3 volume (0-100)
            "showfilename": "false",
            "backcolor": "000000",
            "forecolor": "00ff00",
            "autoplay": "true", 							// play immediately on page load?
            "showdownload": "false", 							// show download button in player
            "showfilename": "false"								// show .mp3 filename after player
        };

        // use passed options, if they exist
        if (passedOptions) {
            jQuery.extend(options, passedOptions);
        }

        // iterate through each object
        return this.each(function () {
            // filename needs to be enclosed in tag (e.g. <span class='mp3'>mysong.mp3</span>)
            //var filename = options.filepath + jQuery(this).html();
            // do nothing if not an .mp3 file
            //var validfilename = filename.indexOf(".mp3");
            //if (validfilename == -1) { return false; }
            // build the player HTML
            var el = $(this);
            var id = el.data('_jmp3');
            if (id) {
                el.find("#_" + id).remove();
            }
            id = Date.now();
            el.data('_jmp3', id)
            var mp3html = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="0" height="0" style="display:none" id="_' + id + '" ';
            //mp3html += 'width="' + options.width + '" height="20" ';
            mp3html += 'codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab">';
            mp3html += '<param name="movie" value="' + playerpath + 'singlemp3player.swf?';
            mp3html += 'showDownload=' + options.showdownload + '&file=' + filename + '&autoStart=' + options.autoplay;
            mp3html += '&backColor=' + options.backcolor + '&frontColor=' + options.forecolor;
            mp3html += '&repeatPlay=' + options.repeat + '&songVolume=' + options.volume + '" />';
            mp3html += '<param name="wmode" value="transparent" />';
            mp3html += '<embed wmode="transparent" width="' + options.width + '" height="0" ';
            mp3html += 'src="' + playerpath + 'singlemp3player.swf?'
            mp3html += 'showDownload=' + options.showdownload + '&file=' + filename + '&autoStart=' + options.autoplay;
            mp3html += '&backColor=' + options.backcolor + '&frontColor=' + options.forecolor;
            mp3html += '&repeatPlay=' + options.repeat + '&songVolume=' + options.volume + '" ';
            mp3html += 'type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />';
            mp3html += '</object>';
            // don't display filename if option is set
            //if (options.showfilename == "false") { jQuery(this).html(""); }
            el.append(mp3html + "&nbsp;");

            // Eolas workaround for IE (Thanks Kurt!)
            //if (jQuery.browser.msie) { this.outerHTML = this.outerHTML; }
        });
    };
    //mp3 end
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
function bindChange(target, fn) {
    var el = document.getElementById(target);
    if (el) {
        if ("\v" == "v") {
            el.onpropertychange = fn;
        } else {
            el.addEventListener("input", fn, false);
        }
    }
}
//play background sound
$(document).ready(function () {
    //<object type=application/x-shockwave-flash data=beep.swf width=0 height=0><param name=movie value=beep.swf /><param name=loop value=false></object>
});