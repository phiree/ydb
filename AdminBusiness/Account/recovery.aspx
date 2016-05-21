<%@ Page Title="" Language="C#"  
 AutoEventWireup="true" CodeFile="recovery.aspx.cs" Inherits="ForgetPassword" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="register-html">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办重置密码" />
    <meta name="keywords" content="一点办" />
    <!--<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />-->
    <title>一点办重置密码</title>
    <link href='http://api.youziku.com/webfont/CSS/568e3429f629d80f4cd910a4' rel='stylesheet' type='text/css' />
    <link href='http://api.youziku.com/webfont/CSS/568e353ff629d80f4cd910a7' rel='stylesheet' type='text/css' />
    <link rel="Stylesheet" href="/css/main.css" type="text/css" />
</head>
<body class="register-body">
    <div class="register-wrap">
        <form runat="server">
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
                            <h3>重置密码</h3>
                        </div>
                        <div class="reg-model-m">
                            <div>
                                <div class="register-input-w short thin-b">
                                    <span class="register-input-title">新密码</span>
                                    <label class="register-input-icon" for="tbxPassword">
                                        <i class="usernameIcon"></i>
                                    </label>
                                    <asp:TextBox CssClass="register-input" runat="server" TextMode="Password" id="tbxPassword"></asp:TextBox>
                                </div>
                            </div>
                            <div>
                                <div class="register-input-w short thin-b">
                                    <span class="register-input-title">确认密码</span>
                                    <label class="register-input-icon" for="tbxPasswordConfirm">
                                        <i class="passwordIcon"></i>
                                    </label>
                                    <asp:TextBox CssClass="register-input" runat="server" TextMode="Password" id="tbxPasswordConfirm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="login_err_msg md m-b10" >
                                <ul></ul>
                            </div>
                            <div>
                                <div class="m-b10">
                                    <asp:Label runat="server" CssClass="f-s14" ID="lblMsg"></asp:Label>
                                    <asp:HyperLink runat="server" NavigateUrl="/login.aspx" Visible=false ID="hlLogin">修改成功,请登录</asp:HyperLink>
                                </div>
                                <asp:Button runat="server" CssClass="register-green-btn" ID="btnReset" Text="确认" OnClick="btnReset_Click" />

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
</body>
<script src='<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>static/Scripts/jquery-1.11.3.min.js'></script>
<script src="/js/jquery.validate.js"></script>
<script>
    $.validator.setDefaults({
        ignore: [],
    });

    $.validator.addMethod("pwd", function (value, element) {
        return value == "" ? true : /^[A-Za-z0-9_-]+$/.test(value);
    }, "密码格式错误");

    $.validator.addMethod("pwdConfirm", function (value, element) {
        return $("#tbxPasswordConfirm").val() == $("#tbxPassword").val() ? true : false
    }, "两次输入的密码不一致");

    var pass_validate_rules ={};
    var pass_validate_messages={};


    //tbxPassword
    pass_validate_rules["tbxPassword"]=
    {
        required: true,
        minlength: 6,
        maxlength: 20,
        pwd: true

    };
    pass_validate_messages["tbxPassword"]=
    {
        required: "请填写密码",
        minlength: "不能少于6个字符",
        maxlength: "不能超过20个字符"
    };

    //tbxPasswordConfirm
    pass_validate_rules["tbxPasswordConfirm"]=
    {
        required: true,
        minlength: 6,
        maxlength: 20,
        pass:true,
        passConfirm: true
    };
    pass_validate_messages["tbxPasswordConfirm"]=
    {
        required: "请再次输入密码",
    };

    $($("form")[0]).validate(
            {
                errorElement: "div",
                errorLabelContainer: ".login_err_msg ul",
                wrapper: "li",
                rules: pass_validate_rules,
                messages: pass_validate_messages
            }

    );
</script>
<!--[if lte IE 9]>
<script src="/js/jquery.placeholder.min.js"></script>
<script>$('input, textarea').placeholder();</script>
<![endif]-->
</html>
