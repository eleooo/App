(function(){"use strict";Date.prototype.format=function(n){var t=n,i;return n||(t="yyyy-MM-dd hh:mm:ss"),i=["日","一","二","三","四","五","六"],t=t.replace(/yyyy|YYYY/,this.getFullYear()),t=t.replace(/yy|YY/,this.getYear()%100>9?(this.getYear()%100).toString():"0"+this.getYear()%100),t=t.replace(/MM/,this.getMonth()>=9?(parseInt(this.getMonth())+1).toString():"0"+(parseInt(this.getMonth())+1)),t=t.replace(/M/g,parseInt(this.getMonth())+1),t=t.replace(/w|W/g,i[this.getDay()]),t=t.replace(/dd|DD/,this.getDate()>9?this.getDate().toString():"0"+this.getDate()),t=t.replace(/d|D/g,this.getDate()),t=t.replace(/hh|HH/,this.getHours()>9?this.getHours().toString():"0"+this.getHours()),t=t.replace(/h|H/g,this.getHours()),t=t.replace(/mm/,this.getMinutes()>9?this.getMinutes().toString():"0"+this.getMinutes()),t=t.replace(/m/g,this.getMinutes()),t=t.replace(/ss|SS/,this.getSeconds()>9?this.getSeconds().toString():"0"+this.getSeconds()),t=t.replace(/s|S/g,this.getSeconds()),t.replace(/iii/g,this.getMilliseconds()<10?"00"+this.getMilliseconds():this.getMilliseconds()<100?"0"+this.getMilliseconds():this.getMilliseconds())},Date.prototype.DateAdd=function(n,t){var i=this;switch(n){case"s":return new Date(Date.parse(i)+1e3*t);case"n":return new Date(Date.parse(i)+6e4*t);case"h":return new Date(Date.parse(i)+36e5*t);case"d":return new Date(Date.parse(i)+864e5*t);case"w":return new Date(Date.parse(i)+6048e5*t);case"q":return new Date(i.getFullYear(),i.getMonth()+t*3,i.getDate(),i.getHours(),i.getMinutes(),i.getSeconds());case"m":return new Date(i.getFullYear(),i.getMonth()+t,i.getDate(),i.getHours(),i.getMinutes(),i.getSeconds());case"y":return new Date(i.getFullYear()+t,i.getMonth(),i.getDate(),i.getHours(),i.getMinutes(),i.getSeconds())}},Number.prototype.round=function(n){var t=Math.pow(10,n),i=this;return Math.round(i*t)/t},String.prototype.endsWith=function(n){return this.substr(this.length-n.length)===n},String.prototype.startsWith=function(n){return this.substr(0,n.length)===n},String.prototype.trim=function(){return this.replace(/^\s+|\s+$/g,"")},String.prototype.trimEnd=function(){return this.replace(/\s+$/,"")},String.prototype.trimStart=function(){return this.replace(/^\s+/,"")},Array.add=Array.enqueue=function(n,t){n[n.length]=t},Array.addRange=function(n,t){n.push.apply(n,t)},Array.clear=function(n){n.length=0},Array.clone=function(n){return n.length===1?[n[0]]:Array.apply(null,n)},Array.contains=function(n,t){return Array.indexOf(n,t)>=0},Array.dequeue=function(n){return n.shift()},Array.indexOf=function(n,t,i){var u,r;if(typeof t=="undefined")return-1;if(u=n.length,u!==0)for(i=+i,isNaN(i)?i=0:(isFinite(i)&&(i=i-i%1),i<0&&(i=Math.max(0,u+i))),r=i;r<u;r++)if(typeof n[r]!="undefined"&&n[r]===t)return r;return-1},Array.insert=function(n,t,i){n.splice(t,0,i)},Array.parse=function(n){return n?eval(n):[]},Array.remove=function(n,t){var i=Array.indexOf(n,t);return i>=0&&n.splice(i,1),i>=0},Array.removeAt=function(n,t){n.splice(t,1)}})(window),function(n,t){$.fn.lazyload=function(i){var f=this,r={load:null},u=!1;return $.extend(r,i),$(n).bind("scroll",function(){!u&&r.load&&$(n).scrollTop()==$(t).height()-$(n).height()&&r.load()}),this}}(window,document),function(){function u(n){this._n=n}function f(n,t,i){var u=Math.pow(10,t),r,f;return r=(Math.round(n*u)/u).toFixed(t),i&&(f=new RegExp("0{1,"+i+"}$"),r=r.replace(f,"")),r}function e(n,t){return t.indexOf("$")>-1?c(n,t):t.indexOf("%")>-1?l(n,t):t.indexOf(":")>-1?a(n,t):y(n,t)}function h(i,u){var f,o;if(u.indexOf(":")>-1)i._n=v(u);else if(u===r)i._n=0;else{f=u,n[t].delimiters.decimal!=="."&&(u=u.replace(/\./g,"").replace(n[t].delimiters.decimal,"."));var h=new RegExp("[^a-zA-Z]"+n[t].abbreviations.thousand+"(?:\\)|(\\"+n[t].currency.symbol+")?(?:\\))?)?$"),c=new RegExp("[^a-zA-Z]"+n[t].abbreviations.million+"(?:\\)|(\\"+n[t].currency.symbol+")?(?:\\))?)?$"),l=new RegExp("[^a-zA-Z]"+n[t].abbreviations.billion+"(?:\\)|(\\"+n[t].currency.symbol+")?(?:\\))?)?$"),a=new RegExp("[^a-zA-Z]"+n[t].abbreviations.trillion+"(?:\\)|(\\"+n[t].currency.symbol+")?(?:\\))?)?$"),s=["KB","MB","GB","TB","PB","EB","ZB","YB"],e=!1;for(o=0;o<=s.length;o++)if(e=u.indexOf(s[o])>-1?Math.pow(1024,o+1):!1,e)break;i._n=(e?e:1)*(f.match(h)?Math.pow(10,3):1)*(f.match(c)?Math.pow(10,6):1)*(f.match(l)?Math.pow(10,9):1)*(f.match(a)?Math.pow(10,12):1)*(u.indexOf("%")>-1?.01:1)*Number((u.indexOf("(")>-1?"-":"")+u.replace(/[^0-9\.-]+/g,"")),i._n=e?Math.ceil(i._n):i._n}return i._n}function c(i,r){var o=r.indexOf("$")<=1?!0:!1,f="",u;return r.indexOf(" $")>-1?(f=" ",r=r.replace(" $","")):r.indexOf("$ ")>-1?(f=" ",r=r.replace("$ ","")):r=r.replace("$",""),u=e(i,r),o?u.indexOf("(")>-1||u.indexOf("-")>-1?(u=u.split(""),u.splice(1,0,n[t].currency.symbol+f),u=u.join("")):u=n[t].currency.symbol+f+u:u.indexOf(")")>-1?(u=u.split(""),u.splice(-1,0,f+n[t].currency.symbol),u=u.join("")):u=u+f+n[t].currency.symbol,u}function l(n,t){var r="",i;return t.indexOf(" %")>-1?(r=" ",t=t.replace(" %","")):t=t.replace("%",""),n._n=n._n*100,i=e(n,t),i.indexOf(")")>-1?(i=i.split(""),i.splice(-1,0,r+"%"),i=i.join("")):i=i+r+"%",i}function a(n){var i=Math.floor(n._n/3600),t=Math.floor((n._n-i*3600)/60),r=Math.round(n._n-i*3600-t*60);return i+":"+(t<10?"0"+t:t)+":"+(r<10?"0"+r:r)}function v(n){var i=n.split(":"),t=0;return i.length===3?(t=t+Number(i[0])*3600,t=t+Number(i[1])*60,t=t+Number(i[2])):i.length===2&&(t=t+Number(i[0])*60,t=t+Number(i[1])),Number(t)}function y(i,u){var y=!1,k=!1,e="",a="",v="",c=Math.abs(i._n),b,p,d,l;if(i._n===0&&r!==null)return r;if(u.indexOf("(")>-1&&(y=!0,u=u.slice(1,-1)),u.indexOf("a")>-1&&(u.indexOf(" a")>-1?(e=" ",u=u.replace(" a","")):u=u.replace("a",""),c>=Math.pow(10,12)?(e=e+n[t].abbreviations.trillion,i._n=i._n/Math.pow(10,12)):c<Math.pow(10,12)&&c>=Math.pow(10,9)?(e=e+n[t].abbreviations.billion,i._n=i._n/Math.pow(10,9)):c<Math.pow(10,9)&&c>=Math.pow(10,6)?(e=e+n[t].abbreviations.million,i._n=i._n/Math.pow(10,6)):c<Math.pow(10,6)&&c>=Math.pow(10,3)&&(e=e+n[t].abbreviations.thousand,i._n=i._n/Math.pow(10,3))),u.indexOf("b")>-1)for(u.indexOf(" b")>-1?(a=" ",u=u.replace(" b","")):u=u.replace("b",""),b=["B","KB","MB","GB","TB","PB","EB","ZB","YB"],l=0;l<=b.length;l++)if(p=Math.pow(1024,l),d=Math.pow(1024,l+1),i._n>=p&&i._n<d){a=a+b[l],p>0&&(i._n=i._n/p);break}u.indexOf("o")>-1&&(u.indexOf(" o")>-1?(v=" ",u=u.replace(" o","")):u=u.replace("o",""),v=v+n[t].ordinal(i._n)),u.indexOf("[.]")>-1&&(k=!0,u=u.replace("[.]","."));var s=i._n.toString().split(".")[0],o=u.split(".")[1],g=u.indexOf(","),h="",w=!1;return o?(o.indexOf("[")>-1?(o=o.replace("]",""),o=o.split("["),h=f(i._n,o[0].length+o[1].length,o[1].length)):h=f(i._n,o.length),s=h.split(".")[0],h=h.split(".")[1].length?n[t].delimiters.decimal+h.split(".")[1]:"",k&&Number(h.slice(1))===0&&(h="")):s=f(i._n,null),s.indexOf("-")>-1&&(s=s.slice(1),w=!0),g>-1&&(s=s.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g,"$1"+n[t].delimiters.thousands)),u.indexOf(".")===0&&(s=""),(y&&w?"(":"")+(!y&&w?"-":"")+s+h+(v?v:"")+(e?e:"")+(a?a:"")+(y&&w?")":"")}function p(t,i){n[t]=i}var i,o="1.4.9",n={},t="en",r=null,s=typeof module!="undefined"&&module.exports;i=function(n){return i.isNumeral(n)?n=n.value():Number(n)||(n=0),new u(Number(n))},i.version=o,i.isNumeral=function(n){return n instanceof u},i.language=function(r,u){if(!r)return t;if(r&&!u){if(!n[r])throw new Error("Unknown language : "+r);t=r}return(u||!n[r])&&p(r,u),i},i.language("en",{delimiters:{thousands:",",decimal:"."},abbreviations:{thousand:"k",million:"m",billion:"b",trillion:"t"},ordinal:function(n){var t=n%10;return~~(n%100/10)==1?"th":t===1?"st":t===2?"nd":t===3?"rd":"th"},currency:{symbol:"$"}}),i.zeroFormat=function(n){r=typeof n=="string"?n:null},i.fn=u.prototype={clone:function(){return i(this)},format:function(n){return e(this,n?n:i.defaultFormat)},unformat:function(n){return h(this,n?n:i.defaultFormat)},value:function(){return this._n},valueOf:function(){return this._n},round:function(n){return this._n.round(n)},set:function(n){return this._n=Number(n)||0,this},add:function(n){return this._n=this._n+(Number(n)||0),this},subtract:function(n){return this._n=this._n-(Number(n)||0),this},multiply:function(n){return this._n=this._n*(Number(n)||0),this},divide:function(n){return this._n=this._n/Number(n),this},difference:function(n){var t=this._n-(Number(n)||0);return t<0&&(t=-t),t}},s&&(module.exports=i),typeof ender=="undefined"&&(this.numeral=i),typeof define=="function"&&define.amd&&define([],function(){return i})}.call(this),function(){var n=function(){function n(n,i){return i===undefined?(localStorage||t)[n]:(localStorage||t)[n]=i&&typeof i!="string"?JSON.stringify(i):i}var t={};return{ViewCache:function(t,i){return n(t,i)},WebAuthKey:function(t){return n("WebAuthKey",t)},UserPhone:function(t){return n("UserPhone",t)},UserPwd:function(t){return n("UserPwd",t)},UserID:function(t){return n("UserID",t)},CompanyID:function(t){return n("CompanyID",t)},LatestUpdateOn:function(t){return n("LatestUpdateOn",t)},Version:function(t){return n("Version",t)},IsSavePwd:function(t){return n("IsSavePwd",t)},IsAutoLogin:function(t){return n("IsAutoLogin",t)}}};window.DS=n()}(window),function(){var n=function(){function t(n,t,i){return i=i||"Get",{name:n,isAuth:t,action:i}}function u(n){return app.getUrl()+"/public/RestHandler.ashx/"+n.name+"/"+n.action}function i(n,t,i){if(!app.ol()){i({code:-1,message:"No network connect."});return}t=t||{},n.isAuth&&(t.__t=DS.WebAuthKey());var f=u(n);r[f]||($.ajax({url:f,data:t,dataType:"json",type:"POST",cache:!1,success:function(n){i(n),delete t,delete n},complete:function(){delete r[f],app.spinner(!1)},beforeSend:function(){app.spinner(!0)},error:function(){delete r[f],app.spinner(!1),app.showtips("services call errors.")}}),r[f]=!0)}var r={},f=typeof Zepto==undefined,n={Login:t("App",!1,"Login"),SendPassword:t("App",!1,"SendPassword"),Ver:t("App",!1,"GetVer"),GetOrders:t("OrderMeal",!0,"GetOrders"),GetOrderDetail:t("OrderMeal",!0,"GetDetail"),GetOrderTemps:t("OrderMeal",!0,"GetTemps"),SendOrderTemps:t("OrderMeal",!0,"SendTemps"),ConfirmOrder:t("OrderMeal",!0,"SendMessage"),FacebookQuery:t("FaceBook",!0,"Get"),ReplyFacebook:t("FaceBook",!0,"Edit"),GetMyInfo:t("User",!0,"Get"),SaveMyInfo:t("User",!0,"Edit"),GetMenus:t("MealMenu",!0,"Get"),SaveMenus:t("MealMenu",!0,"Edit"),GetItem:t("OrderMeal",!0,"GetItem"),SaveItem:t("OrderMeal",!0,"SaveItem"),GetItems:t("OrderMeal",!0,"GetItems"),GetRushItems:t("OrderMeal",!0,"GetRushItems"),DelItem:t("OrderMeal",!0,"DelItem"),GetFinance:t("App",!0,"GetFinance")};return{Login:function(t,r,u){i(n.Login,{u:t,p:r,s:2},function(n){n.code==0&&(DS.CompanyID(n.data.c),DS.UserID(n.data.id),DS.UserPhone(n.data.p),DS.WebAuthKey(n.data.t)),u(n)})},SendPassword:function(t,r){i(n.SendPassword,{userPhone:t},r)},Ver:function(){i(n.Ver,undefined,function(n){app.logInfo("current version :"+n.data),DS.Version(n.data)})},GetOrders:function(t,r){i(n.GetOrders,t,r)},OrderDetail:function(t,r){i(n.GetOrderDetail,{id:t},r)},ConfirmOrder:function(t,r){i(n.ConfirmOrder,t,r)},FacebookQuery:function(t,r){i(n.FacebookQuery,t,r)},ReplyFacebook:function(t,r){i(n.ReplyFacebook,t,r)},GetMyInfo:function(t){i(n.GetMyInfo,null,t)},SaveMyInfo:function(t,r){i(n.SaveMyInfo,t,r)},GetMenus:function(t,r){i(n.GetMenus,t,r)},SaveMenus:function(t,r){i(n.SaveMenus,t,r)},GetItem:function(t,r){i(n.GetItem,{id:t},r)},GetItems:function(t,r){i(n.GetItems,t,r)},GetRushItems:function(t,r){i(n.GetRushItems,t,r)},SaveItem:function(t,r){i(n.SaveItem,t,r)},delItem:function(t,r){i(n.DelItem,t,r)},GetFinance:function(t,r){i(n.GetFinance,t,r)},getUrl:function(t){var i=n[t];return i?u(i):null},callServices:function(t,r,u){var f=n[t];f&&i(f,r,u)}}};window.WS=new n}(),function(){var n=function(n){function v(){if(u&&!i){t?t.close():t=!1,i=!0;var n=new window[u](l);"addEventListener"in n?(n.addEventListener("open",function(t){e(n,t)}),n.addEventListener("message",s),n.addEventListener("close",c),n.addEventListener("error",o)):(n.onopen=function(t){e(n,t)},n.onmessage=s,n.onclose=c,n.onerror=o),r.Login=app.logError}}function y(){var n={Date:DS.LatestUpdateOn(),UserId:DS.UserID(),CompanyId:DS.CompanyID(),SubSys:2,LoginSys:2,Platform:app.platform(),Version:app.appVer()};h("Login-"+DS.WebAuthKey(),n)}function e(n){i=!1,t=n,y()}function o(n){console.log(n)}function s(n){var t=n.data;typeof t=="string"&&(t=JSON.parse(t)),t.cmd&&r[t.cmd]&&r[t.cmd](t),delete t}function h(n,i){i=i||"",typeof i!="string"&&(i=JSON.stringify(i)),f()&&t.send(n+" "+i)}function c(){i=!1,t==!1?app.showtips("无法连接到推送服务器."):t=!1}function f(){return t&&t.readyState==a.OPEN}var l=n,a={CONNECTING:0,OPEN:1,CLOSING:2,CLOSED:3},t=!1,i=!1,r={},u="MozWebSocket"in window?"MozWebSocket":"WebSocket"in window?"WebSocket":null;return{Inited:function(){return!(t===!1)},Connected:function(){return f()},connect:function(){f()||v()},support:function(){return u!=undefined},regCommmand:function(n,t){r[n]=t},log:function(n){n&&(typeof n!="string"&&(n=JSON.stringify(n)),h("Log",n))},close:function(){t&&t.close()}}};window.WebPusher=n}(),function(){var n=function(){function w(n){var i=f[n];return i===undefined&&(i=new window["$_"+n],f[n]=i,t[n]=i),i}function b(){u=new WebPusher($D.pusher),$D.WIN||(document.addEventListener("online",d,!1),document.addEventListener("resume",function(){c=!1,d()},!1),document.addEventListener("pause",function(){c=!0},!1),document.addEventListener("backbutton",function(){h.length>0?app.goback():app.confirm("你确定要退出程序吗?",undefined,"确定,取消",function(n){n=="1"?(u.close(),navigator.app.exitApp()):void 0})},!0),document.addEventListener("touchend",function(n){var t=n.target.nodeName.toUpperCase();if(!(t in["INPUT","TEXTAREA"]))return n.preventDefault(),n.stopPropagation(),!1})),o=$("body"),p=(v=$("#footer",o)).find("a").bind("tap",function(n){var i=n.currentTarget.getAttribute("t");t[i].call(this,n)}),l=$("#spinner",o),a=$("#prompter",o).bind("tap",app.hideTips),DS.IsAutoLogin()&&DS.WebAuthKey()?t.showOrderList():t.showLogin(),$(window.applicationCache).bind("downloading",function(){app.showtips("正在更新程序...")}).bind("updateready",function(){this.swapCache(),app.alert("请重启应用程序.","更新完成","退出程序",function(){$D.WIN?document.location.reload():navigator.app.exitApp()})}).bind("cached",function(){app.showtips("更新完成.")})}function g(n){if(!app.ol()){app.logError("没有检测到网络连接.",!0);return}u.Connected()||(u.connect(),u.regCommmand("Notify",function(n){app.notify(n.data.message,n.data.title)}),n&&u.regCommmand("Order",n))}function nt(n,t,i,r){return n.box()!==!1&&n.box()!==undefined?!1:(n.box($("<div class='left'><\/div>").hide().bind("tap",it).appendTo(o)),!$.isFunction(VT[t]))?($.isFunction(n.init)&&n.init(),!1):$.isFunction(n.renderView)?(n.renderView(function(u){n.box().html(VT[t](u)),$.isFunction(n.init)&&n.init(),k(n,t,i,r)}),!0):(n.box().html(VT[t](undefined)),$.isFunction(n.init)&&n.init(),!1)}function k(n,t,i,u){var f,o,s;e&&(f=w(e),$.isFunction(f.onClose)?f.onClose():void 0,f.box().hide(),u||h.push(e),r&&(r.stop(),r.release(),r=null)),o=!1,p.each(function(n,i){s=i.getAttribute("v"),s.indexOf(","+t+",")>-1?(i.className="nav_on",o=!0):i.className=""}),o?v.show():v.hide(),$.isFunction(n.show)&&n.show(i),n.box().show(),e=t}function i(n,t,i){if(n!==e){$(window).unbind("scroll");var r=w(n);nt(r,n,t,i)||k(r,n,t,i)}}function tt(n){return n.indexOf(".")==-1?f[e][n]:t[n.replace("app.","")]}function it(n){var t=$(n.target),r=t.attr(n.type)||(t=t.parent("[tap]"),t.attr(n.type)),i;r&&(i=tt(r),$.isFunction(i)&&i.call(t,t,n))}function d(){t.initPS()}var t=n.prototype,o,l,a,s=!1,f={},e=!1,p=!1,v=!1,y="OrderList",h=[],r=!1,c=!1,u=!1;t.init=function(){$(document).ready(function(){$D.WIN?b():document.addEventListener("deviceready",b)})},t.initPS=function(){g(f[y].onPushOrder)},t.psInited=function(){return u.Inited()},t.goback=function(){var n=h.pop()||y;i(n,undefined,!0)},t.showLogin=function(n){i("Login",n)},t.showBalance=function(n){i("Balance",n)},t.showOrderHandle=function(n){i("OrderHandle",n)},t.showOrderList=function(n){i(y,n)},t.showDetail=function(n){return i("Detail",n),!1},t.showMenu=function(n){return i("Menu",n),!1},t.showHall=function(n){return i("Hall",n),!1},t.showReview=function(n){return i("Review",n),!1},t.showRush=function(n){return i("Rush",n),!1},t.showRushRecord=function(n){return i("RushRecord",n),!1},t.showSale=function(n){return i("Sale",n),!1},t.showSaleList=function(n){return i("SaleList",n),!1},t.showTemp=function(n){return i("Temp",n),!1},t.logout=function(){DS.IsAutoLogin(!1),DS.WebAuthKey(null);var i,n;for(i in f)n=f[i],$.isFunction(n.reset)&&n.reset();u.close(),h=[],t.showLogin()},t.notify=function(n,i){$D.WIN||c?t.showtips((i?i+":":"")+n):(window.plugins.statusBarNotification.notify(i,n),navigator.notification.beep(2))},t.logInfo=function(n,i){t.showtips(n),console.log(n),i&&u.log(n)},t.trace=function(n,i){t.logInfo(n,i)},t.logError=function(n){t.showtips(n),console.log(n)},t.bindDateSelector=function(n,t){$("#"+n,t).scroller("destroy").scroller({preset:"date",theme:"android",mode:"scroller",display:"modal",lang:"zh",dateFormat:"yy-mm-dd"})},t.inbackground=function(){return c},t.play=function(n){r&&(r.stop(),r.release(),r=!1),n&&(r=new Media(n),r.play())},t.isCordovaApp=function(){return!$D.WIN},t.ol=function(){if($D.WIN)return navigator.onLine;var n=navigator.connection.type;return n!=Connection.UNKNOWN&&n!=Connection.NONE},t.spinner=function(n){n?l.show():l.hide()},t.hideTips=function(){a.addClass("hide").removeClass("top"),$.isFunction(s)&&(s(),s=!1)},t.showtips=function(n,t){console.log(n),$.isFunction(t)&&(s=t),a.removeClass("hide").addClass("top").text(n)},t.getUrl=function(){return $D.url},t.confirm=function(n,t,i,r){if($D.WIN){var u=confirm((t||"")+n);r(u)}else navigator.notification.confirm(n,r,t||$D.appName,i)},t.alert=function(n,t,i,r){if($D.WIN){var u=alert((t||"")+n);r(u)}else navigator.notification.alert(n,r,t||$D.appName,i)},t.isSocket=function(){return u.support()},t.platform=function(){return $D.WIN?navigator.platform:device.platform},t.appVer=function(){return $D.WIN?navigator.appVersion:device.version}};window.app=new n,app.init()}(),function(){var n=function(){var i,t=!1,r=n.prototype;r.box=function(n){return n&&(t=n),t},r.renderView=function(n){var t=new Date,i=t.DateAdd("d",1-t.getDate()).format("yyyy-MM-dd");n({beginDate:i,endDate:t.format("yyyy-MM-dd")})},r.init=function(){i=$("#fList",t).hide(),app.bindDateSelector("txtDetailBeginDate",t),app.bindDateSelector("txtDetailEndDate",t)},r.reset=function(){i.html("")},r.show=function(){var n={t:0,d1:$("#txtDetailBeginDate",t).val(),d2:$("#txtDetailEndDate",t).val()};WS.GetFinance(n,function(n){i.html(n.data),n.data?i.show():i.hide()})}},t=function(){var i,n=!1,r=t.prototype;r.box=function(t){return t&&(n=t),n},r.renderView=function(n){var t=new Date,i=t.DateAdd("d",1-t.getDate()).format("yyyy-MM-dd");n({beginDate:i,endDate:t.format("yyyy-MM-dd")})},r.init=function(){i=$("#dList",n).hide(),app.bindDateSelector("txtBalanceBeginDate",n),app.bindDateSelector("txtBalanceEndDate",n)},r.reset=function(){i.html("")},r.show=function(){var t={t:1,d1:$("#txtBalanceBeginDate",n).val(),d2:$("#txtBalanceEndDate",n).val()};WS.GetFinance(t,function(n){i.html(n.data),n.data?i.show():i.hide()})}};window.$_Detail=n,window.$_Balance=t}(window),function(){var n=function(){function h(){t.p=o.val(),t.CompanyWorkTime=u.val(),t.OnSetSum=s.val(),f.val()&&(t.p1=f.val()),e.val()&&(t.p2=e.val())}var t={},o,u,s,f,e,i=!1,r=n.prototype;r.box=function(n){return n&&(i=n),i},r.reset=function(){i.html("")},r.show=function(){WS.GetMyInfo(function(n){t=n.data||t,t.p=DS.UserPhone(),t.CompanyWorkTime=t.CompanyWorkTime||"",t.OnSetSum=t.OnSetSum||"",i.html(VT.HallView(t))})},r.init=function(){o=$("#txtPhone",i),u=$("#txtWorkingTime",i),s=$("#txtOnSetSum",i),f=$("#p1",i),e=$("#p2",i)},r.onClose=function(){},r.addWorkingtime=function(){var t=prompt("请输入新营业时间,格式如:10:00-11:00"),n;t&&(n=u.val(),n=n?n+","+t:t,u.val(n))},r.changeStatus=function(n){t.IsSuspend?(n.attr("src","images/on.png"),t.IsSuspend=!1):(n.attr("src","images/off.png"),t.IsSuspend=!0)},r.saveMyInfo=function(){var n=o.val(),i,r;if(!n||n.length==0){app.logInfo("你的登录账号不能为空.");return}if(i=f.val(),r=e.val(),i&&i!=r){app.logInfo("你两次输入的密码不一致.");return}h(),WS.SaveMyInfo(t,function(t){t.code>-1&&DS.UserPhone(n),app.logInfo(t.message)})}};window.$_Hall=n}(window),function(){var n=function(){function h(){r<=0?(t.val("短信获取密码"),t.removeClass("gray"),t.addClass("green")):(t.val(r+c),r=r-1,setTimeout(h,1e3))}var f=!1,e=!1,o=!1,s=!1,c="秒后可重发",t=!1,r=0,u=n.prototype,i=!1;u.box=function(n){return n&&(i=n),i},u.init=function(){f=$("#txtLoginUserPhone",i).val("13800100001"),e=$("#txtLoginUserPwd",i).val("123456"),o=$("#cbAutoLogin",i),s=$("#cbSavePwd"),DS.IsSavePwd()&&(f.val(DS.UserPhone()),e.val(DS.UserPwd()),s.attr("checked","checked")),DS.IsAutoLogin()&&o.attr("checked","checked")},u.login=function(){var t=f.val(),n;if(!t||t.length==0){app.logError("请输入你的登录账号.");return}if(n=e.val(),!n||n.length==0){app.logError("请输入你的登录密码.");return}WS.Login(t,n,function(n){n.code==0?(s.attr("checked")&&DS.IsSavePwd(!0),o.attr("checked")&&DS.IsAutoLogin(!0),app.goback(),app.showOrderList()):app.logError(n.message)})},u.sendPassword=function(){if(t=t||$(this),!t.hasClass("gray")){var n=f.val();if(!n||n.length==0){app.logError("请输入你的登录账号.");return}WS.SendPassword(n,function(n){n.code==0?(t.removeClass("green"),t.addClass("gray"),r=60,h()):app.logError(n.message)})}},u.showSendMsn=function(){$("#sendPwd",i).toggle()}};window.$_Login=n}(window),function(){var n=function(){function l(n,r){return i&&i.attr("data-id")==n?i:(i=$("#__"+n,t),i.length==0&&(i=$("<li id='__"+n+"' data-id='"+n+"'><h2>"+r+"<\/h2><\/li>"),t.append(i)),i)}function s(n){return VT.MenuItem(n)}function a(n){var f,r,e,u,o;for(t.remove(),i=!1,u=0;u<n.length;u++)r=n[u],f=l(r.dirid,r.dirname),e=f.find("#_"+r.id),o=s(r),e.length==0?f.append(o):e.replaceWith(o);c.append(t)}function h(){if(!(u>=f)){var n={q:v(),p:u+1};WS.GetMenus(n,function(n){u==0&&t.html(""),n.code>-1&&(a(n.data.menus),u++,f=n.data.pageCount)})}}function v(){var n=o.val();return n==o.attr("defVal")?"":n}var u=0,f=1,o,t,c,e,i=!1,r=n.prototype;r.box=function(n){return n&&(e=n),e},r.show=function(){h(),t.lazyload({load:h})},r.reset=function(){u=0,f=1,t.html("")},r.init=function(){o=$("#txtMenuName",e).focusin(function(){$(this).val()==$(this).attr("defVal")&&$(this).val("")}).focusout(function(){$(this).val()==""&&$(this).val($(this).attr("defVal"))}),t=$("#menuContainer",e),c=t.parent()},r.changePrice=function(n){var i=$(n),u=numeral(i.val()).round(2),f=numeral(i.attr("data-price")).round(2),r;if(u==f){app.logInfo("新价与原价相同.");return}r={cmd:"chg",v:u,id:i.attr("data-id")},WS.SaveMenus(r,function(n){n.code>-1&&t.find("#_"+r.id).replaceWith(s(n.data))})},r.outOfStockItem=function(){var n=$(this),i={cmd:"out",v:!(n.attr("data-isout")=="true"),id:n.attr("data-id")};WS.SaveMenus(i,function(n){n.code>-1&&t.find("#_"+i.id).replaceWith(s(n.data))})},r.deleteItem=function(){var i=$(this),n={cmd:"del",id:i.attr("data-id")};WS.SaveMenus(n,function(i){i.code>-1&&t.find("#_"+n.id).remove()})},r.searchMenu=function(){f=1,u=0,h()}};window.$_Menu=n}(window),function(){var n=function(){function y(){return{counter:{notstarted:numeral(0),inprogress:numeral(0),modified:numeral(0),urge:numeral(0),canceled:numeral(0),completed:numeral(0),all:numeral(0)},summary:{sum:numeral(0),sumok:numeral(0),sumcash:numeral(0),sumpoint:numeral(0)}}}function d(n,t){for(var i=0;i<n.length;i++)n[i].OrderUpdateOn>t&&(t=n[i].OrderUpdateOn);return t}function p(n,i){var f,s,r,h,e,u,o;for(t.remove(),e=0;e<n.length;e++){r=n[e],r.status=g(r),f=$(VT.OrderListItem(r));for(u in r)u&&u!="ID"&&u!="status"&&f.attr("data-"+u.toLowerCase(),r[u]);s=t.find("#_"+r.ID),s.length==0?i?t.fist().before(f):t.append(f):s.replaceWith(f)}o=t.find("li"),i&&(o.remove().sort(function(n,t){return parseInt(t.getAttribute("data-id"))-parseInt(n.getAttribute("data-id"))}),t.append(o)),v.before(t),nt(o)}function g(n){var t="",i="red";return n.OrderStatus==2?t="notstarted":n.OrderStatus==3?(t="modified",i="yellow"):n.OrderStatus==4?n.OrderDateUpload>n.OrderDate?t="urge":(t="inprogress",i="green"):n.OrderStatus==5?(t="canceled",i="dark"):n.OrderStatus==6&&(t="completed",i="dark"),{status:t,text:k[t],cls:i}}function nt(n){var t=y();n.each(function(n,i){a(i,t)}),w(t),delete t}function w(n){var t,i;for(t in n.counter)$("a[data-status='"+t+"']",e).find("i").text(n.counter[t].format("0"));for(i in n.summary)$("i[data-status='"+i+"']",o).text(n.summary[i].format("0.00"))}function a(n,t){var i=n.getAttribute("data-status");t.counter[i]!=undefined&&t.counter[i].add(1),t.counter.all.add(1),t.summary.sum.add(n.getAttribute("data-ordersum")),t.summary.sumok.add(n.getAttribute("data-ordersumok")),t.summary.sumcash.add(n.getAttribute("data-orderpaycash")),t.summary.sumpoint.add(n.getAttribute("data-orderpoint"))}function tt(){var i=$(this).attr("data-status"),n=y();i=="all"?(e.hide(),o.show(),$("li",t).show().each(function(t,i){a(i,n)})):t.find("li").each(function(t,r){r.getAttribute("data-status")==i?(r.style.display="list-item",a(r,n)):r.style.display="none"}),w(n),delete n}function c(n,t){if(f){t&&t();return}if(n||!(s>=l)){var i={d1:$("#txtOrderListBeginDate",r).val(),d2:$("#txtOrderListEndDate",r).val(),c:DS.CompanyID(),p:s+1,q:it()};n&&(i.t=DS.LatestUpdateOn()),f=!0,WS.GetOrders(i,function(r){r.code>-1?(s!=0||app.psInited()||(DS.LatestUpdateOn(d(r.data.orders,i.d1)),app.isSocket()?app.initPS():h==0&&(h=1,b())),r.data.orders.length>0&&(p(r.data.orders,n),f=!1,n||(s++,l=r.data.pageCount)),t&&t()):app.logInfo(r.message)})}}function b(){h==1&&setTimeout(function(){c(!0,b)},5e3)}function it(){return val=i.val(),val==i.attr("defVal")?"":val}var t,v,i,r=!1,e,o,s=0,l=1,f=!1,h=0,k={notstarted:"待处理",inprogress:"处理中",modified:"已修改",urge:"催餐中",canceled:"已取消",completed:"订餐成功"},u=n.prototype;u.box=function(n){return n&&(r=n),r},u.reset=function(){l=1,s=0,f=!1,h=0,t.html("")},u.show=function(){$(window).lazyload({load:c}),c(!1)},u.init=function(){app.bindDateSelector("txtOrderListBeginDate",r),app.bindDateSelector("txtOrderListEndDate",r),t=$("#orderContainer",r),v=$("#state",r),e=$("#s1"),o=$("#s2"),$("a",e).tap(tt),$("span",o).tap(function(){e.show(),o.hide()}),i=$("#txtOrderListUserPhone",r).focusin(function(){val=i.val(),val==i.attr("defVal")&&i.val("")}).focusout(function(){val=i.val(),val&&val.length!=0||i.val(i.attr("defVal"))}),i.val(i.attr("defVal"))},u.onPushOrder=function(n){n.data.length>0&&(f=!0,DS.LatestUpdateOn(n.data,DS.LatestUpdateOn()),p(n.data,!0),f=!1,n.hasNew&&r.css("display")=="none"&&app.notify("你有新的订单需要处理.","乐多分"))},u.renderView=function(n){var t=(new Date).format("yyyy-MM-dd");n({beginDate:t,endDate:t})},u.showTempView=function(n){app.showTemp(n.attr("data-id"))},u.showHandleView=function(n){app.showOrderHandle(n.attr("data-id"))},u.showOrderList=function(){u.reset(),c(!1)}},t=function(){function a(){var n,t=numeral(0),r=0,f,e;for(e in u)n=u[e],f=n.NewPrice||n.OrderPrice,n.IsCompanyItem&&r==0&&(r=n.OrderSum),t.add((n.IsCompanyItem?n.OrderQty-1:n.OrderQty)*f);i.OrderSumOk=t.add(r).round(2),delete t}function v(n,t){if(t){var i=numeral(t.NewPrice);i.valueOf()>=0?e[n]=t.MenuName+"价格调整为"+i.format("0.00")+"元":e[n]&&delete e[n],a(),h(),delete i}}function y(n,t){if(t){var i=t.IsOutOfStock;i?o[n]=t.MenuName:o[n]&&delete o[n],h()}}function h(){var f=c(),t=l(),e=$('input[value="1"]',r).attr("data-message",""),n="",u,o;f.length>0&&(u=numeral(i.OrderSumOk).add(i.ServiceSum),n="经餐厅确认："+f.join("，")+"，您的订单总计为"+u.format("0.00")+"元。",e.eq(1).attr("data-message",n).siblings("span").html(n),delete u),t.length>0&&(n="很抱歉，"+t.join("，")+"今天暂缺，请修改后重新下单。",o="很抱歉，"+i.CompanyName+"表示，"+t.join("，")+"今天暂缺，请修改后重新下单。",e.eq(0).attr("data-message",o).siblings("span").html(n))}function c(){var n=[],t;for(t in e)n.push(e[t]);return n}function l(){var n=[],t;for(t in o)n.push(o[t]);return n}var r=!1,f=!1,u={},i={},n=t.prototype,e={},o={},s=!1;n.box=function(n){return n&&(f=n),f},n.changePrice=function(n){n=$(n);var i=n.attr("data-id"),f=parseFloat(n.val())||0,t=u[i];t&&(t.NewPrice=f,v(i,t),$('input[value="1"]',r).eq(1).attr("checked",!0))},n.outOfStock=function(n){var i=n.attr("data-id"),f=n.hasClass("green"),t=u[i];t&&(f?(t.IsOutOfStock=!0,n.addClass("red").removeClass("green")):(t.IsOutOfStock=!1,n.addClass("green").removeClass("red")),y(i,t),$('input[value="1"]',r).eq(0).attr("checked",!0))},n.selectMsn=function(n){n.find("input").attr("checked",!0)},n.init=function(){},n.reset=function(){f.html("")},n.show=function(t){n.reset(),WS.OrderDetail(t,function(n){n.code==0?(i=n.data,f.html(VT.OrderHandleView(i)),r=$("#reviewContainer",f)):app.logError(n.message)})},n.processItem=function(n){u[n.MenuId.toString()]=n},n.confirmOrder=function(){var t,f,n,e;if(s){app.logInfo("正在发送,请稍等...");return}if(t=$('input[name="msnType"]:checked',r),f=t.val(),f=="1"&&c().length==0&&l().length==0){app.logInfo("你还没有调整价信息或缺货信息.");return}if(n=t.attr("data-message"),!n||n.length==0){app.logInfo("请输入回复内容.");return}e={orderId:i.OrderId,orderSessionVal:i.orderSessionVal,msnType:f,message:n,orders:JSON.stringify(u)},s=!0,WS.ConfirmOrder(e,function(n){app.logInfo(n.message),n.code==0&&(i.orderSessionVal=n.data.orderSessionVal),s=!1})},n.toggleReview=function(){r.toggle()}},i=function(){function a(t){var i,r;u&&(i=new FileUploadOptions,i.fileKey="voice",i.fileName=o,i.mimeType="media/mp3",i.params={id:s,m:t},r=new FileTransfer,r.upload(e+o,encodeURI(WS.getUrl("SendOrderTemps")),function(t){u=!1;var i=JSON.parse(t.responseText);i.code>-1&&(h(i.data),$("#txtMessage",n).val(""))},function(n){app.logError(n.source)},i))}function c(t){u?a(t):WS.callServices("SendOrderTemps",{m:t,id:s},function(t){t.code<0?app.logError(t.message):(u=!1,h(t.data),$("#txtMessage",n).val(""))})}function h(n){var t=f.find("#_"+n.id);t.length>0?t.replaceWith(VT.TempItem(n)):f.append(VT.TempItem(n))}function v(n){for(var t=0;t<n.length;t++)h(n[t])}function l(n){if(!app.isCordovaApp()&&n){app.logError("暂不支持此操作.");return}n?e?t||(t=new Media(e+o,function(){console.log("recordAudio():Audio Success")},function(n){console.log("recordAudio():Audio Error: "+n.code)}),t.startRecord()):window.requestFileSystem(LocalFileSystem.PERSISTENT,0,function(n){e=n.root.fullPath+"/"},function(n){app.logError("获取系统目录失败:"+n.target.error.code)}):t&&(t.stopRecord(),u=t.getDuration()>=2,t.release(),t=!1)}function y(t){s=t,WS.callServices("GetOrderTemps",{id:t},function(t){t.code==0&&(v(t.data.temps),$("#phone",n).text(t.data.MemberPhoneNumber),$("#timespan",n).text(t.data.Timespan))})}var r=i.prototype,n=!1,f=!1,t=!1,o="voice.wav",e=!1,u=!1,s=0;r.box=function(t){return t&&(n=t),n},r.show=function(n){u=!1,y(n)},r.reset=function(){u=!1,f.html("")},r.init=function(){f=$("#tempContainer",n),$("#recordVoice",n).bind("touchstart",function(){l(!0)}).bind("touchend",function(){l(!1)})},r.playVoice=function(n){app.play(n.attr("voice"))},r.sendMessage=function(){var t=$("#txtMessage",n).val();t&&t.length>0?c(t):$("#temp_review",n).toggle()},r.quickSend=function(t){c(t.text()),$("#temp_review",n).toggle()}};window.$_OrderList=n,window.$_OrderHandle=t,window.$_Temp=i}(window),function(){var n=function(){function o(){var n={t:4,b:DS.CompanyID(),i:f+1,d1:$("#txtBeginReviewDate",r).val(),d2:$("#txtEndReviewDate",r).val()};WS.FacebookQuery(n,function(n){var o;if(n.code>-1){if(u.eq(0).text(n.data.good),u.eq(1).text(n.data.normal),u.eq(2).text(n.data.bad),n.data.html.length==0&&f==0)t.html("");else{var c=n.data.html.length/2,r=0,h=0,e="",i;for(t.remove(),o=0;o<c;o++)h=n.data.html[r],e=n.data.html[r+1],i=t.find("#item"+h),i.length>0?i.replaceWith(e):(i=$(e),t.append(i)),r=r+2;s.append(t)}f++}})}var t=!1,s,e=!1,u=!1,h=!1,r,f=0,i=n.prototype;i.box=function(n){return n&&(r=n),r},i.reset=function(){f=0,t.html("")},i.show=function(){o(),t.lazyload({load:o})},i.init=function(){t=$("#reviewList",r),s=t.parent(),u=$("#rw_number > i",r),app.bindDateSelector("txtBeginReviewDate",r),app.bindDateSelector("txtEndReviewDate",r)},i.renderView=function(n){var t=new Date,i=t.DateAdd("d",1-t.getDate()).format("yyyy-MM-dd");n({beginDate:i,endDate:t.format("yyyy-MM-dd")})},i.showReview=function(){o()},i.filterReview=function(n){var u=n.attr("data-type"),o=n.text(),f,r,i;if(u=="All")t.find("li").show();else for(f=t.find("li"),r=0;r<f.length;r++)i=f.eq(r),i.attr("data-reply")==u?i.show():i.hide();n.attr("data-type",e.attr("data-type")).text(e.text()),e.attr("data-type",u).text(o).trigger("tap")},i.showReplyBox=function(n){$("#box"+n.attr("data-id"),t).toggle()},i.replyFaceBook=function(n){var i=$("#txtbox"+n.attr("data-id"),t).val(),r;if(!i||i.length==0){app.logInfo("请输入回复内容.");return}r={fbID:n.attr("data-id"),content:encodeURIComponent(i)},WS.ReplyFacebook(r,function(n){if(n.code>=0){var u=n.data.replace("[0]",i);$("#item"+r.fbID,t).append(u).find(".rw4,.rw5").remove(),app.logInfo("回复成功.")}else app.logInfo(n.message)})}};window.$_Review=n}(window),function(){var n=function(){function h(){var u=(new Date).format("yyyy-MM-dd"),o=new Date(u).DateAdd("d",7).format("yyyy-MM-dd"),n=[],i=numeral(0),r,e;return $.each(f,function(u,f){r=t.find("#_"+f),n.push(unescape(r.attr("data-name"))),i.add(r.attr("data-price"))}),e={ItemTitle:n.join("+"),ItemPoint:null,ItemNeedPay:null,ItemAmount:1,OrderFreqLimit:null,OrderSumLimit:null,ItemDate:u,ItemEndDate:o,WorkingHours:null,ItemLimit:3,ItemPic:null,ItemSum:i.value(),CompanyID:null,ItemInfo:JSON.stringify(f),ItemStatus:1,ItemID:0},delete n,delete i,delete items,e}function l(n,r){return i&&i.attr("data-id")==n?i:(i=$("#__"+n,t),i.length==0&&(i=$("<li id='__"+n+"' data-id='"+n+'\'><h2 style="color: #333; cursor: pointer;" tap="toggleMenus">'+r+"<\/h2><\/li>"),t.append(i)),i)}function a(n){return VT.SaleItem(n)}function v(n){var f,r,e,u,s;for(i=!1,t.remove(),u=0;u<n.length;u++)r=n[u],f=l(r.dirid,r.dirname),e=f.find("#_"+r.id),s=a(r),e.length==0?f.append(s):e.replaceWith(s);o.append(t)}function c(){if(!(r>=e)){var n={p:r+1};WS.GetMenus(n,function(n){r==0&&t.html(""),n.code>-1&&(v(n.data.menus),r++,e=n.data.pageCount)})}}var r=0,e=1,t,o,s=!1,i=!1,f,u=n.prototype;u.box=function(n){return n!==undefined&&(s=n),s},u.reset=function(){r=0,e=1,t.html("")},u.show=function(n){n&&(r=0,e=1),f=[],t.find("input.red").removeClass("red").addClass("green"),r==0&&c(),t.lazyload({load:c})},u.init=function(){t=$("#saleContainer"),o=t.parent()},u.toggleSale=function(n){n.hasClass("red")?app.confirm("还需要继续选择菜单吗?","促销方案","继续选择,下一步",function(t){t=="1"?(Array.remove(f,parseInt(n.attr("data-id"))),n.removeClass("red").addClass("green")):app.showRush(h())}):(f.push(parseInt(n.attr("data-id"))),n.removeClass("green").addClass("red"),app.confirm("还需要继续选择菜单吗?","促销方案","继续选择,下一步",function(n){n!="1"&&app.showRush(h())}))},u.toggleMenu=function(n){n.siblings().toggle()}},i=function(){function h(n){s=n,u.attr("src","data:image/jpeg;base64,"+n),u.show()}function c(){var i,r;return(e.each(function(u,f){i=f.value,r=f.getAttribute("t"),t=="dec"?i=parseFloat(i)||0:t=="int"&&(i=parseInt(i)||0),n[f.id]=i}),n.ItemLimit=parseInt(o.find(":checked").val()),n.ItemSum<n.ItemNeedPay)?(app.showtips("现金支付额不能大于促销总额."),!1):n.ItemPoint>=.5&&n.ItemPoint<=1.5?!0:(app.showtips("抢购积分必须在0.5-1.5之间."),!1)}var n=!1,r=!1,u,e,o,s=!1,f=i.prototype;f.box=function(n){return n&&(r=n),r},f.init=function(){u=$("#ItemPic",r),e=$("input .input",r),o=$("input[name='ItemLimit']",r),app.bindDateSelector("ItemDate",r),app.bindDateSelector("ItemEndDate",r)},f.show=function(i){n=i;var r,f;e.each(function(i,u){r=n[u.id],f=u.getAttribute("t"),r&&t=="dec"&&(r=numeral(r).format("0.00")),u.value=r||""}),r=n.ItemLimit,o.find("[value='"+r+"']").attr("checked",!0),r=n.ItemPic,s=!1,r?(u.attr("src",r),u.show()):u.hide()},f.takeaPic=function(){if(!app.isCordovaApp()){app.showtips("暂不支持此功能.");return}navigator.camera.getPicture(h,function(){},{quality:30,destinationType:Camera.DestinationType.DATA_URL})},f.saveItem=function(){if(c()){var t={item:JSON.stringifty(n),img:s||""};WS.SaveItem(t,function(n){app.showtips(n.message)})}}},r=function(){function s(t){var r,i;for(n.remove(),i=0;i<t.length;i++)r=n.find("#_"+t[i].ItemID),r.length==0?n.append(VT.SaleListItem(t[i])):n.replaceWith(VT.SaleListItem(t[i]));o.append(n)}function e(){if(!(i>=f)){var t={d1:$("#txtSaleListBeginDate",u).val(),d2:$("#txtSaleListEndDate",u).val(),p:i+1};WS.GetItems(t,function(t){t.code>-1&&(i==0&&n.html(""),s(t.data.items),i=i+1,f=t.data.pageCount)})}}var i=0,f=1,n,o,u=!1,t=r.prototype;t.box=function(n){return n&&(u=n),u},t.reset=function(){i=0,f=0,n.html("")},t.renderView=function(n){var t=new Date,i=t.DateAdd("d",1-t.getDate()).format("yyyy-MM-dd"),r=t.DateAdd("m",1).format("yyyy-MM-dd");n({beginDate:i,endDate:r})},t.init=function(){n=$("#saleList",u),o=n.parent(),app.bindDateSelector("txtSaleListBeginDate",u),app.bindDateSelector("txtSaleListEndDate",u)},t.show=function(){t.reset(),e(),n.lazyload({load:e})},t.showSaleList=function(){f=1,i=0,e()},t.showRush=function(n){WS.GetItem(n.attr("data-id"),function(n){n.code>-1?app.showRush(n.data):app.showtips(n.message)})},t.delItem=function(t){WS.delItem({id:t.attr("data-id")},function(i){i.code>-1&&n.find("#_"+t.attr("data-id")).remove(),app.showtips(i.message)})}},u=function(){function a(t){var i,r,u;for(n.remove(),u=0;u<t.length;u++)i=t[u],r=n.find("#_"+i.ItemID),r.length>0?(e.subtract(r.attr("data-qty")).add(1),o.subtract(r.attr("data-point")).add(i.ItemPoint),r.replaceWith(VT.RushRecordItem(i))):(e.add(i.ItemQty),o.add(i.PointSum),n.append(VT.RushRecordItem(i)));h.before(n),c.text(e.format("0")),l.text(o.format("0.00"))}function s(){if(!(r>=f)){var i={d1:$("#txtRushRecordBeginDate",t).val(),d2:$("#txtRushRecordEndDate",t).val(),p:r+1};WS.GetRushItems(i,function(t){t.code>-1&&(r==0&&n.html(""),a(t.data.items),r=r+1,f=t.data.pageCount)})}}var r=0,f=1,n,h,c,l,t=!1,e=numeral(0),o=numeral(0),i=u.prototype;i.box=function(n){return n&&(t=n),t},i.reset=function(){n.html(""),f=1,r=0},i.renderView=function(n){var t=new Date,i=t.DateAdd("d",1-t.getDate()).format("yyyy-MM-dd");n({beginDate:i,endDate:t.format("yyyy-MM-dd")})},i.init=function(){n=$("#rushList",t),h=n.siblings(),app.bindDateSelector("txtRushRecordBeginDate",t),app.bindDateSelector("txtRushRecordEndDate",t),c=$("#amountSum",t),l=$("#pointSum",t)},i.show=function(){i.reset(),s(),n.lazyload({load:s})},i.showRushList=function(){s()}};window.$_Sale=n,window.$_Rush=i,window.$_SaleList=r,window.$_RushRecord=u}(window)