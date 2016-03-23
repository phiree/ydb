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


    Backbone.customApi = function(method, model, options){
        var params,reqData;

            params = {
            type : 'POST',
            /* dataType设置为text, 避免返回的数据进行二次转换导致格式变为字符串的parser error的错误*/
            //dataType: 'text',

            /* mock.js 本地测试代码 */
            dataType: 'json',
            contentType: "application/x-www-form-urlencoded"
        };

        if ( !method ) {
            throw new Error('method is empty');
        }

        if ( !options.url ) {
            params.url = _.result(model, 'url') || urlError();
        }

        if ( options.protocolCode == null ) {
            throw new Error('customApi protocolCode is empty');
        }


        /**
         * 数据合成仅用两种模式，简化接口传输对数据的处理，避免臃肿：
         * data             : 仅用option.data
         * model            : 仅用model原始数据
         */
        if ( options.data && typeof options.data === "object" ) {
            reqData = options.data;
        } else {
            reqData = options.attrs || model.toJSON(options);
        }


        /* 使用自定义方法替代原有的RESTFul查询发方法  */
        if ( method === 'create' || method === 'update' || method === 'patch' || method === 'delete' || method === 'read' ) {
            options.data = Adapter.reqPackage(options.protocolCode , reqData);
        }

        if ( !options.dataFilter ) {
            options.dataFilter = function(rawData, type){

                /* 服务器链接代码 */
                //var jsonResp = JSON.parse(rawData);
                //var Resp = jsonResp.RespData;

                /* mock.js本地测试代码 */
                var respObj = Adapter.respUnpack(rawData);

                if ( respObj.respCorrect ){
                    if ( respObj.hasArrayData ){
                        return respObj.respData.arrayData;
                    } else {
                        return respObj.respData;
                    }
                }

            };
        }

        var error = options.error;
        options.error = function(xhr, textStatus, errorThrown) {
            options.textStatus = textStatus;
            options.errorThrown = errorThrown;
            if ( error ) { error.apply(this, arguments); }
        };


        //make request
        var xhr = options.xhr = Backbone.ajax(_.extend(params, options));
        model.trigger('request', model, xhr, options);
        return xhr;
    };

    Backbone.nativeSync = Backbone.sync;

    /**
     *
     * @param options
     * @returns {*}
     */
    Backbone.getSyncMethod = function(options) {
        // override backbone.sync to customApi when customApi is selected.
        var customApi = options && options.customApi;
        
        if( !customApi ) {
            return Backbone.nativeSync;
        }
        return Backbone.customApi;
    };

    // override backbone.sync.
    Backbone.sync = function(method, model, options) {
        return Backbone.getSyncMethod(options).apply(this, [method, model, options]);
    };

    return Backbone.customApi;
}));