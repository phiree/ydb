/*********************
ajax图片上传
需求: 
   input type=file 必须指定两个属性:imageType 和 businessId.
******************/
$(document).on('change', 'input[type=file]', function (ev) {
    var that = this;
    var formData = new FormData();
    formData.append('file', $(that)[0].files[0]);
    //传入图片种类和商家ID
    var imageType = $(that).attr('imageType');
    var businessId = $(that).attr("businessId");
    
    formData.append("imageType", imageType);
    formData.append("businessId", businessId);

    $.ajax(
        {
            url: '/AjaxService/FileUploader.ashx',
            type: "post",
            async: false,
            processData: false,
            contentType: false,
            data: formData,
            success: function (filepath) {
                //var n = $("<span class='tip'>上传成功</span>");
                //$(that).after(n);
                //n.show("slow");
                //n.fadeOut(3000);
            }, //success
            error: function (errmsg) {
                alert('transfer error:' + errmsg);
            } //error
        }); //ajax
});  //document