$(document).ready(function () {
    //var default_user_text = "手机号码/电子邮箱";

    //$("#tbxUserName").InlineTip({ 'tip': default_user_text });

    var regCheck = function(){

        var $user_name_box = $(".regUserName");
        //var user_name_rule = /^1\d{10}$|^.+@.+\..+$/;
        //var user_name_rule = /^(\w+((-w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+)$|^(0?(13|14|17|15|18)[0-9]{9})$/;
        var user_name_rule = /^(\w+((-w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+)$/;
        var user_warn_element = document.createElement("div");
        var user_warn_text = document.createTextNode("");


        $(user_warn_element).addClass("err_msg");
        $user_name_box.parent().append(user_warn_element);
        $(user_warn_element).hide();


        /**
         * 用户验证函数
         */
        var userNameCheck = function(){
            var _user_name = $user_name_box.val();
            var $_checkIcon = $user_name_box.parent().find(".checkIcon");


            //if ( _user_name == null || _user_name == default_user_text ){
            if ( _user_name == null || _user_name == "" ){
                chkIconAnm(false, false, $_checkIcon);
                $(user_warn_element).show();
                user_warn_text = "请填写用户名";
                $(user_warn_element).text(user_warn_text);
            } else if ( !user_name_rule.test(_user_name) ) {
                $(user_warn_element).show();
                chkIconAnm(false, false, $_checkIcon);
                user_warn_text = "格式错误";
                $(user_warn_element).text(user_warn_text);
            } else if ( userNameDupChk(_user_name) == "error" ) {
                $(user_warn_element).show();
                chkIconAnm(false, false, $_checkIcon);
                user_warn_text = "请求出错";
                $(user_warn_element).text(user_warn_text);
            } else if( userNameDupChk(_user_name) ){
                $(user_warn_element).show();
                chkIconAnm(false, false, $_checkIcon);
                user_warn_text = "用户名已存在";
                $(user_warn_element).text(user_warn_text);
            } else {
                chkIconAnm(false, true, $_checkIcon);
                $(user_warn_element).hide();
                return true;
            }
        };

        /**
         * AJAX验证用户是否存在函数
         * @param {string}  用户名
         */
        var userNameDupChk = function(username){

            var dup_ajax = $.ajax({
                type: "get",
                url: "/ajaxservice/is_username_duplicate.ashx?username=" + username,
                async: false
            });

            if ( dup_ajax.readyState == 4 ) {
                if ( dup_ajax.status == 200 || dup_ajax.status == 0 ){
                    return dup_ajax.responseText == "Y";
                } else {
                    return "error";
                }
            }

        };

        $user_name_box.blur(userNameCheck);

        var pass_rule = /^[A-Za-z0-9_-]+$/;
        var $pass_box = $('#regPs');
        var $pass_conf_box = $('#regPsConf');
        var pass_warn_element = document.createElement("div");
        var pass_warn_text = document.createTextNode("");
        var pass_conf_warn_element = document.createElement("div");
        var pass_conf_warn_text = document.createTextNode("");
        var ruleR;

        $(pass_warn_element).addClass("err_msg");
        $(pass_conf_warn_element).addClass("err_msg");
        $pass_box.parent().append(pass_warn_element);
        $pass_conf_box.parent().append(pass_conf_warn_element);
        $(pass_warn_element).hide();
        $(pass_conf_warn_element).hide();


        /**
         * 密码规则验证函数
         */
        var passCheck = function (e) {
            var eve = e || window.event;
            var _pass_value = $pass_box.val();
            var $_checkIcon = $pass_box.parent().find(".checkIcon");
            if ( !_pass_value ){
                    chkIconAnm(true, false , $_checkIcon);
                    $(pass_warn_element).hide();
                    ruleR = false;
            } else {
                if (( _pass_value.length >= 6 ) && ( _pass_value.length <= 20 ) && pass_rule.test( _pass_value ) ) {
                    chkIconAnm(false, true, $_checkIcon);
                    $(pass_warn_element).hide();
                    ruleR = true;
                    return true;
                } else {
                    chkIconAnm(false, false, $_checkIcon);
                    $(pass_warn_element).show();
                    pass_warn_text = "密码不符合要求，要求6-20个字符";
                    $(pass_warn_element).text(pass_warn_text);
                    ruleR = false;
                }
            }
        };

        /**
         * 确认密码验证函数
         */
        var passConfCheck = function(){
            var _pass_conf_value = $pass_conf_box.val();
            var $_checkIcon = $pass_conf_box.parent().find(".checkIcon");


            if ( !ruleR ) {
                chkIconAnm(true, false, $_checkIcon);
            } else {
                if ( !_pass_conf_value ) {
                    chkIconAnm(true, false, $_checkIcon);
                    $(pass_conf_warn_element).hide();
                } else {
                    if ( _pass_conf_value == $pass_box.val() ) {
                        chkIconAnm(false, true, $_checkIcon);
                        $(pass_conf_warn_element).hide();
                        return true;
                    } else {
                        chkIconAnm(false, false, $_checkIcon);
                        $(pass_conf_warn_element).show();
                        pass_conf_warn_text = "两次密码不一致";
                        $(pass_conf_warn_element).text(pass_conf_warn_text);
                    }
                }
            }
        };

        $pass_box.bind('input propertychange', passCheck);
        $pass_conf_box.bind('input propertychange', passConfCheck);

        var $agree_LIC =  $('input[name="agreeLic"]:checkbox');
        if ( $agree_LIC.get(0) ) {
            $agree_LIC.get(0).checked = true;
        }

        var agree_warn_element = document.createElement("div");
        var agree_warn_text = document.createTextNode("");

        $(agree_warn_element).addClass("err_msg");
        $agree_LIC.parent().append(agree_warn_element);
        $(agree_warn_element).hide();


        var agreeCheck = function() {
            if ( !$agree_LIC.get(0).checked ) {
                agree_warn_text = "请仔细阅读协议，同意协议条例方可注册。";
                $(agree_warn_element).text(agree_warn_text);
                $(agree_warn_element).show();

            } else {
                $(agree_warn_element).hide();
                return true;
            }
        };

        $agree_LIC.bind("click",agreeCheck);



        var $reg_submit = $('#regPsSubmit');

        $reg_submit.click(function(eve){
            var e = eve || window.event;
            if ( userNameCheck() && passCheck() && passConfCheck() && agreeCheck() ) {
                return true;
            } else {
                e.preventDefault();
            }
        });

        /**
         * 注册框旁边图标动画函数
         * @param {boolean}  是否隐藏icon
         * @param {boolean}  是否符合条件
         * @param {jquery object}  图标jquery对象
         */
        var chkIconAnm = function (hide, jdg, icon) {
            var _hide = hide;
            var _judge = jdg;
            var $_icon = icon;


            if (!_hide) {
                //条件不符合
                if (!_judge) {
                    $_icon.removeClass('chkRight').addClass('chkError');
                } else {
                    $_icon.removeClass('chkError').addClass('chkRight');
                }
            } else {
                $_icon.removeClass('chkError').removeClass('chkRight');
            }
        };

    };

    regCheck();

    var today = function(){
        var $date = $(".date");
        var now = document.createElement("span");
        var nowDate = new Date();

        $(now).text( nowDate.getFullYear() + " 年 " + ( nowDate.getMonth() + 1 )  + " 月 " + nowDate.getDate() + " 号 " );
        $date.append(now);
    };
    today();

});