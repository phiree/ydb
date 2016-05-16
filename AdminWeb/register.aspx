<%@ Page Title="" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset><legend>注册</legend>
        <div>用户名:<asp:TextBox ID="tbxUserName" runat="server"></asp:TextBox></div>
        <div>密码:<asp:TextBox ID="tbxPwd" TextMode="Password" runat="server"></asp:TextBox></div>
        <div><asp:RadioButtonList runat="server" ID="rblUserType">
            <asp:ListItem Value="4">客服</asp:ListItem>
             <asp:ListItem Value="1">客户</asp:ListItem>
             </asp:RadioButtonList></div>
        <div>
            <asp:Button runat="server" ID="btnRegister" Text="注册" OnClick="btnRegister_Click" />
            
        </div>
    </fieldset>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" Runat="Server">
</asp:Content>

