<%@ Page Title="" Language="C#"  
 AutoEventWireup="true" CodeFile="recovery.aspx.cs" Inherits="ForgetPassword" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办重置密码" />
    <meta name="keywords" content="一点办" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>一点办重置密码</title>
    <script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>static/Scripts/jquery-1.11.3.min.js"></script>
    <script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>
    <!--<link href="/css/validation.css" rel="stylesheet" type="text/css">-->
    <link rel="Stylesheet" href="/css/base.css" type="text/css" />
    <link rel="Stylesheet" href="/css/login_reg.css" type="text/css" />
    <Style>
        p.error {
               background:url("../image/shop-icon-13.png") no-repeat 0px 50%;
               padding: 4px 10px 4px 30px;
               font-size: 12px;
               line-height: 2em;
               color: #ff8b8b;
           }
    </Style>
</head>
<body>
    <div class="reg-wrap">
        <div class="main">
            <div class="reg-layout">
                <div class="wrap-reg">
                    <form runat="server">
                    <div class="conReg">
                        <div class="conLogo">
                            <div class="logo">
                                <div class="logo-title-l"></div>
                                <div class="logo-title-r">
                                    <img src="/image/login_reg/icon_1.png" alt="logo" />
                                </div>


                            </div>
                            <div>
                                <div class="logo-title-l">
                                    <p>重新设置密码</p>
                                </div>
                                <div class="logo-title-r">
                                    <i class="icon logoTitle"></i>
                                </div>
                            </div>

                        </div>
                        <div class="conMain main-reg clearfix">
                            <div class="reg-box">
                                <div class="reg-box-userName">
                                    <div class="reg-box-l"><p>新密码</p></div>
                                    <div class="reg-box-r">
                                        <div class="reg-input">
                                            <asp:TextBox runat="server" TextMode="Password" id="tbxPassword"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="reg-box">
                                <div class="reg-box-userName">
                                    <div class="reg-box-l"><p>确认密码</p></div>
                                    <div class="reg-box-r">
                                        <div class="reg-input">
                                            <asp:TextBox runat="server" TextMode="Password" id="tbxPasswordConfirm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="reg-box">
                                <div class="reg-box-userName">
                                    <div class="reg-box-l"></div>
                                    <div class="reg-box-r">
                                        <div class="m-b10"><asp:Label runat="server" CssClass="f-s14" ID="lblMsg"></asp:Label><asp:HyperLink runat="server" NavigateUrl="/login.aspx" Visible=false ID="hlLogin">修改成功,请登录</asp:HyperLink></div>
                                        <asp:Button runat="server" CssClass="reset-Pass-btn" ID="btnReset" Text="确认" OnClick="btnReset_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
<script type="text/javascript" src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>static/Scripts/jquery-1.11.3.min.js"></script>
<script src="/js/jquery.lightbox_me.js" type="text/javascript"></script>
<script src="/js/jquery.validate.js" type="text/javascript"></script>
<script>
    $.validator.setDefaults({
        ignore: []
    });

    $.validator.addMethod("pass", function (value, element) {
        return value == "" ? true : /^[A-Za-z0-9_-]+$/.test(value);
    }, "请输入有效的密码");

    $.validator.addMethod("passConfirm", function (value, element) {
        return $("#tbxPasswordConfirm").val() == $("#tbxPassword").val() ? true : false
    }, "两次输入的密码不一致");

    var pass_validate_rules ={};
    var pass_validate_messages={};


    //tbxPassword
    pass_validate_rules["tbxPassword"]=
    {
        required: true,
        minlength: 6,
        pass: true

    };
    pass_validate_messages["tbxPassword"]=
    {
        required: "请填写密码",
        pass: "请填写有效的密码",
        minlength: "密码长度不小于6"
    };

    //tbxPasswordConfirm
    pass_validate_rules["tbxPasswordConfirm"]=
    {
        required: true,
        minlength: 6,
        pass:true,
        passConfirm: true
    };
    pass_validate_messages["tbxPasswordConfirm"]=
    {
        required: "请确认密码",
        minlength: "密码长度不小于6"
    };

    $($("form")[0]).validate(
            {
                errorElement: "p",
                errorPlacement: function(error, element) {
                    error.appendTo( element.parent() );
                },
                rules: pass_validate_rules,
                messages: pass_validate_messages
            }

    );
</script>
<!--[if lte IE 9]>
<script src="/js/jquery.placeholder.min.js" type="text/javascript"></script>
<script>$('input, textarea').placeholder();</script>
<![endif]-->
<script src="/js/login_reg.js" type="text/javascript"></script>

</html>
