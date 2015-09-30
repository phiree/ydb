<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ceshi.aspx.cs" Inherits="WeiPayWeb.ceshi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>购物车</title>
        
      <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1" />
        <meta http-equiv="Content-Type" content="text/html; charset=GBK" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    您买了一双鞋。
    
    共计<asp:Label ID="Label1" runat="server" Text="">60</asp:Label>元
    <br />
        <asp:Button ID="Button1" runat="server" Text="提交" onclick="BtnSave_Click" />

    </div>
    </form>
</body>
</html>
