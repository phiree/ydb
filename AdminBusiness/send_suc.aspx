<%@ Page Language="C#" AutoEventWireup="true" CodeFile="send_suc.aspx.cs" Inherits="register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" href="/css/base.css" type="text/css" />
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
                        <div class="conLogo clearfix">
                            <div class="logo">
                                <div class="logo-title-l"></div>
                                <div class="logo-title-r">
                                    <img src="image/login_reg/zhuce_2.png" alt="logo" />
                                </div>
                            </div>
                            <div>
                                <div class="logo-title-l">
                                    <p>
                                        发送成功<br />
                                        开启移动智能O2O新历程</p>
                                </div>
                                <div class="logo-title-r">
                                    <i class="icon logoTitle"></i>
                                </div>
                            </div>
                        </div>
                        <div class="conMain main-done clearfix">
                            <div class="regComp">
                                <i class="icon icon-regComp"></i><span>
                                    验证邮件已经成功,请进入您的邮箱根据提示完成验证</span>
                            </div>
                            <div class="buttonBox t-c">
                            <a href="/account/security.aspx" class="compBtn"></a>  
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
<!--<script src="js/login_reg.js" type="text/javascript"></script>-->
</html>
