<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Account_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="/css/myshop.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="mainContent clearfix">
    <form class="clearfix" runat="server">
        <div class="leftContent" id="leftCont">
            <div>
                <ul>
                    <li><a href="myshop.html"><img id="me1" src="/image/jibenxinxi_3.png" /></a></li>
                    <li><a href="secret.html"><img id="me2" src="/image/zhanghaoanquan_2.png" /></a></li>
                </ul>
            </div>
        </div>
        <div class="rightContent" id="rightCont">
            <div id="userInfoAreaid">
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
                            <input runat="server" type="text" id="tbxName" name="inputShopName" value="请输入您的店铺名称" class="inputShopName" />
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
                <!--<div class="decoration1"></div>-->
                <!--<div class="decoration2"></div>-->
                <div class="decoration3">
                    <img src="/image/shopicon.jpg" />
                    <span class="ShopDetails">店铺详细资料</span>
                </div>
                <div class="ShopDetailsArea clearfix">
                    <div class="ShopDetailsAreaLeft">
                        <div class="LeftClearance1 shopIntroduced">
                            <p class="p_introduced">店铺介绍</p>
                            <p><textarea id="tbxIntroduced" runat="server" name="shopIntroduced">(可输入60个字)</textarea></p>
                        </div>
                        <div class="LeftClearance1 pinput">
                            <p class="p_ContactPhone">联系电话</p>
                            <p><input type="text" id="tbxContactPhone" runat="server" name="ContactPhone" /></p>
                        </div>
                        <div class="LeftClearance1 pinput">
                            <p class="p_addressDetail">详细店址</p>
                            <p><input type="text" id="tbxAddress" runat="server" name="addressDetail" /></p>
                        </div>
                        <div class="LeftClearance1 pinput">
                            <p class="p_email">邮箱地址</p>
                            <p><input type="text" runat="server" id="tbxEmail" name="email" /></p>
                        </div>
                        <div class="LeftClearance1 pinput">
                            <p class="p_WorkingTime">从业时间</p>
                            <p><input type="text" runat="server" id="tbxBusinessYears" name="email" /></p>
                        </div>
                        <div class="LeftClearance1 BusinessLicense">
                            <p class="p_BusinessLicense">营业执照</p>
                            <p><asp:FileUpload runat="server"  ID="fuBusinessLicence"/> <img src="/image/dianjishangchuan_1.png" /></p>
                        </div>
                    </div>
                    <div class="ShopDetailsAreaRight">
                        <div class="RightClearance1 ShopFigure">
                            <p class="p_ShopFigure">店铺图片展示</p>
                            <div>
                                <div class="picture">
                                    <img class="picEditBtn" src="/image/bianjji_1.png" />
                                    <img class="picDelBtn" src="/image/delete_1.png" />
                                    <img src="/image/picture_1.png" />
                                </div>
                                <img src="/image/dianjishangchuan_1.png" />
                                <img src="/image/dianjishangchuan_1.png" />
                            </div>
                        </div>
                        <div class="RightClearance1 employees">
                            <p class="p_employees">员工人数</p>
                            <div id="employeesid" class="WorkingTimediv">
                                <cite>10人</cite>
                                <ul>
                                    <li><a href="javascript:;" value="1">10人</a></li>
                                    <li><a href="javascript:;" value="2">20人</a></li>
                                    <li><a href="javascript:;" value="3">50人</a></li>
                                </ul>
                                <input type="hidden" value="1" id="employees" />
                            </div>
                            <span>员工信息编辑</span>
                        </div>
                        <div class="RightClearance1 pinput2">
                            <p class="p_DirectorName">负责人姓名</p>
                            <p><input type="text" name="DirectorName" /></p>
                        </div>
                        <div class="RightClearance1 employees">
                            <p class="p_DocumentType">证件类型</p>
                            <div id="DocumentTypeid" class="WorkingTimediv">
                                <cite>身份证</cite>
                                <ul>
                                    <li><a href="javascript:;" value="1">身份证</a></li>
                                    <li><a href="javascript:;" value="2">学生证</a></li>
                                    <li><a href="javascript:;" value="3">其它</a></li>
                                </ul>
                                <input type="hidden" value="1" id="DocumentType" />
                            </div>
                        </div>
                        <div class="RightClearance1 pinput3">
                            <p class="p_DocumentNumber">证件号码</p>
                            <p><input type="text" name="DocumentNumber" /></p>
                        </div>
                        <div class="RightClearance1 HeadProfilePicture">
                            <p class="p_HeadProfilePicture">负责人证件照上传</p>
                            <p><img src="/image/dianjishangchuan_1.png" /></p>
                        </div>
                    </div>
                </div>
                <div class="bottomArea">
                    <input name="imageField" type="image" id="imageField1" src="/image/baocun_1.png" />
                    <input name="imageField" type="image" id="imageField2" src="/image/baocun_2.png" />
                </div>
            </div>
            <div id="account" class="account">
                账号安全
            </div>
        </div>
    </form>
</div>
    <div>
     <p>
         <asp:Label ID="username" runat="server" Text=""></asp:Label></p>
     <p>商家名称：<asp:TextBox ID="businessName" runat="server"></asp:TextBox></p>
     <p>经度：<asp:TextBox ID="Longitude" runat="server"></asp:TextBox></p>
     <p>纬度：<asp:TextBox ID="Latitude" runat="server"></asp:TextBox></p>
     <p>公司介绍：<asp:TextBox ID="Description" runat="server"></asp:TextBox></p>
     <p>公司地址：<asp:TextBox ID="Address" runat="server"></asp:TextBox></p>  
     <p>
         <asp:Button ID="Button1" runat="server" Text="确认提交" OnClick="dataSub" /></p>    
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="bottom" runat="server">
<script type="text/javascript" src="/js/global.js"></script>
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