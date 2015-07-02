<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CashTicket_Generator_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
 现金券生成记录
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:GridView runat="server" ID="gv">
<Columns>
<asp:HyperLinkField  HeaderText="查看生成详情" DataTextField="TimeCreated" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="detail.aspx?id={0}" />
</Columns>
<EmptyDataTemplate>
尚无生成记录.<a href="generator.aspx">您可以点击这里生成现金券</a> 
</EmptyDataTemplate>
</asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>

