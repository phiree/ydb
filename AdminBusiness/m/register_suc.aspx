<%@ Page Title="" Language="C#" MasterPageFile="~/m/m.master" AutoEventWireup="true" CodeFile="register_suc.aspx.cs" Inherits="m_register_suc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link rel="stylesheet" href="css/login.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div data-role="page" style=" background:none;" id="reg-ok" data-theme="mya" data-title="会员注册成功">
<div class="main-content">

      <div class="main-logo">
   <div class="logo-inco"><img src="images/logo-2.png" width="100%"/></div>
   <p style=" text-align:center;">欢迎来到点助商户会员注册页面开启移动智能O2O新历程</p>
   </div>
    <br/>

   <table align="center">
     <tr>
       <td><img src="images/zcok.png" width="55"/></td>
       <td>
         <table>
           <tr> <td>恭喜您</td></tr>
           <tr> <td>已成功注册成为点助会员</td></tr>
         </table>
       </td>
       
     </tr>
   
   </table>
   <div style="text-align:center;">
    <a href="login.aspx" target="_top" data-transition="slidedown" data-role="button" data-inline="true" style="margin-top:35px;">确定</a>
    </div>
  </div>
</div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bottom" Runat="Server">
</asp:Content>

