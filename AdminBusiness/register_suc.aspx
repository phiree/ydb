<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register_suc.aspx.cs" Inherits="register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="register-html">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办重置密码" />
    <meta name="keywords" content="一点办" />
    <!--<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />-->
    <title>注册成功</title>
    <link href='http://api.youziku.com/webfont/CSS/568e3429f629d80f4cd910a4' rel='stylesheet' type='text/css' />
    <link href='http://api.youziku.com/webfont/CSS/568e353ff629d80f4cd910a7' rel='stylesheet' type='text/css' />
    <link rel="Stylesheet" href="./css/main.css" type="text/css" />
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
                        <h3>注册成功</h3>
                    </div>
                    <div class="reg-model-m">
                        <div class="recovery-suc clearfix">
                            <i class="icon recovery-suc-icon"></i>
                            <div class="recovery-m">
                                <div class="recovery-h">
                                    注册成功
                                </div>
                                <div class="recovery-p">
                                    恭喜你已经成功注册成为一点半用户。<a id="doReg" class="doReg" runat="server" href="/business/">进入管理页</a>
                                </div>
                            </div>
                        </div>
                        <div class="regComp">
                            <asp:Label runat="server" ID="lblSendError" CssClass="errorText"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div class="footer">
        <a href="http://www.miibeian.gov.cn/">琼ICP备15000297号-4</a> Copyright © 2015 All Rights Reserved
    </div>
</div>
<script>
    (function () {
        if (!window.location.search.match("customerService")) {
            document.getElementById("doReg").style.visibility = "hidden";

            setTimeout(function () {
                window.location.href = document.getElementById("doReg").getAttribute("href");
            }, 3000);
        } else {
        }
    })()
</script>
</body>
