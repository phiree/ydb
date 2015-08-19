/**
 * Created by LiChang on 2015/5/15.
 */

(function ($) {

    (function(){
        var htmlH = $("html").height();
        var navbarH = $(".navbar").height();

        //return $(window).resize(function(){
            //$(".mh").css({
            //    minHeight: htmlH - navbarH
            //})

            $(window).bind("load resize", function() {

                var height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
                var paddingH = 28;
                height = height - navbarH;
                if (height < 1) height = 1;
                if (height > navbarH) {
                    $(".mh").css("min-height", (height) + "px");
                    $(".mh-in").css("min-height", (height - paddingH) + "px");
                }
            });

            //var url = window.location;
            //var element = $('ul.nav a').filter(function() {
            //    return this.href == url || url.href.indexOf(this.href) == 0;
            //}).addClass('active').parent().parent().addClass('in').parent();
            //if (element.is('li')) {
            //    element.addClass('active');
            //}

        //})


    })();

    (function(){
        return $('.hour-select').each(function () {
                var selectList = $(this).find("ul");
                for ( var i = 0 ; i < 25 ; i++ ) {
                    selectList.append("<li><a>"+ i  +"</a></li>");
                }
            }
        )
    })();

    (function(){
        return $('.minite-select').each(function () {
                var selectList = $(this).find("ul");
                for ( var i = 0 ; i < 60 ; i++ ) {
                    selectList.append("<li><a>"+ i  +"</a></li>");
                }
            }
        )
    })();

    (function(){
        return $('.years-select').each(function () {
                var selectList = $(this).find("ul");
                for ( var i = 0 ; i < 100 ; i++ ) {
                    selectList.append("<li><a>"+ i +"</a></li>");
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
                selectInput.val($(this).attr("value")).focus().blur();
                selectList.hide();
            });
        }
        )
    })();

    //(function () {
    //
    //    $('.input-file-btn').change(function () {
    //        var imgObjPreview = $(this).siblings(".input-file-pre").get(0);
    //
    //        if (this.files && this.files[0]) {
    //            imgObjPreview.src = window.URL.createObjectURL(this.files[0]);
    //        }
    //        else {
    //            this.select();
    //            this.blur();
    //            var imgSrc = document.selection.createRange().text;
    //
    //            try {
    //                imgObjPreview.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
    //                imgObjPreview.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
    //            }
    //            catch (e) {
    //                return false;
    //            }
    //            document.selection.empty();
    //
    //            return
    //        }
    //        return true;
    //    })
    //
    //})();
    (function(){
        $(".btn-cancel").each(function(){
            $(this).click(function(){
                return confirm("是否取消保存？");
            })
        })
    })();


    (function (){
        var $navBtn = $(".nav-btn-bg:not(.nav-btn-un)");
        var pageHref = window.location.href;
        var hrefFiter = /\/\w+/;

        $navBtn.each(function(){
            var btnHref = $(this).parent().attr("href");
            var btnIcon = $(this).find(".nav-btn-icon");
            var btnTitle = $(this).find(".nav-btn-t");
            var bgPosition = parseInt(btnIcon.css("background-position-X"));
            var realHref = btnHref.match(hrefFiter);
            var currentPage = false;

            if ( pageHref.indexOf(realHref) >= 0 ) {
                currentPage = true;
                $(this).css({
                    borderBottomColor : "#30B5FA"
                });
                btnIcon.css({
                    backgroundPositionX : bgPosition - 32
                });
                btnTitle.css({
                    color : "#30B5FA"
                })
            }

            $(this).hover(function(){
                $(this).css({
                    borderBottomColor : "#30B5FA"
                });
                btnIcon.css({
                    backgroundPositionX : bgPosition - 32
                });
                btnTitle.css({
                    color : "#30B5FA"
                })
            },function(){
                 if ( currentPage ) {
                     return
                 } else {
                     $(this).css({
                         borderBottomColor : "transparent"
                     });
                     btnIcon.css({
                         backgroundPositionX : bgPosition
                     });
                     btnTitle.css({
                         color : "#B6CCE3"
                     })
                 }
                }
            );


        })

        var $sideArrow = $(".side-arrow")
        var $sideBtn = $(".side-btn-bg");

        $sideBtn.each(function(){
            var btnHref = $(this).parent().attr("href");
            var bgPosition = parseInt($(this).css("background-positionX"));
            if ( pageHref.indexOf(btnHref) >= 0 ) {
                var toTop = $(this).position().top
                $sideArrow.css({
                    top :　toTop,
                    display : "inline-block"
                })
            }
        })

    })()

})(jQuery);


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

