/**
 * 接口封装器 v1.0.0
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
    var tools = YDBan.tools;

    var Adapter = {};
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
     * @param raw response raw data
     * @returns {*}
     */
    Adapter.respUnpack = function(raw){
        var rawData, respObj;
        rawData = raw;
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

        if ( !rawData.protocol_CODE ){
            throw new Error("error protocol code");
        } else {
            respObj.protocolCode = rawData.protocol_CODE;
        }

        if ( rawData == null ) {
            throw new Error("raw data is null");
        }

        if ( typeof rawData === "string" ){
            try {
                rawData = JSON.parse(rawData);
            } catch(error) {
                throw new Error ( "parse error" + error );
            }
        }

        if ( typeof rawData !== "object" ){
            throw new Error("type error: data is typeof" + typeof rawData + ",it should be type of json");
        }

        if ( rawData.state_CODE !== "009000" ){
            respObj.respCorrect = false;
            throw new Error('ERROR_MSG: ' + rawData.protocol_CODE + '-' + rawData.state_CODE + '-' + errorMap[rawData.state_CODE] + ': ' + rawData.err_Msg );
        } else {
            respObj.respCorrect = true;
            if ( typeof rawData.RespData === "object" && rawData.RespData ){
                respObj.respData = rawData.RespData;

                if ( respObj.respData.arrayData && tools.isArray(respObj.respData.arrayData) ) {
                    respObj.hasArrayData = true;
                }
            }
        }

        respObj.stateCode = rawData.state_CODE;

        return respObj;
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