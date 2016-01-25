(function(){
    Mock.mockjax(jQuery);

    var today = new Date();
    // 模拟递增日期，初始化减7，因为Mock.js和CustomAPI的dataFilter冲突会导致函数在执行一次
    var todayDate = today.getDate() - 7;

    function resqDay(){
        //debugger;
        var Date = today.getFullYear() + "-" + (today.getMonth() + 1) + '-' + (todayDate++);
        return Date;
    }

    Mock.mock(/days.json/,function(options){
        var respDate = Random.now('yyyy-M-d');
        debugger;
        //console.log(options);
        var day = Mock.mock({
            'protocol_CODE' : 'slf001007',
            'state_CODE': "009000",
            'RespData' : {
                'arrayData|7': [
                    {
                        'date' : resqDay,
                        'dayMaxOrder|1-10' : 10,
                        'dayDoneOrder|1-10' : 2,
                        'dayEnable|1' : true,
                        //'timeBuckets' : null
                    }
                ]
            },
            'stamp_TIMES' :  "1490192929222",
            'serial_NUMBER' :  "00147001015869149756"
        });

        return day;
    });

    Mock.mock(/timeBuckets.json/, function(options){
            var timeBucket = Mock.mock({
                'protocol_CODE' : 'slf002006',
                'state_CODE': "009000",
                'RespData' : {
                    'arrayData|2-4': [
                        {
                            'timeStart' : Random.time('HH:mm'),
                            'timeEnd' : Random.time('HH:mm'),
                            'maxNum|1-5' : 5,
                            'doneNum|1-10' : 2,
                            'timeEnable|1' : true,
                            'arrayOrders|10-20' : [
                                {
                                    'ordered|1' : true,
                                    'orderId' : '#'
                                }
                            ]
                        }
                    ]
                },
                'stamp_TIMES' :  "1490192929222",
                'serial_NUMBER' :  "00147001015869149756"
            });
            return timeBucket;
        }
    );
//
//Mock.mock(/orders.json/, function(options){
//    console.log(options);
//        var order = Mock.mock({
//            'data|4-10' : [
//                {
//                    'ordered|1' : true,
//                    'href' : '#'
//                }
//            ]
//        });
//    return  order.data
//    }
//);
})();