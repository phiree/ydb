<%@ Page Title="" Language="C#"  
 AutoEventWireup="true" CodeFile="recovery.aspx.cs" Inherits="ForgetPassword" %>

 <form runat="server">
新密码:<asp:TextBox runat="server" id="tbxPassword"></asp:TextBox><br />
确认密码:<asp:TextBox runat="server" id="tbxPasswordConfirm"></asp:TextBox>
<asp:Button runat="server" ID="btnReset" Text="确认" OnClick="btnReset_Click" />
<br />
<asp:Label runat="server" ID="lblMsg"></asp:Label>
<asp:HyperLink runat="server" NavigateUrl="/login.aspx" Visible=false ID="hlLogin">修改成功,请登录</asp:HyperLink>
 
 </form>