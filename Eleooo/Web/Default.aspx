<%@ Page Title="" Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Eleooo.Web" %>

<script runat="server" language="c#">
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.UrlReferrer == null && AppContext.Context.User.Id > 0)
            //{
            //    Eleooo.Common.Utilities.LoginOutSigOut( );
            //    Response.Redirect("/Default.aspx");
            //}

            if (Utilities.Compare(Request.Params["Action"], "Logout"))
            {
                Utilities.LoginOutSigOut( );
                Response.Redirect("/", true);
                return;
            }
        }
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=ResBLL.Res["page_title"]%></title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <meta content="IE=7.0000" http-equiv="X-UA-Compatible" />
    <meta name="GENERATOR" content="MSHTML 8.00.7600.16700" />
    <meta name="keywords" content="<%=ResBLL.Res["meta_keywords"] %>" />
    <meta name="Description" content="<%=ResBLL.Res["meta_Description"] %>" />
    <link rel="Shortcut Icon" href="/favicon.ico" />
    <link rel="Bookmark" href="/favicon.ico" />
    <link href="/App_Themes/ThemesV2/css/inc.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <!--头部开始-->
    <div class="yd-logo">
        <img src="/App_Themes/ThemesV2/images/yd-logo.png" alt="" /></div>
    <div class="yd-san_bg">
        <div class="yd-san">
            <div class="qyh">
                <a href="Public/ViewItemList.aspx">
                    <img src="/App_Themes/ThemesV2/images/no.png" width="264" height="209" border="0"
                        alt="" /></a></div>
            <div class="dkc">
                <a href="Public/OrderMealPage.aspx">
                    <img src="/App_Themes/ThemesV2/images/no.png" width="287" height="197" border="0"
                        alt="" /></a></div>
            <div class="zjf">
                <a href="Public/ViewAdsList.aspx">
                    <img src="/App_Themes/ThemesV2/images/no.png" width="272" height="203" border="0"
                        alt="" /></a></div>
            <div class="dc">
                <a href="Public/OrderMealPage.aspx">
                    <img src="/App_Themes/ThemesV2/images/dc.png" width="373" height="246" border="0"
                        alt="" /></a></div>
        </div>
    </div>
    <!--头部结束-->
    <!--版权开始-->
    <div class="bottom">
        <div class="bottom-lj_bg">
            <div class="bottom-lj">
                <div class="dbw">
                    <h3>
                        用户指南</h3>
                    <ul>
                        <li><a href="/Public/Help.aspx?h=s1&c=h1">怎么订快餐</a> </li>
                        <li><a href="/Public/Help.aspx?h=s1&c=h2">如何抢优惠</a> </li>
                        <li><a href="/Public/Help.aspx?h=s1&c=h3">怎么赚积分</a> </li>
                    </ul>
                </div>
                <div class="dbw1">
                    <h3>
                        商务合作</h3>
                    <ul>
                        <li style="display: none"><a href="/Public/Help.aspx?h=s2&c=h1">什么是联盟商家</a> </li>
                        <li><a href="/Public/Help.aspx?h=s2&c=h2">商家推广</a> </li>
                        <li><a href="/Public/Help.aspx?h=s2&c=h3">合作流程</a> </li>
                        <li><a href="/Public/Login.aspx?subsys=Company">商家登录</a></li>
                    </ul>
                </div>
                <div class="dbw2">
                    <h3>
                        公司信息</h3>
                    <ul>
                        <li><a href="/Public/Help.aspx?h=s3&c=h1">关于乐多分</a> </li>
                        <li><a href="/Public/Help.aspx?h=s3&c=h2">10分钟消费圈</a> </li>
                        <li><a href="/Public/Help.aspx?h=s3&c=h3">加入我们</a> </li>
                    </ul>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="bottom-w_bg">
            <div class="bottom-w">
                <p>
                    Copyright &copy;2012 乐多分 版权所有 All Rights Reserved. 粤ICP备12036243号</p>
                <!-- Baidu Button BEGIN -->
                <div id="bdshare" class="bdshare_t bds_tools get-codes-bdshare" style="float: right;
                    padding-top: 10px;">
                    <a class="bds_qzone"></a><a class="bds_tsina"></a><a class="bds_tqq"></a><a class="bds_renren">
                    </a><span class="bds_more" style="color: #fff;">更多</span> <a class="shareCount">
                    </a>
                </div>
                <script type="text/javascript" id="bdshare_js" data="type=tools"></script>
                <script type="text/javascript" id="bdshell_js"></script>
                <script type="text/javascript">
                    document.getElementById("bdshell_js").src = "http://bdimg.share.baidu.com/static/js/shell_v2.js?cdnversion=" + new Date().getHours();
                </script>
                <!-- Baidu Button END -->
            </div>
        </div>
    </div>
    <!--版权结束-->
</body>
</html>
