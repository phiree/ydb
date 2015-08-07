
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
    <link href="/css/business.css" rel="stylesheet" type="text/css" />
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
                                      <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><asp:LoginName ID="LoginName1" runat="server" /><span class="caret"></span></a>
                                      <ul class="dropdown-menu">
                                        <li><a href="#">帐号安全</a></li>
                                      </ul>
                                    </li>
                                    <li role="presentaion" ><asp:LoginStatus ID="LoginStatus1" formnovalidate  runat="server"  /></li>
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
                <div class="business-container">
                    <div class="business-add">
                           <a class="btn btn-info" href="Edit.aspx">+&nbsp;新建店铺</a>
                        </div>
                       <div class="business-list">
                           <asp:Repeater runat="server" ID="rptBusinessList">
                              <ItemTemplate>
                                  <div class="business-list-item m-b20">
                                    <div class="cont-container">
                                        <div class="cont-row">
                                            <div class="cont-col-4">

                                                <div class="business-h"><%#Eval("Name")%></div>
</div>
                                            <div class="cont-col-8">
                                                <div class="cont-row">
                                                    <div class="cont-col-12">
        <p class="business-default-intro">
            服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。服务介绍服务介绍，服务介绍服务介绍。
</p>
</div>
</div>
                                                <div class="cont-row">
                                                    <div class="cont-col-6">
                                                        <div class="business-note">
                                                        <p><span>联系电话：</span><span></span></p>
                                                        <p><span>从业时间：</span><span></span></p>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col-6">
                                                        <div class="business-note">
                                                            <p><span>店铺地址：</span><span></span></p>
                                                            <p><span>员工人数：</span><span></span></p>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="cont-row">
                                                    <div class="cont-col">
                                                        <p class="t-r"><a class="btn btn-info btn-into" href='Detail.aspx?businessId=<%#Eval("Id") %>'>进入店铺</a></p>
                                                    </div>

</div>
</div>
</div>
</div>
                                  </div>
                              </ItemTemplate>
                          </asp:Repeater>
                        </div>
                    </div>
                   <!--</div>-->
                   </div>
                   <div id="newBusslightBox" class="dis-n">
                       输入店铺名称：<input type="text" />
                       <input type="button" value="确认创建"/>
                       <input class="lightClose" type="button" value="残忍取消"/>
                   </div>
                </form>

            </div>
        </div>
</body>
<script src="/js/bootstrap/js/bootstrap.js"></script>
<script src="/js/metisMenu/metisMenu.js"></script>
<script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
<script>
    $(function(){
        $("#menu").metisMenu();
    })

    $("#addNewBusiness").click(function (e) {
            $('#newBusslightBox').lightbox_me({
                centered: true,
                onLoad : function(){

                }
            });
            e.preventDefault();
        });
</script>
</html>
