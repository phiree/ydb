
$(function () {

    $("#ipTagAdd").click(function () {
        var container = $(".ipTagContainer");
        var ipTag = $("#ipTag").val();
        var serviceId = $("#ipTag").attr("serviceId");


        if (ipTag == "") {
            return;
        } else {
            $.post(
                "/ajaxservice/taghandler.ashx",
                { "action": "add",
                    "tagText": ipTag,
                    "serviceId": serviceId
                },
                function (result) {
                    $("#ipTag").val("");
                    result = JSON.parse(result);
                    for (var i in result)
                    {
                        var tagId = result[i].tagId;
                        var tagText = result[i].tagText;
                   
                        var newTag = $("<span class='spTag d-inb' tagid='" + tagId + "' ></span>");
                        var newTagText = $("<span class='spTagText' tagid='" + tagId + "'>" + tagText + "</span>");
                        var tagDel = $("<input class='spTagDel' type='button' tagid='" + tagId + "' value='删除'/>");
                  
                    newTagText.appendTo(newTag);
                    tagDel.appendTo(newTag);
                    newTag.appendTo(container);
                    }
                }
            );
        } //
    })
    $(document).on("click",".spTagDel", function () {
        var that = this;
        var tagId = $(that).attr("tagid");
        $.post(
            "/ajaxservice/taghandler.ashx",
            {
                "action": "delete",
                "tagId": tagId
            },
            function () {
                $(that).parent(".spTag").remove();

            }
        );
    });
});
