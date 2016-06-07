<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        推送类型<asp:RadioButtonList runat="server" ID="rblAppType">
        <asp:ListItem Value="ios" Selected="True">IOS</asp:ListItem>
          <asp:ListItem Value="android">Android</asp:ListItem>
         </asp:RadioButtonList></div>
     
         <div>
        订单号<asp:TextBox runat="server" ID="tbxOrderId" ></asp:TextBox>
   </div>
        <div>
           设备号:<asp:TextBox runat="server" ID="tbxDeviceToken">75645d9263bb15257718f7af39d20cd6245b049970a763680ce8da6ea7fcb7b7</asp:TextBox>
        </div>
         <div>
           推送消息:<asp:TextBox runat="server" ID="tbxMessage">推送测试消息</asp:TextBox>
        </div>
        <div>
            <asp:Button runat="server" ID="btnPush" Text="推送" OnClick="btnPush_Click" />
            <br />
            <asp:TextBox ReadOnly="true" runat="server" TextMode="MultiLine"  Width="500" Height="400" id="tbxLog"></asp:TextBox>
        </div>
    </form>
</body>
</html>
