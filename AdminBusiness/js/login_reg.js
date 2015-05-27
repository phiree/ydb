$(document).ready(function () {
    //验证图标显示控制
    var chkIconAnm = function (hide, jdg, icon) {
        if (!hide) {
            if (!jdg) {  //条件不符合
                icon.removeClass('chkRight').addClass('chkError').fadeIn(300);
            } else {  //条件符合
                icon.removeClass('chkError').addClass('chkRight').fadeIn(300);
            }
        } else {
            icon.fadeOut(300);
        }
    };

    //验证用户是否存在
    var showCheck = function (objInput, icon) {
        objInput.blur(function () {
            var val = objInput.val();
            var transData = { num: [val] };

            if (objInput.val() != '') {
                $.ajax({
                    type: 'GET',
                    async: false,
                    url: "http://localhost/test.php?",
                    data: transData,
                    dataType: 'jsonp',
                    //            jsonp:"callback",
                    success: function (json) {//后台返回json,json=1用户不存在,json=0用户存在
                        chkIconAnm(false, json, icon);
                    },
                    error: function () {
                        alert('error!');
                    }
                });
            } else {
                chkIconAnm(true, false, icon)
                return false;
            }
        });
    };
    showCheck($('#phone'), $('#phoneCheck'));
    showCheck($('#email'), $('#emailCheck'));

    //密码验证
    var passCheck = function (psw, pswConf, pswChk, pswConfChk) {
        var passRule = /^[A-Za-z0-9_-]+$/;
        var ruleR;

        psw.change(function () { //密码是否符合规则
            //        psw.bind('input',function(){ //密码是否符合规则
            if ((psw.val().length >= 6) && (psw.val().length <= 20) && passRule.test(psw.val())) {
                chkIconAnm(false, true, pswChk);
                ruleR = true;
            } else {
                chkIconAnm(false, false, pswChk);
                ruleR = false;
            }
        });

        pswConf.change(function () { //密码确认是否一致
            if (!ruleR) {
                chkIconAnm(true, false, pswConfChk);
            } else {
                if (pswConf.val() == psw.val()) {
                    chkIconAnm(false, true, pswConfChk);
                } else {
                    chkIconAnm(false, false, pswConfChk);
                }
            }
        });
    }
    passCheck($('#regPs'), $('#regPsConf'), $('#psChk'), $('#psConfChk'));

    //注册名选择方法
    var selectMeth = function () {
        //        alert($('#regMeth').val());
        var resetMeth = function (objInput, icon) {
            objInput.val(null);
            chkIconAnm(true, false, icon);
        };

        if ($('#regMeth').val() == '1') {
            resetMeth($('#email'), $('#emailCheck'));
            $('.methText').text('输入号码');
            $('.emailBox').hide();
            $('.phoneBox').show();
        } else if ($('#regMeth').val() == '2') {
            //            alert($('#regMeth').val());
            resetMeth($('#phone'), $('#phoneCheck'));
            $('.methText').text('输入邮箱');
            $('.phoneBox').hide();
            $('.emailBox').show();
        } else {
            return null;
        }
    }

    //自定义下拉表单
    function mySelect(selectId, inputSelectId, onChange) {
        var list = $(selectId).find('ul');
        var mouseIsOut = false;

        $(selectId).find('cite').click(function () {
            if (list.css("display") == "none") {
                list.slideDown("fast")
            } else {
                list.slideUp("fast")
            }
        });
        $(selectId).mouseover(function () {
            mouseIsOut = false
        });
        $(selectId).mouseout(function () {
            mouseIsOut = true
        });
        $(document).click(function () {
            if (mouseIsOut) {
                list.hide()
            }
        });
        $(list).find('a').click(function () {
            var txt = $(this).text();
            var value = $(this).attr('value');

            $(selectId).find('cite').html(txt);
            inputSelectId.val(value);
            onChange();
            list.hide();
        })
    }
    mySelect($('#regMethList'), $('#regMeth'), selectMeth);

    var regChang = function () {
        var $regForm1 = $('#regFormUser');
        var $regForm2 = $('#regFormPass');

        // $regForm1.submit
        $("#phoneSubmit").click(
            function () {
                $('.main-reg').hide();
                $('.main-psw').show();
            }
        )
            $("#regPsSubmit").click(function () {
                $('.main-reg').show();
                $('.main-psw').hide();
        });

        $regForm2.submit(
            function () {
                $('.main-psw').hide();
                $('.main-done').show();
                $('.regLogo').find('a').attr('href', 'http://www.baidu.com');
                $('.regLogo').find('img').attr('src', 'images/icon_1.png');
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