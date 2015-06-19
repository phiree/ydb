﻿<%@ Page Title="" Language="C#" MasterPageFile="~/adminBusiness.master" AutoEventWireup="true" CodeFile="Security.aspx.cs" Inherits="Account_Security" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="../css/myshop.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageDesc" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="mainContent clearfix">
   
        <div class="leftContent" id="leftCont">
            <div>
                <ul>
                    <li><a href="./Default.aspx"><i class="nav-btn side-btn-myshop"></i></a></li>
                    <li><a href="./Security.aspx"><i class="nav-btn side-btn-secret"></i></a></li>
                </ul>
            </div>
        </div>
        <div class="rightContent" id="rightCont">
            <div class="myshopInfoArea clearfix">
                <div class="myshopInfoTilte">
                    <h1>商家基本信息</h1>
                    <!--<img src="../image/touxiangkuang11.png" alt="头像"/>-->
                </div>
                <div class="headInfoArea clearfix">
                    <!--<div class="headDecoration1">-->
                    <!--</div>-->
                    <div class="headImage">
                        <img src="../image/myshop/touxiangkuang_11.png" alt="头像"/>
                    </div>
                    <div class="headInfo">
                        <p><input runat="server" type="text" id="tbxName" name="inputShopName" value="请输入您的店铺名称" class="inputShopName" /></p>
                        <p class="InfoCompletetxt">资料完成程度</p>
                        <div class="InfoPercentage">
                            <div class="InfoComplete">
                                <span class="progress"></span>
                            </div>
                            <span class="completePercentage">90%</span>
                        </div>
                    </div>
                    <div class="headEditImg">
                        <a href="javascript:void(0);" class="headEditBtn"></a>
                    </div>
                </div>
            </div>
            <div class="secret-title">
                <span >账号信息相关</span>
            </div>
            <div class="secret-cont">
                <div class="secret-m standard-info">
                    <div class="secret-m-title t-1">基础信息</div>
                    <div class="secret-detail">
                        <div><span class="secret-d-t">登录账号&nbsp;:</span><span><%=CurrentUser.UserName%></span></div>
                        <div><span class="secret-d-t">手机号码&nbsp;:</span><p class="secret-d-a d-inb">XXXXXX</p><a href="">更换号码</a></div>
                        <div><span class="secret-d-t">绑定邮箱&nbsp;:</span><p class="secret-d-a d-inb">XXXXXX</p><a href="">更换邮箱</a></div>
                        <div><span class="secret-d-t">上次登陆&nbsp;:</span><span>XXXX年XX月XX日</span><span class="m-l20">14:00</span></div>
                    </div>
                </div>
                <div class="secret-m secert-info">
                    <div class="secret-m-title t-2">账户安全信息</div>
                    <div class="secret-detail">
                        <div class="clearfix">
                            <span class="secret-d-t">登陆密码&nbsp;:</span><span class="secret-d-p d-inb" >安全性高的密码可以使账号更安全，建议您定期更换密码，且设置一个包含数字和字母，并长度超过6位数的密码</span>
                            <!--<a class="m-l20" href="Changepassword.aspx">修改</a>-->
                            <a id="passChange" class="m-l20" href="javascript:void(0);">修改</a>
                            <div class="secret-d-tip"><i class="icon secret-icon-1"></i><div>强</div></div>
                        </div>
                        <div class="clearfix">
                            <span class="secret-d-t">身份认证&nbsp;:</span><span class="secret-d-p d-inb">重新上传负责人证件照</span>
                            <a class="m-l20" href="Default.aspx">修改</a>
                            <div class="secret-d-tip"><i class="icon secret-icon-2"></i><div>已认证</div></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="lightBox" class="dis-n">
        <!--<div id="lightBox">-->
            <div class="secret-change" ng-app="changePass" ng-controller="changePass">
                <div class="secret-change-title">
                    <span>修改密码</span>
                    <i class="icon close icon-close" ng-click="CPassReset()"></i>
                </div>
                <div class="secret-change-nav clearfix">
                    <div class="change-nav fl"><i class="icon"></i>设置新密码</div>
                    <div class="change-nav m-l50 fr"><i class="icon" ></i>修改密码成功</div>
                </div>
                <div class="secret-change-m">
                    <div class="m-auto" ng-hide="!CPassSuccess">
                        <!--<div class="m-b20"><span class="m-r20">验证码</span><input class="secret-input-setVCode" type="text" /><input class="secret-btn-VCode m-l20" type="button" value="发送验证码"></div>-->
                        <!--<div><span class="m-r20">新密码</span><input class="secret-input-newPass" type="password"  /><span><i class="icon m-l20"></i>强</span></div>-->
                        <!--<div class="secret-change-sub"><input class="secret-btn-next" ng-click="CPassNext()" type="button" value="下一步" /></div>-->
                        <asp:ChangePassword ID="ChangePassword1"
                        runat="server"
                        ChangePasswordTitleText="修改密码"
                        CssClass="CPBox"
                        PasswordLabelText="旧密码"
                        ConfirmNewPasswordLabelText="确认新密码"
                        NewPasswordLabelText="新密码"
                        ChangePasswordButtonText="确认"
                        ChangePasswordButtonType="Image"
                        ChangePasswordButtonImageUrl="../image/myshop/shop_tx_107.png"
                        ChangePasswordButtonStyle-CssClass="p-20"
                        CancelButtonText="取消"
                        CancelButtonType="Image"
                        CancelButtonImageUrl="../image/myshop/shop_tx_108.png"
                        CancelButtonStyle-CssClass="p-20"
                        TitleTextStyle-CssClass="CPTitle"
                        LabelStyle-CssClass="CPLabel"
                        TextBoxStyle-CssClass="CPTextBox"
                        ChangePasswordFailureText=""
                        SuccessText="密码修改成功"
                        
                        >
                        <SuccessTemplate>
                            <div id="CPResult" class="m-auto" ng-hide="CPassSuccess">
                                <div class="t-c">
                                    <i class="icon secret-icon-done"></i>
                                    <div class="secret-change-done d-inb">
                                        <p>恭喜你</p>
                                        <p>修改密码成功</p>
                                    </div>
                                </div>
                                <div class="secret-change-sub"><input class="close secret-btn-done" ng-click="CPassReset()" type="button" value="确认" /></div>
                            </div>
                        </SuccessTemplate>
                        </asp:ChangePassword>
                    </div>

                </div>
            </div>

        </div>
  
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bottom" Runat="Server">
<script type="text/javascript" src="<% =ConfigurationManager.AppSettings["cdnroot"]%>/static/Scripts/jquery-1.10.2.js"></script>
<script type="text/javascript" src="/js/jquery.lightbox_me.js" ></script>
<script type="text/javascript" src="http://apps.bdimg.com/libs/angular.js/1.2.5/angular.min.js"></script>
<script type="text/javascript" src="/js/global.js"></script>
<script type="text/javascript">
    var changecontinueBtn = $('#ContentPlaceHolder1_ChangePassword1_ChangePasswordContainerID_ChangePasswordImageButton')


//
//    changePass.controller("changePass",function($scope){
//        $scope.CPassSuccess = true;
//        $scope.CPassNext = function(){
//            $scope.CPassSuccess = !$scope.CPassSuccess;
//            passSwitch();
//        }
//        changecontinueBtn.click(function(){
//           $scope.CPassSuccess = !$scope.CPassSuccess;
////           $('.CPBox').css({
////           "display" : "none"
////           });
////           $('#CPResult').css({
////           "display" : "block"
////           })
//           passSwitch();
//        });
//
//        $scope.CPassReset = function(){
//            $scope.CPassSuccess = true;
//            console.log($scope.CPassSuccess);
//            passSwitchReset();
//        }
//
//    });
//
//    $('.secret-change-nav').append('<div class="passSwitchBG"></div>');
//    $('.passSwitchBG').css({
//        'width' : 155 + 'px',
//        'position' : 'absolute',
//        'left' : '150px',
//        'right' : 0,
//        'bottom' : '-3px',
//        'border-bottom' : '3px solid #04b1eb'
//    });
//
//    var passSwitch = function () {
//        $('.passSwitchBG').animate({
//            left : '+355px'
//        },300);
//    }
//
//    var passSwitchReset = function () {
//        $('.passSwitchBG').css({
//            'width' : 155 + 'px',
//            'position' : 'absolute',
//            'left' : '150px',
//            'right' : 0,
//            'bottom' : '-3px',
//            'border-bottom' : '3px solid #04b1eb'
//        });
//    }

    $('#passChange').click(function(e){
        $('#lightBox').lightbox_me({
            centered: true
        });
        $("#lightBox").appendTo($("form:first"));
       e.preventDefault();
    })
</script>
</asp:Content>

