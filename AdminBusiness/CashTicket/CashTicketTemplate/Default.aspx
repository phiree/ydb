<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CashTicket_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:GridView runat="server" ID="gv">
    <Columns>
        <asp:HyperLinkField  Text="生成现金券"  DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/CashTicket/generator/Generator.aspx?templateid={0}" />
        <asp:HyperLinkField  DataTextField="Name" DataNavigateUrlFields="id" DataNavigateUrlFormatString="/cashticket/cashtickettemplate/edit.aspx?id={0}" />
    </Columns>

</asp:GridView>
 <a href="/cashticket/cashtickettemplate/edit.aspx">创建现金券</a>
</asp:Content>

