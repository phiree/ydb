<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <h3>  设备类型</h3><asp:RadioButtonList runat="server" ID="rblAppType">
                <asp:ListItem Value="ios">IOS</asp:ListItem>
                <asp:ListItem Value="android">Android</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
             <h3>  推送类型:</h3><asp:RadioButtonList runat="server" ID="rblPushTarget">
                <asp:ListItem Value="user">推送给用户</asp:ListItem>
                <asp:ListItem Value="business">推送给商户</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
             <h3>  消息类型:</h3><asp:RadioButtonList runat="server" ID="rblChatType">
                <asp:ListItem Value="Chat">文本或者多媒体</asp:ListItem>
                <asp:ListItem Value="OrderNotice">订单变更通知</asp:ListItem>
                <asp:ListItem Value="SysNotice">系统通知</asp:ListItem>
                             </asp:RadioButtonList>
        </div>
         <div>
              <h3> 消息来源:</h3><asp:RadioButtonList runat="server" ID="rblChatFromResource">
                <asp:ListItem Value="YDBan_CustomerService">助理</asp:ListItem>
                <asp:ListItem Value="YDBan_Store">商家</asp:ListItem>
                <asp:ListItem Value="Unknow">其他</asp:ListItem>
                             </asp:RadioButtonList>
        </div>
        <div>
             <h3>  订单Id:</h3><asp:TextBox runat="server" ID="tbxOrderId">test_orderId</asp:TextBox>
        </div>
        <div>
              <h3> 订单状态</h3><asp:RadioButtonList runat="server" ID="rblOrderStatus">
                <asp:ListItem Value="EndCancel">已取消</asp:ListItem>
                <asp:ListItem Value="Payed">已支付</asp:ListItem>
                <asp:ListItem Value="EndWarranty">已过保</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
              <h3> 推送目标:</h3> 
            <asp:RadioButtonList runat="server" ID="rblTokenList">
                <asp:ListItem Value="581714ea866776956f5a57bb8a010c4135e0f4a06c8579457258175dbcd1f79b"> 
                    测试服务器_IOS_Cat: 581714ea866776956f5a57bb8a010c4135e0f4a06c8579457258175dbcd1f79b
                </asp:ListItem>
                <asp:ListItem Value="dbc2f70f6f9b96c9ce6bbf5d21cef044273fbd3f9297c47bd3a45566ae0b98fe">   
                     正式服务器_IOS_畅畅维修: dbc2f70f6f9b96c9ce6bbf5d21cef044273fbd3f9297c47bd3a45566ae0b98fe
                </asp:ListItem>
                  <asp:ListItem Value="18608956891">   
                    测试/正式服务器_AND_18608956891: 18608956891
                </asp:ListItem>
            </asp:RadioButtonList>
          自定义token:<asp:TextBox runat="server" ID="tbxToken"></asp:TextBox>
        </div>
         
        <div>
           <h3>    推送消息:</h3><asp:TextBox runat="server" ID="tbxMessage">推送测试消息</asp:TextBox>
        </div>
        <div>
            <asp:Button runat="server" ID="btnPush" Text="推送" OnClick="btnPush_Click" />
            <br />
            <asp:TextBox ReadOnly="true" runat="server" TextMode="MultiLine" Width="500" Height="400" ID="tbxLog"></asp:TextBox>
        </div>
    </form>
</body>
</html>
