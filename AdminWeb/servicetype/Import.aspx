<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="servicetype_Import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:FileUpload runat="server" ID="fu" /><asp:Button runat="server" ID="btnUpload"
        Text="导入" OnClick="btnUpload_Click" /><asp:Label runat="server" ID="lblMsg"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

