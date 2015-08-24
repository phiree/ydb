
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
                    var newTag = $("<span class='spTag d-inb' tagid='" + result + "' ></span>");
                    var newTagText = $("<span class='spTagText' tagid='" + result + "'>" + ipTag + "</span>");
                    var tagDel = $("<input class='spTagDel' type='button' tagid='" + result + "' value='删除'/>");
                    $("#ipTag").val("");
                    newTagText.appendTo(newTag);
                    tagDel.appendTo(newTag);
                    newTag.appendTo(container);
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
