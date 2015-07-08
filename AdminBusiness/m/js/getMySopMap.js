
// 百度地图API功能
    var map = new BMap.Map("container");
    map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
    var myCityListObject = new BMapLib.CityList({
        container : "city-container",
        map : map
    });
    console.log(map.getCenter());
    map.enableScrollWheelZoom();
    map.clearOverlays();    //清除地图上所有覆盖物

    map.addEventListener("tilesloaded", function (e) {
        //  var point = new BMap.Point(116.404, 39.915);
        // var marker = new BMap.Marker(point);
        //   map.addOverlay(marker);
        //$("#businessID-button").css("display","none");
        $("#city-container .ui-select:eq(3)").css("display", "none");
      //  $("#city-container .ui-select:eq(3)").p

    });
    function tiemout() {
        setTimeout(function () {
            defaultSetPoint();
        }, 500);
    }
    function defaultSetPoint (){
        if ($('#hiAddrId').attr("value")) {
            var readAddrJson = jQuery.parseJSON($('#hiAddrId').attr("value"));
            var addressNode = readAddrJson.province + readAddrJson.city + readAddrJson.district + readAddrJson.street + readAddrJson.streetNumber;
            var vPoint = new BMap.Point(readAddrJson.lng, readAddrJson.lat);
            var rMarker = new BMap.Marker(vPoint); // 创建点

            $("#serArea-txt").html(addressNode);
            $(".showAdd").html(addressNode);

            map.setCenter(vPoint);     // 初始化地图,设置中心点坐标和地图级别
            map.setZoom(12);
            console.log(vPoint);
            console.log(map.getCenter());
            map.addOverlay(rMarker);    //增加点
        }
    }
    setTimeout(defaultSetPoint, 500); //简单粗暴的通过延时解决页面初始化时，中心点被CityList的DataMgr初始覆盖;

    var gc = new BMap.Geocoder(); //地址解析类
    function showInfo(e) {
        map.clearOverlays();
        var point = new BMap.Point(e.point.lng, e.point.lat);
        var marker = new BMap.Marker(point); // 创建点
        map.addOverlay(marker);    //增加点
        var gc = new BMap.Geocoder();
        gc.getLocation(point, function (rs) {
            var addComp = rs.addressComponents;
            var addJson = {
                "province": addComp.province,
                "city": addComp.city,
                "district": addComp.district,
                "street": addComp.street,
                "streetNumber": addComp.streetNumber,
                "lng": rs.point.lng,
                "lat": rs.point.lat
            };

            $('#hiAddrId').attr("value", JSON.stringify(addJson));


            var mapAddr = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
//            var lngLat = e.point.lng + "," + e.point.lat;

          //  $("#lngLat").val(lngLat);//经度与纬度
            $(".showAdd").html(mapAddr);//地点

        });


    }
//    function deletePoint() {
//        var allOverlay = map.getOverlays();
//
//        map.clearOverlays();
//    }


    map.addEventListener("click", showInfo);
