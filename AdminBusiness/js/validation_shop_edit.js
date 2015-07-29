
 

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
 
 
    service_validate_rules[name_prefix + "tbxCardIdNo"] =
    {
     
            required: true,
            idcard: function(element){
            if ($("#selCardType").val()=="0")
            {
                return true;
            }
            },
            maxlength:function(element){
             if ($("#selCardType").val()=="1")
            {
                return 50;
            }
            }


        };


    service_validate_messages[name_prefix + "tbxCardIdNo"] =
    {
   
            required: "请填写证件号码",
            idcard:"请输入正确的证件号码",
            maxlength:"证件号码超出50个字符,请核实修改或者联系客服"
           

        };

          service_validate_rules[name_prefix + "selStaffAmount"] =
    {
     
            required: true,
            range: [1,1000]

        };


    service_validate_messages[name_prefix + "selStaffAmount"] =
    {
   
            required: "请填写员工人数",
            range:"请输入1-1000之间的数值"
           

        };
 
 
    service_validate_rules[name_prefix + "hiAddrId"] =
{

    required: true
     


};
service_validate_messages[name_prefix + "hiAddrId"] =
{

    required: "请选择店铺地址"

};
$.validator.addMethod('filesize', function(value, element, param) {
    // param = size (en bytes) 
    // element = element to validate (<input>)
    // value = value of the element (file name)
    return this.optional(element) || (element.files[0].size <= param) 
});
service_validate_rules["input-file-btn"]=
{
    accept: "image/*",
    filesize:500*1024
     
};
service_validate_messages["input-file-btn"] =
{

    accept: "请选择(png,jpg,gif)格式的图片文件",
    filesize:"请上传小于2M的图片"

};
 



