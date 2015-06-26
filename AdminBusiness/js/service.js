//商圈地图
var map = new BMap.Map("businessMap");
var cityListObject = new BMapLib.CityList({ container: "businessCity" });
map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
map.enableScrollWheelZoom();


//商圈缩略图
var submap = new BMap.Map("businessMapSub");
submap.centerAndZoom(new BMap.Point(116.404, 39.915), 10);
submap.disableDoubleClickZoom();
submap.disableDragging();


//ip定位
var myCity = new BMap.LocalCity();
myCity.get(function(result){
    submap.panTo(result.center);
});

//    商圈设置
$("#setBusiness").click(function (e) {
    $('#mapLightBox').lightbox_me({
        centered: true,
        onLoad : function(){
            myCity.get(function(result){
                map.panTo(result.center);
            });
        }
    });

    e.preventDefault();
});


var businessJson = "", //商圈JSON信息
    provinceName = "", //省
    cityName = "", //市
    boroughName = "", //区
    businessName = "";

cityListObject.addEventListener("cityclick",function(e){
    switch( e.area_type ){
        case 1 :
            provinceName = e.area_name;
            cityName = "", //重新选择省时，清空其他内容
            boroughName = "",
            businessName = ""
            break;
        case 2 :
            if ( e.area_code == 132 || e.area_code == 332  || e.area_code == 332 || e.area_code == 131 ) {
                provinceName = cityName = e.area_name;
            } else {
                cityName = e.area_name;
            };
            break;
        case 3 :
            boroughName = e.area_name;
            break;
        case 10 :
            businessName = e.area_name;
            break;
        default :
            console.log("error");
    };

    if (e.geo) {
        submap.panTo(e.geo);
    } else {
        return
    };

    console.log(e.geo);

    businessJson = {
        "provinceName" : provinceName,
        "cityName" : cityName,
        "boroughName" : boroughName,
        "businessName" : businessName,
        "businessLocLng" : e.geo.lng,
        "businessLocLat" : e.geo.lat
    };

});


$('#confBusiness').click(function () {
    $('#hiBusinessAreaCode').val(JSON.stringify(businessJson));

    var businiessText = $('#businessText');
    var businessNode = "<span>" + businessJson.provinceName + "</span><span>" + businessJson.cityName + "</span><span>" + businessJson.boroughName + "</span><span>" + businessJson.businessName + "</span>"
    businiessText.html(businessNode);
});

//信息载入是读取地图信息
(function readBusinessLoc() {
    if ( $('#hiBusinessAreaCode').val() ){
        var readBusinessJson = jQuery.parseJSON($('#hiBusinessAreaCode').val());
        var subMapPoint = new BMap.Point();
            subMapPoint.lng = readBusinessJson.businessLocLng;
            subMapPoint.lat = readBusinessJson.businessLocLat;
        submap.panTo(subMapPoint);

        var businessNode = "<span>" + readBusinessJson.provinceName + "</span><span>" + readBusinessJson.cityName + "</span><span>" + readBusinessJson.boroughName + "</span><span>" + readBusinessJson.businessName + "</span>"
        var businiessText = $('#businessText');
        businiessText.html(businessNode);
    }
})();


//服务选择
$(function () {
    $("#tabsServiceType").TabSelection({
        "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
        'enable_multi':false,
        "leaf_clicked": function (id) {
            tabRadioShow(id);
        }

    });
});

// 单选时，选择的服务类型显示
function tabRadioShow(id){
    var radioShowBox = $('#radioShowBox');

//            var TypeNodeBox = "<div item_id=" + id + ">" + radioText + "</div>";
    var TypeNodeBox = $(document.createElement("div"));


    if ( id ) {
        var radioItem = $(event.currentTarget).find($("span[item_id=" + id + "]"));
        var radioText = radioItem.text();

        $("#hiTypeId").val(id);
        TypeNodeBox.attr("item_id",id);
        radioItem.addClass('radioCk');
        console.log(radioItem.siblings());
        $(event.currentTarget).find($("span[item_id!=" + id + "]")).each(function(){
            $(this).removeClass('radioCk');
        });

        if ( radioShowBox.html() != "" ){
            radioShowBox.find('div').text(radioText);
        } else {
            TypeNodeBox.text(radioText);
            TypeNodeBox.addClass('business-radioCk');
            radioShowBox.append(TypeNodeBox);
        }
    } else {
        return
    }
}

//  多选时,选择的服务类型显示
function tabCheckedShow(that, id, checked, level) {
    var checkedShowBox = $('#serCheckedShow');
    var v_id = id;
    var v_level = level;
    var checkedItem = $($(that).parents('.serviceTabsItem')).find('.item');
    var checkedText = checkedItem.html();
    var checkedParentId = checkedItem.attr("parent_id");

    if (checked == true) {
        createTypeBox($(that), checkedParentId, checkedText, v_level);

    } else {
        removeTypeBox($(that), checkedParentId, checkedText, v_level);
    }

    function createTypeBox(that, p_id, text, level) {
        console.log(level);
        var printBox = checkedShowBox;
        var TypeNodeBox = "<div v_id=" + v_id + ">" + text + "</div>";

        switch (level) {
            case "0":
                printBox = checkedShowBox;
                printBox.append(TypeNodeBox);
                $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class", "level0");
                break;
            case "1":
                printBox = $(checkedShowBox.find($("div[v_id=" + p_id + "]")));
                printBox.append(TypeNodeBox);
                $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class", "level1");
                break;
            case "2":
                printBox = $(checkedShowBox.find($("div[v_id=" + p_id + "]")));
                printBox.append(TypeNodeBox);
                $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class", "level2");
                break;
            case "3":
                printBox = $(checkedShowBox.find($("div[v_id=" + p_id + "]")));
                printBox.append(TypeNodeBox);
                $(printBox.find($("div[v_id=" + v_id + "]"))).attr("class", "level3");
                break;
            default:
                break;
        };
    };

    function removeTypeBox(that, p_id, text, level) {
        $(checkedShowBox.find($("div[v_id=" + v_id + "]"))).remove();
    }
}

$("#setSerType").click(function (e) {
    $('#SerlightBox').lightbox_me({
        centered: true
    });
    e.preventDefault();
})
