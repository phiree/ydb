/**
 * validation_emp_edit.js v1.0.0 @ 2016-05-17 by licdream@126.com
 * validate rule in emp edit.
 */
$(function(){
    $.validator.setDefaults({
        ignore: []
    });

    $.validator.addMethod("phone", function (value, element) {
        return /(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)|(13\d{9}$)|(15[0-9]\d{8}$)|(18[0-9]\d{8}$)|(17[0-9]\d{8}$)/.test(value);
    }, "请输入有效的电话号码");

    $.validator.addMethod("code", function (value, element) {
        return value == "" ? true : /(^[\x00-\xFF]+$)/.test(value);
    }, "请勿输入特殊字符");

    var name_prefix = 'ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$';
    var emp_validae_rules = {};
    var emp_validae_messages = {};

    // 编号
    emp_validae_rules[name_prefix + "Code"] =
    {
        required: true,
        code: true,
        maxlength: 100
    };

    emp_validae_messages[name_prefix + "Code"] =
    {
        required: "请填写编号",
        maxlength: "请勿超过100个字符"
    };

    //姓名
    emp_validae_rules[name_prefix + "Name"] =
    {
        required: true,
        maxlength: 50
    };
    emp_validae_messages[name_prefix + "Name"] =
    {
        required: "请填写姓名",
        maxlength: "不能超过50个字符"
    };

    //昵称
    emp_validae_rules[name_prefix + "NickName"] =
    {
        required: true,
        maxlength: 50
    };
    emp_validae_messages[name_prefix + "NickName"] =
    {
        required: "请填写昵称",
        maxlength: "不能超过50个字符"
    };


    emp_validae_rules[name_prefix + "Phone"] =
    {
        phone: true
    };


    $($("form")[0]).validate(
        {
            errorElement: "p",
            errorPlacement: function (error, element) {
                if ( $(element).attr("id") == "ContentPlaceHolder1_Gender1" ) {
                    error.appendTo((element.parent()).parent());
                } else {
                    error.appendTo(element.parent());
                }
            },
            rules: emp_validae_rules,
            messages: emp_validae_messages,
        }
    );
});