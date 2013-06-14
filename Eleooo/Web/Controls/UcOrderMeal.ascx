<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcOrderMeal.ascx.cs" Inherits="Eleooo.Web.Controls.UcOrderMeal" %><%@ Register Assembly="Eleooo.Web"
    Namespace="Eleooo.Web.Controls" TagPrefix="ele" %><%@ Register TagPrefix="ele" TagName="UcFaceBookCompany" Src="~/Controls/UcFaceBookCompany.ascx" %><ele:ResLink
        ID="rs1" runat="server" Src="/Scripts/SendMsn.js" />
<style type="text/css">
    .backToTop { display: none; width: 60px; height: 22px; background: url('/App_Themes/ThemesV2/images/top.png') no-repeat left top; line-height: 1.2; padding: 5px 0; color: #fff; font-size: 12px; text-align: center; position: fixed; _position: absolute; right: 520px; bottom: 270px; _bottom: "auto"; cursor: pointer; }
</style>
<div class="kk" style="overflow: hidden; zoom: 1">
    <div class="left" id="leftBox">
        <div class="dc-title">
            <div class="dc-title-tu">
                <img src='<%= this.Company.CompanyPhoto %>' alt="" width="138px" height="91px" /></div>
            <div class="dc-title-w">
                <span>
                    <%= FormatText(this.Company.CompanyName,"<b>{0}</b>") %></span>
                <p>
                    地址：<%=this.Company.CompanyAddress %></p>
                <p>
                    人气：<u class="s_yellow"><%=this.GetOrderCount() %></u> &nbsp;&nbsp; 点评：<a class="s_dp" id="faceBookCount" href="javascript:void(0);"><%=this.GetFaceBookRateCount() %></a></p>
                <p>
                    积分：<%=OrderMealRewardRate%></p>
            </div>
            <div class="dc-title-aniu">
                <a href="javascript:void(0)" id="btnAddFav">
                    <img width="94" height="24" border="0" src="/App_Themes/ThemesV2/images/tjct.png" alt="" id="favCompanyPic" runat="server" /></a>
                <p>
                    菜单已于<%=this.GetMenuUpdateDate() %>月更新</p>
            </div>
        </div>
        <ele:UcFaceBookCompany ID="faceBook" runat="server" />
        <div class="cd-lb" id="menuBox">
            <asp:Repeater ID="rpMenuDir" runat="server" EnableViewState="false" OnItemDataBound="rpMenuDir_ItemDataBound">
                <HeaderTemplate>
                    <ul id="menuContainer">
                </HeaderTemplate>
                <FooterTemplate>
                    </ul></FooterTemplate>
                <ItemTemplate>
                    <li><a href="javascript:void(0)" onclick="orderMeal.changeMenuDir(<%#Eval("Key.Key") %>)">
                        <%# FormatMenuDirtext(Eval("Key.Value")) %></a>
                        <ul id="dir_<%#Eval("Key.Key") %>" <%# GetDirAttribute(Eval("Key.Key"),Container.ItemIndex) %>>
                            <asp:Repeater ID="rpMenu" runat="server" EnableViewState="false">
                                <ItemTemplate>
                                    <li id="li_menu_<%#Eval("MenuId") %>" onclick="orderMeal.addMenuItem('<%#Eval("MenuId") %>')"><b id="li_menu_b_<%#Eval("MenuId") %>" style="display: none"></b><a
                                        href="javascript:void(0)">
                                        <%# FormatMenutext(Eval("MenuName")) %></a> <span>
                                            <%#Eval("MenuPrice")%></span></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="right" id="slider">
        <div class="xd" id="orderBox">
            <div class="xd-title">
                <span>我的订单</span></div>
            <div class="xd-main">
                <div class="xd-nr1" id="orderInfoContainer">
                    <p>
                        选择您喜欢的餐点，我会帮您记下来：D</p>
                    <h3>
                        订快餐送积分，比钱更值钱哦...</h3>
                    <div style="color: rgb(30, 141, 168); overflow: hidden; padding-top: 19px; padding-bottom: 8px; zoom: 1;" id="companyContentContainer" runat="server">
                        <div style="width: 25px; float: left;">
                            <img src="/App_Themes/ThemesV2/images/lab.png" alt="" /></div>
                        <div style="width: 260px; float: right; line-height: 24px;">
                            <%=SubSonic.Sugar.Strings.StripHTML(HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(Company.CompanyContent))) %>
                        </div>
                    </div>
                </div>
                <div class="zfyx">
                    <div class="zfyx-title">
                        <span>嘱咐一下</span></div>
                    <div class="zfyx-main">
                        <textarea rows="2" cols="20" name="txtUserMemo" id="txtUserMemo" defaultval="如：不要辣、我没零钱、分机818"></textarea>
                    </div>
                </div>
                <div class="wdxx" id="myInfoContainer">
                    <div class="wdxx-title">
                        <span>我的信息</span></div>
                    <div class="wdxx-main">
                        <ul>
                            <li class="input05"><span>地址：</span>
                                <input type="text" name="txtMansionName" id="txtMansionName" style="width: 190px;" />
                            </li>
                            <li class="input07">
                                <input class="input_l" type="text" name="txtFloor" id="txtFloor" /><span style="padding: 0px 5px;">楼</span><input class="input_s" type="text" name="txtRoom" id="txtRoom"
                                    title="(如：XXX室)" value="(如：XXX室)" />
                            </li>
                            <li class="input04"><span>手机：</span>
                                <input type="text" onchange="orderMeal.onPhoneChange();" name="txtUserPhone" id="txtUserPhone" style="width: 95px;" value="" maxlength="11" />
                            </li>
                            <li class="input04" id="codeContainer"><span>验证：</span><input type="text" style="width: 95px;" value="" name="txtCode" id="txtCode" /><a class="dx_link" href="javascript:void(0)"
                                id="btnSendCode" text="重发验证码">获取验证码</a>
                                <!--<a href="#" class="dx_gray">60秒后可重发</a>-->
                            </li>
                        </ul>
                    </div>
                    <div class="wdxx-aniu" isshow="false">
                        <a href="javascript:void(0)" onclick="orderMeal.submitOrder();">
                            <img src="/App_Themes/ThemesV2/images/xd-aniu-cs.png" width="81" height="30" border="0" alt="" /></a></div>
                </div>
            </div>
        </div>
        <div class="dcxxt" id="dcxxt">
            <div class="dcxxt-title">
                <span>订餐小贴士</span></div>
            <div class="dcxxt-main">
                <ul>
                    <li><a href="#">我的外卖保证能送吗？</a>
                        <p style="display: none">
                            是的。乐多分根据您所在的大厦/小区，为 您推荐附近的餐厅，从而确保100%送达。</p>
                    </li>
                    <li><a href="#">多长时间能送到？</a>
                        <p style="display: none">
                            送餐时间可参考餐厅公布的平均速度，鉴于高峰时段饭市集中，以实际送达时间为准。</p>
                    </li>
                    <li><a href="#">乐多分负责送餐吗？</a>
                        <p style="display: none">
                            乐多分暂未开通送餐服务，您的外卖均由餐厅负责配送。</p>
                    </li>
                    <li><a href="#">我的外卖还没收到怎么办？</a>
                        <p style="display: none">
                            您可以点击催餐按钮，乐多分客服会在第一时间帮您联系餐厅，并将送餐进度实时反馈。
                        </p>
                    </li>
                    <li><a href="#">订快餐能送积分吗？</a>
                        <p style="display: none">
                            是的。您的每一笔外卖订单均可获得餐点金额3%的积分返馈。
                        </p>
                    </li>
                    <li><a href="#">用积分可以订快餐吗？</a>
                        <p style="display: none">
                            可以。餐厅会不时提供促销活动，您可以用积分抢购超值外卖。
                        </p>
                    </li>
                    <li><a href="#">怎么查看订单记录？</a>
                        <p style="display: none">
                            成功下单后，您可以在“我的订单”里查看订单情况。
                        </p>
                    </li>
                    <li><a href="#">如果遇到其他问题怎么办？</a>
                        <p style="display: none">
                            如遇特殊情况，您可以致电400-080-9095，乐多分客服会及时为您提供协助。
                        </p>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <div id="selectedItemtemplate" style="display: none">
        <ul class="order_list" id="orderContainer">
        </ul>
        <div class="sc">
            <ul>
                <li class="input04">
                    <input type="text" id="disServicesSum" style="width: 100px;" value="送餐费：元" readonly="readonly" />
                </li>
                <li class="input04">
                    <input type="text" style="width: 100px; float: left; margin-right: 8px;" value="总计：0元" id="disSum" />
                    <a href="javascript:void(0)" class="qk_link" onclick="orderMeal.delAllMenu();">
                        <img border="0" src="/App_Themes/ThemesV2/images/qk.png" alt="" /></a> </li>
                <li class="input05">
                    <input type="text" style="width: 200px;" value="" id="disPoint" readonly="readonly" />
                </li>
                <li>
                    <div style="color: rgb(30, 141, 168); overflow: hidden; zoom: 1;">
                        <div style="width: 25px; float: left;">
                            <img src="/App_Themes/ThemesV2/images/lab.png" alt="" /></div>
                        <div style="width: 260px; float: right;">
                            <span style="line-height: 24px;">受市场因素影响，价格可能会有微调。</span></div>
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <div id="menuItemTemplate" style="display: none">
        <li id="menu_{menuId}"><span class="menu_name">{menuName}</span><div class="menuRig">
            <a href="javascript:void(0)" onclick="orderMeal.downAmount('{menuId}');">
                <img border="0" align="absmiddle" src="/App_Themes/ThemesV2/images/jh.png" alt="" /></a>
            <input class="input06" value="50" type="text" readonly="readonly" id="txtMenu_{menuId}" />
            <a href="javascript:void(0)" onclick="orderMeal.upAmount('{menuId}');">
                <img border="0" align="absmiddle" src="/App_Themes/ThemesV2/images/jjh.png" alt="" /></a> <span id="menuSum_{menuId}">￥{menuPrice}</span> <a href="javascript:void(0)"
                    onclick="orderMeal.delMenuItem('{menuId}');">
                    <img border="0" align="middle" src="/App_Themes/ThemesV2/images/dc01-jh.png" alt="" /></a></div>
        </li>
    </div>
</div>
<%--<script src="/Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>--%>
<script type="text/javascript" language="javascript">
    var OrderMeal = function (opts) {
        var Opts = $.extend({
            companyId: 0,
            isLogin: false,
            isWorkingTime: true,
            menus: {},
            orderId: 0, //单据ID
            orders: {},  //已选择的菜单{id:{menudId:0,menuName:'',menuPrice:0,menuAmount:0,dirId:0}}
            orderCount: 0, //已选择菜单数
            orderSum: 0, //金额
            companyRate: 0, //积分比例
            serviceSum: 0, //送餐费
            oldOrder: {}, //旧数据
            userData: { userFavAddress: [], info: { txtUserMemo: '', txtUserPhone: '', txtSeat: '', txtFloor: '', txtRoom: '', mansionId: 0} }, //会员数据
            isCheckCode: true,
            mansionId: '',
            mansionName: '',
            onSetSum: '',
            itemId: 0
        }, opts),
        curDir, orderCtr, tplCtr, msnId, isInitOrderContainer, addrCtr, isProcess;
        this.changeMenuDir = function (dirId) {
            var dir = $("#dir_" + dirId);
            var isShow = dir.attr("show") === 'true';
            $("#menuContainer > li > ul[show='true']").hide().attr('show', 'false');
            if (!isShow)
                dir.show().attr('show', 'true');
        };
        this.addMenuItem = function (menuId) {
            if (!Opts.menus[menuId]) {
                alert("你所选择的菜单不存在!");
                return;
            } else if (Opts.orders[menuId]) {
                //alert("你已选择了此菜单!");
                this.upAmount(menuId);
                return;
            }
            _addMenuItemCore(menuId, 1, true);
        };
        this.delAllMenu = function () {
            if (Opts.orderCount <= 0) {
                alert("请选择餐点");
                return;
            }
            for (var m in Opts.orders) {
                this.delMenuItem(m, true);
            }
        };
        this.delMenuItem = function (menuId) {
            if (!Opts.menus[menuId]) {
                alert("你所选择的菜单不存在!");
                return;
            } else if (!Opts.orders[menuId]) {
                alert("你还没选择此菜单!");
                return;
            }
            var order = _getOrderItem(menuId);
            $("#menu_" + menuId).remove();
            Opts.orderCount--;
            _refreshInfo(menuId, -order.menuAmount);
            delete Opts.orders[menuId];
            $("#li_menu_b_" + menuId).hide();
            $("#li_menu_" + menuId).removeClass("gray_li");
            //            if (Opts.orderCount == 0)
            //                $("#dcxxt").show();
            $("#leftBox").css("min-height", $("#slider").height());
        };
        this.upAmount = function (menuId) {
            if (!Opts.menus[menuId]) {
                alert("你所选择的菜单不存在!");
                return;
            } else if (!Opts.orders[menuId]) {
                alert("你还没选择此菜单!");
                return;
            }
            var menu = Opts.menus[menuId];
            if (!_checkMenuGroup(menu))
                return;
            _refreshInfo(menuId, 1);
            var item = Opts.orders[menuId];
            $("#li_menu_b_" + menuId).text(item.menuAmount);
        };
        this.downAmount = function (menuId) {
            if (!Opts.menus[menuId]) {
                alert("你所选择的菜单不存在!");
                return;
            } else if (!Opts.orders[menuId]) {
                alert("你还没选择此菜单!");
                return;
            } else if (Opts.orders[menuId].menuAmount <= 1) {
                //alert("订购数量不能小于一.");
                this.delMenuItem(menuId);
                return;
            }

            _refreshInfo(menuId, -1);
            var item = Opts.orders[menuId];
            $("#li_menu_b_" + menuId).text(item.menuAmount);
        };
        this.submitOrder = function () {
            if (isProcess) {
                alert("正在提交，请稍候.");
                return;
            }
            if (!_checkUserInput())
                return;
            var data = {};
            data["companyId"] = Opts.companyId;
            data["userPhone"] = $("#txtUserPhone").val();
            data["mansionId"] = Opts.mansionId;
            data["address"] = _getUserSeat() + _getUserFloor() + '|' + _getUserRoom();
            data["msnLogId"] = msnId;
            data["checkCode"] = $("#txtCode").val();
            data["orderId"] = Opts.orderId;
            data["memo"] = $("#txtUserMemo").attr("defaultval") == $("#txtUserMemo").val() ? '' : $("#txtUserMemo").val();
            data["itemId"] = Opts.itemId;
            isProcess = true;
            $.ajax({
                type: "POST",
                url: "/Public/OrderMealServices.asmx/OrderMeal",
                dataType: "xml", data: { userData: __toJSON(data), orderData: __toJSON(getOrderData()) },
                success: function (xml) {
                    var result = __toObject($(xml).text());
                    isProcess = false;
                    if (result.orderId > 0) {
                        if (result.code == 2)
                            alert(result.message);
                        window.location.href = "/Member/OrderMealViewPage.aspx?OrderId=" + result.orderId;
                        return false;
                    }
                    alert(result.message);
                }
            });
            //            $.ajax({
            //                type: "POST",
            //                url: "/public/resthandler.ashx/app/log",
            //                data: { source: 'addressLog', message: $("#txtMansionName").val() + "-" + $("#txtFloor").val() + "-" + $("#txtRoom").val() }
            //            });
        };
        function getOrderData() {
            var result = [];
            for (var item in Opts.orders)
                result.push(Opts.orders[item]);
            return result;
        }
        function sendCode(fnCallback) {
            if (!_checkWorkingTime())
                return;
            var phone = $("#txtUserPhone").val();
            if (!phone || phone.length < 11) {
                alert("请输入正确的手机号码");
                $("#txtUserPhone").focus();
                return;
            }
            if (!(Opts.orderCount > 0) && isLogin) {
                alert("请选择餐点");
                return;
            }
            $.ajax({
                type: "POST",
                url: "/Public/RestHandler.ashx/OrderMeal/GetMsnCode",
                dataType: "json", data: { phone: phone },
                success: function (result) {
                    if (result.code > 0) {
                        msnId = result.code;
                        var btn = $("#btnSendCode");
                        var sendCount = btn.data("sendCount") || 0;
                        if (sendCount >= 1) {
                            btn.hide();
                            return;
                        }
                        btn.data("sendCount", sendCount + 1);
                        fnCallback();
                    } else if (result.code == -2) {
                        alert("您已是乐多分会员，请直接登录，即可下单^_^");
                        document.location.href = "/Public/Login.aspx?ReturnUrl=" + encodeURIComponent("/Public/OrderMealPage.aspx") + "&phone=" + phone;
                        return;
                    }
                    else
                        alert(result.message);
                }
            });
        };
        function _getUserSeat() {
            var val = addrCtr.val() || "";
            if (val && val.length > 0) {
                val = val.replace(Opts.mansionName, "") + "||";
            }
            return val;
        }
        function _getUserFloor() {
            var floor = ($("#txtFloor").val() || '').replace(/\|/g, '').replace(/ /g, '').replace(/楼|层$/, '');
            if (floor.length > 0) {
                var c = floor.charAt(floor.length - 1);
                if (c == '楼' || c == '层')
                    floor = floor.substring(0, floor.length - 1);
            }
            return floor;
        }
        function _getUserRoom() {
            var room = ($("#txtRoom").val() || '').replace(/\|/g, '').replace(/ /g, '').replace(/^楼|层/, '')
            if (room == $("#txtRoom").attr('title')) {
                room = '';
            } else if (room.length > 0) {
                var c = room.charAt(0);
                if (c == '楼' || c == '层')
                    room = room.substring(1, room.length - 1);
            }
            return room;
        }
        function _checkUserAddrInput() {
            setTimeout(function () {
                var val = addrCtr.val();
                if (!val || val.indexOf(Opts.mansionName) == -1)
                    addrCtr.val(Opts.mansionName);
            }, 100);
        }
        function _checkUserInput() {
            if (!_checkWorkingTime())
                return false;
            if (Opts.orderCount <= 0) {
                alert("请选择餐点");
                return false;
            }
            //检测套餐和赠送品是否合法
            var m;
            var order;
            for (var o in Opts.orders) {
                m = Opts.menus[o];
                if (!_checkMenuGroup(m, true))
                    return false;
            }

            if (Opts.isCheckCode && !(msnId > 0)) {
                alert("请验证手机号码");
                $("#codeContainer").show();
                $("#txtUserPhone").focus();
                return false;
            }
            val = $("#txtUserPhone").val();
            if (!val) {
                alert("请输入手机号码");
                $("#txtUserPhone").focus();
                return false;
            }
            if (Opts.isCheckCode) {
                val = $("#txtCode").val();
                if (!val) {
                    alert("请验证手机号码");
                    $("#txtCode").focus();
                    return false;
                }
            } else if (Opts.userData.txtUserPhone) {
                val = $("#txtUserPhone").val();
                if (val != Opts.userData.info.txtUserPhone) {
                    alert("请验证手机号码");
                    Opts.isCheckCode = true;
                    $("#codeContainer").show();
                    return false;
                }
            }
            val = _getUserFloor();
            if (!val || val == '') {
                alert("请输入你的楼层.");
                $("#txtFloor").focus();
                return false;
            }
            val = _getUserRoom();
            if (!val || val == '') {
                alert("请输入你的房间号.");
                $("#txtRoom").focus();
                return false;
            }
            //            val = $("#txtUserMemo").val();
            //            if (val && val.length > 15) {
            //                alert("嘱咐内容不能大于15个字.");
            //                $("#txtUserMemo").focus();
            //                return false;
            //            }
            if (Opts.onSetSum > 0 && Opts.orderSum < Opts.onSetSum) {
                alert(Opts.onSetSum.toString() + "元起送，再选点别的东东吧！");
                return false;
            }

            return true;
        }
        function _checkUnderZeroPrice(menu, allowEq) {
            var countLTZero = allowEq ? 0 : 1;
            var countGTZero = 0;
            var dirId = menu.dirId;
            var order;
            //计算同组大于零的总数
            for (var o in Opts.orders) {
                order = Opts.orders[o];
                if (order.dirId === dirId) {
                    if (order.menuPrice > 0)
                        countGTZero = order.menuAmount + countGTZero;
                    else if (order.menuPrice < 0)
                        countLTZero = order.menuAmount + countLTZero;
                }
            }
            if (!allowEq && countGTZero == 0) {
                alert("该菜品不可单点哦！");
                return false;
            } else if (countGTZero < countLTZero) {
                if (allowEq && countGTZero == countLTZero)
                    return true;
                alert("您点的菜品数量不符哦！");
                return false;
            }
            return true;
        }
        function _checkEqualZeroPrice(menu, allowEq) {
            var hasGTZeroPrice = false;
            var hasDirGTZeroPrice = false;
            var count = 0;
            for (var mId in Opts.menus) {
                var m = Opts.menus[mId];
                if (m.dirId == menu.dirId && m.menuPrice > 0) {
                    hasDirGTZeroPrice = true;
                    break;
                }
            }
            for (var o in Opts.orders) {
                order = Opts.orders[o];
                if (hasDirGTZeroPrice) {
                    if (order.dirId == menu.dirId && order.menuPrice > 0) {
                        hasGTZeroPrice = true;
                        count++;
                        break;
                    }
                } else if (order.menuPrice > 0) {
                    hasGTZeroPrice = true;
                    break;
                }
            }
            if (!hasGTZeroPrice) {
                if (hasDirGTZeroPrice && count > 0)
                    alert("必须与本系列其他菜品搭配哦！");
                else
                    alert("该菜品不可单点哦！");
                return false;
            }
            return true;
        }
        function _checkMenuGroup(menu, allowEq) {
            var price = menu.menuPrice;
            if (price < 0)
                return _checkUnderZeroPrice(menu, allowEq);
            else if (price == 0)
                return _checkEqualZeroPrice(menu, allowEq);
            return true;
        }
        function _initAddMenuItem() {
            if (!isInitOrderContainer) {
                $("#orderInfoContainer").html($("#selectedItemtemplate").html());
                orderCtr = $("#orderContainer");
                isInitOrderContainer = true;
                $("#myInfoContainer [isshow=false]").show();
                if (Opts.isCheckCode)
                    $("#codeContainer").show();
            }
            $("#dcxxt").hide();
        }
        function _checkWorkingTime() {
            if (!(Opts.isWorkingTime))
                alert("当前暂不外送^_^");
            //            var d = new Date();
            //            var d1 = new Date(2013, 1, 5);
            //            var d2 = new Date(2013, 1, 17);
            //            if (d >= d1 && d <= d2) {
            //                alert("春节假期休息中");
            //                return false;
            //            }
            return Opts.isWorkingTime;
        }
        function _addMenuItemCore(menuId, amount, isLimit) {
            if (!_checkWorkingTime())
                return;
            var m = Opts.menus[menuId.toString()];
            if (isLimit && !_checkMenuGroup(m))
                return;
            _initAddMenuItem();
            var html = __replace(tplCtr.html(), 'menuId', m.menuId);
            html = __replace(html, 'menuName', m.menuName.substr(0, 10));
            html = __replace(html, 'menuPrice', m.menuPrice);
            orderCtr.append(html);
            _refreshInfo(menuId, amount);
            var orderItem = Opts.orders[menuId];
            if (orderItem && !orderItem.isCompanyItem) {
                $("#li_menu_b_" + menuId).text(orderItem.menuAmount).show();
                $("#li_menu_" + menuId).addClass("gray_li");
            }
            $("#leftBox").css("min-height", $("#slider").height());
        };
        function _refreshInfo(menuId, amount) {
            var order = _getOrderItem(menuId);
            order.menuAmount = order.menuAmount + amount;
            var menuSum = __round(amount * order.menuPrice, 2);
            Opts.orderSum += menuSum;
            var txtMenu = $("#txtMenu_" + menuId);
            if (txtMenu.length > 0)
                txtMenu.val(order.menuAmount);
            txtMenu = $("#menuSum_" + menuId);
            if (txtMenu.length > 0)
                txtMenu.text("￥" + __round(order.menuAmount * order.menuPrice, 2));
            //set point info
            $("#disPoint").val("成功下单后您可获" + __round(Opts.orderSum * Opts.companyRate, 2) + "个积分");
            $("#disSum").val("总计：" + (Opts.orderCount > 0 ? (Opts.orderSum + Opts.serviceSum) : 0) + "元");
        };
        function _getOrderItem(menuId) {
            if (typeof menuId != 'string')
                menuId = menuId.toString();
            if (!Opts.orders[menuId]) {
                var menu = Opts.menus[menuId];
                Opts.orderCount++;
                Opts.orders[menuId] =
                    {
                        'menudId': menu.menuId,
                        'menuName': menu.menuName,
                        'menuPrice': menu.menuPrice,
                        'menuAmount': 0,
                        'dirId': menu.dirId,
                        'sort': Opts.orderCount,
                        'isCompanyItem': menu.menuId < 0
                    };
            }
            return Opts.orders[menuId];
        };
        //        function _checkUserAddrInput() {
        //            setTimeout(function () {
        //                var val = addrCtr.val();
        //                if (!val || val.indexOf(Opts.mansionName) == -1)
        //                    addrCtr.val(Opts.mansionName);
        //            }, 100);
        //        }
        this.onPhoneChange = function () {
            var val = $("#txtUserPhone").val();
            if (Opts.userData["info"] && !(Opts.userData.info.txtUserPhone && val == Opts.userData.info.txtUserPhone)) {
                Opts.isCheckCode = true;
                $("#codeContainer").show();
                msnId = 0;
            }
        }
        function __init() {
            isProcess = false;
            isInitOrderContainer = false;
            //$("#txtAddress").
            $("#btnAddFav").click(__addFavCompany);
            tplCtr = $("#menuItemTemplate");
            msnId = 0;
            if (!Opts.isCheckCode) {
                $("#codeContainer").hide();
                $('#txtUserPhone').attr("readonly", "readonly");
            }
            //set addr info
            addrCtr = $('#txtMansionName');

            if (typeof (Opts.userData.info) != 'undefined') {
                $("#txtFloor").val(Opts.userData.info.txtFloor);
                if (Opts.userData.info.txtRoom) {
                    $("#txtRoom").val(Opts.userData.info.txtRoom);
                }
                $('#txtUserPhone').val(Opts.userData.info.txtUserPhone);
                $('#' + Opts.mansionSelectorId).val(Opts.userData.info.mansionId);
                if (Opts.userData.info.txtUserMemo && Opts.userData.info.txtUserMemo != '')
                    $('#txtUserMemo').val(Opts.userData.info.txtUserMemo);
                if (Opts.userData.info.txtSeat) {
                    addrCtr.val(Opts.mansionName + Opts.userData.info.txtSeat);
                    addrCtr.attr("readonly", "readonly");
                } else
                    addrCtr.val(Opts.mansionName);
            } else
                addrCtr.val(Opts.mansionName);
            $("#txtRoom").focusin(function () {
                if ($(this).val() == $(this).attr("title"))
                    $(this).val('');
            }).focusout(function () {
                if ($(this).val() == '')
                    $(this).val($(this).attr('title'));
            });
            if (Opts.serviceSum > 0)
                $("#disServicesSum").val("送餐费：" + Opts.serviceSum + "元");
            else
                $("#disServicesSum").parent().hide();
            $("#disSum").val("总计：0元");
            if ($("#txtUserMemo").val() == '')
                $("#txtUserMemo").val($("#txtUserMemo").attr("defaultval"));
            $("#txtUserMemo").focusin(function () {
                var val = $("#txtUserMemo").val();
                if (val == $("#txtUserMemo").attr("defaultval"))
                    $("#txtUserMemo").val("");
            });
            $("#txtUserMemo").focusout(function () {
                var val = $("#txtUserMemo").val();
                if (!val || val == "")
                    $("#txtUserMemo").val($("#txtUserMemo").attr("defaultval"));
            });

            $("#disPoint").val("");
            //bind user address input event
            var el = document.getElementById("txtMansionName");
            if ("\v" == "v") {
                el.onpropertychange = _checkUserAddrInput;
            } else {
                el.addEventListener("input", _checkUserAddrInput, false);
            }
            if (!(Opts.orderId > 0))
                $("#myInfoContainer [isshow=false]").hide();
            //init send code
            new SendWaitMsnMessage({ btnSend: 'btnSendCode', fnSendHandler: sendCode });
            if (Opts.itemId > 0) {
                _addMenuItemCore(-Opts.itemId, 1);
            }
            if (Opts.orderId > 0 && Opts.oldOrder.length > 0) {
                var m;
                for (var i = 0; i < Opts.oldOrder.length; i++) {
                    m = Opts.oldOrder[i];
                    if (!m.isCompanyItem) {
                        _addMenuItemCore(m.menuId, m.amount);
                        var item = Opts.orders[m.menuId.toString()];
                        $("#li_menu_b_" + m).text(item.menuAmount).show();
                        $("#li_menu_" + m).addClass("gray_li");
                    }
                }
            }
        };
        function __addFavCompany() {
            if (!Opts.isLogin) {
                alert("收藏餐厅，请先登录。");
                return;
            }
            if (!Opts.companyId || Opts.companyId <= 0) {
                alert("当前商家不合法");
                return;
            }
            $.ajax({
                type: "POST",
                url: "/Public/OrderMealServices.asmx/AddFavCompany",
                dataType: "xml", data: { companyId: Opts.companyId },
                success: function (xml) {
                    var result = __toObject($(xml).text());
                    if (result.code == 0) {
                        $("#<%=favCompanyPic.ClientID%>").attr("src", "/App_Themes/ThemesV2/images/cwdctsc.png");
                    } else if (result.code == 1) {
                        $("#<%=favCompanyPic.ClientID%>").attr("src", "/App_Themes/ThemesV2/images/tjct.png");
                    }
                }
            });
        };
        function __replace(sources, key, value) {
            var re = eval('/{' + key + '}/g');
            return sources.replace(re, value);
        };
        function __round(dec, num) {
            var dd = 1;
            var tempnum;
            for (i = 0; i < num; i++) {
                dd *= 10;
            }
            tempnum = dec * dd;
            tempnum = Math.round(tempnum);
            return tempnum / dd;
        };
        function __toObject(arg) {
            return JSON.parse(arg);
        };
        function __toJSON(obj) {
            return JSON.stringify(obj);
        };
        __init();
    }
</script>
<script type="text/javascript">
    $.fn.smartFloat = function () {
        var position = function (element) {
            var top = element.position().top, pos = element.css("position"), h = $("#orderBox").height(), height = document.documentElement.clientHeight;
            $(window).scroll(function () {
                var scrolls = $(this).scrollTop();
                var h1 = $("#orderBox").height();
                if (scrolls > top && h1 > h && h1 < height) {
                    if (window.XMLHttpRequest) {
                        element.css({
                            position: "fixed",
                            top: 0
                        });
                    } else {
                        element.css({
                            top: scrolls
                        });
                    }
                } else {
                    element.css({
                        position: pos,
                        top: top
                    });
                }
            });
        };
        return $(this).each(function () {
            position($(this));
        });
    };
</script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        window["orderMeal"] = new OrderMeal(<%=this.InitData %>);
        $(".dcxxt-main > ul > li >a").attr("href", "javascript:void(0);")
                                         .click(function () {
                                             var s = $(this).siblings();
                                             var dis = s.css("display");
                                             $(".dcxxt-main > ul > li > p:parent").hide();
                                             if (s.length > 0) {
                                                 if (dis == "none")
                                                     s.show();
                                                 else
                                                     s.hide();
                                             }
                                             $("#leftBox").css("min-height", $("#slider").height());
                                         });
            //绑定
            $("#slider").smartFloat();
            //set min-height
            $("#leftBox").css("min-height", $("#slider").height());
			//$("#slider").height($(".kk .left").height());
            FaceBook.init();
    });
</script>
