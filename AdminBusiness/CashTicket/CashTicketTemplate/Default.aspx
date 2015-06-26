<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CashTicket_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:GridView runat="server" ID="gv">
    <Columns>
        <asp:HyperLinkField  Text="生成现金券"  DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/CashTicket/Generator/Generator.aspx?templateid={0}" />
        
    </Columns>

</asp:GridView>
 <a herf="CashTicketTemplateEdit.aspx">创建现金券</a>
</asp:Content>

