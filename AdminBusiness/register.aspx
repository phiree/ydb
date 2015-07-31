<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" href="/css/base.css" type="text/css" />
    <link rel="Stylesheet" href="/css/login_reg.css" type="text/css" />
</head>
<body>
    <div class="reg-wrap">
        <div class="head">
        </div>
        <div class="main">
            <div class="layout">
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
                                    <p>欢迎来到点助商户会员注册页面<br />
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
                                            <asp:TextBox runat="server" CssClass="regUserName" ID="tbxUserName" placeholder="手机号码/电子邮箱" ValidationGroup="vg_UserName" ClientIDMode="Static"></asp:TextBox>
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
                                                    class="agreeLIC-a" href="#">点助服务协议</a>》，并同意所有条款。</label>
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
            </div>
        </div>
        <div class="footer">
        </div>
    </div>
</body>
<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
<!--[if IE 8]>
<script src="/js/jquery.placeholder.min.js" type="text/javascript"></script>
<script>$('input, textarea').placeholder();</script>
<![endif]-->
<script src="/js/login_reg.js" type="text/javascript"></script>
</html>
