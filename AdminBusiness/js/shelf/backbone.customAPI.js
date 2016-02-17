/**
 * Backbone custom API Adapter
 * Version 0.0.1
 * by chang li
 * @2015-12-09
 *
 * 说明：
 * 模仿sync实现方法，实现自定义接口类型
 */
(function (root, factory) {
    if (typeof exports === 'object' && typeof require === 'function') {
        module.exports = factory(require("backbone"));
    } else if (typeof define === "function" && define.amd) {
        // AMD. Register as an anonymous module.
        define(["backbone"], function(Backbone) {
            // Use global variables if the locals are undefined.
            return factory(Backbone || root.Backbone);
        });
    } else {
        factory(Backbone);
    }
}(this, function(Backbone) {
    function urlError(){
        throw new Error('A "url" property or function must be specified');
    }

    /* 接口格式封装 */
    function requestAdapter(protocolCode, data){
        var formattedData;
        formattedData = {
            protocol_CODE : protocolCode,
            ReqData : data,
            stamp_TIMES :  "1490192929222",
            serial_NUMBER :  "00147001015869149756"
        };
        //return formattedData;
        return JSON.stringify(formattedData);
    }

    Backbone.customAdapter = function(method, model, options){

        var params = {
            type : 'POST',
            jsonp: false,

            /* dataType设置为text, 避免返回的数据进行二次转换导致格式变为字符串的parser error的错误*/
            dataType: 'text',

            /* mock.js 本地测试代码 */
            /* dataType: 'json', */

            data : {},
            contentType: "application/x-www-form-urlencoded",
        };

        if ( !method ) {
            throw new Error('method is empty');
        }

        if ( !options.url ) {
            params.url = _.result(model, 'url') || urlError();
        }

        if ( options.data === null && model ) {
            params.data = JSON.stringify(options.attrs || model.toJSON(options));
        }

        if ( options.protocolCode == null ) {
            throw new Error('customAPI protocolCode is empty');
        }

        var data = _.extend(params.data, options.data);

        /* 使用自定义方法替代原有的RESTFul查询发方法  */
        if ( method === 'create' || method === 'update' || method === 'patch' || method === 'delete' ) {

            if ( options.postDataSelect !== null ) {

                /* 通过设置postDataSelect里的属性为true来决定post其model中的对应属性 */
                data.postData = _.pick(model.toJSON(options), function(value, key, object){
                    return options.postDataSelect && options.postDataSelect[key];
                });
            }
        }

        options.data = requestAdapter(options.protocolCode , data);

        if ( !options.dataFilter ) {
            options.dataFilter = function(rawData, type){
                //console.log(type);
                //console.log(rawData);
                var jsonResp = JSON.parse(rawData);
                var Resp = jsonResp.RespData;

                /* mock.js本地测试代码 */
                //var Resp = rawData.RespData;
                if ( Resp === null ) {
                    if ( options.methodPost ){
                        return;
                    } else {
                        throw "Resp is empty";
                    }

                } else {
                    return Resp.arrayData;
                }
            };
        }

        var error = options.error;
        options.error = function(xhr, textStatus, errorThrown) {
            //console.log(xhr);
            //console.log(textStatus);
            //console.log(errorThrown);
            options.textStatus = textStatus;
            options.errorThrown = errorThrown;
            if ( error ) { error.apply(this, arguments); }
        };


        //make request
        var xhr = options.xhr = Backbone.ajax(_.extend(params, options));
        model.trigger('request', model, xhr, options);
        return xhr;
    };

    Backbone.customAPI = Backbone.sync;

    Backbone.getSyncMethod = function(model, options) {
        var customAPI = options && options.customAPI;
        if( !customAPI ) {
            return Backbone.customAPI;
        }
        return Backbone.customAdapter;
    };

// override "backbone.sync" to customAPI when customAPI is selected
    Backbone.sync = function(method, model, options) {
        return Backbone.getSyncMethod(model, options).apply(this, [method, model, options]);
    };

    return Backbone.customAdapter;
}));