<%@ Page Title="" Language="C#"   AutoEventWireup="true" CodeFile="forget.aspx.cs" Inherits="forget" %>
 
 <form runat="server">
请输入注册邮箱:<asp:TextBox runat="server" ID="tbxEmail" ></asp:TextBox>
 <asp:Button runat="server" ID="btnRecover" OnClick="btnRecover_Click" Text="重置密码" />
 <asp:Label runat="server" ID="lblMsg"></asp:Label>
 
 </form>
