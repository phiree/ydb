<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    <script>
        $.post(
        "DianzhuApi.ashx",
        
            {
                "protocol_CODE": "VCM001003", 
                "ReqData": { 
                                " srvID ": "6F9619FF8B86D011B42D00C04FC964FF", 
                            }, 
                "stamp_TIMES": "1490192929222", 
                "serial_NUMBER": "00147001015869149756" 
              }
         
        ,
        function (result) { 
        alert(result);
        }
        );
    </script>
</body>
</html>
