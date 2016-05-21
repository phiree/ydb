$.validator.setDefaults({
    ignore: []
});

$.validator.addMethod("userName", function (username) {
    return /^(\w+((-w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+)$/.test(username);
}, "用户名格式错误");

$.validator.addMethod("pwd", function (pwd) {
    return /^[A-Za-z0-9_-]+$/.test(pwd);
}, "密码格式错误");

$.validator.addMethod("pwdConfirm", function () {
    return $("#regPsConf").val() === $("#regPs").val();
}, "两次输入的密码不一致");

var reg_validate_rules = {};
var reg_validate_messages = {};


//tbxname
reg_validate_rules['tbxUserName'] =
{
    required: true,
    userName: true,
    remote: "/ajaxservice/is_username_duplicate.ashx?"
};

reg_validate_messages['tbxUserName'] =
{
    required: "请填写用户名",
    userName: "用户名格式错误",
    remote: "用户已存在"
};

reg_validate_rules['regPs'] =
{
    required: true,
    minlength: 6,
    maxlength: 20,
    pwd: true
};

reg_validate_messages['regPs'] =
{
    required: "请填写密码",
    minlength: "密码不能少于6个字符",
    maxlength: "密码不能超过20个字符"
};

reg_validate_rules['regPsConf'] =
{
    required: true,
    pwd: true,
    pwdConfirm: true
};

reg_validate_messages['regPsConf'] =
{
    required: "请再次输入密码",
    pwdConfirm: "两次输入的密码不一致"
};

reg_validate_rules['agreeLic'] =
{
    required: true
};

reg_validate_messages['agreeLic'] =
{
    required: "请阅读协议，并同意"
};