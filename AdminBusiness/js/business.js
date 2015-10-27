function initialize(){
    //var map = new BMap.Map("addressMap");
    //var cityListObject = new BMapLib.CityList({ container: "addressCity", map : map});
    var geoc = new BMap.Geocoder();
    var myCity = new BMap.LocalCity();

    var id_prefix = '';
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
            if(point){
                geoc.getLocation(point,function(result){
                    //console.log(result);

                    $('#hiAddrId').attr("value",JSON.stringify(result.addressComponents));

                });
            } else {
                myCity.get(function(result){
                    geoc.getLocation(result.center,function(result){
                        $('#hiAddrId').attr("value",JSON.stringify(result.addressComponents));
                    });
                });
                //confirm("请输入详细有效的地址");
            }
        });
    }

};


