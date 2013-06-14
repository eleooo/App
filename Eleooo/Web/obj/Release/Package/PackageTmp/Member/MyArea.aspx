<%@ Page Title="修改我的商圈" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.master"
    AutoEventWireup="true" CodeBehind="MyArea.aspx.cs" Inherits="Eleooo.Web.Member.MyArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery-selector.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="mainBase">
        <div class="left">
            <ul class="work_city">
                <%--                <li id="boxName" runat="server"><span>我的姓名：</span>
                    <div>
                        <asp:TextBox class="cz_txt" ID="txtMemberName" size="20" MaxLength="10" runat="server" />
                    </div>
                </li>
                <li id="boxEmail" runat="server"><span>我的邮箱：</span>
                    <div>
                        <asp:TextBox class="cz_txt" ID="txtMemberEmail" size="20" MaxLength="30" runat="server" />
                    </div>
                </li>
                <li id="boxSex" runat="server"><span>我的性别：</span>
                    <div>
                        <asp:RadioButtonList ID="rbSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                            CssClass="radio">
                            <asp:ListItem Value="true" Text="男" Selected="True" />
                            <asp:ListItem Value="false" Text="女" />
                        </asp:RadioButtonList>
                    </div>
                </li>--%>
                <li><span>工作圈：</span>
                    <div>
                        <select class="select2" id="workAreaS1" name="workAreaS1">
                        </select>
                        <select class="select2" id="workAreaS2" name="workAreaS2">
                        </select>
                    </div>
                </li>
                <li><span>生活圈：</span>
                    <div>
                        <select class="select2" id="liveAreaS1" name="liveAreaS1">
                        </select>
                        <select class="select2" id="liveAreaS2" name="liveAreaS2">
                        </select>
                    </div>
                </li>
                <li><span>腐败圈：</span>
                    <div>
                        <select class="select2" id="fbAreaS1">
                        </select>
                        <select class="select2" id="fbAreaS2">
                        </select>
                        <input class="add_btn" value="+添加" type="button" id="btnAdd" />
                        <input type="hidden" id="fbArea" name="fbArea" />
                    </div>
                </li>
                <li class="add_em">
                    <div id="slContainer" style="width: 100%;">
                    </div>
                </li>
                <li>
                    <input type="button" class="bc_btn" value="确 认" onclick="__doPostBack('Edit');" />
                    <label runat="server" id="txtMessage" style="color: Red;">对圈子的设定3个月内只能修改一次哦</label>
                </li>
            </ul>
        </div>
        <div class="right">
            <h3>
                为什么要设置圈子？</h3>
            <p>
                选择您的圈子，锁定您身边的商家和优惠，让惊喜触手可及。</p>
            <p>
                对圈子的设定三个月内只能修改一次哦！</p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottomScript"
    runat="server">
    <script type="text/javascript">
        var MultiSelector = function (opts) {
            var _opts = {
                containerId: '', btnId: '', defItems: [], postValueId: '', selectorId: '', maxItemAllow: 3
            };
            var _values = {};
            function _init() {
                _opts = $.extend(_opts, opts);
                var item;
                for (i = 0; i < _opts.defItems.length; i++) {
                    item = _opts.defItems[i];
                    appendItem(item.id, item.name, false);
                }
                $("#" + _opts.btnId).click(onButtonClick);
            }
            function onButtonClick() {
                var el = $("#" + _opts.selectorId + " option:selected");
                var id = el.val();
                if (!id || !(id > 0)) {
                    alert("请选择一个圈子.");
                    return;
                }
                var text = el.text();
                appendItem(id, text, true);
            }
            function appendItem(id, text, isLimit) {
                if (isLimit && getValuesLength() >= _opts.maxItemAllow) {
                    alert("你最多只允许选择" + _opts.maxItemAllow + "个圈子");
                    return;
                }
                if (_values[id.toString()]) {
                    alert("你已经选择了此商圈.");
                    return;
                }
                appendItemCore(id, text);
                _values[id.toString()] = text;
                refreshPostVal();
            }
            function appendItemCore(id, text) {
                $("<em>" + text + "</em>").appendTo("#" + _opts.containerId)
                                          .css("width",text.length * 2 + 40)
                                          .prepend($("<a>×</a>").attr("href", "javascript:void(0)")
                                                                .attr("val", id)
                                                                .click(function () { removeItemCore(this) }));
            }
            function removeItemCore(el) {
                var val = $(el).attr("val");
                $(el).parent().remove();
                delete _values[val.toString()];
                refreshPostVal();
            }
            function refreshPostVal() {
                var vals = [];
                for (var v in _values)
                    vals.push(v);
                $("#" + _opts.postValueId).val(vals.join(','));
            }
            function getValuesLength() {
                var i = 0;
                for (var v in _values)
                    i++;
                return i;
            }
            _init();
        };
        $(document).ready(function () {
            //工作圈
            $.fn.regMulitSelector({ sid: "workAreaS1,workAreaS2", pid: 1, dv: '<%=GetAreaDepthSelectedValue(CurrentUser.AreaDepth2) %>' });
            //生活圈
            $.fn.regMulitSelector({ sid: "liveAreaS1,liveAreaS2", pid: 1, dv: '<%=GetAreaDepthSelectedValue(CurrentUser.AreaDepth1) %>' });
            //腐败圈
            $.fn.regMulitSelector({ sid: "fbAreaS1,fbAreaS2", pid: 1, dv: '<%=GetAreaDepthSelectedValue(CurrentUser.AreaDepth3) %>' });
            new MultiSelector({ containerId: 'slContainer', btnId: 'btnAdd', defItems: <%=GetMultiDepthsNameIds(CurrentUser.AreaDepth3) %>, postValueId: 'fbArea', selectorId: 'fbAreaS2' });
        });
    </script>
</asp:Content>
