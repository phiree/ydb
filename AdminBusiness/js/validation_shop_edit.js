
 

var service_validate_rules ={};
var service_validate_messages={};

 
//tbxname
service_validate_rules[name_prefix + "tbxName"] =
{
    required: true,
    maxlength: 100
};
service_validate_messages[name_prefix + "tbxName"] =
{
    required: "请填写店铺名称",
    maxlength: "不能超过100个字符"
};
//hiTypeId
service_validate_rules[name_prefix + "tbxIntroduced"] =
{
    required: true,
     maxlength: 1000
};
service_validate_messages[name_prefix + "tbxIntroduced"] =
{
    required: "请输入店铺介绍",
    maxlength: "不能超过1000个字符"
};
//minprice
service_validate_rules[name_prefix + "tbxContactPhone"] =
{
    phone: true,
     
};
 
 
 
//serviceScope
service_validate_rules[name_prefix + "tbxEmail"] =
{
    email: true
     
};
service_validate_messages[name_prefix + "tbxEmail"] =
{
    email: "请输入正确的email格式"
};
//服务单价
service_validate_rules[name_prefix + "tbxBusinessYears"] =
{
     
    range: [1, 1000]

};
service_validate_messages[name_prefix + "tbxBusinessYears"] =
{
   
    range: "请输入1-1000之间的数值"
};
//预约时间
service_validate_rules[name_prefix + "tbxContact"] =
{
   maxlength:20

};
service_validate_messages[name_prefix + "tbxContact"] =
{
     
    maxlength: "请输入20字以内的姓名(汉字)"
};
 
//服务时间
if ($("#selCardType").val()=="0")
{
    service_validate_rules[name_prefix + "tbxCardIdNo"] =
    {
     
            required: true,
            idcard: true


        };


    service_validate_messages[name_prefix + "tbxCardIdNo"] =
    {
   
            required: "请填写身份证号码"
           

        };
    }
else
{
    service_validate_rules[name_prefix + "tbxCardIdNo"] =
    {
     
            required: true,
            maxlength:50


        };


    service_validate_messages[name_prefix + "tbxCardIdNo"] =
    {
   
            required: "请填写证件号码",
           maxlength:"证件号码超过50个字符,请联系客服."

        };
}

    service_validate_rules[name_prefix + "hiAddrId"] =
{

    required: true
     


};
service_validate_messages[name_prefix + "hiAddrId"] =
{

    required: "请选择店铺地址"
   


};
 



