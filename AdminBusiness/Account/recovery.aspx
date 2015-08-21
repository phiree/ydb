<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="recovery.aspx.cs" Inherits="ForgetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sitenav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
新密码:<asp:TextBox runat="server" id="tbxPassword"></asp:TextBox><br />
确认密码:<asp:TextBox runat="server" id="tbxPasswordConfirm"></asp:TextBox>
<asp:Button runat="server" ID="btnReset" Text="确认" OnClick="btnReset_Click" />
<br />
<asp:Label runat="server" ID="lblMsg"></asp:Label>
<asp:HyperLink runat="server" NavigateUrl="/login.aspx" Visible=false ID="hlLogin">修改成功,请登录</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>

