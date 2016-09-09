/**
 * ipTag.js v1.0.0 @ 2016-05-17 by licdream@126.com
 * 商户服务标签编辑控件
 */
$(function () {

    /**
     * 清楚重复数组内重复元素方法
     * @param someArray
     * @returns {*}
     */
    function getUnique(someArray) {
        var tempArray = someArray.slice(0);//复制数组到临时数组
        for (var i = 0; i < tempArray.length; i++) {
            for (var j = i + 1; j < tempArray.length;) {
                if ( tempArray[j] == tempArray[i] )
                //后面的元素若和待比较的相同，则删除并计数；
                //删除后，后面的元素会自动提前，所以指针j不移动
                {
                    tempArray.splice(j, 1);
                }
                else {
                    j++;
                }
                //不同，则指针移动
            }
        }
        return tempArray;
    }


    // 新建服务，服务标签添加
    $("#ipTagAddNew").on("click", function(){
        var $tagInput = $("#addTagNew");
        var $tagHidden = $("#tbxTag");
        var $container = $("#ipTagContainerNew");
        var tagText = $tagInput.val(), arrTag;
        var tagHiddenText = $tagHidden.val(), arrHiddenTag;

        if ( tagText === "" ) return;

        arrTag = tagText.split(/\s+/);

        if( tagHiddenText !== "" ){
            var concatArr;
            arrHiddenTag = tagHiddenText.split(/\s+/);

            // 如果已经添加值，则合并判断
            concatArr = arrHiddenTag.concat(arrTag);

            arrHiddenTag = getUnique(concatArr);
            tagHiddenText = arrHiddenTag.join(" ");
        } else {
            arrHiddenTag = getUnique(arrTag);
            tagHiddenText = arrHiddenTag.join(" ");
        }


        $tagHidden.val(tagHiddenText);
        $tagInput.val("");
        $container.html("");

        for( var i = 0 ; i < arrHiddenTag.length ; i++ ){

            var $tagItem = $("<span class='spTag d-inb'></span>");
            var $tagTxt = $("<span class='spTagText'></span>").text(arrHiddenTag[i]);
            var $tagDelete = $("<input class='spTagDel' type='button' />");

            $tagItem.append($tagTxt).append($tagDelete);

            $container.append($tagItem);

            // 删除新建标签
            $tagDelete.on("click", function(e){

                var $target = $(e.target);
                var deleteText = $target.siblings(".spTagText").eq(0).html();
                var tempText = $tagHidden.val();

                // 动态匹配要删除的字符串
                tempText = tempText.replace(new RegExp("\\b\\s*" + deleteText + "\\b"),"");

                $tagHidden.val(tempText);

                $(e.target).parent(".spTag").remove();
            });

        }

    });

    // 服务编辑，ajax方式标签添加
    $("#ipTagAdd").on("click", function () {
        var $container = $(".ipTagContainer");
        var $ipTag = $("#ipTag");
        var tagText = $ipTag.val(),tagArr;
        var serviceId = $ipTag.attr("serviceId");

        if ( tagText !== "" ) {
            // TODO : 重复性判断

            tagText = getUnique(tagText.split(/\s+/)).join(" ");

            $.ajax({
                url : "/ajaxservice/taghandler.ashx",
                type : "POST",
                data : {
                    "action": "add",
                    "tagText": tagText,
                    "serviceId": serviceId
                },
                success : function(resp){
                    $("#ipTag").val("");
                    result = JSON.parse(resp);
                    for (var i in result) {
                        var tagId = result[i].tagId;
                        var tagText = result[i].tagText;

                        var newTag = $("<span class='spTag d-inb' tagid='" + tagId + "' ></span>");
                        var newTagText = $("<span class='spTagText' tagid='" + tagId + "'>" + tagText + "</span>");
                        var tagDel = $("<input class='spTagDel' type='button' tagid='" + tagId + "' value='删除'/>");

                        newTagText.appendTo(newTag);
                        tagDel.appendTo(newTag);
                        newTag.appendTo($container);
                    }
                },
                error : function(err){
                    console.log(err);
                }
            });
        }

    });

    // 服务编辑，ajax标签删除
    $(".spTagDel").on("click", function () {
        var that = this;
        var tagId = $(that).attr("tagid");

        $.ajax({
            url : "/ajaxservice/taghandler.ashx",
            type : "POST",
            data : {
                "action": "delete",
                "tagId": tagId
            },
            success : function(resp){
                if ( resp !== "" ){
                    $(that).parent(".spTag").remove();
                }
            },
            error : function(xhr, errorThrown, text){
                if(JSON.parse(xhr.responseText).msg === "unlogin")
                window.location.href = "/login.aspx"
            }
        });
    });
});
