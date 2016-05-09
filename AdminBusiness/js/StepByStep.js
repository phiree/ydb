(function (factory){
    if ( typeof define === "function" && define.amd ){
        define(["jquery"], function($){
            factory($);
        })
    } else {
        factory(jQuery);
    }
})(function($){
    var pluginName = "stepByStep";

    var StepByStep = function(ele, option){
        this.$ele = $(ele);
        this.options = $.extend({}, StepByStep.DEFAULTS, option);
        this.init();
    };

    StepByStep.DEFAULTS = {
        defaultStep : 0,
        stepTipsSelect: ".steps-tips",
        stepTipSelect: ".steps-tip",
        stepListSelect: ".steps-list",
        stepItemSelect: ".steps-step",
        stepLinesSelect: ".steps-lines",
        stepPrevSelect: ".step-prev",
        stepNextSelect: ".step-next",
        stepSaveSelect: ".step-save",
        stepCancelSelect: ".step-cancel",
        stepCtrlSelect: ".step-ctrl",
        curStepSelect : ".cur-step",
        stepPrevFunc: null,
        stepNextFunc: null,
        stepValid: null,
        stepLastFunc: null,
    }

    $.extend(StepByStep.prototype, {
        init: function(){
            var defaultStep = this.options.defaultStep;
            this.build()._delegate();

            if ( typeof defaultStep === "function" && defaultStep ){ defaultStep = defaultStep(); }
            if ( typeof defaultStep === "string" ) defaultStep = parseInt(defaultStep);
            this.doneStep = (( defaultStep - 1 ) < 0) ? 0 : defaultStep;
            this.setStep(defaultStep);

            return this;
        },
        build: function () {
            this.$stepTips = this.$ele.find(this.options.stepTipsSelect);
            this.$stepList = this.$ele.find(this.options.stepListSelect);
            this.$stepLines = this.$ele.find(this.options.stepLinesSelect);
            this.$stepCtrl = this.$ele.find(this.options.stepCtrlSelect);
            this.$stepPrev = this.$ele.find(this.options.stepPrevSelect);
            this.$stepNext = this.$ele.find(this.options.stepNextSelect);
            this.totalStep = this.$stepList.find(this.options.stepItemSelect).length;
            return this
        },
        _delegate: function () {
            var _this = this;

            this.$stepPrev.on("click", function(){
                _this.curStep = _this.$stepList.find(_this.options.curStepSelect).index();
                if ( _this.curStep > 0 ){
                    _this.setStep( _this.curStep - 1 );
                }
                if ( _this.options.stepPrevFunc && typeof _this.options.stepPrevFunc === "function" ) _this.options.stepPrevFunc(_this.curStep - 1);
            });

            this.$stepNext.on("click", function(){
                _this.curStep = _this.$stepList.find(_this.options.curStepSelect).index();
                if ( typeof _this.options.stepValid === "function" && _this.options.stepValid ){
                    if (_this.options.stepValid() ){ stepNext(); }
                } else {
                    stepNext();
                }
            });

            function stepNext(){
                if ( _this.curStep < _this.totalStep ){
                    _this.setStep(_this.curStep + 1);
                    if ( _this.options.stepNextFunc && typeof _this.options.stepNextFunc === "function" ) _this.options.stepNextFunc(_this.curStep + 1);
                }
            }
        },
        setStep : function(step){
            var $steps = this.$stepList.find(this.options.stepItemSelect);

            $steps.eq(step).addClass("cur-step").siblings().removeClass("cur-step");

            this._setStepTips(step)._setStepLines(step)._setStepCtrl(step);
        },
        _setStepTips: function(step){
            var doneStep;
            this.doneStep = doneStep = (( step - 1 ) < 0) ? 0 : step;
            var $tips = this.$stepTips.find(this.options.stepTipSelect);
            $tips.eq(step).addClass("cur-step").siblings().removeClass("cur-step").removeClass("done-step")
                .slice(0 , doneStep ).addClass("done-step");
            return this;
        },
        _setStepLines: function(step){
            var $lines = this.$stepLines.find(this.options.stepLinesSelect);

            // 步骤线控制
            if ( !this.$stepLines.length ) {
                if ( step > 0 ){
                    // 步骤线的数量为步骤-1
                    var lineStep = (( step - 1 ) < 0 ) ? 0 : step - 1;
                    var lineDoneStep = ((lineStep - 1) < 0 ) ? 0 : lineStep;

                    $lines.eq(lineStep).addClass("cur-step").siblings().removeClass("cur-step")
                        .find('.steps-line').slice(0 , lineDoneStep ).addClass("done-step")
                        .find('.steps-line').slice( lineDoneStep , -1 ).removeClass("done-step");
                } else {
                    $lines.find('.steps-line').removeClass("cur-step");
                }
            }
            return this;
        },
        _setStepCtrl: function(step){
            // 步骤控制区域样式控制，设置当前为第几步
            if(this.$stepCtrl.get(0).className.match(/(step\-\d)/)){
                var ctrlMatchClass = this.$stepCtrl.get(0).className.match(/(step\-\d)/)[0];
                this.$stepCtrl.removeClass(ctrlMatchClass).addClass("step-" + (step + 1));
            } else {
                this.$stepCtrl.addClass("step-" + (step + 1));
            }

            return this;
        }
    });

    $.fn[pluginName] = function(option){
        return this.each(function(){
            var data = $.data(this, "stepByStep");
            var options = $.extend({}, typeof option === 'object' && option);

            if (!data){
                $.data(this, "stepByStep", data = new StepByStep(this, options));
            }

            if (typeof option === "string"){
                data[option]();
            }

        })
    }
});