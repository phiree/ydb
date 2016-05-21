/**
 * 员工指派订单插件，模拟backbone的MVC方式实现。
 * @template AdminBusiness\DZOrder\Default.aspx#orders_template
 * @depends interfaceAdapter.js
 */

(function (){

    /**
     * underscore.js 模板设置修改
     * @type {{interpolate: RegExp, escape: RegExp, evaluate: RegExp}}
     */
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

    /**
     * 订单model
     * @param attribute
     * @param options
     * @constructor
     */
    var OrderModel = function(attribute, options){
        options || (options = {});
        this.url = options.url || globalApiUrl;
        this.attribute = attribute || {}
    };

    $.extend(OrderModel.prototype,{
        initialize : function(){

        }
    });

    /**
     * 订单model Collection
     * @param models
     * @param options
     * @constructor
     */
    var OrderCollection = function(models, options){
        options || (options = {});
        this.model = options.model || OrderModel;
        this.url = options.url ;
        this.reqObj = {
            userID : options.merchantID ? options.merchantID : merchantID,
            target : "ALL",
            pageSize : "999",
            pageNum : "1"
        };
        this.reqData = Adapter.reqPackage("ORM001006", this.reqObj);
        this.models = models || [];

        this.initialize();
    };


    $.extend(OrderCollection.prototype, {
        initialize : function(){

        },
        /**
         * 同步服务端数据
         * @param callback
         * @returns {*}
         */
        sync : function(callback){
            var _this = this;

            return $.ajax({
                type : "post",
                dataType : "json",
                url : _this.url,
                data : _this.reqData,
                success : function(row, textStatus, xhr){
                    var data = Adapter.respUnpack(row);

                    if ( data.hasArrayData ) {
                        _this.reset(data.respData.arrayData);
                    }

                    if ( typeof callback === "function") {
                        callback(_this.models);
                    }
                },
                error : function(XMLHttpRequest, textStatus, errorThrown){
                    console.log(XMLHttpRequest);
                    console.log(textStatus);
                    console.log(errorThrown);
                }
            });

        },
        reset : function(arrayData){
            this.models = [];
            for( var i = 0 ; i < arrayData.length ; i++ ){
                this.models.push(new this.model(arrayData[i]));
            }
        }
    });

    var orderCollection = new OrderCollection([], {
        url : globalApiUrl,
        //url : "/order.json",
        model : OrderModel
    });

    /**
     * 指派数据Model
     * @param attribute
     * @param options
     * @constructor
     */
    var AssignModel = function(attribute, options){
        options || (options = {});
        this.url = options.url || globalApiUrl;
        this.attribute = attribute || {}
    };

    $.extend(AssignModel.prototype, {
        sync : function(){

        }
    });

    var AssignCollection = function(models, options){
        options || (options = {});
        this.model = options.model || OrderModel;
        this.url = options.url ;
        this.models = models || [];
        this.apiCode = {
            select : "ASN002004",
            update : "ASN002001"
        }
    };

    $.extend(AssignCollection.prototype, {
        initialize : function(){

        },
        sync : function(options, method, callback){
            var _this = this;
            var options = options || {};
            var setApiCode = this.apiCode[method];
            var reqData = Adapter.reqPackage(setApiCode, options.reqData );

            return $.ajax({
                type : "post",
                dataType : "json",
                url : _this.url,
                data : reqData,
                success : function(row, textStatus, xhr){
                    var data = Adapter.respUnpack(row);

                    if ( data.hasArrayData ) {
                        _this.reset(data.respData.arrayData);
                    }

                    if ( typeof callback === "function") {
                        callback(_this.models);
                    }
                },
                error : function(XMLHttpRequest, textStatus, errorThrown){
                    console.log(XMLHttpRequest);
                    console.log(textStatus);
                    console.log(errorThrown);
                }
            });

        },
        reset : function(arrayData){
            this.models = [];
            for( var i = 0 ; i < arrayData.length ; i++ ){
                this.models.push(new this.model(arrayData[i]));
            }
        }
    });

    var assignCollection = new AssignCollection([], {
        url : globalApiUrl,
        //url : "/assign.json",
        model : AssignModel
    });

    /**
     * 自定义item model来保证构造DOM View的Model的单一性.
     * @param attribute
     * @param options
     */
    var ItemModel = function(attribute, options){
        options || (options = {});
        this.attribute = attribute || {}
    };

    $.extend(ItemModel.prototype, {
        changeMark : function(){
            switch ( this.attribute.mark ) {
                case "Y" :
                    this.attribute.mark = "N";
                    break;
                case "N" :
                    this.attribute.mark = "Y";
                    break;
            }
        }
    });

    var ItemCollection = function(models, options){
        options || (options = {});
        this.model = options.model || ItemModel;
        this.models = [];
        this.orders = [];
        this.assign = [];
    };

    $.extend(ItemCollection.prototype, {
        initialize : function(){

        },

        sync : function(options, callback){
            var _this = this;
            var orderSync;
            var assignSync;
            var options = options || {};

            orderSync = orderCollection.sync();

            assignSync = assignCollection.sync({
                reqData : options.reqData
            }, "select");

            // 用when方法来延迟执行
            $.when(orderSync, assignSync).done(function(argOrder, argAssign){
                if (argOrder[1] === "success" && argAssign[1] === "success" ){
                    _this.orders = orderCollection.models;
                    _this.assign = assignCollection.models;
                    _this.reset();
                } else {
                    throw new Error("date request error")
                }
                callback && callback(_this.models);
            });
        },
        reset : function(){
            var _this = this;
            var attr = [];
            var orders = this.orders;
            var assign = this.assign;
            // 标志订单model指派状态
            _this.models = [];
            //遍历订单，通过订单Id查询指派状态
            $.each(orders, function(index, model){
                var _orderModel = model;
                var curAttr = attr[index] = _orderModel.attribute;
                var marked = false;

                // 遍历指派状态，绑定订单状态
                $.each(assign, function(index, model){
                    if (model.attribute.orderID === _orderModel.attribute.orderID){
                        marked = true;
                        curAttr.mark = model.attribute.mark;
                    }
                });

                if ( !marked ){
                    curAttr.mark = "N";
                }
                _this.models.push(new _this.model(curAttr));
            })
        }
    });

    var itemCollection = new ItemCollection();

    var ItemView = function(options){
        this.model = options.model;
        this.template = options.template;
        this.templateDOM = _.template($(this.template).html(), templateOptions);
        this.el = null;
        this.$el = this.el ? $(this.el) : null;

        this.initialize();
    };

    $.extend(ItemView.prototype, {
        initialize : function(){

        },
        _delegate : function(){
            var _this = this;
            this.$el.find('[data-role="item"]').bind("click", function () {
                _this.model.changeMark();
            })
        },
        render : function(){
            var refreshView = this.templateDOM(this.model.attribute);
            this.el = refreshView;
            this.$el = $(refreshView);
            // delegate event when render DOM.
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
    var AppModel = function(attribute, options){
        options || (options = {});
        this.attribute = attribute || {};
        this.assignAarray = [];
        this.reqData = attribute.reqData || {}
    };

    $.extend(AppModel.prototype, {
        _assignBuild : function(){
            var _this = this;
            $.each(itemCollection.models, function(index, model){
                var assignObj = {};
                assignObj.userID = _this.attribute.reqData.userID;
                assignObj.mark = model.attribute.mark;
                assignObj.orderID = model.attribute.orderID;
                _this.assignAarray[index] = assignObj;
            })
        },
        _assignSync : function(callback){
            var _this = this;
            var reqData = {};

            // 指派查询请求数据构建
            reqData.merchantID = this.reqData.merchantID;
            reqData.storeID = this.reqData.storeID;
            reqData.arrayData = this.assignAarray;
            reqData = Adapter.reqPackage("ASN002001", reqData);

            var assignSync = $.ajax({
                type : "post",
                url : _this.attribute.url,
                data : reqData,
                success : function(row, textStatus, xhr){
                    var data = Adapter.respUnpack(row);

                    if ( data.hasArrayData ) {
                        _this.reset(data.respData.arrayData);
                    }

                    if ( typeof callback === "function") {
                        callback();
                    }
                },
                error : function(XMLHttpRequest, textStatus, errorThrown){
                    console.log(XMLHttpRequest);
                    console.log(textStatus);
                    console.log(errorThrown);
                }
            })
        }
    });

    var AppView = function(options){
        this.model = options.model,
        this.$container =  $(options.container || "body");
        this.$trigger = $(options.trigger || '[data-role="appointSubmit"]');
        this.url = options.url ;
    };

    $.extend(AppView.prototype, {
        initialize :function(){
            this._delegate();
            return this;
        },
        refresh :function(){
            var _this = this;
            var reqData = this.model.reqData;

            _this.$container.children().remove();
            itemCollection.sync({
                reqData : reqData
            }, function(models){
                $.each(models, function(index, model){
                    var $itemView = new ItemView({
                        model : model,
                        template : "#orders_template"
                    }).render().$el;

                    _this.$container.append($itemView);
                })
            });
        },
        _delegate : function(){
            var _this = this;
            this.$trigger.one('click', function(){
                _this.model._assignBuild();
                _this.model._assignSync(function(){
                    alert("指派成功")
                });
            });
        }
    });

    var appView = new AppView({
        container : "#ordersContainer",
        trigger : '[data-role="appointSubmit"]'
    });

    $(document).on('click.appoint', '[data-role="appointToggle"]', function (e) {
        var storeID = Adapter.getParameterByName("businessId");
        //var orderID = $(this).attr("data-appointTargetId");
        var userID = $(this).attr("data-appointTargetId");
        //var userID = "6F9619FF-8B86-D011-B42D-00C04FC964FF";
        var appModel = new AppModel({
            url : globalApiUrl,
            reqData : {
                storeID : storeID,
                userID : userID,
                merchantID : merchantID
            }
        });

        e.preventDefault();
        $("#orderAppointLight").lightbox_me({
            centered : true
        });

        appView.model = appModel;
        appView.initialize().refresh();
    });

})();