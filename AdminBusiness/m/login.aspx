<%@ Page Title="" Language="C#" MasterPageFile="~/m/m.master" AutoEventWireup="true" 
CodeFile="~/login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="css/login.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div data-role="page" id="loginpage" data-theme="mya"  data-title="登录" style=" background:none;" >
    <div class="main-content">
       <div class="main-logo">
               <div class="logo-inco"><img src="images/shop_icon_001.png" width="100%"/></div>
             <p style=" text-align:center;" data-theme="mya">解决万事找对点，一切帮你妥妥办让你生活有变化</p>
             
       </div>
       <br/>
     
        <p><span id="vUserName" class="erroTxt"></span>  <asp:TextBox runat="server" ID="tbxUserName"  onFocus="myFocus(this)"  onBlur="myBlur(this,'#vUserName')" ></asp:TextBox> <input type="hidden" id="vuserid"/></p>      
       
        <p><span id="vPassword" class="erroTxt"></span>  <asp:TextBox runat="server" ID="tbxPassword" TextMode="Password"  onFocus="valPwdFocus(this)" onBlur="mydlpwdBlur(this,'#vPassword')"></asp:TextBox><input type="hidden" id="vpwdid"/>
                               </p>
      <div class="ui-grid-a">  
         <div class="ui-block-a">
          <asp:Button runat="server" ID="btnLogin" Text="登录" CssClass="loginBtn" OnClientClick="return vLoginFun()" OnClick="btnLogin_Click" /></div>
            <div class="ui-block-b" data-theme="mya">
               <div style="width:100%;height:60.375px; line-height:60.375px; position:relative;">
     
               <input runat="server" id="savePass" type="checkbox" style=" margin-top:13px;" /><span style="left:30px; top:-8px; position:absolute;">记住密码</span>
               </div>
            </div>
      </div>
      <asp:Label ForeColor="Red" runat="server" ID="lblMsg"></asp:Label>
      <br/>
      <div>
      <a data-role="none" href="/m/register.aspx" target="_top" style=" float:left;" data-transition="slideup"  class="my-a-n">新会员注册</a>
      <a  data-role="none" href="#" target="_top" data-transition="slideup"  style=" float:left;" class="my-a-n">忘记密码</a>
      </div>
  
     
     
  </div>
</div>
<script type="text/javascript">

    
</script>
  

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bottom" Runat="Server">

</asp:Content>

