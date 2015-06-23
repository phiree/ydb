<%@ Page Title="" Language="C#" MasterPageFile="~/m/m.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="m_register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div data-role="page" id="regpage" data-theme="mya" data-title="会员注册" style=" background:none;">
<div class="main-content">

   <div class="main-logo">
   <div class="logo-inco"><img src="images/logo-2.png" width="100%"/></div>
   <p style=" text-align:center;">欢迎来到点助商户会员注册页面开启移动智能O2O新历程</p>
   </div>
   <br/>
   
       
                 <label for="iphone-tyle">2种注册方式</label>
                  <select name="iphone-tyle" id="iphone-tyle" onChange="getSelectVal()" data-theme="a">
                  <option value="iphone-num">手机号码</option>
                  <option value="email">电子邮箱</option>
              </select>  
             <div id="iphone-div">
             <label for="iphone">输入号码：</label>
             <div style=" position:relative;">  
             <input type="text" name="iphone" id="iphone" data-inline="true"> <span style=" position:absolute; top:10px; left:8px; color:#333;">+86</span>  
             </div>
             </div>
             <div id="email-div">
             <label for="email">输入邮箱：</label>
             <div style=" position:relative;">  
             <input type="text" name="email" id="email" data-inline="true">  
             </div>
             </div>
              <label for="yzm">验证码：</label>
              <div class="ui-grid-a"> 
                 <div class="ui-block-a">
                  <input type="text" name="yzm" id="yzm" data-inline="true">
                 </div>
                  <div class="ui-block-b">
                        <div class="cyzm">65652</div>
                 </div>
              </div>
      
     <div style="width:100%;height:60.375px; line-height:60.375px; position:relative;">
     
              <input type="checkbox" name="agree" id="agree" value="agree" checked data-inline="true" /><span style="left:30px; top:-32px; position:absolute; font-size:12px;">我已经仔细阅读过点助服务协议,并同意所有条款。</span>
     </div>
    <a data-role="button" href="#okpage" data-transition="slideup">下一步</a>
    <br/>
  <a href="#loginpage" data-transition="slidedown"  class="my-a-2">返回登录页</a>
     


  
  </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bottom" Runat="Server">
<script type="text/javascript">

    function getSelectVal() {
        var txt = $("#iphone-tyle  option:selected").text();
        if (txt == "电子邮箱") {
            $("#email-div").css("display", "block");
            $("#iphone-div").css("display", "none");
        } else {
            $("#email-div").css("display", "none");
            $("#iphone-div").css("display", "block");
        }

    }
    $(document).ready(function () {
        $("#email-div").css("display", "none");
        $("#iphone-div").css("display", "block");

    })
</script>
</asp:Content>

