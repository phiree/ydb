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

<asp:GridView  CssClass="table"   runat="server" ID="gv"  AllowPaging="true" PageSize="15"  OnPageIndexChanging="pagechanging">
<Columns>
<asp:BoundField  HeaderText="订单id" DataField="Id" />
<asp:BoundField  HeaderText="订单状态" DataField="OrderStatus" />
<asp:BoundField  HeaderText="交易号" DataField="TradeNo" />
    <asp:BoundField  HeaderText="用户名" DataField="CustomerName" />
    <asp:BoundField  HeaderText="订单金额" DataField="OrderAmount" />
    <asp:BoundField  HeaderText="创建时间" DataField="OrderCreated" />
<asp:HyperLinkField  Text="退款" DataNavigateUrlFields="TradeNo,OrderAmount" DataNavigateUrlFormatString="refund/default.aspx?tradeno={0}&orderamount={1}"/>
</Columns>
</asp:GridView>
</asp:Content>
