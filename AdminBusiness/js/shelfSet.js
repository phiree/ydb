(function (){
    var plugName = "shelfSet";

    var merChantID = document.getElementById("merchantID").value;

    var ShelfSet = function(ele, options){
        this.$element = $(ele);
        this.$itemsContainer = $(this.$element.attr('data-setContainer'));
        this.$shelfSetTrigger = $(this.$element.attr('data-setTrigger'));
        this.$createBox = $(this.$element.attr('data-setCreate'));
        this.$createConfirm = $(this.$element.attr('data-setCreateCfm'));
        this.week = this.$element.attr('data-week');
        this.options = $.extend({}, ShelfSet.DEFAULTS, options);
        this.creating = 0;
    };

    ShelfSet.DEFAULTS = {
        itemTemplate : null,
        svcID : null,
        reqUrl : '',
        buildCallback : null

    };

    $.extend(ShelfSet.prototype, {
        init : function(){
            this.delegate();
            this.pullItems();
        },
        /**
         * 事件集中委派
         */
        delegate : function(){
            var _this = this;

            this.$shelfSetTrigger.bind('click.shelfSet', function(){
                _this._toggleCreate();
            });

            this.$createConfirm.bind('click.shelfSet', function(){
                _this.createConfirm();
            });
        },
        createConfirm : function(){
            var createData = this._getSetData(this.$createBox);


            if ( this.creating ){
                //this.buildItem(createData);
                this._createItemData(createData);
            }
        },
        pullItems : function(){
            this._selectItemsData();
        },
        buildItem : function(data){
            var tpl = this.options.itemTemplate,
                _this = this,
                itemHTML,
                $itemHTML,
                $container = this.$itemsContainer,
                callback =  this.options.buildCallback;

            if ( !tpl ){
                throw new Error("itemTemplate must be request");
            }

            if ( typeof data === "object" && data ){
                itemHTML = juicer(tpl, data);
            }

            $itemHTML = delegateItem($(itemHTML));

            if ( typeof callback === "function" ) {
                callback($itemHTML);
            }

            $container.append($itemHTML);

            /**
             * item子元素时间绑定
             * @param $itemHTML
             * @returns {*}
             */
            function delegateItem($itemHTML){
                var $itemH = $itemHTML;
                $itemH.find('[data-role="delete"]').bind('click', function(){
                    _this.removeItem($itemHTML);
                });
                $itemH.find('[data-role="startTime"]').bind('change.shelfSet', refresh);
                $itemH.find('[data-role="endTime"]').bind('change.shelfSet', refresh);
                $itemH.find('[data-role="maxOrder"]').bind('change propertychange', refresh);
                $itemH.find('[data-role="enable"]').bind('change propertychange', refresh);

                function refresh(){
                    var trigger = this;
                    _this.refreshItem(trigger);
                }

                return $itemH;
            }
        },
        removeItem : function($itemHTML){
            var $itemH = $itemHTML,
                wid;

            wid = $itemH.attr("data-wid");

            $itemH.remove();

            this._deleteItemData(wid);
        },
        _toggleCreate : function( ){
            var $createBox = this.$createBox;
            var $shelfSetTrigger = this.$shelfSetTrigger;

            if ( this.creating ){
                this.creating = 0;
                $createBox.removeClass('on');
                $shelfSetTrigger.removeClass('on');
            } else {
                this.creating = 1;
                $createBox.addClass('on');
                $shelfSetTrigger.addClass('on');
            }
        },
        endCreate : function(){

        },
        refreshItem : function (trigger){
            var refreshData,
                $trigger = $(trigger),
                $target;

            $target = $('[data-wid=' + $trigger.attr("data-target") + ']');

            refreshData = this._getSetData($target);
            this._updateItemData(refreshData)
        },
        /**
         * 增加数据
         * @param data
         * @private
         */
        _createItemData : function(data){
            var reqData,reqOptions,reqRow,wtObj;

            wtObj = $.extend(data, {
                open : "Y",
                tag : "默认工作时间"
            });

            reqRow = {
                merchantID : merChantID,
                svcID : Adapter.getParameterByName("serviceid"),
                workTimeObj : wtObj
            };

            if( typeof data === 'object' && data ){
                reqData = Adapter.reqPackage("WTM001001", reqRow )
            }

            reqOptions = {
                data : reqData,
                url : this.options.reqUrl,
                success : function(data, textStatus, jqXHR){
                    var repData = Adapter.respUnpack(data);
                }
            };
            this._sync(reqOptions);
        },
        /**
         * 删除数据
         * @param wid 要删除的workTimeID
         * @private
         */
        _deleteItemData : function(wid){
            var reqData = {},reqOptions;

            reqData.wid = wid;

            reqData = Adapter.reqPackage("WTM001002", reqData);

            reqOptions = {
                data : reqData,
                url : this.options.reqUrl,
                success : function(data, textStatus, jqXHR){

                }
            };

            this._sync(reqOptions);
        },
        /**
         * 更新数据
         * @param data
         * @private
         */
        _updateItemData : function(data){
            var reqOptions;
            reqOptions = {
                data : Adapter.reqPackage("WTM001003", data),
                url : this.options.reqUrl,
                success : function(data, textStatus, jqXHR){

                }
            };
            this._sync(reqOptions);
        },
        /**
         * 获取数据
         * @private
         */
        _selectItemsData : function(){
            var _this = this,
                reqData = {},
                reqOptions;

            if ( typeof this.week === 'string' ){
                reqData.week = this.week;
            }

            if ( !!this.options.svcID ) {
                reqData.svcID = this.options.svcID
            }

            reqData = Adapter.reqPackage("WTM001006", reqData);

            reqOptions = {
                data : reqData,
                url : this.options.reqUrl,
                success : function(data, textStatus, jqXHR){
                    var data = Adapter.respUnpack(data);

                    if ( data.hasArrayData ){
                        var arrayData = data.respData.arrayData;
                        for (var i = 0 ; i < arrayData.length ; i++){
                            _this.buildItem(arrayData[i]);
                        }
                    }
                }
            };

            this._sync(reqOptions);
        },
        _getSetData : function($setEle){
            var $ele = $setEle,
                data = {},
                $StartTime = $ele.find('[data-role="startTime"]'),
                $EndTime = $ele.find('[data-role="endTime"]'),
                $MaxNum = $ele.find('[data-role="maxOrder"]'),
                $week = $ele.find('[data-role="week"]');

            data = $.extend(data, {
                startTime : $StartTime.val(),
                endTime : $EndTime.val(),
                maxOrder : $MaxNum.val(),
                week : $week.val()
            });

            if ( data.maxOrder < 0 || !data.maxOrder ) {
                data.maxOrder = '0' ;
            }

            return data;
        },
        _sync : function(options){
            function urlError(){
                throw new Error('A "url" property must be specified.')
            }

            var params = {
                type : "post",
                dataType : 'json',
                data : {}
            };

            if ( !options.url ){
                params.url = options.url || urlError();
            }

            if ( options.data !== null && options.data ){
                params.data = options.data;
            }

            $.ajax($.extend(params, options));
        }
    });

    $.fn[plugName] = function(option){
        return this.each(function (){
            var $this = $(this);
            var data = $this.data("shelfSet");
            var options = $.extend({}, ShelfSet.DEFAULTS, typeof option === 'object' && option);

            if ( !data ) {
                $this.data("shelfSet", ( data = new ShelfSet(this, options)));
            }

            if ( typeof option === 'object' ){
                data.init()
            }
        })
    }
})();