<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="~/Business/Default.aspx.cs" Inherits="Business_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<!--店铺列表-->
<html lang="en">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="description" content="一点办后台管理系统" />
    <meta name="keywords" content="一点办" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>商家后台主页</title>
    <link href="/css/main.css" rel="stylesheet" type="text/css" />
    <!--<link href="/js/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />-->
    <!--<link href="/js/metisMenu/metisMenu.css" rel="stylesheet" type="text/css" />-->
    <!--<link href="/css/base.css" rel="stylesheet" type="text/css" />-->
    <!--<link href="/css/onePointFive-custom.css" rel="stylesheet" type="text/css" />-->
    <!--<link href="/css/business.css" rel="stylesheet" type="text/css" />-->
    <!--<link href="/css/animate.css" rel="stylesheet" type="text/css" />-->
    <!--<link href="/css/less/biz-list.css" rel="stylesheet" type="text/css" />-->
    <!--<link href="/css/validation.css" rel="stylesheet" type="text/css" />-->
    <script src="/js/html5shiv.min.js"></script>
    <script src="/js/respond.min.js"></script>
</head>
<body>
    <div class="wrap">
        <form id="form1" runat="server">
            <div class="navbar navbar-default navbar-static-top" role="navigation">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <a class="navbar-brand brand-logo" href="/Business/default.aspx">
                            <div id="logo"></div>
                        </a>
                        <a class="navbar-brand brand-h">一点办商户管理系统
                        </a>
                    </div>
                    <ul class="nav navbar-nav navbar-right">

                        <li class="dropdown nav-li-bj">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true"
                               aria-expanded="false">
                                <asp:LoginName ID="LoginName1" CssClass="v-m" runat="server"/>
                                <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li id="accountNum"><a href="/account/security.aspx?businessId=<%=Request[" businessid"]
                                    %>">帐号安全</a></li>
                            </ul>
                        </li>
                        <li role="presentaion" class="nav-li-bj ">
                            <asp:LoginStatus ID="LoginStatus1" CssClass="LoginStatus" formnovalidate LogoutText=""
                                             runat="server"/>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="content-fluid">
                <div class="content-head">
                    <h1 class="cont-h1">
                        店铺列表
                    </h1>
                    <a id="addNewBusiness" href="/Business/edit.aspx"><strong>+</strong>&nbsp;新建店铺</a>
                </div>
                    <div class="hide" id="business-main">
                        <div class="grid-container">
                            <div class="grid-row-full">
                                <div class="grid-col-12">
                                    <!--店铺列表 start-->
                                    <div class="biz-list-wrap">
                                        <ul class="biz-list" id="bizList">
                                            <asp:Repeater runat="server" ID="rptBusinessList"
                                                          OnItemCommand="rptBusinessList_ItemCommand">
                                                <ItemTemplate>
                                                    <li class="biz-list-item">
                                                        <!--<div class="biz-item-left">-->
                                                            <!--<div class="biz-tip">-->
                                                                <div class="biz-head">店铺</div>
                                                                <!--<i class="biz-tip-icon icon"></i>-->
                                                            <!--</div>-->
                                                        <!--</div>-->
                                                        <!--<div class="biz-item-right animated fadeInUpSmall">-->
                                                            <div class="biz-item-main">
                                                                <div class="biz-item-h"><%#Eval("Name")%></div>
                                                                <div class="biz-item-m">
                                                                    <div class="grid-row">
                                                                        <div class="gird-col-12">
                                                                            <div class="biz-img">
                                                                                <img src='<%# ((Dianzhu.Model.BusinessImage)Eval("BusinessAvatar")).Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(((Dianzhu.Model.BusinessImage)Eval("BusinessAvatar")).ImageName)+"&width=70&height=70&tt=3":"../image/myshop/touxiang_70_70.png" %>'/>
                                                                            </div>
                                                                            <div class="biz-info m-b10">
                                                                                <div class="m-b10">
                                                                                    <h3 class="biz-info-h">店铺电话</h3>
                                                                                    <p class="biz-info-d d-p">
                                                                                        <%#Eval("Phone")%></p>
                                                                                </div>
                                                                                <div class="m-b10">
                                                                                    <h3 class="biz-info-h">详细地址</h3>
                                                                                    <p class="biz-info-d">
                                                                                        <%#Eval("Address")%></p>
                                                                                </div>
                                                                                <div>
                                                                                    <h3 class="biz-info-h">店铺介绍</h3>
                                                                                    <p class="biz-info-d l-h18 biz-intro-fixed">
                                                                                        <%#Eval("Description")%></p>
                                                                                </div>
                                                                            </div>
                                                                            <div class="biz-href">
                                                                                <a class="biz-into" href='Detail.aspx?businessId=<%#Eval("Id") %>'>进入店铺</a>
                                                                                <asp:Button CssClass="biz-delete" title="删除店铺"
                                                                                            runat="server"
                                                                                            OnClientClick="javascript:return confirm('确定要删除该店铺么?')"
                                                                                            CommandArgument='<%#Eval("Id") %>'
                                                                                            CommandName="delete"/>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="biz-item-svc">
                                                                    <ul class="svc-list">
                                                                        <asp:Repeater runat="server"
                                                                                      ID="rptServiceType">
                                                                            <ItemTemplate>
                                                                                <li class='svc-li svcType-icon-<%#Eval("Id") %>'>
                                                                                    <input type="hidden"
                                                                                           value='<%#Eval("Id") %>'/>

                                                                                    <p class="svc-item-h">
                                                                                        <%#Eval("Name") %></p>
                                                                                </li>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>


                                                                    </ul>
                                                                </div>

                                                                <!--<div class="">-->

                                                                <!--</div>-->
                                                            </div>
                                                        <!--</div>-->
                                                            <div class="d-hr"></div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                        <!--<div class="biz-list-bg">-->
                                            <!--<div class="biz-bg-t"></div>-->
                                            <!--<div class="biz-bg-m"></div>-->
                                            <!--<div class="biz-bg-b"></div>-->
                                        <!--</div>-->
                                    </div>
                                    <!--店铺列表 end-->
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="business-new" class="animated fadeInUpSmall hide" >
                        <div class="empty-biz">
                                <a id="firstAddBusiness" class="empty-biz-add m-b20">点击创建新店铺</a>
                                <p id="firstAddMsg" class="empty-biz-msg hide">
                                    感谢您的使用一点办，为了给您提供更好的服务，建议您进入<a style="text-decoration:underline;" href="/account/security.aspx?businessId=<%=Request["businessid"] %>">帐号安全</a>绑定您的手机号码。
                                </p>
                        </div>
                    </div>
                    <!--true时为已填写手机-->
                    <input id="hiCreateID" type="hidden" runat="server" value="false"/>
            </div>
            <div class="footer">
                <a href="http://www.miibeian.gov.cn/">琼ICP备15000297号-4</a> Copyright © 2015 All Rights
                Reserved
            </div>
            <div id="newBusslightBox" class="dis-n newBiz">
                <div class="grid-container">
                    <div class="grid-row">
                        <div class="grid-col-12">
                            <div class="newBiz-h">
                                创建新的店铺
                            </div>
                        </div>
                    </div>
                    <div class="newBiz-m">
                        <div class="newBiz-row">
                            <p class="newBiz-h5">店铺名称</p>
                            <div class="newBiz-input">
                                <input class="input-mid" runat="server" id="tbxName" type="text" data-toggle="tooltip"
                                       data-placement="top" title="请填写店铺名称"/>
                            </div>
                        </div>


                                <div class="newBiz-row">
                                    <p class="newBiz-h5">店铺地址</p>
                                    <div class="newBiz-input">
                                        <input class="input-mid" runat="server" id="tbxAddress" type="text" data-toggle="tooltip"
                                               data-placement="top" title="请填写店铺的详细地址"/>
                                        <input type="hidden" focusid="setAddress" runat="server" clientidmode="Static"
                                               id="hiAddrId" name="addressDetailHide"/>
                                    </div>
                                </div>
                                <div class="newBiz-row">
                                    <p class="newBiz-h5">店铺电话</p>
                                    <div class="newBiz-input">
                                        <input class="input-mid" runat="server" id="tbxContactPhone" type="text"
                                               data-toggle="tooltip" data-placement="top" title="请填写店铺的联系电话"/>
                                    </div>
                                </div>

                                <div class="newBiz-row dis-n">
                                    <p class="cont-h5">
                                        店铺邮箱或网址
                                    </p>
                                    <div class="newBiz-input">
                                        <input class="input-mid" runat="server" id="tbxWebSite" type="text" data-toggle="tooltip"
                                               data-placement="top" title="请填写店铺邮箱或网址"/>
                                    </div>
                                </div>
                        <div class="newBiz-row">
                            <p class="newBiz-h5">店铺介绍</p>
                            <div class="newBiz-input v-t">
                                <textarea class="input-textarea buss-textarea" runat="server"
                                          id="tbxDescription" data-toggle="tooltip" data-placement="top"
                                          title="请填写简单店铺介绍"></textarea>
                            </div>
                        </div>

                        <div class="newBiz-row">
                                    <p class="t-c">
                                        <input class="btn btn-info" type="submit" runat="server" id="btnCreate"
                                               onserverclick="btnCreate_Click" value="创建"/><input
                                            class="lightClose btn btn-cancel m-l20" type="button" value="取消"/>
                                    </p>
                                </div>
                    </div>
                </div>


            </div>
        </form>

    </div>
</body>
<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>static/Scripts/jquery-1.11.3.min.js"></script>
<script src="/js/bootstrap/js/bootstrap.js"></script>
<script src="/js/metisMenu/metisMenu.js"></script>
<script src="/js/global.js"></script>
<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery.validate.js"></script>
<script src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/additional-methods.js" type="text/javascript"></script>
<script type="text/javascript" src="/js/jquery.lightbox_me.js"></script>
<script src="/js/validation_business_add.js" type="text/javascript"></script>
<script type="text/javascript" src="/js/business.js"></script>
<script>
    $(function () {
        if ($("#bizList").find(".biz-list-item").length == 0) {
            $("#business-new").removeClass("hide");
        } else {
            $("#business-main").removeClass("hide");
        }
    });

    $(function () {
        $("#addNewBusiness").click(function (e) {
            $('#newBusslightBox').lightbox_me({
                centered: true
            });
            $("#newBusslightBox").appendTo($("form:first"));
            e.preventDefault();
        });

        $("#firstAddBusiness").click(function (e) {
            $('#newBusslightBox').lightbox_me({
                centered: true
            });
            $("#newBusslightBox").appendTo($("form:first"));
            e.preventDefault();
        });

        if ($("#hiCreateID").attr("value") == "true") {
            return;
        } else {
            $("#firstAddMsg").removeClass("hide");
            return;
        }
    });

    $(function () {
        $("#menu").metisMenu();
        $('[data-toggle="tooltip"]').tooltip(
                {
                    placement: 'right',
                    delay: {show : 500, hide : 0},
                    trigger: 'hover'
                }
        );
    });

    $(function () {
        $($("form")[0]).validate(
                {
                    errorElement: "p",
                    errorPlacement: function (error, element) {
                        error.appendTo(element.parent());
                    },
                    rules: business_validate_rules,
                    messages: business_validate_messages
                }
        );
    });
</script>
<script>
    function loadBaiduMapScript() {
        var script = document.createElement("script");
        script.src = "http://api.map.baidu.com/api?v=2.0&ak=wMCvOKib7TV9tkVBUKGCLAQW&callback=initialize";
        document.body.appendChild(script);
    }

    $(document).ready(function () {
        loadBaiduMapScript();
    })
</script>
<%if (!Request.IsLocal)
    { %>
<script type="text/javascript">
    var cnzz_protocol = (("https:" == document.location.protocol) ? " https://" : " http://");
    document.write(unescape("%3Cspan id='cnzz_stat_icon_1256240621' style='display:none'%3E%3C/span%3E%3Cscript src='"
            + cnzz_protocol + "s4.cnzz.com/z_stat.php%3Fid%3D1256240621%26show%3Dpic1' type='text/javascript'%3E%3C/script%3E"));
</script>
<% }%>
</html>
