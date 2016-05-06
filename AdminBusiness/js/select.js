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
            var _this = this;

            this.$list = buildList(this.type);
            this.$input = this.$el.find("input").length && this.$el.find("input");
            this.$a = this.$list.find("a");

            this.$cite = $("<cite></cite>");
            this.$cite.html(this.$a.eq(0).html());

            this.$el.prepend(this.$cite);

            for (var i = 0; i < this.$a.length; i++) {
                this.$a.eq(i).attr({
                    value: i,
                    href: "javascript:void(0);"
                });
            }

            // 初始化下拉列表的值
            if ( !this.$input.val() ) {
                this.$input.val(this.$a.eq(0).attr("value"));
            } else {
                this.$cite.html(this.$a.eq(this.$input.val()).html());
            }

            function buildList(type){
                var $list = $("<ul></ul>");
                if ( type && typeof type === "string" ){
                    switch ( type ){
                        case "num" :
                            for (var i = 0; i < 100; i++) {
                                $list.append("<li><a>" + i + "</a></li>");
                            };
                            break;
                        case "hour" :
                            for (var i = 0; i < 60; i++) {
                                $list.append("<li><a>" + i + "</a></li>");
                            };
                            break;
                        case "minute" :
                            for (var i = 0; i < 25; i++) {
                                $list.append("<li><a>" + i + "</a></li>");
                            };
                            break;
                        default :
                            for (var i = 0; i < 100; i++) {
                                $list.append("<li><a>" + i + "</a></li>");
                            };
                            break;
                    }
                    _this.$el.prepend($list);
                    return $list;
                }

                return _this.$el.find("ul").length && _this.$el.find("ul");

            }
        };

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