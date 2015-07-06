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
    <div>
    <div id="dvResults"></div>
    </div>
    </form>
    <script>
    var data={ 
              "protocol_CODE": "USM001001", 
                  "ReqData": { 
                  "userEmail": "a@a.a", 
                   
                  "userPWord": "123456", 
                  }, 
              "stamp_TIMES": "1490192929212", 
              "serial_NUMBER": "00147001015869149751" 
            };
            var data_str=JSON.stringify(data);
        $.post(
         "DianzhuApi.ashx",
         data_str , 
        function (result) { 
            $("#dvResults").append("<span>USM001001:"+JSON.stringify(result)+"</span><br/>");
        }
        );
    </script>
</body>
</html>
