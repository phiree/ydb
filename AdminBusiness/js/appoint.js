/*
 * a tool to appoint by ajax.
 * */

/* standard by AMD , for the future */
(function (factory) {
    if (typeof define === 'function' && define.amd) {
        defined(['jquery'], factory);
    } else {
        factory($);
    }
}(function ($) {
    var pluginName = 'appoint';

    /* constructor */
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

        /* 刷新item时请求的ajax data */
        pullReqData: {},

        /* 刷新item时请求的的url */
        pullUrl : null,

        /* 上次数据的url */
        uploadUrl : null,

        /* 可是设置json对要upload数据进行预处理 */
        uploadPreFixData : {},

        /* 指派提交按钮 */
        appointSubmit: null,

        /* 指派成功成功后的回调函数 */
        appointSucFunc: null,

        /* 可指定的提交json数据属性名，checkItemName为选择元素id的属性名，appointTargetName为目标id的属性名 */
        checkItemName : 'item',
        appointTargetName : 'target'
    };

    $.extend(Appoint.prototype, {
        toggle: function () {
            var beforePullFunc = this.options.beforePullFunc;

            this.$appointSubmit.attr('data-appointTargetId', this.appointTargetId);

            /* 预处理函数 */
            if ( typeof beforePullFunc === 'function' && beforePullFunc ) {
                beforePullFunc();
            }

            this.pull();

        },
        pull : function (jsonData){
            var _this = this;
            var pullData = $.extend( jsonData, typeof this.options.pullReqData === 'object' && this.options.pullReqData );
            $.ajax({
                url : _this.options.pullUrl,
                dataType : 'json',
                data : pullData,
                success : function(data, textStatus, xhr){
                    _this.refresh(data);
                    _this.itemsBuild();
                }
            });
        },
        /* 在每次指派item前，通过ajax获取新的item状态
         * 通过underscore.js模版引擎来刷新页面视图,自定义引用符号为"{% code %}",以免与asp.net的引用符号冲突
         * */
        refresh : function(jsonData){
            var $template = $(this.options.template);
            var template = _.template($template.html() ,{
                interpolate: /\{%=(.+?)%\}/g,
                escape:      /\{%-(.+?)%\}/g,
                evaluate:    /\{%(.+?)%\}/g
            });

            this.$container.html(template(jsonData));
        },
        /* items构造 */
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
            var checkId;
            var appointJSON = {};

            if ( !checkItems.length ) { return false; }

            /* 如果设置为单选时，检测check的长度 */
            if ( this.options.single && checkItems.length > 1 ) { return false; }
            if ( !this.options.single ) {
                checkId = [];
                for (var i = 0; i < checkItems.length; i++) {
                    checkId.push(this.attr('data-itemId'));
                }
            } else {
                checkId = checkItems.eq(0).attr('data-itemId');
            }

            appointJSON = this._appointJSONBuild(checkId);
            this._upload(appointJSON);
        },
        _appointJSONBuild : function(checkId){
            var _this = this;
            var appointJSON = {} ;

            appointJSON.single = _this.options.single;

            /* 通过自定义的变量名指定appointJSON数据 */
            appointJSON[_this.options.checkItemName] = checkId;
            appointJSON[_this.options.appointTargetName] = _this.appointTargetId;

            /* 合并预处理json数据 */
            appointJSON = $.extend( _this.options.uploadPreFixData, appointJSON);

            return appointJSON;
        },
        _upload: function (jsonData) {
            var _this = this;
            $.ajax({
                url : _this.options.uploadUrl,
                dataType : 'json',
                data : jsonData,
                success : function(data, textStatus, xhr){
                    if ( typeof _this.options.appointSucFunc === 'function' ){
                        _this.options.appointSucFunc();
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