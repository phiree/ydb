<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="test_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <fieldset>
        <legend>创建订单</legend>
        <div>
            客户名称:<asp:TextBox runat="server" ID="tbxCustomerName"></asp:TextBox>
            <asp:Button runat="server" OnClick="btnCreateOrder_Click" Text="生成" />
            <asp:Label runat="server" ID="lblCreateOrderResult"></asp:Label>
        </div>
    </fieldset>
    </div>
    </form>
</body>
</html>
