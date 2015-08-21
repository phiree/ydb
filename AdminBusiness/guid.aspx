<%@ Page Language="C#" AutoEventWireup="true" CodeFile="guid.aspx.cs" Inherits="guid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <% for (int i = 0; i < 1000; i++)
       { %>
     <%=Guid.NewGuid()%><br />
    <%}%>

    </div>
    </form>
</body>
</html>
