﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>出错了............</h1>
    <div style="color:#888;display:none;">
    <label>技术细节:</label>
    <p>
    <%=Server.UrlDecode(Request.Params["msg"]) %>
    </p>
       
    </div>
         <p>
            <a href="/">点击重试</a>
        </p>
    </div>
    </form>
</body>
</html>
