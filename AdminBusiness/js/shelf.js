(function (){
    var templateOptions = {
        interpolate: /\{%=(.+?)%\}/g,
        escape:      /\{%-(.+?)%\}/g,
        evaluate:    /\{%(.+?)%\}/g
    };

    var globalReqUrl = "shm001007.json";

    var SnapShot = function(options){
        this.options = $.extend({}, SnapShot.DEFAULTS, options);
        this.maxOrderDic = null;
        this.workTimeDic = null;
        this.orderObjectDic = null;
    };

    SnapShot.DEFAULTS = {
        reqUrl : "shm001007.json",
        queryData : {
            startTime : dateFix({startHour : true}),
            endTime : dateFix({endHour : true}),
            type : "maxOrder|workTime|order"
        }
    };

    function dateFix(option){
        var date = new Date(),
            dateStr = "",
            options = option ?  option : {};
        if (options.date && typeof options.date === "string") {
            dateStr = options.date
        } else {
            dateStr = ("" + date.getUTCFullYear() + (date.getUTCMonth() + 1) + date.getUTCDate());
        }

        if(options.startHour) dateStr = dateStr + "0000";
        if(options.endHour) dateStr = dateStr + "2359";

        return dateStr;
    }

    $.extend(SnapShot.prototype, {
        load : function(queryData){
            var _this = this;
            var reqDate = $.extend({}, _this.options.queryData, queryData);

            this.fetch({
                data : reqDate,
                success : function(resp){
                    if (resp.respCorrect){
                        _this.maxOrderDic = resp.respData.snapshotDic.maxOrderDic;
                        _this.workTimeDic = resp.respData.snapshotDic.workTimeDic;
                        _this.orderObjectDic = resp.respData.snapshotDic.orderObjectDic;
                    }
                }
            });
        },
        fetch : function(option){
            var options = option || {};
            var success = options.success;
            options.success = function(resp){
                var response = Adapter.respUnpack(resp);
                if(success) success(response);
            };
            this.sync(options);
        },
        sync : function(options){
            var xhr = $.ajax({
                url : globalReqUrl,
                type : "POST",
                dataType : "json",
                //dataType : "text",
                data : options.data,
                success : options.success
            });
            return xhr;
        }
    });

    var snapShot = new SnapShot();



    var AppView = Backbone.View.extend({
        tagName : "div",
        className : "wt-show-wrap",
        template : _.template($("#workTimeShowTmp").html(), templateOptions),
        init : function(){
            snapShot.load();
        },
        build : function () {

        },
    });

    var app = new AppView();
})();