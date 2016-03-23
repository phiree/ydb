/**
 * 订单指派员工插件，模拟backbone的MVC方式实现。
 * @template AdminBusiness\DZOrder\Default.aspx#staffs_template
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
    var urlConfig = "http://localhost:806/dianzhuapi.ashx";

    /**
     * 员工model
     * @param attribute
     * @param options
     * @constructor
     */
    var StaffModel = function(attribute, options){
        options || (options = {});
        this.url = options.url || urlConfig;
        this.attribute = attribute || {}
    };

    $.extend(StaffModel.prototype,{
        initialize : function(){

        }
    });

    /**
     * 员工model Collection
     * @param models
     * @param options
     * @constructor
     */
    var StaffCollection = function(models, options){
        options || (options = {});
        this.model = options.model || StaffModel;
        this.url = options.url ;
        this.reqObj = {
            storeID : options.storeID ? options.storeID : Adapter.getParameterByName("businessId"),
            merchantID : options.merchantID || merchantID
        };
        this.reqData = Adapter.reqPackage("ASN001006", this.reqObj);
        this.models = models || [];

        this.initialize();
    };


    $.extend(StaffCollection.prototype, {
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

    var staffCollection = new StaffCollection([], {
        url : urlConfig,
        model : StaffModel
    });

    /**
     * 指派数据Model
     * @param attribute
     * @param options
     * @constructor
     */
    var AssignModel = function(attribute, options){
        options || (options = {});
        this.url = options.url || urlConfig;
        this.attribute = attribute || {}
    };

    $.extend(AssignModel.prototype, {
        sync : function(){

        }
    });

    var AssignCollection = function(models, options){
        options || (options = {});
        this.model = options.model || StaffModel;
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
        sync : function(option, method, callback){
            var _this = this;
            var options = option || {};
            var setApiCode = this.apiCode[method];
            var reqData = Adapter.reqPackage(setApiCode, options.reqData );

            return $.ajax({
                type : "post",
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
        url : urlConfig,
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
        this.staffs = [];
        this.assign = [];
    };

    $.extend(ItemCollection.prototype, {
        initialize : function(){

        },

        sync : function(options, callback){
            var _this = this;
            var staffSync;
            var assignSync;
            var options = options || {};

            staffSync = staffCollection.sync();

            assignSync = assignCollection.sync({
                reqData : options.reqData
            }, "select");

            // 用when方法来延迟执行
            $.when(staffSync, assignSync).done(function(argStaff, argAssign){
                if (argStaff[1] === "success" && argAssign[1] === "success" ){
                    _this.staffs = staffCollection.models;
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
            var staffs = this.staffs;
            var assign = this.assign;
            // 标志员工model指派状态
            _this.models = [];
            //遍历员工，通过员工Id查询指派状态
            $.each(staffs, function(index, model){
                var _staffModel = model;
                var curAttr = attr[index] = _staffModel.attribute;
                var marked = false;

                // 遍历指派状态，绑定员工状态
                $.each(assign, function(index, model){
                    if (model.attribute.userID === _staffModel.attribute.userID){
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
                assignObj.userID = model.attribute.userID;
                assignObj.mark = model.attribute.mark;
                assignObj.orderID = _this.attribute.reqData.orderID;
                _this.assignAarray[index] = assignObj;
            })
        },
        _assignSync : function(callback){
            var _this = this;
            var reqData = {};

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
        this.model = options.model;
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
                        template : "#staffs_template"
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
        container : "#staffsContainer",
        trigger : '[data-role="appointSubmit"]'
    });

    $(document).on('click.appoint', '[data-role="appointToggle"]', function (e) {
        var storeID = Adapter.getParameterByName("businessId");
        var orderID = $(this).attr("data-appointTargetId");
        var appModel = new AppModel({
            url : urlConfig,
            reqData : {
                storeID : storeID,
                orderID : orderID,
                merchantID : merchantID
            }
        });

        e.preventDefault();
        $("#staffAppointLight").lightbox_me({
            centered : true
        });

        appView.model = appModel;
        appView.initialize().refresh();
    });

})();