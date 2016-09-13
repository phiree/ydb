/**
 * appointToOrder.js v1.0.0 @ 2016-05-17 by licdream@126.com
 * 订单指派员工插件，模拟backbone的MVC方式实现。
 * @template AdminBusiness\DZOrder\Default.aspx#staffs_template
 * @depends interfaceAdapter.js
 */

(function () {

    /**
     * underscore.js 模板设置修改
     * @type {{interpolate: RegExp, escape: RegExp, evaluate: RegExp}}
     */
    var templateOptions = {
        interpolate: /\{\%=(.+?)\%\}/g,
        escape: /\{\%-(.+?)\%\}/g,
        evaluate: /\{\%(.+?)\%\}/g
    };

    // 全局获取用户ID;
    var merchantID = document.getElementById("merchantID").value;

    /**
     * 全局API url设置
     * @type {string}
     */
    //var globalApiUrl = document.getElementById("hiApiUrl").value;
    var globalApiUrl = "/AjaxService/RequestRestfulApi.ashx ";

    var restFulAdapter = window.RestfulProxyAdapter || {};

    var storeID = YDBan.url.getUrlParam("businessId");

    var orderID = YDBan.url.getUrlParam("orderId");


    /**
     * 员工model
     * @param attribute
     * @param options
     * @constructor
     */
    var StaffModel = function (attribute, options) {
        options || (options = {});
        this.url = options.url || globalApiUrl;
        this.attribute = attribute || {}
    };

    $.extend(StaffModel.prototype, {
        initialize: function () {

        }
    });

    /**
     * 员工model Collection
     * @param models
     * @param options
     * @constructor
     */
    var StaffCollection = function (models, options) {
        options || (options = {});
        this.model = options.model || StaffModel;
        this.models = models || [];
        this.url = options.url;
        this.reqObj = {
            storeID: options.storeID ? options.storeID : storeID,
            merchantID: options.merchantID || merchantID
        };
        this.reqData = Adapter.reqPackage("ASN001006", this.reqObj);

        this.initialize();
    };

    $.extend(StaffCollection.prototype, {
        initialize: function () {

        },
        /**
         * 同步服务端数据
         * @param option
         * @param callback
         * @returns {*}
         */
        fetch: function (option) {
            var _this = this,
                success = option.success;

            option = option ? $.extend({}, option) : {};

            option.url = option.url || this.url;

            option.success = function (resp) {
                // 同步成功， 重置model.
                _this.reset(resp);

                if ( success ) { success( this, resp ); }
            };

            return restFulAdapter.sync("read", this, option);
        },
        reset: function (arrayData) {
            this.models = [];
            for (var i = 0; i < arrayData.length; i++) {
                this.models.push(new this.model(arrayData[i]));
            }
        }
    });

    var staffCollection = new StaffCollection([], {
        url: globalApiUrl,
        model: StaffModel
    });


    ///**
    // * 订单model
    // * @param attribute
    // * @param options
    // * @constructor
    // */
    //var OrderModel = function (attribute, options) {
    //    options || (options = {});
    //    this.url = options.url || globalApiUrl;
    //    this.attribute = attribute || {}
    //};
    //
    //$.extend(OrderModel.prototype, {
    //    initialize: function () {
    //
    //    },
    //    /**
    //     * 同步服务端数据
    //     * @param option
    //     * @param callback
    //     */
    //    fetch: function (option, callback) {
    //        var _this = this;
    //
    //        option = option ? $.extend({}, option) : {};
    //
    //        option.url = option.url || this.url;
    //
    //        option.success = function (row) {
    //
    //            _this.reset(row);
    //
    //            if ( typeof callback === "function" ) {
    //                callback(_this.attribute);
    //            }
    //        };
    //
    //        return restFulAdapter.sync("read", this, option);
    //    },
    //    reset : function (attribute) {
    //        if (typeof attribute !== "undefined"){
    //            this.attribute = $.extend(this.attribute, attribute);
    //        }
    //    }
    //});
    //
    //var orderModel = new OrderModel({}, {
    //    url: globalApiUrl
    //});
    //
    ///**
    // * 订单model Collection
    // * @param models
    // * @param options
    // * @constructor
    // */
    //var OrderCollection = function (models, options) {
    //    options || (options = {});
    //    this.model = options.model || OrderModel;
    //    this.models = models || [];
    //    this.url = options.url;
    //    //this.reqObj = {
    //    //    userID : options.merchantID ? options.merchantID : merchantID,
    //    //    target : "ALL",
    //    //    pageSize : "999",
    //    //    pageNum : "1"
    //    //};
    //    this.reqObj = {
    //        assign: false,
    //        pageSize: "999",
    //        pageNum: "1"
    //    };
    //    //this.reqData = Adapter.reqPackage("ORM001006", this.reqObj);
    //    // restful api method
    //    this.reqData = this.reqObj;
    //
    //    this.initialize();
    //};
    //
    //$.extend(OrderCollection.prototype, {
    //    initialize: function () {
    //
    //    },
    //    /**
    //     * 同步服务端数据
    //     * @param option
    //     * @param callback
    //     */
    //    fetch: function (option, callback) {
    //        var _this = this;
    //        var models = this.models;
    //
    //        option = option ? $.extend({}, option) : {};
    //
    //        option.success = function (row) {
    //            _this.reset(row);
    //            if ( typeof callback === "function" ) {
    //                callback(_this.models);
    //            }
    //            _this.reset(row);
    //        };
    //
    //        return restFulAdapter.sync('read', models, option);
    //    },
    //    create : function (key, value, option) {
    //        var attrs, attr = this.attribute;
    //
    //        return restFulAdapter.sync('create', attrs, option);
    //    },
    //    patch : function(key, value, option){
    //        var attrs ;
    //
    //        if ( key == null || typeof key === "object"){
    //            attrs = key;
    //            option = value;
    //        }
    //
    //
    //        return restFulAdapter.sync('patch', attrs, option);
    //
    //    },
    //    reset: function (arrayData) {
    //        this.models = [];
    //        for (var i = 0; i < arrayData.length; i++) {
    //            this.models.push(new this.model(arrayData[i]));
    //        }
    //    }
    //});

    //var orderCollection = new OrderCollection([], {
    //    url: globalApiUrl,
    //    //url : "/order.json",
    //    model: OrderModel
    //});

    /**
     * 订单model
     * @param attribute
     * @param options
     * @constructor
     */
    var FormanModel = function (attribute, options) {
        options || (options = {});
        this.url = options.url || globalApiUrl;
        this.attribute = attribute || {}
    };

    $.extend(FormanModel.prototype, {
        initialize: function () {

        },
        /**
         * 同步服务端数据
         * @param option
         * @param callback
         */
        fetch: function (option) {
            var _this = this;
            var success = option.success;

            option = option ? $.extend({}, option) : {};

            option.url = option.url || this.url;

            option.success = function (resp) {

                _this.reset(resp);

                if ( success ) { success(_this, resp) }
            };

            return restFulAdapter.sync("read", this, option);
        },
        patch : function (key, val, option) {
            var _this = this, attrs, success = option.success;

            if ( key == null || typeof key === "object"){
                attrs = key;
                option = val;
            } else  {
                (attrs = {})[key] = val;
            }


            option = option ? $.extend({}, option) : {};

            option.url = option.url || this.url;


            $.extend(this.attribute, attrs);


            option.success = function (resp) {

                _this.reset(resp);

                if ( success ) { success(_this, resp) }
            };

            return restFulAdapter.sync("patch", this, option);
        },
        reset : function (attribute) {
            if (typeof attribute !== "undefined"){
                this.attribute = $.extend(this.attribute, attribute);
            }
        }
    });

    var formanModel = new FormanModel({}, {
        url: globalApiUrl
    });


    /**
     * 自定义item model来保证构造DOM View的Model的单一性.
     * @param attribute
     * @param options
     */
    var ItemModel = function (attribute, options) {
        options || ( options = {});
        this.attribute = attribute || {};
        this.staff = {};
        //this.forman = {};
        //this.order = {};

        this.initialize(options);
    };

    $.extend(ItemModel.prototype, {
        initialize : function(){

        },
        selectItem: function () {
            if (this.attribute.mark){
                return
            }
            itemCollection.selectSingle(this.attribute.id);
        }
    });

    var ItemCollection = function (models, options) {
        options || (options = {});
        this.model = options.model || ItemModel;
        this.models = models || [];
        this.staffs = [];
        this.order = {};
        this.forman = {};
    };

    $.extend(ItemCollection.prototype, {
        initialize: function () {

        },
        fetch: function (options) {
            var _this = this;
            var staffSync,orderSync,formanSync;
            var option = options || {};

            staffSync = staffCollection.fetch({
                apiurl : "http://dev.ydban.cn:8041/api/v1/stores/" + storeID + "/staffs"
            });

            formanSync = formanModel.fetch({
                apiurl : "http://dev.ydban.cn:8041/api/v1/orders/"+ orderID +"/forman"
            });

            // 用when方法来延迟执行,等待员工和订单都已同步数据
            $.when(staffSync, formanSync)
                .done(function (argStaff, argAssign) {
                    if ( argStaff[1] === "success" && argAssign[1] === "success" ) {

                        _this.staffs = staffCollection.models;
                        _this.forman = formanModel;

                        // 重置collections
                        _this.reset();
                    } else {
                        throw new Error("date request error")
                    }

                    option.success && option.success(_this);
                })
                .fail(function () {
                    option.error && option.error()
                });
        },
        reset: function () {
            var _this = this;

            this.models = [];

            //遍历员工，通过员工Id查询指派状态
            $.each(this.staffs, function (index, model) {

                var newItemModel;

                model.attribute.mark = (model.attribute.id === _this.forman.attribute.id);

                newItemModel = new _this.model(model.attribute);

                newItemModel.staff = model;

                _this.models.push(newItemModel);
            })
        },
        // 单选一个员工
        selectSingle: function(id){
            var _this = this;
            $.each(this.models , function(index, model){
                if ( model.attribute.id === id ){
                    model.attribute.mark = true;
                    _this.forman = model;
                } else {
                    model.attribute.mark && (model.attribute.mark = false);
                }
            })
        },
        getFormanID : function(){
            return this.forman.attribute.id;
        }
    });

    var itemCollection = new ItemCollection();

    var ItemView = function (options) {
        this.model = options.model;
        this.template = options.template;
        this.templateDOM = _.template($(this.template).html(), templateOptions);
        this.el = null;
        this.$el = this.el ? $(this.el) : null;

        this.initialize();
    };

    $.extend(ItemView.prototype, {
        initialize: function () {

        },
        _delegate: function () {
            var _this = this;
            this.$el.find('[data-role="item"]').bind("click", function () {
                _this.model.selectItem();
                appView.refreshItems();
            })
        },
        render: function () {
            var refreshView = this.templateDOM(this.model.attribute);
            this.el = refreshView;
            this.$el = $(refreshView);
            // delegate event when render DOM.渲染的时候，重新委派事件
            this._delegate();
            return this;
        }
    });


    /**
     * 构建AppModel来包含创建一次指派所需数据和方法。
     * @param attribute
     * @param options
     * @constructor
     */
    var AppModel = function (attribute, options) {
        options || (options = {});
        this.attribute = attribute || {};
        this.assignAarray = [];
        this.formanID = "";
        this.oldFormanID  = "";


        // 用于请求的相关数据
        //this.reqData = attribute.reqData || {};

        this.initialize(options);
    };

    $.extend(AppModel.prototype, {
        initialize : function(options){

        },
        // 指派员工列表构建
        _buildAssignList: function () {
            var _this = this;

            this.oldFormanID = this.formanID;

            this.formanID = itemCollection.getFormanID();

        },
        // 上传同步指派信息
        _assignUpdate: function (option) {

            this._buildAssignList();

            if (this.oldFormanID === this.formanID ){
                option.warn("员工已指派该订单");
                return;
            }

            formanModel.patch("staffID", this.formanID, {
                apiurl : "http://dev.ydban.cn:8041/api/v1/orders/"+ orderID +"/forman",
                success :　option.success,
                error : option.error
            });

        }
    });

    var AppView = function (options) {
        this.model = options.model;
        this.$container = $(options.container || "body");
        this.$trigger = $(options.trigger || '[data-role="appointSubmit"]');
        this.url = options.url;
    };

    $.extend(AppView.prototype, {
        initialize: function () {
            this._delegate();
            return this;
        },
        reloadItems: function(){
            var _this = this;

            this.$container.children().remove();

            this.showLoading(true);

            itemCollection.fetch({
                success: function (items) {
                    _this.model.formanID = items.forman.attribute.id || "";
                    _this.showLoading(false);
                    _this.refreshItems();
                },
                error: function () {
                    _this.showError();
                }
            });
        },
        // 刷新指派员工列表
        refreshItems: function (models) {
            var _this = this;

            this.$container.children().remove();

            $.each(itemCollection.models, function (index, model) {

                var $itemView = new ItemView({
                    model: model,
                    template: "#staffs_template"
                }).render().$el;

                _this.$container.append($itemView);
            })

        },
        showLoading : function(toggle){
            if ( toggle ){
                this.$container.addClass("loading");
            } else {
                this.$container.removeClass("loading");
            }
        },
        showError : function(){
            this.$container.removeClass("loading");
            this.$container.addClass("error");
        },
        // 绑定指派事件
        _delegate: function () {
            var _this = this;

            this.$trigger.off('click').on('click', handler);

            function handler () {
                _this.model._assignUpdate({
                    success :　function () {
                        alert("指派成功");
                        // TODO: 暂时采用这样的方式关闭窗口
                        $(".lightClose").click();
                    },
                    warn : function(warnText){
                        alert(warnText);
                    }
                });
            }
        }
    });

    var appView = new AppView({
        container: "#staffsContainer",
        trigger: '[data-role="appointSubmit"]'
    });

    $(document).on('click.appoint', '[data-role="appointToggle"]', function (e) {
        var appModel = new AppModel({
            url: globalApiUrl
        });

        e.preventDefault();
        $("#staffAppointLight").lightbox_me({
            centered: true
        });

        appView.model = appModel;
        appView.initialize().reloadItems();
    });

})();
