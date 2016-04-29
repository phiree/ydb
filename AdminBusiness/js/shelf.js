(function (){
    var templateOptions = {
        interpolate: /\{%=(.+?)%\}/g,
        escape:      /\{%-(.+?)%\}/g,
        evaluate:    /\{%(.+?)%\}/g
    };

    // 请求URl
    var globalReqUrl = "shm001007.json";

    // 商家ID
    var merchantID = document.getElementById("merchantID").value;

    // 引入自定义_Event 类
    var Event = window._Event;

    var testUrl = {
        WTM001001 : "test.001001.json",
        WTM001002 : "test.001002.json",
        WTM001003 : "test.001003.json",
        WTM001004 : "test.001004.json",
        WTM001005 : "test.001005.json",
        WTM001006 : "test.001006.json",
        SVC001003 : "test.s001003.json",
        SVC001005 : "test.s001005.json"
    };

    var dateTools = function(){
        var dateTools = {};
        dateTools.dateFormat = function(dateObj, options){
            var date = dateObj || new Date();
            var option = (typeof options === "string") ? options.toUpperCase() : "YYYYMMDD";

            switch (option){
                case "YYYYMMDD":
                    return ("" + date.getFullYear() + fix((date.getMonth() + 1), 2) + fix(date.getDate(), 2));
                    break;
                case "YYYYMMDDHHMM":
                    return ("" + date.getFullYear() + fix((date.getMonth() + 1), 2) + fix(date.getDate(), 2) + fix(date.getHours(), 2) +  fix(date.getMinutes(), 2));
                    break;
                case "HHMM":
                    return fix((date.getHours()), 2) + fix((date.getMinutes()), 2);
                    break;
                default:
                    return ("" + date.getFullYear() + fix((date.getMonth() + 1), 2) + fix(date.getDate(), 2));
                    break;
            }
        };
        dateTools.getStartDayOfWeek = function(dateObj){
            var date = dateObj || new Date();
            return ("" + date.getFullYear() + fix((date.getMonth() + 1), 2) + fix((date.getDate() - date.getDay()), 2))
        };
        dateTools.getEndDayOfWeek = function(dateObj){
            var date = dateObj || new Date();
            return ("" + date.getFullYear() + fix((date.getMonth() + 1), 2) + fix((date.getDate() + 7 - date.getDay()), 2));
        };
        return dateTools;
    }();

    /**
     * 数字长度补全函数
     * @param num 补全的数字
     * @param length 补全的长度
     * @returns {string}
     */
    function fix(num, length) {
        return ('' + num).length < length ? ((new Array(length + 1)).join('0') + num).slice(-length) : '' + num;
    }

    /**
     * 快照类
     * @param options
     * @constructor
     */
    var SnapShot = function(options){
        this.options = $.extend({}, SnapShot.DEFAULTS, options);
        this.maxOrderDic = null;
        this.workTimeDic = null;
        this.orderObjectDic = null;
    };

    SnapShot.DEFAULTS = {
        reqUrl : "shm001007.json",
        queryData : {
            startTime : dateTools.dateFormat(),
            endTime : dateTools.dateFormat(),
            type : "maxOrder|workTime|order"
        }
    };

    $.extend(SnapShot.prototype, Event , {
        load : function(option){
            var prams = option ? option : {};
            var _this = this;
            var reqDate = $.extend({}, _this.options.queryData, prams.queryData);
            var success = option.success;

            this.fetch({
                data : reqDate,
                success : function(resp){
                    if (resp.respCorrect){
                        _this.maxOrderDic = resp.respData.snapshotDic.maxOrderDic;
                        _this.workTimeDic = resp.respData.snapshotDic.workTimeDic;
                        _this.orderObjectDic = resp.respData.snapshotDic.orderObjectDic;
                    }
                    prams.callback && prams.callback(_this, resp);
                    _this.trigger("load", _this, resp);
                    success && success();
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

    // 工作时间类Model
    var WorkTimeModel = Backbone.Model.extend({
        defaults : {
            tag : "",
            startTime : "00:00",
            endTime : "00:00",
            open : "Y",
            week : "0",
            maxOrder : "0"
        },
        url : testUrl,
        initialize : function(){
            this.attributes.orders = [];
        },
        /* 自定义_save函数实现时间段model固化，较原来Backbone实现的save函数简单 */
        _save : function(options){
            options = options ? _.clone(options) : {};
            if (options.parse === void 0) options.parse = true;
            var success = options.success;
            var model = this;
            options.success = function(resp) {
                if (success) success(model, resp, options);
            };
            this.sync('update', this, options);
        }
    });

    var WorkTimeCollection = Backbone.Collection.extend({
        model : WorkTimeModel,
        initialize :function(models){
        },
        _fetch : function (options) {
            options = options ? _.clone(options) : {};
            if (options.parse === void 0) options.parse = true;
            var success = options.success;
            var collection = this;
            options.success = function(resp) {
                var method = options.reset ? 'reset' : 'set';
                collection[method](resp, options);
                if (success) success(collection, resp, options);
                collection.trigger('sync', collection, resp, options);
            };
            return this.sync('read', this, options);
        }
    });

    // 工作时间类View
    var WorkTimeView = Backbone.View.extend({
        tagName : "div",
        className : "time-bucket noHis",
        template : _.template($("#timeBucket_template").html(), templateOptions),
        events : {
            'click [data-role="open"]' : "setOpen",
            'click .multiDelete' : "multiDelete",
            'click .multiAdd' : "multiAdd",
            'change .multiNum' : "multiNum"
        },
        initialize : function(){
            this.listenTo(this.model, "destroy", this.removeView);
            this.listenTo(this.model, "change:maxOrder", this.renderOrder)
        },
        render : function(){
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },
        setOpen : function(){
            var cur = this.model.get("open") === "Y" ? "N" : "Y";
            var modelFix = {
                workTimeID : true,
                open : true
            };
            this.model.set({ open : cur });

            /* 通过model自定义的_save函数实现数据保存 */
            this.model._save({
                url : testUrl.WTM001003,
                customApi : true,
                protocolCode : "WTM001003",
                data : {
                    "merchantID": merchantID,
                    "workTimeObj" : _.pick(this.model.attributes, function(value, key, object){
                        return modelFix[key];
                    })
                }
            });
        },
        multiNum : function(e){
            var num = parseInt(e.target.value);
            if ( num < 0 ){
                e.target.value = 0;
                return false;
            }
        },
        multiDelete : function(){
            var num = parseInt(this.$(".multiNum").val());
            var modelFix = {
                workTimeID : true,
                maxOrder : true
            };

            if ( (parseInt(this.model.get("maxOrder")) - num) < this.model.attributes.orders.length ) {
                num = parseInt(this.model.get("maxOrder") - this.model.attributes.orders.length);
            }

            this.model.set({ maxOrder : (parseInt(this.model.get("maxOrder")) - parseInt(num)).toString() });

            /* 通过model自定义的_save函数实现数据保存 */
            this.model._save({
                url : testUrl.WTM001003,
                customApi : true,
                protocolCode : "WTM001003",
                data : {
                    "merchantID": merchantID,
                    "workTimeObj" : _.pick(this.model.attributes, function(value, key, object){
                        return modelFix[key];
                    })
                }
            });
        },
        multiAdd : function(){
            var num = this.$(".multiNum").val();
            var modelFix = {
                workTimeID : true,
                maxOrder : true
            };

            this.model.set({ maxOrder : (parseInt(this.model.get("maxOrder")) + parseInt(num)).toString() });
            /* 通过model自定义的_save函数实现数据保存 */
            this.model._save({
                url : testUrl.WTM001003,
                customApi : true,
                protocolCode : "WTM001003",
                data : {
                    "merchantID": merchantID,
                    "workTimeObj" : _.pick(this.model.attributes, function(value, key, object){
                        return modelFix[key];
                    })
                }
            });
        },
        /**
         * 计算符合该时段的order并绘制
         * @returns {HisWorkTimeView}
         */
        renderOrder : function(){
            var orderArray = this.model.attributes.orderArray;
            this.model.attributes.orders = [];

            for (var i = 0; i < orderArray.length ; i++){
                var startTime = orderArray[i].startTime.slice(-4),
                    start = this.model.attributes.startTime.replace(":",""),
                    end = this.model.attributes.endTime.replace(":","");
                if ( start < startTime && startTime <= end ){
                    this.model.attributes.orders.push(orderArray[i]);
                }
            }
            this.render();
            return this;
        }
    });

    // 历史工作时间Model
    var HisWorkTimeModel = Backbone.Model.extend({
        defaults : {
            tag : "",
            startTime : "00:00",
            endTime : "00:00",
            open : "Y",
            week : "0",
            maxOrder : "0"
        },
        url : testUrl,
        initialize : function(){
            this.attributes.orders = [];
        },
        /* 自定义_save函数实现时间段model固化，较原来Backbone实现的save函数简单 */
        _save : function(options){
            options = options ? _.clone(options) : {};
            if (options.parse === void 0) options.parse = true;
            var success = options.success;
            var model = this;
            options.success = function(resp) {
                if (success) success(model, resp, options);
            };
            this.sync('update', this, options);
        }
    });

    var HisWorkTimeCollection = Backbone.Collection.extend({
        model : WorkTimeModel,
        initialize :function(models){

        },
        _fetch : function (options) {
            options = options ? _.clone(options) : {};
            if (options.parse === void 0) options.parse = true;
            var success = options.success;
            var collection = this;
            options.success = function(resp) {
                var method = options.reset ? 'reset' : 'set';
                collection[method](resp, options);
                if (success) success(collection, resp, options);
                collection.trigger('sync', collection, resp, options);
            };
            return this.sync('read', this, options);
        }
    });

    // 历史工作时间View
    var HisWorkTimeView = Backbone.View.extend({
        tagName : "div",
        className : "time-bucket his",
        template : _.template($("#hisWorkTimeTmp").html(), templateOptions),
        initialize : function(){

        },
        render : function(){
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },
        /**
         * 计算符合该时段的order并绘制
         * @returns {HisWorkTimeView}
         */
        renderOrder : function(){
            var orderArray = this.model.attributes.orderArray;
            this.model.attributes.orders = [];

            for (var i = 0; i < orderArray.length ; i++){
                var startTime = orderArray[i].startTime.slice(-4),
                    start = this.model.attributes.startTime.replace(":",""),
                    end = this.model.attributes.endTime.replace(":","");
                if ( start < startTime && startTime <= end ){
                    this.model.attributes.orders.push(orderArray[i]);
                }
            }
            this.render();
            return this;
        }
    });

    /**
     * 工作时间单日的model,主要用来处理单日maxOrder的变更。
     */
    var WorkDayModel = Backbone.Model.extend({
        defaults : {
            maxOrder : null,
            week : null
        },
        initialize : function(){

        },
        _save : function(options){
            options = options ? _.clone(options) : {};
            if (options.parse === void 0) options.parse = true;
            var success = options.success;
            var model = this;
            options.success = function(resp) {
                if (success) success(model, resp, options);
            };
            this.sync('update', this, options);
        },
        setMaxOrder : function(val){
            this.set("maxOrder", val);
        }
    });

    // 工作直接单日View
    var WorkDayView = Backbone.View.extend({
        tagName : "div",
        className : "day-container",
        template : _.template($("#day_template").html(),templateOptions),
        events : {
            'change .day_edit' : "editing",
            'change .day_enable' : "close",
        },
        initialize : function(){
            var _this = this;
        },
        render : function(){
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },
        editing : function(event){
            var dayEnable = this.$(".day_enable");

            if ( !dayEnable.get(0).checked ) {
                return false;
            } else {
                if ( event.target && event.target.checked )  {
                    return this.$el.addClass('editing');
                } else {
                    return this.$el.removeClass('editing');
                }
            }
        },
        close : function(event){
            if ( event.target && event.target.checked ){
                return this.$(".time-buckets-wrap").removeClass('tb-close');
            } else {
                return this.$(".time-buckets-wrap").addClass('tb-close');
            }
        },
        /*
         * 编辑视图动作
         *
         * @Params : string,动作的名称.
         * */
        _editViewControl : function(toggle){

            var edit = this.$('.t-b-edit');
            var goodList = this.$(".good-list");

            if ( !toggle ) { return false; }
            if ( toggle === 'open') {
                edit.addClass('show');
                goodList.addClass('edit');
            } else {
                edit.removeClass('show');
                goodList.removeClass('edit');
            }

        }
    });

    // 货架化展示App View
    var AppView = Backbone.View.extend({
        tagName : "div",
        className : "wt-show-wrap",
        template : _.template($("#app_template").html(), templateOptions),
        initialize : function(){
            var _this = this;

            this.reqDate = new Date();
            this.workTimes = new WorkTimeCollection();
            this.render();

            snapShot.on("load", function(snapShot, response){

                _this.snapShot = snapShot;
                _this.buildSnapShot();
                _this.workTimes._fetch({
                    reset : true,
                    url : testUrl.WTM001006,
                    customApi : true,
                    protocolCode : "WTM001006",
                    data : {
                        "merchantID" : merchantID,
                        "svcID" : Adapter.getParameterByName("serviceid"),
                        "week" : _this.reqDate.getDay()
                    }
                });
            });

            this.workTimes.on("sync", function(collection, response, options){
                _this.buildWorkTimes(collection);
            });

            this.initDayTab();
            this.loadShelf(this.reqDate);
        },
            render : function(){
            this.$el.html(this.template());
            $("#goodShelf").append($(this.el));
            return this;
        },
        // 重新渲染Day视图
        clearDay : function(){
            this.$el.find(".day-container").html("");
            return this;
        },
        // 初始化头部标签
        initDayTab : function(){
            var _this = this;
            var reqFormatDate = dateTools.dateFormat(this.reqDate, "YYYYMMDD");
            var reqDay = this.reqDate.getDay();
            var $tabContainer = this.$('.day-tabs');

            // 轮询创建日期标签
            for ( var i = 0 ; i < 7 ; i ++){
                var $tab = $('<input type="button" class="day-tab">');
                var date = parseInt(reqFormatDate) + ( i - reqDay + 1);

                // 为当日标签添加样式
                if (date.toString() === reqFormatDate) $tab.addClass("active");

                $tab.val(date).attr("data-date", date);

                // 点击标签时读取请求日期的快照数据。
                $tab.on("click", function(){
                    var reqDate = $(this).attr("data-date");
                    var dateObj = new Date();

                    $(this).addClass("active").siblings().removeClass("active");

                    dateObj.setFullYear(parseInt(reqDate.slice(0, 4)), parseInt(reqDate.slice(4, 6)) - 1, parseInt(reqDate.slice(6, 8)));
                    _this.reqDate = dateObj;
                    _this.loadShelf()
                });

                $tabContainer.append($tab);
            }
        },
        /**
         * 计算并构建非历史时段的可设置工作时间段
         * @param collection
         */
        buildWorkTimes : function(collection){
            var _this = this;
            var dateStr = dateTools.dateFormat(this.reqDate, "YYYYMMDD");
            var orderArray = [];

            if (!this.snapShot.orderObjectDic[dateStr]){
                this.showError();
                return
            }

            orderArray = this.snapShot.orderObjectDic[dateStr];

            _.each(collection.models, function(model, index, collection){
                var startTime, endTime, nowTime, workTimeView;
                startTime = model.get("startTime").replace(":", "");
                endTime = model.get("endTime").replace(":", "");

                model.attributes.orderArray = orderArray;
                workTimeView = new WorkTimeView({model: model});

                nowTime = fix((new Date().getHours()), 2) + fix((new Date().getMinutes()), 2);
                if ( !(nowTime > startTime && nowTime > endTime)  ) {
                    _this.$el.find(".time-buckets").append(workTimeView.renderOrder().$el);
                }
            });
        },
        // 构建快照类DOM
        buildSnapShot : function() {

            // 在重新构造快照数据前，清空快照
            this.clearDay();

            // 构建快照单日
            this.buildHisDay();

            // 构建快照工作时间
            this.buildHisWorkTime();

        },
        buildHisDay : function () {
            var dateStr = dateTools.dateFormat(this.reqDate, "YYYYMMDD");
            var maxOrderArray = [];

            if (!this.snapShot.maxOrderDic[dateStr]){
                this.showError();
                return
            }

            maxOrderArray = this.snapShot.maxOrderDic[dateStr];

            for(var i = 0 ; i < maxOrderArray.length ; i++ ) {
                var workDayModel = new WorkDayModel(maxOrderArray[i]);
                var workDayView = new WorkDayView({model: workDayModel});
                this.$el.find(".day-container").append(workDayView.render().$el);
            }
        },
        buildHisWorkTime : function(){
            var dateStr = dateTools.dateFormat(this.reqDate, "YYYYMMDD");
            var workTimeArray = [];
            var orderArray = this.snapShot.orderObjectDic[dateStr];
            var nowDate = dateTools.dateFormat();
            var nowTime = dateTools.dateFormat(new Date(), "HHMM");

            if (!this.snapShot.workTimeDic[dateStr]){
                this.showError();
                return
            }

            workTimeArray = this.snapShot.workTimeDic[dateStr];


            for (var i = 0 ; i < workTimeArray.length ; i++){
                var hisWorkTimeModel, hisWorkTimeView, startTime, endTime;
                hisWorkTimeModel = new HisWorkTimeModel(workTimeArray[i]);
                hisWorkTimeModel.attributes.orderArray = orderArray;
                hisWorkTimeView = new HisWorkTimeView({model : hisWorkTimeModel});

                startTime = hisWorkTimeModel.get("startTime").replace(":","");
                endTime = hisWorkTimeModel.get("endTime").replace(":","");

                if ( nowDate < dateStr ) return;
                if ( nowTime > endTime && nowTime > startTime ){
                    this.$el.find(".time-buckets").append(hisWorkTimeView.renderOrder().$el);
                }
            }
        },
        loadShelf : function(){
            var _this = this;
            var reqDate = dateTools.dateFormat(this.reqDate, "YYYYMMDD");
            //var reqDay = this.reqDate.getDay();
            this.$(".day-container").removeClass("error");
            this.$(".day-container").addClass("loading");
            snapShot.load({
                queryData : {
                    "startTime": reqDate,
                    "endTime": reqDate,
                    "type": "maxOrder|workTime|order"
                },
                success : function(){
                    _this.$(".day-container").removeClass("loading");
                }
            });
        },
        showError : function(){
            this.$(".day-container").addClass("error");
        }
    });

    var app = new AppView();
})();