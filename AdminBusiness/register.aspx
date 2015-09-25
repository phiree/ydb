<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办注册" />
    <meta name="keywords" content="一点办" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>一点办注册</title>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>

    <link rel="Stylesheet" href="/css/base.css" type="text/css" />
    <link rel="Stylesheet" href="/css/login_reg.css" type="text/css" />
</head>
<body>
    <div class="reg-wrap">
        <div class="head">
        </div>
        <div class="main">
            <div class="reg-layout">
                <div class="wrap-reg">
                    <form id="form1" runat="server">
                    <div class="conReg">
                        <div class="conLogo">
                            <div class="logo">
                                <div class="logo-title-l"></div>
                                <div class="logo-title-r">
                                    <img src="image/login_reg/zhuce_2.png" alt="logo" />
                                </div>


                            </div>
                            <div>
                                <div class="logo-title-l">
                                    <p>欢迎来到一点办商户会员注册页面<br />
                                    开启移动智能O2O新历程</p>
                                </div>
                                <div class="logo-title-r">
                                    <i class="icon logoTitle"></i>
                                </div>
                            </div>

                        </div>
                        <div class="conMain main-reg clearfix">
                            <div class="reg-box">
                                <div class="reg-box-userName">
                                    <div class="reg-box-l"><p>注册帐号</p></div>
                                    <div class="reg-box-r">
                                        <div class="reg-input">
                                            <asp:TextBox runat="server" CssClass="regUserName" ID="tbxUserName" placeholder="请输入电子邮箱注册" ValidationGroup="vg_UserName" ClientIDMode="Static"></asp:TextBox>
                                            <i class="checkIcon"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="reg-box-pass">
                                    <div class="reg-box-l"><p>登录密码</p></div>
                                    <div class="reg-box-r">
                                        <div class="reg-input">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="regPs" TextMode="Password"></asp:TextBox>
                                            <i class="checkIcon"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="reg-box-passConf">
                                    <div class="reg-box-l"><p>确认密码</p></div>
                                    <div class="reg-box-r">
                                        <div class="reg-input">
                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="regPsConf" TextMode="Password"></asp:TextBox>
                                            <i class="checkIcon"></i>
                                        </div>
                                        <div>
                                            <div class="agree">
                                                <input id="agreeLic" name="agreeLic" type="checkbox" value="yes" /><label class="v-m m-l10" for="agreeLic">我已经仔细阅读过《<a
                                                    class="agreeLIC-a" id="agreeLicHref" target="_blank" href="/protocol.html">一点办服务协议</a>》，并同意所有条款。</label>
                                            </div>
                                            <div class="buttonBox">
                                                <asp:Button runat="server" ID="regPsSubmit"  ClientIDMode="Static" OnClick="regPsSubmit_OnClick"
                                                    CssClass="regBtn" />
                                            </div>
                                            <div class="doLogin">
                                                <a class="logReg-a" href="login.aspx"><img src="image/login_reg/back_2.png">返回登录</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
                <div class="reg-footer">
                    <a href="http://www.miibeian.gov.cn/">琼ICP备15000297号-4</a> Copyright © 2015 All Rights Reserved
                </div>
            </div>
        </div>
    </div>
</body>
<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
<script src="/js/jquery.lightbox_me.js" type="text/javascript"></script>
<!--[if lte IE 9]>
<script src="/js/jquery.placeholder.min.js" type="text/javascript"></script>
<script>$('input, textarea').placeholder();</script>
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
