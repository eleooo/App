<%@ Page Title="商家区域维护" Language="C#" MasterPageFile="~/MasterPage/MainMaster.Master"
    AutoEventWireup="true" CodeBehind="SysAreaEdit.aspx.cs" Inherits="Eleooo.Web.Admin.SysAreaEdit" %>

<%@ Register Src="~/Controls/UcPagePosition.ascx" TagName="UcPagePosition" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UcMemberInfo.ascx" TagPrefix="uc" TagName="UcMemberInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function __onBtnCloseClick() {
            if (onBtnCloseClick) {
                onBtnCloseClick();
            }
            else if (parent.dlgHandlerCallback) {
                parent.dlgHandlerCallback();
            } else {
                window.close();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">
    <table border="0" cellspacing="0" cellpadding="5" width="99%">
        <tbody>
            <tr>
                <td style="line-height: 23px">
                    <uc:UcPagePosition ID="UcPagePosition1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 23px" width="50%">
                </td>
            </tr>
        </tbody>
    </table>
    <div>
        <ul style="list-style:none;">
            <li style="line-height: 25px;">
                <div style="text-align: center;">
                上级区域:
                <asp:DropDownList runat="server" ID="ddlParent" Width="200" 
                        CssClass="vertical-align: middle;"></asp:DropDownList>
                </div>
            </li>
            <li style="line-height: 25px;">
                <div style="text-align: center; ">
                区域名称:
                <asp:TextBox ID="txtAreaName" runat="server" Width="200" CssClass="vertical-align: middle;"></asp:TextBox>
                </div>
            </li>
            <li style="line-height: 25px;">
                <div style="text-align: center;">
                    区域简码:
                    <asp:TextBox ID="txtCode" runat="server" Width="200" CssClass="vertical-align: middle;"></asp:TextBox>
                    <br />
                    <label >如果没有输入区域简码,系统会自动生成</label>
                </div>
            </li>
            <li>
                <div style="text-align: center">
                    <input type='submit' style='line-height: 18px; width: 100px; height: 25px; font-size: 14px;
                        font-weight: bold' onclick='this.disabled=true;__doPostBack("<%=this.CurAction %>");' id='btnSubmit'
                        value='保存(S)' />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input type='button' style='line-height: 18px; width: 100px; height: 25px; font-size: 14px;
                        font-weight: bold' onclick='__onBtnCloseClick();' id='btnClose' value='取消(C)' />
                </div>
            </li>
        </ul>
    </div>
    <span id="txtMessage" style="color:Red;" runat="server"></span>
    <asp:LinkButton runat="server" />
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMemberInfo">
    <uc:UcMemberInfo ID="UcMemberInfo1" runat="server" />
</asp:Content>
