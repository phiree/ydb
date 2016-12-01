<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapBaidu.aspx.cs" Inherits="area_MapBaidu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </div>
        <hr />
    <div>
        成功：<asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
    </div>
        <hr />
    <div>
        失败：<asp:Label ID="lblFail" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
