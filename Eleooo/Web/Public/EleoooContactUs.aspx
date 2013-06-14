<%@ Page Title="乐多分-联系我们" Language="C#" MasterPageFile="~/MasterPage/PublicMaster.Master"
    AutoEventWireup="true" CodeBehind="EleoooContactUs.aspx.cs" Inherits="Eleooo.Web.Public.EleoooContactUs" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <div class="title">
        <span>联系我们</span>
        <uc:ucpageposition id="pagePos" runat="server" />
    </div>
    <br />
    <div class="nr">
        <p>
            <h1>
                深圳乐多分科技发展有限公司
            </h1>
        </p>
        <p>
            公司地址： 深圳市福田区深南中路2018号兴华大厦东座10楼</p>
        <p>
            电话号码： 0755－22257739</p>
        <p>
            电子邮箱： hz@eleooo.com</p>
        <p>
            官方网站： www.eleooo.com</p>
        <p>
            <iframe src="http://j.map.baidu.com/AiXTc" width="700" height="500" frameborder="0">
            </iframe>
        </p>
    </div>
</asp:Content>
