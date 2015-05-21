<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Account_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="css/style.css" type="text/css" />
<script>
    function delInfo() {
        var flag = true;
        var list = document.getElementsByName("chbItem");
        //alert(list.length);
        if ((list.length + "") == "undefined") {



            if (list.checked) {
                flag = false;
            }

        }
        else {
            for (i = 0; i < list.length; i++) {
                if (list[i].checked) {
                    flag = false;

                }

            }
        }

        if (flag) {
            alert("对不起，你还没有选择要删除的信息！")
        }
        else {
            if (confirm("警告：\n你现在的动作很危险，数据一旦被删除将无法还原！")) {

            }
            else {
                flag = true;

            }
        }

        return !flag;
    }

    //全选框事件
    function checkAll(checked) {
        var list = document.getElementsByName("chbItem");
        for (var i = 0; i < list.length; i++)
            list[i].checked = checked;
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="data_rp" runat="server">
          <HeaderTemplate><%-- 我是头模板--%> 
<table width="100%">

<tr>
<td>选择</td>
<td>id</td>
<td>商家地址</td>
<td>纬度||经度</td>
<td>33</td>
<td>操作</td>
</tr>

</HeaderTemplate>
        <ItemTemplate>
        <tr>
                <td>
                    <input id="Checkbox1" type="checkbox" name="chbItem" value='<%# Eval("id")%>' /></td>
 
        <td><%# Eval("id")%></td>
        <td><%# Eval("Address")%></td>
<td><%# Eval("Longitude")%>||<%# Eval("Latitude")%></td>
<td></td>

<td>
 <asp:Button ID="delbt"
                runat="server" Text="删除" CommandName="delete" CommandArgument='<%# Eval("Id")%>' OnCommand="delbt_Command" OnClientClick="javascript:return confirm('警告：\n数据一旦被删除将无法还原！')" />

</td>
        </tr> 
        </ItemTemplate>

        </asp:Repeater>  
    </div>
    </form>
</body>
</html>
