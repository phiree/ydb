;(function($,window,document,undefined){

    var TimeSelect = function(ele,opt){
        this.$element = ele,
        this.defaults = {
            "timeSelectM": "time-select-m",
            "timeSelect": "time-select",
            "timeTrigger" : "time-trigger",
            "valueInput" : "time-value",
            "confirm" : "time-confirm",
            "close" : "time-close",
            "square" : ":",
            "hour" : "00",
            "min" : "00"
        }
        this.options = $.extend({},this.defaults,opt);
    }

    TimeSelect.prototype = {
        init : function(){



            var protoThis = this;
            var _this = $(this.$element);
            var _trigger = _this.find("." + protoThis.options.timeTrigger);
            var _printValue = _this.find("." + protoThis.options.valueInput);
            var _selectBox = $("<div class='" + protoThis.options.timeSelectM +"'></div>")
            var _hourSelcet = $("<div class='d-inb " + protoThis.options.timeSelect + "'></div>");
            var _minSelcet = $("<div class='d-inb " + protoThis.options.timeSelect + "'></div>");
            var _close = $("<input type='button' class='" + protoThis.options.close + "' value='取消'/>");
            var _confirm = $("<input type='button' class='" + protoThis.options.confirm + "' value='确定'/>");
            var _btnContainer = $("<div class='time-select-btn'></div>")
            var _selectContainer = $("<div class='time-select-container'></div>")
            var _hourUnit = $("<span class='hour-unit'>时</span>");
            var _minUnit = $("<span class='min-unit'>分</span>");

            var _hourPrint = "00";
            var _minPrint = "00";
            var _defHour = "00";
            var _defMinu = "00";


            if( _printValue.attr("value") != "" && _printValue.attr("value") != undefined  ){
                var arr = _printValue.attr("value").split(protoThis.options.square);
                _defHour = arr[0];
                _defMinu = arr[1];
                _trigger.html(_defHour + ":" + _defMinu);
                _hourPrint = _defHour;
                _minPrint = _defMinu;
            } else {
                protoThis.options.hour != "00" ? _defHour = protoThis.options.hour : _defHour = "00";
                protoThis.options.min != "00" ? _defMinu = protoThis.options.min : _defMinu = "00";
                _printValue.attr("value",_hourPrint + ":" + _minPrint);
                _trigger.html(_defHour + ":" + _defMinu);
            }

            protoThis.createSelect(_hourSelcet,24,null,_defHour,function(val){_hourPrint = val});
            protoThis.createSelect(_minSelcet,59,5,_defMinu,function(val){_minPrint = val});

            _trigger.click(function(){

                _selectContainer.append(_hourSelcet);
                _selectContainer.append(_hourUnit);
                _selectContainer.append(_minSelcet);
                _selectContainer.append(_minUnit);
                _btnContainer.append(_confirm);
                _btnContainer.append(_close);
                _selectBox.append(_selectContainer);
                _selectBox.append(_btnContainer);
                _this.prepend(_selectBox);
            })

            var mouseOut = false;
            _selectBox.bind("mouseover mouseout",function(e){
                if (e.type == "mouseover"){
                    mouseOut = false;
                }else if(e.type == "mouseout"){
                    mouseOut = true;
                }
            });

            _trigger.bind("mouseover mouseout",function(e){

                if (e.type == "mouseover"){
                    mouseOut = false;
                }else if(e.type == "mouseout"){
                    mouseOut = true;
                }
            })

            $(document).click(function () {
                if (mouseOut) {
                    _selectBox.detach();
                }
            });


            _close.click(function(){
                _selectBox.detach();
            });

            _confirm.click(function(){
                _printValue.attr("value",_hourPrint + ":" + _minPrint).focus().blur();
                _trigger.html(_hourPrint + ":" + _minPrint);
                _selectBox.detach();
            });
        },

        createSelect : function(selectCtn,range,step,defVal,printFunc){
            return selectCtn.each(function () {
                var _cite = $("<cite></cite>"),
                    _selectList = $("<ul></ul>"),
                    _step = step ? step : 1;


                function fix(num, length) {
                    return ('' + num).length < length ? ((new Array(length + 1)).join('0') + num).slice(-length) : '' + num;
                }

                for ( var i = 0 ; i < range ; i += _step ) {
                    var _selectListItem = $("<li></li>"),
                        _selectListLink = $("<a></a>");

                    var val = fix(i,2);
                    _selectListLink.attr({
                        value : val,
                        href : "javascript:void(0)"
                    });
                    _selectListLink.html(val);
                    _selectListLink.appendTo(_selectListItem);
                    _selectListItem.appendTo(_selectList);

                    _selectListLink.click(function () {
                        //console.log($(this).attr("value"))
                        _cite.html($(this).attr("value"));
                        printFunc($(this).attr("value"));

                        _selectList.hide();
                    });
                }

                defVal != "00" ? _cite.html(fix(defVal,2)) : _cite.html("00");

                $(this).append(_cite);
                $(this).append(_selectList);


                _cite.click(function () {
                    if (_selectList.css("display") == "none") {
                        _selectList.slideDown("fast");
                    } else {
                        _selectList.slideUp("fast");
                    }
                });

                var mouseOut = true;
                $(this).bind("mouseover mouseout",function(e){
                    if (e.type == "mouseover"){
                        mouseOut = false;
                    }else if(e.type == "mouseout"){
                        mouseOut = true;
                    }
                });

                $(document).click(function () {
                    if (mouseOut) {
                        _selectList.hide();
                    }
                });

            });
        },



    }

    $.fn.timeSelect = function(options){
        return this.each(function(){
            var timeSelect = new TimeSelect(this,options);
            return timeSelect.init();
        })
    }


})(jQuery,window,document);




