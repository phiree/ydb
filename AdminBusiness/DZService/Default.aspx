<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DZService_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:GridView runat="server" ID="gvServices">
<Columns>
<asp:HyperLinkField  DataNavigateUrlFields="Id" DataNavigateUrlFormatString="edit.aspx?id={0}" DataTextField="Name"/>
</Columns>
</asp:GridView>
<a href="SelectType.aspx">增加服务</a>
</asp:Content>

