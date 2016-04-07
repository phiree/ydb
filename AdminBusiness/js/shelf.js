(function (){
    var templateOptions = {
        interpolate: /\{%=(.+?)%\}/g,
        escape:      /\{%-(.+?)%\}/g,
        evaluate:    /\{%(.+?)%\}/g
    };

    var globalReqUrl = "shm001007.json";


    //var workDay = Backbone.model.extend({
    //
    //});

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

    function fix(num, length) {
        return ('' + num).length < length ? ((new Array(length + 1)).join('0') + num).slice(-length) : '' + num;
    }

    function dateFix(option){
        var date = new Date(),
            dateStr = "",
            options = option ?  option : {};
        if (options.date && typeof options.date === "string") {
            dateStr = options.date
        } else {
            dateStr = ("" + date.getUTCFullYear() + fix((date.getUTCMonth() + 1), 2) + fix(date.getUTCDate(), 2));
        }

        return dateStr;
    }

    $.extend(SnapShot.prototype, {
        load : function(option){
            var prams = option ? option : {};
            var _this = this;
            var reqDate = $.extend({}, _this.options.queryData, prams.queryData);

            this.fetch({
                data : reqDate,
                success : function(resp){
                    if (resp.respCorrect){
                        _this.maxOrderDic = resp.respData.snapshotDic.maxOrderDic;
                        _this.workTimeDic = resp.respData.snapshotDic.workTimeDic;
                        _this.orderObjectDic = resp.respData.snapshotDic.orderObjectDic;
                    }
                    prams.callback && prams.callback(_this, resp);
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
        template : _.template($("#app_template").html(), templateOptions),
        initialize : function(){
            var _this = this;
            this.render();

            this.hisMaxOrderTmp = _.template($("#hisMaxOrder").html(), templateOptions);
            this.hisWorkTimeTmp = _.template($("#hisWorkTimeTmp").html(), templateOptions);
            this.hisOrderObjTmp = _.template($("#hisOrderObj").html(), templateOptions);

            snapShot.load({
                callback : function(snapShot, resp){
                    _this.flitSnapShot(snapShot)
                }
            });
        },
        render : function(){
            this.$el.html(this.template());
            $("#goodShelf").append($(this.el));
            return this;
        },
        flitSnapShot : function(snapShot) {
            var _this = this;
            var today = dateFix();

            today = "20160228";

            console.log(snapShot.maxOrderDic[today]);

            for(var i = 0 ; i < snapShot.maxOrderDic[today].length ; i++ ) {
                this.buildMaxOrder(snapShot.maxOrderDic[today][i]);
            }

            for(var i = 0 ; i < snapShot.workTimeDic[today].length ; i++ ) {
                this.buildWorkTime(snapShot.workTimeDic[today][i]);
            }

            for(var i = 0 ; i < snapShot.orderObjectDic[today].length ; i++ ) {
                this.buildOrderObject(snapShot.orderObjectDic[today][i]);
            }

            //this.initMaxOrder();
            //this.initworkTime();
            //this.initorderObject();·
        },

        buildMaxOrder : function (maxOrder) {
            this.hisMaxOrderTmp(maxOrder);
            this.$el.find(".day-container").append(this.hisMaxOrderTmp(maxOrder));
        },
        buildWorkTime : function(workTime){
            workTime.startTimeFix = workTime.startTime.replace(":", "");
            workTime.endTimeFix = workTime.endTime.replace(":", "");
            this.hisWorkTimeTmp(workTime);
            this.$el.find(".time-buckets").append(this.hisWorkTimeTmp(workTime));
        },
        buildOrderObject: function(orderObject){
            var _this = this;
            var startTime = orderObject.startTime.slice(-4);
            var workTimes = this.$el.find(".wt-wrap");

            workTimes.each(function(){
                var start = $(this).attr("data-startTime");
                var end = $(this).attr("data-endTime");

                if ( start < startTime && startTime < end ){
                    $(this).append(_this.hisOrderObjTmp(orderObject));
                }
            });
        }
    });

    var app = new AppView();
})();