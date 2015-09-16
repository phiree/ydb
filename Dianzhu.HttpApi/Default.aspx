<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/json2.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
</head>
<body>

    <form id="form1" runat="server">
    <input type="button" value="全部接口" onclick="textall()" />
    <div> 
        <div id="dvResults">
        </div>
    </div>
    </form>
    <script src="test-data.js"></script>
    <script>

        var apiTest = {
            requestArray: test_data,
            need_to_test: need_to_test,
            test_all:false,
            begin: function () {
                for (var i = 0; i < this.requestArray.length; i++) {
                    var data = this.requestArray[i];
                    if (!this.test_all && $.inArray(data.protocol_CODE.toLowerCase(), need_to_test)<0)
                    { continue; }
                    var data_str = JSON.stringify(data, null, 4);
                    $.ajax({
                        url: "DianzhuApi.ashx",
                        method: "POST",
                        data: data_str,
                        async: false,
                        success: function (result) {
                            apiTest.writelog("请求:<br/><pre>" + data_str + "</pre><br/>返回值:<br/><pre>" + JSON.stringify(result, null, 4) + "</pre><hr/>");
                        }, //success
                        error: function (errmsg) {

                        }, //error
                        complete: function (result) { } //complete
                    }); //ajax
                } //for
            }, //begin
            writelog: function (msg) {
                $("#dvResults").append(msg);
            } //writelog
        };  //apitest

        apiTest.begin();
        function textall() {
            apiTest.test_all = true;
            apiTest.begin();
        }
    </script>
</body>
</html>
