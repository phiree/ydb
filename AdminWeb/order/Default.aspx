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
   订单状态： <select runat="server" id="StatusSelect" onChange="var jmpURL=this.options[this.selectedIndex].value; if(jmpURL!='') {window.open(jmpURL);} else {this.selectedIndex=0;}">
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
