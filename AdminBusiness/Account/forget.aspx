<%@ Page Title="" Language="C#"   AutoEventWireup="true" CodeFile="forget.aspx.cs" Inherits="forget" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办密码找回" />
    <meta name="keywords" content="一点办" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>一点办密码找回</title>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
    <script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jqueryui/jquery-ui.min-1.10.4.js"></script>

    <link rel="Stylesheet" href="/css/base.css" type="text/css" />
    <link rel="Stylesheet" href="/css/login_reg.css" type="text/css" />
</head>
<body>
    <div class="reg-wrap">
        <div class="main">
            <div class="layout">
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
                                    <p>忘记密码了？输入邮箱找回。</p>
                                </div>
                                <div class="logo-title-r">
                                    <i class="icon logoTitle"></i>
                                </div>
                            </div>

                        </div>
                        <div class="conMain main-reg clearfix">
                            <div class="reg-box">
                                <div class="reg-box-userName">
                                    <div class="reg-box-l"><p>请输入邮箱</p></div>
                                    <div class="reg-box-r">
                                        <div class="reg-input">
                                            <asp:TextBox runat="server" ID="tbxEmail" ></asp:TextBox>
                                        </div>
                                        <div class="m-b10"><asp:Label CssClass="f-s14" runat="server" ID="lblMsg"></asp:Label></div>
                                        <div><asp:Button runat="server" CssClass="reset-Pass-btn" ID="btnRecover" OnClick="btnRecover_Click" Text="重置密码" /></div>
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
<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
<script src="/js/jquery.lightbox_me.js" type="text/javascript"></script>
<!--[if lte IE 9]>
<script src="/js/jquery.placeholder.min.js" type="text/javascript"></script>
<script>$('input, textarea').placeholder();</script>
<![endif]-->
<script src="/js/login_reg.js" type="text/javascript"></script>
</html>
