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
                            <div class="logo-l">
                                <p>
                                    欢迎来到点助商户会员注册页面<br />
                                    开启移动智能O2O新历程</p>
                            </div>
                            <div class="logo-r">
                                <div class="logo regLogo">
                                        <img src="image/login_reg/zhuce_2.png" alt="logo" />
                                </div>
                                <div class="logoTitle">
                                </div>
                            </div>
                        </div>
                        <div class="conMain main-reg clearfix">
                            <div class="reg-user-l box-l">
                                <p>注册帐号</p>
                                <p>登录密码</p>
                                <p>确认密码</p>
                            </div>
                            <div class="box-r">
                                <div class="emailBox m-b10">
                                    <asp:TextBox runat="server" ID="tbxUserName" ValidationGroup="vg_UserName" ClientIDMode="Static"></asp:TextBox>
                                     <i id="userCheck" class="checkIcon"></i><span id="userCheckText" class="regWarnTexti"></span>
                                </div>
                                <!--<p class="rPage" id="usernameConf"></p>-->
                                <div class="regPs">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="regPs" TextMode="Password"></asp:TextBox>
                                    <i id="psChk" class="checkIcon"></i>
                                    <div id="passCheckText" class="regWarnText dis-n">密码不符合要求，要求6-20个字符</div>
                                </div>
                                <div class="regPsConf">
                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="regPsConf" TextMode="Password"></asp:TextBox>
                                    <i id="psConfChk" class="checkIcon"></i>
                                    <div id="passConfText" class="regWarnText dis-n">两次密码不一致</div>
                                </div>
                                <p class="agree">
                                    <input id="agreeLic" name="agreeLic" type="checkbox" value="yes" /><label class="v-m m-l10" for="agreeLic">我已经仔细阅读过《<a
                                        class="agreeLIC-a" href="#">点助服务协议</a>》，并同意所有条款。</label></p>
                                <!--<div class="buttonBox">-->
                                    <!--<input id="userConfirm"  type="button" class="regBtn"/>-->
                                <!--</div>-->
                                <div class="buttonBox">
                                    <!--<input type="button" id="userConfirmBack" class="userConfirm-Back"/>-->
                                    <asp:Button runat="server" ID="regPsSubmit" onClientClick="javascipt:return false;" ClientIDMode="Static" OnClick="regPsSubmit_OnClick"
                                        CssClass="regBtn" />
                                </div>
                                <p class="doLogin">
                                    <a class="logReg-a" href="login.aspx">
                                        <img src="image/login_reg/back_2.png">返回登录</a>
                                </p>
                            </div>
                        </div>
                        <div class="conMain main-psw clearfix">
                            <div class="box-l">
                            </div>
                            <div class="box-r">
                                <%--<iframe name="uploadForm1" style="display: none"></iframe>--%>
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
<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>

<script src="/js/InlineTip.js" type="text/javascript"></script>
<script src="/js/login_reg.js" type="text/javascript"></script>

</html>
