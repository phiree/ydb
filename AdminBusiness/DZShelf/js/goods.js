
$(function(){
    // 时间段Model
    var TimeBucket = Backbone.Model.extend({
        defaults : {
            timeBucketId : 123,
            timeStart : "10:00",
            timeEnd : "12:00",
            maxNum : 5,
            doneNum : 2,
            timeEnable : true,
            arrayOrders : []
        },
        initialize : function(options){
            this.sortOrders();
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
        addOrderNum : function(Num){
                this.save({maxNum : this.get('maxNum') + Num});
        },
        setMaxNum : function(maxNum){
            if ( !!maxNum && ( maxNum > this.get('doneNum')) ){
                this.save({maxNum : maxNum});
            }
        },
        //orders按是否可以预定进行排序，已预订(ordered = true)放在前面;
        sortOrders : function(){
            var sortOrders = _.sortBy(this.get('arrayOrders') , function(order){
                return !order.ordered;
            });
            this.set('arrayOrders', sortOrders);
        },
        addOrders : function(num){
            var newOrder = {
                ordered : false,
                orderId : '#'
            };

            //复制数组为新数组，否则无法触发change事件
            var currentOrders = this.get('arrayOrders').slice();
            for ( var i = 0 ; i < num ; i++ ){
                currentOrders.push(newOrder);
            }

            var parentDate = this.get('parentDate');
            this.save('arrayOrders', currentOrders , {
                customAPI : true,
                protocolCode : 'GOOD001006',
                data : {
                    parentDate : parentDate
                }
            });
        },
        deleteOrders : function(num){
            //当未预约订单数大于减数时,才可以进行订单删除操作

            var orders = _.countBy(this.get('arrayOrders') , function(order){
                return !(order.ordered == false) ? 'ordered' : 'notOrdered'
            });
            if ( !orders.notOrdered || (orders.notOrdered < num) ) return;
            var currentOrders = this.get('arrayOrders').slice();
            for ( var i = 0 ; i < num ; i++ ){
                currentOrders.pop();
            }
            this.save('arrayOrders' , currentOrders);
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
        className : 'timeBucket',
        template : _.template($("#timeBucket_template").html()),
        ordersTemplate : _.template($("#orders_template").html()),
        events :　{
            'click .multiAdd' : 'addOrders',
            'click .multiDelete' : 'deleteOrders',
            'click .deleteOrder' : 'deleteOneOrder'
        },
        initialize : function(){
            //this.listenTo(this.model, "change" , this.render);
            this.listenTo(this.model, "change:arrayOrders" , this. refreshOrdersView);
        },
        render : function(){
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },
        //仅刷新order部分视图。
        refreshOrdersView : function(){
            this.$el.find('.order_list').html($(this.ordersTemplate(this.model.toJSON())))
        },
        addOrders : function () {
            var num = this.$('.multiNum').val();
            this.model.addOrders(num);
        },
        deleteOneOrder : function(){
            this.model.deleteOrders(1);
        },
        deleteOrders : function () {
            var num = this.$('.multiNum').val();
            this.model.deleteOrders(num);
        }
    });



    var Day = Backbone.Model.extend({
        defaults : {
            date : null,
            dayMaxOrder : 10,
            dayDoneOrder : 2,
            dayEnable : true,
            //timeBuckets : null
        },
        initialize : function(){

        },
        addTimeBucket : function(){
            this.timeBuckets.add(new TimeBucket({empty : true}));
        }
    });

    var Days = Backbone.Collection.extend({
        model : Day,
        initialize : function(){

        }
    });

    var DayView = Backbone.View.extend({
        tagName : 'div',
        className : 'day_view',
        template : _.template($('#day_template').html()),
        events :　{
            'click .addTimeBucket' : 'addTimeBucket'
        },
        initialize : function () {
            this.initDayView();
        },
        render : function(){
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },
        initDayView : function(){
            debugger;
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
                },
            });
        },
        addTimeBucketView : function(timeBucketModel){
            var timeBucketView = new TimeBucketView({model : timeBucketModel});
            timeBucketView.render();
            this.$('.time_list').append(timeBucketView.render().el);
        },
        addTimeBucket : function(){
            this.model.addTimeBucket();
        }
    });

    var days = new Days();

    var appView = Backbone.View.extend({
        tagName : 'div',
        template : _.template($('#app_template').html()),
        events : {

        },
        initialize : function(){
            days.url = '/days.json';
            //days.url = 'http://localhost:806/dianzhuapi.ashx';
            var today = new Date();
            var requestDate = today.getFullYear() + "-" + (today.getMonth() + 1) + '-' + today.getDate();
            var requestDateF = today.getFullYear() + "/" + (today.getMonth() + 1) + '/' + today.getDate();

            this.render();
            var _this = this;

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
                debugger;
                _this.initDayTab();
                var today = _.find(collection.models, function(model){
                    debugger;
                    return model.get('date') == requestDate
                });
                _this.addDayView(today);
            }
        },
        render : function(){
            this.$el.html(this.template());
            $('#goodsBox').append($(this.el));
            return this;
        },
        addDayView: function(dayModel){
            debugger;
            var dayView = new DayView({model : dayModel});
            this.$('.day_container').html(dayView.render().el);
        },
        initDayTab: function(){
            var _this = this;
            var tab = this.$('.day_tabs');
            _.each(days.models, function(day){
                tab.append($('<input type="button" class="day_tab">').val(day.get('date')));
            });

            tab.find('.day_tab').on('click' , function(){
                var date = $(this).val();
                _this.showDayView(date)
            })
        },
        showDayView: function(date){
            var gotoday = _.find(days.models, function(model){
                return model.get('date') == date
            });
            this.addDayView(gotoday);
        }
    });

    var newApp = new appView;
});