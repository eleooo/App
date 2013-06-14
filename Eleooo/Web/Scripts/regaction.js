String.format = function (sourceStr, params, index) {
    if (!params)
        return sourceStr;
    var indexPos = index ? '{' + index + '}' : '{0}';
    if (typeof (params) == 'string')
        return sourceStr.replace(indexPos, params);
    else {
        for (i = 0; i < params.length; i++) {
            sourceStr = String.format(sourceStr, params[i], i);
        }
        return sourceStr;
    }
}
function getParam(params) {
    if (typeof (params) == 'string')
        return params.split('|');
    else if (params)
        return params;
    else
        return '';
}
function dlgHandlerCallback() {
    __doPostBack();
}
function actionDlg(el) {
    if (!action_dlg_url)
        return;
    var dlg_title = '';
    var dlg_width = 900;
    var dlg_height = 600;
    var action_url = '';
    var index = el.attr('index') || 0;
    if ($.isArray(action_dlg_url) && index >= 0) {
        dlg_title = action_dlg_title[index];
        action_url = String.format(action_dlg_url[index], getParam(el.attr('param')));
        if (action_dlg_width) {
            dlg_width = $.isArray(action_dlg_width) ? action_dlg_width[index] : action_dlg_width;
        }
        if (action_dlg_height) {
            dlg_height = $.isArray(action_dlg_height) ? action_dlg_height[index] : action_dlg_height;
        }
    }
    else {
        dlg_title = action_dlg_title;
        action_url = String.format(action_dlg_url, getParam(el.attr('param')));
        if (action_dlg_width)
            dlg_width = action_dlg_width;
        if (action_dlg_height)
            dlg_height = action_dlg_height;
    }
    if (!dlg_title)
        dlg_title = '';
    tipsWindown(dlg_title, "iframe:" + action_url, dlg_width, dlg_height, "true", "", "true", "", dlgHandlerCallback);
}
function actionDel(el) {
    var params = el.attr('param');
    __doPostBack('Delete', params);
}
function actionEdit(el) {
    var params = el.attr('param');
    __doPostBack('Edit', params);
}
function onAction() {
    var el = this;
    if (el.className == 'action_dlg')
        actionDlg($(el));
    else if (el.className == 'action_del')
        actionDel($(el));
    else if (el.className == 'action_edit')
        actionEdit($(el));
}
$(document).ready(function () {
    var action_arr = $(".action_dlg,.action_edit,.action_del,.action_add,.action_query");
    $.each(action_arr, function (i, ctrl) {
        $(ctrl).click(onAction);
    });
});

