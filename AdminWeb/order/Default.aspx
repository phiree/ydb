<<<<<<< HEAD
﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="order_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
订单管理
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
function delInfo() 
{
 var flag=true;
	var list = document.getElementsByName("chbItem");
	//alert(list.length);
	 if ((list.length+"")=="undefined")
	{
		
		
		
		if (list.checked)
		{
		flag=false;
		}	
	 
	}
   else
	{
		for (i=0;i<list.length;i++) 
        
		
		
	{
			if (list[i].checked)
			
			{
			flag=false;
				
			}
			
	}
	}

	if (flag)
	{
	 	alert("对不起，你还没有选择要删除的信息！")
	 }
	else
	{ 
		if (confirm("警告：\n你现在的动作很危险，数据一旦被删除将无法还原！"))
		{
		
		}
		else
		{
		flag=true;
		
		}
	}
	
	return !flag;
}

//全选框事件
 function checkAll(checked) 
        {
            var list = document.getElementsByName("chbItem");
            for (var i = 0; i < list.length; i++)
                list[i].checked = checked;
        }


</script>
<!--
订单列表
-->
<div>
   订单状态： <select runat="server" id="StatusSelect" OnChange="var jmpURL=this.options[this.selectedIndex].value; if(jmpURL!='') {window.location.href=(jmpURL);} else {this.selectedIndex=0;}">
        <option  value="default.aspx">全部</option>
       <option value="default.aspx?status=Draft">创建中</option>
        <option value="default.aspx?status=Created">已创建</option>
        <option value="default.aspx?status=Payed">已付款</option>
        <option value="default.aspx?status=Canceled">申请退款</option>
        <option value="default.aspx?status=Assigned">Assigned</option>
        <option value="default.aspx?status=IsCanceled">IsCanceled</option>
       <option value="default.aspx?status=Finished">Finished</option>
       <option value="default.aspx?status=Aborded">Aborded</option>
       <option value="default.aspx?status=Appraise">Appraise</option>
    </select>
</div>

<asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="data_ItemDataBound">
<HeaderTemplate><%-- 我是头模板--%>
<table   width="100%">
<tr>
<td>选择</td>
<td>id号</td>
<td>交易号</td>
<td>订单状态</td>
<td>更新时间</td>
<td>金额</td>
<td>操作</td>
</tr>
</HeaderTemplate>
<ItemTemplate><%--我是项模板--%>
<tr>
<td><input id="Checkbox1" type="checkbox" name="chbItem" value='<%# Eval("Id")%>' />
    
</td>
<td><%# Eval("Id")%></td>
<td><%# Eval("TradeNo")%></td>
<td><%# Eval("OrderStatus")%></td>
<td><%# Eval("OrderFinished")%></td>
    <td><%# Eval("OrderAmount")%></td>
<td> <asp:Button ID="delbt" runat="server" Text="删除" CommandName="delete" CommandArgument='<%# Eval("id")%>' OnCommand="delbt_Command" OnClientClick="javascript:return confirm('警告：\n数据一旦被删除将无法还原！')" />
<a target="_blank" href="refund/default.aspx?id=<%# Eval("Id")%>&tradeno=<%# Eval("TradeNo")%>&orderamount=<%# Eval("OrderAmount")%>">退款</a>
</td>
</tr>        
</ItemTemplate>
<FooterTemplate><%--这是脚模板--%>
        <tr>
        <td colspan="8" style="font-size:12px;" align="center">
        共<asp:Label ID="lblpc" runat="server" Text="Label"></asp:Label>页 当前为第
        <asp:Label ID="lblp" runat="server" Text="Label"></asp:Label>页
        <asp:HyperLink ID="hlfir" runat="server" Text="首页"></asp:HyperLink>
        <asp:HyperLink ID="hlp" runat="server" Text="上一页"></asp:HyperLink>
        <asp:HyperLink ID="hln" runat="server" Text="下一页"></asp:HyperLink>
        <asp:HyperLink ID="hlla" runat="server" Text="尾页"></asp:HyperLink>
         跳至第
         <asp:DropDownList ID="ddlp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlp_SelectedIndexChanged" >
         </asp:DropDownList>页
        </td>
        </tr>
        <tr><td colspan="8">
<input type="checkbox" onclick="checkAll(this.checked)" />全选            
<asp:Button ID="alldel" runat="server" Text="全选删除" CommandName="alldel" OnClick="alldel_Click" OnClientClick="return delInfo();" />
            </td>    
            </tr>
        </table>
        </FooterTemplate>
    </asp:Repeater>  
</asp:Content>
=======
﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="order_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
订单管理
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!--
订单列表
-->
<div>
   订单状态： <select runat="server" id="StatusSelect" OnChange="var jmpURL=this.options[this.selectedIndex].value; if(jmpURL!='') {window.open(jmpURL);} else {this.selectedIndex=0;}">
        <option  value="default.aspx">全部</option>
       <option value="default.aspx?status=Draft">创建中</option>
        <option value="default.aspx?status=Created">已创建</option>
        <option value="default.aspx?status=Payed">已付款</option>
        <option value="default.aspx?status=Canceled">申请退款</option>
        <option value="default.aspx?status=Assigned">Assigned</option>
        <option value="default.aspx?status=IsCanceled">IsCanceled</option>
       <option value="default.aspx?status=Finished">Finished</option>
       <option value="default.aspx?status=Aborded">Aborded</option>
       <option value="default.aspx?status=Appraise">Appraise</option>
    </select>
    

</div>
<asp:GridView  CssClass="table"  DataKeyNames="Id"
     OnRowDeleting="GridView1_RowDeleting" 
    runat="server" ID="gv"  AllowPaging="true" PageSize="15"  OnPageIndexChanging="pagechanging">
<Columns>
<asp:BoundField  HeaderText="订单id" DataField="Id" />
<asp:BoundField  HeaderText="订单状态" DataField="OrderStatus" />
<asp:BoundField  HeaderText="交易号" DataField="TradeNo" />
    <asp:BoundField  HeaderText="用户名" DataField="CustomerName" />
    <asp:BoundField  HeaderText="订单金额" DataField="OrderAmount" />
    <asp:BoundField  HeaderText="创建时间" DataField="OrderCreated" />
<asp:HyperLinkField Target="_blank"  Text="编辑" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="edit.aspx?id={0}"/>
<asp:HyperLinkField Target="_blank"  Text="退款" DataNavigateUrlFields="TradeNo,OrderAmount,Id" DataNavigateUrlFormatString="refund/default.aspx?tradeno={0}&orderamount={1}&id={2}"/>
 <asp:TemplateField ShowHeader="False" >
<ItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('确定要删除吗？')"
Text="删除"></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
 

</Columns>
</asp:GridView>
</asp:Content>
>>>>>>> 55d230ed535ef0b5efa00fd4c633c7853eb54bdd
