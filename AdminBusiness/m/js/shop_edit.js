$(function () {


    $("#tbxBusinessYears").scroller({ preset: "time" });
     

    $("#ContentPlaceHolder1_ContentPlaceHolder1_imgBusinessImage").attr("data-ajax", "false");
    $("#ContentPlaceHolder1_ContentPlaceHolder1_imgChargePerson").attr("data-ajax", "false");

    //样式处理

    $('.myche3').append('<div class="li-inco">' + '</div>');

    $('.my-li').click(function () {
        var target = $(this).attr("target");

        $("#super_right_panel").panel("open");
        var that = this;
        /*
        var v = $(that).find(".display_holder").text();
        switch (target) {
            case "phone": $("#tbxContactPhone").val(v); break;

        }
        */
        //隐藏/显示
        open_click(target, function () {

            switch (target) {

                case "name": display(that, $("#tbxName").val()); break;
                case 'description': display(that, $("#tbxIntroduced").val()); break;
                case 'phone': phonedisplay(that, $("#tbxContactPhone").val()); break;
                case 'address': display(that, $("#tbxAddress").val()); break;
                case 'email': emaildisplay(that, $("#tbxEmail").val()); break;
                case 'workingyears': display(that, $("#tbxBusinessYears").val()); break;
                case 'staffamount': display(that, $("#selStaffAmount").val()); break;
                case 'chargename': display(that, $("#tbxContact").val()); break;
                case 'idtype': display(that, $("#selCardType option:selected").text()); break;
                case 'idno': CardIdNodisplay(that, $("#tbxCardIdNo").val()); break;

            }
        });

    });
    $(".panel-ul a.getMaphrefClass").attr("href", "#secondview");

    function display(li, value) {

        $(li).find(".display_holder").text(value);



    }
    //点击联系电话的调用
    function phonedisplay(li, value) {
        if (myShopvalidatemobile('#tbxContactPhone', '#vregPhonetxt')) {
            $(li).find(".display_holder").text(value);
        }


    }
    //点击邮箱的调用
    function emaildisplay(li, value) {
        if (myShopemailCheck('#tbxEmail', '#vregEmailtxt')) {
            $(li).find(".display_holder").text(value);
        }


    }
    //点击证件号的调用
    function CardIdNodisplay(li, value) {
        if (myshopCardCheck('#tbxCardIdNo', '#erroTxtCardIdNo')) {
            $(li).find(".display_holder").text(value);
        }


    }
    //初始化
    //帮助类,open_classname:激活右侧panel的<a> 的class值, save_fun:右侧panel确定按钮click事件应该做的事情.
    function open_click(open_classname, display_fun) {
        $(".rp").hide();
        $("." + open_classname).show();
        //2 更改右侧保存按钮的事件

        $("#rp_save").on('click', display_fun);
    }



});