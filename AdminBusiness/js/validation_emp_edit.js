$.validator.setDefaults({
    ignore: []
});
$.validator.addMethod("phone", function (value, element) {
    return /(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)|(13\d{9}$)|(15[0-9]\d{8}$)|(18[0-9]\d{8}$)|(17[0-9]\d{8}$)/.test(value);
}, "请输入有效的电话号码");
$.validator.addMethod("code", function (value, element) {
    return value == "" ? true : /(^[\x00-\xFF]+$)/.test(value);
}, "请勿输入特殊字符");


var service_validate_rules ={};
var service_validate_messages={};

//编号
service_validate_rules[name_prefix + "Code"] =
{
    required: true,
    code: true,
    maxlength: 100
};

service_validate_messages[name_prefix + "Code"] =
{
    required: "请填写编号",
    maxlength: "请勿超过100个字符"
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



