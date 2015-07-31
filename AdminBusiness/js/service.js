/**
* 初始化商圈地图
*/
var map = new BMap.Map("businessMap");
var cityListObject = new BMapLib.CityList({ container: "businessCity" , map : map });
map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
map.enableScrollWheelZoom();


/**
* 初始化商圈缩略地图
*/
var submap = new BMap.Map("businessMapSub");
submap.centerAndZoom(new BMap.Point(116.404, 39.915), 10);
submap.disableDoubleClickZoom();
submap.disableDragging();


/**
 * 根据ip定位当前位置
 */
var myCity = new BMap.LocalCity();

$("#setBusiness").click(function (e) {
    $('#mapLightBox').lightbox_me({
        centered: true,
        onLoad : function(){
            mapInit();
        }
    });
    e.preventDefault();
});

function writeBusiness(){
    /**
     * JSON格式商圈信息
     * @JSON
     */
    var businessJson = {};
    var provinceName = "",
        cityName = "",
        boroughName = "",
        businessName = "";

    /**
     * 点击城市时地图定位到指定城市
     * @param e
     */
    function writeBusinessJson (e) {
        console.log(e.area_type);
        console.log(e.area_name);
        console.log(e.area_code);

        switch (e.area_type) {
            //重新选择省时，清空其他内容。
            case 1 :
                provinceName = e.area_name;
                cityName = "";
                boroughName = "";
                businessName = "";
                break;
            case 2 :
                if (e.area_code == 132 || e.area_code == 332 || e.area_code == 289 || e.area_code == 131) {
                    provinceName = "";
                    cityName = e.area_name;
                } else {
                    cityName = e.area_name;
                }
                break;
            case 3 :
                boroughName = e.area_name;
                break;
            case 10 :
                businessName = e.area_name;
                break;
            default :
                alert("地图选择有误");
        }

        if (e.geo) {
            submap.panTo(e.geo);
        } else {
            return
        }

        businessJson = {
            "provinceName": provinceName,
            "cityName": cityName,
            "boroughName": boroughName,
            "businessName": businessName,
            "businessLocLng": e.geo.lng,
            "businessLocLat": e.geo.lat
        };
    }


    cityListObject.addEventListener("cityclick",
        function(e){
            writeBusinessJson (e);
        }
    );

    $('#confBusiness').click(function () {
        var businiessText = $("#businessText");

        if ( !$.isEmptyObject(businessJson) ){
            $('#hiBusinessAreaCode').attr("value",JSON.stringify(businessJson));
            var businessNode = "<span>" + businessJson.provinceName + "</span><span>" + businessJson.cityName + "</span><span>" + businessJson.boroughName + "</span><span>" + businessJson.businessName + "</span>"
            businiessText.html(businessNode);
        } else {
            return;
        }
    });
}

writeBusiness();

/**
 * 载入时读取地图信息
 */
function mapInit() {
    if ( $('#hiBusinessAreaCode').attr("value") ){
        var readBusinessJson = jQuery.parseJSON($('#hiBusinessAreaCode').attr("value"));
        var subMapPoint = new BMap.Point();
        subMapPoint.lng = readBusinessJson.businessLocLng;
        subMapPoint.lat = readBusinessJson.businessLocLat;
        submap.panTo(subMapPoint);
        map.panTo(subMapPoint);

        var businessNode = "<span>" + readBusinessJson.provinceName + "</span><span>" + readBusinessJson.cityName + "</span><span>" + readBusinessJson.boroughName + "</span><span>" + readBusinessJson.businessName + "</span>"
        var businiessText = $('#businessText');
        businiessText.html(businessNode);
    } else {
        myCity.get(function(result){
            submap.panTo(result.center);
        });
        myCity.get(function(result){
            map.panTo(result.center);
        });
    }
};

mapInit();

(function readTypeData(){
    var hiTypeValue = $("#hiTypeId").attr("value");
    if ( hiTypeValue != undefined ) {
        $("#lblSelectedType").removeClass("dis-n");
        $("#lblSelectedType").addClass("d-inb");
    } else {
        return;
    }
})();


/**
 * 初始化服务选择
 */
//$("#tabsServiceType").TabSelection({
//    "datasource": "/ajaxservice/tabselection.ashx?type=servicetype",
//    'enable_multi':false,
//    "leaf_clicked": function (id) {
//        tabRadioShow(id);
//    }
//});

/**
 * 单选时，选择的服务类型显示。
 */
function tabRadioShow(id){
    var radioShowBox = $('#radioShowBox');
    var TypeNodeBox = $(document.createElement("div"));
    var radioContainer = $('#tabsServiceType');

    if ( id ) {
        var radioItem = radioContainer.find($("span[item_id=" + id + "]"));
        var radioText = radioItem.text();

        $("#hiTypeId").attr("value", id );
        TypeNodeBox.attr("item_id",id);
        radioItem.addClass('radioCk');

        radioContainer.find($("span[item_id!=" + id + "]")).each(function(){
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
        return false ;
    }
}


/**
 * 多选时,选择的服务类型显示。
 */
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
