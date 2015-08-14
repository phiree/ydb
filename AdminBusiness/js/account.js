function initialize(){
    var map = new BMap.Map("addressMap");
    var cityListObject = new BMapLib.CityList({ container: "addressCity", map : map});
    var geoc = new BMap.Geocoder();
//var myGeo = new BMap.Geocoder();

    var id_prefix = 'ContentPlaceHolder1_ContentPlaceHolder1_';
    var addressId = id_prefix + "tbxAddress";

    if (typeof G(addressId).addEventListener != "undefined") {
        G(addressId).addEventListener("change", addressGoe ,false);
    } else {
        G(addressId).attachEvent("onchange", addressGoe );
    }

    function G(id) {
        return document.getElementById(id);
    }

    function addressGoe(){
        var e = e || window.event;
        var _this = e.srcElement || e.target;
        var strAddress = _this.value;

        geoc.getPoint(strAddress, function(point){
            console.log(point)
            if(point){
                geoc.getLocation(point,function(result){
                    console.log(result);

                    $('#hiAddrId').attr("value",JSON.stringify(result.addressComponents));
                    //var readAddrJson = jQuery.parseJSON($('#hiAddrId').attr("value"));

                })
            } else {
                console.log("无法解析当前地址");
                alert("请输入详细有效的地址");
            }
        })
    }
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
            addrNodeBox.addClass('myshop-addPrint d-inb');
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
};


