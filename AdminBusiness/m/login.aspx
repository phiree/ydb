<%@ Page Title="" Language="C#" MasterPageFile="~/m/m.master" AutoEventWireup="true" 
CodeFile="~/login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="css/login.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div data-role="page" id="loginpage" data-theme="mya"  data-title="登录" style=" background:none;" >
    <div class="main-content">
       <div class="main-logo">
               <div class="logo-inco"><img src="images/logo.png" width="100%"/></div>
             <p style=" text-align:center;" data-theme="mya">解决万事找对点，一切帮你妥妥办让你生活有变化</p>
             
       </div>
       <br/>
     
        <p>  <asp:TextBox runat="server" ID="tbxUserName"></asp:TextBox> </p>      
       
        <p>  <asp:TextBox runat="server" ID="tbxPassword" TextMode="Password"></asp:TextBox>
                               </p>
      <div class="ui-grid-a">  
         <div class="ui-block-a">
          <asp:Button runat="server" ID="btnLogin" Text="登录" CssClass="loginBtn" OnClick="btnLogin_Click" /></div>
            <div class="ui-block-b" data-theme="mya">
               <div style="width:100%;height:60.375px; line-height:60.375px; position:relative;">
     
               <input runat="server" id="savePass" type="checkbox" /><span style="left:30px; top:-8px; position:absolute;">记住密码</span>
               </div>
            </div>
      </div>
      <br/>
      <a href="/m/register.aspx" data-transition="slideup"  class="my-a">会员注册</a>
  
     
     
  </div>
</div>

  

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bottom" Runat="Server">

</asp:Content>

