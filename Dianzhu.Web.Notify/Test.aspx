<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--
             string strOrderId = context.Request["orderid"];
                    string strOrderTitle = context.Request["ordertitle"];
                    string strOrderStatus = context.Request["orderstatus"];
                    string strOrderType = context.Request["ordertype"];
                    string strOrderStatusFriendly = context.Request["orderstatusfriendly"];
                    string strUserId = context.Request["userid"];
                    string strToResource = context.Request["toresource"];
            -->
    <a href="IMServerAPI.ashx?type=systemnotice&body=body&group=customer">systemnotice</a>
         <a href="IMServerAPI.ashx?type=ordernotice&orderid=orderId&ordertitle=ordertitle&orderstatus=orderstatus&ordertype=ordertype&orderstatusfriendly=orderstatusfriendly&userid=userid&toresource=toresource">ordernotice</a>
    </div>
    </form>
</body>
</html>
