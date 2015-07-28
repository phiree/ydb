/**
 * Created by LiChang on 2015/5/15.
 */

$(document).ready(function ($) {

    (function(){
        return $('.time-select').each(function () {
                var selectList = $(this).find("ul");
                for ( var i = 0 ; i < 25 ; i++ ) {
                    selectList.append("<li><a>"+ i + ":00" +"</a></li>");
                }
            }
        )
    })();

    (function () {
    
        return $('.select').each(function () {
            $(this).prepend("<cite></cite>");

            var selectList = $(this).find("ul"),
                selectOption = selectList.find("a"),
                selectPrint = $(this).find("cite"),
                selectInput = $(this).find("input");


            selectPrint.html(selectOption.eq(0).html());
            for (var i = 0; i < selectOption.length; i++) {
                selectOption.eq(i).attr({
                    value : i,
                    href : "javascript:void(0)"
                });
            }

            if ( !selectInput.val() ) {
                selectInput.val(selectOption.eq(0).attr("value"));
            } else {
                selectPrint.html(selectOption.eq(selectInput.val()).html());
            }

            selectPrint.click(function () {
                if (selectList.css("display") == "none") {
                    selectList.slideDown("fast");

                } else {
                    selectList.slideUp("fast");
                }
            });

            var mouseOut = true;
            $(this).bind("mouseover mouseout",function(e){
                if (e.type == "mouseover"){
                    mouseOut = false;
                }else if(e.type == "mouseout"){
                    mouseOut = true;
                }
            });

            $(document).click(function () {
                if (mouseOut) {
                    selectList.hide();
                }
            });

            selectOption.click(function () {
                selectPrint.html($(this).text());
                selectInput.val($(this).attr("value"));
                selectList.hide();
            });
        }
        )
    })();

    (function () {

        $('.input-file-btn').change(function () {
            var imgObjPreview = $(this).siblings(".input-file-pre").get(0);

            if (this.files && this.files[0]) {
                imgObjPreview.src = window.URL.createObjectURL(this.files[0]);
            }
            else {
                this.select();
                this.blur();
                var imgSrc = document.selection.createRange().text;

                try {
                    imgObjPreview.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
                    imgObjPreview.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
                }
                catch (e) {
                    return false;
                }
                document.selection.empty();

                return
            }
            return true;
        })

    })();

    (function (){
        var $navBtn = $(".nav-btn");
        var $sideBtn = $(".side-btn");
        var pageHref = window.location.href;
        var hrefFiter = /\/\w+/;

        $navBtn.each(function(){
            var btnHref = $(this).parent().attr("href");
            var bgPosition = parseInt($(this).css("background-positionX"));
            var realHref = btnHref.match(hrefFiter);

            if ( pageHref.indexOf(realHref) >= 0 ) {
                $(this).css({
                    backgroundPositionX : bgPosition - 81
            })
            }
        })

        $sideBtn.each(function(){
            var btnHref = $(this).parent().attr("href");
            var bgPosition = parseInt($(this).css("background-positionX"));
            if ( pageHref.indexOf(btnHref) >= 0 ) {
                $(this).css({
                    backgroundPositionX : bgPosition - 81
                })
            }
        })
    })()

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
        if (event.originalEvent.wheelDelta) { delta = event.originalEvent.wheelDelta / 120; }
        if (event.originalEvent.detail) { delta = -event.originalEvent.detail / 3; }
        // New school multidimensional scroll (touchpads) deltas
        deltaY = delta;
        // Gecko
        if (orgEvent.axis !== undefined && orgEvent.axis === orgEvent.HORIZONTAL_AXIS) {
            deltaY = 0;
            deltaX = -1 * delta;
        }
        // Webkit
        if (orgEvent.wheelDeltaY !== undefined) { deltaY = orgEvent.wheelDeltaY / 120; }
        if (orgEvent.wheelDeltaX !== undefined) { deltaX = -1 * orgEvent.wheelDeltaX / 120; }
        // Add event and delta to the front of the arguments
        args.unshift(event, delta, deltaX, deltaY);
        return ($.event.dispatch || $.event.handle).apply(this, args);
    }
})(jQuery);

