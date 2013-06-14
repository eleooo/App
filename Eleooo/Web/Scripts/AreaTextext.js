var allAreaData = [];
var convertTagsToArray = function (tags) {
    if (!tags || tags == '')
        return [];
    return tags.split(",");
}
var getDepthById = function (id) {
    if (!id || id == '')
        return '';
    $.each(allAreaData, function (i, data) {
        if (data.id == id)
            return data.depth;
    });
    return '';
}
var getNameById = function (id) {
    if (!id || id == '')
        return '';
    $.each(allAreaData, function (i, data) {
        if (data.id == id)
            return data.name;
    });
    return '';
}
var getDepthByName = function (areaName) {
    if (!areaName || areaName == '')
        return '';
    $.each(allAreaData, function (i, data) {
        if (data.name == areaName)
            return data.depth;
    });
    return '';
}
var getNameByDepth = function (areaDepth) {
    if (!areaDepth || areaDepth == '')
        return '';
    $.each(allAreaData, function (i, data) {
        if (data.depth == areaDepth)
            return data.name;
    });
    return '';
}
var getSelectorValue = function (selectorId) {
    var ids = selectorId.split(',');
    var val = '';
    for (i = ids.length - 1; i >= 0; i--) {
        val = $("#" + ids[i]).attr("value");
        if (val > 0)
            break;
    }
    return val;
}
var getTextextVal = function (textext) {
    var arr = eval(textext.hiddenInput().val());
    return arr.join(",");
}
var initAreaTextext = function (textAreaId, btnAddId, selectorId, postTagsId, selectedTags) {
    $.getJSON('/Public/RestHandler.ashx/Area?pid=-1', function (data) {
        allAreaData = data.data;
    });
    $('#' + textAreaId)
        .textext({
            plugins: 'tags',
            items: allAreaData,
            tagsItems: convertTagsToArray(selectedTags),
            itemManager: {
                stringToItem: function (str) {
                    return getDepthByName(str);
                },

                itemToString: function (item) {
                    return getNameByDepth(item);
                },

                compareItems: function (item1, item2) {
                    return item1 == item2;
                }
            }
        });
    //$('#' + postTagsId).val(selectedTags);
    var btn = $('#' + btnAddId);
    btn.attri("textAreaId", textAreaId);
    btn.attri("selectorId", selectorId);
    btn.attri("postTagsId", postTagsId);
    btn.bind('click', function (e) {
        var sID = $(this).attri("selectorId");
        var txtAreaId = $(this).attri("textAreaId");
        var postTagId = $(this).attri("postTagsId");
        var val = getSelectorValue(sID);
        if (val > 0) {
            var ext = $('#' + txtAreaId).textext()[0];
            ext.tags().addTags([getDepthById(val)]);
            $('#' + postTagId).val(getTextextVal(ext));
        }
    });
}

