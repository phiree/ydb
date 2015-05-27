<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="css/login_reg.css" type="text/css" />
</head>
<body>
    <div class="wrap">
       
        <div class="head">
        </div>
        <div class="main">
            <div class="layout">
                <div class="wrap-login"> <form id="form1" runat="server">
                    <div class="conLogin">
                        <div class="conLogo clearfix">
                            <div class="logo-l">
                                <p>
                                    解决万事找对点，一切帮你妥妥办<br />
                                    让你生活有变化</p>
                            </div>
                            <div class="logo-r">
                                <div class="logo">
                                    <img src="image/login_reg/icon_1.png" alt="logo" />
                                </div>
                                <div class="logoTitle">
                                </div>
                            </div>
                        </div>
                        <div class="conMain main-login clearfix">
                            <div class="box-l">
                                <p>
                                    <em>0</em>2o的终极商业模式</p>
                                <p>
                                    <em>1</em>个全新的平台</p>
                                <p>
                                    <em>2</em>4小时服务</p>
                                <p>
                                    <em>3</em>步接单</p>
                            </div>
                            <div class="box-r">
                                <p class="date">
                                    四月 08 , 2015</p>
                                <div class="clearfix">
                                    <p class="username">
                                        <label class="usernameIcon" for="username">
                                            <i></i>
                                        </label>
                                        <input id="username" type="text" placeholder="手机号/会员名/邮箱" />
                                    </p>
                                    <!--<i id="userCheck" class="f-l"></i>-->
                                </div>
                                <p class="password">
                                    <label class="passwordIcon" for="password">
                                        <i></i>
                                    </label>
                                    <input id="password" type="password" placeholder="密码" value="" />
                                </p>
                                <div class="loginBox">
                                    <input type="submit" class="loginBtn" value="" />
                                    <p class="savePass">
                                        <input id="savePass" type="checkbox" /><label for="savePass">记住密码</label></p>
                                </div>
                                <!--<p id="testAjax"></p>-->
                                <p class="doReg">
                                    <a href="register.aspx"><img src="image/login_reg/zhuce_1.png">注册会员</a></p>
                            </div>
                        </div>
                    </div> </form>
                </div>
              
            </div>
            <div class="conReg">
            </div>
        </div>
        <div class="footer">
        </div>
       
    </div>
</body>
<script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/vendor/jquery.js"></script>
<script src="js/login_reg.js" type="text/javascript"></script>
</html>
