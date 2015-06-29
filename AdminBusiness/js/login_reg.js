$(document).ready(function () {
    //验证图标显示控制

    $("#tbxUserName").InlineTip({ 'tip': "手机号码/电子邮箱" });

    var chkIconAnm = function (hide, jdg, icon) {
        if (!hide) {
            if (!jdg) {  //条件不符合
                icon.removeClass('chkRight').addClass('chkError');
            } else {  //条件符合
                icon.removeClass('chkError').addClass('chkRight');
            }
        } else {
            icon.fadeOut(300);
        }
    };

    //验证用户是否存在
//    var showCheck = function (objInput, icon) {
//        objInput.blur(function () {
//            var val = objInput.val();
//            var transData = { username: val };
//
//            if (objInput.val() != '') {
//                $.ajax({
//                    type: 'GET',
//                    async: false,
//                    url: "/AjaxService/is_username_duplicate.ashx",
//                    data: transData,
//
//                    //            jsonp:"callback",
//                    success: function (json) {//后台返回json,json=1用户不存在,json=0用户存在
//                        if (json == "N") json = 1;
//                        else if (json == "Y") json = 0;
//                        chkIconAnm(false, json, icon);
//                    },
//                    error: function (e) {
//                        alert('error!'+e);
//                    }
//                });
//            } else {
//                chkIconAnm(true, false, icon)
//                return false;
//            }
//        });
//    };
//    showCheck($('#tbxUserName'), $('#phoneCheck'));

    //密码验证
    var passCheck = function (psw, pswConf, pswChk, pswConfChk) {
        var passRule = /^[A-Za-z0-9_-]+$/;
        var ruleR;
//        psw.change(function () { //密码是否符合规则
                    psw.bind('input',function(){ //密码是否符合规则
            if ((psw.val().length >= 6) && (psw.val().length <= 20) && passRule.test(psw.val())) {
                chkIconAnm(false, true, pswChk);
                $('#passCheckText').hide();
                ruleR = true;
            } else {
                chkIconAnm(false, false, pswChk);
                $('#passCheckText').show();
                ruleR = false;
            }
        });

//        pswConf.change(function () { //密码确认是否一致
            pswConf.bind('input',function(){ //密码确认是否一致
                if (!ruleR) {
                chkIconAnm(true, false, pswConfChk);
            } else {
                if (pswConf.val() == psw.val()) {
                    chkIconAnm(false, true, pswConfChk);
                    $('#passConfText').hide();
                    $('#regPsSubmit').attr("onclick","return true;");
                    return true;
                } else {
                    chkIconAnm(false, false, pswConfChk);
                    $('#passConfText').show();
                }
            }
        });


    }
    passCheck($('#regPs'), $('#regPsConf'), $('#psChk'), $('#psConfChk'));


    //注册名选择方法

//    var selectMeth = function () {
//        //        alert($('#regMeth').val());
//        var resetMeth = function (objInput, icon) {
//            objInput.val(null);
//            chkIconAnm(true, false, icon);
//        };
//
//        if ($('#regMeth').val() == '1') {
//            resetMeth($('#email'), $('#emailCheck'));
//            $('.methText').text('输入号码');
//            $('.emailBox').hide();
//            $('.phoneBox').show();
//        } else if ($('#regMeth').val() == '2') {
//            //            alert($('#regMeth').val());
//            resetMeth($('#phone'), $('#phoneCheck'));
//            $('.methText').text('输入邮箱');
//            $('.phoneBox').hide();
//            $('.emailBox').show();
//        } else {
//            return null;
//        }
//    }

    //用户名验证

    var regFilter = /^1\d{10}$|^.+@.+\..+$/;
    var regChang = function () {
//        var $regForm1 = $('#regFormUser');
//        var $regForm2 = $('#regFormPass');

        // $regForm1.submit
        $("#userConfirm").bind("click",
            function () {
//                console.log($("#tbxUserName").val().match(regFilter));
                var agreeLic =  $('input[name="agreeLic"]:checked');
//                console.log(agreeLic.val());
                if ( $("#tbxUserName").val() == null || $("#tbxUserName").val() == "手机号码/电子邮箱" ){
                    $('#userCheck').addClass('chkError');
                    $("#userCheckText").text("请填写用户名");
                } else if ( !$("#tbxUserName").val().match(regFilter) ) {
                    $('#userCheck').addClass('chkError');
                    $("#userCheckText").text("格式不正确");
                } else if ( !agreeLic.val() ) {
                    $(".userCheckText").text("没有同意协议");
                } else {
                    $('#usernameConf').text($("#tbxUserName").val());
                    $('#userCheck').hide();
                    $("#userCheckText").text("");
                    $('.main-reg').toggle();
                    $('.main-psw').toggle();
                }
            }
        )
        $("#userConfirmBack").bind("click",
            function(){
                $('.main-reg').toggle();
                $('.main-psw').toggle();
            }
        )
    }
    regChang();


    var regOrLogin = function () {
        $('.doReg').click(
            function () {
                return;
                $('.wrap-login').animate(
                    {
                        top: -50 + '%'
                    },
                    1000, false);
                $('.wrap-reg').animate(
                    {
                        top: 50 + '%'
                    },
                    1000, false)
            });

        $('.doLogin').click(
            function () {
                return;
                $('.wrap-login').animate(
                    {
                        top: 50 + '%'
                    },
                    1000, false);
                $('.wrap-reg').animate(
                    {
                        top: 150 + '%'
                    },
                    1000, false)
            });
    }
    regOrLogin();

});