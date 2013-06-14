<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pupSale.aspx.cs" Inherits="Eleooo.Web.SiteAppPage.pupSale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/App_Themes/Admin/images/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class='tbl_bg' border="0" cellspacing="1" cellpadding="2">
        <tbody>
            <tr class="tbl_row">
                <td align="right" width="70">
                    <label runat="Server" id="lblOrderSumOk">
                        消费金额:</label>
                </td>
                <td>
                    <asp:Label runat="Server" ID="txtOrderSumOk"></asp:Label>
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right" width="70">
                    <label runat="Server" id="lblOrderRate">
                        折扣比例:</label>
                </td>
                <td>
                    <asp:Label runat="Server" ID="txtOrderRate"></asp:Label>
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right" width="70">
                    <label runat="Server" id="lblOrderPay">
                        现金支付:</label>
                </td>
                <td>
                    <asp:Label runat="Server" ID="txtOrderPay"></asp:Label>
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right" width="70">
                    <label runat="Server" id="lblOrderPayCash">
                        储值支付:</label>
                </td>
                <td>
                    <asp:Label runat="Server" ID="txtOrderPayCash"></asp:Label>
                </td>
            </tr>
            <tr class="tbl_row">
                <td align="right" width="70">
                    <label runat="Server" id="lblOrderPayPoint">
                        积分支付:</label>
                </td>
                <td>
                    <asp:Label runat="Server" ID="txtOrderPayPoint"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
