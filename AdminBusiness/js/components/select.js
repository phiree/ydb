/**
 * select.js v1.0.0 @ 2016-05-17 by licdream@126.com
 * custom select plugin.
 */
(function (factory) {
    if ( typeof define === "function" && define.amd ) {
        define(['jquery'], function ($) {
            factory($);
        });
    } else {
        factory(jQuery);
    }
}(function ($) {
    var pluginName = "customSelect";

    var Select = (function () {

        function _Select(element, options) {
            this.$el = $(element);
            this.type = typeof this.$el.attr("data-type") === "string" && this.$el.attr("data-type");
            this.optoins = $.extend({}, _Select.DEFAULTS, options);

            this._buildDOM();
            this._delegate();
        }

        _Select.DEFAULTS = {};

        _Select.prototype._buildDOM = function () {
            var _this = this,citeText;

            // 如容器内已有ul则使用ul里的li,a元素
            if ( this.$el.find("ul").length ){
                this.$list = this.$el.find("ul");
                this.$a = this.$list.find("a");
                this.$a.each(function(index){
                    // 这里有两次赋值，val赋值是为了jquery内容部程序访问得到，value属性赋值是为了a标签正确的设置value属性。可能jQuery这里有bug，暂时不深究。
                    $(this).val(index).attr({
                        value: index,
                        href: "javascript:void(0);"
                    })
                });
                // 如果容器没有ul，且设置了type则自动生成
            } else if ( this.type ) {
                buildTypeList.call(this, this.type);
            } else {
                return
            }

            this.$input = this.$el.find("input").length && this.$el.find("input");

            this.$cite = $("<cite></cite>");

            this.$el.prepend(this.$cite);

            // 初始化下拉列表标题cite的值
            // TODO: 逻辑可以优化一下
            if ( !this.$input.val() ) {
                // 如果input没有值,则input默认为第一个a标签的值
                this.$input.val(this.$a.eq(0).attr("value"));
                citeText = this.$a.eq(0).html();
            } else {
                // 如果input有值,则input筛选对应值的a标签的文本为cite的值
                var val = this.$input.val();
                var selectedA = this.$a.filter(function(index){
                    return $(this).val() == val;
                });

                // 如果找不到对应的a标签, 则默认使用第一个a标签的值
                if (selectedA.length){
                    citeText = selectedA.html();
                } else {
                    citeText = this.$a.eq(0).html();
                }
            }
            this.$cite.html(citeText);

        };

        function buildTypeList(type){
            this.$list = $("<ul></ul>");
            if ( type && typeof type === "string" ){
                var count,step;

                if (type === "year"){
                    count = 1980; step = -1;
                    for (var i = new Date().getFullYear(); i > count; i += step ) {
                        this.$list.append($("<li></li>").append($("<a></a>").html(i).val(i).attr({
                            value : i,
                            href: "javascript:void(0);"
                        })));
                    }
                } else {
                    switch ( type ){
                        case "hour" :
                            count = 25;
                            step = 1;
                            break;
                        case "minute" :
                            count = 60;
                            step = 5;
                            break;
                        case "num" :
                            count = 99;
                            step = 1;
                            break;
                        default :
                            break;
                    }

                    for (var i = 0; i < count; i += step ) {
                        this.$list.append($("<li></li>").append($("<a></a>").html(i).val(i).attr({
                            value : i,
                            href: "javascript:void(0);"
                        })));
                    }
                }

                this.$a = this.$list.find("a");

                this.$el.prepend(this.$list);
            }
        }

        _Select.prototype._delegate = function () {
            var _this = this;
            var mouseOut = true;

            this.$cite.on("click.customSelect", function () {
                if ( _this.$list.css("display") == "none" ) {
                    _this.$list.slideDown("fast");
                } else {
                    _this.$list.slideUp("fast");
                }
            });

            this.$a.on("click.customSelect", function () {
                var value = $(this).attr("value"),
                    text = $(this).text();
                _this.$cite.html(text);
                _this.$input.attr("value", value).focus().blur();
                _this.$list.hide();
            });

            this.$el.on("mouseover mouseout", function (e) {
                if ( e.type === "mouseover" ) {
                    mouseOut = false;
                } else if ( e.type === "mouseout" ) {
                    mouseOut = true;
                }
            });

            $(document).on("click.customSelect", function () {
                if ( mouseOut ) {
                    _this.$list.hide();
                }
            });

        };

        return _Select;
    })();

    $.fn[pluginName] = function (option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data("customSelect");
            var options = ( typeof option === "object" && option );

            if ( !data ) {
                $this.data("customSelect", ( data = new Select(this, options)))
            }
        });
    };

}));