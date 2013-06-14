<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMasterV2.master"
    AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="Eleooo.Web.Public.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <link href="/App_Themes/ThemesV2/css/sub.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <!--内容开始-->
    <div class="sub_con">
        <div class="left" style="width: 690px;" id="s1" runat="server" visible="false">
            <div class="mainmenu">
                <ul>
                    <li id="h1" content="content1" onclick="showHelp('h1');"><a href="javascript:void(0)">
                        怎么订快餐</a></li>
                    <li id="h2" content="content2" onclick="showHelp('h2');"><a href="javascript:void(0)">
                        如何抢优惠</a></li>
                    <li id="h3" content="content3" onclick="showHelp('h3');"><a href="javascript:void(0)">
                        怎么赚积分</a></li>
                </ul>
            </div>
            <div class="help" id="content1" style="display: none;">
                <p>
                    根据您所处的位置，搜索附近的餐厅，选择您喜欢的餐点，无需注册，即可下单。</p>
                <p>
                    成为会员后，系统将自动记录您的信息，让订餐更加方便。</p>
                <p>
                    如果您更换了送餐地址，系统也会自动为您保存，您总共可以保存3个常用送餐地址。</p>
                <p>
                    成功下单后，您可以及时修改或取消订单。而且，送餐进度会实时更新，让您一目了然。您也可以点击催餐按钮，餐厅会第一时间响应。
                </p>
            </div>
            <div class="help" id="content2" style="display: none;">
                <p>
                    只要您有积分，即可抢购心仪的各种优惠，范围涵盖了日常生活中的各个领域。所有优惠项目均经过乐多分严格审核，绝无任何隐性消费，您可以放心到店体验。</p>
                <p>
                    抢购成功后，您可以根据自己的时间安排，选择预计到店时间，省去了电话预约的麻烦。当然，您也可以对预计到店时间进行修改（限一次）。</p>
                <p>
                    乐多分提供的优惠项目不同于团购。无需预付，更放心；不用预约，更省心。您的消费体验不仅不会有丝毫打折，反而会享受到更加贴心的服务。</p>
            </div>
            <div class="help" id="content3" style="display: none;">
                <p>
                    订快餐，送积分--您的每一笔外卖订单均可获得餐点金额3%的积分返馈。</p>
                <p>
                    到店消费，拿积分--凡已开通积分服务的乐多分合作商家，您的每一笔消费，均可获得积分返馈，具体返馈比例以商家公布的为准。</p>
                <p>
                    看广告，赚积分--您可以通过浏览广告，获得广告主给予的积分奖励。您的个人信息越全面，过往累积消费额度越高，看广告的权限越大。因此，您能够浏览广告的次数也越多，并且浏览后奖励的积分更高。</p>
            </div>
        </div>
        <div class="left" style="width: 690px;" id="s2" runat="server" visible="false">
            <div class="mainmenu">
                <ul>
                    <li id="h1" content="content1" onclick="showHelp('h1');"><a href="javascript:void(0)">
                        什么是联盟商家</a></li>
                    <li id="h2" content="content2" onclick="showHelp('h2');"><a href="javascript:void(0)">
                        商家推广</a></li>
                    <li id="h3" content="content3" onclick="showHelp('h3');"><a href="javascript:void(0)">
                        合作流程</a></li>
                </ul>
            </div>
            <div class="help" id="content1" style="display: none">
                <p>
                    乐多分联盟商家即为同属于一个商圈内，具备诚信经营和良好口碑的各行业优质商家。</p>
                <p>
                    联盟商家可根据自身经营情况，为乐多分会员提供各类高品质的优惠项目，以便于会员通过积分抢购的方式到店体验。</p>
                <p>
                    联盟商家须对乐多分会员的每一笔消费提供相应的折扣优惠或积分返馈，并接受所有会员进行积分抵现消费。</p>
            </div>
            <div class="help" id="content2" style="display: none">
                <p>
                    店面服务你做主，营销推广交给我。</p>
                <p>
                    乐多分旨在充分满足广大商家在提高客户忠诚度，扩大潜在客户群体，以及低成本营销推广等方面的实际需求。采取消费积分通用、商圈联合促销、消费行为分析，以及定向广告投放等手段，实现消费者与商家之间真正的良性互动，让物美价廉和薄利多销有机结合，达到多方共赢的目的。
                </p>
                <p>
                    如果您是本地化生活服务类优质商家，请与我们联络：hz@eleooo.com</p>
            </div>
            <div class="help" id="content3" style="display: none">
                <p>
                    乐多分的运营宗旨是为会员提供高品质的消费导引，为商家提供最具价值的客户资源。</p>
                <p>
                    乐多分对合作商家的经营资质和优惠方案均进行严格审核，以确保会员获得物超所值的消费体验。</p>
                    <img src="../App_Themes/ThemesV2/images/h001.jpg" alt="合作流程" />
            </div>
        </div>
        <div class="left" style="width: 690px;" id="s3" runat="server" visible="false">
            <div class="mainmenu">
                <ul>
                    <li id="h1" content="content1" onclick="showHelp('h1');"><a href="javascript:void(0)">
                        关于乐多分</a></li>
                    <li id="h2" content="content2" onclick="showHelp('h2');"><a href="javascript:void(0)">
                        10分钟消费圈</a></li>
                    <li id="h3" content="content3" onclick="showHelp('h3');"><a href="javascript:void(0)">
                        加入我们</a></li>
                </ul>
            </div>
            <div class="help" id="content1" style="display: none">
                <p>
                    乐多分是国内领先的O2O生活消费平台，专注于城市消费领域，服务于本地优质商家和消费者。以消费积分为载体，打造10分钟消费圈，为用户提供最便捷的消费体验和最精准、靠谱的优惠信息，彻底颠覆并重新定义积分价值。</p>
                <p>
                    乐多分的终极目标是：汇聚最具价值的消费者和商家资源，打通信息交互与资金流通管道，构建与都市主流人群日常生活息息相关的消费资产增值应用平台。</p>
            </div>
            <div class="help" id="content2" style="display: none">
                <p>
                    在步行10分钟的覆盖半径内，从各行业中筛选一定数量的优质商家，结成非竞争性联盟商圈，共同服务于本商圈内所有潜在消费者。</p>
                <p>
                    通过发行同一种消费积分使各商家之间实现互联互通，为会员提供精准、便捷、可靠、实惠的消费体验。
                </p>
            </div>
            <div class="help" id="content3" style="display: none">
                <p>
                    乐多分是一支笃定、务实、创新和有企图的团队。为用户创造价值，是我们的主张和信仰，并为此矢志不移。我们相信互联网时代消费者行为习惯的改变，将彻底颠覆传统本地化生活服务类商家的经营方向，也将全方位影响消费者与商家之间的价值传递路径。</p>
                <p>
                    我们坚信：物美价廉和薄利多销并非一对矛盾体，真正的价值在于双方的良性互动。</p>
                <p>
                    我们为此而努力！</p>
            </div>
        </div>
        <div class="right">
            <div class="r_p">
                <a href="#">
                    <img src="/App_Themes/ThemesV2/images/eloo_01.jpg" alt="" /></a></div>
            <div class="r_p">
                <a href="#">
                    <img src="/App_Themes/ThemesV2/images/eloo_02.jpg" alt="" /></a></div>
        </div>
    </div>
    <!--内容结束-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBottom" runat="server">
    <script type="text/javascript" language="javascript">
        var currentNode;
        function showHelp(h) {
            if (currentNode && currentNode.attr("id") == h)
                return;
            //hidden old
            if (currentNode) {
                currentNode.removeClass('current');
                $("#" + currentNode.attr("content")).hide();
            }
            var node;
            if (h)
                node = $("#" + h);
            if (!node || node.length == 0)
                node = $("#h1");
            node.addClass("current");
            $("#" + node.attr("content")).show();
            currentNode = node;
        }
        $(document).ready(function () {
            showHelp('<%=Request["c"] %>');
        });
    </script>
</asp:Content>
