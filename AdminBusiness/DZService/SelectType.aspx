<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="SelectType.aspx.cs" Inherits="DZService_SelectType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>请选择服务类别:</div>
<asp:Repeater runat="server" ID="rptServiceType">
<ItemTemplate>

</ItemTemplate>
</asp:Repeater>
</asp:Content>

