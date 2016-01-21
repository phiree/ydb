// Mock.mock(rurl, template)
//Mock.mock(/\.json/, 'get', {
//
//    'dayMaxOrder|1-20' : 100,
//    'dayDoneOrder|1-20' : 2,
//    'timeBuckets|1-2' : [{
//        'timeStart' : "10:00",
//        'timeEnd' : "12:00",
//        'maxNum|1-10' : 5,
//        'doneNme|1-10' : 2,
//        'timeEnable' : true,
//        'orders|1-2' : [{
//            'enable' : true,
//            'href' : '#'
//        }]
//    }]
//});
//
//Mock.mock(/\.json/, 'put', function(options) {
//    console.log(options);
//});
//Mock.mock(/\.json/, 'post', function(options) {
//    console.log(options);
//});


//$.ajax({
//    url: 'hello.json',
//    dataType: 'json'
//}).done(function(data, status, jqXHR){
//    $('<pre>').text(JSON.stringify(data, null, 4))
//        .appendTo('body')
//});

///*! src/mockjax.js */
//function find(options) {
//    for (var sUrlType in Mock._mocked) {
//        var item = Mock._mocked[sUrlType];
//        if ((!item.rurl || match(item.rurl, options.url)) && (!item.rtype || match(item.rtype, options.type.toLowerCase()))) {
//            return item;
//        }
//    }
//    function match(expected, actual) {
//        if (Util.type(expected) === "string") {
//            return expected === actual;
//        }
//        if (Util.type(expected) === "regexp") {
//            return expected.test(actual);
//        }
//    }
//}
//function convert(item, options) {
//    return Util.isFunction(item.template) ? item.template(options) : Mock.mock(item.template);
//}
//Mock.mockjax = function mockjax(jQuery) {
//    function mockxhr() {
//        return {
//            readyState: 4,
//            status: 200,
//            statusText: "",
//            open: jQuery.noop,
//            send: function() {
//                if (this.onload) this.onload();
//            },
//            setRequestHeader: jQuery.noop,
//            getAllResponseHeaders: jQuery.noop,
//            getResponseHeader: jQuery.noop,
//            statusCode: jQuery.noop,
//            abort: jQuery.noop
//        };
//    }
//    function prefilter(options, originalOptions, jqXHR) {
//        var item = find(options);
//        if (item) {
//            var originalFilter = options.dataFilter;
//            options.dataFilter = options.converters["text json"] = options.converters["text jsonp"] = options.converters["text script"] = options.converters["script json"] = function() {
//                var resp = convert(item, options);
//                if ( originalFilter && typeof originalFilter == 'function') {
//                    return originalFilter( resp );
//                } else {
//                    return resp;
//                }
//            };
//            options.xhr = mockxhr;
//            if (originalOptions.dataType !== "script") return "json";
//        }
//    }
//    jQuery.ajaxPrefilter("json jsonp script", prefilter);
//    return Mock;
//};

Mock.mockjax(jQuery);

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
                    'date' : respDate,
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
        //console.log(options);
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
                        'arrayOrders|1-10' : [
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
        return timeBucket
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

