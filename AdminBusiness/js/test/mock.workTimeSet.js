Mock.mockjax(jQuery);
Mock.mock(/test.001001.json/, function(){
    return {
        "protocol_CODE": "WTM001006",
        "state_CODE": "009000",
        "RespData": {
            "arrayData": [
                "6F9619FF-8B86-D011-B42D-00C04FC964FF"
            ]
        },
        "stamp_TIMES": "1490192929335",
        "serial_NUMBER": "00147001015869149751"
    }
});
Mock.mock(/test.001002.json/, function(){
    return {
        "protocol_CODE": "WTM001006",
        "state_CODE": "009000",
        "stamp_TIMES": "1490192929335",
        "serial_NUMBER": "00147001015869149751"
    }
});
Mock.mock(/test.001006.json/, function(){
    return {
        "protocol_CODE": "WTM001006",
        "state_CODE": "009000",
        "RespData": {
            "arrayData": [
                {
                    "workTimeID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                    "tag": "默认工作时间",
                    "startTime": "08:00",
                    "endTime": "12:00",
                    "week": "2",
                    "open": "N",
                    "maxOrder": "8",
                },
                {
                    "workTimeID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                    "tag": "默认工作时间",
                    "startTime": "14:00",
                    "endTime": "19:00",
                    "week": "2",
                    "open": "N",
                    "maxOrder": "7",
                },
                {
                    "workTimeID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                    "tag": "默认工作时间",
                    "startTime": "20:00",
                    "endTime": "23:00",
                    "week": "1",
                    "open": "Y",
                    "maxOrder": "5",
                },
            ]
        },
        "stamp_TIMES": "1490192929335",
        "serial_NUMBER": "00147001015869149751"
    }
});

Mock.mock(/test.001003.json/, function(){
    return {
        "protocol_CODE": "WTM001006",
        "state_CODE": "009000",
        "RespData": {
            "workTimeID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
            "tag": "Y",
            "startTime": "07:00",
            "endTime": "19:00",
            "open": "Y"
        },
        "stamp_TIMES": "1490192929335",
        "serial_NUMBER": "00147001015869149751"
    }
});
Mock.mock(/test.s001005.json/, function(){
    return {
        "protocol_CODE": "SVC001005",
        "state_CODE": "009000",
        "RespData": {
            "svcObj": {
                "svcID": "6F9619FF-8B86-D011-B42D-00C04FC964FF",
                "name": "特色洗衣",
                "type": "保洁,洗衣",
                "introduce": "超越常规洗衣，突破极限",
                "area": "海南省海口市",
                "startAt": "60",
                "unitPrice": "70",
                "deposit": "70",
                "appointmentTime": "60",
                "serviceTimes": "6F9619FF-8B86-D011-B42D-00C04FC964FF, 6F9619FF-8B86-D011-B42D-00C04FC964FF, 6F9619FF-8B86-D011-B42D-00C04FC964FF",
                "maxOrder": "90",
                "doorService": "Y",
                "serviceObject": "all",
                "payWay": "WeiPay",
                "tag": "洗衣,常规",
                "open": "Y",
                "maxOrderString" : "10,10,10,10,10,20,90"
            }
        },
        "stamp_TIMES": "1490192929335",
        "serial_NUMBER": "00147001015869149751"
    }
})