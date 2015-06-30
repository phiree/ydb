<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register_suc.aspx.cs" Inherits="register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" href="css/login_reg.css" type="text/css" />
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
                                    <a href="#">
                                        <img src="image/login_reg/zhuce_2.png" alt="logo" /></a>
                                </div>
                                <div class="logoTitle">
                                </div>
                            </div>
                        </div>
                     
                        <div class="conMain main-done clearfix">
                            <div class="regComp">
                                <img src="image/login_reg/zhengque_1.png"><span>恭喜你<br />
                                    已经注册成为点助会员</span>
                            </div>
                            <div class="buttonBox t-c">
                            <a href="login.aspx" class="compBtn"></a>  
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
<script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min"></script>
<script src="js/login_reg.js" type="text/javascript"></script>
</html>
