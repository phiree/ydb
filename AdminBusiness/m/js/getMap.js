
// 百度地图API功能
    var map = new BMap.Map("container");
    var myCityListObject = new BMapLib.CityList({container : "city-container" , map : map});
    //map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
  //  map.centerAndZoom("海口", 11);     // 初始化地图,设置中心点坐标和地图级别
    map.enableScrollWheelZoom();
    map.addEventListener("tilesloaded", function (e) {
        $(".showAdd").css("display", "none");
        
    });
	
