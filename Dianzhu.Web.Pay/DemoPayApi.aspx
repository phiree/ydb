<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DemoPayApi.aspx.cs" Inherits="DemoPayApi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2>支付接口模拟</h2>
     用户名:   <asp:TextBox runat="server" ID="tbxUserName"></asp:TextBox><br />
     密码:    <asp:TextBox TextMode="Password" runat="server" ID="tbxPwd"></asp:TextBox>
        (留空是支付成功,否则是支付失败)
        <br />
        <asp:Button runat="server" ID="btnPay" Text="登录并支付" OnClick="btnPay_Click" />
    </div>
    </form>
</body>
</html>
