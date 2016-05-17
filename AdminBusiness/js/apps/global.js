/**
 * global.js v1.0.0 @2016/5/17
 * 全局使用函数
 * Created by LiChang on 2015/5/15.
 */
$(function () {
    // 页面最小高度设为100%
    (function ($, window, document){
        $(window).bind("resize", setFluid);
        $(document).ready(setFluid);
        function setFluid() {
            var narHeight = 79,
                windowHeight = $(window).height(),
                height = windowHeight - narHeight;
            $(".content-layout").css("min-height", (height) + "px");
            $(".content-layout-fluid").css("min-height", (height) + "px");
        }
    })(jQuery, window, document);

    // 应用metisMenu
    (function ($, window, document){
        var $menu = $("#menu");
        !($menu.length == 0) && $menu.metisMenu();
    })(jQuery, window, document);

    // 侧边栏滚动控制
    (function ($, window, document){
        var $sidebar = $('.sidebar');

        if ($sidebar.length == 0) return ;

        var sideTop = $sidebar.offset().top;

        if ($('body').scrollTop() >= sideTop) {
            $sidebar.addClass('sidebar-fixed');
        } else {
            $sidebar.removeClass('sidebar-fixed');
        }

        $(window).scroll(function () {
            if ($('body').scrollTop() >= sideTop) {
                $sidebar.addClass('sidebar-fixed');
            } else {
                $sidebar.removeClass('sidebar-fixed');
            }
        })

    })(jQuery, window, document);

    // 侧边栏链接样式控制
    (function ($, window, document){
        var url = window.location;
        var localUrl = urlFilter(url);

        var element = $('#menu').find("li").find("a").filter(function () {
            var thisHref = urlFilter(this.href);
            console.log(thisHref)
            return thisHref == localUrl || localUrl.indexOf(thisHref) == 0;
        });

        element.addClass('active').parent().addClass('in');

        function urlFilter(url) {
            var filter = /[a-zA-z]+:\/\/(\S+\.)/;
            if (filter.exec(url)) {
                return (filter.exec(url))[0].toLowerCase();
            }
        }
    })(jQuery, window, document);

});

(function ($) {
    var types = ['DOMMouseScroll', 'mousewheel'];
    $.event.special.mousewheel = {
        setup: function () {
            if (this.addEventListener) {
                for (var i = types.length; i;) {
                    this.addEventListener(types[--i], handler, false);
                }
            } else {
                this.onmousewheel = handler;
            }
        },
        teardown: function () {
            if (this.removeEventListener) {
                for (var i = types.length; i;) {
                    this.removeEventListener(types[--i], handler, false);
                }
            } else {
                this.onmousewheel = null;
            }
        }
    };
    $.fn.extend({
        mousewheel: function (fn) {
            return fn ? this.bind("mousewheel", fn) : this.trigger("mousewheel");
        },
        unmousewheel: function (fn) {
            return this.unbind("mousewheel", fn);
        }
    });
    function handler(event) {
        var orgEvent = event || window.event, args = [].slice.call(arguments, 1), delta = 0, returnValue = true, deltaX = 0, deltaY = 0;
        event = $.event.fix(orgEvent);
        event.type = "mousewheel";
        // Old school scrollwheel delta
        if (event.originalEvent.wheelDelta) {
            delta = event.originalEvent.wheelDelta / 120;
        }
        if (event.originalEvent.detail) {
            delta = -event.originalEvent.detail / 3;
        }
        // New school multidimensional scroll (touchpads) deltas
        deltaY = delta;
        // Gecko
        if (orgEvent.axis !== undefined && orgEvent.axis === orgEvent.HORIZONTAL_AXIS) {
            deltaY = 0;
            deltaX = -1 * delta;
        }
        // Webkit
        if (orgEvent.wheelDeltaY !== undefined) {
            deltaY = orgEvent.wheelDeltaY / 120;
        }
        if (orgEvent.wheelDeltaX !== undefined) {
            deltaX = -1 * orgEvent.wheelDeltaX / 120;
        }
        // Add event and delta to the front of the arguments
        args.unshift(event, delta, deltaX, deltaY);
        return ($.event.dispatch || $.event.handle).apply(this, args);
    }
})(jQuery);

