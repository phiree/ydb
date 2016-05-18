(function( factory ){
    if ( typeof define === "function" && define.amd ){
        define(['jquery'], function($){
            factory($);
        });
    } else {
        factory(jQuery);
    }
}(function($){
    var pluginName = "cascadeCheck";

    function CascadeCheck(element, options){
        this.$element = $(element);
        this.$searchInput = $(this.$element.attr("data-searchInput"));
        this.$searchConf = $(this.$element.attr("data-searchConf"));
        this.$searchClose = $(this.$element.attr("data-searchClose"));

        /* 参考bootstrap中的形式，第二次扩展options。感觉没必要，可能是为了类的options初始化和插件options的初始化单独分离 */
        this.options = $.extend({}, CascadeCheck.DEFAULTS, options);
        this.jsonData = {};
        this.searchReasult = {};
        this.checked = false;

        if ( typeof this.options.container === 'string' && this.options.container.length ){
            this.$container = $(this.options.container);
        } else {
            this.$container = this.$element;
        }
    }

    CascadeCheck.DEFAULTS = {
        // listLevel为false时，为动态多级列表
        listLevel : false,

        localData : true,
        data : {},

        dataSource : null,
        reqData : {},

        container : null,

        checkedCallBack : null,

        manualConfirm : true,
        ConfirmTrigger : null,

        submitTarget : null,
        submitCallback : null,

        printListCk : null,

        markSearch : true,

        list : 'ul',
        listClass : 'checkList',
        listItem : 'li',
        listItemClass　: 'checkListItem',
        listWrap : 'div',
        listWrapClass : 'checkListWrap'
    };

    $.extend( CascadeCheck.prototype , {
        /* 初始化 */
        init : function(){
            this._getData();

            this.initSearch();
            /* 提交前如果需要手动确认绑定 */
            if ( this.options.manualConfirm ) {
                this._bindConfirm();
            }
        },
        /* 构建列表 */
        build : function(){
            var printData;
            if ( !this.options.listLevel ) {
                printData = this._getDateByLevel(this.jsonData,0);
                this._printList(printData, 0);
            }
        },
        /* 搜索 */
        /* TODO：搜索开关待完善 */
        initSearch : function(){
            /* search事件绑定 */
            var _this = this;

            _this.$searchConf.on('click.cascadeCheck', function(){
                var str = _this.$searchInput.val();

                if ( str === "" ) { return; }

                str = trim(str);
                _this.search(str);
            });

            _this.$searchClose.on('click.cascadeCheck', function(){
                _this.$searchInput.val('');
                _this.$searchClose.removeClass('show');
                _this.closeSearch();
            });

            _this.$searchInput.on('keyup.cascadeCheck', function(e){
                if (e.keyCode === 13 ){
                    _this.$searchConf.click();
                }
                _this.$searchClose.addClass("show");
            });

            function trim (string){
                return string.replace(/(^\s*)|(\s*$)/g, '');
            }
        },
        search : function(searchStr){
            if ( !searchStr.length ) { return; }
            this.searchReasult = this._getJsonDataByStr(this.jsonData, searchStr);
            this.searching = true;

            if ( this.options.markSearch ){
                this.markId = this._getSearchFamilyId();
            }

            this.build();
        },
        closeSearch : function(){
            if ( this.searching ) {
                this.searching = false;

                this.build();
            }
        },
        _check : function( trigger ){

            this._checkId = $(trigger).attr('data-cid');
            this._checkText = $(trigger).text();

            if ( typeof this.options.checkedCallBack === 'function' ){
                this.options.checkedCallBack( this._checkId, this._checkText );
            }

        },
        _bindConfirm : function(){
            var _this = this;
            if ( this.options.confirmTrigger !== null && $(this.options.confirmTrigger).length ){
                $(this.options.confirmTrigger).bind('click.cascadeCheckConfirm', function(){
                    _this._submit();
                });
            }
        },
        _submit : function(){

            if ( this.options.submitTarget !== null && $(this.options.submitTarget).length ){
                // focus and blur to make the validate.js work normal.
                $(this.options.submitTarget).attr('value', this._checkId).focus().blur();
            }

            if ( typeof this.options.submitCallback === 'function'){
                /* 执行提交回调函数，
                 * @params : checked,是否选中列表提示
                 * */
                this.options.submitCallback( this.checked, this._checkId, this._checkText );
            }
        },
        /* 高亮列表中的搜索结果 */
        _markList : function( $list ){
            var $markList = $list;

            for ( var i = 0 ; i < this.markId.length ; i++ ){
                $markList.find('[data-cid="' + this.markId[i] + '"]').addClass('searchMark');
            }

            return $markList;
        },
        /* 列表构造器 */
        _listViewBuilder : function(data, level){
            var _this = this;
            var buildData = data;
            var $checklist = $('<' + this.options.list + '/>');

            $checklist.attr("data-level", level);
            for ( var i = 0 ; i < buildData.length ; i++ ){
                var $checklistItem = $('<' +　this.options.listItem + '/>');
                $checklistItem
                    .addClass(this.options.listItemClass)
                    .text(buildData[i].name)
                    .attr("data-level", level)
                    .attr("data-cid", buildData[i].id)
                    .attr("data-role", "checkItem")
                    .bind("click.checkList", clickHandler);

                if ( this._getDataByParentId(this.jsonData, buildData[i].id).length !== 0){
                    $checklistItem.addClass('par');
                }

                $checklist.append($checklistItem);
            }

            /* if searching , mark the result  */
            if ( this.searching && this.options.markSearch ){
                $checklist = this._markList($checklist);
            }

            function clickHandler(){
                $(this).addClass('selected').siblings().removeClass('selected');
                _this._refreshChildView(this);
            }

            return $checklist;
        },
        /* 点击上一级时，刷新下一级列表内容 */
        _refreshChildView : function( trigger ){
            var _this = this;
            var curLevel = parseInt($(trigger).attr("data-level"));
            var childLevel = curLevel + 1;
            var childData = this._getDataByParentId(this.jsonData, $(trigger).attr("data-cid"));

            /* 没有子数据时，进行选定操作 */
            if ( !childData.length ){
                this.checked = true;
                this._check(trigger);

                /* 如果为自动确认，则提交 */
                if ( !_this.options.manualConfirm ) {
                    _this._submit();
                }
            } else {
                this.checked = false;
            }

            this._printList(childData, childLevel);
        },
        /* 列表内容输出 */
        _printList : function( data , level){

            var $list = this._listViewBuilder(data , level);

            var $refreshListWrap = this.$container.find('.' + this.options.listWrapClass + '[data-level="' + level + '"]');

            if ( !data.length ){
                /* 如果数据为空，移除元素 */
                if ( typeof $refreshListWrap === 'object' && $refreshListWrap.length > 0 ) {
                    $refreshListWrap.nextAll('.' + this.options.listWrapClass).remove();
                    $refreshListWrap.remove();
                }
            } else {
                if ( typeof $refreshListWrap === 'object' && $refreshListWrap.length > 0 ) {
                    $refreshListWrap.html($list);
                    $refreshListWrap.nextAll('.' + this.options.listWrapClass).remove();
                } else {
                    // 包裹list
                    if ( typeof this.options.listWrap === 'string' && this.options.listWrap !== '' ){
                        var $printListWrap = $('<' + this.options.listWrap + '/>');
                        $printListWrap
                            .addClass(this.options.listWrapClass)
                            .attr("data-level", level)
                            .attr("data-role", "checkItemWrap")
                            .append($list);
                        this.$container.append($printListWrap);
                    }
                }
            }

            if ( typeof this.options.printListCk === 'function' ){
                this.options.printListCk();
            }

        },
        /* 返回搜索结果的家族Id数组 */
        _getSearchFamilyId : function( ){
            var searchResult = this.searchReasult;
            var arrResultId = [];
            var arrResultParentsId = [];

            for ( var i = 0 ; i < searchResult.length ; i++ ){
                arrResultId.push(searchResult[i].id);
                arrResultParentsId = $.merge(arrResultParentsId, this._getParentsID(searchResult[i]));
            }

            return $.merge(arrResultId, arrResultParentsId);
        },
        /* 返回单一项的所有父Id数组 */
        _getParentsID : function(item){
            var _this = this;
            var arrParentId = [];
            var parent = getParent(item);

            while ( parent.length !== 0){
                arrParentId.push(parent[0].id);
                parent = getParent(parent[0]);
            }

            function getParent (childItem){
                return $.grep( _this.jsonData, function(cur, index){
                    return cur.id === childItem.parent_id;
                });
            }

            return arrParentId;
        },
        /* 根据parent_Id查询数据 */
        _getDataByParentId : function(data, ParentId){
            var _data = data ? data : this.jsonData;
            return $.grep(_data, function(cur,index){
                return cur.parent_id === ParentId;
            });
        },
        _getDateByLevel : function(data, level){
            return $.grep(data, function(cur, index){
                return cur.level === level;
            });
        },
        _getJsonDataByStr : function(data, str){
            var _data = data ? data : this.jsonData;
            return $.grep(_data, function(cur, index){
                var reg = new RegExp(str);
                return reg.test(cur.name);
            });
        },
        _getData : function(){
            var _this = this;
            if ( this.options.localData && typeof this.options.data === 'object' ){
                this.jsonData = this.options.data;
                this.build();
            } else if ( !this.options.localData && typeof this.options.dataSource === 'string' ) {
                /* TODO: ajax获取数据待完善，待测试 */
                $.ajax({
                    url : this.options.dataSource,
                    dataType : 'json',
                    data : this.options.reqData,
                    success : function (data, textStatus, xhr) {
                        _this.jsonData = data;
                        _this.build();
                    }
                });
            }
        },
    });

    $.fn[pluginName] = function(option){
        return this.each(function(){
            var $this = $(this);
            var data = $this.data('cascadeCheck');
            /* 避免option不为object时，参数错误，第一次初始化option。 */
            var options = $.extend({}, CascadeCheck.DEFAULTS, typeof option === 'object' && option );

            if ( !data ) {
                $this.data('cascadeCheck', ( data = new CascadeCheck( this, options)));
            }

            if ( typeof option === 'object' ){
                data.init();
            }

            if ( typeof option === 'string' ) {
                data[option]();
            }
        });
    };

}));