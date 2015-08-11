/*********************
ajax图片上传
需求: 
   input type=file 必须指定两个属性:imageType 和 businessId.
******************/
$(".input-file-btn").change(function (ev) {
    var that = this;

    var limitNum = 6;

    var imageType = $(that).attr('imageType');
    var businessId = $(that).attr("businessId");

    var parent = $(this).parent();
    var parentClone = parent.clone(true);

    var imgObjPreview = $(this).siblings(".input-file-pre").get(0);
    var $imgObjMark = $(this).siblings(".input-file-mark");

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
    }
    $imgObjMark.show();
    // if (this.files[0].size > 2 * 1024 * 1024) { $(that).blur(); return true; }


    var myform = document.createElement("form");
    myform.action = "/AjaxService/FileUploader.ashx";
    myform.method = "post";
    myform.enctype = "multipart/form-data";
    document.body.appendChild(myform);

    var form = $(myform);
    var fu = $(this).clone(true).val("");
    var ipbusinessId = document.createElement("input");
    ipbusinessId.name = "businessId";
    ipbusinessId.value = businessId;
    $(ipbusinessId).appendTo(form);

    var ipimageType = document.createElement("input");
    ipimageType.name = "imageType";
    ipimageType.value = imageType;
    $(ipimageType).appendTo(form);

    var fua = $(this).appendTo(form);
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
            if (parent.hasClass("headFile")) {
                return;
            } else if( imgNumLimit(parent) ){
                //parentClone.addClass("m-l10");
                parentClone.insertAfter(parent);
            } else {
                return;
            }
        },
        error: function () {
            alert("图片上传失败，请刷新页面重新上传");
        }
    });

    function imgNumLimit(ele){
        var container = ele.parent();
        var prevImgNum = container.find(".download-img-pre").length;
        var inputImgNum = container.find(".input-file-box").length;

        if ( (prevImgNum + inputImgNum) >= limitNum ) {
            return false;
        } else {
            return true;
        }
    }

    return true;

});

(function(){
    var imgList = $('.img-list');
    var limitNum = 6;

    imgList.each(function(){
        var prevImgNum = $(this).find(".download-img-pre").length;
        var inputImgNum = $(this).find(".input-file-box").length;
        var fileBox =  $(this).find(".input-file-box");

        if ( (prevImgNum + inputImgNum) > limitNum ) {
            return
        } else {
            fileBox.removeClass("dis-n");
        }
    })

})();
