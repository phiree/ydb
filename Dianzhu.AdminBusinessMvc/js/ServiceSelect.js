/**
 * ServiceSelect by licdream@126.com
 * 2015-7-28
 *
 feature:-------------------------------------
 when click a option in the select list, the children selects will load in a new list.
 requirements:----------------------

 1) datasource must be an json array,each object has 3 properties: id, parentid(0 is top level),name;
 or  an url to fetch json array with  same structure  from server.
 example:
 [
 {'name':'food','id':1,'parentid':0},
 {'name':'fruit','id':2,'parentid':1},
 {'name':'apple','id':3,'parentid':2},
 ]
 2) html structure:
 <div id="ServiceSelect">
 </div>
 usage:---------------------------------
 $("#ServiceSelect").ServiceSelect();
 $("#ServiceSelect").ServiceSelect({
  //options go here
  });
 */

$.fn.ServiceSelect = function (options) {


    var params = $.extend({
        "datasource": null, //local json list,or  ajax_url that return a  jsonlist .
        "element": this,
        "choiceContainer": null,
        "choiceConfBtn": null,
        "lastClickFunc": null,
        "choiceClass": "choiceSer",
        "printInputID" : null
    }, options);

    function init(){
        var _this = params.element;
        var AjaxData = null;
        var valueInput = $("#" + params.printInputID);
        var choiceContainer = $("#" + params.choiceContainer);
        var choiceConfBtn = $("#" + params.choiceConfBtn);
        var confirmValue = "";
        //console.log(valueInput);

        jQuery.ajax({
            url: params.datasource + "&id=0",

            success: function (result) {
                AjaxData = result;
                createList(AjaxData);
            },
            async: false
        });

        function dataRequest (id){
            var data = [];
                jQuery.ajax({
                    url: params.datasource + "&id=" + id,

                    success: function (result) {
                        data = result;
                    },
                    async: false
                });
            return data;
        }

        function createList ( data ) {
            var $serList = $(document.createElement("ul"));
            var dataArray = data;

            $serList.addClass("serListUl")
            if ( data[0] && data[0].level != "undefined"){
                var dataLevel = data[0].level;
                $serList.attr("list-level", dataLevel);
            } else {
                return;
            }

            for (var i = 0; i < dataArray.length ; i++) {
                var $serListItem = $(document.createElement("li"));
                $serListItem.addClass("serListItem");
                $serListItem.text( data[i].name );
                $serListItem.attr("data-level",data[i].level);
                $serListItem.attr("data-name",data[i].name);
                $serListItem.attr("data-id",data[i].id);
                $serListItem.bind("click",serviceClick);
                $serList.append( $serListItem );
            }
            $(_this).append($serList);
        }

        function serviceClick(e){
            var thisID = $(this).attr("data-id");
            var thisLevel = Number($(this).attr("data-level"));
            var restChildLevel = ( 3 - thisLevel );
            var childDataArray = dataRequest(thisID);
            var childLevel = thisLevel + 1;
            var childList = $("ul[list-level=" + childLevel +"]");
            //var isLastChild = null;
            //console.log(restChildLevel);


            if ( !childList ) {
                createList(childDataArray);
            } else {
                for ( var i = 0 ; i < restChildLevel ; i++ ){
                    var nextChildLevel = thisLevel + i + 1;
                    var restChildList = $("ul[list-level=" + nextChildLevel +"]");
                    //console.log(restChildList);
                    restChildList.remove();
                }
                createList(childDataArray);
            }

            _this.find("ul").find("li").removeClass(params.choiceClass);
            //console.log( !childDataArray.length );
            if ( !childDataArray.length ){
                //isLastChild = true;
                var lastChoiceValue = $(this).attr("data-id");
                //console.log(valueInput);
                confirmValue = lastChoiceValue;
                //valueInput.attr("value",lastChoiceValue);
                printChoice($(this).attr("data-name"));
                params.lastChildFunc ? params.lastChildFunc : lastSerClick(this);
            } else {
                //isLastChild = false;
                confirmValue = null;
                //valueInput.attr("value",null);
                printChoice(null);
                choiceConfBtn.hide();
            }

        }

        function printChoice (name){
            choiceContainer.find("span").remove();
            if ( !name ){
                choiceContainer.find("span").remove();
            } else {
                var $choiceEle = $(document.createElement("span"));
                $choiceEle.text(name);
                choiceContainer.append($choiceEle);
            }

        }

        function lastSerClick(ele){
            var $ele = $(ele);
            $ele.addClass(params.choiceClass);
            choiceConfBtn.show()
        }

        choiceConfBtn.bind("click",function(){
            valueInput.attr("value",confirmValue);
        });

        function reset (){
            choiceContainer.find("span").remove();
            _this.find("ul").find("li").removeClass(params.choiceClass);
        }
    }


    init();
    


}