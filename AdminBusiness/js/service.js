function initializeService(){
    var map = new BMap.Map("allmap");
    //var myCityListObject = new BMapLib.CityList({container : "city-container" , map : map });
    var geoc = new BMap.Geocoder();
    /**
     * 初始化商圈地图
     */
    //var map = new BMap.Map("businessMap");
    //var cityListObject = new BMapLib.CityList({ container: "businessCity" , map : map });
    map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
    map.disableDoubleClickZoom();


    var top_left_control = new BMap.ScaleControl({anchor: BMAP_ANCHOR_TOP_LEFT});// 左上角，添加比例尺
    var top_left_navigation = new BMap.NavigationControl();  //左上角，添加默认缩放平移控件
    //var top_right_navigation = new BMap.NavigationControl({anchor: BMAP_ANCHOR_TOP_RIGHT, type: BMAP_NAVIGATION_CONTROL_SMALL}); //右上角，仅包含平移和缩放按钮
    /*缩放控件type有四种类型:
     BMAP_NAVIGATION_CONTROL_SMALL：仅包含平移和缩放按钮；BMAP_NAVIGATION_CONTROL_PAN:仅包含平移按钮；BMAP_NAVIGATION_CONTROL_ZOOM：仅包含缩放按钮*/

    //添加控件和比例尺
    function add_control(){
        map.addControl(top_left_control);
        map.addControl(top_left_navigation);
    }

    //add_control();


    function delete_control(){
        map.removeControl(top_left_control);
        map.removeControl(top_left_navigation);
    }

    /**
     * 初始化商圈缩略地图
     */
    //var submap = new BMap.Map("businessMapSub");
    //submap.centerAndZoom(new BMap.Point(116.404, 39.915), 10);
    //submap.disableDoubleClickZoom();
    //submap.disableScrollWheelZoom();
    //submap.disableDragging();


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


    //function writeBusiness(){
    //    /**
    //     * JSON格式商圈信息
    //     * @JSON
    //     */
    //    var businessJson = {};
    //    var provinceName = "",
    //        cityName = "",
    //        boroughName = "",
    //        businessName = "";
    //
    //    /**
    //     * 点击城市时地图定位到指定城市
    //     * @param e
    //     */
    //    function writeBusinessJson (e) {
    //        console.log(e.area_type);
    //        console.log(e.area_name);
    //        console.log(e.area_code);
    //
    //        switch (e.area_type) {
    //            //重新选择省时，清空其他内容。
    //            case 1 :
    //                provinceName = e.area_name;
    //                cityName = "";
    //                boroughName = "";
    //                businessName = "";
    //                break;
    //            case 2 :
    //                if (e.area_code == 132 || e.area_code == 332 || e.area_code == 289 || e.area_code == 131) {
    //                    provinceName = "";
    //                    cityName = e.area_name;
    //                } else {
    //                    cityName = e.area_name;
    //                }
    //                break;
    //            case 3 :
    //                boroughName = e.area_name;
    //                break;
    //            case 10 :
    //                businessName = e.area_name;
    //                break;
    //            default :
    //                alert("地图选择有误");
    //        }
    //
    //        if (e.geo) {
    //            submap.panTo(e.geo);
    //        } else {
    //            return
    //        }
    //
    //        businessJson = {
    //            "provinceName": provinceName,
    //            "cityName": cityName,
    //            "boroughName": boroughName,
    //            "businessName": businessName,
    //            "businessLocLng": e.geo.lng,
    //            "businessLocLat": e.geo.lat
    //        };
    //    }
    //
    //
    //    cityListObject.addEventListener("cityclick",
    //        function(e){
    //            writeBusinessJson (e);
    //        }
    //    );
    //
    //    $('#confBusiness').click(function () {
    //        var businiessText = $("#businessText");
    //
    //        if ( !$.isEmptyObject(businessJson) ){
    //            $('#hiBusinessAreaCode').attr("value",JSON.stringify(businessJson));
    //            var businessNode = "<span>" + businessJson.provinceName + "</span><span>" + businessJson.cityName + "</span><span>" + businessJson.boroughName + "</span><span>" + businessJson.businessName + "</span>"
    //            businiessText.html(businessNode);
    //        } else {
    //            return;
    //        }
    //    });
    //}
    //
    //writeBusiness();

    ///**
    // * 载入时读取地图信息
    // */
    //function mapInit() {
    //
    //    if ( $('#hiBusinessAreaCode').attr("value") ){
    //        var readBusinessJson = jQuery.parseJSON($('#hiBusinessAreaCode').attr("value"));
    //        var subMapPoint = new BMap.Point();
    //        subMapPoint.lng = readBusinessJson.businessLocLng;
    //        subMapPoint.lat = readBusinessJson.businessLocLat;
    //        submap.panTo(subMapPoint);
    //        map.panTo(subMapPoint);
    //
    //        var businessNode = "<span>" + readBusinessJson.provinceName + "</span><span>" + readBusinessJson.cityName + "</span><span>" + readBusinessJson.boroughName + "</span><span>" + readBusinessJson.businessName + "</span>"
    //        var businiessText = $('#businessText');
    //        businiessText.html(businessNode);
    //    } else {
    //        myCity.get(function(result){
    //            submap.panTo(result.center);
    //        });
    //        myCity.get(function(result){
    //            map.panTo(result.center);
    //        });
    //    }
    //};
    //
    //mapInit();



    /**
     * 服务点覆盖类 by lichang
     * @param {sPoint} 服务点中心
     * @param {sRadius} 服务点范围
     */
    function ServerPoint(sPoint, sRadius) {
        this.sPoint = sPoint;
        this.sRadius = sRadius;
        this.marker = new BMap.Marker(sPoint);
        this.circle = new BMap.Circle(sPoint, sRadius);
    }

    ServerPoint.prototype = {
        constructor: ServerPoint,
        setPoint: function (setPoint) {
            this.sPoint = setPoint;
            this.marker.setPosition(this.sPoint);
            this.circle.setCenter(this.sPoint);
        },
        getPoint: function () {
            return this.sPoint;
        },
        setRadius: function (setRadius) {
            this.sRadius = setRadius;
            this.circle.setRadius(this.sRadius);
        },
        getRadius: function () {
            return this.sRadius;
        },
        draw: function () {
            map.addOverlay(this.marker);
            map.addOverlay(this.circle);
        },
        remove: function () {
            map.removeOverlay(this.marker);
            map.removeOverlay(this.circle);
        }

    };


    (function(){
        /**
         *  服务点添加，删除，控制
         */
        var addSP = document.getElementById('addSP') ? document.getElementById('addSP') : null;
        var editSP = document.getElementById('editSP') ? document.getElementById('editSP') : null;
        var delSP = document.getElementById('delSP') ? document.getElementById('delSP') : null;
        var saveSP = document.getElementById('saveSP') ? document.getElementById('saveSP') : null;
        var SPContainer = document.getElementById('SPContainer') ? document.getElementById('SPContainer') : null;
        var arrServerPoint = new Array();//服务点数组
        var arrSPBtn = new  Array();//服务点标签数组
        var currentCont; //当前计数
        var currentSP;  //当前服务点
        var currentSPBtn; //当前服务点按钮

        var rResult = document.getElementById('r-result');
        var radiusContainer = document.getElementById('radius-container');

        newServerPoint(); //默认新建一个服务点

        /* 添加新的服务点 */
        if ( addSP != null ){
            if (typeof addSP.addEventListener != "undefined") {
                addSP.addEventListener("click", newServerPoint,false);
            } else {
                addSP.attachEvent("onclick", newServerPoint);
            }
        }

        function newServerPoint(){
            if ( arrServerPoint.length < 1 ? true : arrServerPoint[arrServerPoint.length - 1].getPoint() != undefined ) {
                var sp = new ServerPoint();
                arrServerPoint.push(sp);


                currentSP = arrServerPoint[arrServerPoint.length - 1];
                //SPBindAction();

                var a = document.createElement('a');
                a.innerHTML = arrServerPoint.length;
                a.id = 'sp-' + arrServerPoint.length;
                a.className = 'sp-style';
                a.title = arrServerPoint.length;

                if ( SPContainer ){
                    SPContainer.appendChild(a);
                    arrSPBtn = SPContainer.getElementsByTagName('a');
                }



                currentCont = arrServerPoint.length - 1;
                selectSP(currentCont);


                if (arrSPBtn.length > 0 ){
                    if (typeof arrSPBtn[arrSPBtn.length - 1].addEventListener != "undefined") {
                        arrSPBtn[arrSPBtn.length - 1].addEventListener("click",function(){
                            currentCont = (this.title - 1); //获取a标签中的数值;
                            selectSP(currentCont);

                        },false);
                    } else {
                        function handler(){ //解决IE8下attachEvent中this指向window的问题；
                            var e = e || window.event;
                            var _this = e.srcElement || e.target;
                            //console.log(_this.title);
                            currentCont = (_this.title - 1); //获取a标签中的数值;
                            selectSP(currentCont);
                        }
                        arrSPBtn[arrSPBtn.length - 1].attachEvent("onclick",handler)
                    }
                }

                if ( document.getElementById('addError') != null ) {
                    //console.log(document.getElementById('addError'))
                    document.getElementById('addError').style.display = 'none';
                }

            } else {
                //console.log('当前服务点未设置，无法添加新服务点');
                if ( document.getElementById('addError') != null ){
                    document.getElementById('addError').style.display = 'inline';
                }

                return false;
            }
        }

        /* 设置当前服务点 */
        function selectSP(cont){
            currentSPBtn = arrSPBtn[cont];
            currentSP = arrServerPoint[cont];
            currentEff(cont);
            //console.log(cont);
            //console.log(currentSPBtn);
            //console.log(currentSP);
        }

        /* 选中效果函数 */
        function currentEff(cont){
            for (var i = 0 ; i < arrSPBtn.length ; i++ ) { //设置a标签的class
                if ( i != (cont) ){
                    arrSPBtn[i].className = 'sp-style';
                } else {
                    currentSPBtn.className += ' sp-style-on';
                }
            }

            for (var i = 0 ; i < arrSPBtn.length ; i++ ) { //设置服务点的样式
                if ( i != (cont) ){ //隐藏其他服务点
                    arrServerPoint[i].circle.hide();
                    arrServerPoint[i].marker.hide();
                } else { //显示当前服务点
                    arrServerPoint[i].circle.show();
                    arrServerPoint[i].marker.show();
                    map.panTo(arrServerPoint[i].getPoint());
                }
            }
        }

        /* 删除服务点 */
        if ( delSP != null ){
            if (typeof delSP.addEventListener != "undefined") {
                delSP.addEventListener("click", delServerPoint,false);
            } else {
                delSP.attachEvent("onclick", delServerPoint);
            }
        }

        function delServerPoint(){
//        if ( arrServerPoint.length > 1 ? true : arrServerPoint[arrServerPoint.length - 1].getPoint() != undefined ) {
            if ( arrServerPoint.length > 1 ) {
                currentSP.remove();
                arrServerPoint.splice(currentCont,1);

                SPContainer.removeChild(SPContainer.childNodes[currentCont--]);
                if ( currentCont < 0 ){
                    currentCont = 0;
                    selectSP(currentCont);
                } else {
                    selectSP(currentCont);
                }

                arrSPBtn = SPContainer.getElementsByTagName('a');
                //console.log(arrSPBtn.length);
                for (var i = 0 ; i < arrSPBtn.length ; i++ ) { //设置服务点的样式
                    arrSPBtn[i].innerHTML = (i + 1);
                    arrSPBtn[i].id = 'sp-' + (i + 1);
                    arrSPBtn[i].title = (i + 1);
                }
            } else {
                console.log('至少设置一个服务点');
                document.getElementById('delError').style.display = 'inline';
                return false;
            }
        }

        /* 用下拉表单设置服务半径 */
        var serRadius = document.getElementById('ser-radius');
        var radius = serRadius.value;
        serRadius.onchange = function(){
            radius = serRadius.value;
            currentSP.setRadius(radius);
            autoSaveSerPoint();
        }

        /* 放置服务点 */
        map.addEventListener("click",setServerPoint);
        function setServerPoint(e){
            map.clearOverlays();    //清除地图上所有覆盖物

            var newPoint = new BMap.Point(e.point.lng, e.point.lat);
            currentSP.setPoint(newPoint);
            currentSP.setRadius(radius);
            currentSP.draw();
            autoSaveSerPoint();
            if ( document.getElementById('delError') != null ) {
                document.getElementById('delError').style.display = 'none';
            }
            if ( document.getElementById('addError') != null ) {
                document.getElementById('addError').style.display = 'none';
            }
            //document.getElementById('delError').style.display = 'none';
            //document.getElementById('addError').style.display = 'none';
        }

        function SPBindAction(){
            currentSP.circle.addEventListener("mousedown",mouseDownAction);
            currentSP.circle.addEventListener("click",evePrevent);
            currentSP.circle.addEventListener("dblclick",evePrevent);
        }

        function SPUnbindAction(){
            currentSP.circle.removeEventListener("mousedown",mouseDownAction);
            currentSP.circle.removeEventListener("click",evePrevent);
            currentSP.circle.removeEventListener("dblclick",evePrevent);
        }

        function mouseDownAction(e) {
            baidu.preventDefault(e);
            baidu.stopBubble(e);
            startDragCircle(e);
        }

        function evePrevent(e){
            baidu.preventDefault(e);
            baidu.stopBubble(e);
        }


        /* 拖拽服务点函数半径 */
        function startDragCircle(e){ //开始拖拽
            map.disableDragging();
            currentSP.circle.setStrokeColor('red');
            map.addEventListener("mousemove",dragCircle);
            baidu.on(document, 'mouseup', endDragCircle);
        }

        /* 刷新服务半径 */
        function dragCircle(e) {
            var dragdistance = map.getDistance(currentSP.getPoint(), e.point); //服务点拖拽半径
            currentSP.setRadius(dragdistance);
        }

        /*结束拖拽*/
        function endDragCircle(e){
            baidu.preventDefault(e);
            baidu.stopBubble(e);
            map.enableDragging();
            currentSP.circle.setStrokeColor('green');
            map.removeEventListener("mousemove",dragCircle);
            baidu.un(document, 'mouseup', endDragCircle);
            autoSaveSerPoint()
        }



        /* 保存服务点 */
        var hiBussAreaCode = $('#hiBusinessAreaCode');
        if ( saveSP ){
            if (typeof saveSP.addEventListener != "undefined") {
                saveSP.addEventListener("click", savaServerPoint,false);
            } else {
                saveSP.attachEvent("onclick", savaServerPoint);
            }
        }

//    saveSP.addEventListener("click",savaServerPoint);
        function savaServerPoint(){
            //var arrTransDate = new Array();


            if ( currentSP.getPoint() != undefined ){
                //console.log(currentSP.getPoint());
                //for ( var i = 0 ; i < arrServerPoint.length ; i++ ) {
                //    arrTransDate[i] = (arrServerPoint[i].getPoint().lng + ',' + arrServerPoint[i].getPoint().lat + ',' + arrServerPoint[i].getRadius());
                //    });
                //}
                //var strTransDate = arrTransDate.join('||');

                var jsonTranDate = {
                    serPointCirle: {
                        lng:null,
                        lat:null,
                        radius:null,
                    },
                    serPointComp: {},
                    serPointAddress: null
                };

                var pt = new BMap.Point(currentSP.getPoint().lng , currentSP.getPoint().lat);

                geoc.getLocation(pt,function(result){
                    jsonTranDate.serPointCirle.lng = currentSP.getPoint().lng;
                    jsonTranDate.serPointCirle.lat = currentSP.getPoint().lat;
                    jsonTranDate.serPointCirle.radius = currentSP.getRadius();
                    jsonTranDate.serPointComp = result.addressComponents;
                    jsonTranDate.serPointAddress = result.address;
                    console.log(JSON.stringify(jsonTranDate));
                    hiBussAreaCode.attr("value",JSON.stringify(jsonTranDate)).focus().blur();
                    addressPrint(jsonTranDate);

                });
                map.panTo(pt);
                resetServerMap();
                $("#saveMsg").addClass("dis-n");
                $(editSP).removeClass("dis-n");
                $(saveSP).addClass("dis-n");
            } else {
                resetServerMap();
                $("#saveMsg").removeClass("dis-n");
                $(editSP).removeClass("dis-n");
                $(saveSP).addClass("dis-n");
            }


        }


        /*自动保存服务点*/
        function autoSaveSerPoint (){
            var jsonTranDate = {
                serPointCirle: {
                    lng:null,
                    lat:null,
                    radius:null,
                },
                serPointComp: {},
                serPointAddress: null
            };

            var pt = new BMap.Point(currentSP.getPoint().lng , currentSP.getPoint().lat);

            geoc.getLocation(pt,function(result){
                jsonTranDate.serPointCirle.lng = currentSP.getPoint().lng;
                jsonTranDate.serPointCirle.lat = currentSP.getPoint().lat;
                jsonTranDate.serPointCirle.radius = currentSP.getRadius();
                jsonTranDate.serPointComp = result.addressComponents;
                jsonTranDate.serPointAddress = result.address;
                console.log(JSON.stringify(jsonTranDate));

                hiBussAreaCode.attr("value",JSON.stringify(jsonTranDate));
                addressPrint(jsonTranDate);

            });


        }


        function resetServerMap(){
            map.disableDragging();
            map.removeEventListener("click",setServerPoint);
            map.disableScrollWheelZoom();
            delete_control();
            SPUnbindAction();
            $(radiusContainer).addClass("dis-n");
            $(rResult).addClass("dis-n");
        }

        function initServerMap(){
            map.disableDragging();
            map.removeEventListener("click",setServerPoint);
            map.disableScrollWheelZoom();
        }


        if ( editSP != null ){
            if (typeof editSP.addEventListener != "undefined") {
                editSP.addEventListener("click", editServerMap,false);
            } else {
                editSP.attachEvent("onclick", editServerMap);
            }
        }

        function editServerMap(){
            map.enableDragging();
            map.addEventListener("click",setServerPoint);
            map.enableScrollWheelZoom();
            add_control();
            SPBindAction();
            $(radiusContainer).removeClass("dis-n");
            $(rResult).removeClass("dis-n");
            $("#saveMsg").addClass("dis-n");
            $(saveSP).removeClass("dis-n");
            $(editSP).addClass("dis-n");
        }

        /*初始读取服务点*/
        function readServerPoint(){
            initServerMap();
            //currentSP.circle.removeEventListener("mousedown",mouseDownAction);
            //currentSP.circle.removeEventListener("click",evePrevent);
            //currentSP.circle.removeEventListener("dblclick",evePrevent);

            //var readSerPoint = new ServerPoint();
            //var readPoint = new BMap.Point(localAddrJson.serPointCirle.lng, localAddrJson.serPointCirle.lat);
            //var readRadius = localAddrJson.serPointCirle.radius;
            //
            //currentSP = readSerPoint;
            //currentSP.setPoint(readPoint);
            //currentSP.setRadius(readRadius);
            //console.log(readSerPoint);
            //currentSP.draw();


            if ( $('#hiBusinessAreaCode').attr("value") ){
                var readBusinessJson = jQuery.parseJSON($('#hiBusinessAreaCode').attr("value"));
                //var subMapPoint = new BMap.Point();

                var readSerPoint = new ServerPoint();
                var readPoint = new BMap.Point(readBusinessJson.serPointCirle.lng, readBusinessJson.serPointCirle.lat);
                var readRadius = readBusinessJson.serPointCirle.radius;

                currentSP = readSerPoint;
                currentSP.setPoint(readPoint);
                currentSP.setRadius(readRadius);
                console.log(readSerPoint);
                currentSP.draw();

                map.centerAndZoom(readPoint,15);
                addressPrint(readBusinessJson);
            } else {
                $("#saveMsg").removeClass("dis-n");

                myCity.get(function(result){
                    map.panTo(result.center);
                });

            }
        }

        function addressPrint(json){
            $("#saveAddress").text(json.serPointAddress);
            var radius = new Number(json.serPointCirle.radius);
            $("#saveRadius").text( "半径" + radius.toFixed(2) + "m");
        }

        readServerPoint();





    var suggestGeo = new BMap.Geocoder();

        if (typeof G("suggestId").addEventListener != "undefined") {
            G("suggestId").addEventListener("input", serPointGeo ,false);
        } else {
            G("suggestId").attachEvent("oninput", serPointGeo );
        }

    function serPointGeo(){
        var e = e || window.event;
        var _this = e.srcElement || e.target;
        var strSuggest = _this.value;

        suggestGeo.getPoint(strSuggest, function(point){

            if(point){
                map.centerAndZoom(point,15)
                currentSP.setPoint(point);
                currentSP.setRadius(radius);
                currentSP.draw();
                autoSaveSerPoint();
            } else {
                return
            }
        })
    }


    /* 百度地图自动填充关键词 */
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

        setPlace();
    });

    function setPlace(){
        map.clearOverlays();    //清除地图上所有覆盖物
        function myFun(){
            var pp = local.getResults().getPoi(0).point;    //获取第一个智能搜索的结果
            map.centerAndZoom(pp, 15);
            //map.addOverlay(new BMap.Marker(pp));    //添加标注
            currentSP.setPoint(pp);
            currentSP.setRadius(radius);
            currentSP.draw();
            autoSaveSerPoint();
        }
        var local = new BMap.LocalSearch(map, { //智能搜索
            onSearchComplete: myFun
        });
        local.search(myValue);
    }

    })();
};

  


