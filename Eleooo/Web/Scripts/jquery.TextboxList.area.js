/// <reference path="jquery-1.4.1-vsdoc.js" />
;(function ($) {
var AreaExt = function(opts){
    if(opts==null || opts == undefined)
        return $(this).data('areaext');
    var instance = new AreaExt();
    instance.init(opts,this);
    $(this).data('areaext',instance);
    return instance;
};
var p = AreaExt.prototype,
    POST_INPUT = "<input id='{0}' name='{0}' type='hidden' />",
    DEFAULT_OPTIONS = 
    {
        btnAddId:'',
        selectorId:'',
        postTagsId:'',
        areaLimit:false,
        selectedItems:[]
    },
    _opts,
    _textext;
var format = function (sourceStr, params, index) {
    if (params == null)
        return sourceStr;
    index = index?index:0;
    var indexPos = eval('/\\{'+index+'\\}/g');
    if (typeof (params) == 'string')
        return sourceStr.replace(indexPos, params);
    else if($.isArray(params)) {
        for (i = 0; i < params.length; i++) {
            sourceStr = format(sourceStr, params[i], i);
        }
        return sourceStr;
    }
};
var self = this;
p.init = function(opts,binder){
    self = this;
    self._opts = $.extend(DEFAULT_OPTIONS,opts);
    self._textext = new $.TextboxList(binder, 
                    {
                        endEditableBit: false,
                        startEditableBit: false,
                        hideEditableBits: false,
                        inBetweenEditableBits: false,
                        unique: true
                    });
    $(binder).before(format(POST_INPUT,[self._opts.postTagsId]));
    $('#' + self._opts.btnAddId).click(function (e) {
        var vals = self._textext.getValues();
        if(vals && vals.length >=5){
            alert("最多允许选择5个圈子");
            return;
        }
        var val = self.getSelectorItem(self._opts.selectorId);
        if(val)
            self._textext.add(val.name,val.id);
    });
    self._textext.addEvent('bitAdd',self.onChanged);
    self._textext.addEvent('bitRemove',self.onChanged);
    self.addTagsByDepth(self._opts.selectedItems);
};
p.onChanged = function(item){
    $('#'+self._opts.postTagsId).val(self.getTextextVal());
};

p.addTagsByDepth = function(items){    
    if(items == null || items.length == 0)
        return items;
    $.each(items,function(i,data){
          self._textext.add(data.name,data.id);
    });
};
p.getSelectorItem = function (selectorId) {
    if(selectorId == null || selectorId == '')
        return;
    var ids = selectorId.split(',');
    var val,text;
    var item ;
    if(self._opts.areaLimit){
        val = $("#"+ids[ids.length - 1]).attr("value");
        if (val > 0){
            text = $("#" + ids[ids.length - 1]).find("option:selected").text();
            item = {id:val,name:text};
        }
        else
            alert("请选择圈子");
        return item;
     }
    for (i = ids.length - 1; i >= 0; i--) {
        val = $("#" + ids[i]).attr("value");
        if (val > 0){
            text = $("#" + ids[i]).find("option:selected").text();
            item = {id:val,name:text};
            break;
        }
    }
    return item;
};
p.getTextextVal = function () {
    var arr = self._textext.getValues();
    if(!arr || arr.lenght == 0)
        return '';
    var items = [];
    $.each(arr,function(i,data){
        items.push(data[0]);
    });
    return items.join(",");
};
$.fn.areaext = AreaExt;
})(jQuery);