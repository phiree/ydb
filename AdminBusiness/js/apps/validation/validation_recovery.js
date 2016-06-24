/**
 * validation_recovery.js v1.0.0 @ 2016-05-17 by licdream@126.com
 * validate rule in recovery.
 */
$(function (){
    $.validator.setDefaults({
        ignore: [],
    });

    $.validator.addMethod("pwd", function (value, element) {
        return value == "" ? true : /^[A-Za-z0-9_-]+$/.test(value);
    }, "密码格式错误");

    $.validator.addMethod("pwdConfirm", function (value, element) {
        return $("#tbxPasswordConfirm").val() == $("#tbxPassword").val()
    }, "两次输入的密码不一致");

    var pass_validate_rules ={};
    var pass_validate_messages={};

    //tbxPassword
    pass_validate_rules["tbxPassword"]=
    {
        required: true,
        minlength: 6,
        maxlength: 16,
        pwd: true

    };
    pass_validate_messages["tbxPassword"]=
    {
        required: "请填写密码",
        minlength: "不能少于6个字符",
        maxlength: "不能超过16个字符"
    };

    //tbxPasswordConfirm
    pass_validate_rules["tbxPasswordConfirm"]=
    {
        required: true,
        minlength: 6,
        maxlength: 16,
        pass:true,
        passConfirm: true
    };
    pass_validate_messages["tbxPasswordConfirm"]=
    {
        required: "请再次输入密码",
    };

    $($("form")[0]).validate(
        {
            errorElement: "div",
            errorLabelContainer: ".login_err_msg ul",
            wrapper: "li",
            rules: pass_validate_rules,
            messages: pass_validate_messages
        }

    );
});