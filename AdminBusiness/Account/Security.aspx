<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Security.aspx.cs" Inherits="Account_Security" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="/css/myshop.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="mainContent clearfix">
    <form class="clearfix" action="" method="post" >
        <div class="leftContent" id="leftCont">
            <div>
                <ul>
                    <li><a href="/account/"><img id="me1" src="/image/jibenxinxi_4.png" /></a></li>
                    <li><a href="/account/security.aspx"><img id="me2" src="/image/zhanghaoanquan_1.png" /></a></li>
                </ul>
            </div>
        </div>
        <div class="rightContent" id="rightCont">
            <div class="userInfoArea">
                <div class="infoTilte1">
                    <span>商家基本信息</span>
                    <img src="/image/incotou.jpg" />
                </div>
                <div class="headInfoArea">
                    <div class="headDecoration1"></div>
                    <div class="headImage">
                        <img src="/image/userheadimg.png" />
                    </div>
                    <div class="headInfo">
                        <input type="text" name="inputShopName" value="请输入您的店铺名称" class="inputShopName" />
                        <br />
                        <span class="InfoCompletetxt">资料完成程度</span>
                        <div class="InfoPercentage">
                            <div class="InfoComplete">
                                <span class="progress"></span>
                            </div>
                            <span class="completePercentage">90%</span>
                        </div>
                    </div>
                    <div class="headEditImg"></div>
                </div>
            </div>
            <div class="decoration3">
                <img src="/image/shopicon.jpg" />
                <span class="ShopDetails">账号信息相关</span>
            </div>
            <div class="secret-cont">
                <div class="secret-m standard-info">
                    <div class="secret-title t-1">基础信息</div>
                    <div class="secret-detail">
                        <div><span class="secret-d-t">登录账号&nbsp;:</span><span><%=CurrentUser.UserName%></span></div>
                         <div><span class="secret-d-t">上次登陆&nbsp;:</span><span><%=CurrentUser.LastLoginTime %></span> </div>
                    </div>
                </div>
                <div class="secret-m secert-info">
                    <div class="secret-title t-2">账户安全信息</div>
                    <div class="secret-detail">
                        <div class="clearfix">
                            <span class="secret-d-t">登陆密码&nbsp;:</span><span class="secret-d-p d-inb" >安全性高的密码可以使账号更安全，建议您定期更换密码，且设置一个包含数字和字母，并长度超过6位数的密码</span>
                            <a class="m-l20" href="Changepassword.aspx">修改</a>
                            <div class="secret-d-tip"><i class="icon icon-scret-1"></i><div>强</div></div>
                        </div>
                        <div class="clearfix">
                            <span class="secret-d-t">身份认证&nbsp;:</span><span class="secret-d-p d-inb">重新上传负责人证件照</span>
                            <a class="m-l20" href="default.aspx">修改</a>
                            <div class="secret-d-tip"><i class="icon icon-scret-2"></i><div>已认证</div></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
<script type="text/javascript" src="js/global.js"></script>
<script type="text/javascript">
    var m1 = true;
    var m2 = false;
    function meinit(str) {
        m1 = false;
        m2 = false;
        str = true;
        $("#me1").attr("src", "/image/jibenxinxi_4.png");
        $("#me2").attr("src", "/image/zhanghaoanquan_2.png");

    }
    function myselect(divselectid, inputselectid) {
        // alert("ddd");


        var list = $(divselectid + " ul");
        var inputselect = $(inputselectid);
        $(divselectid + " cite").click(function () {
            if (list.css("display") == "none") {
                list.slideDown("fast");
            } else {
                list.slideUp("fast");
            }
        });
        var mouseisout = false;
        $(divselectid).mouseover(function () {
            mouseisout = false;
        });
        $(divselectid).mouseout(function () {
            mouseisout = true;
        });
        $(document).click(function () {
            if (mouseisout) {
                list.hide();
            }
        });

        $(divselectid + " ul li a").click(function () {
            var txt = $(this).text();
            $(divselectid + " cite").html(txt);
            var value = $(this).attr("value");
            inputselect.val(value);
            list.hide();
        });
    }
    $(document).ready(function () {
        $("#me1").click(function () {
            meinit(m1);

            $("#me1").attr("src", "/image/jibenxinxi_3.png");
            $("#userInfoAreaid").css("display", "block");
            $("#account").css("display", "none");


        });
        $("#me2").click(function () {
            meinit(m2);
            $("#me2").attr("src", "/image/zhanghaoanquan_1.png");
            $("#userInfoAreaid").css("display", "none");
            $("#account").css("display", "block");

        });
        $(".picture").hover(function () {
            $(".picDelBtn").css("display", "block");
            $(".picEditBtn").css("display", "block");

        }, function () {
            $(".picDelBtn").css("display", "none");
            $(".picEditBtn").css("display", "none");

        }


        );

        $(".picDelBtn").css("display", "none");
        $(".picEditBtn").css("display", "none");

        $(".progress").css("width", "90%");
        $(".completePercentage").html("90%");

        myselect("#WorkingTimeid", "#WorkingTime");
        myselect("#employeesid", "#employees");
        myselect("#DocumentTypeid", "#DocumentType");

    });
</script>
</asp:Content>

