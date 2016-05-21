/*
* 基于ASP.NET Webform 的ajax图片上传.input type=file 必须指定两个属性:imageType 和 businessId.
* created by licdream@126.com
*
* 1.options
* {
*   imgList: selector, 图片列表的容器
*   limitNum: num, 图片限制数量
*   imgBox: selector 图片input外容器
* }
*
* 2.html structure:
* <div class="img-list">--imgList--
*    <div class="download-img-pre m-r10 m-b10 fl">
*    <div class="input-file-box dis-n">--imgBox--
*       <input type=file class="input-file-btn"  businessId="<%=b.Id %>" imageType="businesslicense" />
*       <i class="input-file-bg"></i>
*       <i class="input-file-mark"></i>
*       <img class="input-file-pre"/>
*    </div>
* </div>
*
* */
(function($) {


    /**
     * 页面加载时图片的显示限制
     * @function
     * @param Selector list 图片列表容器
     * @param Number limit 限制数量
     */
    function imgListInit (list,limit){
        var prevImgNum = list.find(".download-img-pre").length;
        var inputImgNum = list.find(".input-file-box").length;
        var fileBox =  list.find(".input-file-box");
        //input button hide when length of img file larger than limit;
        if ( (prevImgNum + inputImgNum) > limit ) {
            return;
        } else {
            fileBox.removeClass("dis-n");
        }
    }

    /**
     * 上传时对input的显示限制
     * @function
     * @param Selector list 图片列表容器
     * @param Number limit 限制数量
     */
    function imgNumLimit(list,limit){
        if ( limit == 1) {
            return false;
        } else {
            var prevImgNum = list.find(".download-img-pre").length;
            var inputImgNum = list.find(".input-file-box").length;
            return ( (prevImgNum + inputImgNum) >= limit ) ? false : true;
        }
    }

    /**
     * 上传图片
     * @function
     * @param Selector fileInput 图片上传input
     * @param Object settings
     */
    function upLoadImg (fileInput, settings){
        var _this = fileInput;
        var limitNum = settings.limitNum;

        var imageType = $(_this).attr('imageType'),
            businessId = $(_this).attr("businessId");

        var inputBox = $(_this).parent(),
            inputBoxClone = inputBox.clone(true),
            imgList = (typeof settings.imgList === "string") ? inputBox.parent("" + settings.imgList + "") : settings.imgList;

        var limitFlag = imgNumLimit(imgList,limitNum);
        //flag shows that if the length of img files larger than limit or not,ture is not;判断图片数量是否大于限制,ture表示没有大于限制;

        var imgObjPreview = $(_this).siblings(".input-file-pre").get(0),
            $imgObjMark = $(_this).siblings(".input-file-mark");

        if (_this.files && _this.files[0]) {
            imgObjPreview.src = window.URL.createObjectURL(_this.files[0]);
        }
        else {
            _this.select();
            _this.blur();
            var imgSrc = document.selection.createRange().text;

            try {
                imgObjPreview.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)";
                imgObjPreview.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
            }
            catch (e) {
                return false;
            }
            document.selection.empty();
        }
        $imgObjMark.show();

        var myform = document.createElement("form");
        myform.action = "/AjaxService/FileUploader.ashx";
        myform.method = "post";
        myform.enctype = "multipart/form-data";
        document.body.appendChild(myform);

        var form = $(myform);
        var fu = $(_this).clone(true).val("");
        var ipbusinessId = document.createElement("input");
        $(myform).hide();
        ipbusinessId.name = "businessId";
        ipbusinessId.value = businessId;
        $(ipbusinessId).appendTo(form);

        var ipimageType = document.createElement("input");
        ipimageType.name = "imageType";
        ipimageType.value = imageType;
        $(ipimageType).appendTo(form);

        var fua = $(_this).appendTo(form);
        $(fua).attr("name", "upload_file");

        form.ajaxSubmit({
            success: function (data) {
                form.remove();
                $imgObjMark.hide();
                var r = data.split(",");
                if (r[0] == "F") {
                    alert(r[1]);
                    return;
                }

                if ( limitFlag ) {
                    inputBoxClone.insertAfter(inputBox);
                } else {
                    return;
                }
            },
            error: function () {
                alert("图片上传失败，请刷新页面重新上传");
            }
        });
        return true;
    }

    var methods = {
        init: function (options) {

            return this.each(function() {
                var $this = $(this);

                var settings = $this.data('pluginName');

                if( typeof(settings) === 'undefined') {
                    var defaults = {
                        imgList: $($this.parent()).parent(),
                        limitNum: 6,
                        imgBox: $this.parent()
                    };

                    settings = $.extend({}, defaults, options);

                    $this.data('pluginName', settings);
                } else {

                    settings = $.extend({}, settings, options);

                // $this.data('pluginName', settings);
                }

                imgListInit(settings.imgList,settings.limitNum);
                $this.change(function(){
                    upLoadImg(this,settings);
                });

            });
        }
    };

    $.fn.imgUpload = function() {
        var method = arguments[0];

        if(methods[method]) {
            method = methods[method];
            arguments = Array.prototype.slice.call(arguments, 1);
        } else if( typeof(method) === 'object' || !method ) {
            method = methods.init;
        } else {
            $.error( 'Method ' +  method + ' does not exist on jQuery.imgUpload' );
            return this;
        }

        return method.apply(this, arguments);
    };

})(jQuery);
