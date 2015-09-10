<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办登录" />
    <meta name="keywords" content="一点办" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>一点办登录</title>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>

    <link rel="Stylesheet" href="css/base.css" type="text/css" />
    <link rel="Stylesheet" href="css/login_reg.css" type="text/css" />
</head>
<body>
    <div class="reg-wrap">
        <div class="head">
        </div>
        <div class="main">
            <div class="layout">
                <div class="wrap-login">
                    <form id="form1" runat="server">
                    <div class="conLogin">
                        <div class="conLogo clearfix">
                            <div class="logo">
                                <div class="logo-title-l"></div>
                                <div class="logo-title-r">
                                    <img src="image/login_reg/icon_1.png" alt="logo" />
                                </div>
                            </div>
                            <div>
                                <div class="logo-title-l">
                                    <p>
                                        解决万事找对点，一切帮你妥妥办<br />
                                        让你生活有变化</p>
                                </div>
                                <div class="logo-title-r">
                                    <i class="icon logoTitle"></i>
                                </div>
                            </div>
                        </div>
                        <div class="conMain main-login clearfix">
                            <div class="box-l log-intro">
                                <p>
                                    <em>O</em><span>2O的终极商业模式</span></p>
                                <p>
                                    <em>1</em><span>个全新的平台</span></p>
                                <p>
                                    <em>2</em><span>4小时服务</span></p>
                                <p>
                                    <em>3</em><span>步接单</span></p>
                            </div>
                            <div class="box-r">
                                <p class="date">
                                    </p>
                                    <p class="username">
                                        <label class="usernameIcon" for="tbxUserName">
                                            <i class="icon"></i>
                                        </label>
                                        <asp:TextBox runat="server"  placeholder="电子邮箱" ID="tbxUserName"></asp:TextBox>
                                    </p>
                                <p class="password">
                                    <label class="passwordIcon" for="tbxPassword">
                                        <i class="icon"></i>
                                    </label>
                                    <asp:TextBox runat="server" ID="tbxPassword" placeholder="密码" TextMode="Password"></asp:TextBox>
                                </p>
                                <div class="loginBox">
                                     
                                    <asp:Button runat="server" ID="btnLogin" CssClass="loginBtn" OnClick="btnLogin_Click" />
                                    <p class="savePass">
                                        <input runat="server" id="savePass" type="checkbox" /><label for="savePass">记住我</label></p>
                                </div>
                                <div class="login_err_msg" >
                                <asp:Label runat="server" ID="lblMsg"></asp:Label>
                                </div>
                                <p class="doReg"><a class="logReg-a" href="register.aspx"><img src="image/login_reg/zhuce_1.png">注册会员</a><a class="doReg-forget fr" href="/account/forget.aspx">忘记密码？</a></p>
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
            <div class="conReg">
            </div>
        </div>
        <div class="footer">
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
