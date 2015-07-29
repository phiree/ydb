/*********************
ajax图片上传
需求: 
   input type=file 必须指定两个属性:imageType 和 businessId.
******************/
$('input[type = file]').change(function (ev) {
    var that = this;
    var imageType = $(that).attr('imageType');
    var businessId = $(that).attr("businessId");


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
        }
    });
    return true;


    var formData = new FormData();
    formData.append('file', $(that)[0].files[0]);
    //传入图片种类和商家ID


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
                        var n = $("<span class='tip'>上传成功</span>");
                        $(that).after(n);
                        n.show("slow");
                        n.fadeOut(3000);
                    }, //success
                    error: function (errmsg) {
                        alert('transfer error:' + errmsg);
                    } //error
                }); //ajax
});              //document
