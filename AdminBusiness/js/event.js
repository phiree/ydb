(function(){
    var _Event = {
        _events : []
    };


        /**
         * 自定义事件，参照Backbone的事件方式实现
         * @param name : event action name.
         * @param callback : event trigger callback.
         * @param context : trigger callback params.
         * @returns {_Event}
         */
    _Event.on = function(name, callback, context){
            if (!eventsApi(this, name, "on", [callback, context]) || !callback) return false;
            this._events || (this._events = {});
            var events = this._events[name] || (this._events[name] = []);
            events.push({callback: callback, context: context, ctx: context || this});
            return this;
        };
    _Event.once = function(name, callback, context){
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
    _Event.off = function(name, callback, context){
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
    _Event.trigger = function(name){
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

    if ( this._Event !== "function" ){
        this._Event = _Event;
    }
})();