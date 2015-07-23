/**
 * Created by LiChang on 2015/5/15.
 */

$(document).ready(function ($) {
  
    (function () {
    
        return $('.select').each(function () {
            $(this).prepend("<cite></cite>");

            var selectList = $(this).find("ul"),
                selectOption = selectList.find("a"),
                selectPrint = $(this).find("cite"),
                selectInput = $(this).find("input");

            selectPrint.width($(this).width());
            selectList.width($(this).width());

            selectPrint.html(selectOption.eq(0).html());
            for (var i = 0; i < selectOption.length; i++) {
                selectOption.eq(i).attr({ value: i, href: "javascript:void(0)" });
            }
            if ( !selectInput.val() ) {
                selectInput.val(selectOption.eq(0).attr("value"));
            } else {
                selectPrint.html(selectOption.eq(selectInput.val()).html());
            }
            selectList.hide();


            selectPrint.click(function () {
                if (selectList.css("display") != "none") {
                    selectList.slideUp("fast");
                } else {
                    selectList.slideDown("fast");
                }
            });

            var mouseOut = true;
            $(this).mouseover(function () {
                mouseOut = false;
            });

            $(this).mouseout(function () {
                mouseOut = true;
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
});
