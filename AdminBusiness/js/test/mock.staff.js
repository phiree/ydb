Mock.mockjax(jQuery);
Mock.mock(/order.json/, function(){

    /* 本地测试数据 */
    return Mock.mock({
        "protocol_CODE": "ORM001006",
        "state_CODE": "009000",
        "RespData": {
            "arrayData": [
                {
                    "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                    "title": "ABC",
                    "status": "Payed",
                    "startTime": "201511161200",
                    "endTime": "201511171200",
                    "exDoc": "自带工具，线下结算",
                    "money": "1",
                    "address": "北京故宫",
                    "km": "600",
                    "svcObj": {
                        "svcID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "name": "棒棒娃",
                        "type": "设计>平面设计",
                        "startTime": "201511161200",
                        "endTime": "201511171200"
                    },
                    "userObj": {
                        "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "alias": "棒棒娃",
                        "imgUrl": "http://i-guess.cn/ihelp/userimg/issumao.png"
                    },
                    "storeObj": {
                        "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "alias": "望海国际",
                        "imgUrl": "http://i-guess.cn/ihelp/userimg/issumao_MD.png"
                    }
                },
                {
                    "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FE",
                    "title": "ABC",
                    "status": "Payed",
                    "startTime": "201511161200",
                    "endTime": "201511171200",
                    "exDoc": "自带工具，线下结算",
                    "money": "1",
                    "address": "北京故宫",
                    "km": "600",
                    "svcObj": {
                        "svcID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "name": "棒棒娃",
                        "type": "设计>平面设计",
                        "startTime": "201511161200",
                        "endTime": "201511171200"
                    },
                    "userObj": {
                        "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "alias": "棒棒娃",
                        "imgUrl": "http://i-guess.cn/ihelp/userimg/issumao.png"
                    },
                    "storeObj": {
                        "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "alias": "望海国际",
                        "imgUrl": "http://i-guess.cn/ihelp/userimg/issumao_MD.png"
                    }
                },
                {
                    "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FD",
                    "title": "ABC",
                    "status": "Payed",
                    "startTime": "201511161200",
                    "endTime": "201511171200",
                    "exDoc": "自带工具，线下结算",
                    "money": "1",
                    "address": "北京故宫",
                    "km": "600",
                    "svcObj": {
                        "svcID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "name": "棒棒娃",
                        "type": "设计>平面设计",
                        "startTime": "201511161200",
                        "endTime": "201511171200"
                    },
                    "userObj": {
                        "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "alias": "棒棒娃",
                        "imgUrl": "http://i-guess.cn/ihelp/userimg/issumao.png"
                    },
                    "storeObj": {
                        "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                        "alias": "望海国际",
                        "imgUrl": "http://i-guess.cn/ihelp/userimg/issumao_MD.png"
                    }
                }
            ]
        },
        "stamp_TIMES": "1490192929335",
        "serial_NUMBER": "00147001015869149751"
    })
});
Mock.mock(/assign.json/, function(){
    return Mock.mock({
        "protocol_CODE": "ASN002004",
        "state_CODE": "009000",
        "RespData": {
            "arrayData": [
                {
                    "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                    "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FE",
                    "mark": "Y"
                },
                {
                    "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                    "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FD",
                    "mark": "Y"
                },
                {
                    "userID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                    "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FC",
                    "mark": "N"
                }
            ]
        },
        "stamp_TIMES": "1490192929335",
        "serial_NUMBER": "00147001015869149751"
    })
})