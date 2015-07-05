
// 百度地图API功能
    var map = new BMap.Map("container");
    var myCityListObject = new BMapLib.CityList({container : "city-container"});
    map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
  //  map.centerAndZoom("海口", 11);     // 初始化地图,设置中心点坐标和地图级别
    map.enableScrollWheelZoom();

    map.addEventListener("tilesloaded", function (e) {
        //  var point = new BMap.Point(116.404, 39.915);
        // var marker = new BMap.Marker(point);           
        //   map.addOverlay(marker);
        //$("#businessID-button").css("display","none");

        $("#city-container .ui-select:eq(3)").css("display", "none");
        $("#city-container .ui-select:eq(3)").p
    });


    var gc = new BMap.Geocoder(); //地址解析类
    function showInfo(e) {
        //alert(e.point.lng + ", " + e.point.lat);

        //alert(allOverlay.length-1)
        deletePoint();
        var point = new BMap.Point(e.point.lng, e.point.lat)
        var marker = new BMap.Marker(point); // 创建点
        map.addOverlay(marker);    //增加点
        var gc = new BMap.Geocoder();
        gc.getLocation(point, function (rs) {
            var addComp = rs.addressComponents;
            var mapAddr = addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber;
            var lngLat = e.point.lng + "," + e.point.lat;

            $("#lngLat").val(lngLat);//经度与纬度
           
            $(".showAdd").html(mapAddr)//地点

        });


    }
    function deletePoint() {
        var allOverlay = map.getOverlays();

        map.clearOverlays();
    }

    map.addEventListener("click", showInfo); 
    //百度地图自动填充关键词
    function G(id) {
        return document.getElementById(id);
    }

    var ac = new BMap.Autocomplete(    //建立一个自动完成的对象
            {"input" : "suggestId"
                ,"location" : map
            });

    ac.addEventListener("onhighlight", function(e) {  //鼠标放在下拉列表上的事件
        var str = "";
        var _value = e.fromitem.value;
        var value = "";
        if (e.fromitem.index > -1) {
            value = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
        }
        str = "FromItem<br />index = " + e.fromitem.index + "<br />value = " + value;

        value = "";
        if (e.toitem.index > -1) {
            _value = e.toitem.value;
            value = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
        }
        str += "<br />ToItem<br />index = " + e.toitem.index + "<br />value = " + value;
        G("searchResultPanel").innerHTML = str;
    });

    var myValue;
    ac.addEventListener("onconfirm", function(e) {    //鼠标点击下拉列表后的事件
        var _value = e.item.value;
        myValue = _value.province +  _value.city +  _value.district +  _value.street +  _value.business;
        G("searchResultPanel").innerHTML ="onconfirm<br />index = " + e.item.index + "<br />myValue = " + myValue;

       // setPlace();
    });

    function setPlace(){
        map.clearOverlays();    //清除地图上所有覆盖物
        function myFun(){
            var pp = local.getResults().getPoi(0).point;    //获取第一个智能搜索的结果
            map.centerAndZoom(pp, 18);
            map.addOverlay(new BMap.Marker(pp));    //添加标注

        }
        var local = new BMap.LocalSearch(map, { //智能搜索
            onSearchComplete: myFun
        });
        local.search(myValue);
    }