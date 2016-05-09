$(function () {
    $.validator.setDefaults({
        ignore: []
    });

    $.validator.addMethod("integer", function (value, element) {
        return value == "" ? true : /^[1-9]\d*$/.test(value);
    }, "请填写整数");

    var name_prefix = 'ctl00$ctl00$ContentPlaceHolder1$ContentPlaceHolder1$ctl00$';
    var service_validate_rules = {};
    var service_validate_messages = {};

    // 服务名称
    service_validate_rules[name_prefix + "tbxName"] =
    {
        required: true,
        maxlength: 100
    };
    service_validate_messages[name_prefix + "tbxName"] =
    {
        required: "请填写服务名称",
        maxlength: "不能超过100个字符"
    };

    //服务类型
    service_validate_rules[name_prefix + "hiTypeId"] =
    {
        required: true
    };
    service_validate_messages[name_prefix + "hiTypeId"] =
    {
        required: "请选择服务类型"
    };

    //服务起步价
    service_validate_rules[name_prefix + "tbxMinPrice"] =
    {
        integer: true,
        required: true,
        range: [1, 20000]
    };
    service_validate_messages[name_prefix + "tbxMinPrice"] =
    {
        integer: "价格应为整数",
        required: "请填写最低服务价格",
        range: "价格限制为1-20000元"
    };

    //服务介绍
    service_validate_rules[name_prefix + "tbxDescription"] =
    {
        required: true,
        rangelength: [1, 500]
    };
    service_validate_messages[name_prefix + "tbxDescription"] =
    {
        required: "请填写服务介绍",
        rangelength: "不能超过500个字符"
    };

    //服务范围
    service_validate_rules[name_prefix + "hiBusinessAreaCode"] =
    {
        required: true

    };
    service_validate_messages[name_prefix + "hiBusinessAreaCode"] =
    {
        required: "请选择服务范围"
    };

    //服务单价
    service_validate_rules[name_prefix + "tbxUnitPrice"] =
    {
        integer: true,
        required: true,
        range: [1, 20000]

    };
    service_validate_messages[name_prefix + "tbxUnitPrice"] =
    {
        integer: "请填写整数价格",
        required: "请填写单价",
        range: "请输入1-20000之间的价格"
    };

    // 服务订金
    service_validate_rules[name_prefix + "tbxDespoist"] = {
        required: true,
        range: [0, 20000]
    };

    service_validate_messages[name_prefix + "tbxDespoist"] =
    {
        required: "请填写订金",
        range: "请输入1-20000之间的价格"
    };

    //预约时间
    service_validate_rules[name_prefix + "tbxOrderDelay"] =
    {
        integer: "请填写整数价格",
        required: true,
        range: [1, 1440]

    };
    service_validate_messages[name_prefix + "tbxOrderDelay"] =
    {
        integer: "请填写整数分钟",
        required: "请填写预约时长",
        range: "请输入1-1440之间的数字"
    };

    $($("form")[0]).validate(
        {
            ignore: [],
            errorElement: "p",
            errorPlacement: function (error, element) {
                error.appendTo(element.parent());
            },
            rules: service_validate_rules,
            messages: service_validate_messages
        }
    );
});