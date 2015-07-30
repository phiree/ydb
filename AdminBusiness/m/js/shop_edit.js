$(function () {




    $("#ContentPlaceHolder1_ContentPlaceHolder1_imgBusinessImage").attr("data-ajax", "false");
    $("#ContentPlaceHolder1_ContentPlaceHolder1_imgChargePerson").attr("data-ajax", "false");

    //样式处理

  $('.myche3').append('<div class="li-inco">' +'</div>');

    $('.my-li').click(function () {
        var target = $(this).attr("target");




        $("#super_right_panel").panel("open");

        var that = this;
        //隐藏/显示
        open_click(target, function () {
          
            switch (target) {
                case "name": display(that, $("#tbxName").val()); break;
                case 'description': display(that, $("#tbxIntroduced").val()); break;
                case 'phone': phonedisplay(that, $("#tbxContactPhone").val()); break;
                case 'address': display(that, $("#tbxAddress").val()); break;
                case 'email': display(that, $("#tbxEmail").val()); break;
                case 'workingyears': display(that, $("#tbxBusinessYears").val()); break;
                case 'staffamount': display(that, $("#selStaffAmount").val()); break;
                case 'chargename': display(that, $("#tbxContact").val()); break;
                case 'idtype': display(that, $("#selCardType option:selected").text()); break;
                case 'idno': display(that, $("#tbxCardIdNo").val()); break;

            }
        });

    });
    $(".panel-ul a.getMaphrefClass").attr("href", "#secondview");

    function display(li, value) {
       
          $(li).find(".display_holder").text(value);
     


  }

  function phonedisplay(li, value) {
      if (myShopvalidatemobile('#tbxContactPhone', '#vregPhonetxt')) {
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