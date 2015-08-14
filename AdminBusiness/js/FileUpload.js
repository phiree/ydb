/*********************
ajax图片上传
需求: 
   input type=file 必须指定两个属性:imageType 和 businessId.
******************/
//暂时简单粗暴的解决图片上传限制
$(".input-file-btn.file-limit-2").change(function (ev) {
    var limit = 2;
    upLoadImg(this,limit);
});


$(".input-file-btn.file-limit-6").change(function (ev) {
    var limit = 6;
    upLoadImg(this,limit);
});

$(".input-file-btn.file-default").change(function (ev) {
    upLoadImg(this,10);
});

function imgNumLimit(ele,limit){
    var container = ele.parent();
    var prevImgNum = container.find(".download-img-pre").length;
    var inputImgNum = container.find(".input-file-box").length;

    if ( (prevImgNum + inputImgNum) >= limit ) {
        return false;
    } else {
        return true;
    }
}

function upLoadImg (ele,limit){
    var _this = ele;
    var limitNum = limit;

    var imageType = $(_this).attr('imageType');
    var businessId = $(_this).attr("businessId");

    var parent = $(_this).parent();
    var parentClone = parent.clone(true);

    var imgObjPreview = $(_this).siblings(".input-file-pre").get(0);
    var $imgObjMark = $(_this).siblings(".input-file-mark");

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
            if (parent.hasClass("headFile")) {
                return;
            } else if( imgNumLimit(parent,limitNum) ){
                parentClone.insertAfter(parent);
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

(function(){
    var imgList = $('.img-list.img-list-limit6');
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


(function(){
    var imgList = $('.img-list.img-list-limit2');
    var limitNum = 2;

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
