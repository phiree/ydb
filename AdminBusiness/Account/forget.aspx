<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="forget.aspx.cs" Inherits="forget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sitenav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
请输入注册邮箱:<asp:TextBox runat="server" ID="tbxEmail" ></asp:TextBox>
 <asp:Button runat="server" ID="btnRecover" OnClick="btnRecover_Click" Text="重置密码" />
 <asp:Label runat="server" ID="lblMsg"></asp:Label>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>

