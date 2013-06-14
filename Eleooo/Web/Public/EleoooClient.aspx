<%@ Page Title="乐多分客户端程序" Language="C#" MasterPageFile="~/MasterPage/PublicMaster.Master"
    AutoEventWireup="true" CodeBehind="EleoooClient.aspx.cs" Inherits="Eleooo.Web.Public.EleoooClient" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagPrefix="uc" TagName="UcPagePosition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 99%;
        }
        .style3
        {
            width: 418px;
        }
        .style4
        {
            width: 90px;
        }
        .style5
        {
            width: 180px;
            height: 77px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="title">
        <span>乐多分客户端程序</span>
        <uc:UcPagePosition ID="pagePos" runat="server" />
    </div>
    <br />
    &nbsp;
    <br />
    <br />
    <table class="style1">
        <tr>
            <td class="style3" rowspan="7">
                <img alt="乐多分客户端程序" width="418px" height="349px" src="/App_Themes/Admin/images/ClientApp.png" />
            </td>
            <td class="style4">
                程序名称:
            </td>
            <td>
                乐多分客户程序
            </td>
        </tr>
        <tr>
            <td class="style4">
                版&nbsp;&nbsp;本:
            </td>
            <td>
                <span id="lblVersion" runat="server"></span>
            </td>
        </tr>
        <tr>
            <td class="style4">
                系统需求:
            </td>
            <td>
                Windows XP,Windows Vista,Windows7
            </td>
        </tr>
        <tr>
            <td class="style4">
                运行环境:
            </td>
            <td>
                <a href="http://www.microsoft.com/downloads/zh-cn/details.aspx?FamilyID=0856EACB-4362-4B0D-8EDD-AAB15C5E04F5"
                    target="_blank">Microsoft .NET Framework 2.0 </a>
            </td>
        </tr>
        <tr>
            <td class="style4">
                网络要求:
            </td>
            <td>
                2M以上电信带宽
            </td>
        </tr>
        <tr>
            <td class="style4">
                硬盘空间:
            </td>
            <td>
                10M
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;
            </td>
            <td>
                <a href="/Client/Eleooo.Client.application">
                    <img border="0" height="90px" width="180px" alt="安装乐多分" src="/App_Themes/Admin/images/Install.png" /></a>
            </td>
        </tr>
    </table>
</asp:Content>
