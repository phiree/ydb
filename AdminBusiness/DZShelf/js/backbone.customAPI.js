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
        throw new Error('A "url" property or function must be specified')
    };

    function requestAdapter(protocolCode, data){
        var formattedData;
        formattedData = {
            protocol_CODE : protocolCode,
            ReqData : data,
            stamp_TIMES :  "1490192929222",
            serial_NUMBER :  "00147001015869149756",
        };
        debugger;
        return JSON.stringify(formattedData);
    }

    Backbone.customAdapter = function(method, model, options){

        var params = {
            type : 'POST',
            jsonp: false,
            //dataType: 'text',
            dataType: 'json',
            data : {},
            contentType: "application/x-www-form-urlencoded",
        };

        if ( !method ) {
            throw new Error('method is empty');
        }

        if ( !options.url ) {
            params.url = _.result(model, 'url') || urlError();
        }

        if ( options.data == null && model ) {
            params.data = JSON.stringify(options.attrs || model.toJSON(options));
        }

        if ( options.protocolCode == null ) {
            throw new Error('customeAPI protocolCode is empty');
        }

        var data = _.extend(params.data, options.data);
        if ( method === 'create' || method === 'update' || method === 'patch' || method === 'delete' ) {
            data.postData = JSON.stringify(model.toJSON(options));
        }

        options.data = requestAdapter(options.protocolCode , data);

        if ( !options.dataFilter ) {
            options.dataFilter = function(rawData, type){
                console.log(type);
                console.log(rawData);
                var result = rawData;
                //var jsonResp = JSON.parse(rawData);
                //var Resp = jsonResp.RespData;
                var Resp = result.RespData;
                if (Resp == null) {
                    throw "Resp is empty";
                } else {
                    return Resp.arrayData;
                }

            }
        }

        var error = options.error;
        options.error = function(xhr, textStatus, errorThrown) {
            debugger;
            console.log(xhr);
            console.log(textStatus);
            console.log(errorThrown);
            options.textStatus = textStatus;
            options.errorThrown = errorThrown;
            if ( error ) error.apply(this, arguments);
        };


        //make request
        var xhr = options.xhr = Backbone.ajax(_.extend(params, options));
        debugger;
        console.log(xhr);
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