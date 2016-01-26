(function(factory){
    if ( typeof define === "function" && define.amd ){
        define(["backbone"], function(backbone){
            return factory(backbone);
        });
    } else  {
        factory ();
    }
}(function(backbone){
    var templateSetting = {
        interpolate: /\{%=(.+?)%\}/g,
        escape:      /\{%-(.+?)%\}/g,
        evaluate:    /\{%(.+?)%\}/g
    };
    // 时间段Model
    var TimeBucket = Backbone.Model.extend({
        defaults : {
            timeBucketId : 123,
            timeStart : "10:00",
            timeEnd : "12:00",
            maxNum : 5,
            doneNum : 2,
            timeEnable : true,
            arrayGoods : []
        },
        initialize : function(options){
            this.sortGoods();
        },
        enableTime : function(){
            if ( !this.get('timeEnable') ) {
                this.save({timeEnable : !this.get('timeEnable')});
            }
        },
        disableTime : function(){
            if ( !!this.get('timeEnable') ) {
                this.save({timeEnable : !this.get('timeEnable')});
            }
        },
        addGoodNum : function(Num){
            this.save({maxNum : this.get('maxNum') + Num});
        },
        setMaxNum : function(maxNum){
            if ( !!maxNum && ( maxNum > this.get('doneNum')) ){
                this.save({maxNum : maxNum});
            }
        },
        //goods按是否可以预定进行排序，已预订(ordered = true)放在前面;
        sortGoods : function(){
            var sortGoods = _.sortBy(this.get('arrayGoods') , function(good){
                return !good.ordered;
            });
            this.set('arrayGoods', sortGoods);
        },
        addGoods : function(num){
            var newGood = {
                ordered : false,
                orderId : '#'
            };

            //复制数组为新数组，否则无法触发change事件
            var currentGoods = this.get('arrayGoods').slice();
            for ( var i = 0 ; i < num ; i++ ){
                currentGoods.push(newGood);
            }

            var parentDate = this.get('parentDate');
            this.save('arrayGoods', currentGoods , {
                customAPI : true,
                protocolCode : 'GOOD001006',
                data : {
                    parentDate : parentDate
                }
            });
        },
        deleteGoods : function(num){
            //当未预约订单数大于减数时,才可以进行订单删除操作
            var goods = _.countBy(this.get('arrayGoods') , function(good){
                return !(good.ordered == false) ? 'ordered' : 'notOrdered';
            });
            if ( !goods.notOrdered || (goods.notOrdered < num) ) return;
            var currentGoods = this.get('arrayGoods').slice();
            for ( var i = 0 ; i < num ; i++ ){
                currentGoods.pop();
            }
            this.save('arrayGoods' , currentGoods);
        }
    });

    var TimeBuckets = Backbone.Collection.extend({
        model : TimeBucket,
        initialize : function(){

        }
    });

    // 时间段View
    var TimeBucketView = Backbone.View.extend({
        tagName : 'div',
        className : 'time-bucket',
        template : _.template($("#timeBucket_template").html(),templateSetting),
        goodsTemplate : _.template($("#goods_template").html(),templateSetting),
        events :　{
            'click .multiAdd' : 'addGoods',
            'click .multiDelete' : 'deleteGoods',
            'click .deleteGood' : 'deleteOneGood'
        },
        initialize : function(){
            //this.listenTo(this.model, "change" , this.render);
            this.listenTo(this.model, "change:arrayGoods" , this. refreshGoodsView);
            this.initGoodList();
        },
        render : function(){
            this.$el.html(this.template(this.model.toJSON()));

            this.$el.find('.t-b-window').each(function(){
                var $this = $(this);
                var goodPrev = $this.find('.good-prev');
                var goodNext = $this.find('.good-next');
                var goodWrap = $this.find('.good-list-wrap');

                goodPrev.bind( 'click', {action : 'prev'} , goodAct);
                goodNext.bind( 'click', {action : 'next'} , goodAct);

                /*
                * 服务货架good-list控制函数
                * */
                function goodAct(eve){
                    debugger;
                    var moveWidth = 90;
                    var curLeft = $this.find('.good-list-wrap').scrollLeft();

                    if ( eve.data && eve.data.action === 'prev') {
                        goodWrap.scrollLeft( curLeft + moveWidth );
                    } else if ( eve.data.action === 'next' ){
                        goodWrap.scrollLeft( curLeft - moveWidth );
                    } else {
                        return false;
                    }
                }
            });

            return this;
        },
        //仅刷新good部分视图。
        refreshGoodsView : function(){
            this.$el.find('.good-list').html($(this.goodsTemplate(this.model.toJSON())));
        },
        addGoods : function () {
            var num = this.$('.multiNum').val();
            this.model.addGoods(num);
        },
        deleteOneGood : function(){
            this.model.deleteGoods(1);
        },
        deleteGoods : function () {
            var num = this.$('.multiNum').val();
            this.model.deleteGoods(num);
        },
        initGoodList : function(){
            this.render();
        }
    });

    var Day = Backbone.Model.extend({
        defaults : {
            date : null,
            dayMaxOrder : 10,
            dayDoneOrder : 2,
            dayEnable : true
        },
        initialize : function(){

        },
        addTimeBucket : function(){
            this.timeBuckets.add(new TimeBucket());
        }
    });

    var Days = Backbone.Collection.extend({
        model : Day,
        initialize : function(){

        }
    });

    var DayView = Backbone.View.extend({
        tagName : 'div',
        className : 'day-view',
        template : _.template($('#day_template').html(),templateSetting),
        events :　{
            'click .addTimeBucket' : 'addTimeBucket',
            'click .day_edit' : 'dayEditView',
            'click .day_enable' : 'dayEnableView'
        },
        initialize : function () {
            this.initDayView();
        },
        render : function(){
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },
        initDayView : function(){
            //不通过set方法，而是通过this属性直接设置timeBuckets属性为一个指向新的Collection的指针,这样是为了view中的listen可以正确的监听Collection对象。
            this.model.timeBuckets = new TimeBuckets();
            this.model.timeBuckets.url = '/timeBuckets.json';
            //this.model.timeBuckets.url = 'http://localhost:806/dianzhuapi.ashx';
            var requsetDate = this.model.get('date');

            this.listenTo(this.model.timeBuckets, 'add' , this.addTimeBucketView);
            this.render();
            this.model.timeBuckets.fetch({
                customAPI : true,
                protocolCode : 'slf002006',
                data : {
                    date : requsetDate,
                    serviceId: "8e431b59-cc9e-4a98-a1a6-a5830110e478"
                }
            });
        },
        addTimeBucketView : function(timeBucketModel){
            var timeBucketView = new TimeBucketView({model : timeBucketModel});
            this.$('.time-buckets').append(timeBucketView.render().el);
        },
        addTimeBucket : function(){
            this.model.addTimeBucket();
        },
        /*
        * 编辑状态View控制
        * */
        dayEditView : function(event){

            var dayEnable = this.$(".day_enable");

            if ( !dayEnable.get(0).checked ) {
                return false;
            } else {
                if ( event.target && event.target.checked )  {
                    return this._editViewControl('open');
                } else {
                    return this._editViewControl('close');
                }
            }
        },
        /*
        * 日开关View控制
        * */
        dayEnableView : function(event){
            var timeBuckets = this.$('.time-buckets');
            var dayEdit = this.$('.day_edit');
            // checked的改变和事件触发的顺序好像比较诡异，是checked的属性先改变然后在到触发事件，可以研究一下。
            debugger;
            if ( event.target && event.target.checked ) {
                timeBuckets.removeClass('t-b-close');
            } else {
                // 如果编辑视图开启,关闭编辑视图.
                if ( dayEdit.get(0).checked ) {
                    dayEdit.prop('checked', false);
                    this._editViewControl('close');
                }
                timeBuckets.addClass('t-b-close');
            }
        },
        /*
        * 编辑视图动作
        *
        * @Params : string,动作的名称.
        * */
        _editViewControl : function(toggle){

            var edit = this.$('.t-b-edit');
            var goodList = this.$(".good-list");

            if ( !toggle ) { return false; }
            if ( toggle === 'open') {
                edit.addClass('show');
                goodList.addClass('edit');
            } else {
                edit.removeClass('show');
                goodList.removeClass('edit');
            }

        }
    });

    var days = new Days();

    var appView = Backbone.View.extend({
        tagName : 'div',
        template : _.template($('#app_template').html(),templateSetting),
        events : {

        },
        initialize : function(){
            //debugger;
            days.url = '/days.json';
            //days.url = 'http://localhost:806/dianzhuapi.ashx';
            var today = new Date();
            var requestDate = today.getFullYear() + "-" + (today.getMonth() + 1) + '-' + today.getDate();
            var requestDateF = today.getFullYear() + "/" + (today.getMonth() + 1) + '/' + today.getDate();
            var _this = this;

            this.render();

            days.fetch({
                customAPI : true,
                protocolCode : 'slf001007',
                data : {
                    date : requestDate,
                    serviceId : "8e431b59-cc9e-4a98-a1a6-a5830110e478"
                },
                success : start
            });
            function start(collection, resp, options){
                //debugger;
                _this.initDayTab();
                var today = _.find(collection.models, function(model){
                    //debugger;
                    return model.get('date') === requestDate;
                });
                _this.addDayView(today);
            }
        },
        render : function(){
            this.$el.html(this.template());
            $('#goodShelf').append($(this.el));
            return this;
        },
        initDayTab: function(){
            var _this = this;
            var today = new Date();
            var requestDate = today.getFullYear() + "-" + (today.getMonth() + 1) + '-' + today.getDate();

            var tab = this.$('.day-tabs');
            _.each(days.models, function(day){
                tab.append($('<input type="button" class="day-tab">').val(day.get('date')));
                tab.find(".day-tab[value=" + requestDate + "]").addClass('active');
            });
            tab.find('.day-tab').on('click' , function(){
                var date = $(this).val();
                _this.showDayView(date);
                $(this).addClass('active').siblings().removeClass('active');
            });
        },
        showDayView: function(date){
            var goToDay = _.find(days.models, function(model){
                return model.get('date') === date;
            });
            this.addDayView(goToDay);
        },
        addDayView: function(dayModel){
            var dayView = new DayView({model : dayModel});
            this.$('.day-container').html(dayView.render().el);
        }
    });

    var newApp = new appView();
})
);

