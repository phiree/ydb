$.validator.setDefaults({
    ignore: []
});
$.validator.addMethod("phone", function (value, element) {
    return /((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/.test(value);
}, "请输入有效的电话号码");
$.validator.addMethod("integer", function (value, element) {
    return value == "" ? true : /^[0-9]\d*$/.test(value);
}, "请填写整数");

$.validator.addMethod("time24", function (value, element) {
    return /([01]\d|2[0-3])(:[0-5]\d)/.test(value);
}, "Invalid time format.");

var service_validate_rules ={};
var service_validate_messages={};
//编号

service_validate_messages[name_prefix + "Code"] =
{
    required: "请填写编号",
    maxlength: "不能超过100个字符"
};

service_validate_rules[name_prefix + "Code"] =
{
    required: true,
    integer: true,
    range: [1, 100000]
};


service_validate_messages[name_prefix + "Code"] =
{
    integer: "请输入如-xx1---xx1xx-的数字",
    required: "请填写编号",
    range: "请输入如-xx1---xx1xx-的数字"


};












//姓名
service_validate_rules[name_prefix + "Name"] =
{
    required: true,
    maxlength: 50
};
service_validate_messages[name_prefix + "Name"] =
{
    required: "请填写姓名",
    maxlength: "不能超过50个字符"
};
//昵称
service_validate_rules[name_prefix + "NickName"] =
{
    required: true,
    maxlength: 50
};
service_validate_messages[name_prefix + "NickName"] =
{
    required: "请填写昵称",
    maxlength: "不能超过50个字符"
};
service_validate_rules[name_prefix + "Phone"] =
{
    phone: true
};



