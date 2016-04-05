/*!
 a tool to appoint by ajax.
 */

/* standard by AMD , for the future */
(function (factory) {
    if (typeof define === 'function' && define.amd) {
        defined(['jquery', 'Adapter'], factory);
    } else {
        factory($);
    }
}(function ($) {
    var pluginName = 'appoint';

    /**
     * @description Appoint constructor
     * @param element
     * @param options
     * @constructor
     */
    function Appoint(element, options) {
        this.$element = $(element);
        this.options = $.extend({}, Appoint.DEFAULTS, options);
        this.$appointSubmit = $(this.options.appointSubmit);
        this.$container = $(this.options.container);
        this.appointTargetId = this.$element.attr('data-appointTargetId');
    }

    /* defaults */
    Appoint.DEFAULTS = {
        /* 包含item的容器 */
        container: null,

        /* item使用的模版引擎，目前仅限制为underscore的模版 */
        template: null,

        /* 单个指派设置 */
        single: true,

        /* 再刷新item前的前置函数 */
        beforePullFunc: null,

        /* 刷新item时请求的的url和data */
        pullUrl : null,
        pullReqData: {},

        /* 上传数据的url */
        uploadUrl : null,

        /* 对要上传的数据进行扩展 */
        uploadPreFixData : null,

        /* 指派提交按钮 */
        appointSubmit: null,

        /* 指派成功成功后的回调函数 */
        appointSucFunc: null,

        /* 可指定的提交json数据属性名，itemName为选择元素id的属性名，targetName为目标id的属性名 */
        itemName : 'item',
        targetName : 'target'
    };

    $.extend(Appoint.prototype, {
        /**
         *
         *
         */
        toggle: function () {
            var beforePullFunc = this.options.beforePullFunc;

            this.$appointSubmit.attr('data-appointTargetId', this.appointTargetId);

            /* 预处理函数 */
            if ( typeof beforePullFunc === 'function' && beforePullFunc ) {
                beforePullFunc();
            }

            this.pull();

        },

        pull : function (){
            var _this = this;
            var pullData = this.options.pullReqData ? this.options.pullReqData : "";

            $.ajax({
                type : "post",
                url : _this.options.pullUrl,
                data : pullData,
                success : function(row, textStatus, xhr){
                    var data = Adapter.respUnpack(row);
                    _this.refresh(data.respData);
                    _this.itemsBuild();
                },
                error : function(XMLHttpRequest, textStatus, errorThrown){
                    console.log(XMLHttpRequest);
                    console.log(textStatus);
                    console.log(errorThrown);
                }
            });
        },

        /**
         *
         * @param jsonData
         */
        refresh : function(jsonData){
            /* 在每次指派item前，通过ajax获取新的item状态. */
            /* 通过underscore.js模版引擎来刷新页面视图,自定义引用符号为"{% code %}",以免与asp.net的引用符号冲突 */
            var $template = $(this.options.template);
            var template = _.template($template.html() ,{
                interpolate: /\{%=(.+?)%\}/g,
                escape:      /\{%-(.+?)%\}/g,
                evaluate:    /\{%(.+?)%\}/g
            });

            this.$container.html(template(jsonData));
        },
        /**
         * items构造
         */
        itemsBuild : function(){
            if ( !this.options.single ) {
                return;
            }
            /* 单选时对checkBox进行操作 */
            var items = this.$container.find('[data-role="item"]');

            items.bind('click.appoint', function(e){
                var target = e.target;
                items.not(target).removeAttr("checked");
            });
        },
        appoint: function () {
            var checkItems = this.$container.find('[data-role="item"]:checked');
            var checkId = [];
            var appointJSON = {};

            if ( !checkItems.length ) { return false; }

            /* 如果设置为单选时，检测check的长度 */
            if ( this.options.single && checkItems.length > 1 ) { return false; }

            checkItems.each(function(){
                checkId.push($(this).attr('data-itemId'));
            });

            appointJSON = this._appointJSONBuild(checkId);

            this._upload(appointJSON);
        },
        /**
         * 构造指派json
         * @param checkId
         * @returns {{}}
         * @private
         */
        _appointJSONBuild : function(checkId){
            var _this = this, appointJSON = {} ;
            var arrayData = [];

            // TODO： 脱离不稳定的businessId获取到option内
            if ( !!Adapter.getParameterByName("businessId") ){
                //appointJSON.storeID = Adapter.getParameterByName("businessId");
                appointJSON.storeID = "e2f4fb71-04fc-43d7-a255-a5af00ae5705";
            }

            for ( var i = 0; i < checkId.length; i++ ){
                var obj = {};
                /* 通过自定义checkName和targetName定义被指派的目标，实现双向指派 */
                obj[_this.options.itemName] = checkId[i];
                obj[_this.options.targetName] = _this.appointTargetId;
                obj.mark = "Y";
                arrayData.push(obj);
            }

            appointJSON.arrayData = arrayData;

            return appointJSON;
        },
        /**
         * 封装指派信息并提交
         * @param jsonData
         * @private
         */
        _upload: function (jsonData) {
            var _this = this,
                reqString,
                preFix = _this.options.uploadPreFixData,
                success = _this.options.appointSucFunc;

            if ( typeof preFix === "function" ){
                reqString = preFix(jsonData);
            }

            $.ajax({
                type : "post",
                url : _this.options.uploadUrl,
                data : reqString,
                success : function(data, textStatus, xhr){
                    console.log(data);
                    var respObj = Adapter.respUnpack(data);
                    if ( respObj.respCorrect && typeof success === 'function' ){
                        success();
                    }
                },
                error : function(XMLHttpRequest, textStatus, errorThrown){

                }
            });
        }
    });

    $.fn[pluginName] = function (option) {
        return this.each(function () {
            var data = $.data(this, "appoint");
            var options = $.extend({}, Appoint.DEFAULTS, typeof option === 'object' && option);
            if (!data) {
                $.data(this, "appoint", data = new Appoint(this, options));
            }
            if (typeof option === 'string') {
                data[option]();
            }
        });
    };

    /* 绑定submit到触发开关，使其能在唯一一个元素上保存data数据 */
    function getToggleFromSubmit($submit) {
        return $('[data-role="appointToggle"][data-appointTargetId="' + $submit.attr('data-appointTargetId') + '"]');
    }

    $(document).on('click.appoint', '[data-role="appointToggle"]', function (e) {
        e.preventDefault();
        var $this = $(this);
        $.fn[pluginName].call($this, 'toggle');
    });

    $(document).on('click.appoint', '[data-role="appointSubmit"]', function (e) {
        e.preventDefault();
        var $this = $(this);
        var $toggle = getToggleFromSubmit($this);
        $.fn[pluginName].call($toggle, 'appoint');
    });
}));