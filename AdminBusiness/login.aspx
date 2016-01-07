<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" id="login-html">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办登录" />
    <meta name="keywords" content="一点办" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>一点办登录</title>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>
    <link rel="Stylesheet" href="css/main.css" type="text/css" />
    <link href='http://api.youziku.com/webfont/CSS/568e3429f629d80f4cd910a4' rel='stylesheet' type='text/css' />
    <link href='http://api.youziku.com/webfont/CSS/568e353ff629d80f4cd910a7' rel='stylesheet' type='text/css' />
    <!--<link rel="Stylesheet" href="css/base.css" type="text/css" />-->
    <!--<link rel="Stylesheet" href="css/login_reg.css" type="text/css" />-->
</head>
<body id="login-body">
    <div class="register-wrap">
        <form id="form1" runat="server">
            <div class="register-section">
                <div class="register-panel">
                    <div class="register-detial">
                        <div class="login_err_msg" >
                            <asp:Label runat="server" ID="lblMsg"></asp:Label>
                        </div>
                        <p class="t-r">
                            <span class="username">
                                <span class="register-input-title">用户名</span>
                                <label class="usernameIcon" for="tbxUserName">
                                    <i class="icon"></i>
                                </label>
                                <asp:TextBox runat="server"  placeholder="电子邮箱" ID="tbxUserName"></asp:TextBox>
                            </span>
                        </p>
                        <p class="t-r">
                            <span class="password">
                                <span class="register-input-title">密码</span>
                                <label class="passwordIcon" for="tbxPassword">
                                    <i class="icon"></i>
                                </label>
                                <asp:TextBox runat="server" ID="tbxPassword" placeholder="密码" TextMode="Password"></asp:TextBox>
                            </span>
                        </p>
                        <div class="loginBox">
                            <p class="savePass">
                                <input runat="server" id="savePass" type="checkbox" /><label for="savePass">记住我</label>
                                <a class="doReg-forget fr" href="/account/forget.aspx">忘记密码？</a>
                                <a class="logReg-href fr m-r10" href="register.aspx">注册会员</a>
                            </p>
                        </div>
                    </div>
                    <div class="register-go">
                        <asp:Button runat="server" ID="btnLogin" CssClass="loginBtn" OnClick="btnLogin_Click" Text="登录"/>
                    </div>
                </div>
                <div class="register-brand">
                    <img id="register-logo" src="images/pages/register/logo_100x100.png" alt="logo" />
                    <div class="brand-head">
                        <h3 class="cssc0a9477146a8">一点办商户管理系统</h3>
                        <h4 class="cssc0a50b3d46a8">静心观天下·才能发现世界的精彩</h4>
                    </div>
                </div>
            </div>
        </form>
        <div class="footer">
            <a href="http://www.miibeian.gov.cn/">琼ICP备15000297号-4</a> Copyright © 2015 All Rights Reserved
        </div>
    </div>
</body>
<!--[if lte IE 9]>
<script src="/js/jquery.placeholder.min.js" type="text/javascript"></script>
<script type="text/javascript">$('input, textarea').placeholder();</script>
<![endif]-->
<script src="/js/login_reg.js" type="text/javascript"></script>
<%if (!Request.IsLocal){ %>
<script type="text/javascript">
  var cnzz_protocol = (("https:" == document.location.protocol) ? " https://" : " http://");
  document.write(unescape("%3Cspan id='cnzz_stat_icon_1256240621' style='display:none'%3E%3C/span%3E%3Cscript src='"
      + cnzz_protocol + "s4.cnzz.com/z_stat.php%3Fid%3D1256240621%26show%3Dpic1' type='text/javascript'%3E%3C/script%3E"));
      </script>
      <% }%>
</html>
