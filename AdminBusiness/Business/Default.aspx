
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Business/Default.aspx.cs" Inherits="Business_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--<!doctype html>-->
<!--店铺列表-->
<html lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>商家后台主页</title>
    <link href="/js/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="/js/metisMenu/metisMenu.css" rel="stylesheet" type="text/css" />
    <link href="/js/bootstrap/css/onePointFive-custom.css" rel="stylesheet" type="text/css" />
    <link href="/css/base.css" rel="stylesheet" type="text/css" />
    <%--<link href="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jqueryui/themes\jquery-ui-1.10.4.custom\css\custom-theme\jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />--%>
    <script src="/js/html5shiv.min.js"></script>
    <script src="/js/respond.min.js"></script>
    <script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
</head>
<body>
        <div class="wrap">
            <div class="mainContainer">
                <form id="form1" runat="server">
                    <div class="wrapper">
                        <div class="navbar navbar-default navbar-static-top" role="navigation">
                            <div class="container-fluid">
                                <div class="navbar-header">
                                      <!--<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">-->
                                    <!--<span class="sr-only">Toggle navigation</span>-->
                                    <!--<span class="icon-bar"></span>-->
                                    <!--<span class="icon-bar"></span>-->
                                    <!--<span class="icon-bar"></span>-->
                                  <!--</button>-->
                                  <a class="navbar-brand" href="#">
                                       <img alt="一点半" src="">
                                  </a>
                                  <a class="navbar-brand" >
                                    一点半商户后台管理
                                    </a>
                                </div>
                                <ul class="nav navbar-nav navbar-right">

                                    <li class="dropdown">
                                      <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Dropdown <span class="caret"></span></a>
                                      <ul class="dropdown-menu">
                                        <li><a href="#">Action</a></li>
                                        <li><a href="#">Another action</a></li>
                                        <li><a href="#">Something else here</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Separated link</a></li>
                                      </ul>
                                    </li>
                                    <li role="presentation" class="disabled" ><a href="#">Link</a></li>
                                  </ul>
                            </div>
                        </div>

                        <!--<div class="clearfix">-->
                            <!--<div class="sidebar">-->
                                    <!--<aside class="sidebar">-->
                                      <!--<nav class="sidebar-nav">-->
                                        <!--<ul class="metismenu" id="menu">-->
                                          <!--<li>-->
                                            <!--<a href="#">我的商铺</a>-->
                                          <!--</li>-->
                                          <!--<li>-->
                                            <!--<a href="#">我的服务</a>-->
                                          <!--</li>-->
                                          <!--<li role="presentation" class="disabled">-->
                                            <!--<a href="#" >我的订单</a>-->
                                            <!--&lt;!&ndash;<a href="#">Menu 2 <span class="glyphicon arrow"></span></a>&ndash;&gt;-->
                                          <!--</li>-->
                                        <!--</ul>-->
                                      <!--</nav>-->
                                    <!--</aside>-->
                            <!--</div>-->
                         <!--</div>-->

                   <asp:Repeater runat="server" ID="rptBusinessList">
                   <ItemTemplate>
                       <div class="block">
                       <%--我的店铺:<%#Eval("Name")%>--%>
                       <%--<a class="nav-btn-bg" href='Edit.aspx?businessId=<%#Eval("Id") %>'><%#Eval("Name") %>编辑我的店铺</a>--%>
                       <a class="nav-btn-bg" href='Detail.aspx?businessId=<%#Eval("Id") %>'><%#Eval("Name") %>参看我的店铺</a>
                       </div>
                   </ItemTemplate>
                   </asp:Repeater>
                   </div>
                </form>
            </div>
        </div>
</body>
<script src="/js/bootstrap/js/bootstrap.js"></script>
<script src="/js/metisMenu/metisMenu.js"></script>
<script>
    $(function(){
        $("#menu").metisMenu();
    })

</script>
</html>
