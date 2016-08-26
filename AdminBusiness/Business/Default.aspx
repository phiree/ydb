<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="~/Business/Default.aspx.cs" Inherits="Business_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<!--店铺列表-->
<html lang="en">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-stand|ie-comp">
    <meta name="description" content="一点办后台管理系统" />
    <meta name="keywords" content="一点办" />
    <title>商家后台主页</title>
    <link rel="shortcut icon" href="/favicon.ico" />
    <link rel="bookmark" href="/favicon.ico" type="image/x-icon"　/>
    <link href="/css/main.css" rel="stylesheet" type="text/css" />
    <!--有字库连接-->
    <link href='http://api.youziku.com/webfont/CSS/568e353ff629d80f4cd910a7' rel='stylesheet' type='text/css' />
    <!--[if lte IE 9]>
        <script src="/js/plugins/html5shiv.min.js"></script>
        <script src="/js/plugins/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="wrap">
        <form id="form1" runat="server">
            <div class="navbar navbar-default navbar-static-top" role="navigation">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <div class="navbar-brand brand-logo">
                            <a id="logo" href="/Business/default.aspx"></a>
                        </div>
                        <h1 class="navbar-brand brand-h cssc0a9477146a8">
                            一点办商户管理系统
                        </h1>
                        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#ydb-navbar-collapse" aria-expanded="false">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                    </div>
                    <div class="collapse navbar-collapse" id="ydb-navbar-collapse">
                        <ul class="nav navbar-nav navbar-right  ">
                            <li role="presentation">
                                <div class="loginName">
                                    <span class="icon navbar-icon-acc"></span>
                                    <asp:LoginName  ID="LoginName1" CssClass="v-m" runat="server" />
                                </div>
                            </li>
                            <li role="presentation">
                                <div class="navbar-a-wrap">
                                    <a href="/account/security.aspx" class="icon navbar-icon navbar-icon-set"></a>
                                </div>
                            </li>
                            <li role="presentation">
                                <div class="navbar-a-wrap">
                                    <asp:LoginStatus ID="LoginStatus1" CssClass="icon navbar-icon navbar-icon-logout"  formnovalidate   runat="server" LogoutText=""/>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="content-layout-fluid">
                <div class="content hide" id="business-list">
                    <div class="content-head full-head">
                        <h3 class="cont-h2">
                            店铺列表
                        </h3>
                        <a id="addNewBusiness" class="btn btn-gray-light" href="/Business/edit.aspx"><strong>+</strong>&nbsp;新建店铺</a>
                    </div>
                    <div class="content-main">
                        <div class="animated fadeInUpSmall">
                            <div class="container">
                                <div class="row">
                                    <div class="col-md-12">
                                        <!--店铺列表 start-->
                                        <div >
                                            <ul class="biz-list" id="bizList">
                                                <asp:Repeater runat="server" ID="rptBusinessList"
                                                              OnItemCommand="rptBusinessList_ItemCommand">
                                                    <ItemTemplate>
                                                        <li class="biz-list-item">
                                                            <div class="biz-head">店铺</div>
                                                            <div class="biz-item-main">
                                                                <div class="biz-item-h"><%#Eval("Name")%></div>
                                                                <div class="biz-item-m">
                                                                    <div class="biz-img">
                                                                        <img src='<%# ((Dianzhu.Model.BusinessImage)Eval("BusinessAvatar")).Id!=Guid.Empty?"/ImageHandler.ashx?imagename="+HttpUtility.UrlEncode(((Dianzhu.Model.BusinessImage)Eval("BusinessAvatar")).ImageName)+"&width=70&height=70&tt=3":"../images/common/touxiang/touxiang_70_70.png" %>'/>
                                                                    </div>
                                                                    <div class="biz-info m-b10">
                                                                        <div class="m-b10">
                                                                            <div class="biz-info-h">店铺地址</div>
                                                                            <div class="biz-info-d">
                                                                                <%#Eval("Address")%></div>
                                                                        </div>
                                                                        <div class="m-b10">
                                                                            <div class="biz-info-h">店铺电话</div>
                                                                            <div class="biz-info-d d-p">
                                                                                <%#Eval("Phone")%></div>
                                                                        </div>
                                                                        <div>
                                                                            <div class="biz-info-h">店铺介绍</div>
                                                                            <div class="biz-info-d l-h18 biz-intro-fixed">
                                                                                <%#Eval("Description")%></div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="biz-href">
                                                                        <a class="biz-into" href='Detail.aspx?businessId=<%#Eval("Id") %>'>进入店铺</a>
                                                                        <asp:Button CssClass="biz-delete"
                                                                                    text="删除店铺"
                                                                                    title="删除店铺"
                                                                                    runat="server"
                                                                                    OnClientClick="javascript:return confirm('确定要删除该店铺么?')"
                                                                                    CommandArgument='<%#Eval("Id") %>'
                                                                                    CommandName="delete"/>
                                                                    </div>

                                                                </div>
                                                                <div class="biz-item-svc">
                                                                    <ul class="svc-list clearfix">
                                                                        <asp:Repeater runat="server"
                                                                                      ID="rptServiceType">
                                                                            <ItemTemplate>
                                                                                <li class='svc-li'>
                                                                                    <a class='svc-icon svcType-b-icon-<%#Eval("Id") %>'></a>
                                                                                    <div class="svc-item-h">
                                                                                        <%#Eval("Name") %>
                                                                                    </div>
                                                                                </li>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                            <!--</div>-->
                                                            <div class="d-hr"></div>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                        <!--店铺列表 end-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="content hide" id="business-new">
                    <div class="content-head full-head">
                        <h3 class="cont-h2">
                            店铺列表
                        </h3>
                    </div>
                    <div class="content-main">
                        <div class="animated fadeInUpSmall" >
                            <div class="empty-biz">
                                <div class="empty-biz-icon"></div>
                                <a id="firstAddBusiness" class="empty-biz-add">点击创建新店铺 <strong>+</strong></a>
                                <!--<p id="firstAddMsg" class="empty-biz-msg hide">-->
                                    <!--感谢您的使用一点办，为了给您提供更好的服务，建议您进入&nbsp;<a style="text-decoration:underline;" href="/account/security.aspx?businessId=<%=Request["businessid"] %>">帐号安全</a>&nbsp;绑定您的手机号码。-->
                                <!--</p>-->
                            </div>
                        </div>
                    </div>
                </div>
                <div class="footer">
                    <a href="http://www.miibeian.gov.cn/">琼ICP备15000297号-4</a> Copyright © 2015 All Rights
                    Reserved
                </div>
            </div>
            <div id="newBusslightBox" class="dis-n newBiz">
                <div class="newBiz-h">
                    创建新的店铺
                </div>
                <div class="newBiz-m">
                    <div class="newBiz-row">
                        <div class="newBiz-h5">店铺名称</div>
                        <div class="newBiz-input">
                            <input class="input-mid" runat="server" id="tbxName" type="text" data-toggle="tooltip"
                                   data-placement="top" title="请填写店铺名称"/>
                        </div>
                    </div>


                    <div class="newBiz-row">
                        <div class="newBiz-h5">店铺地址</div>
                        <div class="newBiz-input">
                            <input class="input-mid" runat="server" id="tbxAddress" type="text" data-toggle="tooltip"
                                   data-placement="top" title="请填写店铺的详细地址"/>
                            <input type="hidden" focusid="setAddress" runat="server" clientidmode="Static"
                                   id="hiAddrId" name="addressDetailHide"/>
                        </div>
                    </div>
                    <div class="newBiz-row">
                        <div class="newBiz-h5">店铺电话</div>
                        <div class="newBiz-input">
                            <input class="input-mid" runat="server" id="tbxContactPhone" type="text"
                                   data-toggle="tooltip" data-placement="top" title="请填写店铺的联系电话"/>
                            <!--true时为已填写手机-->
                            <input id="hiCreateID" type="hidden" runat="server" value="false"/>
                        </div>
                    </div>
                    <div class="newBiz-row dis-n">
                        <div class="cont-h5">
                            店铺邮箱或网址
                        </div>
                        <div class="newBiz-input">
                            <input class="input-mid" runat="server" id="tbxWebSite" type="text" data-toggle="tooltip"
                                   data-placement="top" title="请填写店铺邮箱或网址"/>
                        </div>
                    </div>
                    <div class="newBiz-row">
                        <div class="newBiz-h5">店铺介绍</div>
                        <div class="newBiz-input v-t">
                    <textarea class="input-textarea buss-textarea" runat="server"
                              id="tbxDescription" data-toggle="tooltip" data-placement="top"
                              title="请填写简单店铺介绍"></textarea>
                        </div>
                    </div>

                    <div class="newBiz-row">
                        <div class="t-c">
                            <input class="btn btn-info" type="submit" runat="server" id="btnCreate"
                                   onserverclick="btnCreate_Click" value="创建"/><input
                                class="lightClose btn btn-cancel-light m-l20" type="button" value="取消"/>
                        </div>
                    </div>
                </div>

            </div>
        </form>
    </div>
</body>
<script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>static/Scripts/jquery-1.11.3.min.js"></script>
<script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/jquery.validate.js"></script>
<script src="<% =Dianzhu.Config.Config.GetAppSetting("cdnroot")%>/static/Scripts/additional-methods.js"></script>
<script src="/js/vendors/bootstrap.js"></script>
<script src="/js/apps/global.js?v=1.0.0"></script>
<script src="/js/plugins/jquery.lightbox_me.js"></script>
<script src="/js/apps/validation/validation_business_add.js?v=1.0.0"></script>
<script src="/js/apps/pages/business.js?v=1.0.0"></script>
<script>
    $(function () {
        (function (){
            if ($("#bizList").find(".biz-list-item").length == 0) {
                $("#business-new").removeClass("hide");
            } else {
                $("#business-list").removeClass("hide");
            }

            if ( !($("#hiCreateID").attr("value") === "true")) {
                $("#firstAddMsg").removeClass("hide");
            }
        })();

        (function(){
            var $lightBox = $('#newBusslightBox');
            $("#addNewBusiness, #firstAddBusiness").click(function (e) {
                e.preventDefault();
                $lightBox.lightbox_me({centered: true}).appendTo($("form:first"));
            });
        })();

        $('[data-toggle="tooltip"]').tooltip(
            {
                placement: 'right',
                delay: {show : 500, hide : 0},
                trigger: 'hover'
            }
        );
    });
</script>
<script>
    function loadBaiduMapScript() {
        var script = document.createElement("script");
        script.src = "http://api.map.baidu.com/api?v=2.0&ak=n7GnSlMbBkmS3BrmO0lOKKceafpO5TZc&callback=initialize";
        document.body.appendChild(script);
    }

    $(document).ready(function () {
        loadBaiduMapScript();
    })
</script>
<%if (!Request.IsLocal)
    { %>
<script>
    (function(){
        var cnzz_protocol = (("https:" == document.location.protocol) ? " https://" : " http://");
        document.write(unescape("%3Cspan id='cnzz_stat_icon_1256240621' style='display:none'%3E%3C/span%3E%3Cscript src='"
                + cnzz_protocol + "s4.cnzz.com/z_stat.php%3Fid%3D1256240621%26show%3Dpic1' type='text/javascript'%3E%3C/script%3E"));
    })();
</script>
<% }%>
</html>
