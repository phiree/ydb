<%@ Page MasterPageFile="~/admin.master" Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="order_index" %>

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
<div>
   订单状态： <select runat="server" id="StatusSelect" OnChange="var jmpURL=this.options[this.selectedIndex].value; if(jmpURL!='') {window.location.href=(jmpURL);} else {this.selectedIndex=0;}">
        <option  value="index.aspx">全部</option>
       <option value="index.aspx?status=Draft">创建中</option>
       <option value="index.aspx?status=Created">已创建</option>
               <option value="index.aspx?status=Payed">已付款</option>
               <option value="index.aspx?status=Canceled">申请退款</option>
        <option value="index.aspx?status=Assigned">Assigned</option>
        <option value="index.aspx?status=IsCanceled">IsCanceled</option>
       <option value="index.aspx?status=Finished">Finished</option>
       <option value="index.aspx?status=Aborded">Aborded</option>
       <option value="index.aspx?status=Appraise">Appraise</option>
    </select>
</div>
 <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="data_ItemDataBound">
     <HeaderTemplate>
         <table width="100%">
             <tr>
                 <td>选择</td>
                 <td>id号</td>
                 <td>交易号</td>
                 <td>订单状态</td>
                 <td>更新时间</td>
                 <td>金额</td>
                 <td>商家名称</td>
                 <td>操作</td>
             </tr>
     </HeaderTemplate>
<ItemTemplate>
<tr>
<td><input id="Checkbox1" type="checkbox" name="chbItem" value='<%# Eval("Id")%>' />
    
</td>
<td><%# Eval("Id")%></td>
<td> 
  
</td>
<td><%# Eval("OrderStatus")%></td>
<td><%# Eval("OrderCreated")%></td>
 <td><%# Eval("OrderAmount")%></td>
<td><%#DataBinder.Eval(Container.DataItem, "Service.Business.Name")%>
</td>
<td> <asp:Button ID="delbt" runat="server" Text="删除" CommandName="delete" CommandArgument='<%# Eval("id")%>' OnCommand="delbt_Command" OnClientClick="javascript:return confirm('警告：\n数据一旦被删除将无法还原！')" />
    
</td>
</tr>
   <asp:Repeater runat="server" ID="rptPayment">
       <ItemTemplate>
       <%# Eval("PayTarget") %>  <a target="_blank" href="refund/default.aspx?id=<%# Eval("Id")%>&tradeno=<%# Eval("TradeNo")%>&orderamount=<%# Eval("OrderAmount")%>">退款</a>
       </ItemTemplate>
   </asp:Repeater>

</ItemTemplate>
     <FooterTemplate><%--这是脚模板--%>
        <tr>
        <td colspan="8" style="font-size:12px;" align="center">
        共<asp:Label ID="lblpc" runat="server" Text="Label"></asp:Label>页 当前为第
        <asp:Label ID="lblp" runat="server" Text="Label"></asp:Label>页  
         <asp:HyperLink ID="hlfir" runat="server">首页</asp:HyperLink> 
        <asp:HyperLink ID="hlp" runat="server">上一页</asp:HyperLink>
        <asp:HyperLink ID="hln" runat="server">下一页</asp:HyperLink>
        <asp:HyperLink ID="hlla" runat="server">尾页</asp:HyperLink>
         跳至第
         <asp:DropDownList ID="ddlp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlp_SelectedIndexChanged" >
         </asp:DropDownList>页
        </td>
        </tr>
        <tr><td colspan="8">
<input type="checkbox" onclick="checkAll(this.checked)" />全选  
<asp:Button Id="alldel" runat="server" Text="全选删除" CommandName="alldel" OnClick="alldel_Click" OnClientClick="return delInfo();" />
            </td>    
            </tr>
        </table>
        </FooterTemplate>
</asp:Repeater>
    <UC:AspNetPager runat="server" ID="pager"   UrlPaging="true"></UC:AspNetPager>
</asp:Content>
