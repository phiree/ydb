/**
 * 接口封装器
 * create by liChang at 2016/02/19
 *
 * description : this is a tool to package or unpack data by constant rule.
 * there are two methods you can use.
 * method：
 * Adapter.reqPackage to package the data .
 * Adapter.reqUnpack to unpack the response data to a fixed json.
 *
 */



(function(){
    /**
     *
     * @description custom tool to achieve extend feature.
     */
    var tools = function(){
        var tools = {};
        tools.extend = function(){
            var options, src, name, copy, clone, copyIsArray,
                target = arguments[0] || {},
                i = 1,
                length = arguments.length;

            if ( typeof target !== "object" ) {
                target = {};
            }

            if ( length === 1 ){
                target = this;
                i--;
            }

            for( ; i < length; i++){
                if ( (options = arguments[i]) !== null ){
                    for ( name in options ) {
                        src = target[ name ];
                        copy = options[ name ];

                        if ( target === copy ){
                            continue;
                        }

                        if ( copy && ( tools.isObject(copy) || (copyIsArray = tools.isArray(copy)) ) ) {
                            if ( copyIsArray ){
                                copyIsArray = false;
                                clone = src && tools.isArray(src) ? src : [];
                            } else {
                                clone = src && tools.isPlainObject(src) ? src : {};
                            }

                            target[ name ] = tools.extend(clone, copy);
                        } else if ( copy !== undefined ){
                            target[ name ] = copy;
                        }


                    }
                }
            }

            return target;
        };
        tools.isArray = Array.isArray || function(obj){
            return Object.prototype.toString.call(obj) === '[object Array]';
        };
        tools.isObject = function(obj){
            var type = typeof  obj;
            return type === 'function' || type === 'object' && !!obj;
        };
        return tools;
    }();

    var Adapter = function (){
        return new Adapter();
    };

    /**
     *
     * @param protocolCode protocol code
     * @param data source data
     */
    Adapter.reqPackage = function( protocolCode,data ){
        var result = {};

        //接口号
        result.protocol_CODE = protocolCode;

        //请求数据
        result.ReqData = {};

        //时间戳
        result.stamp_TIMES = "";

        //序列号
        result.serial_NUMBER = "";

        if ( data !== undefined ){
            result.ReqData = tools.extend({}, data );
        }

        result.stamp_TIMES = createTime();

        result.serial_NUMBER = createSerial("009");

        return result;
    };

    /**
     *
     * @param row response row data
     * @returns {*}
     */
    Adapter.respUnpack = function(row){
        var rowData, output;
        rowData = row;
        output = {};
        if ( rowData == null ) {
            throw new Error("row data is null");
        }

        if ( typeof rowData === "string" ){
            try {
                rowData = JSON.parse(rowData)
            } catch(error) {
                throw new Error ( "parse error" + error );
            }
        }

        if ( typeof rowData !== "object" ){
            throw new Error("type error: row data is typeof" + typeof rowData + ",it should be type of json");
        }

        if ( !rowData.respData ){
            output.respData = rowData.RespData;
        }

        if ( !rowData.protocol_CODE ){
            throw new Error("error protocol code");
        } else {
            output.protocolCode = rowData.protocol_CODE;
        }

        output.stateCode = rowData.state_CODE;

        return output;
    };

    /**
     *
     * @returns {string} timestamp
     */
    function createTime (){
        return (new Date().getTime()).toString();
    }

    /**
     * @description return a serial number with fixString at the top.
     * @param fixString
     * @returns {*}
     */
    function createSerial(fixString){
        return fixString + (Math.ceil( Math.random() * 100000 )).toString();
    }


    /* amd */
    if( typeof define === 'function' && define.amd ){
        define("Adapter", [], function () {
            return Adapter;
        });
    }

    if ( this.Adapter !== "function" ){
        this.Adapter = Adapter;
    }

})();