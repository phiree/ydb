

(function($) {
    'use strict';

    var TimeTable = function(element, options) {
        this.$element = $(element);
        this.options = $.extend({}, TimeTable.DEFAULTS, options);
        this.transitioning = null;

        this.init();
    };

    TimeTable.TRANSITION_DURATION = 350;

    TimeTable.DEFAULTS = {
        scorllable: true,
        splitTask: true,
        splitUnitWidth: 44,
        row: 7,
        colunm: 24,
        colHeadTemplate: "<div></div>",
        unitWidth: 90,
        startTime: 9,
        taskData : {}
    };

    TimeTable.prototype.init = function() {

        var $this = this;

        $this.tablebuild();
        $this.printTask(this.options.taskData);
        $this.setStartTime();
    };

    TimeTable.prototype.tablebuild = function(){
        var $this =  $(this.$element);
        var $ttBody = $this.find('.tt-body');
        var $ttHead = $this.find('.tt-head');
        var $ttHeadColumns = $this.find('.tt-head').find('.tt-head-columns');
        var $ttFgColumns = $this.find('.tt-body').find('.tt-body-foreground');
        var $ttBgRows = $this.find('.tt-body').find('.tt-body-background');
        var $ttBodyColumns = $this.find('.tt-body').find('.tt-body-columns');
        var $ttBodyRows = $this.find('.tt-body').find('.tt-body-rows');

        //同步头部滚动
        if ( this.options.scorllable ) {
            $ttBody.width(this.options.unitWidth * this.options.colunm);
            $ttHead.width(this.options.unitWidth * this.options.colunm);

            var $ttBodyScr = $this.find('.tt-body-scrollable');
            var $ttHeadScr = $this.find('.tt-head-scrollable');
            $ttBodyScr.on("scroll",function(){
                $ttHeadScr.scrollLeft($ttBodyScr.scrollLeft());
            })
        }


        for ( var i = 0 ; i < this.options.colunm ; i++ ) {
            $ttHeadColumns.append($("<div class='tt-head-column'>"+ i + ":00" +"</div>").width(this.options.unitWidth));
            $ttBodyColumns.append($("<div class='tt-fg-column'></div>").width(this.options.unitWidth));
        }

        for ( var i = 0 ; i < this.options.row ; i++ ) {
            i%2 ? $ttBgRows.append("<div class='tt-row tt-row-odd'></div>") : $ttBgRows.append("<div class='tt-row tt-row-even'></div>");
            $ttBodyRows.append("<div class='tt-row'><div class='tt-row-content'></div></div>")
        }



    };

    TimeTable.prototype.printTask = function(data){
        var $this = $(this.$element);
        var $taskRow = $this.find('.tt-body-rows').find('.tt-row').find('.tt-row-content');

        for( var i = 0 ; i < data.length ; i++ ){
            var rowNum = data[i].weekday;

            var arrTask = this.createTask(data[i]);
            for ( var j = 0 ; j < arrTask.length ; j++ ) {
                $taskRow.eq(rowNum).append(arrTask[j]);
            }

        }

    };

    TimeTable.prototype.createTask = function(data){

        var openTime = data.openTime;
        var arrTask = [];

        var format = function (time) {
            var hour = parseInt( time.split(":")[0] ,  10) ;
            var minite = parseInt( time.split(":")[1] ,  10) ;

            return new Array(hour,minite);
        };

        for (var i = 0 ; i < openTime.length ; i++ ){
            var beginTime = data.openTime[i].beginTime;
            var endTime = data.openTime[i].endTime;

            var arrBeginTime = format(beginTime);
            var arrEndTime = format(endTime);

            if ( this.options.splitTask ) {

                var splitNum = arrEndTime[0] - arrBeginTime[0];
                for( var j = 0 ; j < splitNum ; j++) {

                    var task = $('<div class="tt-task tt-task-split"></div>');
                    var splitUnitZoom = this.options.splitUnitWidth/this.options.unitWidth;
                    var splitUnitLeft = ((arrBeginTime[0] * this.options.unitWidth) + j * this.options.unitWidth) + (( 1 - splitUnitZoom )/2 * this.options.unitWidth);

                    task.css({
                        left : splitUnitLeft,
                        width : this.options.splitUnitWidth
                    });

                    arrTask.push(task);
                }

            } else {
                var task = $('<div class="tt-task"></div>');
                task.css({
                    left : arrBeginTime[0] * this.options.unitWidth,
                    width : (arrEndTime[0] - arrBeginTime[0]) * this.options.unitWidth
                });

                arrTask.push(task);
            }

        }

        return arrTask;

    };

    TimeTable.prototype.setStartTime = function(){
        var $this =  $(this.$element);
        var $ttBodyScr = $this.find('.tt-body-scrollable');
        var $ttHeadScr = $this.find('.tt-head-scrollable');
        var startTime = this.options.startTime * this.options.unitWidth;
            $ttHeadScr.scrollLeft(startTime);
            $ttBodyScr.scrollLeft(startTime);
    }

    function Plugin(option) {
        return this.each(function() {
            var $this = $(this);
            var data = $this.data('mm');
            var options = $.extend({},
                TimeTable.DEFAULTS,
                $this.data(),
                typeof option === 'object' && option
            );

            if (!data) {
                $this.data('mm', (data = new TimeTable(this, options)));
            }
            if (typeof option === 'string') {
                data[option]();
            }
        });
    }

    var old = $.fn.TimeTable;

    $.fn.TimeTable = Plugin;
    $.fn.TimeTable.Constructor = TimeTable;

    $.fn.TimeTable.noConflict = function() {
        $.fn.TimeTable = old;
        return this;
    };

})(jQuery);
