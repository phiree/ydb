/**
 * TabSelection by phiree@gmail.com
 * 2015-6-5
 * display tree-view structure data in jquery-ui tabs
 * demo:http://codepen.io/phiree/pen/xGqNzE
 *
 feature:-------------------------------------
 when click a item in the tab-panel, the children items will load in a new created tab.
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
 <div id="tabs">
 <ul></ul>
 </div>
 usage:---------------------------------
 $("#tabs").TabSelection();
 $("#tabs").TabSelection({
  //options go here
  });
 */

$.fn.ServiceSelect = function (options) {


    var params = $.extend({
        "datasource": null, //local json list,or  ajax_url that return a  jsonlist .
        //"enable_multiselect": true,
        //"leaf_clicked": null,// when leaf clicked ,works when enable_multiselect is
        //"check_changed": null,// checkbox chaged callback,only works when enable_multiselect is true
        //"init_data":[]//
        "this": this,
        "lastClickFunc": null,
        "choiceClass": "choiceSer"
    }, options);

    function init(){
        var _this = params.this;
        var AjaxData = null;

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
            if ( data[0] && data[0].level != "undefined"){
                var dataLevel = data[0].level;
                $serList.attr("list-level", dataLevel);
            } else {
                return;
            }

            for (var i = 0; i < dataArray.length ; i++) {
                var $serListItem = $(document.createElement("li"));
                $serListItem.text( data[i].name );
                $serListItem.attr("data-level",data[i].level);
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
            var isLastChild = null;
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
            console.log( !childDataArray.length );
            if ( !childDataArray.length ){
                isLastChild = true;
                params.lastChildFunc ? params.lastChildFunc : lastSerClick(this);
            } else {
                isLastChild = false;
            }



        }

        function lastSerClick(ele){
            var $ele = $(ele);
            $ele.addClass(params.choiceClass);
        }
    }


    init();
    


}