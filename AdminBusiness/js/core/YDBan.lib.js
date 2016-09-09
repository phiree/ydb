/**
 * YDBan.lib.js
 *
 * (c) 2016 licdream@126.com, JSYK.
 * this is a simple lib of tools.
 *
 * Under GPL license.
 *
 */
(function(root, factory){

    // AMD, require.js
    if (typeof define === 'function' && define.amd){
        define(['jquery'], function($){
            root.YDBan = factory(root, {}, $)
        });

    // CommonJS, node.js
    } else if(typeof exports !== 'undefined') {
        var $ = require('jquery');
        root.YDBan = factory(root, {}, $);

    // normal
    } else {
        root.YDBan = factory(root, {}, (root.jQuery || root.$ ));
    }
})(this, function(root, YDBan, $){

    var old = root.YDBan;

    YDBan.VERSION = "0.0.1";

    YDBan.$ = $;

    YDBan.noConflict = function(){
        root.YDBan = old;
        return this;
    };

    /**
     * 拓展工具库
     * @description custom tool to achieve extend feature.
     */
    var tools = YDBan.tools = (function(){
        var _tools = {};

        // 对象扩展方法
        _tools.extend = function(){
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
                        if ( copy && ( (copyIsArray = _tools.isArray(copy) ) || _tools.isObject(copy)  ) ) {
                            if ( copyIsArray ){
                                copyIsArray = false;
                                clone = src && _tools.isArray(src) ? src : [];
                            } else {
                                clone = src && _tools.isObject(src) ? src : {};
                            }

                            target[ name ] = _tools.extend(clone, copy);
                        } else if ( typeof copy !== "undefined" ){
                            target[ name ] = copy;
                        }


                    }
                }
            }

            return target;
        };

        // 数组类型判断
        _tools.isArray = Array.isArray || function(obj){
                return Object.prototype.toString.call(obj) === '[object Array]';
            };

        // 纯对象类型判断
        _tools.isObject = function(obj){
            var type = typeof  obj;
            return type === 'function' || type === 'object' && !!obj;
        };

        return _tools;
    })();

    /**
     * event.js v1.0.0 @ 2016-05-17 by licdream@126.com
     * 自定义事件工具
     */
    YDBan.event = (function(){
        var _event = {
            _events : []
        };

        /**
         * 自定义事件，参照Backbone的事件方式实现
         * @param name : event action name.
         * @param callback : event trigger callback.
         * @param context : trigger callback params.
         * @returns {_event}
         */
        _event.on = function(name, callback, context){
            if (!eventsApi(this, name, "on", [callback, context]) || !callback) return false;
            this._events || (this._events = {});
            var events = this._events[name] || (this._events[name] = []);
            events.push({callback: callback, context: context, ctx: context || this});
            return this;
        };
        _event.once = function(name, callback, context){
            if (!eventsApi(this, name, "once", [callback, context]) || !callback) return false;
            var self = this;
            var triggerFunc = function(){
                self.off(name, once);
                callback.apply(this, arguments);
            };
            var once = function(){
                triggerFunc();
                triggerFunc = null;
            };

            return this.on(name, once, context);
        };
        _event.off = function(name, callback, context){
            if (!eventsApi(this, name, "off", [callback, context]) || !callback) return false;
            if(!name && !callback && !context){
                this._events = void 0;
                return this;
            }

            var names = name ? [name] : getEventKeys(this._events);
            for (var i = 0, length = names.length; i < length; i++){
                name = names[i];

                var events = this._events[name];
                if (!events) continue;

                if (!callback && !context){
                    delete this._event[name];
                    continue;
                }

                // 过滤出初callback之外的绑定的剩余函数
                var remaining = [];
                for (var j = 0, k = events.length; j < k; i++){
                    var event = events[j];
                    if (
                        callback && callback !== event.callback ||
                        context && context !== event.context
                    ) {
                        remaining.push(event);
                    }
                }

                if (remaining.length){
                    this._events[name] = remaining;
                } else {
                    delete this._events[name];
                }
            }
            return this;
        };
        _event.trigger = function(name){
            if (!this._events) return this;
            var args = [].slice.call(arguments, 1);

            var events = this._events[name];
            var allEvents = this._events.all;
            if(events) triggerEvents(events, args);
            if(allEvents) triggerEvents(allEvents, arguments);
        };

        var eventSplitter = /\s+/;

        function getEventKeys(events){
            if ( typeof events !== "object" ) return;
            var keys = [];
            for (var key in events){
                Object.prototype.hasOwnProperty.call(events, key) && keys.push(key);
            }
            return keys;
        }

        function eventsApi(obj, name, action, reset){
            if ( typeof name === "object" ){
                for(var key in name){
                    obj[action].apply(obj, [key, name[key]].concat(reset));
                }
                return false;
            }

            if ( eventSplitter.test(name)){
                var names = name.split(eventSplitter);
                for (var i = 0, length = names.length; i < length; i++){
                    obj[action].apply(obj, [names[i]].concat(reset));
                }
                return false;
            }
            return true;
        }

        var triggerEvents = function(events, args){
            var ev, i = -1, l = events.length, a1 = args[0], a2 = args[1], a3 = args[3];

            switch(args.length){
                case 0: while (++i < l) (ev = events[i]).callback.call(ev.ctx);
                    return;
                case 1: while (++i < l) (ev = events[i]).callback.call(ev.ctx, a1);
                    return;
                case 2: while (++i < l) (ev = events[i]).callback.call(ev.ctx, a1, a2);
                    return;
                case 3: while (++i < l) (ev = events[i]).callback.call(ev.ctx, a1, a2, a3);
                    return;
                default: while (++i < l) (ev = events[i]).callback.apply(ev.ctx, args);
                    return;
            }
        };

        if ( root.event !== "function" ){
            return _event;
        }
    })();

    /**
     * url 工具
     * @type {YDBan.url}
     */
    YDBan.url = (function(){
        var urlTools = {};

        /**
         * tool to get parameter by name form url
         * @param name parameter name
         * @param url url location
         * @returns {*}
         */
        urlTools.getUrlParam = function(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        };

        return urlTools;
    })();

    return YDBan;
});