
/*!
 * $.createPlugin(pluginName, pluginImplementation)
 *
 * jQuery Plugin Creation Method (boilerplate plugin implementations)
 * This script defines a new jQuery method called "createPlugin",
 * used to easily create new jQuery plugins within the "$.fn" plugin scope.
 *
 * This method returns nothing, and receives two required parameters:
 *   [string] pluginName: the unique plugin name.
 *   [object] pluginImpl: the object containing the plugin implementation.
 *
 * The method tries to easily and clearly decouple the code used to implement
 * the business logic (as of the plugin implementation) from the code required
 * to implement some of the jQuery's best practices when creating new jQuery plugins.
 *
 * ====== Sample plugin implementation object: ======
 *
 * // Creates all the business logic of your plugin (aka your plugin implementation object)
 * var testPluginCode = {
 *     _defaults: {}, // you can put your plugin defaults in here.
 *
 *     init: function() {
 *         // plugin initialization goes here.
 *         // this method is called just once per instance.
 *         // you can use this.element to access the HTML element on which the plugin was called at.
 *         // this.$element is the same as $(this.element).
 *         // this.options represents the options object passed in during plugin initialization, merged with the _defaults value defined for your plugin.
 *         // this._name represents the name on which the plugin was registered at the $.fn scope.
 *     },
 *
 *     doSomething: function(paramA, paramB) {
 *         // plugin business logic for method "doSomething".
 *     }
 * };
 *
 * // Creates your plugin, which will be called "myPlugin",
 * // using the "testPluginCode" object as the plugin implementation code.
 * (function($){
 *     $.createPlugin('myPlugin', testPluginCode);
 * }(jQuery));
 *
 * // Now, you can use your plugin the same way you'd use any other jQuery plugin:
 *
 * var myDiv = $('#testDiv');               // gets a jQuery object representing a specific DOM element
 * myDiv.myPlugin({pluginData: null});      // initializes your plugin for the resulting jQuery object
 * myDiv.myPlugin('doSomething', 1, {});    // executes the "doSomething" method of your plugin
 *
 **************************************************************
 * Copyright (c) 2013-2013 M. Anderson (anderson.smiranda at gmail.com)
 * Version: 3.0.0 (10-OCT-2013)
 * Dual licensed under the MIT and GPL licenses.
 * Requires: jQuery v1.7.1 or later
 *
 * This piece of code was inspired by this great post from Addy Osmani (2011-OCT-11):
 * http://coding.smashingmagazine.com/2011/10/11/essential-jquery-plugin-patterns/
 */

;(function($, window, document, undefined) {
    $.createPlugin = function(pluginName, pluginImpl) {

        // basic error log function. outputs its arguments to the console.log, if it exists.
        // (if the user browser is not showing its console window, this function does nothing)
        function log() {
            if (window.console && console.log)
                console.log('[createPlugin] ' + Array.prototype.join.call(arguments,' // '));
        }

        // basic function to help identifying javascript native (system) types,
        // returns true for arrays, dates, strings, numbers, booleans, nulls, undefined and functions.
        function isSystemType(o) {
            return (typeof o !== 'object' || Object.prototype.toString.call(o).slice(8, -1) !== 'Object');
        }

        // validates the method's input parameters
        if (typeof pluginName !== 'string' || !pluginName)
        {
            log('Error: you must specify a valid plugin name on the first parameter of the method.', pluginName);
            return;
        }
        if (pluginImpl == undefined || !pluginImpl || isSystemType(pluginImpl))
        {
            log('Error: you must specify a valid plugin implementation on the second parameter of the method.', pluginImpl);
            return;
        }

        // validates if there's a plugin already defined with this plugin name.
        if (typeof $.fn[pluginName] === 'function')
        {
            log('Error: there\'s already a jQuery plugin defined with this name.', pluginName, $.fn[pluginName].constructor);
            return;
        }

        // Object.create support test,
        // and fallback implementation for browsers without native support for it.
        if (typeof Object.create !== 'function') {
            Object.create = function(o) {
                function F() {}
                F.prototype = o;
                return new F();
            };
        }

        // default (base, or abstract) jQuery plugin implementation
        var jQueryPlugin = {
            _name: '',
            _defaults: {},
            _init: function(pluginName, element, options) {
                this._name = pluginName;
                this.element = element;
                this.$element = $(element);
                this.options = $.extend({}, this._defaults, options)

                if (typeof this.init === 'function')
                    this.init();
            }
        };

        // jQuery plugin creation process.
        $.fn[pluginName] = function(options) {

            // slice arguments to leave only arguments after function name.
            var args = Array.prototype.slice.call(arguments, 1);

            // Cache any plugin method call, to make it possible to return a value
            var results;

            // Creates the plugin instance and tries to call its "init" function (once),
            // As well as returning the created plugin instance for subsequent calls.
            // This method also tries to execute plugin methods,
            // as long as the first argument is a string containing a valid method name.
            this.each(function() {
                var element = this, $item = $(element), pluginKey = 'plugin_' + pluginName, instance = $.data(element, pluginKey);

                // if there's no plugin instance for this element, create a new one, calling its "init" method, if it exists.
                if (!instance) {
                    var pluginType = $.extend({}, jQueryPlugin, pluginImpl);
                    instance = $.data(element, pluginKey, Object.create(pluginType));
                    if (instance && typeof instance._init === 'function')
                        instance._init.apply(instance, [pluginName, element, options]);
                }

                // if we have an instance, and as long as the first argument (options) is a valid string value, tries to call a method from this instance.
                if (instance && typeof options === 'string' && options[0] !== '_' && options !== 'init') {

                    var methodName = (options == 'destroy' ? '_destroy' : options);
                    if (typeof instance[methodName] === 'function')
                        results = instance[methodName].apply(instance, args);

                    // Allow instances to be destroyed via the 'destroy' method
                    if (options === 'destroy')
                        $.data(element, pluginKey, null);
                }
            });

            // If the earlier cached method gives a value back, return the resulting value, otherwise return this to preserve chainability.
            return results !== undefined ? results : this;
        };
    };
}(window.jQuery, window, document));

// ***************** END // $.createPlugin() // ************************


// Sample Code:
//
// Defines a new test plugin implementation...
var StepByStep = {

    // this is your plugin defaults. you're not required to provide it.
    _defaults: {
        defaultStep: 0,
        totalStep: 3,
        stepWrap: "steps-wrap",
        stepTipsClass: "steps-tips",
        stepListClass: "steps-list",
        stepLinesClass: "steps-lines",
        stepPrevFunc: null,
        stepNextFunc: null,
        stepPrev: "step-prev",
        stepNext: "step-next",
        stepLastFunc: null
    },

    // this is your plugin initialization. you're not required to provide it.
    init: function() {
        // Plugin initialization... this is private! (or protected, if you wish)
        // Also note that you're not required to provide a init method!
        this._stepBuild();
    },
    _stepBuild: function (){
        var $ele = this.$element,
            _this = this,
            $stepTips = $ele.find("." + this.options.stepTipsClass),
            $stepLists = $ele.find("." + this.options.stepListClass),
            $stepPrev = $ele.find("." + this.options.stepPrev),
            $stepNext = $ele.find("." + this.options.stepNext),
            defaultStap = this.options.defaultStep,
            totalStep = this.options.totalStep,
            stepNextFunc = this.options.stepNextFunc;

        this.setStep(0);

        $stepPrev.on("click",function(){
            var currentStep = $stepLists.find(".cur-step").index();
            if ( currentStep > 0  ) {
                _this.setStep( currentStep - 1 );
            }
        });
        $stepNext.on("click",function(){
            if ( stepNextFunc() ){
                var currentStep = $stepLists.find(".cur-step").index();
                if ( currentStep < totalStep  ) {
                    _this.setStep( currentStep + 1 );
                }
            }
        });


    },
    setStep: function(step){
        var $ele = this.$element,
            $stepTips = $ele.find("." + this.options.stepTipsClass),
            $stepLines = $ele.find("." + this.options.stepLinesClass),
            $stepLists = $ele.find("." + this.options.stepListClass),
            $stepPrev = $ele.find("." + this.options.stepPrev),
            $stepNext = $ele.find("." + this.options.stepNext);

        var doneStep = (step - 1 ) < 0 ? 0 : step;


        $stepTips.find('.steps-tip').eq(step).addClass("cur-step").siblings().removeClass("cur-step");
        $stepLists.find('.steps-step').eq(step).addClass("cur-step").siblings().removeClass("cur-step");


        $stepTips.find('.steps-tip').slice(0 , doneStep ).addClass("done-step");
        $stepTips.find('.steps-tip').slice( doneStep , -1 ).removeClass("done-step");

        if ( step > 0 ){
            var lineStep = ( step - 1 ) < 0 ? 0 : step - 1 ;
            var lineDoneStep = (lineStep - 1 ) < 0 ? 0 : lineStep;
            $stepLines.find('.steps-line').eq(lineStep).addClass("cur-step").siblings().removeClass("cur-step");
            $stepLines.find('.steps-line').slice(0 , lineDoneStep ).addClass("done-step");
            $stepLines.find('.steps-line').slice( lineDoneStep , -1 ).removeClass("done-step");
        }




        switch(step)
        {
            case 0:
                $stepNext.removeClass("dis-n");
                $stepPrev.addClass("dis-n");
                break;
            case $stepLists.find('.steps-step').length - 1:
                $stepPrev.removeClass("dis-n");
                $stepNext.addClass("dis-n");
                this.options.stepLastFunc();
                break;
            default:
                $stepPrev.removeClass("dis-n");
                $stepNext.removeClass("dis-n");
        }
    },

    DoneStep: function(){

    },
    // this is your plugin action. implement it as you wish/need/want.
    doSomething: function(text) {
        // Public plugin action... the user can call this!

    },
    // This one is private. the user will not be able to call it!
    _myPrivateMethod: function() { },

    // this is your plugin "destructor", but you're not required to provide it.
    _destroy: function() {
        // This one is a bonus!
        // Here you can provide the code to actually "destroy" your plugin.
        // This will be called whenever the user calls your plugin with
        // the "destroy" method.
        this.$element.append('destroyed!<br/>');
    }
}
//
// Creates a new plugin called "myPlugin",
// which implements the "StepByStep" definitions above.
$.createPlugin('StepByStep', StepByStep);
//
// Now we just use "myPlugin" as any regular jQuery plugin...
// The plugin initialization is called just once,
// "automagically" on your first plugin call,
// for each element that matches the specified selector.
