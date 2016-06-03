(function (){
    var notify = (function(){
        return {
            polling : function(options){
                var defaultOptions = {
                    delay : 5000
                };
                var delay = options.delay || defaultOptions.delay;

                if ( typeof options === "object" && options ){
                    var reqData = options.reqData;
                    var callback = options.callback;

                    setInterval(function(){
                        $.ajax({
                            url : options.url,
                            type : "GET",
                            dataType : "json",
                            data : reqData,
                            success : function(data, textStatus, jqXHR){
                                if ( typeof callback === "function" && callback ){
                                    callback(data);
                                }
                            }}
                        )
                    }, delay);
                }
            }
        };
    })();

    if ( typeof window.Dianzhu !== "undefined" ){
        window.Dianzhu.notify = notify
    } else {
        window.Dianzhu = { notify: notify };
    }

})();
