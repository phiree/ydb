$(function () {

    //初始化
    displaycertificated();
    displaycompensation();
    displayDescription();
    displayis_for_business();
    displaymaxorder_day();
    displaymaxorder_hour();
    displayminprice();
    displayName();
    displayopen_period();
    displaypaytype();
    displaypreorder_delay();
    displayservicemode();
    displayServiceScope();
    displayunitprice();
    //打开右侧panel

    $(".panel-ul a.hrefClass").attr("href", "#right-panel-super");

    /*打开右侧panel时
    1)隐藏右侧panel中的其他元素,
    2)修改右侧确定按钮的click事件
    3)右侧确定按钮点击时,关闭panel,给相关元素赋值.
    */

    //名称
    $("#open_name").click(function () {
        open_click("name", displayName);
    });
    function displayName() { $("#serviceName").text($("#tbxName").val()); }


    //类别
    $("#open_servicetype").click(
        function () {
            open_click("servicetype", null);
        }
    );

    //描述
    $("#open_description").click(
        function () {
            open_click("description", displayDescription);
        }
    );
    function displayDescription() { $("#serInfo-txt").text($("#tbxDescription").val()); }



    //服务范围
    $("#open_servicescope").click(
        function () {
            open_click("servicescope", displayServiceScope);
        }
    );
    function displayServiceScope() { $("#serArea-txt").text($("#hiBusinessAreaCode").val()); }
    //最低服务费
    $("#open_minprice").click(
        function () {
            open_click("minprice", displayminprice);
        }
    );
    function displayminprice() { $("#serMinPrice-txt").text($("#tbxMinPrice").val()); }
    //服务单价

    $("#open_unitprice").click(
        function () {
            open_click("unitprice", displayunitprice);
        }
    );
    function displayunitprice() {
        var price = $("#tbxUnitPrice").val();
        var unit = $("#rblChargeUnit input[type=radio]:checked").siblings("label").text();
        $("#spanUnitPrice").text(price + "元/每" + unit);
    }
    //提前预约时长

    $("#open_preorder_delay").click(
        function () {
            open_click("preorder_delay", displaypreorder_delay);
        }
    );
    function displaypreorder_delay() { $("#serPreorder_delay_txt").text($("#tbxOrderDelay").val()); }
    //open_openperiod
    $("#open_openperiod").click(
        function () {
            open_click("open_period", displayopen_period);
        }
    );
    function displayopen_period() {
        var begin = $("#tbxServiceTimeBegin").val();
        var end = $("#tbxServiceTimeEnd").val();
        $("#serPeriod-txt").text(begin + "至" + end);
    }
    //最大接单量每日

    $("#open_maxorder_day").click(
        function () {
            open_click("maxorder_day", displaymaxorder_day);
        }
    );
    function displaymaxorder_day() { $("#spanMax_order_day").text($("#tbxMaxOrdersPerDay").val()); }
    //最大接单量每小时

    $("#open_maxorder_hour").click(
        function () {
            open_click("maxorder_hour", displaymaxorder_hour);
        }
    );
    function displaymaxorder_hour() { $("#span_maxorder_hour").text($("#tbxMaxOrdersPerHour").val()); }
    //服务方式(是否上门

    $("#open_servicemode").click(
        function () {
            open_click("servicemode", displayservicemode);
        }
    );
    function displayservicemode() {
        $("#spanServicemode").text($("#rblServiceMode input[type=radio]:checked").siblings("label").text());
    }
    //可以对公
    $("#open_is_for_business").click(
        function () {
            open_click("is_for_business", displayis_for_business);
        }
    );
    function displayis_for_business() {
        var checked = $("#cblIsForBusiness[type=checkbox]")[0].checked;
        $("#span_forbusiness").text(checked ? "是" : "否");
    }
    //先行赔付
    $("#open_compensation").click(
        function () {
            open_click("is_compensation_advance", displaycompensation);
        });
    function displaycompensation() {
        var checked = $("#cbxIsCompensationAdvance[type=checkbox]")[0].checked;
        $("#span_compensation").text(checked ? "是" : "否");
    }
    //平台认证
    $("#open_certificated").click(
        function () {
            open_click("is_certificated", displaycertificated);
        });
    function displaycertificated() {
        var checked = $("#cbxIsCertificated[type=checkbox]")[0].checked;
        $("#span_certificated").text(checked ? "是" : "否");
    }
    //支付方式

    $("#open_paytype").click(
        function () {
            open_click("paytype", displaypaytype);
        });
    function displaypaytype() {
        $("#span_paytype").text($("#rblPayType input[type=radio]:checked").siblings("label").text());
    }
    /*打开右侧panel时*/

    //帮助类,open_classname:激活右侧panel的<a> 的class值, save_fun:右侧panel确定按钮click事件应该做的事情.
    function open_click(open_classname, display_fun) {
        $(".rp").hide();
        $("." + open_classname).show();
        //2 更改右侧保存按钮的事件
        $("#rp_save").on('click', display_fun);
    }
});