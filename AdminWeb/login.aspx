<%@ Page Title="" Language="C#" MasterPageFile="~/empty.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%if (Request.IsAuthenticated)
        { %>
    已登录用户: <%=User.Identity.Name %><a href="logoff.aspx">注销</a>
    <% } else { %>
    <div>
        用户名:<asp:TextBox runat="server" ID="tbxUserName"></asp:TextBox>
    </div>
    <div>密码:<asp:TextBox runat="server" TextMode="Password" ID="tbxPwd"></asp:TextBox></div>
    <div>
        <asp:Button runat="server" ID="btnLogin"  OnClick="btnLogin_Click" Text="登录"/>
        <asp:Label runat="server" ID="lblMsg" ></asp:Label>
        
    </div>
    <%} %>
</asp:Content>

