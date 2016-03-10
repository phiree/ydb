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
                        //TODO : may not worked in IE
                        if ( copy && ( (copyIsArray = tools.isArray(copy) ) || tools.isObject(copy)  ) ) {
                            if ( copyIsArray ){
                                copyIsArray = false;
                                clone = src && tools.isArray(src) ? src : [];
                            } else {
                                clone = src && tools.isObject(src) ? src : {};
                            }

                            target[ name ] = tools.extend(clone, copy);
                        } else if ( typeof copy !== "undefined" ){
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
    Adapter.reqPackage = function( protocolCode, data ){
        var reqObj = {}, reqData;
        reqData = data;

        //接口号
        reqObj.protocol_CODE = protocolCode;

        //请求数据
        reqObj.ReqData = {};

        //时间戳
        reqObj.stamp_TIMES = "";

        //序列号
        reqObj.serial_NUMBER = "";

        //客户端名称
        reqObj.appName = "adminBusiness";

        if ( typeof reqData !== "undefined" ){
            if ( typeof reqData === "object" && reqData ){
                reqObj.ReqData = tools.extend({}, data );
            } else {
                throw new Error('request data type error: no json type data');
            }
        }

        reqObj.stamp_TIMES = createTime();

        reqObj.serial_NUMBER = createSerial("009");

        return JSON.stringify(reqObj);
    };

    /**
     *
     * @param row response row data
     * @returns {*}
     */
    Adapter.respUnpack = function(row){
        var rowData, respObj;
        rowData = row;
        respObj = {};

        var errorMap = {
            "009000" : "正常",
            "009001" : "未知数据类型",
            "009002" : "数据库访问错误",
            "009003" : "违反数据唯一性约束",
            "009004" : "数据库访问返回值个数错误",
            "009005" : "数据资源忙",
            "009006" : "数据超出范围",
            "009007" : "提交过于频繁",
            "001001" : "用户认证错误",
            "001002" : "用户密码错误",
            "001003" : "密码错误次数超限被锁定",
            "001004" : "外部系统IP被拒绝",
            "001005" : "第三方登陆失效",
            "001006" : "第三方登陆即将失效"
        }

        if ( !rowData.protocol_CODE ){
            throw new Error("error protocol code");
        } else {
            respObj.protocolCode = rowData.protocol_CODE;
        }

        if ( rowData == null ) {
            throw new Error("row data is null");
        }

        if ( typeof rowData === "string" ){
            try {
                rowData = JSON.parse(rowData);
            } catch(error) {
                throw new Error ( "parse error" + error );
            }
        }

        if ( typeof rowData !== "object" ){
            throw new Error("type error: data is typeof" + typeof rowData + ",it should be type of json");
        }

        if ( rowData.state_CODE !== "009000" ){
            respObj.respCorrect = false;
            throw new Error('ERROR_CODE: ' + rowData.state_CODE + '-' + errorMap[rowData.state_CODE] + ': ' + rowData.err_Msg );
        } else {
            respObj.respCorrect = true;
            if ( typeof rowData.RespData === "object" && rowData.RespData ){
                respObj.respData = rowData.RespData;

                if ( respObj.respData.arrayData && tools.isArray(respObj.respData.arrayData) ) {
                    respObj.hasArrayData = true;
                }
            }
        }

        respObj.stateCode = rowData.state_CODE;

        return respObj;
    };

    /**
     * tool to get parameter by name form url
     * @param name parameter name
     * @param url url location
     * @returns {*}
     */
    Adapter.getParameterByName = function(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
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