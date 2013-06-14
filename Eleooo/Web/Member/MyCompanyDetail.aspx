<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/EleoooMemberMasterV2.master"
    AutoEventWireup="true" CodeBehind="MyCompanyDetail.aspx.cs" Inherits="Eleooo.Web.Member.MyCompanyDetail" %>

<%@ Import Namespace="Eleooo.Web" %>
<%@ Register Assembly="Eleooo.Web" Namespace="Eleooo.Web.Controls" TagPrefix="ele" %>
<asp:Content ContentPlaceHolderID="dlgSupport" runat="server" ID="Content0">
    <ele:ResLink ID="ResLink3" runat="server" Src="/App_Themes/ThemesV2/css/inc.css" />
    <ele:ResLink ID="ResLink1" runat="server" Src="/App_Themes/ThemesV2/style/public.css" />
    <ele:ResLink ID="ResLink2" runat="server" Src="/App_Themes/ThemesV2/style/admin.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="sqsortDetail" style="margin-top: 15px;">
        <div class="con">
            <div class="posDiv">
                <a href="javascript:void(0)" onclick="openItemLink('<%=this.LastItemID %>');">
                    <img alt="" align="absmiddle" src="/App_Themes/ThemesV2/images/zxyh.png" /><span class="s1">最新优惠</span></a>
                <a href="#faceBook">
                    <img alt="" align="absmiddle" src="/App_Themes/ThemesV2/images/wydp.png" /><span class="s1"><%=this.view.TotalRecord %>封点评</span></a>
            </div>
            <h1>
                <a href="#">
                    <%=this.Company.CompanyName %></a>
            </h1>
            <ul class="shop_Item">
                <li><span>电话：</span><div>
                    <%=this.Company.CompanyPhone %></div>
                </li>
                <li><span>地址：</span><div>
                    <%=this.Company.CompanyAddress %></div>
                </li>
                <li><span>特色：</span><div>
                    <%=this.Company.CompanyServices %></div>
                </li>
                <li><span>线路：</span><div>
                    <%=this.Company.CompanyIntro %>
                </div>
                </li>
            </ul>
        </div>
        <div class="dimg">
            <img alt="" src="<%=Eleooo.Common.FileUpload.GetFilePath(Company.CompanyPhoto, SaveType.Company) %>" />
<%--            <div class="dBtn">
                <input type="button" name="" value="最新优惠" class="sqBtn fl" onclick="openItemLink('<%=this.LastItemID %>');" />
                <input type="button" name="" value="看看广告" class="sqBtn fr" onclick="openAdsLink('<%=this.CompanyID %>','<%=this.LastAdsID %>');" />
            </div>--%>
        </div>
    </div>
    <div>
        <h1 class="sjTil">
            商家详情 <span></span>
        </h1>
        <div class="sjDetail">
            <img alt="" src="<%=this.GetDetailCompnayPic() %>" class="fr ml_20" width="216px"
                height="161px" />
            <p>
                <%=this.CompanyContent %>
            </p>
        </div>
        <h1 class="sjTil">
            实景图片 <span></span>
        </h1>
        <div class="sjImg">
            <div class="carousel default">
                <a class="prev" href="javascript:">
                    <img src="/App_Themes/Member/images/prov.jpg" alt="" />
                </a>
                <div style="width: 900px;left:43px;" class="jCarouselLite" id="sjImgContainer" runat="server">
                </div>
                <a class="next" href="javascript:">
                    <img alt="right" src="/App_Themes/Member/images/next.jpg" alt="" />
                </a>
            </div>
        </div>
        <h1 class="sjTil">
            最新评论 <span></span>
        </h1>
        <a name="faceBook"></a>
        <div class="sjpl">
            <ele:DataListExt ID="view" runat="server" AllowPaging="true" ShowFootPaging="true"
                ShowHeadPaging="false" EmptyDataIsShowHeaderAndFooterTemplate="true">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <FooterTemplate>
                    </ul></FooterTemplate>
                <ItemTemplate>
                    <li>
                        <div>
                            <span class="c_fb7c04">会员：<%#Eval("MemberPhoneNumber",0) %></span>
                        </div>
                        <p>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <%#Eval("FaceBookMemo",1) %>
                        </p>
                        <div style="text-align: right;">
                            <span class="fr c_0896ae">
                                <%#Eval("FaceBookDate","{0:yyyy年MM月dd日 HH:mm}")%></span></div>
                    </li>
                </ItemTemplate>
            </ele:DataListExt>
            <div class="sjdp">
                <h3>
                    我要点评</h3>
                <textarea class="txt_dp" name="txtFaceBook" rows="2" cols="2"></textarea>
                <span class="c_0896ae txt_dp_tip">50-1000字</span>
                <div>
                    <input type="button" class="sjplsubmit" value="提 交" onclick="__doPostBack('Add');" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBottomScript"
    runat="server">
    <script type="text/javascript">
        var openItemLink = function (itemId) {
            if (itemId && itemId != '')
                window.location = '/Public/ViewItemDetail.aspx?ItemID=' + itemId;
            else
                alert("该商家暂无优惠提供");
        }
        var openAdsLink = function (companyID, adsID) {
            if (adsID && adsID != '')
                window.location = '/Public/ViewAdsList.aspx?CompanyID=' + companyID;
            else
                alert("该商家暂无广告浏览");
        }
    </script>
    <script src="/Scripts/jcarousellite_1.0.1.js" type="text/javascript"></script>
    <!--[if IE 6]>
<script type="text/javascript" src="script/DD_belatedPNG.js"></script>
<script language="javascript" type="text/javascript">
               DD_belatedPNG.fix(".todayTopDetailL,h5");
</script>
<![endif]-->
    <script type="text/javascript">
        $(document).ready(function () {
            if ($(".jCarouselLite img").length > 0) {
                $(".default .jCarouselLite").jCarouselLite({
                    btnNext: ".default .next",
                    btnPrev: ".default .prev",
                    visible: 4
                });
            }
        });
    </script>
</asp:Content>
