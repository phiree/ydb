<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    商户名称
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbx_Name"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    负责人手机号码
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbx_MobilePhone"></asp:TextBox>
                    <span>请准确填写,将用作登录名</span>
                </td>
            </tr>
            <tr>
                <td>
                    密码
                </td>
                <td>
                    <asp:TextBox runat="server" TextMode=Password ID="tbx_Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    地址
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxAddress"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    地理坐标
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxLon"></asp:TextBox>
                    <asp:TextBox runat="server" ID="tbxLat"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    服务分类
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxCategory"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    公司介绍
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxDescription"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    资质证书
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbxCertification"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan=2>
                    <asp:Button runat="server" ID="tbxOK"  OnClick="btnOK_Click" Text='注册'/>
                </td>
                
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
