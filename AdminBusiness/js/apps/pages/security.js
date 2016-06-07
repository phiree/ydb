$(".btnChange").click(function (eve) {
    var e = eve || window.event;

    var changed_data = {};
    var userId = $("#currentUserId").val();
    var change_field = $(this).attr("change_field");
    var newValue = "";

    switch (change_field) {
        case "phone":
            newValue = $("#tbxNewPhone").val();
            if ( !isPhone(newValue) ){
                warnText(this,"手机号码格式有误");
                e.preventDefault();
                return false;
            }
            break;
        case "email":
            newValue = $("#tbxNewEmail").val();
            if ( !isEmail(newValue) ){
                warnText(this,"邮箱格式有误");
                e.preventDefault();
                return false;
            }
            break;
    }

    
    changed_data["id"] = userId;
    changed_data["changed_field"] = change_field;
    changed_data["changed_value"] = newValue;

    $.post("/ajaxservice/changebusinessInfo.ashx", changed_data,
        function (result) {
            if (result.result == "True") {
                $("#tbxNewPhone").val("");
                $("#tbxNewEmail").val("");
                alert("修改成功");
                location.reload();
            }
            else {
                alert(result.msg);
            }

        }); //post
});     //click

/* 简单判断验证邮箱是否填写，以显示邮箱验证按钮 */
if ($("#currentUserEmail").html() != ""){
    $("#currentUserEmailVali").removeClass("dis-n");
}

function isEmail(value) {
    var email_rlue = /\w+((-w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+/;

    if ( value != "") {
        return !!email_rlue.test(value);
    } else {
        return false;
    }
}

function isPhone(value) {
    var phone_rule = /(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)|(13\d{9}$)|(15[0-9]\d{8}$)|(18[0-9]\d{8}$)|(17[0-9]\d{8}$)/;
    if ( value != "" ) {
        return !!phone_rule.test(value);
    } else {
        return false;
    }
}

function warnText ( ele , str ){
    var $that = $(ele);

    if ( !$that.parent().find(".secret-warn").length ) {
        var warnNode = $(document.createElement("p"));
        warnNode.addClass("secret-warn");
        warnNode.text(str);
        $that.parent().append(warnNode);
    } else {
        $that.parent().find(".secret-warn").text(str);
    }
}

