<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcOrderMealSelectMansion.ascx.cs"
    Inherits="Eleooo.Web.Controls.UcOrderMealSelectMansion" %>
<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Src="~/Controls/UcOrderMealFlow.ascx" TagPrefix="uc" TagName="UcOrderMealFlow" %>
<%@ Register Namespace="Eleooo.Web.Controls" Assembly="Eleooo.Web" TagPrefix="ele" %>
<ele:ResLink ID="rs1" Src="/Scripts/acbox/jquery.ajaxComboBox.6.1.js" runat="server" />
<ele:ResLink ID="ResLink1" Src="/Scripts/acbox/Style.css" runat="server" />
<style type="text/css">
    .xrk2 li a:hover
    {
        text-decoration: underline;
    }
</style>
<uc:UcOrderMealFlow ID="orderMealFlow" runat="server" />
<div class="xrk">
    <div class="xrk1">
        <span style="float: left; margin-right: 10px;">
            <img src="/App_Themes/ThemesV2/images/wyz.png" alt="" /></span>
        <input type="text" value="请输入您所在的大厦或小区，如：赛格广场" defval="请输入您所在的大厦或小区，如：赛格广场" id="txtInputVal"
            name="txtInputVal" />
        <img src="/App_Themes/ThemesV2/images/xrk-san.png" style="float: left;" alt="" />
        <span style="float: right;"><a href="javascript:void(0)" onclick="onSubmitMansion();">
            <img width="82" height="60" src="/App_Themes/ThemesV2/images/jwm.png" alt="" /></a></span>
        <div class="clear">
        </div>
    </div>
    <div class="xrk2" id="noLoginContainer" runat="server">
        <ul>
            <li>请选定您的位置，我们会找出能够为您提供外卖服务的餐厅</li>
            <li>已经使用过订餐服务的会员请直接<a href="/Public/Login.aspx?ReturnUrl=%2fPublic%2fOrderMealPage.aspx">登录</a></li>
        </ul>
    </div>
    <div class="xrk3" id="myFavAddrContainer" runat="server">
        <ele:DataListExt ID="rpMyFavAddr" runat="server" EnableViewState="false" AllowPaging="false">
            <HeaderTemplate>
                <ul>
                    <li class="xrk2" id="addrCount3" style="display: none">请选定您的送餐地址<br>
                        您总共可以保存三个常用送餐地址，如需增加请进行应的删减</li>
                    <li class="xrk2" id="addrCount2" style="display: none">请选定您的送餐地址<br>
                        如果您更换了送餐地址，请重新搜索附近的餐厅</li>
                    <li class="xrk2" id="addrCount0" style="display: none">请选定您的位置，我们会找出能够为您提供外卖服务的餐厅<br>
                        您总共可以保存三个常用送餐地址</li>
            </HeaderTemplate>
            <ItemTemplate>
                <li><span class="dc1">
                    <input type="radio" value='<%# Eval("id") %>' name="myFavMansionId" addr='<%# Eval("name") %>'
                        mansion="<%# Eval("mansion") %>" /></span> <span class="dc01">
                            <input type="text" name="" class="input_w" readonly="readonly" value="<%# Eval("mansion") %><%#  Utilities.ConcatAddres(Eval("name")) %>" /></span>
                    <a href="javascript:void(0);" onclick="onMyFavMansionDel(<%#Eval("id")%>,'<%# Eval("name") %>');">
                        <img border="0" src="/App_Themes/ThemesV2/images/dc01-jh.png" alt="" /></a>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </ele:DataListExt>
    </div>
</div>
<div class="lctu">
    <div class="lctu01">
        <div class="lctu01-left">
            <img src="/App_Themes/ThemesV2/images/lctu01.png" alt="" /></div>
        <div class="lctu01-right">
            <span>精确</span>
            <p>
                根据您的位置，</p>
            <p>
                查找出的餐厅，</p>
            <p>
                100%能够为您送餐。</p>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="lctu02">
        <div class="lctu01-left">
            <img src="/App_Themes/ThemesV2/images/lctu02.png" alt="" /></div>
        <div class="lctu01-right">
            <span>快捷</span>
            <p>
                信息高效匹配，</p>
            <p>
                送餐快人一步，</p>
            <p>
                再也不用望眼欲穿了。</p>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="lctu03">
        <div class="lctu01-left">
            <img src="/App_Themes/ThemesV2/images/lctu03.png" alt="" /></div>
        <div class="lctu01-right">
            <span>省心</span>
            <p>
                查看送餐进度，</p>
            <p>
                用鼠标催餐，</p>
            <p>
                就是这么简单。</p>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="clear">
    </div>
</div>
<script type="text/javascript" language="javascript">
    var inputBox;
    var onMyFavMansionDel = function (mansionId, addr) {
        __doPostBack("Delete", mansionId + ";#" + addr);
    }
    var onSubmitMansion = function () {
        var val = $('#txtInputVal_primary_key').val();
        if (!val) {
            var checkedItem = $('input:radio[name="myFavMansionId"]:checked');
            val = checkedItem.val();
            $.cookie('addr', checkedItem.attr("addr"));
        } else {
            $.cookie('addr', null);
        }
        if (!val && $("#txtInputVal").attr("result") > 0) {
            alert("请选定您的位置^_^");
            return false;
        }
        else if (!val && $('#txtInputVal').val() && $('#txtInputVal').val() !== $('#txtInputVal').attr('defVal')) {
            alert("没有找到这个位置，换个名称试试吧^_^");
            return;
        } else if (!val) {
            alert("请选定您的位置^_^");
            return false;
        }
        window.location.href = "/Public/OrderMealPage.aspx?MansionId=" + val;
        return false;
    }
    $(document).ready(function () {
        var is_enter_key = false;
        var cb = $('input:radio[name="myFavMansionId"]');
        var len = cb.length;
        if (len > 0) {
            if (len <= 2)
                $("#addrCount2").show();
            else
                $("#addrCount3").show();
            cb.click(function () {
                var val = $(this).attr("mansion") + $(this).attr("addr").replace('||','').replace('|','楼');
                $("#txtInputVal_primary_key").val('');
                $("#txtInputVal").val(val);
                inputBox.setPreValue(val);
            });
        } else
            $("#addrCount0").show();
        $.cookie('addr', null);
        inputBox = $('#txtInputVal').bind("AutoSubmit", function (event, is_enter) {
            is_enter_key = is_enter !== true;
        }).keydown(function () {
            if (arguments[0].keyCode == 13) {
                if (is_enter_key)
                    onSubmitMansion();
                else
                    is_enter_key = true;
            }
        }).ajaxComboBox(
		        '/Public/RestHandler.ashx/Mansion',
		        {
		            lang: 'cn',
		            empty_message: '请输入您所在的大厦或小区，如：赛格广场',
		            button_img: '/App_Themes/ThemesV2/images/xrk-san.png',
		            input_padding_left: 20,
		            plugin_type: 'simple',
		            bind_to: "AutoSubmit"
		        });

    }); 
</script>
