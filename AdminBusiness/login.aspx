<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="register-html">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-stand|ie-comp">
    <meta name="description" content="一点办登录" />
    <meta name="keywords" content="一点办" />
    <title>一点办登录</title>
    <link rel="shortcut icon" href="/favicon.ico" />
    <link rel="bookmark" href="/favicon.ico" type="image/x-icon" />
    <link href='http://api.youziku.com/webfont/CSS/568e3429f629d80f4cd910a4' rel='stylesheet' type='text/css' />
    <link href='http://api.youziku.com/webfont/CSS/568e353ff629d80f4cd910a7' rel='stylesheet' type='text/css' />
    <link rel="Stylesheet" href="css/main.css" type="text/css" />
    <!--[if lte IE 9]>
    <script src="./js/plugins/respond.min.js"></script>
    <![endif]-->
</head>
<body class="register-body">
    <div class="register-wrap">
        <form id="form1" runat="server">
            <div class="register-section">
                <div class="register-brand">
                    <img id="register-logo" src="images/pages/register/logo_100x100.png" alt="logo" />
                    <div class="brand-head">
                        <h1 class="cssc0a9477146a8">一点办商户管理系统</h1>
                        <p class="cssc0a50b3d46a8">静心观天下·才能发现世界的精彩</p>
                    </div>
                </div>
            </div>
            <div class="register-section" id="captchaHeight">
                <div class="register-panel">
                    <div class="register-detail">
                        <div class="login_err_msg m-b10" id="loginError">
                            <asp:Label runat="server" ID="lblMsg" CssClass="lblMsg"></asp:Label>
                        </div>

                        <div class="register-input-w fluid">
                            <span class="register-input-title">用户名</span>
                            <label class="register-input-icon" for="tbxUserName">
                                <i class="usernameIcon"></i>
                            </label>
                            <asp:TextBox class="register-input" runat="server"  placeholder="电子邮箱" ID="tbxUserName"></asp:TextBox>
                        </div>

                        <div class="register-input-w fluid">
                            <span class="register-input-title">密码</span>
                            <label class="register-input-icon" for="tbxPassword">
                                <i class="passwordIcon"></i>
                            </label>
                            <asp:TextBox class="register-input" runat="server" ID="tbxPassword" placeholder="密码" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="register-input-w fluid thin-b captcha captchaDisplay" ID="captchaBox">
                            <div class="control-group">
                                <div class="control-label" for="inputEmail">
                                    <span class="register-input-title" id="passwordIcon">验证码</span>
                                    <label class="register-input-icon" for="tbxPassword">
                                        <i class="passwordIcon"></i>
                                    </label>
                                    <!--<input name='captcha' type='text' id='captcha' class='register-input error' placeholder='验证码' aria-required='true' aria-describedby='lblMsg'>-->
                                </div>
                                <div class="controls">
                                    <div id="code"></div>
                                </div>
                            </div>
                        </div>
                        <div class="loginBox">
                            <div class="savePass">
                            <div class="register-agree" style="width: 100%">
                                <input runat="server" id="savePass" type="checkbox"/><label for="savePass">记住我</label>
                                <a class="fr" href="/account/forget.aspx">忘记密码？</a>
                                <a class="doReg fr m-r10" href="register.aspx">注册会员</a>
                            </div>
                            </div>
                        </div>
                    </div>
                    <div class="register-go">
                        <asp:Button runat="server" ID="btnLogin" CssClass="register-btn" OnClick="btnLogin_Click" Text="登录"/>
                    </div>
                </div>
            </div>
        </form>
        <div class="footer">
            <a href="http://www.miibeian.gov.cn/">琼ICP备15000297号-4</a> Copyright © 2015 All Rights Reserved
        </div>
    </div>
    <script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>static/Scripts/jquery-1.11.3.min.js"></script>
    <script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/jquery.validate.js"></script>
    <script src="js/components/captcha.js"></script>
    <script src="js/apps/validation/validation_LoginForget.js"></script>
    <script>
        $("#code").captcha({
                callback : function(){
                    $("#captchaBox").removeClass("captchaDisplay");
                    $("#captchaHeight").addClass("captchaHeight");
                }
            });
    </script>
    <!--[if lte IE 9]>
    <script src="/js/plugins/jquery.placeholder.min.js"></script>
    <script>$('input, textarea').placeholder();</script>
    <![endif]-->
    <%if (!Request.IsLocal){ %>
    <script>
        (function(){
            var cnzz_protocol = (("https:" == document.location.protocol) ? " https://" : " http://");
            document.write(unescape("%3Cspan id='cnzz_stat_icon_1256240621' style='display:none'%3E%3C/span%3E%3Cscript src='"
                    + cnzz_protocol + "s4.cnzz.com/z_stat.php%3Fid%3D1256240621%26show%3Dpic1' type='text/javascript'%3E%3C/script%3E"));
        })();
    </script>
    <% }%>
</body>
</html>
