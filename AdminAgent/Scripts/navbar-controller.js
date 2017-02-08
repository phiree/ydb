(function(){
    var href = window.location.href;

    $(".treeview").find("a").each(function(){
        var src = $(this).attr("href").match(/(\w|-)+.html$/g);
        if ( src && href.match(src[0])){
            $(this).parentsUntil(".sidebar-menu", "li").addClass("active");
        }
    })
})();