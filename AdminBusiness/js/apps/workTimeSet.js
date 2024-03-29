/**
 * workTime.js v1.0.0 @ 2016-05-17 by licdream@126.com
 * 工作时间设定功能实现
 */
(function (){
    var templateOptions = {
        interpolate: /\{%=(.+?)%\}/g,
        escape:      /\{%-(.+?)%\}/g,
        evaluate:    /\{%(.+?)%\}/g
    };

    // 全局获取用户ID;
    var merchantID = document.getElementById("merchantID").value;

    /**
     * 全局API url设置
     * @type {string}
     */
    var globalApiUrl = document.getElementById("hiApiUrl").value;
    //var globalApiUrl = "test.json";

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

    function intTime(time){
        return parseInt((time).replace(/\:/, ""), 10)
    }

    /**
     * 工作时间Model
     */
    var WorkTimeModel = Backbone.Model.extend({
        defaults : {
            tag : "",
            startTime : "00:00",
            endTime : "00:00",
            open : "Y",
            week : "0",
            maxOrder : "0"
        },
        url : globalApiUrl,
        initialize : function(){

        },
        /* 自定义_save函数实现时间段model固化，较原来save函数简单 */
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
        initialize : function(){

        },
        /**
         * 自定义fetch函数，用于单日时间段初始化
         * @param options
         * @returns {*}
         * @private
         */
        _fetch : function(options){
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
        },
        /**
         * 时间段验证函数，用于验证设置的时间是否在models的时间段里重复
         * @param startTime 设置的开始时间
         * @param endTime 设置的结束时间
         * @returns {boolean} 验证结果，true为时间段有效
         */
        timeValid: function (startTime, endTime, exceptModel){
            var valid = true, validModels = this.models;

            // 开始时间不得大于或等结束时间
            if ( intTime(startTime) >= intTime(endTime) ){
                return valid = false;
            }


            if(exceptModel){
                validModels = _.reject(this.models, function(modelItem){
                    return modelItem.get("id") === exceptModel.get("id");
                })
            }

            _.each(validModels, function(modelItem){

                // 时段不能包含或覆盖已有时间段
                if ( !( ( intTime(startTime) >= intTime(modelItem.get("endTime")) ) || ( intTime(endTime) <= intTime(modelItem.get("startTime")) ) ) ){
                    valid = false
                }
            });

            return valid
        }
    });

    var WorkTimeView = Backbone.View.extend({
        tagName : "div",
        className : "wt",
        template : _.template($("#workTimeTmp").html(), templateOptions),
        events : {
            'click [data-role="delete"]' : 'deleteWorkTime',
            'click [data-role="edit"]' : "setEdit",
            'click [data-role="confirm"]' : "setConfirm",
            'click [data-role="open"]' : "setOpen"
        },
        initialize : function(){
            this.listenTo(this.model, "destroy", this.removeView)
        },
        render : function(){
            this.$el.html(this.template(this.model.toJSON()));
            this.$(".time-pick").timePick();
            return this;
        },
        /**
         * 删除工作时间段
         */
        deleteWorkTime : function(){
            this.model.destroy({
                url : globalApiUrl,
                customApi : true,
                protocolCode : "WTM001002",
                data: {
                    "svcID": YDBan.url.getUrlParam("serviceid"),
                   
                    "merchantID": merchantID,
                    "workTimeID": this.model.get("workTimeID")
                }
            });
        },
        removeView : function(){
            this.remove();
        },
        setEdit : function(toggle){
            this.setEditView(true);
        },
        /**
         * 修改时间段，确认并提交动作
         */
        setConfirm : function(){
            var attr = {
                startTime : this.$('[data-role="startTime"]').val(),
                endTime : this.$('[data-role="endTime"]').val(),
                maxOrder : this.$('[data-role="maxOrder"]').val()
            };

            this.workDayView.showWorkTimeError(false);

            if ( parseInt(attr.maxOrder) > parseInt(this.workDayView.model.attributes.maxOrder) ){
                return this.workDayView.showWorkTimeError(true, "接单量应小于当日单量");
            }

            // 当设置时间段于原来不一样时，验证时间有效性
            if ( !(this.model.get("startTime") === attr.startTime && this.model.get("endTime") === attr.endTime) && !this.model.collection.timeValid(attr.startTime, attr.endTime, this.model) ){
                return this.workDayView.showWorkTimeError(true, "时间段设置重复或错误");
            }

            // 通过设置相关Model属性为true来筛选Model内需要同步的属性
            var modelFix = {
                workTimeID : true,
                tag : true,
                startTime : true,
                endTime : true,
                open : true,
                week : true,
                maxOrder : true
            };
            this.model.set(attr);

            /* 通过model自定义的_save函数实现数据保存 */
            this.model._save({
                url : globalApiUrl,
                customApi : true,
                protocolCode : "WTM001003",
                data: {
                    "svcID": YDBan.url.getUrlParam("serviceid"),
                    "merchantID": merchantID,
                    "workTimeObj" : _.pick(this.model.attributes, function(value, key, object){
                        return modelFix[key];
                    })
                }
            });
            this.render();
            this.setEditView(false);
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
                url : globalApiUrl,
                customApi : true,
                protocolCode : "WTM001003",
                data: {
                    "svcID": YDBan.url.getUrlParam("serviceid"),
                    "merchantID": merchantID,
                    "workTimeObj" : _.pick(this.model.attributes, function(value, key, object){
                        return modelFix[key];
                    })
                }
            });
        },
        setEditView : function(toggle){
            return toggle ? this.$el.addClass("editing") : this.$el.removeClass("editing");
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

            // 按照开始时间强制重排序
            this.workTimes.comparator = function(m1, m2){
                if ( parseInt(m1.attributes.startTime.replace(/\:/, "")) > parseInt(m2.attributes.startTime.replace(/\:/, ""))){
                    return 1 ;
                } else {
                    return 0;
                }
            };

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
                        "svcID": YDBan.url.getUrlParam("serviceid"),
                        "maxOrderString": maxOrderString
                    }
                }
            })
        }
    });

    var workDays = new WorkDayCollection();

    var WorkDayView = Backbone.View.extend({
        tagName : "div",
        className : "wd-item-wrap",
        template : _.template($("#workDayTmp").html(),templateOptions),
        events : {
            'change [data-role="dayMaxOrder"]' : "setMaxOrder",
            'click [data-role="changeMax"]' : "changeMaxOrder",
            'click [data-role="changeMaxConf"]' : "changeMaxConf",
            'click [data-role="cTrigger"]' : "preCreate",
            'click [data-role="cCancel"]' : "cancelCreate",
            'click [data-role="cConfirm"]' : "addWorkTime"
        },
        initialize : function(){
            var _this = this;
            this.$el.addClass("loading");
            this.listenTo(this.model.workTimes, 'add', this.saveWorkTime);
            this.listenTo(this.model.workTimes, 'reset', function(collection, options){
                this.$el.removeClass("loading");

                _.each(collection.models, function(workTimeModel){
                    /* 初始化id */
                    workTimeModel.set("id" , workTimeModel.get("workTimeID"), {silent: true});
                    _this.initWorkTimeView(workTimeModel);
                })
            });
            /* 初始化单日内的所有时间段 */
            this.model.workTimes._fetch({
                reset : true,
                url : globalApiUrl,
                customApi : true,
                protocolCode : "WTM001006",
                data : {
                    "merchantID" : merchantID,
                    "svcID" : YDBan.url.getUrlParam("serviceid"),
                    "week" : _this.model.get("week")
                }
            });
        },
        render : function(){
            this.$el.html(this.template(this.model.toJSON()));
            this.$(".time-pick").timePick();
            return this;
        },
        clearWortTimeView: function(){
            return this.$el.find('.wt-container').html("");
        },
        initWorkTimeView : function(workTimeModel){
            var workTimeView = new WorkTimeView({model : workTimeModel });
            workTimeView.workDayView = this;
            this.$el.find('.wt-container').append(workTimeView.render().el);
        },
        preCreate : function(){
          this.$el.find(".wd-b").toggleClass("creating");
        },
        cancelCreate : function(){
            this.$el.find(".wd-b").removeClass("creating");
        },
        /**
         * 增加时间段
         */
        addWorkTime : function(){
            var _this = this;
            var workTimeAttr = {
                startTime : _this.$el.find('[data-role="cStartTime"]').val(),
                endTime : _this.$el.find('[data-role="cEndTime"]').val(),
                maxOrder : _this.$el.find('[data-role="cMaxOrder"]').val(),
                week : _this.model.get("week")
            };

            if ( !this.model.workTimes.timeValid(workTimeAttr.startTime, workTimeAttr.endTime) ) {
                return this.showError(true, "时间段设置重复或错误");
            }

            if ( workTimeAttr.maxOrder > this.model.attributes.maxOrder ){
                return this.showError(true, "接单量应小于当日单量");
            }

            this.showError(false);
            // 添加成功，重置设置区数据
            _this.$el.find('[data-role="cStartTime"]').val("00:00");
            _this.$el.find('[data-role="cEndTime"]').val("00:00");
            _this.$el.find('[data-role="cMaxOrder"]').val(0);

            this.model.addWorkTime(workTimeAttr);
            this.$el.find(".wd-b").removeClass("creating");

        },
        /**
         * 固化时间段
         * @param workTimeModel
         */
        saveWorkTime : function(workTimeModel){
            var _this = this;

            workTimeModel.save(null, {
                url : globalApiUrl,
                customApi : true,
                protocolCode : "WTM001001",
                data : {
                    "merchantID": merchantID,
                    "svcID": YDBan.url.getUrlParam("serviceid"),
                    "workTimeObj" : workTimeModel.attributes
                },
                success : addWorkTimeView
            });

            function addWorkTimeView(model, resp, options){

                _this.clearWortTimeView();

                /* 初始化id */
                model.set("id", resp[0], {silent: true} );
                model.set("workTimeID", resp[0], {silent: true} );

                _.each(model.collection.models, function(modelItem){
                    _this.initWorkTimeView(modelItem);
                })

            }
        },
        setMaxOrder : function(e){
            var val = $(e.target).val();
            this.model.setMaxOrder(val);
        },
        changeMaxOrder : function(){
            this.$el.find(".wd-order-edit").toggleClass("editing");
        },
        changeMaxConf : function(){
            workDays._saveMaxOrder();
            this.$el.find(".wd-maxOrder").html(this.model.get('maxOrder'));
            this.$el.find(".wd-order-edit").toggleClass("editing");
        },
        showError: function(toggle, text){
            if ( toggle ){
                this.$el.find(".wd-add").addClass("err");
                this.$el.find(".wd-errMsg").html(text);
            } else {
                this.$el.find(".wd-add").removeClass("err");
            }
        },
        showWorkTimeError: function(toggle, text){
            if ( toggle ){
                this.$el.find(".wd-error").addClass("err").html(text);
            } else {
                this.$el.find(".wd-error").removeClass("err");
            }
        }
    });

    var AppView = Backbone.View.extend({
        tagName : "div",
        className : "wt-set-wrap",
        template : _.template($("#workTimeSetTmp").html(), templateOptions),
        initialize : function (){
            var _this = this;

            if (!YDBan.url.getUrlParam("serviceid")) {
                throw new Error("路径参数缺少serviceid")
            };

            this.render();
            this.showLoading(true);

            // 监听sync事件，添加DayView
            this.listenTo(workDays, 'sync', function(collection, resp, options){

                _this.showLoading(false);

                _.each(collection.models, function(dayModel){
                    _this.addDayView(dayModel);
                })
            });

            workDays._fetch({
                url : globalApiUrl,
                customApi : true,
                protocolCode : "SVC001005",
                data : {
                    "merchantID": merchantID,
                    "svcID": YDBan.url.getUrlParam("serviceid")
                },
                error : function(){
                    _this.$el.removeClass("loading").addClass("error");
                }
            });
        },
        render : function(){
            this.$el.html(this.template());
            $("#workTimeSet").append($(this.el));
            return this;
        },
        addDayView : function(dayModel){
            var workDayView = new WorkDayView({model : dayModel});
            this.$(".wt-set").append(workDayView.render().$el);
        },
        showLoading : function(toggle){
            return toggle ? this.$el.addClass("loading") : this.$el.removeClass("loading");
        }
    });

    var app = new AppView();
})();