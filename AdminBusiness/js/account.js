var map = new BMap.Map("addressMap");
var cityListObject = new BMapLib.CityList({ container: "addressCity", map : map});
var geoc = new BMap.Geocoder();
//var myGeo = new BMap.Geocoder();

map.enableScrollWheelZoom();
map.disableDoubleClickZoom();
map.clearOverlays();

var myCity = new BMap.LocalCity();

map.addEventListener("click",setAddressPoint);

function setAddressPoint (e){
    map.clearOverlays();

    var addPrintBox = $("#addPrintBox");
    var addressText = $("#addressText");
    var addressP = new BMap.Point(e.point.lng, e.point.lat);
    var addressMark = new BMap.Marker(addressP);
    var addrNodeBox = $(document.createElement("div"));


    geoc.getLocation(addressP, function(rs){
        var addComp = rs.addressComponents;
        var addJson = {
            "province": addComp.province,
            "city": addComp.city,
            "district": addComp.district,
            "street" : addComp.street,
            "streetNumber" : addComp.streetNumber,
            "lng" : rs.point.lng,
            "lat" : rs.point.lat
        };

        //console.log(JSON.stringify(addJson));
        $('#hiAddrId').attr("value",JSON.stringify(addJson));
        var addressNode = "<span>" + addComp.province + "</span><span>" + addComp.city + "</span><span>" + addComp.district + "</span><span>" + addComp.street + "</span><span>" + addComp.streetNumber + "</span>"
        addressText.html(addressNode);

        if ( addPrintBox.html() != "" ){
            addPrintBox.find('div').html(addressNode);
        } else {
            addrNodeBox.html(addressNode);
            addrNodeBox.addClass('myshop-addPrint');
            addPrintBox.append(addrNodeBox);
        }
    });
    map.addOverlay(addressMark);

}

/**
 * 载入时读取地图信息
 */
(function readAddressLoc() {
    if ( $('#hiAddrId').attr("value") ){
        var readAddrJson = jQuery.parseJSON($('#hiAddrId').attr("value"));
        var addrNodeBox = $(document.createElement("div"));
        var addressNode = "<span>" + readAddrJson.province + "</span><span>" + readAddrJson.city + "</span><span>" + readAddrJson.district + "</span><span>" + readAddrJson.street + "</span><span>" + readAddrJson.streetNumber + "</span>"
        var addPrintBox = $("#addPrintBox");
        var addressText = $("#addressText");

        addressText.html(addressNode);
        addrNodeBox.html(addressNode);
        addrNodeBox.addClass('myshop-addPrint m-b10');
        addPrintBox.append(addrNodeBox);
    }
})();

/**
 * 打开地图时加载保存的数据
 */
$("#setAddress").click(function (e) {
    $('#addrlightBox').lightbox_me({
        centered: true,
        onLoad : function(){
            if( !$("#hiAddrId").attr("value") ) {
                myCity.get(function(result){
                    map.panTo(result.center);
                });
            } else {
                var readAddrJson = jQuery.parseJSON($('#hiAddrId').attr("value"));
                var nPoint = new BMap.Point(readAddrJson.lng, readAddrJson.lat);
                var addressMark = new BMap.Marker(nPoint);

                map.centerAndZoom(nPoint,13);
                map.addOverlay(addressMark);
            }

        }
    });
    e.preventDefault();
});

//        function G(id) {
//                return document.getElementById(id);
//            }
//
//            var ac = new BMap.Autocomplete(    //建立一个自动完成的对象
//                    {"input" : "suggestId"
//                        ,"location" : map
//                    });
//
//            ac.addEventListener("onhighlight", function(e) {  //鼠标放在下拉列表上的事件
//                var str = "";
//                var _value = e.fromitem.value;
//                var value = "";
//                if (e.fromitem.index > -1) {
//                    value = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
//                }
//                str = "FromItem<br />index = " + e.fromitem.index + "<br />value = " + value;
//
//                value = "";
//                if (e.toitem.index > -1) {
//                    _value = e.toitem.value;
//                    value = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
//                }
//                str += "<br />ToItem<br />index = " + e.toitem.index + "<br />value = " + value;
//                G("searchResultPanel").innerHTML = str;
//            });
//
//            var myValue;
//            ac.addEventListener("onconfirm", function(e) {    //鼠标点击下拉列表后的事件
//                var _value = e.item.value;
//                myValue = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
//                G("searchResultPanel").innerHTML ="onconfirm<br />index = " + e.item.index + "<br />myValue = " + myValue;
//
//                setPlace();
//            });
//
//            function setPlace(){
//                map.clearOverlays();    //清除地图上所有覆盖物
//                function myFun(){
//                    var pp = local.getResults().getPoi(0).point;    //获取第一个智能搜索的结果
//                    map.centerAndZoom(pp, 18);
//                    map.addOverlay(new BMap.Marker(pp));    //添加标注
//
//                }
//                var local = new BMap.LocalSearch(map, { //智能搜索
//                    onSearchComplete: myFun
//                });
//                local.search(myValue);
//            }

//        // 将地址解析结果显示在地图上,并调整地图视野
//        myGeo.getPoint("海南省海口市龙华区海秀中路125号", function(point){
//            if (point) {
//                map.centerAndZoom(point, 18);
//                map.addOverlay(new BMap.Marker(point));
//            }else{
//                alert("您选择地址没有解析到结果!");
//            }
//        }, "北京市");