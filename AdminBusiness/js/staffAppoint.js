/*
 * a tool to appoint staff by ajax.
 * */

/* standard by AMD , for the future */
(function (factory) {
    if (typeof define === 'function' && define.amd) {
        defined(['jquery'], factory);
    } else {
        factory($);
    }
}(function ($) {
    var pluginName = 'staffAppoint';

    /* constructor */
    function StaffAppoint(element, options) {
        this.$element = $(element);
        this.options = $.extend({}, StaffAppoint.DEFAULTS, options);
        this.$appointSubmit = $(this.options.appointSubmit);
        this.$staffContainer = $(this.options.staffContainer);
        this.appointId = this.$element.attr('data-appointOrderId');
    }

    /* defaults */
    StaffAppoint.DEFAULTS = {
        lightBox: null,
        staffContainer: null,
        appointSubmit: null
    };

    $.extend(StaffAppoint.prototype, {
        toggle: function () {
            var lightBox = this.options.lightBox;
            this.$appointSubmit.attr('data-appointOrderId', this.appointId);

            /* 弹出弹窗 */
            if (lightBox && typeof lightBox === 'function') {
                lightBox();
                this.pull();
            } else {
                return;
            }
        },
        appoint: function () {
            var checkStaffs = this.$staffContainer.find('[data-role="staff"]:checked');
            var arrCheckId = [];
            var appointJSON = {};

            if ( !checkStaffs.length ) { return false; }
            if ( this.options.single && checkStaffs.length > 1 ) { return false; }
            if (!this.options.single) {
                for (var i = 0; i < checkStaffs.length; i++) {
                    arrCheckId.push(this.attr('data-staffId'));
                }
            } else {
                arrCheckId.push(checkStaffs.eq(0).attr('data-staffId'));
            }
            $.extend(appointJSON, {
                arrCheckStaff: arrCheckId,
                appointOrderId: this.appointId
            });
            this.upload(appointJSON);
        },
        upload: function (jsonData) {
            $.ajax({
                url : '/staff.json',
                dataType : 'json',
                data : jsonData,
                success : function(msg){
                    alert('appoint success');
                }
            });
        },
        pull : function (jsonData){
            var _this = this;
            var pullData = $.extend( jsonData, typeof this.options.pullReqData === 'object' && this.options.pullReqData );
            $.ajax({
                url : '/staff.json',
                dataType : 'json',
                data : pullData,
                success : function(data, textStatus, xhr){
                    _this.refreshStaff(data);
                }
            });
        },
        /* 在每次指派员工前，通过ajax获取新的员工状态
         * 通过underscore.js模版引擎来刷新页面视图,自定义引用符号为"{% code %}",以免与asp.net的引用符号冲突
         * */
        refreshStaff : function(jsonData){
            var staffTemplate = _.template($('#staffs_template').html() ,{
                interpolate: /\{%=(.+?)%\}/g,
                escape:      /\{%-(.+?)%\}/g,
                evaluate:    /\{%(.+?)%\}/g
            });
            this.$staffContainer.html(staffTemplate(jsonData));
        }
    });

    $.fn[pluginName] = function (option) {
        return this.each(function () {
            var data = $.data(this, "staffApt");
            var options = $.extend({}, StaffAppoint.DEFAULTS, typeof option === 'object' && option);
            if (!data) {
                $.data(this, "staffApt", data = new StaffAppoint(this, options));
            }
            if (typeof option === 'string') {
                data[option]();
            }
        });
    };

    /* 绑定submit到触发开关，使其能在一个元素上保存data数据 */
    function getToggleFromSubmit($submit) {
        return $('[data-role="staffAptToggle"][data-appointOrderId="' + $submit.attr('data-appointOrderId') + '"]');
    }

    $(document).on('click.StaffApt', '[data-role="staffAptToggle"]', function (e) {
        e.preventDefault();
        var $this = $(this);
        $.fn[pluginName].call($this, 'toggle');
    });
    $(document).on('click.StaffApt', '[data-role="appointSubmit"]', function (e) {
        e.preventDefault();
        var $this = $(this);
        var $toggle = getToggleFromSubmit($this);
        $.fn[pluginName].call($toggle, 'appoint');
    });
}));