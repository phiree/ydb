/**
 * validation_shop_edit.js v1.0.0 @ 2016-05-17 by licdream@126.com
 * validate rule in shop edit.
 */
$(function () {
    $.validator.setDefaults({
        ignore: []
    });

    $.validator.addMethod("phone", function (value, element) {
        return /(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)|(13\d{9}$)|(15[0-9]\d{8}$)|(18[0-9]\d{8}$)|(17[0-9]\d{8}$)/.test(value);
    }, "请输入有效的电话号码");

    $.validator.addMethod("idcard", function (value, element) {
        return /^(\d{15}$)|(^\d{17}([0-9]|X|x))$/.test(value);
    }, "请输入有效的身份证号码");

    $.validator.addMethod("integer", function (value, element) {
        return value == "" ? true : /^[1-9]\d*$/.test(value);
    }, "请输入整数");

    $.validator.addMethod("web", function (value, element) {
        return value == "" ? true : /^([^\.]+\.){1,3}[^\.]+$/.test(value);
    }, "请输入有效的网址");

    var name_prefix = "ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$";
    var business_validate_rules = {};
    var business_validate_messages = {};

    //店铺名称
    business_validate_rules[name_prefix + "tbxName"] =
    {
        required: true,
        maxlength: 20
    };
    business_validate_messages[name_prefix + "tbxName"] =
    {
        required: "请填写店铺名称",
        maxlength: "不能超过20个字符"
    };

    //店铺介绍
    business_validate_rules[name_prefix + "tbxIntroduced"] =
    {
        required: true,
        maxlength: 200
    };
    business_validate_messages[name_prefix + "tbxIntroduced"] =
    {
        required: "请输入店铺介绍",
        maxlength: "不能超过200个字符"
    };

    //店铺地址
    business_validate_rules[name_prefix + "tbxAddress"] =
    {
        required: true
    };
    business_validate_messages[name_prefix + "tbxAddress"] =
    {
        required: "请输入店铺地址"
    };


    //店铺联系电话
    business_validate_rules[name_prefix + "tbxContactPhone"] =
    {
        phone: true
    };


    //店铺网址
    business_validate_rules[name_prefix + "tbxWebSite"] =
    {
        required: false,
        web: true
    };

    business_validate_messages[name_prefix + "tbxWebSite"] =
    {
        web: "请输入正确的网址"

    };

    //从业时间
    business_validate_rules[name_prefix + "tbxBusinessYears"] =
    {
        required: false,
        range: [1, 1000]

    };
    business_validate_messages[name_prefix + "tbxBusinessYears"] =
    {
        range: "请选择从业时间"
    };

    //店铺负责人
    business_validate_rules[name_prefix + "tbxContact"] =
    {
        maxlength: 20

    };
    business_validate_messages[name_prefix + "tbxContact"] =
    {
        maxlength: "请输入20字以内的姓名(汉字)"
    };

    // 证件类型
    business_validate_rules[name_prefix + "tbxCardIdNo"] =
    {

        required: true,
        idcard: function (element) {
            if ( $("#selCardType").val() == "0" ) {
                return true;
            }
        },
        maxlength: function (element) {
            if ( $("#selCardType").val() == "1" ) {
                return 50;
            }
        }

    };
    business_validate_messages[name_prefix + "tbxCardIdNo"] =
    {
        required: "请填写证件号码",
        idcard: "证件号码有误",
        maxlength: "证件号码超出50个字符,请核实修改或者联系客服"
    };

    // 员工人数
    business_validate_rules[name_prefix + "selStaffAmount"] =
    {
        required: true,
        integer: true,
        range: [1, 1000]
    };
    business_validate_messages[name_prefix + "selStaffAmount"] =
    {
        integer: "请填写正确的员工人数",
        required: "请填写员工人数",
        range: "请输入1-1000之间的数值"
    };

    $($("form")[0]).validate(
        {
            errorElement: "p",
            errorPlacement: function (error, element) {
                if ( $(element).attr("name") == name_prefix + "tbxBusinessYears" ) {
                    error.appendTo((element.parent()).parent());
                } else {
                    error.appendTo(element.parent());
                }
            },
            rules: business_validate_rules,
            messages: business_validate_messages
        }
    );
});
