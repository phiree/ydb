// 货架化展示App View
/**
 * shelf.js v1.0.0 @ 2016-05-17 by licdream@126.com
 * 服务货架实现
 */
(function (){
    var templateOptions = {
        interpolate: /\{%=(.+?)%\}/g,
        escape:      /\{%-(.+?)%\}/g,
        evaluate:    /\{%(.+?)%\}/g
    };

    // 请求URl
    //var globalApiUrl = "shm001007.json";
    var globalApiUrl = document.getElementById("hiApiUrl").value;

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

    /**
     * 日期对象工具。TODO: 文件内一些日期格式化或反格式化方法还没有包含
     */
    var dateTools = function(){
        var dateTools = {};
        dateTools.dateFormat = function(dateObj, options){

            if (arguments.length === 1){
                options = dateObj;
                dateObj = new Date();
            }

            var date = dateObj || new Date();
            var option = (typeof options === "string") ? options.toUpperCase() : "YYYYMMDD";

            switch (option){
                case "YYYYMMDD":
                    return ("" + date.getFullYear() + fix((date.getMonth() + 1), 2) + fix(date.getDate(), 2));
                    break;
                case "YYYYMMDDHHMMSS":
                    return ("" + date.getFullYear() + fix((date.getMonth() + 1), 2) + fix(date.getDate(), 2) + fix(date.getHours(), 2) +  fix(date.getMinutes(), 2) +　fix(date.getSeconds(), 2));
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
        dateTools.fmt = function (dateObj, fmt) { //author: meizz

            if (arguments.length === 1){
                fmt = dateObj;
                dateObj = new Date();
            }

            var o = {
                "M+": dateObj.getMonth() + 1, //月份
                "d+": dateObj.getDate(), //日
                "h+": dateObj.getHours(), //小时
                "m+": dateObj.getMinutes(), //分
                "s+": dateObj.getSeconds(), //秒
                "q+": Math.floor((dateObj.getMonth() + 3) / 3), //季度
                "S": dateObj.getMilliseconds() //毫秒
            };
            if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (dateObj.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o)
                if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            return fmt;
        }
        ;
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

    function getQueryString(name){
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if(r!=null){ return decodeURI(r[2])}
    }

    /**
     * 快照集合类
     * @param options
     * @constructor
     */
    var SnapShots= function(options){
        this.options = $.extend({}, SnapShots.DEFAULTS, options);
    };

    SnapShots.DEFAULTS = {
        //reqUrl : "shm001007.json",
        queryData : {
            //merchantID : merchantID,
            //svcID : getQueryString("serviceId"),
            svcID : Adapter.getParameterByName("serviceId"),
            //startTime : dateTools.dateFormat(""),
            //endTime : dateTools.dateFormat(""),
            startTime : dateTools.fmt("yyyy-M-d"),
            endTime : dateTools.fmt("yyyy-M-d"),
        }
    };

    $.extend(SnapShots.prototype, Event , {
        load : function(option){
            var prams = option ? option : {};
            var _this = this;
            var reqDate = $.extend({}, _this.options.queryData, prams.queryData);
            var success = option.success;

            this.fetch({
                url : globalApiUrl,
                customApi : true,
                protocolCode : "SHM001007",
                data : reqDate,
                success : function(resp){

                    // 测试代码
                    //if (resp.respCorrect){
                    //    _this._init(resp.respData.snapshots);
                    //}

                    _this._init(resp.snapshots);
                    prams.callback && prams.callback(_this, resp);
                    _this.trigger("load", _this, resp);
                    success && success();
                }
            });
        },
        _init: function(snapshots){
            this.snapshotItems = snapshots;
        },
        /**
         * 根据日期筛选快照
         * @param date 日期,格式为YYYYMMDD
         * @returns {Array}
         */
        getOrderSnapshotByDate : function(date){
            var orderSnapshots = [];
            var items = this.snapshotItems;

            if ( items.length ) {
                for (var i = 0; i < items.length; i++){
                    if (items[i].date === date){
                        orderSnapshots.push(items[i].orderSnapshots);
                    }
                }
            }

            return orderSnapshots

        },
        /**
         * 根据服务开始时间筛选快照
         * @param date 日期,格式为YYYYMMDD
         * @param startTime 开始时间,格式HH:MM,
         * @param endTime 结束时间,格式HH:MM,
         * @returns {Array}
         */
        getOrderSnapshotByTime : function(date, startTime, endTime){
            var _startTime = date + timeFix(startTime);
            var _endTime = date + timeFix(endTime);

            var orderSnapShots = [], stamp;

            stamp = this.getOrderSnapshotByDate(date).length ? this.getOrderSnapshotByDate(date)[0] : [];

            function timeFix(time){
                if (time.indexOf(":")){
                    return time.replace(":", "") + "00";
                }
                return time + "00";
            }

            for ( var i = 0; i < stamp.length; i++ ){
                if (parseInt(stamp[i].orderObj.svcObj.startTime) > parseInt(_startTime) && parseInt(stamp[i].orderObj.svcObj.startTime) < parseInt(_endTime)){
                    orderSnapShots.push(stamp[i])
                }
            }

            return orderSnapShots;
        },
        fetch : function(option){
            var options = option || {};
            var success = options.success;
            options.success = function(resp){

                // 测试代码
                //var response = Adapter.respUnpack(resp);
                //if(success) success(response);

                if(success) success(resp);
            };
            this.sync(options);
        },
        sync : function(options){

            return Backbone.sync('read', this, options);

            // 测试代码
            //var xhr = $.ajax({
            //    url : options.url,
            //    type : "POST",
            //    //dataType : "json",
            //    dataType : "text",
            //    data : JSON.stringify(options.data),
            //    success : options.success
            //});
            //return xhr;
        }
    });

    var snapshots = new SnapShots();

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
        //url : testUrl,
        url : globalApiUrl,
        initialize : function(){
            //this.attributes.orders = [];
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
        className : "time-bucket",
        template : _.template($("#timeBucket_template").html(), templateOptions),
        events : {
            'click [data-role="open"]' : "setOpen",
            'click .multiDelete' : "multiDelete",
            'click .multiAdd' : "multiAdd",
            'change .multiNum' : "multiNum"
        },
        initialize : function(){
            this.listenTo(this.model, "destroy", this.removeView);
            this.listenTo(this.model, "change:maxOrder", this.renderOrder);
            this.listenTo(this.model, "change:open", this.render)
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
                //url : testUrl.WTM001003,
                url : globalApiUrl,
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
                url : globalApiUrl,
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

            //
            if ( parseInt(this.model.get("maxOrder")) + parseInt(num) > this.model.attributes.dayEnableOrderCount ){
                app.showWarnText(true, "时段剩余服务应小于当日剩余服务数量");
                return false;
            }

            app.showWarnText(false);

            this.model.set({ maxOrder : (parseInt(this.model.get("maxOrder")) + parseInt(num)).toString() });
            /* 通过model自定义的_save函数实现数据保存 */
            this.model._save({
                url : globalApiUrl,
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
            var orderSSArray = this.model.attributes.orderSSArray;
            this.model.attributes.orders = orderSSArray;
            //this.model.attributes.orders = [];

            //for (var i = 0; i < orderSSArray.length ; i++){
            //    var startTime = orderSSArray[i].orderObj.svcObj.startTime.slice(-6,-2),
            //        start = this.model.attributes.startTime.replace(":",""),
            //        end = this.model.attributes.endTime.replace(":","");
            //    if ( start < startTime && startTime <= end ){
            //        this.model.attributes.orders.push(orderSSArray[i]);
            //    }
            //}
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
            this.workTimes = new WorkTimeCollection();
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
        addWorkTime : function(attr){
            var workTimeModel = new WorkTimeModel(attr);
            this.workTimes.add(workTimeModel);
        },
        setMaxOrder : function(val){
            this.set("maxOrder", val);
        }
    });

    var WorkDayCollection = Backbone.Collection.extend({
        model : WorkDayModel,
        url : globalApiUrl,
        initialize : function(){
            var _this = this;
        },
        /**
         * 自定义私有fetch函数对SVC001005请求接口的数据处理，使其返回的svcObj能按照预期初始化collection
         * @param options
         * @returns {*}
         */
        _fetch : function(options) {
            options = options ? _.clone(options) : {};
            if (options.parse === void 0) options.parse = true;
            var success = options.success;
            var collection = this;
            options.success = function(resp) {
                var method = options.reset ? 'reset' : 'set';
                var respArray;

                // 转换maxOrderString为数组
                if ( typeof resp.svcObj === "object" && resp.svcObj ){
                    if ( typeof resp.svcObj.maxOrderString === "string" && resp.svcObj.maxOrderString ){
                        respArray = resp.svcObj.maxOrderString.split(",");
                        for ( var i = 0 ; i < respArray.length ; i++  ){
                            respArray[i] = { maxOrder : respArray[i] , week : i + 1 + "" }
                        }
                    }
                }

                collection[method](respArray, options);
                if (success) success(collection, respArray, options);
                collection.trigger('sync', collection, respArray, options);
            };

            return this.sync('read', this, options);
        },
        _save : function(options){
            options = options ? _.clone(options) : {};
            if (options.parse === void 0) options.parse = true;
            var success = options.success;
            var collection = this;
            options.success = function(resp) {
                if (success) success(collection, resp, options);
            };
            this.sync('update', this, options);
        },
        _saveMaxOrder : function () {
            var maxOrderArr = [], maxOrderString;

            _.each(this.models, function(model){
                maxOrderArr.push(model.get("maxOrder"));
            });

            maxOrderString = maxOrderArr.join();

            this._save({
                url : globalApiUrl,
                customApi : true,
                protocolCode : "SVC001003",
                data : {
                    "merchantID": merchantID,
                    "svcObj": {
                        "svcID": Adapter.getParameterByName("serviceId"),
                        "maxOrderString": maxOrderString
                    }
                }
            })
        }
    });

    var workDays = new WorkDayCollection();

    // 工作直接单日View
    var WorkDayView = Backbone.View.extend({
        tagName : "div",
        className : "day",
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

    var AppView = Backbone.View.extend({
        tagName : "div",
        className : "wt-show-wrap",
        template : _.template($("#app_template").html(), templateOptions),
        initialize : function(){
            var _this = this;
            this.reqDate = new Date();

            this.snapshots = snapshots;
            this.workTimes = new WorkTimeCollection();
            this.workDays = workDays;

            this.render();
            this.initDayTab();

            // 注册snapshots的load事件，-对快照进行构建
            //snapshots.on("load", function(snapshotsObj, response){});

            // 注册workTime的sync事件，对返回的工作时间数据构建
            this.listenTo(this.workTimes, "sync", function(collection, response, options){
                _this.buildWorkTimesView(collection);
            });

            // 注册workDay的sync事件，对返回的工作单日数据处理
            this.listenTo(this.workDays, 'sync', function(collection, response, options){

                _this.buildDaysView(collection)
            });

            this.loadShelf(this.reqDate);
        },
        render : function(){
            this.$el.html(this.template());
            $("#goodShelf").append($(this.el));
            return this;
        },
        loadShelf : function(reqDateObj){
            var _this = this, reqDate;

            if ( typeof reqDateObj !== "undefined" ){
                reqDate = dateTools.fmt(reqDateObj, "yyyy-M-d");
            } else {
                return
            }

            _this.showLoading(true);

            // 请求快照数据
            snapshots.load({
                queryData : {
                    "startTime": reqDate,
                    "endTime": reqDate
                },
                success : function(){

                    // 请求工作单日数据
                    _this.workDays._fetch({
                        url : globalApiUrl,
                        customApi : true,
                        protocolCode : "SVC001005",
                        data : {
                            "merchantID": merchantID,
                            "svcID": Adapter.getParameterByName("serviceId")
                        },
                        success : function(){

                            // 请求工作时间数据
                            _this.workTimes._fetch({
                                reset : true,
                                url : globalApiUrl,
                                customApi : true,
                                protocolCode : "WTM001006",
                                data : {
                                    "merchantID" : merchantID,
                                    "svcID" : Adapter.getParameterByName("serviceId"),
                                    "week" : reqDateObj.getDay()
                                },
                                success: function () {
                                    _this.showLoading(false);
                                },
                                error : function(){
                                    _this.showError();
                                }
                            });
                        },
                        error : function(){
                            _this.showError();
                        }
                    });
                }
            });
        },
        /**
         * 初始化头部标签
         */
        initDayTab : function(){
            var _this = this;
            var reqFormatDate = dateTools.dateFormat(this.reqDate, "YYYYMMDD");
            var $tabContainer = this.$('.day-tabs');

            function tranWeek(week){
                var arr = [ "星期日","星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
                return arr[week];
            }

            // 轮询创建日期标签
            for ( var i = 0 ; i < 7 ; i ++){
                var $tab = $('<input type="button" class="day-tab">');
                //var date = parseInt(reqFormatDate) + ( i - reqDay + 1);
                var dateText = (parseInt(reqFormatDate) + i).toString(), dateTemp = new Date();

                // 为当日标签添加样式
                if (dateText === reqFormatDate) { $tab.addClass("active") }

                dateTemp.setFullYear(parseInt(dateText.slice(0, 4)), parseInt(dateText.slice(4, 6)) - 1, parseInt(dateText.slice(6, 8)));

                $tab.val(dateTools.fmt(dateTemp, "yyyy-M-d") + " " + tranWeek(dateTemp.getDay()))
                    .attr("data-date", dateText);

                // 点击标签时读取请求日期的快照数据。
                $tab.on("click", function(){
                    var reqDate = $(this).attr("data-date");
                    var dateObj = new Date();

                    $(this).addClass("active").siblings().removeClass("active");

                    dateObj.setFullYear(parseInt(reqDate.slice(0, 4)), parseInt(reqDate.slice(4, 6)) - 1, parseInt(reqDate.slice(6, 8)));
                    _this.switchDayShelf(dateObj);
                });

                $tabContainer.append($tab);
            }
        },
        /**
         * 按照时间切换当前显示的货架化
         * @param reqDateObj Date()对象
         */
        switchDayShelf : function(reqDateObj){
            this.reqDate = reqDateObj;

            this.clearDay();

            this.loadShelf(reqDateObj);
        },
        /**
         * 构建工作时段
         * @param collection
         */
        buildWorkTimesView : function(collection){
            var _this = this;
            var dateStr = dateTools.dateFormat(this.reqDate, "YYYYMMDD");

            _.each(collection.models, function(model, index, collection){
                var startTime, endTime, nowTime, workTimeView;
                var orderSSArray = [];

                startTime = model.get("startTime").replace(":", "");
                endTime = model.get("endTime").replace(":", "");

                orderSSArray = _this.snapshots.getOrderSnapshotByTime(dateStr, startTime, endTime);

                console.log(_this.reqDate.getDay());

                model.attributes.dayMaxOrderCount = _this.workDays.where({week: (_this.reqDate.getDay() + 1).toString()})[0].attributes.maxOrder;
                model.attributes.dayReorderCount = _this.snapshots.snapshotItems.length;
                model.attributes.dayEnableOrderCount = parseInt(model.attributes.dayMaxOrderCount) - parseInt(model.attributes.dayReorderCount);

                model.attributes.orderSSArray = orderSSArray;

                workTimeView = new WorkTimeView({model: model});

                //nowTime = fix((new Date().getHours()), 2) + fix((new Date().getMinutes()), 2);
                //if ( !(nowTime > startTime && nowTime > endTime)  ) {
                //}

                _this.renderWorkTime(workTimeView);

            });
        },
        /**
         * 构建单日工作时间，根据dateObj筛选单日的数据
         * @param collection 单日工作时间集合Collection
         */
        buildDaysView : function(collection){
            var _this = this;
            var dayOfWeek = this.reqDate.getDay();
            var dateStr = dateTools.dateFormat(this.reqDate, "YYYYMMDD");

            _.each(collection.models, function(dayModel, index, collection){
                if (dayOfWeek == index ){
                    dayModel.attributes.reOrder = _this.snapshots.getOrderSnapshotByDate(dateStr).length;
                    dayModel.attributes.date = dateStr;

                    var workDayView = new WorkDayView({model : dayModel});

                    _this.renderDay(workDayView);
                }
            });
        },
        clearDay : function(){
            this.$el.find(".day-container").html("");
            return this;
        },
        renderDay: function(workDayView){
            return this.$el.find(".day-container").append(workDayView.render().$el);
        },
        renderWorkTime : function(workTimeView){
            return this.$el.find(".time-buckets").append(workTimeView.renderOrder().$el);
        },
        showLoading: function(toggle){
            return toggle ? this.$(".day-container").removeClass("error").addClass("loading") : this.$(".day-container").removeClass("loading");
        },
        showError : function(){
            this.$(".day-container").removeClass("loading").addClass("error");
        },
        showWarnText : function(show, test){
            return !show ? this.$("#day-warn").addClass("hi") : this.$("#day-warn").removeClass("hi").html(test);
        }
    });

    var app = new AppView();
})();
