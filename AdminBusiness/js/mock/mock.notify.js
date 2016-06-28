Mock.mockjax(jQuery);
Mock.mock(/notify.json/, function(){
    return {
        arrayData : [
            {
                "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                "orderStatusObj": {
                    "status": "Payed",
                    "time": "20151116120000",
                    "lastStatus": "Created",
                    "title": "订单已生成",
                    "context": "等待用户支付订金"
                }
            },
            {
                "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                "orderStatusObj": {
                    "status": "Payed",
                    "time": "20151116120000",
                    "lastStatus": "Created",
                    "title": "订单已生成",
                    "context": "等待用户支付订金"
                }
            },
            {
                "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                "orderStatusObj": {
                    "status": "Payed",
                    "time": "20151116120000",
                    "lastStatus": "Created",
                    "title": "订单已生成",
                    "context": "等待用户支付订金"
                }
            }
        ]
    }
});
(function(){
    var polling = Dianzhu.notify.polling;
    var reqData = {};
    var globalApiUrl = "/notify.json";

    polling({
        url : globalApiUrl,
        reqData : reqData,
        callback : function(data){
            for ( var i = 0 ; i < data.arrayData.length; i++ ){
                var notify = $.notify('...',{
                    delay: 3000
                });
                var statusObj = data.arrayData[i];
                var id = statusObj.orderID;
                var statusTitle = statusObj.orderStatusObj.title;
                notify.update({'type': 'success', 'message': '订单号为' + '&nbsp;<strong>' + id + '&nbsp;</strong>&nbsp;' + '的订单状态变更为' +  '&nbsp;<strong>' + statusTitle + '&nbsp;</strong>' });
            }
        },
        delay : 5000
    });

})()