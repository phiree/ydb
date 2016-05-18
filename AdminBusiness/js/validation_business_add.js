$(function(){
    $.validator.addMethod("web", function (value, element) {
        return value == "" ? true : /^([^\.]+\.){1,3}[^\.]+$/.test(value);
    }, "请输入有效的网址");

    $.validator.addMethod("phone", function (value, element) {
        return /(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)|(13\d{9}$)|(15[0-9]\d{8}$)|(18[0-9]\d{8}$)|(17[0-9]\d{8}$)/.test(value);
    }, "请输入有效的电话号码");

    var business_validate_rules = [];
    var business_validate_messages = [];

    //店铺名称
    business_validate_rules["tbxName"] =
    {
        required: true,
        maxlength: 20
    };
    business_validate_messages["tbxName"] =
    {
        required: "请填写店铺名称",
        maxlength: "不能超过12个字符"
    };

    //店铺介绍
    business_validate_rules["tbxDescription"] =
    {
        required: true,
        rangelength: [1, 200]
    };
    business_validate_messages["tbxDescription"] =
    {
        required: "请填写店铺介绍",
        rangelength: "不能超过200个字符"
    };

    //店铺地址
    business_validate_rules["tbxAddress"] =
    {
        required: true
    };
    business_validate_messages["tbxAddress"] =
    {
        required: "请输入店铺地址"
    };

    //联系方式
    business_validate_rules["tbxContactPhone"] =
    {
        //phone: true
        required: true
    };

    business_validate_messages["tbxContactPhone"] =
    {
        //phone: true
        required: "请输入联系方式"
    };

    //店铺网址
    business_validate_rules["tbxWebSite"] =
    {
        //required: true,
        web: true
    };

    business_validate_messages["tbxWebSite"] =
    {
        web: "请输入正确的网址或邮箱地址",
        //required: "请输入正确的网址或邮箱地址"
    };

    $(function () {
        $($("form")[0]).validate(
            {
                errorElement: "p",
                errorPlacement: function (error, element) {
                    error.appendTo(element.parent());
                },
                rules: business_validate_rules,
                messages: business_validate_messages
            }
        );
    });
});
