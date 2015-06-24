<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Generator.aspx.cs" Inherits="CashTicket_Generator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
 
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:TextBox runat="server" id="tbxTotal"></asp:TextBox>
<asp:Button runat="server"  ID="btnGenerate" OnClick="btnGenerate_Click" Text="确定生成"/>
</asp:Content>

