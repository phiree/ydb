/**
 * timepick.js v1.0.0 @ 2016-05-17 by licdream@126.com
 * 自定义时间选择控件
 */
(function(factory){
    if ( typeof define === "function" && define.amd ){
        define(["jquery"], function($){
            factory($);
        })
    } else {
        factory(jQuery);
    }
})(function($){
    var pluginName = "timePick";

    var TimePick = function(ele, options){
        this.$ele = $(ele);
        this.options = options;
        this.showing = false;
        this.hover = false;
        this.initialize();
    };


    TimePick.DEFAULTS = {
        wrap : "div",
        wrapClass : "time-pick-w",
        selectSize : 6,
        minuteStep : 5
    };

    function fix(num, length){
        return ('' + num).length < length ? ((new Array(length + 1)).join('0') + num).slice(-length) : '' + num;
    }

    $.extend(TimePick.prototype, {
        initialize : function(){
            var _this = this;
            this.buildWrap();

            if ( !this.$ele.val || this.$ele.val() === "" ){ this.$ele.val("00:00") };

            this.$ele.bind("keyup keydown", function(e){
                e.preventDefault();
            });

            this.$ele.bind('click.timePick', function(e){
                _this.togglePanel(e);
            });

        },
        buildWrap : function(){
            var $ele = this.$ele;
            var _this = this;
            $ele.wrap(function(){
                return $("<" + _this.options.wrap + ">").addClass(_this.options.wrapClass);
            });
        },
        buildPanel : function(){
            var _this = this;
            var $h_select = $('<ul class="tp-h-select" size=' + this.options.selectSize + '>'),
                $m_select = $('<ul class="tp-m-select" size=' + this.options.selectSize + '>'),
                $select_wrap = $('<div class="tp-wrap">');

            for ( var i = 0 ; i < 24 ; i++ ){
                var $option = $('<li class="tp-option" >');
                $option.val(fix(i, 2)).html(fix(i, 2));
                $option.on("click", function(e){
                    var ele = e.target;
                    _this.select(ele, { hour : true} )
                });
                $h_select.append($option);
            }

            for ( var i = 0 ; i < 60 ; i+= _this.options.minuteStep ){
                var $option = $('<li class="tp-option" >');
                $option.val(fix(i, 2)).html(fix(i, 2));
                $option.on("click", function(e){
                    var ele = e.target;
                    _this.select(ele, { minute : true} )
                });
                $m_select.append($option);
            }

            $select_wrap.append($h_select).append($m_select);

            this.$select = $select_wrap;
            $select_wrap.insertAfter(this.$ele);

            // 避免重复绑定事件
            $(document).trigger("click.timePick.doc");
            $(document).off("click.timePick.doc").on("click.timePick.doc", function(e){
                if ( _this.showing && !$(e.target).is(_this.$ele) &&  !isChildOrSelf($(e.target), _this.$select) ){
                    _this.closePanel();
                }
            });

            function isChildOrSelf($target, $ele){
                return ( $target.closest($ele).length > 0 );
            }

        },
        removePanel : function(){
            this.$select.remove();
        },
        togglePanel : function(e){
            if ( !this.showing && $(e.target).is(this.$ele)  ){
                this.openPanel();
            } else if ( this.showing ){
                this.closePanel();
            }
        },
        openPanel : function(){
            this.buildPanel();
            this.showing = true;
            this.$ele.addClass("showing");
        },
        closePanel : function () {
            this.removePanel();
            this.showing = false;
            this.$ele.removeClass("showing");
        },
        select : function(ele, option){
            var val = this.$ele.val();
            var slc = $(ele).val();

            if ( val == "" || !val ){
                val = "00:00"
            }

            if (option.hour){
                val = val.replace(/^\d{1,2}/, fix(slc, 2))
            }

            if (option.minute){
                val = val.replace(/\d{1,2}$/, fix(slc, 2))
            }

            this.$ele.val(val);
        }
    });

    $.fn[pluginName] = function(option){
        return this.each(function(){
            var data = $.data(this, "timePick");
            var options = $.extend({}, TimePick.DEFAULTS, typeof option === 'object' && option)

            if (!data) {
                $.data(this, "timePick", data = new TimePick(this, options));
            }

            if (typeof  option === "string") {
                data[option]();
            }
        });
    };


});