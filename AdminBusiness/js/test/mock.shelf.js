Mock.mockjax(jQuery);
Mock.mock(/shm001007.json/, function(){
    return {
        "protocol_CODE": "SHM001007",
        "state_CODE": "009000",
        "RespData": {
            "snapshotDic": {
                "maxOrderDic" : {
                    "20160418" : [{
                        "date": "20160418",
                        "maxOrder": "10",
                        "reOrder": "5"
                    }],
                },
                "workTimeDic": {
                    "20160418": [
                        {
                            "workTimeID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                            "tag": "默认工作时间",
                            "startTime": "08:00",
                            "endTime": "12:00",
                            "week": "2",
                            "open": "N",
                            "maxOrder": "8",
                            "svcID": "6F9619FF-8B86-D011-B42D-00C04FC964FF"
                        },
                        {
                            "workTimeID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                            "tag": "默认工作时间",
                            "startTime": "14:00",
                            "endTime": "19:00",
                            "week": "2",
                            "open": "N",
                            "maxOrder": "7",
                            "svcID": "6F9619FF-8B86-D011-B42D-00C04FC964FF"
                        },
                        {
                            "workTimeID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                            "tag": "默认工作时间",
                            "startTime": "20:00",
                            "endTime": "23:00",
                            "week": "1",
                            "open": "Y",
                            "maxOrder": "5",
                            "svcID": "6F9619FF-8B86-D011-B42D-00C04FC964FF"
                        },
                    ],
                },
                "orderObjectDic": {
                    "20160418": [
                        {
                            "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FE",
                            "title": "ABC",
                            "status": "Payed",
                            "startTime": "201511161000",
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
                            "orderID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                            "title": "ABC",
                            "status": "Payed",
                            "startTime": "201511161500",
                            "endTime": "201511171900",
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
                    ],
                }
            }
        },
        "stamp_TIMES": "1490192929335",
        "serial_NUMBER": "00147001015869149751"
    }
});
