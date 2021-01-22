(function(){
    function Error(txt) {
        throw new Error(txt);
    }

    /**
     * 跟Chart图表关联，用于ajax获取数据的按钮类，
     * @param element string 按钮selector
     * @param options object 设置项
     *      .chart chartJS 实例
     *      .chartData chartJS实例的data设置
     *      .chartOptions chartJS实例的设置项
     *      .chartLabelFormat label格式化函数
     *      .reqOptions ajax的option设置项
     * @constructor
     */
    function LineBtn(element, options) {
        this.el = element;
        this.$el = $(element);
        this.options = options || {};
        this.chart = options.chart || Error("no a chart");
        this.chartData = options.chartData || Error("no a chart data");
        this.chartOptions = options.chartOptions || Error("no a chart options");
        this.reqOptions = options.reqOptions ;

        this.init();
    }

    $.extend(LineBtn.prototype, {
        init: function () {
            this.bind()
        },
        bind: function () {
            var that = this;
            var clickBind = this.options.clickBind;

            this.$el.on("click", function (e) {

                if ( typeof clickBind === "function" ){
                    clickBind(e)
                }

                that.fetch(e)
            })
        },
        fetch: function(e){
            var that = this;
            var options = $.extend({}, this.reqOptions);
            var url = this.reqOptions.url || Error("fetch URL require");

            var def = $.ajax(url, options);

            def.done(function(resp, state, xhr){
                if ( state === "success") {
                    that.refreshChart(resp)
                }
            })
        },
        refreshChart: function(resp){
            var chart = this.options.chart;
            var chartData = this.options.chartData;
            var chartOptions = this.options.chartOptions;
            var format = this.options.chartDataFormat;
            var value = resp.XYValue;

            if ( typeof _.isObject(value) ) {
                chartData.labels = _.keys(value);
                chartData.datasets[0].data = _.values(value);

                if ( typeof format === "function") {
                    var formatData = format(chartData.labels, chartData.datasets[0].data);
                    chartData.labels = formatData.labels;
                    chartData.datasets[0].data = formatData.data;
                }

                chart.data = chartData;
                chart.update();
            }
        }
    });

    if ( typeof window.LineBtn === "function") {
        var old = window.LineBtn;
        window.LineBtn = LineBtn;
        window.LineBtn.noConflict = function () {
            window.LineBtn = old;
        }
    };

    return window.LineBtn = LineBtn
})();
