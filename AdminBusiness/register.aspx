<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="register-html">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-stand|ie-comp">
    <meta name="description" content="一点办注册" />
    <meta name="keywords" content="一点办" />
    <title>一点办注册</title>
    <link href='http://api.youziku.com/webfont/CSS/568e3429f629d80f4cd910a4' rel='stylesheet' type='text/css' />
    <link href='http://api.youziku.com/webfont/CSS/568e353ff629d80f4cd910a7' rel='stylesheet' type='text/css' />
    <link rel="Stylesheet" href="/css/main.css" type="text/css" />
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
        <div class="register-section captchaHeight">
            <div class="register-panel">
                <div class="register-detail reg">
                    <div class="login_err_msg m-b10" >
                        <ul></ul>
                    </div>
                    <div class="register-input-w fluid">
                        <span class="register-input-title">用户名</span>
                        <label class="register-input-icon" for="tbxUserName">
                            <i class="usernameIcon"></i>
                        </label>
                        <asp:TextBox runat="server" CssClass="register-input regUserName" ID="tbxUserName" placeholder="请输入电子邮箱注册" ValidationGroup="vg_UserName" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="register-input-w fluid">
                        <span class="register-input-title">密码</span>
                        <label class="register-input-icon" for="tbxPassword">
                            <i class="passwordIcon"></i>
                        </label>
                        <asp:TextBox CssClass="register-input" runat="server" ClientIDMode="Static" ID="regPs" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="register-input-w fluid">
                        <span class="register-input-title">确认密码</span>
                        <label class="register-input-icon" for="regPsConf">
                            <i class="passwordIcon"></i>
                        </label>
                        <asp:TextBox CssClass="register-input" runat="server" ClientIDMode="Static" ID="regPsConf" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="register-input-w fluid thin-b captcha" ID="captchaBox">
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
                        <div class="register-agree">
                            <input id="agreeLic" class="v-m" name="agreeLic" type="checkbox" value="yes" />
                            <label class="v-m" for="agreeLic">我已阅读过《<a
                                class="doReg" id="agreeLicHref" target="_blank" href="/protocol.html">点助服务协议</a>》</label>
                        </div>
                            <a class="doReg fr m-r10" href="login.aspx">返回登录</a>
                        </div>
                    </div>
                </div>
                <div class="register-go">
                    <asp:Button runat="server" ID="regPsSubmit"  ClientIDMode="Static" OnClick="regPsSubmit_OnClick" CssClass="register-btn"  Text="注册"/>
                </div>
            </div>
        </div>
    </form>
    <div class="footer">
        <a href="http://www.miibeian.gov.cn/">琼ICP备15000297号-4</a> Copyright © 2015 All Rights Reserved
    </div>
</div>

<script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>static/Scripts/jquery-1.11.3.min.js"></script>
<script src="js/components/captcha.js"></script>
<script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/jquery.validate.js"></script>
<script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/additional-methods.js"></script>
<script src="/js/plugins/jquery.form.min.js"></script>
<script src="/js/apps/validation/validation_reg.js?v=1.0.0"></script>
<script>
    $("#code").captcha({errorTime: 5});
</script>
<!--[if lte IE 9]>
<script src="/js/plugins/jquery.placeholder.min.js"></script>
<script>$('input, textarea').placeholder();</script>
<![endif]-->
<%if (!Request.IsLocal){ %>
<script>
    (function (){
        var cnzz_protocol = (("https:" == document.location.protocol) ? " https://" : " http://");
        document.write(unescape("%3Cspan id='cnzz_stat_icon_1256240621' style='display:none'%3E%3C/span%3E%3Cscript src='"
                + cnzz_protocol + "s4.cnzz.com/z_stat.php%3Fid%3D1256240621%26show%3Dpic1' type='text/javascript'%3E%3C/script%3E"));
    })();
</script>
<% }%>
</body>
</html>
