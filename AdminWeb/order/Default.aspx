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

<asp:GridView  CssClass="table"   runat="server" ID="gv">
<Columns>
<asp:BoundField  HeaderText="商户名称" DataField="Id" />
<asp:HyperLinkField  Text="详情" DataNavigateUrlFields="id" DataNavigateUrlFormatString="detail.aspx?id={0}"/>

</Columns>
</asp:GridView>
</asp:Content>