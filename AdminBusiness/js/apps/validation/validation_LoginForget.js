/**
 * Created by chen on 2016/8/25.
 */
;$(function(){
    $.validator.setDefaults({
        ignore: []
    });

    $.validator.addMethod("captCha", function () {
        var code = $("#code").data("code");
        //console.log(code);
        return $("#captcha").val().toUpperCase() === code;
    }, "验证码错误");

    var reg_validate_rules = {};
    var reg_validate_messages = {};

    // 验证码
    reg_validate_rules['captcha'] =
    {
        required: true,
        captCha: true
    };

    reg_validate_messages['captcha'] =
    {
        required: "请填写验证码"
    };

    $($("form")[0]).validate(
        {
            ignore:[],
            errorPlacement: function(error) {
                console.log(error);
                error[0].id = "lblMsg";
                if(document.title == "一点办登录"){
                    error[0].className = "error lblMsg lblMsgShow";
                }
                $("#lblMsg").remove();
                $("#loginError").append( error );
                // Append error within linked label
            },
            errorElement: "span",
            rules: reg_validate_rules,
            messages: reg_validate_messages
        }
    );
});
