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

$.fn.TabSelection = function (options) {


    var params = $.extend({

        "datasource": null, //local json list,or  ajax_url that return a  jsonlist .
        "enable_multiselect": false,
        "leaf_clicked": null,// when leaf clicked ,works when enable_multiselect is 
        "check_changed": null,// checkbox chaged callback,only works when enable_multiselect is true
        "init_data":[],//
    }, options);

    //determin the datasource is a ajax_url or a local json object
    var is_ajax = false;
    try {
        var json = $.parseJSON(params.datasource);
    }
    catch (err) {
        is_ajax = true;
    }
    var tabs = $(this).tabs();
    //load top-level data
    $(this).tabs({
        activate: function (event, ui) {

            var tab_name = $(ui.newTab[0]).text();
            var item_names = $(ui.newPanel[0]).children("div").children("span");
            for (var i in item_names) {
                var item = item_names[i];
                if ($(item).text() == tab_name) {
                    // $(item).attr("style","font-weight:900");
                }
            }
        }
    });
    /*
    tab内的一个按钮点击之后
    被点击项目是否有子数据
    没有:其他处理             有:移除该tab之后的所有tab,然后创建一个新tab,加载子数据.
    需要判断当前tab的 index.用以确定其位置.可以使用
    */

    function get_children(parentid) {
        var list = [];
        if (is_ajax) {
            jQuery.ajax({
                url: params.datasource + "&id=" + parentid,

                success: function (result) {
                    list = result;
                },
                async: false
            });
        }
        else {
            for (var i = 0; i < params.datasource.length; i++) {
                var item = params.datasource[i];
                if (item.parentid == parentid) {

                    list.push(item);
                }
            }
        }

        return list;
    }

    function build_children_panel(id) {

        var item_list = get_children(id);
        if (item_list.length == 0) {   ;;// is last leaf
        if(!params.enable_multiselect)
        {params.leaf_clicked(id);}
         return false; }
        var num_tabs = $("div#tabsServiceType ul li").length + 1;
        var tab_panel_content = "";
        for (var i in item_list) {
            var item_check = "";
            if (params.enable_multiselect) { 
            item_check="<input type='checkbox' name='item_id' class='check_item' value='" + item_list[i].id + "' />";
            }
            var item_content ="<div class='serviceTabsItem'>" + item_check+ "<span style='display:inline-block;margin:5px;' class='item'  item_id=" + item_list[i].id + ">" + item_list[i].name + "</span>" +"</div>";

            tab_panel_content += item_content;
        }
        tab_panel_content = "<div>" + tab_panel_content + "</div>";

        $("div#tabsServiceType ul").append(
        //            "<li><a href='/ajaxservice.ashx'>" + "请选择" + "</a></li>"
             "<li><a href='#tab" + num_tabs + "'>" + "请选择" + "</a></li>"
        );
        $("div#tabsServiceType").append(
            "<div class='clearfix' id='tab" + num_tabs + "'>" + tab_panel_content + "</div>"
        );


        $("div#tabsServiceType").tabs("refresh");
        $("div#tabsServiceType").tabs("option", "active", num_tabs - 1);
    }
    build_children_panel(0);
    function item_click(that, id, name) {
        //将当前tab的值设置为name,如果有子项,激活下一个tab,

        $(".ui-tabs-active a").html(name);

        var tab_panel_content = "";
        //移除后面的tab页面
        var tabs_panel = $(that).parents('.ui-tabs-panel')[0];
        var tabs_panels = $($(that).parents('.ui-tabs').children('div'));
        var tabs_headers = $($(that).parents('.ui-tabs').children('ul').children('li'));

        var tabs_panel_index = tabs_panels.index(tabs_panel);
        for (var i = 0; i < tabs_panels.length; i++) {
            if (i > tabs_panel_index) {
                $(tabs_panels[i]).remove();
                $(tabs_headers[i]).remove();
            }
        }
        build_children_panel(id);
    }
    //$('.ui-tabs-panel').not('.ui-tabs-hide').html(build_children_panel(0));
    $('div#tabsServiceType').on('click', '.item', function () {
        var that = this;
        var id = $(that).attr('item_id');
        var name = $(that).html();
        item_click(that, id, name);
        
    });
    $('div#tabsServiceType').on('change', '.check_item', function (ev) {
        var that = this;
        var id = $(that).val();
        var checked = $(that).is(":checked");
//        params.check_changed(id,checked);
        params.check_changed(that,id,checked);
    });


}