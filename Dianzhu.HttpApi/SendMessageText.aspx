<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessageText.aspx.cs" Inherits="SendMessageText" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function ceshi() {
            var xmlHttp = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
            var m = document.getElementById("mobile");
            var Num = "";
            for (var i = 0; i < 6; i++) {
                Num += Math.floor(Math.random() * 10);
            }
            //var c = document.getElementById("contents");
            //添加参数,以求每次访问不同的url,以避免缓存问题
            xmlHttp.open("get", encodeURI("http://222.73.117.158/msg/HttpBatchSendSM?account=jiekou-clcs-06&pswd=Tch357159&mobile="+m.value+"&msg=你的密码已经被重置为："+Num));

            xmlHttp.onreadystatechange = function () {
                if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
                    document.getElementById("result").innerHTML = xmlHttp.responseText;
                }
            }

            //发送请求,参数为null
            xmlHttp.send(null);
            alert("你的密码已经被重置，稍后将以短信的方式发送到您手机，请查收！");
        }
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
      手机号：  <input id="mobile" type="text" />
       <%--验证内容： <input id="contents" type="text" />--%>
        <input id="Button1" type="button" value="输入手机号取回密码"  onclick="ceshi()" />
    </div>
    </form>
</body>
</html>
