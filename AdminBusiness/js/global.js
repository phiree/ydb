/**
 * Created by LiChang on 2015/5/15.
 */

jQuery(document).ready(function ($) {
    (function () {
        var leftCont = document.getElementById('leftCont');
        var rightCont = document.getElementById('rightCont');

        console.log(leftCont.offsetHeight);
        console.log(rightCont.offsetHeight);
        if (rightCont.offsetHeight != 0) {
            leftCont.style.height = rightCont.offsetHeight + 'px';
        }
    })();

    (function () {
        return $('.select').each(function () {
            $(this).prepend("<cite></cite>");

            var selectList = $(this).find("ul"),
                selectOption = selectList.find("a"),
                selectPrint = $(this).find("cite"),
                selectInput = $(this).find("input");

            selectPrint.width($(this).width());
            selectList.width($(this).width());

            (function () {
                selectPrint.html(selectOption.eq(0).text());
                for (var i = 0; i < selectOption.length; i++) {
                    selectOption.eq(i).attr({ value: i, href: "javascript:void(0)" });
                }
                selectInput.val(selectOption.eq(0).attr("value"));
                selectList.hide();
            })();

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
            //          var docObj=document.getElementById("doc");
            //          var imgObjPreview = document.getElementById("ImgPreview");
            var imgObjPreview = $(this).siblings(".input-file-pre").get(0);

            if (this.files && this.files[0]) {

                //              imgObjPreview.style.display = 'block';
                //              imgObjPreview.style.width = '150px';
                //              imgObjPreview.style.height is= '180px';
                //              imgObjPreview.src = this.files[0].getAsDataURL();


                imgObjPreview.src = window.URL.createObjectURL(this.files[0]);
                console.log(imgObjPreview.src);
            }
            else {

                this.select();
                var imgSrc = document.selection.createRange().text;
                var localImagId = document.getElementById("localImag");

                localImagId.style.width = "90px";
                localImagId.style.height = "90px";

                try {
                    localImagId.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
                    localImagId.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
                }
                catch (e) {
                    return false;
                }
                imgObjPreview.style.display = 'none';
                document.selection.empty();
            }
            return true;
        })
    })();
});
