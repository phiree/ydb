<%@ Page Title="" Language="C#"   AutoEventWireup="true" CodeFile="forget.aspx.cs" Inherits="forget" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="register-html">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办密码找回" />
    <meta name="keywords" content="一点办" />
    <title>一点办密码找回</title>
    <link href='http://api.youziku.com/webfont/CSS/568e3429f629d80f4cd910a4' rel='stylesheet' type='text/css' />
    <link href='http://api.youziku.com/webfont/CSS/568e353ff629d80f4cd910a7' rel='stylesheet' type='text/css' />
    <link rel="Stylesheet" href="/css/main.css" type="text/css" />
    <!--[if lte IE 9]>
    <script src="/js/respond.min.js"></script>
    <![endif]-->
</head>
<body class="register-body">
    <div class="register-wrap">
    <form id="form1" runat="server">
        <div class="register-section">
            <div class="register-brand-c">
                <img id="register-logo" src="../images/pages/register/logo_100x100.png" alt="logo" />
                <div class="brand-head">
                    <h1 class="cssc0a9477146a8">一点办商户管理系统</h1>
                    <p class="cssc0a50b3d46a8">静心观天下·才能发现世界的精彩</p>
                </div>
            </div>
        </div>
        <div class="register-section">
            <div class="register-panel-c">
                <div class="reg-model">
                    <div class="reg-model-h">
                        <h3>忘记密码</h3>
                    </div>
                    <div class="reg-model-m">
                        <div class="register-input-w short thin-b">
                            <span class="register-input-title">邮箱地址</span>
                            <label class="register-input-icon" for="tbxEmail">
                                <i class="emailIcon"></i>
                            </label>
                            <asp:TextBox CssClass="register-input" runat="server" ID="tbxEmail" placeholder="请输入邮箱" ></asp:TextBox>
                        </div>
                        <div class="login_err_msg_static">
                            <asp:Label runat="server" ID="lblMsg"></asp:Label>
                        </div>
                        <div class="m-b50 m-t50">
                            <asp:Button runat="server" CssClass="recover-btn register-green-btn" ID="btnRecover" OnClick="btnRecover_Click" Text="重置密码" />
                        </div>
                        <a class="doReg" href="/login.aspx">返回登陆</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div class="footer">
        <a href="http://www.miibeian.gov.cn/">琼ICP备15000297号-4</a> Copyright © 2015 All Rights Reserved
    </div>
</div>
    <script type="text/javascript" src='<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>static/Scripts/jquery-1.11.3.min.js'></script>
    <!--[if lte IE 9]>
    <script src="/js/jquery.placeholder.min.js" type="text/javascript"></script>
    <script>$('input').placeholder();</script>
    <![endif]-->
</body>
</html>
