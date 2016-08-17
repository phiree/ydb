//(function () {
//    $(function () {
//        $(".footer").html("<h4 style='color:#999'>[<span style='font-variant:small-caps'>基本文档：</span>" +$("#input_baseUrl")[0].value + "，<span style='font-variant:small-caps'>版本：</span>v2]");
//    });
//})();

$(document).ready(function () {
    //alert("Hello from custom JavaScript file.");
    var dd = $(".info_title").html();
    //alert(dd);
    dd = dd.replace(/Dianzhu\.Web\.RestfulApi-/, "");
    //DateTime d = new DateTime(2000, 1, 1);
    //Console.WriteLine(d.AddDays(3125).AddSeconds(14653 * 2).ToString("yyyy/MM/dd HH:mm:ss")); 
    //string[] str = v.Split('.');
    //string t = d.AddDays(int.Parse(str[2])).AddSeconds(int.Parse(str[3]) * 2).ToString("yyyy/MM/dd HH:mm:ss");
    //return  "@版本号：" + v+"，"+ "发布时间：" + t +"@";
    var dds = dd.split(".");
    var datet = new Date("2000-01-01 00:00:00");
    datet = new Date(Date.parse(datet) + (86400000 * parseFloat(dds[2])));
    datet = new Date(Date.parse(datet) + (1000 * parseFloat(dds[3])*2));
    //datet1 = 86400000 * 6600;
    //alert(datet);
    //datet.DateAdd("d", parseInt(dds[2]));

    //datet.DateAdd("s", parseInt(dds[3] * 2));
    var t = datet.getFullYear().toString() + "-" + (datet.getMonth() + 1).toString() + "-" + datet.getDate().toString() + " " + datet.getHours().toString() + ":" + datet.getMinutes().toString() + ":" + datet.getSeconds().toString();
    $(".footer").html("<h4 style='color:#999'><span style='font-variant:small-caps'>基础文档：</span>" + $("#input_baseUrl")[0].value + "，<span style='font-variant:small-caps'>版本号：</span>" + dd + "，<span style='font-variant:small-caps'>发布时间：</span>" + t + "");
});


//(function () {
//    $(function () {
//        var basicAuthUI =
//            '<div class="input"><input placeholder="username" id="input_username" name="username" type="text" size="10"></div>' +
//            '<div class="input"><input placeholder="password" id="input_password" name="password" type="password" size="10"></div>';
//        $(basicAuthUI).insertBefore('#api_selector div.input:last-child');
//        $("#input_apiKey").hide();
//        //&nbsp;
//        $('#input_username').change(addAuthorization);
//        $('#input_password').change(addAuthorization);
//    });
//    //&nbsp;
//    function addAuthorization() {
//        var username = $('#input_username').val();
//        var password = $('#input_password').val();
//        if (username && username.trim() != "" && password && password.trim() != "") {
//            var basicAuth = new SwaggerClient.PasswordAuthorization('basic', username, password);
//            window.swaggerUi.api.clientAuthorizations.add("basicAuth", basicAuth);
//            console.log("authorization added: username = " + username + ", password = " + password);
//        }
//    }
//})();

//设置	描述
//y	年
//q	季度
//m	月
//d	日
//w	周
//h	小时
//n	分钟
//s	秒
//ms	毫秒
Date.prototype.DateAdd = function (strInterval, Number) {
    var dtTmp = this;
    switch (strInterval) {
        case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));
        case 'n': return new Date(Date.parse(dtTmp) + (60000 * Number));
        case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));
        case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));
        case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
        case 'q': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'm': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'y': return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
    }
}

//var myDate = new Date();
//myDate.getYear();        //获取当前年份(2位)
//myDate.getFullYear();    //获取完整的年份(4位,1970-????)
//myDate.getMonth();       //获取当前月份(0-11,0代表1月)
//myDate.getDate();        //获取当前日(1-31)
//myDate.getDay();         //获取当前星期X(0-6,0代表星期天)
//myDate.getTime();        //获取当前时间(从1970.1.1开始的毫秒数)
//myDate.getHours();       //获取当前小时数(0-23)
//myDate.getMinutes();     //获取当前分钟数(0-59)
//myDate.getSeconds();     //获取当前秒数(0-59)
//myDate.getMilliseconds();    //获取当前毫秒数(0-999)
//myDate.toLocaleDateString();     //获取当前日期
//var mytime = myDate.toLocaleTimeString();     //获取当前时间
//myDate.toLocaleString();        //获取日期与时间

//不错的时间格式化函数
//function DatePart(interval) {
//    var myDate = new Date();
//    var partStr = '';
//    var Week = ['日', '一', '二', '三', '四', '五', '六'];
//    switch (interval) {
//        case 'y': partStr = myDate.getYear(); break;
//        case 'Y': partStr = myDate.getFullYear(); break;
//        case 'm': partStr = myDate.getMonth() + 1; break;
//        case 'M':
//            var myDatem = myDate.getMonth() + 1;
//            partStr = myDatem > 9 ? myDatem : '0' + myDatem.toString();
//            break;
//        case 'd': partStr = myDate.getDate(); break;
//        case 'D':
//            partStr = myDate.getDate() > 9 ? myDate.getDate() : '0' + myDate.getDate().toString();
//            break;
//        case 'w': partStr = Week[myDate.getDay()]; break;
//        case 'ww': partStr = myDate.WeekNumOfYear(); break;
//        case 'h': partStr = myDate.getHours(); break;
//        case 'n': partStr = myDate.getMinutes(); break;
//        case 's': partStr = myDate.getSeconds(); break;
//    }
//    return partStr;
//}