<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="order_Default" %>

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
   订单状态： <select id="StatusSelect" onChange="var jmpURL=this.options[this.selectedIndex].value; if(jmpURL!='') {window.open(jmpURL);} else {this.selectedIndex=0;}">
        <option selected>全部</option>
       <option value="default.aspx?status=Draft">创建中</option>
        <option value="Created">已创建</option>
        <option value="Payed">已付款</option>
        <option value="ApplyRefund">申请退款</option>
        <option value="RefundReady">退款准备</option>
        <option value="RefundFinished">退款完成</option>
        <option value="Finished">已完成</option>
       <option value="Aborded">已中止</option>
    </select>
    
</div>
<asp:GridView  CssClass="table"       
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
