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
    //var reqUrl = "http://localhost:806/dianzhuapi.ashx";
    var reqUrl = "test.json";

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
        url : reqUrl,
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
            //this.$(".time-select-wrap").timeSelect();
            this.$(".time-pick").timePick();
            return this;
        },
        /**
         * 删除工作时间段
         */
        deleteWorkTime : function(){
            this.model.destroy({
                url : "test.001002.json",
                customApi : true,
                protocolCode : "WTM001002",
                data : {
                    "merchantID": merchantID,
                    "workTimeID": this.model.get("workTimeID")
                }
            });
        },
        removeView : function(){
            this.remove();
        },
        setEdit : function(){
            this.$el.addClass("editing");
        },
        /**
         * 修改时间段，确认并提交动作函数
         */
        setConfirm : function(){
            var attr = {
                startTime : this.$('[data-role="startTime"]').val(),
                endTime : this.$('[data-role="endTime"]').val(),
                maxOrder : this.$('[data-role="maxOrder"]').val()
            };
            // 通过设置相关Model属性为true来筛选Model内需要的属性
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
                url : "test.001003.json",
                customApi : true,
                protocolCode : "WTM001003",
                data : {
                    "merchantID": merchantID,
                    "svcObj" : _.pick(this.model.attributes, function(value, key, object){
                        return modelFix[key];
                    })
                }
            });
            this.render();
            this.$el.removeClass("editing");
        },
        setOpen : function(){
            var cur = this.model.get("open") === "Y" ? "N" : "Y";
            var modelFix = {
                open : true
            };
            this.model.set({ open : cur });

            /* 通过model自定义的_save函数实现数据保存 */
            this.model._save({
                url : "test.001003.json",
                customApi : true,
                protocolCode : "WTM001003",
                data : {
                    "merchantID": merchantID,
                    "svcObj" : _.pick(this.model.attributes, function(value, key, object){
                        return modelFix[key];
                    })
                }
            });
        }
    });


    /**
     * 工作时间单日的model,主要用来处理单日maxOrder的变更。
     */
    var WorkDayModel = Backbone.Model.extend({
        defaults : {
            maxOrder : null
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
        url : reqUrl,
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
                url : "test.s001005.json",
                customApi : true,
                protocolCode : "SVC001005",
                data : {
                    "merchantID": merchantID,
                    "svcObj": {
                        "svcID": Adapter.getParameterByName("serviceid"),
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
            'click [data-role="cConfirm"]' : "addWorkTime",
        },
        initialize : function(){
            var _this = this;
            //this.model.workTimes = new WorkTimeCollection();
            this.listenTo(this.model.workTimes, 'add', this.saveWorkTime);
            this.listenTo(this.model.workTimes, 'reset', function(collection, options){
                _.each(collection.models, function(workTimeModel){
                    /* 初始化id */
                    workTimeModel.set("id" , workTimeModel.get("workTimeID"), {silent: true});
                    _this.initWorkTimeView(workTimeModel);
                })
            });
            /* 初始化单日内的所有时间段 */
            this.model.workTimes._fetch({
                reset : true,
                url : "test.001006.json",
                customApi : true,
                protocolCode : "WTM001006",
                data : {
                    "merchantID": merchantID,
                    "svcID": Adapter.getParameterByName("serviceid")
                }
            });
        },
        render : function(){
            this.$el.html(this.template(this.model.toJSON()));
            //this.$(".time-select-wrap").timeSelect();
            this.$(".time-pick").timePick();
            return this;
        },
        initWorkTimeView : function(workTimeModel){
            var workTimeView = new WorkTimeView({model : workTimeModel});
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
                week : _this.$el.find('[data-role="cWeek"]').val()
            };

            this.model.addWorkTime(workTimeAttr);
            this.$el.find(".wd-b").removeClass("creating");
        },
        saveWorkTime : function(workTimeModel){
            var _this = this;
            // 固化增加的时间段

            workTimeModel.save(null, {
                url : "test.001001.json",
                customApi : true,
                protocolCode : "WTM001001",
                data : {
                    "merchantID": merchantID,
                    "svcID": Adapter.getParameterByName("serviceid"),
                    "workTimeObj" : workTimeModel.attributes
                },
                success : addWorkTimeView
            });

            function addWorkTimeView(model, resp, options){
                model.set("id", resp[0], {silent: true} );
                model.set("workTimeID", resp[0], {silent: true} );
                var workTimeView = new WorkTimeView({model : model});
                _this.$el.find('.wt-container').append(workTimeView.render().el);
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
        }
    });


    var AppView = Backbone.View.extend({
        tagName : "div",
        className : "wt-set-wrap",
        template : _.template($("#workTimeSetTmp").html(), templateOptions),
        initialize : function (){
            var _this = this;
            this.render();
            this.listenTo(workDays, 'sync', function(collection, resp, options){
                _.each(collection.models, function(dayModel){
                    _this.addDayView(dayModel);
                })
            });
            workDays._fetch({
                url : "test.s001005.json",
                customApi : true,
                protocolCode : "SVC001005",
                data : {
                    "merchantID": merchantID,
                    "svcID": Adapter.getParameterByName("serviceid")
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
        }

    });

    var app = new AppView();
})();