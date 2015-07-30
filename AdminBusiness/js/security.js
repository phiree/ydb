$(".btnChange").click(function (eve) {
    var e = eve || window.event;

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


function isEmail(value) {
    var email_rlue = /\w+((-w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+/;

    if ( value != "") {
        return email_rlue.test(value) ? true : false;
    } else {
        return false;
    }
}

function isPhone(value) {
    var phone_rule = /0?(13|14|15|17|18)[0-9]{9}/;
    if ( value != "" ) {
        return phone_rule.test(value) ? true : false;
    } else {
        return false;
    }
}

function warnText ( ele , str ){
    var $that = $(ele);

    if ( !$that.parent().find(".warn-text").length ) {
        var warnNode = $(document.createElement("span"));
        warnNode.addClass("warn-text");
        warnNode.text(str);
        $that.parent().append(warnNode);
    } else {
        $that.parent().find(".warn-text").text(str);
    }
}
