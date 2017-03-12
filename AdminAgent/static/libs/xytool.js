(function () {
    var xyTool = {
        parseValue: function(xyValue){
            if ( !typeof _.isObject(xyValue) ){ throw new Error("xyValue is no a object")}

            return {
                x : _.keys(xyValue),
                y : _.values(xyValue),
                xy : xyValue
            }
        }
    };

    window.xyTool = xyTool;

})();