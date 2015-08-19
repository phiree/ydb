
$(function() {
    $("#ipTagAdd").click(function(){
        var container = $(".ipTagContainer");
        var ipTag = $("#ipTag").val();
        var serviceId = $("#ipTag").attr("serviceId");


        if( ipTag == "" ){
            return;
        } else {
            $.post(
                "/ajaxservice/taghandler.ashx",
                {
                    "tagText": ipTag,
                    "serviceId": serviceId
                },
                function (result) {
                    var newTag = $("<span class='spTag d-inb' tagid='" + result + "' ></span>");
                    var newTagText = $("<span class='spTagText' tagid='" + result + "'>" + ipTag + "</span>");
                    var tagDel = $("<input class='spTagDel' type='button' tagid='" + result + "' value='删除'/>");
                    $("#ipTag").val("");
                    newTagText.appendTo(newTag);
                    tagDel.appendTo(newTag);
                    newTag.appendTo(container);
                }
            );
        }

    })
});
