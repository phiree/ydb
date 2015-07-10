<%@ Page Title="" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CashTicket_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:GridView runat="server" ID="gvCashTickets" DataKeyNames="Id" 
        onrowcommand="gvCashTickets_RowCommand" 
        onrowdatabound="gvCashTickets_RowDataBound">
    <Columns>
    <asp:ButtonField  Text="领取" CommandName="claim" />
    <asp:TemplateField>
        <ItemTemplate>
            <asp:Label runat="server" ID="lblAmount"></asp:Label>
        </ItemTemplate>
    </asp:TemplateField>
    </Columns>
</asp:GridView>
</asp:Content>

